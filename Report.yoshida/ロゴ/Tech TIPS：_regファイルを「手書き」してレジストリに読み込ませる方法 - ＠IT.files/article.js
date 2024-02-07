function printNotITMID(elem){
	var cssname = '/css/print.css';
	var head = document.getElementsByTagName('head')[0];
	var css = head.getElementsByTagName('link');
	var target = [];
	for(var i = 0; i < css.length; i++){
		if(css[i].href.match(cssname)){
			target.push(css[i]);
			break;
		}
	}
	if(target.length == 0){
		this.wraplogo = document.createElement('div');
		this.wraplogo.style.margin = '0 0 10px';
		this.wraplogo.appendChild(printNotITMID.createLOGO());
		document.body.insertBefore(this.wraplogo,document.body.childNodes[0]);
		head.appendChild(printNotITMID.createCSS(cssname));
		elem.innerHTML = 'Web表示に切り替える';
	}else{
		document.body.removeChild(this.wraplogo);
		head.removeChild(target[0]);
		elem.innerHTML = 'プリント表示に切り替える';
	}
};
printNotITMID.createCSS = function(cssname){
	var css = document.createElement('link');
	css.setAttribute('rel','stylesheet');
	css.setAttribute('href',cssname);
	css.setAttribute('type','text/css');
	css.setAttribute('media','all');
	return css;
};
printNotITMID.createLOGO = function(){
	var logo = document.createElement('img');
	logo.setAttribute('src',imgSrv() + '/images/logo/150_' + masterChannel() + '_bgw.gif');
	return logo;
};

/* スーパーバナー画像ローテーション（ITN = DOM , 以外 = document.write） - 2012/03/14
---------------------------------------------------------------------*/
function BANNER_ROTATE(data){

	// 配列数からランダム値を取り出す
	var i = Math.floor(Math.random() * (data['data'].length - 1));

	// 変数設定
	var swf = data['data'][i]['swf'];
	var gif = data['data'][i]['gif'];
	var url = data['data'][i]['link'];
	var clicktag = data['data'][i]['clicktag'];
	var width = data['data'][i]['width'];
	var height = data['data'][i]['height'];
	var bannerid = 'colBoxMiddleSwfRotate_' + swf + '_' + gif;
	bannerid = bannerid.replace(/(\/|\.|\_)/g,'');

	// div 作成しながら jpg|gif の設定
	document.write('<div id="' + bannerid + 'wrap" style="position:relative;z-index:100;">');
	document.write('<div id="' + bannerid + '" style="z-index:200;">');
	if(gif != '') document.write('<a href="' + url + '" onClick="' + clicktag + '"><img src="' + imgSrv() + gif + '"></a>');
	document.write('</div>');
	if(swf != '') document.write('<a href="' + url + '" onClick="' + clicktag + '" style="display:block;width:' + width + 'px;height:' + height + 'px;position:absolute;top:0;left:0;z-index:1000;background:transparent;"><img src="' + imgSrv() + '/images/spacer.gif" width="' + width + '" height="' + height + '"></a>');
	document.write('</div>');

	// swf 指定があったら
	if(swf != ''){

		// ライブラリを呼び出す
		setJs('/js/lib/swfobject.js');

		// ライブラリは非同期読み込みなので念のため遅延実行させる
		setTimeout(function(){

			// ライブラリ内関数が使用可能になったら実行
			domWait('swfobject.embedSWF',function(){
				var flashvars = {};
			
				// 上にマスクするため透過設定
				var params = {wmode:'transparent'};

				var attributes = {};
				swfobject.embedSWF(swf,bannerid,width,height,'9.0.0','',flashvars,params);
			});
		},500);
	}

	// スーパーバナーだったら
	if(data['position'] == 'ITN'){
		if(!document.getElementById('globalHeaderMiddle')) return false;
		if(!document.getElementById('ITN')){
			var ITN = document.createElement('div');
			ITN.setAttribute('id','ITN');
			ITN.appendChild(document.getElementById(bannerid));
			document.getElementById('globalHeaderMiddle').appendChild(ITN);
		}else{
			document.getElementById('ITN').appendChild(document.getElementById(bannerid));
		}
	}
	return true;
};

