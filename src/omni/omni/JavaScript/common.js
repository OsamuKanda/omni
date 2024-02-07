/*--------------------------------------------------------------------------
* JavaScript, version 0.0.1
*  (c) 2010 TACHIBANA SOLUTIONS PLAZA LTD. 
*
*  Make t.nii 2010/05/19 Ver 0.0.0
*  Make t.nii 2011/12/01 Ver 1.0.0
*--------------------------------------------------------------------------*/
/**************************************************************/
/*                                                            */
/* 画面制御用グローバル変数定義                               */
/* 　　　　　　　　　　　　　　　　　　                       */
/**************************************************************/
var meFocus = null;         // 自身のフォーカスエレメント
var nextFocus = null;       // 次のフォーカスエレメント
var UpDownElm = null;       // 次フォーカスの昇順、降順
var mainBtn = null;
var objclient = new Array;  // ClientElementData
var objbtn = new Array;     // Search

var oldText = '';           // OnFocus時の値
var NowElm = null;          // OnFocus時のエレメント

var lastPostBackElement = ''; //最後のPostBack要求を記憶
var SubmitChak = false;     // サブミットボタン押下可否フラグ(true= 変更可能)
var NGFocus = false;        // Enter以外で抜けた場合のValiNGフラグ(true= 元のフォーカス位置)
var strResult = null;
var errCode = '0';
var errMsg = new Array;
errMsg.push(['0', '']);
errMsg.push(['1', '']); // ServerMessege
errMsg.push(['2', '']); // SearchLookUp
errMsg.push(['10', '']); // SessionTimeout
errMsg.push(['11', '']); // ScrollControll
errMsg.push(['100', '']);
/**************************************************************/
/*                                                            */
/* 画面キー制御                                               */
/* 　　　　　　　　　　　　　　　　　　                       */
/**************************************************************/
window.document.ondblclick = dbClick;
function dbClick() {
    return false;
}
window.document.onkeydown = KeyPress;
function KeyPress() {
    if (event != null) {

        //　イベントを利用済みの場合は、イベントを無効にする。
        if (event.keyCode == 0) {
            return false;
        }
        //バックスペースキーを無効にする(HistryBack抑止)
        if (event.keyCode == 8) {
            if (document.activeElement.tagName == 'TEXTAREA') {
                // テキストエリアは有効(readOnry時は無効)
                if (document.activeElement.readOnly == true) {
                    return false;
                }
            } else if ((document.activeElement.tagName == 'INPUT') && (document.activeElement.type == 'button')) {
                // ボタンは無効
                return false;
            } else if ((document.activeElement.tagName == 'INPUT') && (document.activeElement.type == 'text')) {
                // テキストボックスは有効
            } else if ((document.activeElement.tagName == 'INPUT') && (document.activeElement.type == 'checkbox')) {
                // チェックボックスは無効
                return false;
            } else if (document.activeElement.tagName == 'SELECT') {
                // ドロップダウンリスとは無効
                return false;
            } else if (document.activeElement.type == 'image') {
                // イメージボタンは無効
                return false;
            } else {
                return false;
            }
        }
        // ショートカットキーの無効化
        if (event.altKey == true) {
            switch (event.keyCode) {
                case 37: //←
                case 39: //→
                    event.returnValue = false;
                    return false;
                case 90: //Z ★
                case 36: //HOME ★
                    event.keyCode = 0;
                    event.returnValue = false;
                    return false;
            }
        }
        if (event.ctrlKey == true) {
            switch (event.keyCode) {
                case 66:   // B
                case 68:   // D
                case 69:   // E
                case 72:   // H
                case 73:   // I
                case 74:   // J
                case 82:   // R
                case 107:  // + ★
                case 187:  // + ★
                case 109:  // - ★
                case 189:  // - ★
                    event.keyCode = 0;
                    event.returnValue = false;
                    return false;
            }
        }
        if ((event.keyCode == 121) && (event.shiftKey == true)) return false;  // SHIFT + F10
        
        // 特記事項の場合は、改行を入力する
        var blnFlg = true;
        if (document.activeElement.tagName == 'TEXTAREA') {
            if (event.keyCode == 13) {
                switch (idConv(NowElm.id)) {
                    case 'TOKKI':   // B
                    case 'KOSHO':   // D
                    case 'GENIN':   // E
                    case 'TAISHO':   // H
                        blnFlg = false;
                        break;
                    default:
                        break;
                }
            }
        }
        if (blnFlg) {
            // EnterKeyが押された場合(未使用)の場合は、最後のエレメントにフォーカスする。
            if (event.keyCode == 13) {
                event.keyCode = 0;
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm.get_isInAsyncPostBack() == false) {
                    // postback中でない場合のみフォーカスセットする
                    var elm = checkElmID(meFocus);
                    if (elm != null) {
                        elm.focus();
                    } else {
                        elm = getNextFocusElm(0, 'cli');
                        if (elm != null) {
                            elm.focus();
                        }
                    }
                }
                return false;
            }
            

            // Function Key(F1～F12)を無効にする。
            if (event.keyCode >= 112 && event.keyCode <= 123) {
                var code = event.keyCode;
                if (event.keyCode != 123) {
                    event.keyCode = 0;
                }
                var strMode;
                switch (code) {
                    case 112: strMode = 'btnNext'; break;
                    case 113: strMode = 'btnF2'; break;
                    case 114: strMode = 'btnSubmit'; break;
                    case 115: strMode = 'btnF4'; break;
                    case 116: strMode = 'btnF5'; break;                    
                    case 117: strMode = 'btnPre'; break;
                    case 118: strMode = 'btnF7'; break;
                    case 119: strMode = 'btnExcel'; break;
                    case 120: strMode = 'btnBefor'; break;
                    case 123: strMode = 'btnclear'; break;
                    default: return false; break;
                }
                var elm;
                for (i = btnCom.length - 1; i >= 0; i--) {
                    if (btnCom[i][1] == strMode) {
                        elm = document.getElementById(btnCom[i][0]);
                        if (elm.disabled == false) {
                            if (document.activeElement.id == elm.id) {
                                elm.click();
                            } else {
                                mainBtn = elm.id;
                                elm.focus();
                            }
                            break;
                        }
                    }
                }
                return false;
            }
        }
    }
}

