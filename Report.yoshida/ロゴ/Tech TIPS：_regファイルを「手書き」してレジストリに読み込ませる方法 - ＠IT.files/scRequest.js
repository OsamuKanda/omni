/* SC ���N�G�X�g
-------------------------------------------------------*/
function scRequest(data){
	function $(){
		var elements = new Array();
		for(var i = 0; i < arguments.length; i++){
		var element = arguments[i];
		if(typeof element == 'string')
			element = document.getElementById(element);
		if(arguments.length == 1)
			return element;
			elements.push(element);
		}
		return elements;
	};
	var urlrep = document.URL.replace( /(\?|#).*$/, '');
	s.pageName = urlrep;
	s.server = document.domain;

	if(!data['s.channel'] || data['s.channel'] == ''){
		// s.channel ����`
		s.channel = 'null';
	}else{
		// s.channel ��`
		s.channel = data['s.channel'];
	}

	if(!data['s.prop20'] || data['s.prop20'] == ''){
		// s.prop20 ����`
		s.prop20 = s.channel;
	}else{
		// s.prop20 ��`
		s.prop20 = data['s.prop20'];
	}

	if(!data['s.prop22'] || data['s.prop22'] == ''){
	}else{
		// s.prop22 ��`
		s.prop22 = data['s.prop22'];
	}

	s.pageType = '';
	if(!data['dp']){
		// �p�X�w��Ȃ��� adpath �g�p
		var adpath = decodeURIComponent(getMetaContent({'keywords':'adpath'}));
		var shier = adpath.replace('ITmedia','itm').split('/');
	}else{
		// �p�X�w�肠��
		var shier = data['dp'].split(',');
	}
	// path �u���ݒ�i6 �K�w�ڂ܂őΉ��j
	if(data['pathrep1']) shier[0] = data['pathrep1'];
	if(data['pathrep2']) shier[1] = data['pathrep2'];
	if(data['pathrep3']) shier[2] = data['pathrep3'];
	if(data['pathrep4']) shier[3] = data['pathrep4'];
	if(data['pathrep5']) shier[4] = data['pathrep5'];
	if(data['pathrep6']) shier[5] = data['pathrep6'];

	s.hier1 = shier.join(',');
	s.hier2 = shier.join(',').replace(shier[0] + ',','');
	s.hier3 = shier.join(',').replace(shier[0] + ',' + shier[1] + ',','');
	s.prop1 = urlrep;
	s.prop2 = shier.join(',');
	if($('update') == undefined){
		s.prop3 = '';
	}else{
		s.prop3 = $('update').innerHTML.replace(/\d+��\s�X�V/,'');
	}
	if($('byline') == undefined){
		s.prop4 = '';
	}else{
		s.prop4 = $('byline').innerHTML.replace(/<b>|<\/b>/ig,'').replace(/\[|\]/g,'');
	}
	/* s.prop5 : ad link name reserves */
	s.prop5 = '';
	if(!data['s.prop6'] || data['s.prop6'] == ''){
		// s.prop6 ����`
		s.prop6 = shier[2];
	}else{
		// s.prop6 ��`
		s.prop6 = data['s.prop6'];
	}

	if(!data['s.prop7'] || data['s.prop7'] == ''){
		// s.prop7 ����`
		s.prop7 = shier[2] + ',' + shier[3];
	}else{
		// s.prop7 ��`
		s.prop7 = data['s.prop7'];
	}
	s.prop8 = document.title;
	s.prop9 = navigator.userAgent;
	if(data['s.prop10']){
		s.prop10 = data['s.prop10'];
	}else{
		s.prop10 = urlrep;
	}
	/* s.prop13 : document.URL reserves */
	/* E-commerce Variables */
	s.campaign = '';
	s.state = '';
	s.zip = '';
	s.events = 'event3';
	s.products = '';
	s.purchaseID = '';
	s.eVar1 = '';
	s.eVar2 = '';
	s.eVar3 = '';
	s.eVar4 = '';
	s.eVar5 = '';
/************* DO NOT ALTER ANYTHING BELOW THIS LINE ! **************/
	var s_code = s.t();
	if(s_code)document.write(s_code);
	if(navigator.appVersion.indexOf('MSIE') >= 0) document.write(unescape('%3C') + '\!-' + '-');
};
/*--------------------------------------------------------------------*/
/* �g��摜 ONLY */
/*--------------------------------------------------------------------*/
scRequest.largeimage_prop10 = function(article_url){
	var d = document;
	var my_url = d.URL.replace(/(\?|#).*$/,''); /* http://{DOMAIN}/l/im/{CH}/articles/{YYMM}/{DD}/{IMGFILENAME} */
	/*
		article_url ���L�� URL �p�^�[���Ɉ�v���Ȃ��ꍇ�g��摜�y�[�W URL �����̂܂ܕԂ�
	*/
	console.log(article_url);
	if(!article_url.match(/\/.+\/articles\/\d{4}\/\d{2}\/news/)){
		return my_url;
	}
	/*
		article_url ���� news{NNN|NN} �����o��
		�u/�v�ŋ�؂����Ō�̕����񂩂� .html ���폜����
	*/
	var article_url_split = article_url.split('/');
	var article_filename = article_url_split[article_url_split.length - 1].replace(/(\_\d{1,})?\.html.+$/,''); /* news{NNN|NN} */
	/*
		my_url {IMGFILENAME} �̑O�� article_filename �ŊK�w�����
	*/
	var my_url_split = my_url.split('/');
	var my_filename = my_url_split[my_url_split.length - 1]; /* {IMGFILENAME} */
	my_url_split.pop(); /* http://{DOMAIN}/l/im/{CH}/articles/{YYMM}/{DD} */
	var largeimage_prop10 = my_url_split.join('/') + '/' + article_filename + '/' + my_filename; /* http://{DOMAIN}/l/im/{CH}/articles/{YYMM}/{DD}/news{NNN|NN}/{IMGFILENAME} */
	console.log(largeimage_prop10);
	return largeimage_prop10;
};