/* 代替バナーSWF - 2011/12/20
---------------------------------------------------------------------*/
function BANNER_SWF(data){
	if(data['swf'] == '' && data['gif'] == '') return false;
	if(data['swf'] != ''){
		setJs('/js/lib/swfobject.js');
		domWait('swfobject.embedSWF',function(){
			swfobject.embedSWF(data['swf'],'colBoxMiddleSwf_' + data['swf'] + '_' + data['gif'],data['width'],data['height'],'9.0.0');
		});
	}
	if(!document.getElementById('globalHeaderMiddle')) return false;
	if(!document.getElementById('colBoxMiddleSwf_' + data['swf'] + '_' + data['gif'])) return false;
	document.getElementById('globalHeaderMiddle').appendChild(document.getElementById('colBoxMiddleSwf_' + data['swf'] + '_' + data['gif']));
	return true;
};

/* 代替ロゴオフセット - 11/12/20
---------------------------------------------------------------------*/
function BANNER_LOGO_OFFSET_X(data){
	if(!data['x']) return false;
	if(!document.getElementById('globalLogo')) return false;
	var logo = document.getElementById('globalLogo');
	var logoX = parseInt(jQuery(logo).css('left'));
	logo.style.left = logoX + data['x'] + 'px';
	return true;
};
function BANNER_LOGO_OFFSET_Y(data){
	if(!data['y']) return false;
	if(!document.getElementById('globalLogo')) return false;
	var logo = document.getElementById('globalLogo');
	var logoY = parseInt(jQuery(logo).css('top'));
	logo.style.top = logoY + data['y'] + 'px';
	return true;
};

/* 代替ロゴリンク先変更 - 11/12/20
---------------------------------------------------------------------*/
function BANNER_LOGO_LINK(data){
	if(!data['link']) return false;
	if(!document.getElementById('globalLogo').getElementsByTagName('a')[0]) return false;
	var logo = document.getElementById('globalLogo').getElementsByTagName('a')[0];
	logo.href = data['link'];
	return true;
};

// isLogin Setting 20130624
/*
引数：type
 関数格納連想配列指定
 予め連想配列として関数を定義し
 関数実行時に引数として文字列を与え実行させる
*/
/*----------------------------------------------*/
// MAIN
/*----------------------------------------------*/
function ISLOGIN(data){
	if(ISLOGIN.checkMASK() == true){
		switch(ISLOGIN.checkSERVER()){
			case 'www':
				ISLOGIN.www(data);
				break;
			default:
				ISLOGIN.pre(data);
		};
	}
};
/*----------------------------------------------*/
// MASK AREA CHECK
/*----------------------------------------------*/
ISLOGIN.checkMASK = function(){
	var m = '';
	if(document.getElementById('CmsMembersControl')){
		m = true;
	}else{
		m = false;
	}
	return m;
};
/*----------------------------------------------*/
// CHECK SERVER
/*----------------------------------------------*/
ISLOGIN.checkSERVER = function(){
	var d = document.domain;
	var s = '';
	if(d.match(/preview|broom|localhost/)){
		s = 'pre';
	}else{
		s = 'www';
	}
//	alert(s); // テスト用
	return s;
};
/*----------------------------------------------*/
// SERVER FUNCTION (PUBLIC / PREVIEW)
/*----------------------------------------------*/
// PUBLIC
ISLOGIN.www = function(data){
	// isLogin 呼び出し（1 度のみ）
	if(ISLOGIN.checkISLOGINcount == 0){
		ISLOGIN.checkISLOGINcount++;
		ISLOGIN.setISLOGIN(data['isLoginURL'],data['sc']);
	}
	ISLOGIN.checkISLOGIN(data);
	return true;
};