/**************************************************************/
/*                                                            */
/* Window表示制御                                              */
/*                                                            */
/**************************************************************/
// -------------------------------------------------------------
// daialog表示イベント
// -------------------------------------------------------------
// return :    : 
// -------------------------------------------------------------
function WindowOpen(url, mode, option) {
    return window.showModalDialog(url, mode, option);
}
// -------------------------------------------------------------
// WindowOpenイベント
// -------------------------------------------------------------
// return :    : 
// -------------------------------------------------------------
function WindowNewOpen(url, mode, option) {
    return window.open(url, mode, option);
}
/**************************************************************/
/*                                                            */
/* element制御                                              */
/*                                                            */
/**************************************************************/
// -------------------------------------------------------------
// element制御データ
// Focusの背景色データ
// -------------------------------------------------------------
// return :    : 
// -------------------------------------------------------------
var objelmdata = {
    Color: function(arg) {
        switch (arg) {
            case 'FocusON': strColor = '#ffff99'; break;
            case 'FocusOFF': strColor = ''; break;
            case 'ValiNGON': strColor = '#ff0000'; break;
            case 'ValiNGOFF': strColor = '#ff9999'; break;
            case 'MouseON': strColor = '#ccffff'; break;
            case 'MouseOFF': strColor = ''; break;
            case 'ModeBtnBgcON': strColor = '#333399'; break;
            case 'ModeBtnBgcOFF': strColor = ''; break;
            case 'ModeBtnStrON': strColor = '#FFFFFF'; break;
            case 'ModeBtnStrOFF': strColor = ''; break;
            default: strColor = ''; break;
        }
        return strColor;
    }
}

/**************************************************************/
/*                                                            */
/* 文字列制御                                                  */
/*                                                            */
/**************************************************************/
// -------------------------------------------------------------
// 先頭の空白を削除
// -------------------------------------------------------------
// return : str : 文字列
// -------------------------------------------------------------
String.prototype.ltrim = function() {
    return this.replace(/^\s+/, '');
}
// -------------------------------------------------------------
// 末尾の空白を削除
// -------------------------------------------------------------
// return : str : 文字列
// -------------------------------------------------------------
String.prototype.rtrim = function() {
    return this.replace(/\s+$/, '');
}
// -------------------------------------------------------------
// 先頭および末尾の空白を削除
// -------------------------------------------------------------
// return : str : 文字列
// -------------------------------------------------------------
String.prototype.trim = function() {
    return this.replace(/^\s+|\s+$/g, '');
}
/**************************************************************/
/*                                                            */
/* サーバーとのやり取りを文字列、配列に置き換える             */
/*                                                            */
/**************************************************************/
// -------------------------------------------------------------
// 配列elementデータをサーバー提供用の文字列に置き換える。
// -------------------------------------------------------------
// return : str : 文字列
// -------------------------------------------------------------
function ClientControl() {
    var str = '{';
    var i, j;
    for (i = 0; i < objclient.length; i++) {
        for (j = 0; j < objclient[i].length; j++) {
            if (j == 0) str += '{"' + objclient[i][j] + '"';
            else if (j == (objclient[i].length - 1)) str += ', "' + objclient[i][j] + '"}';
            else str += ', "' + objclient[i][j] + '"';
        }
        if (i == (objclient.length - 1)) str += '}';
        else str += ',';
    }
    var elm = document.getElementById(CliCon);
    //elm.value = str;
    elm.value = '';
    return false;
}
// -------------------------------------------------------------
// 配列elementデータをサーバーから取得し配列に置き換える。
// -------------------------------------------------------------
// return : str : 文字列
// -------------------------------------------------------------
function setControl() {
    var elm = document.getElementById(CliCon);
    try {
        objclient = eval(elm.value);
    }
    catch (ex) {
        alert(ex + ' objclientの値が不正です。');
    }
}
// -------------------------------------------------------------
// 配列elementデータをサーバーから取得し配列に置き換える。
// -------------------------------------------------------------
// return : str : 文字列
// -------------------------------------------------------------
function setBtnControl() {
    var elm = document.getElementById(SeaCon);
    try {
        if (elm.value != '') {
            objbtn = eval(elm.value);
        } else {
            if (objbtn.length == 0) {
                objbtn = searchBtn;
            }
        }
    }
    catch (ex) {
        alert(ex + ' img及び検索ボタンの値が不正です。');
    }
}
// -------------------------------------------------------------
// 配列elementデータをサーバーから取得し配列に置き換える。
// -------------------------------------------------------------
// return : str : 文字列
// -------------------------------------------------------------
function getControl() {
    var elm = document.getElementById(CliCon);
    if (elm.value == '') return;
    var i, j;
    try {
        var obj = eval(elm.value);
        if (obj instanceof Array) {
            // 動的部分と静的部分に分割する
            var aryServer = new Array;
            var aryClientUp = new Array;
            var aryClientDown = new Array;
            for (i = 0; i < obj.length; i++) {
                if (obj[i][2] == '1') {
                    aryServer.push(obj[i]);
                }
            }

            if (aryServer.length > 0) {
                // 動的部分の同一IDを削除する。
                var servTmp = new Array;
                var aryServer2 = new Array;
                var bTmp = true;
                for (i = 0; i < aryServer.length; i++) {
                    bTmp = true;
                    for (j = 0; j < servTmp.length; j++) {
                        if (aryServer[i][0] == servTmp[j]) {
                            bTmp = false;
                            break;
                        }
                    }
                    if (bTmp) {
                        servTmp.push(aryServer[i][0]);
                        aryServer2.push(aryServer[i]);
                    }
                }

                // 現在のデータを分割する
                var bAryTmp = false;
                var bAryTmp2 = false;
                for (i = 0; i < objclient.length; i++) {
                    if ((objclient[i][2] == '0') && (bAryTmp == false)) {
                        aryClientUp.push(objclient[i]);
                    } else if ((objclient[i][2] == '0') && (bAryTmp == true)) {
                        aryClientDown.push(objclient[i]);
                    } else {
                        aryServer.push(obj[i]);
                        bAryTmp = true;
                    }
                }
                // データを結合する
                var newdata = aryClientUp.concat(aryServer2, aryClientDown);
            } else {
                var newdata = objclient;
            }


            // Client静的データ分を差し替える
            var j;
            for (i = 0; i < newdata.length; i++) {
                for (j = 0; j < obj.length; j++) {
                    if (newdata[i][0] == obj[j][0]) {
                        newdata[i] = obj[j];
                    }
                }
            }
            objclient = newdata;
            elm.value = '';
            elmControl(obj);
        }
    }
    catch (ex) {
        alert(ex + ' objclientの値が不正です。');
    }
    return false;
}

