var strChangOK = false;     // メイン部変更可否(true= 変更可能)
var aspxMode = 'Input';

var scrollCHG = false;
function Scroll_Y(Panel) {
    document.getElementById(ScrollSet).value = Panel.scrollTop;
}
function setScroll(nNum) {
    if (scrollCHG) {
        scrollCHG = false;
        var scrollElm = document.getElementById('scroll');
        var scrollVal = document.getElementById(ScrollSet).value;
        if (nNum == 2) {
            if (scrollElm.scrollHeight > 161) {
                scrollElm.scrollTop = scrollElm.scrollHeight - 159;
            } else {
                scrollElm.scrollTop = scrollVal;
            }
        } else {
            if (scrollElm.scrollHeight > 161) {
                if ((scrollElm.scrollHeight - 160) < scrollVal) {
                    scrollElm.scrollTop = scrollElm.scrollHeight - 159;
                } else {
                    scrollElm.scrollTop = scrollVal;
                }
            } else {
                scrollElm.scrollTop = scrollVal;
            }
        }
    }
}
var tab = {
    init: function() {
        var tabs = this.setup.tabs;
        var menus = new Array;
        var i;
        for (i = 0; i < tabs.length; i++) {
            menus = menus.concat([document.getElementById('scroll')]);
        }

        for (i = 0; i < menus.length; i++) {
            //if (i !== 0) menus[i].style.display = 'none';
            tabs[i].onclick = function() { tab.showpage(this); return false; };
        }
    },

    showpage: function(obj) {
        var tabs = this.setup.tabs;
        var menus = new Array;
        var i, nNum;
        for (i = 0; i < tabs.length; i++) {
            menus = menus.concat([document.getElementById('scroll')]);
        }
        for (nNum = 0; nNum < tabs.length; nNum++) {
            if (tabs[nNum] === obj) break;
        }

        for (i = 0; i < menus.length; i++) {
            if (i == nNum) {
                menus[nNum].style.display = 'block';
                tabs[nNum].className = 'on';
            }
            else {
                //menus[i].style.display = 'none';
                tabs[i].className = 'off';
            }
        }
    }
}


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
	if (hidelm.value == "0") {
	    setBtnMode('clear');
	} else {
	    setBtnMode('non');
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
	var hidelm = document.getElementById(hidMode);
	var nMode = parseInt(hidelm.value);
	var newelm = document.getElementById(btnMode[0]);
	var dellelm = document.getElementById(btnMode[1]);
	var chgelm = document.getElementById(btnMode[2]);
	if (clr == "clear") {
		nMode = 0;
		hidelm.value = nMode;
		newelm.style.backgroundColor = objelmdata.Color('ModeBtnBgcOFF');
		newelm.style.color = objelmdata.Color('ModeBtnStrOFF');
		dellelm.style.backgroundColor = objelmdata.Color('ModeBtnBgcOFF');
		dellelm.style.color = objelmdata.Color('ModeBtnStrOFF');
		chgelm.style.backgroundColor = objelmdata.Color('ModeBtnBgcOFF');
		chgelm.style.color = objelmdata.Color('ModeBtnStrOFF');
	}

	switch (nMode) {
		case 1:
			newelm.disabled = false;
			newelm.style.backgroundColor = objelmdata.Color('ModeBtnBgcON');
			newelm.style.color = objelmdata.Color('ModeBtnStrON');
			dellelm.disabled = true;
			chgelm.disabled = true;
			break;
		case 2:
			newelm.disabled = true;
			dellelm.disabled = false;
			dellelm.style.backgroundColor = objelmdata.Color('ModeBtnBgcON');
			dellelm.style.color = objelmdata.Color('ModeBtnStrON');
			chgelm.disabled = true;
			break;
		case 3:
			newelm.disabled = true;
			dellelm.disabled = true;
			chgelm.disabled = false;
			chgelm.style.backgroundColor = objelmdata.Color('ModeBtnBgcON');
			chgelm.style.color = objelmdata.Color('ModeBtnStrON');
			break;
		case 0:
			newelm.disabled = false;
			dellelm.disabled = false;
			chgelm.disabled = false;
		default:
			break;
	}
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
        var elm = checkElm(elmNo, "cli");
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
    var elm = checkElm(elmNo, "cli");
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
    var elm = checkElm(elmNo, "cli");
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




function AJCon(strID, nNowID) {
	if (nNowID == -1) {
		return;
	}

	var strAJ = "";
	strAJ = objclient[nNowID][7];

	var bRet = false;
	switch (strAJ) {
	
		case "btnAJSearch":
			bRet = btnAJSearch();
			break;
  		case "btnAJRENNO":
  		case "btnAJGOUKI":
  		case "btnAJSAGYOTANTNM":
		case "btnAJModeCng":
		case "btnAJNext":
		case "btnAJF2":
		case "btnAJSubmit":
		case "btnAJF4":
		case "btnAJF5":
		case "btnAJPre":
		case "btnAJF7":
		case "btnAJExcel":
		case "btnAJBefor":
		case "btnAJclear":
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


function RENNO_Search(e, str) {
    var Qu = '?';
    var elm = document.getElementById(jigyocd);
    Qu += 'JIGYOCD=' + encodeURI(elm.value);
    Qu += '&SAGYOBKBN=2';
    Qu += '&UKETSUKEKBN=2';
    elm = document.getElementById(hidMode);
    if (elm.value == "1") {
        Qu += '&HOKOKUSHOKBN=0';
        Qu += '&SEIKYUKBN=2';
    } else {
        Qu += '&HOKOKUSHOKBN=1';
    }
    Qu += '&disable=true';
    Qu += '&Mode=search';
    Qu += '&VIEWID=OMN301';

    var url = "../../../Inquiry/OMN202/Contents/OMN202.aspx" + Qu;
    var mode = "Search";
    var nWidth = "1000px";
    var nHeight = "655px";
    var option = "dialogWidth=" + nWidth + ";dialogHeight=" + nHeight + ";center:1;status:no;scroll:no;resizable:no;";

    e.disabled = true;
    var elm;
    var retval = new Array;
    retval = WindowOpen(url, mode, option);
    if (retval != null) {
        for (i = 0; i < objclient.length; i++) {
            if (objclient[i][1] == "RENNO" + str) {
                elm = document.getElementById(objclient[i][0]);
                elm.value = retval[0].rtrim();
                elm.style.backgroundColor = objelmdata.Color('FocusOFF');
            }
        }
        elm = document.getElementById(nonyucd);
        elm.value = retval[3].rtrim();
    }

    // 規定値のフォーカスセット
    var elmNo = getElmNo('btnRENNO' + str);
    var elmNo2 = getElmNo('RENNO' + str);
    hidstrFocusSet(elmNo, elmNo2, '0');
    var elm = getNextFocusElm(elmNo, 'cli');
    var elm2 = getNextFocusElmE(elmNo2, 'cli');
    //modori値の処理
    e.disabled = false;
    if (retval != null) {
        elm.focus();
    } else {
        elm2.focus();
    }
    return false;
}

function GOUKI_Search(e, str) {
    var elm = document.getElementById(nonyucd);
    for (i = 0; i < objclient.length; i++) {
        if (objclient[i][1] == "RENNO" + str) {
            var elm2 = document.getElementById(objclient[i][0]);
            break;
        }
    }
    if ((elm.value == "") || (elm2.value == "")){
        alert("物件番号が不正です。");
        return;
    }
    var Qu = '?';
    Qu += 'NONYUCD=' + encodeURI(elm.value);
    elm = document.getElementById(jigyocd);
    Qu += '&JIGYOCD=' + encodeURI(elm.value);
    Qu += '&disable=true';
    Qu += '&Mode=search';

    var url = "../../../Search/OMN812/Contents/OMN812.aspx" + Qu;
	var mode = "Search";
	var nWidth = "902px";
	var nHeight = "629px";
	var option = "dialogWidth=" + nWidth + ";dialogHeight=" + nHeight + ";center:1;status:no;scroll:no;resizable:no;";

	e.disabled = true;
	var elm;
	var retval = new Array;
	retval = WindowOpen(url, mode, option);
	if (retval != null) {
		for (i = 0; i < objclient.length; i++) {
			if (objclient[i][1] == "GOUKI" + str) {
				elm = document.getElementById(objclient[i][0]);
				elm.value = retval[1].rtrim();
				elm.style.backgroundColor = objelmdata.Color('FocusOFF');
				break;
			}
		}
	}

	// 規定値のフォーカスセット
	var elmNo = getElmNo('btnGOUKI' + str);
	var elmNo2 = getElmNo('GOUKI' + str);
	hidstrFocusSet(elmNo, elmNo2, '0');
	var elm = getNextFocusElm(elmNo, 'cli');
	var elm2 = getNextFocusElmE(elmNo2, 'cli');
	//modori値の処理
	e.disabled = false;
	if (retval != null) {
        elm.focus();
	} else {
	    elm2.focus();
	}
	return false;
}


function SAGYOTANTCD_Search(e, str) {

    var url = "../../../Search/OMN805/Contents/OMN805.aspx";
    var mode = "Search";
    var nWidth = "317px";
    var nHeight = "610px";
    var option = "dialogWidth=" + nWidth + ";dialogHeight=" + nHeight + ";center:1;status:no;scroll:no;resizable:no;";

	e.disabled = true;
	var elm;
	var retval = new Array;
	retval = WindowOpen(url, mode, option);
	if (retval != null) {
		for (i = 0; i < objclient.length; i++) {
			if (objclient[i][1] == "SAGYOTANTCD" + str) {
				elm = document.getElementById(objclient[i][0]);
				elm.value = retval[0].rtrim();
				elm.style.backgroundColor = objelmdata.Color('FocusOFF');
			}
		}
	}

	// 規定値のフォーカスセット
	var elmNo = getElmNo('btnSAGYOTANTCD' + str);
	var elmNo2 = getElmNo('SAGYOTANTCD' + str);
	hidstrFocusSet(elmNo, elmNo2, '0');
	var elm = getNextFocusElm(elmNo, 'cli');
	var elm2 = getNextFocusElmE(elmNo2, 'cli');
	//modori値の処理
	e.disabled = false;
	if (retval != null) {
	    if (btnAJOne('btnAJSAGYOTANTNM' + str) == false) {
	        elm.focus();
	    }
	} else {
	    elm2.focus();
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

function tabsCom(nNum) {
    var elm = document.getElementById(nowindex);
    elm.value = nNum;
    elm = document.getElementById(lv);
    elm.click();
    return false;
}