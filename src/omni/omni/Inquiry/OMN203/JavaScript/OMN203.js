var strChangOK = false;     // メイン部変更可否(true= 変更可能)
var aspxMode = 'Inquiry OMN203';

// -------------------------------------------------------------
// ページロードのたびにcallされます
// キー部、ボタン部のスタイルを変更します。
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function FirstsetMode() {
	var hidelm = document.getElementById(hidMode);
	// hidden属性のセット
	if (hidelm.value == "") {
		hidelm.value = "0";
	}

	// 変更フラグ解除
	textChg = false;
	return false;
}

// -------------------------------------------------------------
// 初期ページロード時のFocusElmを返します
// -------------------------------------------------------------
// arg[0] :     :
// return : elm : element
// -------------------------------------------------------------
function getFirstFocus() {
	return document.getElementById(btnMode[0]);
}

// -------------------------------------------------------------
// モードボタン部のフォーカス制御
// -------------------------------------------------------------
// arg[0] : elm : this element
// return : bool: true/false
// -------------------------------------------------------------
function btnModeTab(e) {
	var nCode = event.keyCode;
	var bshift = event.shiftKey;
	var elm = null;
	if (bshift == false) {
		if (nCode == 9) {
			event.keyCode = 0;
			// TABキーのみの場合
				switch (e.id) {
				case btnMode[0]: elm = document.getElementById(btnMode[1]); break;
				case btnMode[1]: elm = document.getElementById(btnMode[2]); break;
				case btnMode[2]: elm = document.getElementById(btnMode[0]); break;
				default: break;
			}
		}
	} else {
		if (nCode == 9) {
			event.keyCode = 0;
			// Shift + TABキーのみの場合
				switch (e.id) {
				case btnMode[0]: elm = document.getElementById(btnMode[2]); break;
				case btnMode[1]: elm = document.getElementById(btnMode[0]); break;
				case btnMode[2]: elm = document.getElementById(btnMode[1]); break;
				default: break;
			}
		}
	}
	if (nCode == 9) {
		if (elm.disabled == false) { elm.focus(); return false; }
	}
	if (nCode == 13) { event.keyCode = 0; e.click(); }
	return false;
}

// -------------------------------------------------------------
// キー部、ボタン部のスタイルを変更します。
// -------------------------------------------------------------
// arg[0] : elm : this element
// arg[1] : num : モード変更される番号
// return :     : 
// -------------------------------------------------------------

function setMode(e, nMode) {
	var bRet = true;
	var hidelm = document.getElementById(hidMode);
	if (nMode == hidelm.value) {
		bRet = false;
	} else {
		if (OnCheckPostBack() == true) {
			bRet = false;
			alert('サーバーとの通信中です。\nしばらくたってから押し直してください');
		} else {
			if (textChg == true) {
				bRet = window.confirm("編集されています。切り替えてよろしいでしょうか？");
			}
		}
	}
	if (bRet == true) {
		var hidelm = document.getElementById(hidMode);
		var elm;
		var i;

		hidelm.value = nMode;
		//AllClear();
		setBtnMode("non");
		textChg = false;
		changeMode = true;
		NGFocus = false;
		
		// モード変更時の通信
		getModeChgAJ();

	}
	return false;
}
// -------------------------------------------------------------
// モードが変更された場合に、ボタンの有効／無効を設定します。
// -------------------------------------------------------------
// arg[0] : str : "clear"=モード解除
// return :     : 
// -------------------------------------------------------------
function setBtnMode(clr) {
	return;
}



// -------------------------------------------------------------
// モード変更時に通信を行います
// -------------------------------------------------------------
// arg[0] :     :
// return :     :
// -------------------------------------------------------------
function getModeChgAJ() {
	var elmNo = getNextFocusElmID(0,'sev');
	var elmFocus = getNextFocusElm(elmNo,'sev');
	for (i = 0; i < AJBtn.length; i++) {
		if (AJBtn[i][1] == 'btnAJModeCng') {
			hidstrFocusSet(elmNo, elmNo, '0');
			document.getElementById(AJBtn[i][0]).click();
			break;
		}
	}
}

// -------------------------------------------------------------
// メインボタン部をすべて活性／非活性にします
// -------------------------------------------------------------
// arg[0] : elm : this element
// return : bool: false
// -------------------------------------------------------------
function ComBtmVeiw(bMode) {
	var i;
	for(i=0;i<btnCom.length;i++){
		document.getElementById(btnCom[i][0]).disabled = bMode;
	}
}