// -------------------------------------------------------------
// PostBack完了後に、VariNGのelementの背景色を変更する。
// -------------------------------------------------------------
// return : str : 文字列
// -------------------------------------------------------------
function elmControl(elmArray) {
    //****************************************************************
    // objclient の配列No
    // 0  ClientID　　　 // サーバー作成ID
    // 1  ID             // クライアントコントロール用ID
    // 2  DataType       // 0:静的データ 1:動的データ(ListViewで利用)
    // 3  Validate       // Validateのパラメータ
    // 4  ValidateOKNG   // 0:OK 1:NG
    // 5  SendFlg        // sendflg
    // 6  Default        // 初期値 主にGroupがGの行をクリアする際に利用(伝票形式のみ？)
    // 7  AJAX_Bottom    // AJBtn名(ValiOK時に押すボタン名称(ID))
    // 8  Group          // グループ名  keyElm,mainElm,G0～G7？(これは伝票形式のレコードグループ)
    // 9  Diasble        // 活性非活性       0:不可 1:可 
    // 10 SetFocus       // SetFocus可否設定 0:不可 1:可
    //******************************************************************
    var i;
    var elm;
    var acFocus = null;
    for (i = 0; i < elmArray.length; i++) {
        elm = checkElmID(elmArray[i][0])
        if (elm) {
            // フォーカス設定ON可否設定
            if ((elmArray[i][9] == undefined) || (elmArray[i][9] == '0')) {
                elm.style.backgroundColor = objelmdata.Color('FocusOFF');
                if (elm.type == 'button') {
                    elm.value = chkBtnElm(elmArray[i], elm.value);
                }
                if (elm.tagName != "SPAN") {
                    elm.disabled = true;
                }
            } else {
                elm.style.backgroundColor = objelmdata.Color('FocusOFF');
                if (elm.type == 'button') {
                    elm.value = chkBtnElm(elmArray[i], elm.value);
                }
                elm.disabled = false;
            }
            // backcolorの設定
            if (elm.type != 'button') {
                if (elmArray[i][4] == '0') {
                    if (document.activeElement == null) {

                    } else {
                        if (document.activeElement.id == elm.id) {
                            elm.style.backgroundColor = objelmdata.Color('FocusON');
                            acFocus = elm;
                        } else {
                            elm.style.backgroundColor = objelmdata.Color('FocusOFF');
                        }
                    }
                } else {
                    if (document.activeElement == null) {

                    } else {
                        if (document.activeElement.id == elm.id) {
                            elm.style.backgroundColor = objelmdata.Color('ValiNGON');
                        } else {
                            elm.style.backgroundColor = objelmdata.Color('ValiNGOFF');
                        }
                        elm.style.backgroundColor = objelmdata.Color('ValiNGOFF');
                        //elmArray[i][4] = '0';
                    }
                }
            }
        }
    }
}

