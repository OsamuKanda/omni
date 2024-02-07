var strChangOK = false;     // メイン部変更可否(true= 変更可能)
var aspxMode = 'Search';

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

    var bRet = btnAJOne(strAJ);
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

function ret(e, strNum) {
	if (!opener) {
		opener = dialogArguments;
	}

	returnValue = strNum;
	close();
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

function SIRCD_Search(e, str, elmmode) {

    var url = "../../../Search/OMN809/Contents/OMN809.aspx";
    var mode = "Search";
    var nWidth = "511px";
    var nHeight = "609px";
    var option = "dialogWidth=" + nWidth + ";dialogHeight=" + nHeight + ";center:1;status:no;scroll:no;resizable:no;";

    e.disabled = true;
    var retval = new Array;
    retval = WindowOpen(url, mode, option);

    var elmVal;
    if (elmmode == "FROM") {
        elmVal = "SIRCDFROM2";
    } else {
        elmVal = "SIRCDTO2";
    }
    
    if (retval != null) {
        for (i = 0; i < objclient.length; i++) {
            if (objclient[i][1] == elmVal) {
                elm = document.getElementById(objclient[i][0]);
                elm.value = retval[0].rtrim();
                elm.style.backgroundColor = objelmdata.Color('FocusOFF');
            }
        }
    }

    // 規定値のフォーカスセット
    var elmNo;
    var elmNo2;
    if (elmmode == "FROM") {
        elmNo = getElmNo('btnSIRCDFROM2' + str);
        elmNo2 = getElmNo('SIRCDFROM2' + str);
    } else {
        elmNo = getElmNo('btnSIRCDTO2' + str);
        elmNo2 = getElmNo('SIRCDTO2' + str);
    }
    hidstrFocusSet(elmNo, elmNo2, '0');
    var elm = getNextFocusElm(elmNo, 'cli');
    var elm2 = getNextFocusElm(elmNo2, 'cli');
    //modori値の処理
    e.disabled = false;
    if (retval != null) {
        if (str == "FROM2") {
            if (btnAJOne('btnAJSIRNMR1') == false) {
                elm.focus();
            }
        } else {
        if (btnAJOne('btnAJSIRNMR2') == false) {
                elm.focus();
            }
        }
    } else {
        elm2.focus();
    }
    return false;
}