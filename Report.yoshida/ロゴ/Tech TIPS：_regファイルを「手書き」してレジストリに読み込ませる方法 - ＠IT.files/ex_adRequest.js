/* �E�F���J���X�N���[����O�Ҕz�M�t���O
   �f�t�H���g false
-------------------------------------------------------*/
ITWS_flag = false;

/* �R���|�W�b�g����
-------------------------------------------------------*/
adcomposite = new Date().getTime();

/* ���t�@���擾
-------------------------------------------------------*/
adreferrer = escape(document.referrer);

/* ITIKW
-------------------------------------------------------*/
if(typeof itikw != 'undefined'){ // �ϐ����݃`�F�b�N
	// console.log('itikw : ' + itikw);
}else{
	itikw = ''; // ��Ő���
}

/* kv_atype
-------------------------------------------------------*/
if(typeof kv_atype != 'undefined'){ // �ϐ����݃`�F�b�N
	// console.log('kv_atype : ' + kv_atype);
}else{
	kv_atype = ''; // ��Ő���
}

/* META
-------------------------------------------------------*/
function getMetaContent(hash){
	getMetaContent.setName(hash['keywords']);
	getMetaContent.setTarget();
	getMetaContent.getMeta();
	getMetaContent.getKeywords();
	if(!hash['encode'] || hash['encode'] == 'yes'){
		return getMetaContent.setContentEncode();
	}else{
		return getMetaContent.setContentNoencode();
	}
};
getMetaContent.setName = function(name){
	this.metaname = name;
};
getMetaContent.setTarget = function(){
	this.target = document.getElementsByTagName('head')[0];
};
getMetaContent.getMeta = function(){
	this.meta = this.target.getElementsByTagName('meta');
};
getMetaContent.getKeywords = function(){
	for(var i = 0; i < this.meta.length; i++){
		if(this.meta[i].name == this.metaname){
			if(this.meta[i].content.match(/\t/)){
				this.content = this.meta[i].content.replace(/\t/g,',');
			}else{
				this.content = this.meta[i].content;
			}
			return true;
		}
	}
	this.content = '';
	return false;
};
getMetaContent.setContentEncode = function(){
	return encodeURIComponent(this.content);
};
getMetaContent.setContentNoencode = function(){
	return this.content;
};

/* adword ����
   1 �x�̏����ŗǂ����߃O���[�o���ɕύX
-------------------------------------------------------*/
// �p�����[�^ scid ������ꍇ word �̐擪�ɓ����i�L�[���[�h�L���̎d�g�݁j
var cms_scid = (function(){
	var rc = getMetaContent({'keywords':'rcid'}); /* meta rcid */
	var sc = '';
	if(rc != ''){
		sc = rc.split('_')[0] + ','; /* ���� rcid �̂��� _ �� split ���� */
	}
	return sc;
})();

// �L�[���[�h�L��
// ��IT ��p
// FD36761

// adserver �ɓn�� word �p�����[�^
var adtag_word = '';
if(kv_atype == 'SP'){
	adtag_word = (cms_scid + getMetaContent({'keywords':'keywords','encode':'no'})).split(',');
}else{
	adtag_word = (cms_scid + itikw + getMetaContent({'keywords':'keywords','encode':'no'})).split(',');
}
// console.log('kv_atype = ' + kv_atype + ' : ' + adtag_word);

/* ���N�G�X�g
-------------------------------------------------------*/
function adRequest(param){

	// �E�F���J���X�N���[����O�Ҕz�M�t���O true �̏ꍇ���N�G�X�g�����I��
	if(ITWS_flag == true) return false;

	// position ���Ȃ���΃��N�G�X�g���Ȃ�
	if(!param['position'] || param['position'] == ''){
		document.write('<!-- no position -->');
		return false;
	}

	// adserver �ɓn�� word �p�����[�^�� encode
	param['key'] = encodeURIComponent(adtag_word);

	// meta adpath
	var adpath = getMetaContent({'keywords':'adpath'});

	// ������ PATH ��D��A�Ȃ���� meta adpath
	if(!param['path'] || param['path'] == ''){
		param['path'] = adpath;
	}else{
		param['path'] = encodeURIComponent(param['path']);
	}

	// �L���b�V���΍��p�����_���l
	param['random'] = new Date().getTime();

	// �����R�[�h�w��
	if(param['oe'] == 'utf-8'){
		param['oe'] = 'utf-8';
	}else{
		param['oe'] = 'shift_jis';
	}

	// ���N�G�X�g URL
	param['src'] = '//dlv.itmedia.jp/adsv/v1?posall=' + param['position'] + '&oe=' + param['oe'] + '&nurl=' + escape(document.URL) + '&fp=' + param['path'] + '&word=' + param['key'] + '&rnd=' + param['random'] + '&composite=' + adcomposite + '&ref=' + adreferrer;

	// SCRIPT or IFRAME�idefault = js�j
	if(param['t'] == 'html' || param['t'] == 'iframe'){
		param['src'] += '&t=html';
		param['script'] = '<iframe src="' + param['src'] + '" frameborder="0" scrolling="no" marginwidth="0" marginheight="0" style="border:0;margin:0;padding:0;width:' + param['width'] + 'px;height:' + param['height'] + 'px;"><\/iframe>';
	}else{
		param['src'] += '&t=js';
		param['script'] = '<script src="' + param['src'] + '"><\/script>';
	}

	// �|�W�V��������
	// �E�F���J���X�N���[��
	if(param['position'] == 'ITWS' || param['position'] == 'WELCOME'){
		adRequest.posITWS(param);

	// �ʏ�|�W�V����
	}else{
		adRequest.pos(param);
	}
	return true;
};

/* �ʏ�|�W�V����
-------------------------------------------------------*/
adRequest.pos = function(param){
	document.write(param['script']);
	return true;
};

/* ��O�|�W�V���� - �E�F���J���X�N���[���iITWS�j
-------------------------------------------------------*/
adRequest.posITWS = function(param){
	if(location.pathname.match(/spv/)){
		var ws = WELCOMESCREEN(masterChannel);
	}else{
		var ws = WELCOMESCREEN(masterChannel());
	}
	if(ws == true){
		document.write(param['script']);
		return true;
	}
	return false;
};