function chkBtnElm(elmArray, str) {
    var strRet = str;
    switch (elmArray[1]) {
        case "btnNext":
            if (elmArray[9] == '0') {
                strRet = ' ';
            } else if ((aspxMode == 'Input OMN605') ||(aspxMode == 'Input OMN607')){
                strRet = 'F1 確認登録';
            } else {
                strRet = 'F1 売上';
            }
            break;
        case "btnF2":
            if (elmArray[9] == '0') strRet = ' ';
            else strRet = 'F2 全選択';
            break;
        case "btnSubmit":
            if (elmArray[9] == '0') {
                strRet = ' ';
            } else if ((aspxMode == 'Batch') || (aspxMode == 'Report')) {
                strRet = 'F3 実行';
            } else {
                //(HIS-057) if (((aspxMode == 'Master') || (aspxMode == 'Input')) && (document.getElementById(hidMode).value == '2')) {
            if (((aspxMode == 'Master') || (aspxMode == 'Input') || (aspxMode == 'Input OMN605') || (aspxMode == 'Input OMN607')) && (document.getElementById(hidMode).value == '2')) {     //(HIS-057)
                    if (typeof delBtnName != 'undefined') {
                        strRet = delBtnName;
                    } else {
                        strRet = 'F3 削除';
                    }
                } else {
                    strRet = 'F3 登録';
                }
            }
            break;
        case "btnF4":
            if (elmArray[9] == '0') strRet = ' ';
            else strRet = 'F4 全解除';
            break;
        case "btnF5":
            if (elmArray[9] == '0') strRet = ' ';
            else strRet = 'F5 チェック削除';
            break;
        case "btnPre":
            if (elmArray[9] == '0') {
                strRet = ' ';
            } else if ((aspxMode == 'Batch') || (aspxMode == 'Report')) {
                strRet = 'F6 プレビュー';
            } else {
                if (aspxMode == 'Inquiry OMN203') {
                    strRet = 'F6 CSV出力';
                } else {
                    strRet = 'F6 点検履歴';
                }
            }
            break;
        case "btnF7":
            if (elmArray[9] == '0') {
                strRet = ' ';
            } else{
                if (aspxMode == 'Inquiry OMN203') {
                    strRet = 'F7 ダウンロード';
                }else{
                    strRet = 'F7 号機別';
                }
            }
            break;
        case "btnExcel":
            if (elmArray[9] == '0') strRet = ' ';
            else strRet = 'F8 Excel';
            break;
        case "btnBefor":
            if (elmArray[9] == '0') strRet = ' ';
            else strRet = 'F9 終了';
            break;
        case "btnclear":
            if (elmArray[9] == '0') strRet = ' ';
            else if (aspxMode == 'Report') strRet = 'F12 キャンセル';
            else strRet = 'F12 クリア';
            break;
        default:
            break;
    }
    return strRet;
}
/**************************************************************/
/*                                                            */
/* Focus制御                                                  */
/*                                                            */
/**************************************************************/
// -------------------------------------------------------------
// エレメントがキーかどうかを返す。
// -------------------------------------------------------------
// arg[0] : elm : element
// return : bool: キーならtrue
//        :     : false
// -------------------------------------------------------------
function chkKeyElm(elm) {
    var bRet = false;
    for (i = 0; i < objclient.length; i++) {
        if (objclient[i][8] == 'keyElm') {
            if (objclient[i][0] == elm.id) {
                bRet = true;
                break;
            }
        }
    }
    return bRet;
}
// -------------------------------------------------------------
// エレメントが有効かを返す。
// -------------------------------------------------------------
// arg[0] : num : objclient配列No
// return : elm : element
//        :     : null
// -------------------------------------------------------------
function checkElm(nNum, mode) {
    if (mode == 'sev') {
        var elm = document.getElementById(objclient[nNum][0]);
        if (elm) {
            if (((elm.tagName == 'INPUT') || (elm.tagName == 'TEXTAREA') || (elm.tagName == 'SELECT')) && (elm.type != 'image')) {
                return elm;
            }
        }
    } else {
        if ((objclient[nNum][9] != '0') && (objclient[nNum][10] != '0')) {
            var elm = document.getElementById(objclient[nNum][0]);
            if (elm) {
                if (((elm.tagName == 'INPUT') || (elm.tagName == 'TEXTAREA') || (elm.tagName == 'SELECT')) && (elm.type != 'image')) {
                    if (elm.disabled != true) return elm;
                }
            }
        }
    }
    return null;
}
// -------------------------------------------------------------
// エレメントが有効かを返す。
// -------------------------------------------------------------
// arg[0] : str : プレフィックス有りelementID
// return : elm : element
//        :     : null
// -------------------------------------------------------------
function checkElmID(strElmID) {
    var elm = document.getElementById(strElmID);
    if (elm) {
        if ((elm.tagName == 'INPUT') || (elm.tagName == 'TEXTAREA') || (elm.tagName == 'SELECT') || (elm.tagName == 'SPAN')) {
            return elm;
        }
    }
    return null;
}
// -------------------------------------------------------------
// idをプレフィックス有無を切り替える。
// -------------------------------------------------------------
// arg[0] : str : elementID
// return : str : elementID
//        :     : 
// -------------------------------------------------------------
function idConv(strID) {
    var i;
    var strRet = '';
    for (i = 0; i < objclient.length; i++) {
        if (objclient[i][1] == strID) { strRet = objclient[i][0]; break; }
        if (objclient[i][0] == strID) { strRet = objclient[i][1]; break; }
    }
    return strRet;
}
// -------------------------------------------------------------
// エレメントIDを検索し、そのエレメントを返す。
// フォーカスできなければ、次のエレメントを返す。
// -------------------------------------------------------------
// arg[0] : str : プレフィクス無しelementID
// return : elm : element
//        :     : null
// -------------------------------------------------------------
function getElm(elmId) {
    var retElm = null;
    var i;
    for (i = 0; i < objclient.length; i++) {
        if (objclient[i][1] == elmId) { retElm = checkElm(i, 'cli'); break; }
    }
    if (retElm == null) { retElm = getNextFocusElm((i + 1), 'cli'); }
    return retElm;
}
// -------------------------------------------------------------
// エレメントIDを検索し、そのエレメントNoを返す。
// フォーカスできなければ、次のエレメントNoを返す。
// -------------------------------------------------------------
// arg[0] : str : プレフィクス無しelementID
// return : num : objclient配列No
//        :     : null
// -------------------------------------------------------------
function getElmNo(elmId) {
    var chkElm = null;
    var retElmNo = null;
    var i;
    for (i = 0; i < objclient.length; i++) {
        if (objclient[i][1] == elmId) { chkElm = checkElm(i, 'cli'); retElmNo = i; break; }
    }
    if (chkElm == null) { retElmNo = getNextFocusElmID((i + 1), 'cli'); }
    return retElmNo;
}
// -------------------------------------------------------------
// 次のエレメントを検索し、エレメントを返す(降順)
// -------------------------------------------------------------
// arg[0] : num : objclient配列No
// return : elm : element
//        :     : null
// -------------------------------------------------------------
function getNextFocusElm(nNum, mode) {
    var elm;
    var retElm = null;
    var i, j;
    if (nNum >= objclient.length) nNum = 0;
    if (nNum > 0) {
        for (i = nNum; i < objclient.length; i++) {
            elm = checkElm(i, mode);
            if (elm != null) { retElm = elm; break; }
            if (i == (objclient.length - 1)) {
                // 先頭からやり直す
                for (j = 0; j < nNum; j++) {
                    elm = checkElm(j, mode);
                    if (elm != null) { retElm = elm; break; }
                }
                break;
            }
        }
    } else {
        for (i = 0; i < objclient.length; i++) {
            elm = checkElm(i, mode);
            if (elm != null) { retElm = elm; break; }
        }
    }
    return retElm;
}
// -------------------------------------------------------------
// 次のエレメントを検索し、エレメントを返す(昇順)
// -------------------------------------------------------------
// arg[0] : num : objclient配列No
// return : elm : element
//        :     : null
// -------------------------------------------------------------
function getNextFocusElmE(nNum, mode) {
    var elm;
    var retElm = null;
    var i, j;

    if (nNum >= 0) {
        for (i = nNum; i >= 0; i--) {
            elm = checkElm(i, mode);
            if (elm != null) { retElm = elm; break; }
            if (i == 0) {
                // 先頭からやり直す
                for (j = (objclient.length - 1); j > nNum; j--) {
                    elm = checkElm(j, mode);
                    if (elm != null) { retElm = elm; break; }
                }
                break;
            }
        }
    } else {
        for (i = (objclient.length - 1); i >= 0; i--) {
            elm = checkElm(i, mode);
            if (elm != null) { retElm = elm; break; }
        }
    }
    return retElm;
}
// -------------------------------------------------------------
// 次のエレメントを検索し、配列Noを返す(降順)
// -------------------------------------------------------------
// arg[0] : num : objclient配列No
// return : elm : element
//        :     : null
// -------------------------------------------------------------
function getNextFocusElmID(nNum, mode) {
    var elm;
    var retElmID = null;
    var i, j;
    if (nNum >= objclient.length) nNum = 0;
    if (nNum > 0) {
        for (i = nNum; i < objclient.length; i++) {
            elm = checkElm(i, mode);
            if (elm != null) { retElmID = i; break; }
            if (i == (objclient.length - 1)) {
                // 先頭からやり直す
                for (j = 0; j < nNum; j++) {
                    elm = checkElm(j, mode);
                    if (elm != null) { retElmID = j; break; }
                }
                break;
            }
        }
    } else {
        for (i = 0; i < objclient.length; i++) {
            elm = checkElm(i, mode);
            if (elm != null) { retElmID = i; break; }
        }
    }
    return retElmID;
}
// -------------------------------------------------------------
// 次のエレメントを検索し、配列Noを返す(昇順)
// -------------------------------------------------------------
// arg[0] : num : objclient配列No
// return : elm : element
//        :     : null
// -------------------------------------------------------------
function getNextFocusElmIDE(nNum, mode) {
    var elm;
    var retElmID = null;
    var i, j;
    if (nNum < 0) nNum = objclient.length - 1;
    for (i = (nNum); i >= 0; i--) {
        elm = checkElm(i, mode);
        if (elm != null) { retElmID = i; break; }
        if (i == 0) {
            // 先頭からやり直す
            for (j = (objclient.length - 1); j > nNum; j--) {
                elm = checkElm(j, mode);
                if (elm != null) { retElmID = j; break; }
            }
            break;
        }
    }
    return retElmID;
}
// -------------------------------------------------------------
// 今の配列Noを返す。
// -------------------------------------------------------------
// arg[0] : str : プレフィクス有りelementID
// return : num : objclient配列No
//        :     : null
// -------------------------------------------------------------
function getNowFocusElmID(strID) {
    var retElmID = null;
    var i;
    for (i = 0; i < objclient.length; i++) {
        if (strID == objclient[i][0]) { retElmID = i; break; }
    }
    return retElmID;
}
// -------------------------------------------------------------
// Hiddenに次のフォーカスをセットする
// -------------------------------------------------------------
// arg[0] : num : objclient配列No
// arg[1] : num : objclient配列No
// return :     : 
// -------------------------------------------------------------
function hidstrFocusSet(nNextID, nNGID, strOrder) {
    var hidNextFocus = document.getElementById(hidFocus);
    var strFocus = getStrFocus(nNextID);
    strFocus += '___';
    strFocus += getStrFocus(nNGID);
    strFocus += '___';
    strFocus += strOrder;
    hidNextFocus.value = strFocus;
    return;
}
// -------------------------------------------------------------
// Hiddenにフォーカスをセット用テキストを返す
// -------------------------------------------------------------
// arg[0] : num : objclient配列No
// return : str : サーバーセット用のelement名
// -------------------------------------------------------------
function getStrFocus(nID) {
    var strFocus = '';
    var focusElm = document.getElementById(objclient[nID][0]);
    if (focusElm.tagName == 'INPUT') {
        if (focusElm.type == 'text') {
            strFocus = 'txt_' + objclient[nID][1];
        } else if (focusElm.type == 'button') {
            strFocus = 'btn_' + objclient[nID][1];
        }
    } else if (focusElm.tagName == 'TEXTAREA') {
        strFocus = 'txt_' + objclient[nID][1];
    } else if (focusElm.tagName == 'SELECT') {
        strFocus = 'ddl_' + objclient[nID][1];
    }
    return strFocus;
}
// -------------------------------------------------------------
// Hiddenのフォーカス情報削除
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function hidstrFocusClr() {
    var hidNextFocus = document.getElementById(hidFocus);
    hidNextFocus.value = '';
    return;
}