// -------------------------------------------------------------
// メインボタン部のフォーカス制御
// -------------------------------------------------------------
// arg[0] : elm : this element
// return : bool: false
// -------------------------------------------------------------
function btnMainTab(e) {
	var nCode = event.keyCode;
	var bshift = event.shiftKey;
	if (bshift == false) {
		if (nCode == 9) {
			// TABキーのみの場合は無効
			event.keyCode = 0;
			nCode = 0;
			var nFocusID = getNowFocusElmID(e.id);
			if (nFocusID != null) {
				var nElm = getNextFocusElmID((nFocusID + 1), 'cli');
				//エレメントにフォーカス
				document.getElementById(objclient[nElm][0]).focus();
			}
		}
	} else {
		if (nCode == 9) {
			// Shift + TABキーのみの場合は無効
			event.keyCode = 0;
			nCode = 0;
			var nFocusID = getNowFocusElmID(e.id);
			if (nFocusID != null) {
				var nElm = getNextFocusElmIDE((nFocusID - 1), 'cli');
				//エレメントにフォーカス
				document.getElementById(objclient[nElm][0]).focus();
			}
		}
	}
	if (nCode == 13){
		event.keyCode = 0;
		e.click();
	}
	return false;
}
// -------------------------------------------------------------
// Searchボタンを押されたときの処理
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function KeyElmChk(e) {

    var elm;
    var Status;
    var ValiStatus = true;
    var i;
    var focusElm = null;
    var bRet = false;
    for (i = 0; i < objclient.length; i++) {
        if (objclient[i][8] == "keyElm") {
            elm = document.getElementById(objclient[i][0]);
            if (elm.type == 'text') {
                Status = Validator.check(elm, objclient[i][3]);
                if (Status[0] == false) {
                    ValiStatus = false;
                    if (focusElm == null) {
                        focusElm = elm;
                    }
                    elm.style.backgroundColor = objelmdata.Color('ValiNGOFF');
                }
            }
        }
    }

    if (ValiStatus == true) {
        var nFocusID = getNowFocusElmID(e.id);
        var nElm = getNextFocusElmID((nFocusID + 1), 'sev');
        hidstrFocusSet(nElm, nFocusID, '0');

        for (i = 0; i < AJBtn.length; i++) {
            if (AJBtn[i][1] == "btnAJSearch") {
                bRet = true;
                document.getElementById(AJBtn[i][0]).click();
                break;
            }
        }
    } else {
        focusElm.focus();
    }
    return false;
}
// -------------------------------------------------------------
// 次画面ボタンを押されたときの処理
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function nextChk(obj, shift, ctrl, alt) {
	// 自分自身にフォーカスをセットする
    var elmNo = getElmNo('btnNext');
    // ボタンが有効か、確認する
    var elm = checkElm(elmNo, "cli")
    if ((elm != null) && (elm.disabled == false)) {
        // 自分自身にフォーカスをセットする
        hidstrFocusSet(elmNo, elmNo, '0');
        var strClickID = "btnAJNext";
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
// 登録ボタンを押されたときの処理
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function submitChk() {
    var elmMode = document.getElementById(hidMode)
    var bRet = true;
    if (elmMode.value == "2") {
        bRet = confirm("削除してよろしいですか？");
    }
    if (bRet) {
        // 自分自身にフォーカスをセットする
        var elmNo = getElmNo('btnSubmit');
        // ボタンが有効か、確認する
        var elm = checkElm(elmNo, "cli")
        if ((elm != null) && (elm.disabled == false)) {
            // 自分自身にフォーカスをセットする
            var elmNo = getNextFocusElmID(0, 'sev');
            var elmNo2 = getNextFocusElmID((elmNo + 1), 'sev');
            hidstrFocusSet(elmNo, elmNo2, '0');
            var strClickID = "btnAJSubmit";
            for (i = 0; i < AJBtn.length; i++) {
                if (strClickID == AJBtn[i][1]) {
                    document.getElementById(AJBtn[i][0]).click();
                    break;
                }
            }
        }
    }
  return false;
}

// -------------------------------------------------------------
// プレビューボタンを押されたときの処理
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function submitPre() {
	// 自分自身にフォーカスをセットする
    var elmNo = getElmNo('btnPre');
    // ボタンが有効か、確認する
    var elm = checkElm(elmNo, "cli")
    if ((elm != null) && (elm.disabled == false)) {
        // 自分自身にフォーカスをセットする
        hidstrFocusSet(elmNo, elmNo, '0');
        var strClickID = "btnAJPre";
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
// Excelボタンを押されたときの処理
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function submitExcel() {
	// 自分自身にフォーカスをセットする
    var elmNo = getElmNo('btnExcel');
    // ボタンが有効か、確認する
    var elm = checkElm(elmNo, "cli")
    if ((elm != null) && (elm.disabled == false)) {
        // 自分自身にフォーカスをセットする
        hidstrFocusSet(elmNo, elmNo, '0');
        var strClickID = "btnAJExcel";
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
// 終了ボタンを押されたときの処理
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function submitBefor() {
    var bln = confirm('終了しますか？')
    if (bln) {
        // 自分自身にフォーカスをセットする
        var elmNo = getElmNo('btnBefor');
        // ボタンが有効か、確認する
        var elm = checkElm(elmNo, "cli")
        if ((elm != null) && (elm.disabled == false)) {
            // 自分自身にフォーカスをセットする
            var elmNo = getNextFocusElmID(0, 'sev');
            var elmNo2 = getNextFocusElmID((elmNo + 1), 'sev');
            hidstrFocusSet(elmNo, elmNo2, '0');
            var strClickID = "btnAJBefor";
            for (i = 0; i < AJBtn.length; i++) {
                if (strClickID == AJBtn[i][1]) {
                    document.getElementById(AJBtn[i][0]).click();
                    break;
                }
            }
        }
    }
    return false;
}

// -------------------------------------------------------------
// 全選択ボタンを押されたときの処理
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function submitF2() {
    // 自分自身にフォーカスをセットする
    var elmNo = getElmNo('btnF2');
    // ボタンが有効か、確認する
    var elm = checkElm(elmNo, "cli")
    if ((elm != null) && (elm.disabled == false)) {
        // 自分自身にフォーカスをセットする
        hidstrFocusSet(elmNo, elmNo, '0');
        var strClickID = "btnAJF2";
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
// 全解除ボタンを押されたときの処理
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function submitF4() {
    // 自分自身にフォーカスをセットする
    var elmNo = getElmNo('btnF4');
    // ボタンが有効か、確認する
    var elm = checkElm(elmNo, "cli")
    if ((elm != null) && (elm.disabled == false)) {
        // 自分自身にフォーカスをセットする
        hidstrFocusSet(elmNo, elmNo, '0');
        var strClickID = "btnAJF4";
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
// ダウンロードボタンを押されたときの処理
// -------------------------------------------------------------
// return :     : 
// -------------------------------------------------------------
function submitF7() {
    // 自分自身にフォーカスをセットする
    var elmNo = getElmNo('btnF7');
    // ボタンが有効か、確認する
    var elm = checkElm(elmNo, "cli")
    if ((elm != null) && (elm.disabled == false)) {
        // 自分自身にフォーカスをセットする
        hidstrFocusSet(elmNo, elmNo, '0');
        var strClickID = "btnAJF7";
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
// 【MouseOver】
// -------------------------------------------------------------
// arg[0] : elm : this element
// return :     : 
// -------------------------------------------------------------
function mouseON(e) {
	e.style.backgroundColor = objelmdata.Color('MouseON');
}

// -------------------------------------------------------------
// 【Mouseout】
// -------------------------------------------------------------
// arg[0] : elm : this element
// return :     : 
// -------------------------------------------------------------
function mouseOUT(e) {
	e.style.backgroundColor = objelmdata.Color('MouseOFF');
}

function AJCon(strID, nNowID) {
    if (nNowID == -1) {
        return;
    }

    var strAJ = "";
    strAJ = objclient[nNowID][7];

    var bRet = false;
    switch (strAJ) {
        case "btnAJSAGYOTANT":
        case "btnAJSAGYOTANTNMFROM12":
        case "btnAJSAGYOTANTNMTO12":
        case "btnAJNONYUNMRFROM12":
        case "btnAJNONYUNMRTO12":
            bRet = btnAJOne(strAJ);
            break;

        default:
            //処 理 ;
            break;
    }
    return bRet;
}

function btnAJOne(strElmId) {
    var i;
    var bRet = false;
    for (i = 0; i < AJBtn.length; i++) {
        if (AJBtn[i][1] == strElmId) {
            bRet = true;
            document.getElementById(AJBtn[i][0]).click();
            break;
        }
    }
    return bRet;
}



// -------------------------------------------------------------
// 【Mouseout】
// -------------------------------------------------------------
// arg[0] : elm : this element
// return :     : 
// -------------------------------------------------------------
function setBg(e, color) {
	e.style.backgroundColor = color;
}

function SAGYOUTANTCD_Search(e, str, strmode) {

    var url = "../../../Search/OMN806/Contents/OMN806.aspx";
    var mode = "Search";
    var nWidth = "464px";
    var nHeight = "588px";
    var option = "dialogWidth=" + nWidth + ";dialogHeight=" + nHeight + ";center:1;status:no;scroll:no;resizable:no;";


    e.disabled = true;
    var elm;
    var retval = new Array;
    retval = WindowOpen(url, mode, option);

    var FIELD = "";
    if (strmode == 'FROM') {
        FIELD = 'SAGYOTANTCDFROM12';
    } else {
        FIELD = 'SAGYOTANTCDTO12';
    }

    if (retval != null) {
        for (i = 0; i < objclient.length; i++) {
            if (objclient[i][1] == FIELD) {
                elm = document.getElementById(objclient[i][0]);
                elm.value = retval[0].rtrim();
                elm.style.backgroundColor = objelmdata.Color('FocusOFF');
            }
        }
    }

    // 規定値のフォーカスセット
    var elmNo;
    var elmNo2;
    if (strmode == 'FROM') {
        var elmNo = getElmNo('btnSAGYOUTANTCDFROM1' + str);
        var elmNo2 = getElmNo('btnSAGYOTANTCDFROM12' + str);
    } else {
        var elmNo = getElmNo('btnSAGYOTANTCDTO12' + str);
        var elmNo2 = getElmNo('SAGYOTANTCDTO12' + str);
    }
    hidstrFocusSet(elmNo, elmNo2, '0');
    var elm = getNextFocusElm(elmNo, 'cli');
    var elm2 = getNextFocusElm(elmNo2, 'cli');
    //modori値の処理
    e.disabled = false;
    if (retval != null) {
        if (strmode == "FROM") {
            if (btnAJOne('btnAJSAGYOTANTNMFROM12') == false) {
                elm.focus();
            }
        } else {
            if (btnAJOne('btnAJSAGYOTANTNMTO12') == false) {
                elm.focus();
            }
        }
    } else {
        elm2.focus();
    }
    return false;
}

function NONYUCD_Search(e, str, strmode) {

    var url = "../../../Search/OMN800/Contents/OMN800.aspx";
    var mode = "Search";
    var nWidth = "581px";
    var nHeight = "672px";
    var option = "dialogWidth=" + nWidth + ";dialogHeight=" + nHeight + ";center:1;status:no;scroll:no;resizable:no;";

    e.disabled = true;
    var retval = new Array;
    retval = WindowOpen(url, mode, option);
    var FIELD = "";
    if (strmode == 'FROM') {
        FIELD = 'NONYUCDFROM12';
    } else {
        FIELD = 'NONYUCDTO12';
    }
    if (retval != null) {
        for (i = 0; i < objclient.length; i++) {
            if (objclient[i][1] == FIELD) {
                elm = document.getElementById(objclient[i][0]);
                elm.value = retval[0].rtrim();
                elm.style.backgroundColor = objelmdata.Color('FocusOFF');
            }
        }
    }

    // 規定値のフォーカスセット
    var elmNo;
    var elmNo2;
    if (strmode == 'FROM') {
        var elmNo = getElmNo('btnNONYUCDFROM1' + str);
        var elmNo2 = getElmNo('NONYUCDFROM12' + str);
    } else {
        var elmNo = getElmNo('btnNONYUCDTO1' + str);
        var elmNo2 = getElmNo('NONYUCDTO12' + str);
    }
    hidstrFocusSet(elmNo, elmNo2, '0');
    var elm = getNextFocusElm(elmNo, 'cli');
    var elm2 = getNextFocusElm(elmNo2, 'cli');
    //modori値の処理
    e.disabled = false;
    if (retval != null) {
        if (strmode == "FROM") {
            if (btnAJOne('btnAJNONYUNMRFROM12') == false) {
                elm.focus();
            }
        } else {
            if (btnAJOne('btnAJNONYUNMRTO12') == false) {
                elm.focus();
            }
        }
    } else {
        elm2.focus();
    }

    return false;
}