// PREVIEW
ISLOGIN.pre = function(data){
	ISLOGIN.fakeMASK(data);
	return true;
};
/*----------------------------------------------*/
// PUBLIC
/*----------------------------------------------*/
// isLogin 呼び出し回数
ISLOGIN.checkISLOGINcount = 0;

// isLogin セット
ISLOGIN.setISLOGIN = function(isLoginURL,sc){

	// isLogin.cgi キャッシュ対策のため path と date local をクエリに
	var now = new Date();
	var year = now.getYear(); // 年
	var month = now.getMonth() + 1; // 月
	var day = now.getDate(); // 日
	var hour = now.getHours(); // 時
	var min = now.getMinutes(); // 分
	if(year < 2000) { year += 1900; }
	
	// 数値が1桁の場合頭に0を付けて2桁で表示する指定
	if(month < 10) { month = "0" + month; }
	if(day < 10) { day = "0" + day; }
	if(hour < 10) { hour = "0" + hour; }
	if(min < 10) { min = "0" + min; }
	var q = location.pathname + year + month + day + hour + min;

	// isLogin 呼び出し（非同期）
	this.isLogin = document.createElement('script');
	this.isLogin.id = 'isLogin';
	this.isLogin.src = isLoginURL + '?date=' + q + '&sc=' + sc;
	document.getElementsByTagName('head')[0].appendChild(this.isLogin);

	return true;
};

// isLogin 存在チェック
ISLOGIN.checkISLOGIN = function(data){

	// isLogin の存在が確認できるまで WAIT
	if(typeof membersArticle === 'undefined'){
		ISLOGIN.checkISLOGINcount++;
		setTimeout(function(){
			ISLOGIN.checkISLOGIN(data);
		},100);
		return false;

	// isLogin の存在を確認
	}else{

		// 実行
		ISLOGIN.goISLOGIN(data);
		return true;
	}
};

// 実行
ISLOGIN.goISLOGIN = function(data){

//	ISLOGIN[data['type']](data);
	ISLOGIN.MASK(data);
	return true;
};

// 実行関数（記事マスク）
ISLOGIN.MASK = function(data){
	var maskid = 'CmsMembersControl';
	var mask = document.getElementById(maskid);
	var divs = mask.getElementsByTagName('div');
	var div = document.createElement('div');
	var status = true;

	// ログインしていない＆記事公開から 6 時間以上たっている（記事閲覧不可能）
	// 6 時間制限
	if(itmIdLogin <= 0 && membersPublic == 0){

		mask.className = maskid;
		data['maskid'] = maskid;
		for(var i = 0; i < divs.length; i++){
			if(divs[i].className == 'CmsMembersControlIn'){
				divs[i].style.display = 'none';
			}
		}
		div.className = 'CmsMembersControlJsOn';
		div.innerHTML = ISLOGIN.MASK_temp(data);
		mask.appendChild(div);

		status = false;

	// ログインしている（記事閲覧）
	}else{

		mask.innerHTML = membersArticle;
		status = true;
	}
	return status;
};