/**************************************************************/
/*                                                            */
/* errorMesseg制御                                            */
/*                                                            */
/**************************************************************/
// -------------------------------------------------------------
// エラーメッセージの表示
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function errView() {
    var elm = document.getElementById(hiderr);
    var strErr = elm.value;
    if (strErr == '') {
        window.alert('エラーはありません');
    } else {
        strErr = strErr.replace(/___/g, '\n');
        window.alert(strErr);
    }
}
// -------------------------------------------------------------
// エラーメッセージの削除
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function errViewDell() {
    var hidelm = document.getElementById(hiderr);
    hidelm.value = '';

    var lblelm = document.getElementById(hidlblerr);
    lblelm.innerHTML = '';

    return;
}
/**************************************************************/
/*                                                            */
/* イベントハンドラ制御                                       */
/*                                                            */
/**************************************************************/
// -------------------------------------------------------------
// 【onKeyDown】
// Tab、Enterを無効扱いにして、Valicheckを行う。
// Tab、Enterなら、blurを発生させる。
// Tab、Enterなら、hiddenFocusに値をセットする。
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function PushEnter() {

    if (NowElm != null) {
        var nCode = event.keyCode;
        var bshift = event.shiftKey;
        if (bshift == false) {
            if (nCode == 9) {
                // TABキーのみの場合は無効
                event.keyCode = 0;
                nCode = 0;
                var nFocusID = getNowFocusElmID(NowElm.id);
                if (nFocusID != null) {
                    UpDownElm = "UP";
                    var nElm = getNextFocusElmID((nFocusID + 1), 'sev');
                    // HiddenのNextFocusにセット
                    hidstrFocusSet(nElm, nFocusID, '0');
                    //次エレメントを記憶
                    nextFocus = getNextFocusElm((nFocusID + 1), 'cli');
                    //このエレメントのBlur発生
                    document.getElementById(objclient[nFocusID][0]).blur();
                }
            }
        } else {
            if (nCode == 9) {
                // Shift + TABキーのみの場合は無効
                event.keyCode = 0;
                nCode = 0;
                var nFocusID = getNowFocusElmID(NowElm.id);
                if (nFocusID != null) {
                    UpDownElm = "DOWN";
                    var nElm = getNextFocusElmIDE((nFocusID - 1), 'sev');
                    // HiddenのNextFocusにセット
                    hidstrFocusSet(nElm, nFocusID, '1');
                    //次エレメントを記憶
                    nextFocus = getNextFocusElmE((nFocusID - 1), 'cli');
                    //このエレメントのBlur発生
                    document.getElementById(objclient[nFocusID][0]).blur();
                }
            }
        }
        if (nCode == 13) {
            event.keyCode = 0;
            // 特記事項のみ改行を許可する。
            var blnFlg = true;
            if (NowElm.tagName == 'TEXTAREA') {
                switch (idConv(NowElm.id)) {
                    case 'TOKKI':   // B
                    case 'KOSHO':   // D
                    case 'GENIN':   // E
                    case 'TAISHO':   // H
                        event.keyCode = 13;
                        blnFlg = false;
                        break;
                    default:
                        break;
                }
            }
            if (blnFlg) {
                // Enter の場合は次のエレメントをセットしておく
                var nFocusID = getNowFocusElmID(NowElm.id);
                if (nFocusID != null) {
                    UpDownElm = "UP";
                    var nElm = getNextFocusElmID((nFocusID + 1), 'sev');
                    // HiddenのNextFocusにセット
                    hidstrFocusSet(nElm, nFocusID, '0');
                    //次エレメントを記憶
                    nextFocus = getNextFocusElm((nFocusID + 1), 'cli');
                    if (NowElm.type == 'button') {
                        //このエレメントのClick発生
                        document.getElementById(objclient[nFocusID][0]).click();
                    } else {
                        //このエレメントのBlur発生
                        document.getElementById(objclient[nFocusID][0]).blur();
                    }
                }
            }
        }
    }
    return;
}
// -------------------------------------------------------------
// 【onFocus】
// -------------------------------------------------------------
// arg[0] : elm : this element
// arg[1] : str : スラッシュ、カンマ削除
// return :     : 
// -------------------------------------------------------------
function getFocus(e, strDell) {
    //PostBackt中は何もしない。フォーカスアウトする
    if (OnCheckPostBack() == true) {
        e.blur();
        return;
    }

    UpDownElm = "";
    // 今のエレメントを退避
    NowElm = e;
    // フォーカス時の値記憶
    oldText = e.value;
    // Enter以外のフォーカスアウトでのValiNG時
    // マウスで抜けた場合は、もとに戻さない
    if ((NGFocus == true) && (nextFocus != null)) {
        if (meFocus != null) {
            if (meFocus == e.id) {
                NGFocus = false;
                meFocus = null;
            } else {
                var elm = checkElmID(meFocus);
                if (elm != null) {
                    elm.focus();
                } else {
                    elm = getNextFocusElmIDE(0, 'cli');
                    if (elm != null) {
                        elm.focus();
                    }
                }
                return;
            }
        }
    } else {
        NGFocus = false;
    }

    // hiddenのフォーカス情報削除
    hidstrFocusClr();
    // 現在のフォーカス位置を記憶
    meFocus = e.id;
    nextFocus = null;

    // elmの背景色セット
    if (e.style.backgroundColor != objelmdata.Color('ValiNGOFF')) {  // 背景が赤でない場合
        e.style.backgroundColor = objelmdata.Color('FocusON');   // 背景を薄黄色
    } else {
        e.style.backgroundColor = objelmdata.Color('ValiNGON');      // 背景を赤色
    }

    // カンマ、スラッシュ削除
    if (strDell != 0) {
        if (strDell == 1) {
            e.value = e.value.replace(/[,]/g, '');
        } else {
            e.value = e.value.replace(/[\/]/g, '');
        }
    }

    // テキストボックスの値を選択する
    if (e.type == 'text') {
        e.select();
    }
    return;
}

