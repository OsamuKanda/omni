/* ウェルカムスクリーン第三者配信フラグ
   デフォルト false
-------------------------------------------------------*/
ITWS_flag = false;

/* コンポジット制御
-------------------------------------------------------*/
adcomposite = new Date().getTime();

/* リファラ取得
-------------------------------------------------------*/
adreferrer = escape(document.referrer);

/* ITIKW
-------------------------------------------------------*/
if(typeof itikw != 'undefined'){ // 変数存在チェック
	itikw += ','; // 末尾に , をつける
	// console.log('itikw : ' + itikw);
}else{
	itikw = ''; // 空で生成
}

/* リクエスト
-------------------------------------------------------*/
function adRequest(param){

	// ウェルカムスクリーン第三者配信フラグ true の場合リクエストせず終了
	if(ITWS_flag == true) return false;

	// position がなければリクエストしない
	if(!param['position'] || param['position'] == ''){
		document.write('<!-- no position -->');
		return false;
	}

	// パラメータ scid がある場合 word の先頭に入れる（キーワード広告の仕組み）
	var scid = (function(){
		var rc = getMetaContent({'keywords':'rcid'}); /* meta rcid */
		var sc = '';
		if(rc != ''){
			sc = rc.split('_')[0] + ','; /* 元は rcid のため _ で split する */
		}
		return sc;
	})();

	// キーワード広告
	param['key'] = scid + encodeURIComponent(itikw) + getMetaContent({'keywords':'keywords'});
	// console.log('ad_word : ' + param['position'] + ' : ' + decodeURIComponent(param['key']));

	// meta adpath
	var adpath = getMetaContent({'keywords':'adpath'});

	// 引数の PATH を優先、なければ meta adpath
	if(!param['path'] || param['path'] == ''){
		param['path'] = adpath;
	}else{
		param['path'] = encodeURIComponent(param['path']);
	}

	// キャッシュ対策用ランダム値
	param['random'] = new Date().getTime();

	// 文字コード指定
	if(param['oe'] == 'utf-8'){
		param['oe'] = 'utf-8';
	}else{
		param['oe'] = 'shift_jis';
	}

	// リクエスト URL
	param['src'] = '//dlv.itmedia.jp/adsv/v1?posall=' + param['position'] + '&oe=' + param['oe'] + '&nurl=' + escape(document.URL) + '&fp=' + param['path'] + '&word=' + param['key'] + '&rnd=' + param['random'] + '&composite=' + adcomposite + '&ref=' + adreferrer;

	// SCRIPT or IFRAME（default = js）
	if(param['t'] == 'html' || param['t'] == 'iframe'){
		param['src'] += '&t=html';
		param['script'] = '<iframe src="' + param['src'] + '" frameborder="0" scrolling="no" marginwidth="0" marginheight="0" style="border:0;margin:0;padding:0;width:' + param['width'] + 'px;height:' + param['height'] + 'px;"><\/iframe>';
	}else{
		param['src'] += '&t=js';
		param['script'] = '<script src="' + param['src'] + '"><\/script>';
	}

	// ポジション判定
	// ウェルカムスクリーン
	if(param['position'] == 'ITWS' || param['position'] == 'WELCOME'){
		adRequest.posITWS(param);

	// フロントエンドマルチルックアップ（mn , pcuser , mobile , lifestyle , smartjapan , ait , ebook , mm , dc , news , executive , enterprise , makoto , bizid , style はアドフレーム完全対応のため除外する）
	}else if(param['position'].match(/(ISTLC|ISALR|ISALC|ISTCPB|ISTCPT|ISAL)/) && !param['path'].match(/%2Fmn|%2Fee|%2Fedn|%2Fpcuser|%2Fmobile|%2Flifestyle|%2Fsmartjapan|%2Fait|%2Febook|%2Fmm|%2Fdc|%2Fnews|%2Fexecutive|%2Fenterprise|%2Fmakoto|%2Fbizid|%2Fstyle|%2Freview/)){
		
		adRequest.posMULTI(param);

// 通常ポジション
	}else{
		if(param['dom'] == true){
			adRequest.posdom(param);
		}else{
			adRequest.pos(param);
		}
	}
	return true;
};

/* 通常ポジション
-------------------------------------------------------*/
adRequest.pos = function(param){
	document.write(param['script']);
	return true;
};

/* 通常ポジション（DOM バージョン）
-------------------------------------------------------*/
adRequest.posdom = function(param){
	document.getElementsByTagName('head')[0].appendChild(param['script']);
	return true;
};

/* 例外ポジション - ウェルカムスクリーン（ITWS）
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

/* 例外ポジション - マルチルックアップ
-------------------------------------------------------*/
adRequest.posMULTI = function(param){
	document.write(adRequest.template(param)['header']);
	document.write(param['script']);
	document.write(adRequest.template(param)['footer']);
	adRequest.posMULTIsync(param);
	return true;
};

/* マルチルックアップ同期非同期対応
-------------------------------------------------------*/
adRequest.posMULTIsync = function(param){
	var UA = navigator.userAgent;

	// 遅延処理
	setTimeout(function(){

		// アドタグを囲む id（末尾の数値を外す）
		var wrapid = param['position'].replace(/^\d\d?/,'');
	
		// id がなければ処理しない
		if(!document.getElementById(wrapid)) return false;
	
		// アドタグを囲む div
		var wrap = document.getElementById(wrapid);

		// アドタグで使われる全ての div
		var div = wrap.getElementsByTagName('div');

		// colBoxIndex が入る div
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

		// colBoxInner がなければ処理しない
		if(colBoxInner.length == 0) return false;

		// colBoxIndex（広告）カウント
		var colBoxIndex = [];

		for(var i = 0; i < div.length; i++){
			if(div[i].className.match('colBoxIndex')){

				// colBoxIndex をカウント
				colBoxIndex.push(div[i]);

				// colBoxIndex を colBoxInner に入れ直す（IE ONLY）
				if(UA.indexOf('MSIE') != -1) colBoxInner[0].appendChild(div[i]);

			}
		}

		// noad だったら枠を削除
		if(colBoxIndex.length == 0) wrap.style.display = 'none';

	},800);

	return true;
};

/* マルチルックアップテンプレート
-------------------------------------------------------*/
adRequest.template = function(param){

	// heading 指定がなければデフォルト「Special」
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
