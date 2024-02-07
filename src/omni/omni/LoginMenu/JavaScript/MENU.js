window.document.onkeydown = KeyPress;
function KeyPress() {
    if (event != null) {

        //　イベントを利用済みの場合は、イベントを無効にする。
        if (event.keyCode == 0) {
            return false;
        }
        //バックスペースキーを無効にする(HistryBack抑止)
        if (event.keyCode == 8) {
            if ((document.activeElement.tagName == 'INPUT') && (document.activeElement.type == 'button')) {
                // ボタンは無効
                return false;
            } else if ((document.activeElement.tagName == 'INPUT') && (document.activeElement.type == 'text')) {
                // テキストボックスは有効
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

        // Function Key(F1～F12)を無効にする。
        if (event.keyCode >= 112 && event.keyCode <= 123) {
            var code = event.keyCode;
            if (event.keyCode != 123) {
                event.keyCode = 0;
            }
            return false;
        }
    }
}

function tabsCom(nNum) {
    var elm = document.getElementById(nowindex);
    elm.value = nNum;
    elm = document.getElementById(lv);
    elm.click();
    return false;
}

function mouseON(e) {
    e.style.backgroundColor = '#ccffff';
}

function mouseOUT(e) {
    e.style.backgroundColor = '';
}

function LOGOUT() {
    window.open('Login.aspx', '', '');
    (window.open('', '_top').opener = top).close();
    return false;
}