// -------------------------------------------------------------
// 【onFocus】ボタン用
// -------------------------------------------------------------
// arg[0] : elm : this element
// return :     : 
// -------------------------------------------------------------
function getBtnFocus(e) {
    // 今のエレメントを退避
    NowElm = e;
    UpDownElm = "";
    if (mainBtn == e.id) {
        meFocus = e.id;
        mainBtn = null;
        e.click();
        return;
    }

    // Enter以外のフォーカスアウトでのValiNG時    
    if (NGFocus == true) {
        if (meFocus != null) {
            if (meFocus == e.id) {
                NGFocus = false;
                meFocus = null;
            } else {
                var elm = checkElmID(meFocus);
                if (elm != null) {
                    elm.focus();
                } else {
                    elm = getNextFocusElmIDE(0, 'cli');
                    if (elm != null) {
                        elm.focus();
                    }
                }
                return;
            }
        }
    }

    // 現在のフォーカス位置を記憶
    meFocus = e.id;
    return;
}
// -------------------------------------------------------------
// 【onBlur】
// -------------------------------------------------------------
// arg[0] : elm : this element
// return :     : 
// -------------------------------------------------------------
function relFocus(e, mode, aj) {
    NowElm = null;
    var AJstatus = false;
    // 各項目の変更が可能の場合のみ、処理を行う。
//    if ((aspxMode == 'Meisai') || (aspxMode == 'Master')) {
//        var hidelm = document.getElementById(hidMode);
//        var nMode = parseInt(hidelm.value);
//        if (nMode == 0) return;
//        if ((nMode >= 1) || (nMode <= 3)) {
//            if (strChangOK == false) {
//                var bFlug = chkKeyElm(e);
//                if (bFlug == false) {
//                    return;
//                }
//            }
//        }
//    }

    // Enter以外のフォーカスアウト時の処理
    if (NGFocus == false) {
        // postback発生中なら、何もしない。
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm.get_isInAsyncPostBack() == false) {
            var Status = new Array(true, '');
            for (i = 0; i < objclient.length; i++) {
                if (objclient[i][0] == e.id) {
                    var nNowID = i;
                    break;
                }
            }
            if (nNowID == undefined) {
                return;
            }
            
            var elm = document.getElementById(objclient[nNowID][0]);
            var Status = Validator.check(e, objclient[nNowID][3]);

            if (Status[0] == true) {
                e.value = Status[1];
                e.style.backgroundColor = objelmdata.Color('FocusOFF');
                // 検索ボタンをすべて有効にします。
                SeachBtnChg();
                // focusON時と値が変わった場合
                if (oldText != e.value) {
                    AJstatus = AJCon(e.id, nNowID);
                }
                if ((e.value != "") && (oldText == e.value) && (objclient[nNowID][4] == "1")) {
                    // focusON時と値が同値で、ValiNGの場合(サーバーNGの場合）
                    if (objclient[nNowID][7] == "") {
                        //サーバー処理がない場合、フラグ解除
                        objclient[nNowID][4] = "0";
                    } else if (objclient[nNowID][7] != "" && (objclient[nNowID][1].match(/(FROM\d)$/) || objclient[nNowID][1].match(/(TO\d)$/))) {
                        //サーバー処理があってもFROM-TOの場合、フラグ解除
                        objclient[nNowID][4] = "0";
                    } else {
                        meFocus = elm.id;
                        e.style.backgroundColor = objelmdata.Color('ValiNGOFF');
                        NGFocus = true;
                    }
                    
                } else {
                    if (AJstatus == false) {
                        //ポストバック発生していない場合、NGフラグ解除
                        objclient[nNowID][4] = "0";
                    }
                }
            } else {
                if (Status[1] == '') {
                    // textboxが空の場合、色のみ変更して、次のフォーカス
                    e.style.backgroundColor = objelmdata.Color('ValiNGOFF');
                    // focusON時と値が変わった場合
                    if (oldText != e.value) {
                        AJstatus = AJCon(e.id, nNowID);
                    }
                } else {
                    // textboxが空でない場合は、フォーカスを元に戻す処理
                    meFocus = elm.id;
                    e.style.backgroundColor = objelmdata.Color('ValiNGOFF');
                    NGFocus = true;
                    // 検索ボタンを無効にする。
                    //SeachBtnMeChg(e.id);
                }

            }

            if (AJstatus == false) {
                if (nextFocus != null) {
                    //フォーカスが当てれる場合にそのままセット。できなければ、次へ
                    var nFocusID = getNowFocusElmID(e.id);
                    if (UpDownElm == "UP") {
                        var elm = getNextFocusElm((nFocusID + 1), 'cli');
                    } else {
                        var elm = getNextFocusElmE((nFocusID - 1), 'cli');
                    }
                    elm.focus();
                }
            }
        }

    }
}
// -------------------------------------------------------------
// 【onBlur】ボタン用
// -------------------------------------------------------------
// arg[0] : elm : this element
// return :     : 
// -------------------------------------------------------------
function relBtnFocus(e) {
    // postback発生中なら、何もしない。
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm.get_isInAsyncPostBack() == false) {
        if (nextFocus != null) { document.getElementById(nextFocus.id).focus(); }
    }

}
/**************************************************************/
/*                                                            */
/* 非同期通信制御                                              */
/*                                                            */
/**************************************************************/
// AJAX Control
var _backgroundElement = document.createElement('div');
// -------------------------------------------------------------
// ページロード
// -------------------------------------------------------------
// arg[0] : elm : 
// return :     : 
// -------------------------------------------------------------
function pageLoad(e) {
    var manager = Sys.WebForms.PageRequestManager.getInstance();
    manager.add_initializeRequest(OnCheckStatus);
    manager.add_beginRequest(OnBeginRequest);
    manager.add_endRequest(OnEndRequest);
    //manager.add_pageLoaded(pageLoaded);

    var status = manager.get_isInAsyncPostBack();

    //$get('pageContent').appendChild(_backgroundElement);
    //GetTabIndex();
    if (status == false) {
        if (aspxMode != 'Search') {
            var nWidth = document.documentElement.clientWidth;
            var nHeight = document.documentElement.clientHeight;
            nWidth = 1000 - nWidth;
            nHeight = 655 - nHeight;
        } else {
            //var nWidth = document.documentElement.clientWidth;
           // var nHeight = document.documentElement.clientHeight + 10;
            var nWidth = 0;
            var nHeight = 0;
        }
        try {
            window.resizeBy(nWidth, nHeight);
        } catch (ex) {
        alert('リサイズに失敗しました\n画面表示が終わるまで、しばらくお待ち下さい');
            //(HIS-127)
            //window.resizeBy(nWidth, nHeight);
        }

　      setControl();
        setBtnControl();
        elmControl(objclient);
        var elm = document.getElementById(hiderr);
        var strErr = elm.value;
        if (strErr.match(/^(result=1__)/i)) {
            errCode = '0';
            var msg = strErr.replace(/^(result=1__)/, '');
            msg = msg.replace(/(___)/g, '\n');
            alert(msg);
            elm.value = '';
        } else {
            elm.value = '';
        }
    }
    FirstsetMode();

}
// -------------------------------------------------------------
// 2重PostBack抑止(後発抑止。クリアボタンのみ有効）
// -------------------------------------------------------------
// arg[0] : obj : sender
// arg[1] : obj : arg
// return :     : 
// -------------------------------------------------------------
function OnCheckStatus(sender, arg) {
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    var i;
    var strID = '';
    for (i = 0; i < AJBtn.length; i++) {
        if (AJBtn[i][1] == 'btnAJclear') {
            strID = AJBtn[i][0];
            break;
        }
    }
    if (prm.get_isInAsyncPostBack() && arg.get_postBackElement().id != strID) {
        arg.set_cancel(true);
    } else if (prm.get_isInAsyncPostBack() && lastPostBackElement == strID) {
        arg.set_cancel(true);
    }
}
// -------------------------------------------------------------
// PostBack中か確認する
// -------------------------------------------------------------
// arg    :     :
// return : bool:true:PostBack中 false:PostBack中でない
// -------------------------------------------------------------
function OnCheckPostBack() {
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    return prm.get_isInAsyncPostBack();
}
// -------------------------------------------------------------
// PostBack開始処理
// -------------------------------------------------------------
// arg[0] : obj : sender
// arg[1] : obj : arg
// return :     : 
// -------------------------------------------------------------
function OnBeginRequest(sender, arg) {

    // hiddeElementに値をセット。
    ClientControl();
    // 最後のPostback要求を記憶
    lastPostBackElement = arg.get_postBackElement().id;
    NGFocus = false;
    meFocus = null;
    if ("ScrollSet" in window) {
        // scroll制御
        scrollCHG = true;
    }
    // div pageContent のパラメータ変更
    document.body.style.cursor = "wait";
    _backgroundElement.style.display = '';
    _backgroundElement.style.position = 'fixed';
    _backgroundElement.style.left = '0px';
    _backgroundElement.style.top = '0px';

    //var clientBounds = Sys.UI.Bounds(0, 0, document.documentElement.clientWidth, document.documentElement.clientHeight);
    var clientWidth = document.documentElement.clientWidth;
    var clientHeight = document.documentElement.clientHeight;
    _backgroundElement.style.width = Math.max(Math.max(document.documentElement.scrollWidth, document.body.scrollWidth), clientWidth) + 'px';
    _backgroundElement.style.height = Math.max(Math.max(document.documentElement.scrollHeight, document.body.scrollHeight), clientHeight) + 'px';
    _backgroundElement.style.zIndex = 10000;
    //_backgroundElement.className = 'modalBackground';

}
// -------------------------------------------------------------
// PostBack終了処理
// -------------------------------------------------------------
// arg[0] : obj : sender
// arg[1] : obj : arg
// return :     : 
// -------------------------------------------------------------
function OnEndRequest(sender, arg) {
    //var http = new ActiveXObject("Microsoft.XMLHTTP");
    //arg.__response._xmlHttpRe
    // div pageContent を隠す
    _backgroundElement.style.display = 'none';
    document.body.style.cursor = "auto";
    var elm = document.getElementById(hiderr);
    var strErr = elm.value;
    if (strErr.match(/^(result=100_)/i)) {
        var strURL = strErr.replace(/^(result=100_)/, '');
        errCode = '0';
        WindowNewOpen(strURL, "PDF", "");
        elm.value = '';
    } else if (strErr.match(/^(result=1__)/i)) {
        errCode = '0';
        var msg = strErr.replace(/^(result=1__)/, '');
        msg = msg.replace(/(___)/g, '\n');
        alert(msg);
        elm.value = '';
    } else if (strErr.match(/^(result=2_)/i)) {
        errCode = strErr.replace(/^(result=2_)/, '');
        if ("ScrollSet" in window) {
            // scroll制御
            setScroll(errCode);
        }
        elm.value = '';
    } else if (strErr.match(/^(result=5_)/i)) {
        errCode = '0'
        elm.value = '';
        if (typeof AUTOSEARCH == "function") {
            AUTOSEARCH();
        }
    } else if (strErr.match(/(result=0)/)) {
        errCode = '0';
        elm.value = '';
    }


    if (errCode != '0') {
        var i;
        for (i = 0; i < errMsg.length; i++) {
            if (errMsg[i][0] == errCode) {
                if (errMsg[i][1] != '') {
                    alert(errMsg[i][1]);
                    break;
                }
            }
        }
        errCode = '0';
    }
    strChangOK = true;
    // hiddenFocusの値をクリア
    hidstrFocusClr();
    nextFocus = null;
    // 一旦、ボタン部をすべて活性化
    //ComBtmVeiw(true);
    getControl();
    //elmControl();
    setBtnControl();
    //setCHKbox();
}