// マスクテンプレート
ISLOGIN.MASK_temp = function(data){
	data['idurl'] = 'https://id.itmedia.jp/isentry/contents';
	data['ac'] = '1a599d548ac1cb9a50f16ce3ba121520c8ab7e05d54e097bfa5b82cb5a328a0f';
	data['return_url'] = encodeURIComponent(document.URL);
	data['encoding'] = 'shiftjis';
	if(!data['bc']){
		data['bc'] = 1;
	}
	var code = [];
	code.push('<div class="colBox colBoxMembersControl"><div class="colBoxOuter">');
	if(data['heading']) code.push('<div class="colBoxHead"><h2>' + data['heading'] + '</h2></div>');
	code.push('<div class="colBoxInner"><div class="colBoxIndex">');
	code.push('<div class="colBoxDescription"><p>' + data['description'] + '</p></div>');
	code.push('<div class="colBoxButton"><a href="' + data['idurl'] + '?sc=' + data['sc'] + '&lc=' + data['lc'] + '&ac=' + data['ac'] + '&pnp=1&bc=' + data['bc'] + '&return_url= ' + data['return_url'] + '&encoding=' + data['encoding'] + '" onClick="var s=s_gi(\'' + thisSite() + '\');s.eVar' + data['eVer'] + '=\'' + data['eVerStr'] + '\';s.tl(this,\'o\',\'' + data['eVerStr'] + '\');s_objectid=\'' + data['eVerStr'] + '\';">続きを読む</a></div>');
	code.push('</div></div></div></div>');
	return code.join('');
};
/*----------------------------------------------*/
// PREVIEW
/*----------------------------------------------*/
ISLOGIN.fakeMASK = function(data){
	if(location.hash.match('maskoff')) return false;
	var maskid = 'CmsMembersControl';
	var mask = document.getElementById(maskid);
	var divs = mask.getElementsByTagName('div');
	var div = document.createElement('div');
	mask.className = maskid;
	data['maskid'] = maskid;
	for(var i = 0; i < divs.length; i++){
		if(divs[i].className == 'CmsMembersControlIn'){
			divs[i].style.display = 'none';
		}
	}
	div.className = 'CmsMembersControlJsOn';
	div.innerHTML = ISLOGIN.fakeMASK_temp(data);
	mask.appendChild(div);
	return true;
};
ISLOGIN.fakeMASK_temp = function(data){
	var msg = 'マスクを解除しますか？';
	var code = [];
	code.push('<div class="colBox colBoxMembersControl"><div class="colBoxOuter">');
	if(data['heading']){
		code.push('<div class="colBoxHead"><h2>' + data['heading'] + '</h2></div>');
	}
	code.push('<div class="colBoxInner"><div class="colBoxIndex">');
	code.push('<div class="colBoxDescription"><p>' + data['description'] + '</p></div>');
	code.push('<div class="colBoxButton"><a href="javascript:void(0);" title="続きを読む（TestMode）" onClick="if(window.confirm(\'' + msg + '\')){location.href = \'#maskoff\';location.reload();}else{return false;}">続きを読む（TestMode）</a></div>');
	code.push('</div></div></div></div>');
	return code.join('');
};
/*----------------------------------------------*/
// 未使用
/*----------------------------------------------*/
// 実行関数（記事マスク）
ISLOGIN.CmsMembersControl = function(param){

	// 書式のないものは処理しない
	var elemname = 'CmsMembersControl';
	var elem = document.getElementById(elemname);

	// テストモード
	var testmode = (function(){

		// broom は無条件にテストモード
		if(document.domain.match('broom')) return true;

		// preview かつ #testmode の場合テストモード
		if(document.domain.match('preview') && location.hash.match('#testmode')) return true;

		// 確認用
//		if(location.hash.match('#testmode')) return true;

		return false;
	})();

	// テストモードである
	if(testmode == true){

		// 処理を続行

	// ログインしていない＆記事公開から 6 時間以上たっている場合記事は見せない
	//	MM 6 時間制限
	}else if(itmIdLogin <= 0 && membersPublic == 0){

		// 処理を続行

	// ログインしている（ここで処理終了）
	}else{

		// テスト環境だったらなにもしない
		if(document.domain.match(/preview|broom|localhost/)){

		// 本番だったら membersArticle を挿入
		}else{
			elem.innerHTML = membersArticle;
		}
		return false;
	}

	elem.className = elemname;
	param['elemname'] = elemname;

	param['testmode'] = testmode;

	// マスク内本文を囲む div を消す
	var a = elem.getElementsByTagName('div');
	for(var i = 0; i < a.length; i++){
		if(a[i].className == 'CmsMembersControlIn'){
			a[i].style.display = 'none';
		}
	}

	// フレーム作成
	var div = document.createElement('div');
	div.className = 'CmsMembersControlJsOn';
	div.innerHTML = ISLOGIN.createFrame(param);

	// フレーム挿入
	elem.appendChild(div);
	return true;

};
ISLOGIN.createFrame = function(param){

	param['idurl'] = 'https://id.itmedia.jp/isentry/contents';
	param['ac'] = '1a599d548ac1cb9a50f16ce3ba121520c8ab7e05d54e097bfa5b82cb5a328a0f';
	param['return_url'] = encodeURIComponent(document.URL);
	param['encoding'] = 'shiftjis';
	if(!param['bc']){
		param['bc'] = 1;
	}

	var code = [];
	code.push('<div class="colBox colBoxMembersControl"><div class="colBoxOuter">');
	if(param['heading']) code.push('<div class="colBoxHead"><h2>' + param['heading'] + '</h2></div>');
	code.push('<div class="colBoxInner"><div class="colBoxIndex">');
	code.push('<div class="colBoxDescription"><p>' + param['description'] + '</p></div>');
	if(param['testmode'] == true){
		code.push('<div class="colBoxButton"><a href="javascript:void(0);" title="続きを読む（TestMode）" onClick="CmsMembersControl.maskOff(\'' + param['elemname'] + '\')">続きを読む（TestMode）</a></div>');
	}else{
		code.push('<div class="colBoxButton"><a href="' + param['idurl'] + '?sc=' + param['sc'] + '&lc=' + param['lc'] + '&ac=' + param['ac'] + '&pnp=1&bc=' + param['bc'] + '&return_url= ' + param['return_url'] + '&encoding=' + param['encoding'] + '" onClick="var s=s_gi(\'' + thisSite() + '\');s.eVar' + param['eVer'] + '=\'' + param['eVerStr'] + '\';s.tl(this,\'o\',\'' + param['eVerStr'] + '\');s_objectid=\'' + param['eVerStr'] + '\';">続きを読む</a></div>');
	}
	code.push('</div></div></div></div>');

	return code.join('');
};
/*----------------------------------------------*/
// 未使用
/*----------------------------------------------*/

