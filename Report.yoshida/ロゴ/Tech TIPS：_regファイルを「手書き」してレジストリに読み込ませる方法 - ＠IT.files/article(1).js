function dispSerialBackNumber(json){

	/**
	* ��IT�A�ڃo�b�N�i���o�[�ݒu�m�F
	* ��IT CMS �ڍs
	*
	* �O�����
	* ���ʃe���v���[�g���g�p���Ă��邱��
	*/

	// �ݒu�m�F�p
/*
	var str = getScriptThis(document).src.split('?');
	var path = str[1].split('&');
	document.write(path[0]);
	return true;
*/

	// �A�� API �p�[�T
	if(json['articles'].length == 0) return false;
	var header = [];
	header.push('<div class="backNumBoxRap"><div class="backNumBox">');
	header.push('<strong>�u' + json['serial_name'] + '�v�ŐV�L���o�b�N�i���o�[</strong>');
	header.push('<div class="backNumBoxIn"><ul>');

	var contents = [];
	for(var i = 0; i < json['articles'].length; i++){
		var data = json['articles'][i];
		if(!data['title']) continue;
		contents.push('<li class="bn' + data['uri'] + '"><span><a href="' + data['uri'] + '">' + data['title'] + '</a></span></li>');
	}

	var footer = [];
	footer.push('</ul></div></div></div>');

	document.write(header.join('\n') + contents.join('\n') + footer.join('\n'));
	return true;
};

// �L�����E�B�W�F�b�g�p
function setBtmTwTab() {
	var fdata = getForum(getMETA('extraid').content)[0];
	if(!fdata) {
		var params = getSnsOption();
	} else {
		var params = getSnsOption(fdata.path);
	}
	var html = '<a class="twitter-timeline" href="https://twitter.com/' + params.twitterid + '" data-widget-id="' + params.tw_widget_id + '" width="637" height="350">@' + params.twitterid + ' ����̃c�C�[�g</a>' + 
			   '<script>!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0];if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src="//platform.twitter.com/widgets.js";fjs.parentNode.insertBefore(js,fjs);}}(document,"script","twitter-wjs");<\/script>';
	document.write(html);
};
function setBtmFbTab() {
	// fb��DOM�ɋL��URL��1�y�[�W�ڂ�ݒ�
	jQuery('#artBtmSnsFb').children('div.fb-comments').attr({'data-href': "http://www.atmarkit.co.jp" + getUrl()['path_cutpageing_cutparam'] });
	
	// fb�R�����g�̎��s����
	(function(d, s, id) {
		var js, fjs = d.getElementsByTagName(s)[0];
			if (d.getElementById(id)) return;
		js = d.createElement(s); js.id = id;
		js.src = "//connect.facebook.net/ja_JP/all.js#xfbml=1&appId=221728961311992";
	fjs.parentNode.insertBefore(js, fjs);
	}(document, 'script', 'facebook-jssdk'));
};
//�o�b�N�i���o�[�g�����e�X�g

jQuery(window).load(function(){
	// �o�b�N�i���o�[�{�b�N�X�A�N�e�B�u�\��
	if(jQuery('.backNumBox li').length != 0) {
		jQuery('.backNumBox li').each(function(){
			if(location.pathname.match(jQuery('a',this).attr('href').replace(location.host,'').replace('.html',''))) {
				jQuery(this).addClass('backNumBoxListActive');
			} else if(location.pathname.match(jQuery('a',this).attr('href').replace("http://www.atmarkit.co.jp",'').replace('.html',''))){
				jQuery(this).addClass('backNumBoxListActive');
			}
		
		})

		if(jQuery('.backNumBox ul').height() > 200){
			jQuery('.backNumBox ul').addClass('backNumBoxScroll');
		} else {
			//
		}

	} else {
		return false;
	};
	// ���o�b�N�i���o�[�{�b�N�X�p CSS ���폜
	if(jQuery('#backNumBox').length != 0) {
		jQuery('#backNumBox').prev().remove();
	} else {
		return false;
	};
});




/* �L���w�b�_�\���p
--------------------------------------------------------------------- */
function setArticlHeader(json) {
	if(!json) return false;
	var metaTag = getMETA('keywords');
	if(!metaTag) return false;
	var kwdText = metaTag.getAttribute('content');
	var kwdList = metaTag.getAttribute('content').split(',');
	if((kwdText.match(/PR/))&&(!kwdText.match(/Editors Eye/))) return false;
	//if(((kwdText.match(/�Ɩ��A�v��Insider/))||(kwdText.match(/PR/)))&&(!kwdText.match(/Editors Eye/))) return false;
	var data = json['data'];
	for(var i = 0; i < data.length; i++) {
		if(!data[i]['KEYWORDS']) continue;
		for(var j = 0; j <  kwdList.length; j++) {
			if(data[i]['KEYWORDS'] != kwdList[j]) continue;
			var tgt = document.getElementById('tmplNewsIn');
			var headerIcon = document.createElement('div');
			headerIcon.className = 'colBox colBoxArticleHeader';
			headerIcon.innerHTML = '<div class="colBoxOuter"><div class="colBoxInner"><div class="colBoxIndex"><div class="colBoxIcon">' +
									'<a href="' + data[i]['URL'] + '" title="' + data[i]['NAME'] + '"><img src="' + imgSrv() + data[i]['PATH'] + '" alt="' + data[i]['NAME'] + '"></a>' +
									'</div></div></div></div>';
			tgt.insertBefore(headerIcon,tgt.firstChild);
			return true;
		}
	}
	return false;
}