function setCHKbox() {
    var i = 0;
    var elm;
    for (i = 0; i < objclient.length; i++) {
        if (objclient[i][0] != "") {
            elm = document.getElementById(objclient[i][0]);
            if (elm.type == 'checkbox') {
                elm.attachEvent('onfocus', "getFocus(this, 0)");
                elm.attachEvent('onkeydown', "PushEnter(this)");
                elm.attachEvent('onblur', "relFocus(this)");
            }
        }
    }
}
/**************************************************************/
/*                                                            */
/* 画面モード制御                                              */
/*                                                            */
/**************************************************************/
// -------------------------------------------------------------
// すべての検索ボタンを変更します。
// -------------------------------------------------------------
// arg[0] : bool: ture = 無効
// return :     : 
// -------------------------------------------------------------
function SeachBtnChg() {
    var i;
    var elm;
    var acFocus = null;
    var focuselm = null;
    for (i = 0; i < objclient.length; i++) {
        elm = checkElmID(objclient[i][0])
        if ((elm) && ((elm.type == 'button') || (elm.type == 'image'))) {
            // フォーカス設定ON可否設定
            if ((objclient[i][9] == undefined) || (objclient[i][9] == '0')) {
                elm.disabled = true;
            } else {
                elm.disabled = false;
            }
        }
    }

}
// -------------------------------------------------------------
// VariNG時の検索ボタンをひとつのみ有効にして、他を無効にします。
// -------------------------------------------------------------
// arg[0] : str : プレフィクス無しelementID
// return :     : 
// -------------------------------------------------------------
function SeachBtnMeChg(strElmID) {
    var i, J;
    var nCng = 0;
    if (objbtn.length > 0) {
        for (i = 0; i < objbtn.length; i++) {
            nCng = 1;
            for (j = 2; j < objbtn[i].length; j++) {
                if (objbtn[i][j] == strElmID) {
                    document.getElementById(objbtn[i][0]).disabled = false;
                    nCng = 0;
                    break;
                }
            }
            if (nCng != 0) {
                if (objbtn[i][1] != 'btnSearch') {
                    document.getElementById(objbtn[i][0]).disabled = true;
                }
            }
        }
    }
}