/* メンバー用 - 12/05/24 - 12/08/23
---------------------------------------------------------------------*/
// ▼テストモード
// 会員ステータスに関係なくマスクする
// ボタンクリックでマスク内表示
function CmsMembersControl(param){

	// 書式のないものは処理しない
	var elemname = 'CmsMembersControl';
	if(!document.getElementById(elemname)) return false;
	var elem = document.getElementById(elemname);

	// テストモード
	var testmode = (function(){

		// broom は無条件にテストモード
		if(document.domain.match('broom')) return true;

		// preview かつ #testmode の場合テストモード
		if(document.domain.match('preview') && location.hash.match('#testmode')) return true;

		// 確認用
//		if(location.hash.match('#testmode')) return true;

		return false;
	})();

	// テストモードである
	if(testmode == true){

		// 処理を続行

	// ログインしていない＆記事公開から 6 時間以上たっている場合記事は見せない
//	MM 6 時間制限
	}else if(param['flagname'] == 0 && membersPublic == 0){
//	}else if(param['flagname'] == 0){

		// 処理を続行

	// ログインしている（ここで処理終了）
	}else{

		// テスト環境だったらなにもしない
		if(document.domain.match(/preview|broom|localhost/)){

		// 本番だったら membersArticle を挿入
		}else{
			elem.innerHTML = membersArticle;
		}
		return false;
	}

	elem.className = elemname;
	param['elemname'] = elemname;

	param['testmode'] = testmode;

	// マスク内本文を囲む div を消す
	var a = elem.getElementsByTagName('div');
	for(var i = 0; i < a.length; i++){
		if(a[i].className == 'CmsMembersControlIn'){
			a[i].style.display = 'none';
		}
	}

	// フレーム作成
	var div = document.createElement('div');
	div.className = 'CmsMembersControlJsOn';
	div.innerHTML = CmsMembersControl.createFrame(param);

	// フレーム挿入
	elem.appendChild(div);
	return true;
};

