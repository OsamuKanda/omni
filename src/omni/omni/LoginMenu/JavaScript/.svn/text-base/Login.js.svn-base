function pageLoad(e) {
    var elm = document.getElementById(LogST);

    if (elm.value.match(/^LoginOK\d/)) {
        document.getElementById(msg).innerHTML = '';
        var nWidth = 1000;
        var nHeight = 655;
        var toppos = Number((window.screen.height - nHeight) / 2);
        var leftpos = Number((window.screen.width - nWidth) / 2);
        var option = "width=" + nWidth + "px,height=" + nHeight + "px,top=" + toppos + ",left=" + leftpos + ",center:yes,status:no,scroll:no,resizable:no";
        //window.showModalDialog('MainMenu.aspx', 'menu', option);
        if (elm.value == 'LoginOK1') {
            window.open('MainMenu.aspx', '', option);
        } else {
            window.open('../../Maintainance/OMN000/Contents/OMN000.aspx', '', option);
        }
        (window.open('', '_top').opener = top).close();
    }
}

window.document.onkeydown = KeyPress;
function KeyPress() {
    if (event != null) {
        // Function Key(F1～F12)を無効にする。
        if (event.keyCode >= 112 && event.keyCode <= 123) event.keyCode = 0;
        document.getElementById(msg).innerHTML = '';
    }
}

function display() {
    var chk = false;
    if (navigator.appName == 'Microsoft Internet Explorer') {
        var bsr = navigator.appName;
        var ua = navigator.userAgent;
        var index = ua.indexOf("MSIE", 0);
        if (index != -1) {
            var verLast = ua.indexOf(";", index);
            var ver = ua.substring(index + 5, verLast);
            //alert("ブラウザ:" + bsr + "  バージョン:" + ver);
            ver = parseInt(ver);
            if (ver >= 7) {
                chk = true;
            }
        }
    }
    if (chk == false) {
        document.getElementById("chkBase").innerHTML = '<p>表示できません。</p>';
    } else {
        document.getElementById("divLogin").style.display = 'block';
        document.getElementById(UserID).focus();
    }
}

function KeyDown(e) {
    if (event != null) {
        if (event.keyCode == 13) {
            event.keyCode = 9;
        }
    }
}

