(function(){
	// ���s SP �r���[�Ώے[������
	// if(navigator.userAgent.indexOf('iPod') != -1) return false;
	// if(navigator.userAgent.indexOf('iPhone') == -1 && navigator.userAgent.indexOf('Android') == -1) return false;

	// path get
	var path = location.pathname;

	// useragent
	var ua = navigator.userAgent;

	// iPad ���O
	if(ua.indexOf('iPad') != -1) return false;

	// Android Tablet ���O
	if(ua.indexOf('Android') != -1 && ua.indexOf('Mobile') == -1) return false;

	// �Ώے[����������
	if((ua.indexOf('iPhone') != -1 || ua.indexOf('iPod') != -1 || ua.indexOf('Android') != -1 || ua.indexOf('Windows Phone') != -1)){

		// �L���^�L�[���[�h�C���f�b�N�X�^SPV �Ή������ҏW���W�ȊO�͏������Ȃ�
//		if(!location.pathname.match(/\/.+\/(articles|news)\/\d\d\d\d\/\d\d\/news/) && !location.pathname.match(/^\/keywords\//) && swspv != '1') return false;

		var s = path.split('/');

		// �����g�b�v
		if(path.match(/^\/$/) || path.match(/^\/root\/$/) || path.match(/^\/root\/index[0-9]?\.html$/)){
			console.log('�����g�b�v�ł�');

		// �`�����l���g�b�v
		}else if(s.length == 3 || (s.length == 4 && path.match('root'))){
			console.log('�`�����l���g�b�v�ł�' + s.length);

		// �L��
		}else if(path.match(/\/.+\/(articles|news)\/\d\d\d\d\/\d\d\/news/)){
			console.log('�L���ł�');

		// �L�[���[�h�C���f�b�N�X
		}else if(path.match(/^\/keywords\//)){
			console.log('�L�[���[�h�C���f�b�N�X�ł�');

		// �T�u�g�b�v�i�c�[���j
		}else if(swspv == '1'){
			console.log('�T�u�g�b�v�ł�');

		}else{
			console.log('�Y���Ȃ�' + s.length);
			return false;
		}

		var target = document.getElementsByTagName('body')[0];

		var stylesheet = document.createElement('link');
		stylesheet.setAttribute('rel','stylesheet');
		stylesheet.setAttribute('type','text/css');
		stylesheet.setAttribute('media','all');
		stylesheet.setAttribute('href','/js/itmid/smartPhoneHeadLink.css');

		var div = document.createElement('div');
		div.setAttribute('id','spvLinkTop');
		div.className = 'spvLink';
		div.innerHTML = '<div class="spvLinkIn"><a href="javascript:void(0);" onClick="movePC2SP({\'name\':\'pc2sp\',\'click\':true});"><span>�X�}�[�g�t�H���p�\���ɕύX</span></a></div>';
		target.insertBefore(div,target.childNodes[0]);
		var div2 = div.cloneNode(true);
		div2.setAttribute('id','spvLinkBtm');

		window.onload = function(){
			target.insertBefore(stylesheet,target.childNodes[0]);
			target.appendChild(div2);
		}

		return true;
	}
})();
