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
	// console.log('itikw : ' + itikw);
}else{
	itikw = ''; // 空で生成
}

/* kv_atype
-------------------------------------------------------*/
if(typeof kv_atype != 'undefined'){ // 変数存在チェック
	// console.log('kv_atype : ' + kv_atype);
}else{
	kv_atype = ''; // 空で生成
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

/* adword 処理
   1 度の処理で良いためグローバルに変更
-------------------------------------------------------*/
// パラメータ scid がある場合 word の先頭に入れる（キーワード広告の仕組み）
var cms_scid = (function(){
	var rc = getMetaContent({'keywords':'rcid'}); /* meta rcid */
	var sc = '';
	if(rc != ''){
		sc = rc.split('_')[0] + ','; /* 元は rcid のため _ で split する */
	}
	return sc;
})();

// キーワード広告
// ＠IT 専用
// FD36761

// adserver に渡す word パラメータ
var adtag_word = '';
if(kv_atype == 'SP'){
	adtag_word = (cms_scid + getMetaContent({'keywords':'keywords','encode':'no'})).split(',');
}else{
	adtag_word = (cms_scid + itikw + getMetaContent({'keywords':'keywords','encode':'no'})).split(',');
}
// console.log('kv_atype = ' + kv_atype + ' : ' + adtag_word);

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

	// adserver に渡す word パラメータを encode
	param['key'] = encodeURIComponent(adtag_word);

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

	// 通常ポジション
	}else{
		adRequest.pos(param);
	}
	return true;
};

/* 通常ポジション
-------------------------------------------------------*/
adRequest.pos = function(param){
	document.write(param['script']);
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