// 会員登録メッセージ
CmsMembersControl.createFrame = function(param){
	param['idurl'] = 'https://id.itmedia.jp/isentry/contents';
	param['ac'] = '1a599d548ac1cb9a50f16ce3ba121520c8ab7e05d54e097bfa5b82cb5a328a0f';
	param['return_url'] = encodeURIComponent(document.URL);
	param['encoding'] = 'shiftjis';

	var code = [];
	code.push('<div class="colBox colBoxMembersControl"><div class="colBoxOuter">');
	code.push('<div class="colBoxHead"><h2>' + param['heading'] + '</h2></div>');
	code.push('<div class="colBoxInner"><div class="colBoxIndex">');
	code.push('<div class="colBoxDescription"><p>' + param['description'] + '</p></div>');
	if(param['testmode'] == true){
		code.push('<div class="colBoxButton"><a href="javascript:void(0);" title="続きを読む（TestMode）" onClick="CmsMembersControl.maskOff(\'' + param['elemname'] + '\')">続きを読む（TestMode）</a></div>');
	}else{
		code.push('<div class="colBoxButton"><a href="' + param['idurl'] + '?sc=' + param['sc'] + '&lc=' + param['lc'] + '&ac=' + param['ac'] + '&pnp=1&bc=1&return_url= ' + param['return_url'] + '&encoding=' + param['encoding'] + '" onClick="var s=s_gi(\'' + thisSite() + '\');s.eVar' + param['eVer'] + '=\'' + param['eVerStr'] + '\';s.tl(this,\'o\',\'' + param['eVerStr'] + '\');s_objectid=\'' + param['eVerStr'] + '\';">続きを読む</a></div>');
	}
	code.push('</div></div></div></div>');

	return code.join('');
};

// テストモード
CmsMembersControl.maskOff = function(elemname){

	// id:cmsMembersControl
	var elem = document.getElementById(elemname);

	// class 削除
	elem.className = '';

	var div = elem.getElementsByTagName('div');
	var box = [];
	for(var i = 0; i < div.length; i++){

		// マスク内本文を表示
		if(div[i].className == 'CmsMembersControlIn'){
			div[i].style.display = 'block';
		}

		// 会員登録メッセージを非表示
		if(div[i].className == 'CmsMembersControlJsOn'){
			div[i].style.display = 'none';
		}
	}
	return true;
};

/* 関連記事最後のページ以外アブストラクト削除 - 2013/04/10
--------------------------------------------------------*/
function endlinkAll(){

	// 記事ではない
	// ページ送りがない場合（単一ページ）はなにもしない
	// 最終ページはなにもしない
	// 関連記事＆関連リンクがない
	// 関連記事が 1 件もない
	if(masterType() != 'article') return false;
	if(!document.getElementById('prev') || !document.getElementById('numb') || !document.getElementById('next')) return false;
	if(document.getElementById('end')) return false;
	if(!document.getElementById('endlinkConnection')) return false;
	if(!document.getElementById('endlink-art1')) return false;

	// 各関連記事取得
	var endlinks = document.getElementById('endlink-art1').parentNode.getElementsByTagName('li');

	// 関連記事＆関連リンクに class 名つける
	var endlink = document.getElementById('endlinkConnection');
	endlink.className += ' endlink2column';

	// アブストラクト削除
	for(var i = 0; i < endlinks.length; i++){
		endlinks[i].innerHTML = endlinks[i].innerHTML.replace(/<(br|BR)>(\n)?.+/,'');
	}
	return false;
};

/* 書式クリックアクション - 2014/02/26
--------------------------------------------------------*/
function setClick(param){
	var e = param['elem'];
	e.style.opacity = 1;
	return;
};
