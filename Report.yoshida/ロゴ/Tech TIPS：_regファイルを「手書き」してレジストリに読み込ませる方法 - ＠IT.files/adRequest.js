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
	itikw += ','; // ������ , ������
	// console.log('itikw : ' + itikw);
}else{
	itikw = ''; // ��Ő���
}

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

	// �p�����[�^ scid ������ꍇ word �̐擪�ɓ����i�L�[���[�h�L���̎d�g�݁j
	var scid = (function(){
		var rc = getMetaContent({'keywords':'rcid'}); /* meta rcid */
		var sc = '';
		if(rc != ''){
			sc = rc.split('_')[0] + ','; /* ���� rcid �̂��� _ �� split ���� */
		}
		return sc;
	})();

	// �L�[���[�h�L��
	param['key'] = scid + encodeURIComponent(itikw) + getMetaContent({'keywords':'keywords'});
	// console.log('ad_word : ' + param['position'] + ' : ' + decodeURIComponent(param['key']));

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

	// �t�����g�G���h�}���`���b�N�A�b�v�imn , pcuser , mobile , lifestyle , smartjapan , ait , ebook , mm , dc , news , executive , enterprise , makoto , bizid , style �̓A�h�t���[�����S�Ή��̂��ߏ��O����j
	}else if(param['position'].match(/(ISTLC|ISALR|ISALC|ISTCPB|ISTCPT|ISAL)/) && !param['path'].match(/%2Fmn|%2Fee|%2Fedn|%2Fpcuser|%2Fmobile|%2Flifestyle|%2Fsmartjapan|%2Fait|%2Febook|%2Fmm|%2Fdc|%2Fnews|%2Fexecutive|%2Fenterprise|%2Fmakoto|%2Fbizid|%2Fstyle|%2Freview/)){
		
		adRequest.posMULTI(param);

// �ʏ�|�W�V����
	}else{
		if(param['dom'] == true){
			adRequest.posdom(param);
		}else{
			adRequest.pos(param);
		}
	}
	return true;
};

/* �ʏ�|�W�V����
-------------------------------------------------------*/
adRequest.pos = function(param){
	document.write(param['script']);
	return true;
};

/* �ʏ�|�W�V�����iDOM �o�[�W�����j
-------------------------------------------------------*/
adRequest.posdom = function(param){
	document.getElementsByTagName('head')[0].appendChild(param['script']);
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

/* ��O�|�W�V���� - �}���`���b�N�A�b�v
-------------------------------------------------------*/
adRequest.posMULTI = function(param){
	document.write(adRequest.template(param)['header']);
	document.write(param['script']);
	document.write(adRequest.template(param)['footer']);
	adRequest.posMULTIsync(param);
	return true;
};

/* �}���`���b�N�A�b�v�����񓯊��Ή�
-------------------------------------------------------*/
adRequest.posMULTIsync = function(param){
	var UA = navigator.userAgent;

	// �x������
	setTimeout(function(){

		// �A�h�^�O���͂� id�i�����̐��l���O���j
		var wrapid = param['position'].replace(/^\d\d?/,'');
	
		// id ���Ȃ���Ώ������Ȃ�
		if(!document.getElementById(wrapid)) return false;
	
		// �A�h�^�O���͂� div
		var wrap = document.getElementById(wrapid);

		// �A�h�^�O�Ŏg����S�Ă� div
		var div = wrap.getElementsByTagName('div');

		// colBoxIndex ������ div
		if(param['position'].match('ITOP')){
			var colBoxInner = [];
			for(var i = 0; i < div.length; i++){
				if(div[i].className.match('colBoxUlist')){
					colBoxInner.push(div[i].getElementsByTagName('ul')[0]);
					break;
				}
			}
		}else{
			var colBoxInner = [];
			for(var i = 0; i < div.length; i++){
				if(div[i].className.match('colBoxInner')){
					colBoxInner.push(div[i]);
					break;
				}
			}
		}

		// colBoxInner ���Ȃ���Ώ������Ȃ�
		if(colBoxInner.length == 0) return false;

		// colBoxIndex�i�L���j�J�E���g
		var colBoxIndex = [];

		for(var i = 0; i < div.length; i++){
			if(div[i].className.match('colBoxIndex')){

				// colBoxIndex ���J�E���g
				colBoxIndex.push(div[i]);

				// colBoxIndex �� colBoxInner �ɓ��꒼���iIE ONLY�j
				if(UA.indexOf('MSIE') != -1) colBoxInner[0].appendChild(div[i]);

			}
		}

		// noad ��������g���폜
		if(colBoxIndex.length == 0) wrap.style.display = 'none';

	},800);

	return true;
};

/* �}���`���b�N�A�b�v�e���v���[�g
-------------------------------------------------------*/
adRequest.template = function(param){

	// heading �w�肪�Ȃ���΃f�t�H���g�uSpecial�v
	if(!param['heading'] || param['heading'] == '') param['heading'] = 'Special';
	var header = [];
	header.push('<div id="colBox' + param['position'].replace(/^\d\d?/,'') + '" class="colBox colBox' + param['position'].replace(/^\d\d?/,'') + '">');
	header.push('<div class="colBoxOuter">');
	header.push('<div class="colBoxHead"><h2>' + param['heading'] + '</h2><span class="colBoxHeadSubtxt">- PR -</span></div>');
	header.push('<div class="colBoxInner">');
	if(param['position'].match('ITOP')) header.push('<div class="colBoxIndex"><div class="colBoxUlist"><ul>');
	var footer = [];
	if(param['position'].match('ITOP')) footer.push('</ul></div></div>');
	footer.push('</div></div></div>');
	return {'header':header.join(''),'footer':footer.join('')};
};

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
