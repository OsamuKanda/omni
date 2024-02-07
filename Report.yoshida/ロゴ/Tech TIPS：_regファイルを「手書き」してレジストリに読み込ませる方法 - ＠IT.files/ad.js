/* WELCOME SCREEN
-------------------------------------------------------*/
function deliteCookie(key){
	var dt = new Date();
	dt.setYear(dt.getYear() - 1);
	var str = key + '=; path=/; expires=' + dt.toGMTString();
	document.cookie = str;
	/*setTimeout('deliteCookie.ref()',500);*/
};
deliteCookie.ref = function(){
	location.reload();
};
function WELCOMESCREEN(masterChannel){
	if(!masterChannel) return false;
	if(navigator.cookieEnabled != true) return false;
	if(document.domain.match(/(preview|localhost)/) && WELCOMESCREEN.getCookie('ITWS_CHANNEL_' + masterChannel)) WELCOMESCREEN.debugMode(masterChannel);
	if(WELCOMESCREEN.timeLimit(masterChannel) == false){
		return false;
	}else{
		return true;
	}
};
WELCOMESCREEN.debugMode = function(masterChannel){
	var cookieval = WELCOMESCREEN.getCookie('ITWS_CHANNEL_' + masterChannel);
	var expires = Number(WELCOMESCREEN.getCookie('ITWS_CHANNEL_' + masterChannel).split('&')[1].replace('expires=','') * 24);
	var a = Date.parse(WELCOMESCREEN.nowYMDSHM);
	var ss = Math.floor(a / 1000);
	var dd = Math.floor(ss / (60 * 60 * 24));
	var ss = ss - dd * 60 * 60 * 24;
	var hh = Math.floor(ss / (60 * 60));
	var ss = ss - hh * 60 * 60;
	var mt = Math.floor(ss / 60);
	var ss = ss % 60;
	var time = hh + ':' + mt + ':' + ss;
	document.write('<div style="font-size:10px;color:#FFF;padding:2px;cursor:pointer;" onClick="deliteCookie(\'ITWS_CHANNEL_' + masterChannel + '\');alert(\'Cookie���폜���܂���\');location.reload();"> ���̃y�[�W�ɂ̓E�F���J���X�N���[�������e����Ă��܂��i' + cookieval + '���ԁj</div>');
/*
	document.write('<div style="font-family:HiraKakuProN-W3, Meiryo, \'���C���I\', ArialMT, Hiragino Kaku Gothic Pro, \'�q���M�m�p�S Pro W3\', Osaka, Verdana, \'�l�r �o�S�V�b�N\';border:2px solid #CCC;background:#FFF;font-size:10px;">');
	document.write('<div style="background:#EEE;padding:3px;font-size">�E�F���J���X�N���[���f�o�b�O���[�h�ipreview�̂݁j</div>');
	document.write('<div style="padding:5px;">');
	document.write('<table width="100%" border="0" cellpadding="0" cellspacing="0">');
//	document.write('<tr><th style="text-align:left;width:120px;">���񌩂��Ƃ�</th><td>' + WELCOMESCREEN.getCookie('ITWS_CHANNEL_' + masterChannel) + '�i' + Date.parse(WELCOMESCREEN.getCookie('ITWS_CHANNEL_' + masterChannel)) + '�j</td></tr>');
	document.write('<tr><th style="text-align:left;width:120px;">���񌩂��Ƃ�</th><td>' + WELCOMESCREEN.getCookie('ITWS_CHANNEL_' + masterChannel) + '</td></tr>');
//	document.write('<tr><th style="text-align:left;">������</th><td>' + WELCOMESCREEN.nowYMDSHM + '�i' + Date.parse(WELCOMESCREEN.nowYMDSHM) + '�j</td></tr>');
	document.write('<tr><th style="text-align:left;">������</th><td>' + WELCOMESCREEN.nowYMDSHM + '</td></tr>');
	document.write('<tr><th style="text-align:left;">COOKIE�ێ�����</th><td>' + (WELCOMESCREEN.cookieTime * 60) + '��</td></tr>');
	document.write('<tr><th style="text-align:left;">�o�ߎ���</th><td>' + time + '</td></tr>');
	document.write('</table>');
	document.write('<div><button onClick="deliteCookie(\'ITWS_CHANNEL_' + masterChannel + '\');" style="font-size:10px;">COOKIE�폜</button></div>');
	document.write('</div></div>');
*/
	return true;
};
WELCOMESCREEN.getCookie = function(key){
	var sCookie = document.cookie;
	var aData = sCookie.split(';');
	var oExp = new RegExp(' ', 'g');
	key = key.replace(oExp, '');
	var i = 0;
	while (aData[i]){
		var aWord = aData[i].split('=');
		aWord[0] = aWord[0].replace(oExp, '');
		if(key == aWord[0]) return unescape(aWord[1]);
		if(++i >= aData.length) break;
	}
	return '';
};
WELCOMESCREEN.nowTime = new Date();
WELCOMESCREEN.nowMS = WELCOMESCREEN.nowTime.getTime();
WELCOMESCREEN.nowFullYear = WELCOMESCREEN.nowTime.getFullYear();
WELCOMESCREEN.nowMonth = WELCOMESCREEN.nowTime.getMonth() + 1;
WELCOMESCREEN.nowDate = WELCOMESCREEN.nowTime.getDate();
WELCOMESCREEN.nowSeconds = WELCOMESCREEN.nowTime.getSeconds();
WELCOMESCREEN.nowHours = WELCOMESCREEN.nowTime.getHours();
WELCOMESCREEN.nowMinutes = WELCOMESCREEN.nowTime.getMinutes();
WELCOMESCREEN.nowSeconds = WELCOMESCREEN.nowTime.getSeconds();
WELCOMESCREEN.nowYMDSHM = WELCOMESCREEN.nowFullYear + '/' + WELCOMESCREEN.nowMonth + '/' + WELCOMESCREEN.nowDate + ' ' + WELCOMESCREEN.nowHours + ':' + WELCOMESCREEN.nowMinutes + ':' + WELCOMESCREEN.nowSeconds;
WELCOMESCREEN.nowYMDSHMparse = Date.parse(WELCOMESCREEN.nowYMDSHM);
WELCOMESCREEN.timeLimit = function(masterChannel){

	// COOKIE �Ȃ��^�����؂�i�A�h�e���v���[�g���Őݒ�j
	if(!WELCOMESCREEN.getCookie('ITWS_CHANNEL_' + masterChannel)){
		return true;

	// COOKIE ����
	}else{
		return false;
	}
};