// -------------------------------------------------------------
// 画面全体をクリアします。
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function ClearChk() {
    var bRet = true;
    var i;
    if (bRet == true) {
        // 暫定対応
        if (getkeyElmNull() == true) {
            for (i = 0; i < (btnMode.length - 1); i++) {
                var elm = document.getElementById(btnMode[i]);
                elm.style.backgroundColor = '';
                elm.style.color = '';
            }
            // 初期モードは未設定
            var hidelm = document.getElementById(hidMode);
            hidelm.value = '0';
            errCode = '0';
            strChangOK = false;
            setBtnMode('clear');
        }
        // focus位置の設定
        var elmNo = getNextFocusElmID(0, 'sev');
        hidstrFocusSet(elmNo, elmNo, '0');
        var strClickID = 'btnAJclear';
        for (i = 0; i < AJBtn.length; i++) {
            if (strClickID == AJBtn[i][1]) {
                document.getElementById(AJBtn[i][0]).click();
                break;
            }
        }
    }
    return false;
}
// -------------------------------------------------------------
// キー部のエレメントがデフォルト値か確認する。
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function getkeyElmNull() {
    var bRet = true;
    var elm;
    var strSelectIndex;
    var i;
    for (i = 0; i < objclient.length; i++) {
        if (objclient[i][8] == 'keyElm') {
            elm = document.getElementById(objclient[i][0]);
            if (elm.tagName == "INPUT") {
                if (elm.type == "text") {
                    if (elm.value != objclient[i][6]) {
                        bRet = false;
                        break;
                    }
                }
            } else if (elm.tagName == "SELECT") {
                var index = elm.selectedIndex;
                var val = elm.options[index].value;
                if (val != objclient[i][6]) {
                    bRet = false;
                    break;
                }
            }
        }
    }
    return bRet;
}


