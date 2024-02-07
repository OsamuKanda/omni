/* 目次
-----------------------------------------------------------
URL 取得
ドメインリスト
SiteCatslyst (preview or www)
SiteCatslyst (designCnt 引数 記事 or それ以外)
DESIGN CLICK COUNT
IMAGE サーバ
外部 JS ロード
外部 CSS ロード
ディレクトリ階層を body class にセット
UL LI の最後の要素に CLASS をつける
OL LI の最後の要素に CLASS をつける
実行 JS 取得
JS TITLE 属性取得
META 取得
WAIT 関数（遅延ロード対応） - 2011/04/18
アイコンシャッフル関数（HTML 版／HTML ツリーが重要|Web ガバナンス） - 2011/07/26
データベース関数（JSON 版|Web ガバナンス） - 2011/07/26
SHUFFLE 関数（e は配列） - 2011/04/19
配列のキーを指定し重複があるものを排除し返す - 2011/06/10
EVENT 関数（e {'this':自分自身,'eventtype':イベントの種類（click , mouseover 等）,'function':関数名}） - 2011/04/19
COOKIE 読み込み - 2011/04/21
COOKIE 書き込み（param {'name':COOKIE名,'value':値,'domain':HOST,'path':PATH,'expires':有効期限,'secure':0 or 1（0 = 無効、1 = 有効）}） - 2011/04/21
ブラウザ幅＆高さ取得 - 2011/08/22
ブラウザスクロール量取得 - 2011/08/22
ノード位置取得 - 2011/08/22
ノード幅＆高さ取得 - 2011/08/22
ソーシャルボタンプルダウン,プルダウン遅延処理追加（setTimeout） - 作業中
ナビゲーションプルダウン（jQuery） - 2011/06/03,プルダウン遅延処理追加（setTimeout） - 作業中
ITMID アラートボタン - 11/06/14 現在未使用
WINDOW 幅＆高さ取得 - 11/08/11
スマートフォン表示 - 11/08/04
RETWEET
bitly API
TWITTER WIDGET
文字バイト数カウント＆指定文字バイト数でカット - 111026
ソーシャルパーツ RD:5609 - 111104
senna 連載 json RD:6327 - 120117
タブ切り替え - 120125
PC or SP VIEW に移動し状態を保存 - 2012/02/14
-----------------------------------------------------------
*/

/* 現在年月日時分秒取得
---------------------------------------------------------------------*/
function nowTime(){
	var nowTime = new Date();
	nowTime.nowMS = nowTime.getTime();
	nowTime.nowFullYear = nowTime.getFullYear();
	nowTime.nowMonth = nowTime.getMonth() + 1;
	nowTime.nowDate = nowTime.getDate();
	nowTime.nowSeconds = nowTime.getSeconds();
	nowTime.nowHours = nowTime.getHours();
	nowTime.nowMinutes = nowTime.getMinutes();
	nowTime.nowSeconds = nowTime.getSeconds();
	return nowTime;
};

/* URL 取得
---------------------------------------------------------------------*/
function getUrl(){
	var url = {
		'url':document.URL,
		'url_cutparam':document.URL.replace(/\.html.+/,'.html'),
		'domain':document.domain,
		'path':location.pathname,
		'path_cutparam':location.pathname.replace(/\.html.+/,'.html'),
		'port':location.port,
		'path_cutpageing_cutparam':location.pathname.replace(/\_\d*\.html(.+)?/,'.html')
	};
	return url;
};

/* ドメインリスト
---------------------------------------------------------------------*/
function thisDomain(){
	var a = getUrl()['path'];
	var b = getUrl()['url'];
	if(b.match('bizmakoto.jp') || a.match(/^\/(makoto|bizid|style|bizmobile)\//)){
		var c = 'http://bizmakoto.jp';
	}else if(b.match('camera.itmedia.co.jp') || a.match(/^\/dc\//)){
		var c = 'http://camera.itmedia.co.jp';
	}else if(b.match('gamez.itmedia.co.jp') || a.match(/^\/games\//)){
		var c = 'http://gamez.itmedia.co.jp';
	}else if(b.match('mag.executive.itmedia.co.jp') || a.match(/^\/executive\//)){
		var c = 'http://mag.executive.itmedia.co.jp';
	}else if(b.match('ebook.itmedia.co.jp') || a.match(/^\/ebook\//)){
		var c = 'http://ebook.itmedia.co.jp';
	}else if(b.match('monoist.atmarkit.co.jp') || a.match(/^\/mn\//)){
		var c = 'http://monoist.atmarkit.co.jp';
	}else if(b.match('eetimes.jp') || a.match(/^\/ee\//)){
		var c = 'http://eetimes.jp';
	}else if(b.match('ednjapan.com') || a.match(/^\/edn\//)){
		var c = 'http://ednjapan.com';
	}else if(b.match('nlab.itmedia.co.jp') || a.match(/^\/nl\//)){
		var c = 'http://nlab.itmedia.co.jp';
	}else if(b.match('gadget.itmedia.co.jp') || a.match(/^\/gg\//)){
		var c = 'http://gadget.itmedia.co.jp';
	}else if(b.match('marketing.itmedia.co.jp') || a.match(/^\/mm\//)){
		var c = 'http://marketing.itmedia.co.jp';
	}else if(b.match('www.atmarkit.co.jp') || a.match(/^\/ait\//) || a.match(/\/AIT\/CMS/)){
		var c = 'http://www.atmarkit.co.jp';
	}else if(b.match(/\/review\.itmedia\.co\.jp/) || a.match(/^\/review\//) || a.match(/\/Review\/CMS/)){
		var c = 'http://review.itmedia.co.jp';
	}else if(b.match(/\/healthcare\.itmedia\.co\.jp/) || a.match(/^\/hc\//) || a.match(/\/Healthcare\/CMS/)){
		var c = 'http://healthcare.itmedia.co.jp';
	}else{
		var c = 'http://www.itmedia.co.jp';
	}
	return c;
};

/* SiteCatslyst (preview or www)
---------------------------------------------------------------------*/
// Move /js/scAccount.js

/* SiteCatslyst (designCnt 引数 記事 or それ以外)
---------------------------------------------------------------------*/
function pageDir(){
	if(getUrl()['path'].match(/\/.+\/articles\/\d\d\d\d\/\d\d\/news/) || getUrl()['path'].match(/\/tt\/news\/\d\d\d\d\/\d\d\/news/)){
		var a = 'articles';
	}else{
		var a = 'indexes';
	}
	return a;
};

/* DESIGN CLICK COUNT
-------------------------------------------------------------*/
function designCnt(pos,opt){

	// 除外リストに含まれる場合カウントしない
	if(designCnt.exc_list[pos] == true) return false;

	if(!opt) var opt = '';
	var channel = (function(){
		if(typeof masterChannel == 'function') return masterChannel();
		return location.pathname.split('/')[0];
	})();
	var type = (function(){
		if(typeof masterType == 'function') return masterType();
		return 'notMasterType';
	})();
	var a = channel + '_' + type + '_' + pos + ' ' + opt;
	s.prop14 = a;
	// s.tl(this,'e',a);
	s_objectID = a;
};

/* DESIGN CLICK COUNT 除外リスト
-------------------------------------------------------------*/
designCnt.exc_list = {
'lart':true, /* 関連記事 */
'llnk':true  /* 関連リンク */
};

/* IMAGE サーバ
---------------------------------------------------------------------*/
function imgSrv(){
	var url = getUrl();
	if(!url['domain'].match(/(preview|broom|localhost)/)){
		return 'http://ima' + 'ge.itmedia.co.jp';
	}else{
		return '';
	}
};

/* 外部 JS ロード
---------------------------------------------------------------------*/
function setJs(url){
	if(setJs.checkJs(url) == false){
		setJs.createJs(url);
		setJs.addJs();
		return true;
	}
	return false;
}
setJs.checkJs = function(url){
	this.head = document.getElementsByTagName('head')[0];
	this.script = this.head.getElementsByTagName('script');
	for(var i = 0; i < this.script.length; i++){
		if(this.script[i].getAttribute('src') == url){
			return true;
			break;
		}
	}
	return false;
}
setJs.createJs = function(url){
	this.head = document.getElementsByTagName('head')[0];
	this.js = document.createElement('script');
	this.js.setAttribute('type','text/javascript');
	this.js.setAttribute('src',url);
}
setJs.addJs = function(){
	this.head.appendChild(this.js);
};

/* 外部 CSS ロード
---------------------------------------------------------------------*/
function setCss(url){
	if(setCss.checkCss(url) == false){
		setCss.createCss(url);
		setCss.addCss();
		return true;
	}
	return false;
}
setCss.checkCss = function(url){
	this.head = document.getElementsByTagName('head')[0];
	this.script = this.head.getElementsByTagName('script');
	for(var i = 0; i < this.script.length; i++){
		if(this.script[i].getAttribute('src') == url){
			return true;
			break;
		}
	}
	return false;
}
setCss.createCss = function(url){
	this.head = document.getElementsByTagName('head')[0];
	this.css = document.createElement('script');
	this.css.setAttribute('type','text/javascript');
	this.css.setAttribute('src',url);
}
setJs.addCss = function(){
	this.head.appendChild(this.css);
};

/* ディレクトリ階層を body class にセット
-------------------------------------------------------------*/
function attachBodyClass(){
	var a = location.pathname.split('/');
	for(var i = 1; i < a.length; i++){
		if(a[i].match('.html') || a[i] == '') continue;
		a[i] = 'dir-' + a[i];
	}
	document.getElementsByTagName('body')[0].className = a.join(' ');
};

/* UL LI の最後の要素に CLASS をつける
-------------------------------------------------------------*/
(function(){
	if(!document.getElementById('masterContents')) return false;
	var a = document.getElementById('masterContents');
	var b = a.getElementsByTagName('ul');
	for(var i = 0; i < b.length; i++){
		var c = b[i].getElementsByTagName('li');
		var d = c.length - 1;
		if(!c[d].className){
			c[d].className = 'lastLiMarginClear';
		}else{
			c[d].className += ' lastLiMarginClear';
		}
	}
})();

/* OL LI の最後の要素に CLASS をつける
-------------------------------------------------------------*/
(function(){
	if(!document.getElementById('masterContents')) return false;
	var a = document.getElementById('masterContents');
	var b = a.getElementsByTagName('ol');
	for(var i = 0; i < b.length; i++){
		var c = b[i].getElementsByTagName('li');
		var d = c.length - 1;
		if(!c[d].className){
			c[d].className = 'lastLiMarginClear';
		}else{
			c[d].className += ' lastLiMarginClear';
		}
	}
})();

/* 実行 JS 取得
-----------------------------------------------------------------------*/
function getScriptThis(e){
	if(e.nodeName.toLowerCase() == 'script'){
		return e;
	}else{
		return arguments.callee(e.lastChild);
	}
};

/* JS TITLE 属性取得
-----------------------------------------------------------------------*/
function getScriptTitleTag(e){
	if(e.nodeName.toLowerCase() == 'script'){
		return e.getAttribute('title');
	}else{
		return arguments.callee(e.lastChild);
	}
};

/* META 取得
-----------------------------------------------------------------------*/
function getMETA(metaname){
	if(!metaname || metaname == '') return false;
	var head = document.getElementsByTagName('head')[0];
	var meta = head.getElementsByTagName('meta');
	var target = [];
	for(var i = 0; i < meta.length; i++){
		if(meta[i].name == metaname){
			target.push(meta[i]);
			break;
		}
	}
	if(target.length == 0) return false;
	return target[0];
};

/* WAIT 関数（遅延ロード対応） - 2011/04/18
-----------------------------------------------------------------------*/
function domWait(a,func){
	var check = 0;
	try{
		eval('check = ' + a);
	}catch(e){
	}
	if(check){
		func()
	}else{
		var f = function(){
			domWait(a,func)
		};
		setTimeout(f,100);
	}
};

/* アイコンシャッフル関数（HTML 版／HTML ツリーが重要|Web ガバナンス） - 2011/07/26
-----------------------------------------------------------------------*/
function colBoxIconShuffle(data){
	if(!data['id'] || data['id'] == '') return false;
	var colBox= document.getElementById(data['id']);
	var colBoxOuter = colBox.getElementsByTagName('div')[0];
	var colBoxHead = colBoxOuter.getElementsByTagName('div')[0];
	var colBoxInner = colBoxOuter.getElementsByTagName('div')[1];
	var colBoxIndex = (function(a){
		var b = a.getElementsByTagName('div');
		var c = [];
		for(var i = 0; i < b.length; i++){
			if(b[i].className == 'colBoxIndex'){
				c.push(b[i]);
			}
		}
		return c;
	})(colBoxInner);
	var colBoxIndexShuffle = elemShuffle(colBoxIndex);
	var colBoxIconN = 0;
	if(!data['loop'] || data['loop'] == ''){
		data['loop'] = colBoxIndexShuffle.length;
	}else{
		data['loop']--;
	}
	for(var i = 0; i < colBoxIndexShuffle.length; i++){
		colBoxInner.appendChild(colBoxIndexShuffle[i]);
		if(!colBoxIndexShuffle[i].getElementsByTagName('img')[0]){
			colBoxIndexShuffle[i].style.display = 'none';
		}else{
			if(data['loop'] < colBoxIconN){
				colBoxIndexShuffle[i].style.display = 'none';
				continue;
			}
			if(colBoxIconN == 0){
				colBoxIndexShuffle[i].style.display = 'block';
				colBoxIndexShuffle[i].getElementsByTagName('a')[0].className = 'active';
				colBoxHead.getElementsByTagName('h2')[0].innerHTML = colBoxIndexShuffle[i].getElementsByTagName('a')[0].getAttribute('title');
			}
			colBoxIconN++;
			attachingEvent({'data':data,'this':colBoxIndexShuffle[i],'eventtype':'mouseover','function':'database.iconShuffleEventMouseover'});
			if(data['designCnt']) attachingEvent({'data':data,'this':colBoxIndexShuffle[i].getElementsByTagName('a')[0],'eventtype':'click','function':'database.iconShuffleEventClick'});
		}
	}
	return true;
};

/* データベース関数（JSON 版|Web ガバナンス） - 2011/07/26
-----------------------------------------------------------------------*/
function database(json){
// script src パラメータ取得
	var jsparam = getScriptThis(document).getAttribute('src').split('?');
// パラメータ分割
	var param = jsparam[1].split('&');
	var data = {};
	for(var i = 0; i < param.length; i++){
		var a = param[i].split('=');
// colBox id
		if(a[0] == 'id'){
			data['id'] = a[1];
// colBox class
		}else if(a[0] == 'class'){
			data['class'] = a[1];
// loop 数
		}else if(a[0] == 'loop'){
			data['loop'] = a[1];
// 使用テンプレート
		}else if(a[0] == 'template'){
			data['template'] = a[1];
// 見出し名
		}else if(a[0] == 'title'){
			data['title'] = a[1];
// デザインカウント名
		}else if(a[0] == 'designCnt'){
			data['designCnt'] = a[1];
		}
	}
// id 指定がなければ空
	if(!data['id']) data['id'] = '';
// class 指定がなければ空
	if(!data['class']) data['class'] = '';
// loop 指定がなければ配列最大数
	if(!data['loop']) data['loop'] = json['data'].length;
// template 指定がなければ default テンプレート使用
	if(!data['template']) data['template'] = 'defaultTemplate';
// title 指定がなければ json name 使用
	if(!data['title']) data['title'] = json['name'];
	eval('database.' + data['template'] + '(json,data)');
	return true;
};
database.header = function(data){
	var a = '<div class="colBox ' + data['class'] +'" id="' + data['id'] +'">';
	a += '<div class="colBoxOuter">';
	a += '<div class="colBoxHead"><h2>' + data['title'] + '</h2></div>';
	a += '<div class="colBoxInner">';
	return a;
}
database.footer = function(){
	var a = '</div></div></div>';
	return a;
}

/* デフォルトテンプレート - 2011/07/28
-------------------------------------------------------*/
database.defaultTemplate = function(json,data){
	var header = database.header(data);
	var contents = [];
	var footer = database.footer();
	var json = json['data'];
	var loop = data['loop'];
	for(var i = 0; i < loop; i++){
		var a = '';
		a += '<div class="colBoxIndex">';
		a += '<div class="colBoxIcon"><a href="' + json[i]['link'] + '" title="' + json[i]['title'] + '">' + json[i]['icon'] + '</a></div>';
		a += '<div class="colBoxSubTitle"><h5>' + json[i]['subtitle'] + '</h5></div>';
		a += '<div class="colBoxTitle"><h3><a href="' + json[i]['link'] + '" title="' + json[i]['title'] + '">' + json[i]['title'] + '</a></h3></div>';
		a += '<div class="colBoxDescription"><p>' + json[i]['description'] + '</p></div>';
		a += '<div class="colBoxClear h10px"></div>';
		a += '</div>';
		contents.push(a);
	}
	document.write(header + contents.join('') + footer);
}

/* アイコンシャッフルテンプレート - 2011/07/28
-------------------------------------------------------*/
database.iconShuffle = function(json,data){
	var header = database.header(data);
	var contents = [];
	var footer = database.footer();
	var iconShuffleN = 0;
	var json = elemShuffle(json['data']);
	var loop = data['loop'];
	for(var i = 0; i < loop; i++){
		if(json['title'] == '') continue;
		if(json[i]['icon'] == ''){
			i--;
			continue;
		}
		var a = '';
		a += '<div class="colBoxIndex">';
		a += '<div class="colBoxIcon"><a href="' + json[i]['link'] + '" title="' + json[i]['title'] + '">' + json[i]['icon'] + '</a></div>';
		a += '</div>';
		contents.push(a);
	}
	document.write(header + contents.join('') + footer);
	var colBox= document.getElementById(data['id']);
	var colBoxOuter = colBox.getElementsByTagName('div')[0];
	var colBoxHead = colBoxOuter.getElementsByTagName('div')[0];
	var colBoxInner = colBoxOuter.getElementsByTagName('div')[1];
	var colBoxIndex = (function(a){
	var b = a.getElementsByTagName('div');
	var c = [];
	for(var i = 0; i < b.length; i++){
		if(b[i].className == 'colBoxIndex'){
			c.push(b[i]);
		}
	}
	return c;
})(colBoxInner);
for(var i = 0; i < colBoxIndex.length; i++){
	if(i == 0){
		colBoxIndex[i].getElementsByTagName('a')[0].className = 'active';
		colBoxHead.getElementsByTagName('h2')[0].innerHTML = colBoxIndex[i].getElementsByTagName('a')[0].getAttribute('title');
	}
	attachingEvent({'data':data,'this':colBoxIndex[i],'eventtype':'mouseover','function':'database.iconShuffleEventMouseover'});
	if(data['designCnt']) attachingEvent({'data':data,'this':colBoxIndex[i].getElementsByTagName('a')[0],'eventtype':'click','function':'database.iconShuffleEventClick'});
}
};
database.iconShuffleEventMouseover = function(elem,data){
	var targetChild = elem.parentNode.getElementsByTagName('a');
	for(var i = 0; i < targetChild.length; i++){
		targetChild[i].className = '';
	}
	var targetIcon = elem.getElementsByTagName('a')[0];
	targetIcon.className = 'active';
	var targetHead = (function(node){
		var a = node.getElementsByTagName('div');
		for(var i = 0; i < a.length; i++){
			if(a[i].className == 'colBoxHead'){
				return a[i].getElementsByTagName('h2')[0];
			}
		}
	})(elem.parentNode.parentNode);
	targetHead.innerHTML = targetIcon.getAttribute('title');
	return true;
};
database.iconShuffleEventClick = function(elem,data){
	designCnt(data['designCnt'],elem.href);
	return true;
};

/* イメージローテーションテンプレート - 2011/07/28
-------------------------------------------------------*/
database.imageRotation = function(json,data){
	var header = database.header(data);
	var contents = [];
	var footer = database.footer();
	var json = json['data'];
	var loop = data['loop'];
	var a = '';
	a += '<div class="colBoxIndex colBoxIndexLphoto">';
	a += '<div class="colBoxIcon"><a href="' + json[0]['link'] + '" title="' + json[0]['title'] + '"><img src="' + imgSrv() + json[0]['lphoto'] + '" alt="' + json[0]['title'] + '"></a></div>';
	a += '</div>';
	a += '<div class="colBoxIndex colBoxIndexSphoto">';
	for(var i = 0; i < loop; i++){
		a += '<div class="colBoxIcon"><a href="' + json[i]['link'] + '" title="' + json[i]['title'] + '">';
		a += '<img src="' + imgSrv() + json[i]['sphoto'] + '" alt="' + json[i]['title'] + '">';
		a += '<img src="' + imgSrv() + json[i]['lphoto'] + '" alt="' + json[i]['title'] + '" style="display:none;">';
		a += '</a></div>';
	}
	a += '</div>';
	contents.push(a);
	document.write(header + contents.join('') + footer);

	var colBox = document.getElementById(data['id']);
	var colBoxOuter = colBox.getElementsByTagName('div')[0];
	var colBoxHead = colBoxOuter.getElementsByTagName('div')[0];
	var colBoxInner = colBoxOuter.getElementsByTagName('div')[1];
	var colBoxIndex = (function(a){
		var b = a.getElementsByTagName('div');
		var c = [];
		for(var i = 0; i < b.length; i++){
			if(b[i].className.match('colBoxIndex')){
				c.push(b[i]);
			}
		}
		return c;
	})(colBoxInner);
	if(data['designCnt']) attachingEvent({'data':data,'this':colBoxIndex[0].getElementsByTagName('a')[0],'eventtype':'click','function':'database.imageRotationEventClick'});
	var colBoxIcon = colBoxIndex[1].getElementsByTagName('div');
	for(var i = 0; i < colBoxIcon.length; i++){
		if(i == 0){
			colBoxIcon[i].getElementsByTagName('a')[0].className = 'active';
		}
		attachingEvent({'data':data,'this':colBoxIcon[i],'eventtype':'mouseover','function':'database.imageRotationEventMouseover'});
		if(data['designCnt']) attachingEvent({'data':data,'this':colBoxIcon[i].getElementsByTagName('a')[0],'eventtype':'click','function':'database.imageRotationEventClick'});
	}
};
database.imageRotationEventMouseover = function(elem,data){
	var lphoto = elem.parentNode.parentNode.getElementsByTagName('div')[0];
	var sphoto = elem.parentNode.getElementsByTagName('div');
	for(var i =  0; i < sphoto.length; i++){
		sphoto[i].getElementsByTagName('a')[0].className = '';
	}
	elem.getElementsByTagName('a')[0].className = 'active';
	var a = elem.getElementsByTagName('a')[0];
	var str = {'title':a.getAttribute('title'),'link':a.getAttribute('href'),'lphoto':a.getElementsByTagName('img')[1].getAttribute('src')};
	lphoto.getElementsByTagName('a')[0].setAttribute('href',str['link']);
	lphoto.getElementsByTagName('a')[0].setAttribute('title',str['title']);
	lphoto.getElementsByTagName('a')[0].getElementsByTagName('img')[0].setAttribute('src',str['lphoto']);
	lphoto.getElementsByTagName('a')[0].getElementsByTagName('img')[0].setAttribute('alt',str['title']);
};
database.imageRotationEventClick = function(elem,data){
	designCnt(data['designCnt'],elem.href);
	return true;
};
/*-------------------------------------------------------*/


/* SHUFFLE 関数（e は配列） - 2011/04/19
-----------------------------------------------------------------------*/
function elemShuffle(e){
	var i = e.length;
	while(--i){
		var j = Math.floor(Math.random() * (i + 1));
		if(i == j) continue;
		var k = e[i];
		e[i] = e[j];
		e[j] = k;
	}
	return e;
};

/* 配列のキーを指定し重複があるものを排除し返す - 2011/06/10
-----------------------------------------------------------------------*/
function uniqueArray(hash){
	var storage = {};
	var uniqueArray = [];
	var i,value;
	for(i = 0; i < hash.length; i++){
		value = hash[i];
		if(!(value in storage)){
			storage[value] = true;
			uniqueArray.push(value);
		}
	}
	return uniqueArray;
}

/* EVENT 関数（e {'this':自分自身,'eventtype':イベントの種類（click , mouseover 等）,'function':関数名}） - 2011/04/19
-----------------------------------------------------------------------*/
function attachingEvent(e){
	if(!e['eventtype'].match(/(click|mouseover|mouseout)/)) return false;
	e['this'].setAttribute(e['eventtype'] + 'Flag','true');
	if(e['this'].addEventListener){
		e['this'].addEventListener(e['eventtype'], function(){
			eval(e['function'] + '(e[\'this\'],e[\'data\']);');
		}, false);
	}else if(e['this'].attachEvent){
		e['this'].attachEvent('on' + e['eventtype'], function(){
			eval(e['function'] + '(e[\'this\'],e[\'data\']);');
		});
	}
	return true;
};

/* COOKIE 読み込み - 2011/04/21
-----------------------------------------------------------------------*/
function getCookie(name){
	if(!name) return '';
	var cookies = document.cookie.split('; ');
	for(var i = 0; i < cookies.length; i++){
		var str = cookies[i].split('=');
		if (str[0] != name) continue;
		return unescape(str[1]);
	}
	return '';
};

/* COOKIE 書き込み（param {'name':COOKIE名,'value':値,'domain':HOST,'path':PATH,'expires':有効期限,'secure':0 or 1（0 = 無効、1 = 有効）}） - 2011/04/21
-----------------------------------------------------------------------*/
function setCookie(param){
	if(!param['name']) return false;
	var str = param['name'] + '=' + escape(param['value']);
	if(param['domain']){
		if(param['domain'] == 1) param['domain'] = location.hostname.replace(/^[^\.]*/, '');
		str += '; domain=' + param['domain'];
	}
	if(param['path']){
		if(param['path'] == 1) param['path'] = location.pathname;
		str += '; path=' + param['path'];
	}
	if(param['expires']){
		var nowtime = new Date().getTime();
		param['expires'] = new Date(nowtime + (60 * 60 * 24 * 1000 * param['expires']));
		param['expires'] = param['expires'].toGMTString();
		str += '; expires=' + param['expires'];
	}
	if(param['secure'] && location.protocol == 'https:'){
		str += '; secure';
	}
	document.cookie = str;
	return true;
};

/* ブラウザ幅＆高さ取得 - 2011/08/22
-----------------------------------------------------------------------*/
function getBrowserWH(){
	var win = window;
	var doc = document;
	if(win.innerWidth || win.innerHeight){
		return {'w':win.innerWidth,'h':win.innerHeight};
	}else if((doc.documentElement && doc.documentElement.clientWidth != 0) || (document.documentElement && document.documentElement.clientHeight != 0)){
		return {'w':doc.documentElement.clientWidth,'h':doc.documentElement.clientHeight};
	}else if(document.body){
		return {'w':doc.body.clientWidth,'h':doc.body.clientHeight};
	}
	return {'w':0,'h':0};
}

/* ブラウザスクロール量取得 - 2011/08/22
-----------------------------------------------------------------------*/
function getBrowserScrollXY(){
	return {'x':document.documentElement.scrollLeft || document.body.scrollLeft,'y':document.documentElement.scrollTop || document.body.scrollTop};
}

/* ノード位置取得 - 2011/08/22
-----------------------------------------------------------------------*/
function getElementXY(e){
	var x = 0;
	var y = 0;
	while(e){
		x += e.offsetLeft;
		y += e.offsetTop;
		e = e.offsetParent;
	}
	return {'x':x,'y':y};
};

/* ノード幅＆高さ取得 - 2011/08/22
-----------------------------------------------------------------------*/
function getElementWH(e){
	return {'w':e.offsetWidth,'h':e.offsetHeight};
};

/* ソーシャルボタンプルダウン
   プルダウン遅延処理追加（setTimeout） - 作業中
------------------------------------------------------*/
function msbGroup(elem,act){
	if(!act) return false;
	var a = elem.parentNode.parentNode;
	var b = a.getElementsByTagName('div');
	var c = [];
	for(var i = 0; i < b.length; i++){
		if(b[i].className == 'msbGroupIn'){
			c.push(b[i]);
			break;
		}
	}
	if(c.length == 0) return false;
	var target = c[0];
	if(act == 'over') target.style.display = 'block';
	if(act == 'out') target.style.display = 'none';
	return true;
}

/* ナビゲーションプルダウン（jQuery） - 2011/06/03
   第三階層対応 - 2013/05/12
-----------------------------------------------------------------------*/
function localNavigationPD(id,flag){
	if(!id || !flag) return false;
	if(!document.getElementById(id)) return false;
	var a = document.getElementById(id);
	var b = a.getElementsByTagName('div');
	var c = [];
	for(var i in flag){
		if(i == '') break;
		for(var j = 0; j < b.length; j++){
			if(b[j].className.match(/^lnavBtn\s?/) && b[j].innerHTML.match(i)){
				b[j].className += ' lnavBtnHover';
				var tempTop = '<div class="lnavBtnGroupOut"><div class="lnavBtnGroupIn">';
				var tempMid = [];
				for(var k = 0; k < flag[i]['data'].length; k++){
					if(!flag[i]['data'][k]['name']) continue;
					if(flag[i]['data'][k]['data']){
						tempMid.push('<div class="lnavBtn lnavBtnHover">');
					}else{
						tempMid.push('<div class="lnavBtn">');
					}
					tempMid.push('<a href="' + flag[i]['data'][k]['url'] + '" onClick="designCnt(\'' + id + '_PD1\',this.href);"><span class="raquo">&raquo;</span> ' + flag[i]['data'][k]['name'] + '</a>');
					if(flag[i]['data'][k]['data']){
						tempMid.push('<div class="lnavBtnGroup" style="position:absolute;padding:0 !important;margin:0;"><div class="lnavBtnGroupOut"><div class="lnavBtnGroupIn">');
						for(var l = 0; l < flag[i]['data'][k]['data'].length; l++){
							if(!flag[i]['data'][k]['data'][l]['name']) continue;
							tempMid.push('<div class="lnavBtn"><a href="' + flag[i]['data'][k]['data'][l]['url'] + '" onClick="designCnt(\'' + id + '_PD2\',this.href);"><span class="raquo">&raquo;</span> ' + flag[i]['data'][k]['data'][l]['name'] + '</a></div>');
						}
						tempMid.push('</div></div></div>');
					}
					tempMid.push('</div>');
				}
				var tempBtm = '</div></div>';
				var joinHTML = tempTop + tempMid.join('') + tempBtm;
				var insertNode = document.createElement('div');
				insertNode.className = 'lnavBtnGroup';
				insertNode.style.display = 'none';
				insertNode.style.position = 'absolute';
				insertNode.innerHTML = joinHTML;
				b[j].appendChild(insertNode);
				if(b[j].getElementsByTagName('a')[0].getElementsByTagName('span')[0]){
					b[j].getElementsByTagName('a')[0].getElementsByTagName('span')[0].innerHTML = '▼';
				}
				break;
				}
			}
		}

	function parentRel(elem){ // 上位で position:relative があれば取得しプルダウンナビゲーションの座標からマイナスする - 11/08/18
		do{
			var style = elem.currentStyle || document.defaultView.getComputedStyle(elem,'');
			if(elem.tagName == 'BODY') break;
			if(style.position == 'relative') break;
			var elem = elem.parentNode;
		}while(style.position != 'relative');
		return elem;
	};

	// 2 階層目イベント
	jQuery('#' + id + ' .lnavBtnHover').hover(
		function(){
			jQuery(this).children('.lnavBtnGroup').css('display','block');
			jQuery(this).children('.lnavBtnGroup').css('left',((getElementXY(this)['x']) - getElementXY(parentRel(a))['x']) + 'px');
			jQuery(this).children('.lnavBtnGroup').css('top',((getElementXY(this)['y'] + getElementWH(jQuery(this).get(0))['h'] - 2) - getElementXY(parentRel(a))['y']) + 'px');
		},function(){
			jQuery(this).children('.lnavBtnGroup').css('display','none');
		}
	);

	// 3 階層目イベント
	jQuery('#' + id + ' .lnavBtnHover .lnavBtnHover').hover(
		function(){
			jQuery(this).children('.lnavBtnGroup').css('display','block');
			jQuery(this).children('.lnavBtnGroup').css('left','200px');
			jQuery(this).children('.lnavBtnGroup').css('top',(getElementXY(this)['y'] - getElementXY(this.parentNode.parentNode.parentNode)['y'] - 1) + 'px');
		},function(){
			jQuery(this).children('.lnavBtnGroup').css('display','none');
		}
	);
	return true;
};

/* ITMID アラートボタン - 11/06/14 現在未使用
-----------------------------------------------------------------------*/
function ITMIDalert(myForm){
	ITMIDalert.setForm(myForm);
	myForm.submit();
	return true;
};
ITMIDalert.setForm = function(myForm){
	myForm.action = 'https://id.itmedia.jp/isentry';
	myForm.method = 'get';
	myForm.article_url.value = ITMIDalert.getURL();
	myForm.encoding.value = ITMIDalert.getCharset();
	myForm.return_url.value = 'https://id.itmedia.jp/app/alert/regist_setting?url=' + ITMIDalert.getURL();
};
ITMIDalert.getURL = function(){
	var a = location.pathname.replace(/\.html.+/,'html');
	if(a.match(/^\/(makoto|bizid|d-style|bizmobile)\//)){
		var b = 'http://bizmakoto.jp';
	}else if(a.match(/^\/dc\//)){
		var b = 'http://camera.itmedia.co.jp';
	}else if(a.match(/^\/games\//)){
		var b = 'http://gamez.itmedia.co.jp';
	}else if(a.match(/^\/executive\//)){
		var b = 'http://mag.executive.itmedia.co.jp';
	}else if(a.match(/^\/ebook\//)){
		var b = 'http://ebook.itmedia.co.jp';
	}else if(a.match(/^\/mn\//)){
		var b = 'http://monoist.atmarkit.co.jp';
	}else if(a.match(/^\/ee\//)){
		var b = 'http://eetimes.jp';
	}else if(a.match(/^\/gg\//)){
		var b = 'http://gadget.itmedia.co.jp';
	}else if(a.match(/^\/nl\//)){
		var b = 'http://nlab.itmedia.co.jp';
	}else if(a.match(/^\/tt\//)){
		var b = 'http://techtarget.itmedia.co.jp';
	}else{
		var b = 'http://www.itmedia.co.jp';
	}
    return b + a;
};
ITMIDalert.getCharset = function(){
    var content_type;
    content_type = ITMIDalert.getMeta('http-equiv','content-type');
    var charset  = content_type.split(';');
    if( charset.length > 1 ){
        charset = charset[1].split("=");
        var encoding = charset[1].toLowerCase();
        if(encoding.match(/shift/) == 'shift'){
            return 'shiftjis';
        }
        if(encoding.match(/euc/) == 'euc'){
            return 'euc-jp';
        }
        if(encoding.match(/utf/) == 'utf'){
            return 'utf8';
        }
    }
    return '';
};
ITMIDalert.getMeta = function(attr_name,key){
	var elements = document.getElementsByTagName('meta');
	for(var i = 0 ; i< elements.length;i++){
		if(elements[i].getAttribute(attr_name) == key){
			return elements[i].getAttribute('content');
		}
	}
	return '';
};

/* WINDOW 幅＆高さ取得 - 11/08/11
-----------------------------------------------------------------------*/
function getBrowserWidth(){
	if(window.innerWidth){
		return window.innerWidth;
	}else if(document.documentElement && document.documentElement.clientWidth != 0){
		return document.documentElement.clientWidth;
	}else if(document.body){
		return document.body.clientWidth;
	}
	return 0;
}
function getBrowserHeight(){
	if(window.innerHeight){
		return window.innerHeight;
	}else if(document.documentElement && document.documentElement.clientHeight != 0){
		return document.documentElement.clientHeight;
	}else if(document.body){
		return document.body.clientHeight;
	}
	return 0;
}

/* スマートフォン表示 - 11/08/04
-----------------------------------------------------------------------*/
(function(){
return false; // 実行しない
if(!getUrl()['domain'].match(/preview|localhost/)) return false;
if(navigator.userAgent.indexOf('iPod') != -1) return false;
if(navigator.userAgent.indexOf('iPhone') == -1 && navigator.userAgent.indexOf('iPad') == -1 && navigator.userAgent.indexOf('Android') == -1) return false;
document.write('<meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=1;">');
document.write('<link rel="stylesheet" type="text/css" media="all" href="/css/smartphone.css">');
window.onload = function(){

// スタイルシート制御
	var a = document.getElementsByTagName('head')[0];
	var b = a.getElementsByTagName('link');
	for(var i = 0; i < b.length; i++){
		if(b[i].href.match(/\/css\/.+\/.+\.css/)){
			var c = b[i].href.match(/\/css\/(.+\/).+\.css/);
			b[i].href = b[i].href.replace(RegExp.$1,'');
		}
	}

// カラム移動
	if(document.getElementById('masterMain') && document.getElementById('masterMainLeft') && document.getElementById('masterMainRight')){
		var masterMain = document.getElementById('masterMain');
		var masterMainLeft = document.getElementById('masterMainLeft');
		masterMain.appendChild(masterMainLeft);
	}

// デバッグナビゲーション
/*
	var navwrap = document.createElement('div');
	navwrap.className = 'colBox colBoxDebugNavigation';
	var nav = '';
	nav += '<div class="colBoxOuter">';
	nav += '<div class="colBoxHead"><h2>デバッグ用ナビゲーション</h2></div>';
	nav += '<div class="colBoxInner">';
	nav += '<div class="colBoxIndex">';
	nav += '<div class="colBoxUlist">';
	nav += '<ul>';
	nav += '<li><a href="/mn/root/">mn</a></li>';
	nav += '<li><a href="/ee/root/">ee</a></li>';
	nav += '<li><a href="/gg/root/">gg</a></li>';
	nav += '<li><a href="/nl/root/">nl</a></li>';
	nav += '<li><a href="/style/">style</a></li>';
	nav += '<li><a href="/enterprise/">enterprise</a></li>';
	nav += '</ul>';
	nav += '<ul>';
	nav += '<li><a href="/news/">news</a></li>';
	nav += '<li><a href="/executive/">executive</a></li>';
	nav += '</ul>';
	nav += '</div>';
	nav += '</div>';
	nav += '</div>';
	nav += '</div>';
	navwrap.innerHTML = nav;
	document.body.insertBefore(navwrap,document.body.childNodes[0]);
*/

// ロゴ表示
	var logowrap = document.createElement('div');
	logowrap.style.marginBottom = '10px';
	var logolink = document.createElement('a');
	logolink.setAttribute('href','/' + masterChannel() + '/');
	var logo = document.createElement('img');
	logo.setAttribute('src',imgSrv() + '/images/logo/150_' + masterChannel() + '_bgw.gif');
	logolink.appendChild(logo);
	logowrap.appendChild(logolink);
	document.body.insertBefore(logowrap,document.body.childNodes[0]);

// 画像制御
	var a = document.getElementById('masterMain');
	var b = a.getElementsByTagName('img');
	for(var i = 0; i < b.length; i++){
		if(b[i].width > 300){
			var w = b[i].width;
			var h = b[i].height;
			var wh = (function(ww,hh){
				return ww / hh;
			})(w,h);
			b[i].width = 300;
			b[i].height = 300 / wh;
		}
	}

/* 
	var masterContents = document.getElementById('masterContents');
	var htmlDiv = masterContents.getElementsByTagName('div');
	var colBoxHead = [];
	for(var i = 0; i < htmlDiv.length; i++){
		var temp = htmlDiv[i].className.split(' ');
		if(temp[0] == 'colBox'){
			var flag = 0;
			var colBoxDiv = htmlDiv[i].getElementsByTagName('div');
			for(var j = 0; j < colBoxDiv.length; j++){
				if(colBoxDiv[j].className.match('colBoxHead')){
					colBoxHead.push(colBoxDiv[j]);
					if(colBoxHead.length > 3) colBoxDiv[j].nextSibling.nextSibling.style.display = 'none';
					colBoxDiv[j].setAttribute('onClick','var node = this.nextSibling.nextSibling;if(node.style.display == \'none\'){node.style.display = \'block\';}else{node.style.display = \'none\';};');
					flag++;
					break;
				}
			}
			if(flag == 0) htmlDiv[i].style.display = 'none';
		}
	}
*/
};
})();

/* RETWEET
-----------------------------------------------------------------------*/
function retweet(data){
	retweet.setDomain(); // ドメイン判定
	retweet.setData(data); // 引数を変数に代入
	retweet.setKey(); // ID と APIKEY を変数に代入
	retweet.setApi(); // ID と APIKEY を APIURL に代入し変数に代入
	retweet.setVariable(); // 変数セット フラグ
	retweet.setHashtag(); // ハッシュタグありなし
	retweet.setTitlecut(); // タイトルをカットするかしないか
	retweet.thispageORindexes(); // location か index か判別
/*	retweet.addScript();*/
	setTimeout('retweet.addScript()',100); // JSON コール
};
retweet.setDomain = function(){ // ドメイン判定
	var a = location.pathname;
	if(a.match(/^\/(makoto|bizid|d-style|bizmobile)\//)){
		var b = 'http://bizmakoto.jp';
	}else if(a.match(/^\/dc\//)){
		var b = 'http://camera.itmedia.co.jp';
	}else if(a.match(/^\/games\//)){
		var b = 'http://gamez.itmedia.co.jp';
	}else if(a.match(/^\/executive\//)){
		var b = 'http://mag.executive.itmedia.co.jp';
	}else if(a.match(/^\/tt\//)){
		var b = 'http://techtarget.itmedia.co.jp';
	}else if(a.match(/^\/ebook\//)){
		var b = 'http://ebook.itmedia.co.jp';
	}else{
		var b = 'http://www.itmedia.co.jp';
	}
	this.mydomain = b;
};
retweet.setData = function(data){ // 引数を変数に代入
	this.data = data;
};
retweet.setKey = function(){ // ID と APIKEY を変数に代入
	if(!this.data['id'] || !this.data['apikey']){
		this.bitly_id  = 'itmedia';
		this.bitly_key = 'R_736fc703c85d8539ea61ad3758938118';
	}else{
		this.bitly_id  = this.data['id'];
		this.bitly_key = this.data['apikey'];
	}
};
retweet.setApi = function(){ // ID と APIKEY を APIURL に代入し変数に代入
	this.api = 'http://api.bit.ly/shorten' + '?version=2.0.1' + '&format=json' + '&callback=retweetCallback' + '&login=' + this.bitly_id + '&apiKey=' + this.bitly_key + '&longUrl=';
};
retweet.setVariable = function(){ // 変数セットフラグ
	if(!this.data['variable']){
		this.variable = false;
	}else{
		this.variable = true;
	}
};
retweet.setHashtag = function(){ // hashtag セット
	if(!this.data['hashtag']){
		this.hashtag = '';
	}else{
		this.hashtag = this.data['hashtag'];
	}
};
retweet.setTitlecut = function(){ // Title カット
	if(!this.data['titlecut']){
		this.titlecut = false;
	}else{
		this.titlecut = this.data['titlecut'];
	}
};
retweet.thispageORindexes = function(){ // location か index か判別
	if(!this.data['elem']){
		retweet.thispage();
	}else{
		retweet.indexes();
	}
};
retweet.addScript = function(){
	document.getElementsByTagName('head')[0].appendChild(this.script);
};
retweet.thispage = function(){
	if(!this.titlecut == false){
		if(document.title.length < this.titlecut){
			this.tit = document.title;
		}else{
			this.tit = document.title.slice(0,this.titlecut) + '...';
		}
	}else{
		this.tit = document.title;
	}
	this.script = document.createElement('script');
	this.script.setAttribute('type','text/javascript');
	this.script.setAttribute('title','{"title":"' + this.tit + '","link":"' + this.mydomain + location.pathname + '","hashtag":"' + this.hashtag + '","variable":' + this.variable + '}');
	this.script.setAttribute('src',this.api + encodeURIComponent(this.mydomain + location.pathname));
};
retweet.indexes = function(){
	if(!this.data['blockclassname']){
		this.blockclassname = 'index';
	}else{
		this.blockclassname = this.data['blockclassname'];
	}
	if(!this.data['titleclassname']){
		this.titleclassname = 'txTitle';
	}else{
		this.titleclassname = this.data['titleclassname'];
	}
	if(!this.data['urlclassname']){
		this.urlclassname = 'URL';
	}else{
		this.urlclassname = this.data['urlclassname'];
	}
	this.btn = this.data['elem'];
	while(this.btn.className != this.blockclassname){
		this.btn = this.btn.parentNode;
	}
	this.box = this.btn;
	this.chl = this.box.childNodes;
	for(var i = 0; i < this.chl.length; i++){
		if(this.chl[i].className == this.titleclassname){
			var a = this.chl[i].getElementsByTagName('a');
			for(var j = 0; j < a.length; j++){
				if(a[j].className == this.urlclassname){
					this.lnk = a[j].getAttribute('href');
					this.tit = a[j].innerHTML;
					break;
				}
			}
		}
	}
	if(this.lnk.match(/^http/)) this.mydomain = '';
	this.script = document.createElement('script');
	this.script.setAttribute('type','text/javascript');
	this.script.setAttribute('title','{"title":"' + this.tit + '","link":"' + this.mydomain + this.lnk + '","hashtag":"' + this.hashtag + '","variable":' + this.variable + '}');
	this.script.setAttribute('src',this.api + encodeURIComponent(this.mydomain + this.lnk));
};

/* bitly API
-----------------------------------------------------------------------*/
function retweetCallback(json){
	var sel = '';
	if(document.selection){
		sel = document.selection.createRange().text;
	}else if(window.selection){
		sel = window.selection.createRange().text;
	}else if(document.getSelection){
		sel = document.getSelection();
	}else if(window.getSelection){
		sel = window.getSelection();
	}

	eval('var attr = ' + getScriptTitleTag(document.getElementsByTagName('head')[0]) + ';');

	var tit = attr['title']; // タイトル
	var lnk = attr['link']; // リンク
	if(!attr['hashtag'] || attr['hashtag'] == ''){ // ハッシュタグ
		var has = '';
	}else{
		var has = ' ' + attr['hashtag'];
	}
	if(attr['variable'] == true){
		shorturl_bitly = json.results[lnk]['shortUrl'];
	}else{
		var f = 'http://twitter.com/intent/tweet?text=' + encodeURIComponent(tit + ' ' + json.results[lnk]['shortUrl'] + has);
		if(navigator.userAgent.indexOf('Chrome') != -1 || navigator.userAgent.indexOf('Safari') != -1){
			location.href = f;
		}else{
			window.open(f,'retweet');
		}
	}
/*	document.removeChild(document.getElementsByTagName('head')[0].lastChild);*/
};


/* TWITTER WIDGET
-----------------------------------------------------------------------*/
function twimgWidget(hash){
if(typeof TWTR.Widget != 'function') return false;
if(!hash['件数'] || hash['件数'] == '' || hash['件数'] == undefined) hash['件数'] = 100;
if(!hash['表示間隔（ミリ秒）'] || hash['表示間隔（ミリ秒）'] == '' || hash['表示間隔（ミリ秒）'] == undefined) hash['表示間隔（ミリ秒）'] = 6000;
if(!hash['ウィジェット本体背景色'] || hash['ウィジェット本体背景色'] == '' || hash['ウィジェット本体背景色'] == undefined) hash['ウィジェット本体背景色'] = '8EC1DA';
if(!hash['ウィジェット本体文字色'] || hash['ウィジェット本体文字色'] == '' || hash['ウィジェット本体文字色'] == undefined) hash['ウィジェット本体文字色'] = 'FFF';
if(!hash['つぶやき背景色'] || hash['つぶやき背景色'] == '' || hash['つぶやき背景色'] == undefined) hash['つぶやき背景色'] = 'FFF';
if(!hash['つぶやき文字色'] || hash['つぶやき文字色'] == '' || hash['つぶやき文字色'] == undefined) hash['つぶやき文字色'] = '444';
if(!hash['つぶやきリンク色'] || hash['つぶやきリンク色'] == '' || hash['つぶやきリンク色'] == undefined) hash['つぶやきリンク色'] = '1985B5';
if(!hash['幅'] || hash['幅'] == '' || hash['幅'] == undefined) hash['幅'] = 290;
if(!hash['高さ'] || hash['高さ'] == '' || hash['高さ'] == undefined) hash['高さ'] = 300;
if(hash['回り込み'] == 'left'){
	document.write('<div style="width:' + hash['幅'] + 'px;margin:0 10px 5px 0;float:' + hash['回り込み'] + ';" id="twimgWidgetID">');
}else if(hash['回り込み'] == 'right'){
	document.write('<div style="width:' + hash['幅'] + 'px;margin:0 0 5px 5px;float:' + hash['回り込み'] + ';" id="twimgWidgetID">');
}else if(hash['回り込み'] == 'center'){
	document.write('<div style="width:' + hash['幅'] + 'px;margin:0 auto;" id="twimgWidgetID">');
}else{
	document.write('<div style="width:' + hash['幅'] + 'px;float:none;" id="twimgWidgetID">');
}
document.write('</div>');
if(hash['アカウント'] != undefined){
	new TWTR.Widget({
	version:2,
	type:'profile',
	rpp:hash['件数'],
	interval:hash['表示間隔（ミリ秒）'],
	width:hash['幅'],
	height:hash['高さ'],
	theme:{
	shell:{
	background:'#' + hash['ウィジェット本体背景色'],
	color:'#' + hash['ウィジェット本体文字色']
	},
	tweets:{
	background:'#' + hash['つぶやき背景色'],
	color:'#' + hash['つぶやき文字色'],
	links:'#' + hash['つぶやきリンク色']
	}
	},
	features:{
	scrollbar:hash['スクロールバー'],
	loop:hash['ループ'],
	live:hash['エフェクト'],
	hashtags:hash['ハッシュタグ'],
	timestamp:hash['タイムスタンプ'],
	avatars:hash['アバター'],
	behavior:'default'
	}
	}).render().setUser(hash['アカウント']).start();
}else if(hash['検索語'] != ''){
	new TWTR.Widget({
	version:2,
	type:'search',
	search:hash['検索語'],
	interval:hash['表示間隔（ミリ秒）'],
	title:hash['サブタイトル'],
	subject:hash['タイトル'],
	width:hash['幅'],
	height:hash['高さ'],
	theme:{
	shell:{
	background:'#' + hash['ウィジェット本体背景色'],
	color:'#' + hash['ウィジェット本体文字色']
	},
	tweets:{
	background:'#' + hash['つぶやき背景色'],
	color:'#' + hash['つぶやき文字色'],
	links:'#' + hash['つぶやきリンク色']
	}
	},
	features:{
	scrollbar:hash['スクロールバー'],
	loop:hash['ループ'],
	live:hash['エフェクト'],
	hashtags:hash['ハッシュタグ'],
	timestamp:hash['タイムスタンプ'],
	avatars:hash['アバター'],
	behavior:''
	}
	}).render().start();
}else{
}
if(!document.getElementById('twtr-widget-1')) return false;
document.getElementById('twimgWidgetID').appendChild(document.getElementById('twtr-widget-1'));
return true;
};

/* 文字バイト数カウント＆指定文字バイト数でカット - 111026
-----------------------------------------------------------------------*/
function cutString(str,num){
	var len = 0;
	var estr = escape(str);
	var ostr = '';
	for(i = 0;i < estr.length; i++){
		len++;
		ostr = ostr + estr.charAt(i);
		if(estr.charAt(i) == '%'){
			i++;
			ostr = ostr + estr.charAt(i);
			if(estr.charAt(i) == 'u'){
				ostr = ostr + estr.charAt(i + 1) + estr.charAt(i + 2) + estr.charAt(i + 3) + estr.charAt(i + 4);
				i += 4;
				len++;
			}
		}
		if(len >= num - 3){
			return unescape(ostr) + '...';
		}
	}
	return unescape(ostr);
};

/* ソーシャルパーツ RD:5609 - 111104
-----------------------------------------------------------------------*/
function snsContents(data){
	snsContents.writ(snsContents.code(data));
//	snsContents.twit(data);
	snsContents.getTarget();
	snsContents.getContents();
	snsContents.setButtons();
	snsContents.addButtons();
	snsContents.setFirstEvent();
};
snsContents.code = function(data){
	if(!data['boxid']) data['boxid'] = '';
	if(!data['width']) data['width'] = '300px';
	var code = {
	'colBoxSnsMostpopular':(function(){
		if(!data['likebox'] || data['likebox'] == ''){
			return '';
		}else{
		var likeboxparams = '' +
		'href=http%3A%2F%2Fwww.facebook.com%2F' + data['likebox'] + '&amp;' +
		'width=' + data['width'].replace('px','') + '&amp;' +
		'colorscheme=light&amp;' +
		'show_faces=false&amp;' +
		'border_color&amp;' +
		'stream=false&amp;' +
		'header=true&amp;' +
		'height=62'
		var activityparams = '' +
		'site=' + data['activitydomain'] + '&amp;' +
		'filter=' + data['activityfilter'] + '&amp;' +
		'width=' + data['width'].replace('px','') + '&amp;' +
		'height=200&amp;' +
		'header=false&amp;' +
		'colorscheme=light&amp;' +
		'linktarget=_blank&amp;' +
		'border_color=%23FFF&amp;' +
		'font&amp;' +
		'recommendations=false'
		return '' +
		'<div class="colBox colBoxSnsMostpopular" id="colBoxSnsMostpopular' + data['boxid'] + '">' +
		'<div class="colBoxOuter">' +
		'<div class="colBoxHead"><h2 name="Facebook">Facebook</h2></div>' +
		'<div class="colBoxInner">' +
		'<iframe src="//www.facebook.com/plugins/likebox.php?' +
		likeboxparams + 
		'" scrolling="no" frameborder="0" style="border:none;overflow:hidden;width:' + data['width'] + ';height:62px;" allowTransparency="true"></iframe>' +
		'<iframe src="//www.facebook.com/plugins/activity.php?' +
		activityparams + 
		'" scrolling="no" frameborder="0" style="border:none;overflow:hidden;width:' + data['width'] + ';height:200px;" allowTransparency="true"></iframe>' +
		'</div>' +
		'</div>' +
		'</div>'
		}
	})(),
	'colBoxSnsFriendsactivity':(function(){
		if(!data['likebox'] || data['likebox'] == ''){
			return '';
		}else{
		var likeboxparams = '' +
		'width=' + data['width'].replace('px','') + '&amp;' +
		'href=http%3A%2F%2Fwww.facebook.com%2F' + data['likebox'] + '&amp;' +
		'colorscheme=light&amp;' +
		'show_faces=false&amp;' +
		'border_color&amp;' +
		'stream=false&amp;' +
		'header=true&amp;' +
		'height=62'
		var recommendparams = '' +
		'site=' + data['activitydomain'] + '&amp;' +
		'width=' + data['width'].replace('px','') + '&amp;' +
		'height=200&amp;' +
		'header=false&amp;' +
		'colorscheme=light&amp;' +
		'linktarget=_blank&amp;' +
		'border_color=%23FFF&amp;' +
		'font'
		return '' +
		'<div class="colBox colBoxSnsFriendsactivity" id="colBoxSnsFriendsactivity' + data['boxid'] + '">' +
		'<div class="colBoxOuter">' +
		'<div class="colBoxHead"><h2 name="おすすめ">おすすめ</h2></div>' +
		'<div class="colBoxInner">' +
		'<iframe src="//www.facebook.com/plugins/likebox.php?' +
		likeboxparams + 
		'" scrolling="no" frameborder="0" style="border:none;overflow:hidden;width:' + data['width'] + ';height:62px;" allowTransparency="true"></iframe>' +
		'<iframe src="//www.facebook.com/plugins/recommendations.php?' +
		recommendparams + 
		'" scrolling="no" frameborder="0" style="border:none;overflow:hidden;width:' + data['width'] + ';height:200px;" allowTransparency="true"></iframe>' +
		'</div>' +
		'</div>' +
		'</div>'
		}
	})(),
	'colBoxSnsEmbeddedTimelines':(function(){
		if(!data['tw_widget_id'] || data['tw_widget_id'] == '' || !data['twitterid'] || data['twitterid'] == ''){
			return '';
		}else{
			return '' + 
			'<div class="colBox colBoxSnsEmbeddedTimelines" id="colBoxSnsEmbeddedTimelines' + data['boxid'] + '"><div class="colBoxOuter">' +
			'<div class="colBoxHead"><h2 name="Twitter">Twitter</h2></div>' +
			'<div class="colBoxInner">' +
			'<a class="twitter-timeline" href="https://twitter.com/' + data['twitterid'] + '" data-widget-id="' + data['tw_widget_id'] + '" width="' + data['width'] + '" height="350">@' + data['twitterid'] + ' からのツイート</a>' + 
			'<script>!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0];if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src="//platform.twitter.com/widgets.js";fjs.parentNode.insertBefore(js,fjs);}}(document,"script","twitter-wjs");<\/script>' +
			'</div>' +
			'</div></div>';
		}
	})()
/*
	'colBoxSnsMosttweeted':(function(){
		if(!data['twitterid'] || data['twitterid'] == ''){
			return '';
		}else{
			return '' +
			'<div class="colBox colBoxSnsMosttweeted" id="colBoxSnsMosttweeted' + data['boxid'] + '"><div class="colBoxOuter" style="padding:10px;">' +
			'<div class="colBoxHead"><h2 name="Twitter">Twitter</h2></div>' +
			'<div class="colBoxInner">' +
			'<div id="socialPartsTwitterJson_' + data['twitterid'] + '"></div>' +
			'</div>' +
			'</div></div>'
		}
	})(),
*/
	};
	return {'code':code,'boxid':data['boxid'],'width':data['width']};
};
snsContents.writ = function(data){
	this.boxid = data['boxid'];
	document.write('<div class="snsContents" id="snsContents' + this.boxid + '" style="width:' + data['width'] + ';">');
	for(var i in data['code']){
		document.write(data['code'][i]);
	}
	document.write('</div>');
};
/*
snsContents.twit = function(data){
	return false;
	if(!data['twitterid'] || data['twitterid'] == '') return false;
	jQuery.ajax({
		scriptCharset:'UTF-8',
		type:'GET',
		url:'http://api.twitter.com/1/statuses/user_timeline/' + data['twitterid'] + '.json?rpp=10&callback=?',
		timeout:5000,
		beforeSend:function(){
			var a = [];
			a.push('<div class="colBoxIndex">');
			a.push('<div class="colBoxTitle"><h3 style="font-size:12px;">ロード中</h3></div>');
			a.push('</div>');
			jQuery('#socialPartsTwitterJson_' + data['twitterid']).html(a.join(''));
		},
		success:function(json,textStatus){
			var a = [];
			if(json[0].error){
				a.push('<div class="colBoxIndex">');
				a.push('<div class="colBoxTitle"><h3 style="font-size:12px;">' + json[0].error + '</h3></div>');
				a.push('</div>');
			}else{
				a.push('<div class="colBoxIndex">');
				a.push('<div class="colBoxIcon"><a href="http://twitter.com/#!/' + json[0].user.screen_name + '"><img src="' + json[0].user.profile_image_url + '"></a></div>');
				a.push('<div class="colBoxTitle"><h3 style="font-size:12px;"><a href="http://twitter.com/#!/'  + json[0].user.screen_name + '">' + json[0].user.name + '</a> @' + json[0].user.screen_name + '</h3></div>');
				a.push('<div class="colBoxDescription"><p style="font-style:italic;font-size:11px;font-weight:bold;">' + json[0].user.description + '</p></div>');
				a.push('<div class="colBoxClear h10px"></div>');
				a.push('<div class="colBoxUlist"><ul>');
				for(var i in json){
					var repText = json[i].text;
					repText = repText.replace(/(s?https?:\/\/[-_.!~*'()a-zA-Z0-9;\/?:@&=+$,%#]+)/gi,'<a href="$1" target="_blank">$1</a>'); // リンク
					repText = repText.replace(/#(\w+)/gi,'<a href="http://twitter.com/search?q=%23$1" target="_blank">#$1</a>'); // ハッシュタグ
					repText = repText.replace(/@(\w+)/gi,'<a href="http://twitter.com/$1" target="_blank">@$1</a>'); // リプライ
					var repDate1 = json[i].created_at.split(' ');
					var repDate2 = repDate1[1] + ' ' + repDate1[2] + ' ,' + repDate1[5] + ' ' + repDate1[3];
					var repDate3 = new Date(repDate2);
					repDate3.setHours(repDate3.getHours() + 9);
					var repDate4  = repDate3.getMonth() + 1; // 月
					var repDate5  = repDate3.getDate(); // 日
					a.push('<li>' + repText + ' （' + repDate4 + '月' + repDate5 + '日 ' + repDate1[3] + '）</li>');
				}
				a.push('</ul></div></div>');
			}
			jQuery('#socialPartsTwitterJson_' + data['twitterid']).html(a.join(''));
		},
		error:function(XMLHttpRequest,textStatus,errorThrown){
			var a = [];
			a.push('<div class="colBoxIndex">');
			if(textStatus == 'timeout'){
				a.push('<div class="colBoxTitle"><h3 style="font-size:12px;">接続がタイムアウトしました</h3></div>');
				a.push('<div class="colBoxDescription"><p style="font-style:italic;font-size:11px;font-weight:bold;"><a href="javascript:void(0);" onClick="snsContents.twit({\'twitterid\':\'' + data['twitterid'] + '\'});">再度問い合わせする</a></p></div>');
			}else if(textStatus == 'error'){
				a.push('<div class="colBoxTitle"><h3 style="font-size:12px;">リクエスト失敗</h3></div>');
			}else if(textStatus == 'parsererror'){
				a.push('<div class="colBoxTitle"><h3 style="font-size:12px;">データパースエラー</h3></div>');
			}else{
			}
			a.push('</div>');
			jQuery('#socialPartsTwitterJson_' + data['twitterid']).html(a.join(''));
		},
		dataType:'json'
	});
	return true;
};
*/
snsContents.getTarget = function(){
	this.target = document.getElementById('snsContents' + this.boxid);
};
snsContents.getContents = function(){
	var a = this.target.getElementsByTagName('div');
	this.contents = [];
	for(var i = 0; i < a.length; i++){
		if(a[i].className.match(/^colBox /)){
			if(!a[i].getElementsByTagName('h2')[0]) continue;
			var b = a[i].getElementsByTagName('h2')[0];
			this.contents.push({'name':b.innerHTML,'forumid':b.getAttribute('name'),'content':a[i]});
 		}
	}
};
snsContents.setButtons = function(){
	this.buttonsCode = [];
	for(var i = 0; i < this.contents.length; i++){
		this.buttonsCode.push('<li name="' + this.contents[i]['forumid'] + '" onClick="snsContents.setEventClick(' + i + ',this);" onMouseOver="snsContents.setEventHover(\'hover\',this);" onMouseOut="snsContents.setEventHover(\'out\',this);">' + this.contents[i]['name'] + '</li>');
	}
};
snsContents.addButtons = function(){
	var a = [];
	a.push('<div class="colBox">');
	a.push('<div class="colBoxOuter">');
	a.push('<div class="colBoxInner">');
	a.push('<div class="colBoxIndex">');
	a.push('<div class="colBoxUlist"><ul>');
	a.push(this.buttonsCode.join(''));
	a.push('</ul></div>');
	a.push('<div class="colBoxClear"></div>');
	a.push('</div>');
	a.push('</div>');
	a.push('</div>');
	a.push('</div>');
	var b = document.createElement('div');
	b.setAttribute('id','snsButtons' + this.boxid);
	b.className = 'snsButtons';
	b.innerHTML = a.join('');
	this.target.insertBefore(b,this.target.childNodes[0]);
};
snsContents.setFirstEvent = function(){
	var a = document.getElementById('snsButtons' + this.boxid);
	var buttons = a.getElementsByTagName('li');
	for(var i = 0; i < buttons.length; i++){
		if(getUrl()['url'].match('#' + buttons[i].getAttribute('name'))){
			buttons[i].className = 'active';
			snsContents.setEventClick(i,buttons[i]);
			return true;
		}
	}
	buttons[0].className = 'active';
	snsContents.setEventClick(0,buttons[0]);
};
snsContents.setEventClick = function(number,elem){
	// BUTTONS //
	var a = elem.parentNode;
	var buttons = a.getElementsByTagName('li');
	for(var i = 0; i < buttons.length; i++){
		if(i == number){
			buttons[i].className = 'active';
		}else{
			buttons[i].className = '';
		}
	}
	// BUTTONS //
	// CONTENTSS //
	var b = a.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
	var c = b.childNodes;
	for(var i = 1; i < c.length; i++){
		// platform.twitter.com/widgets.js の実行タイミングの関係で
		// display:none,block ではだめ
		// 初期が display:none だと iframe 高さ処理が正しく行われないブラウザ（Firefox で確認）がある
		if((number + 1) == c.length){
			c[i].style.visibility = 'visible';
			c[i].style.position = '';
			continue;
		}
		if(i == (number + 1)){
			c[i].style.visibility = 'visible';
			c[i].style.position = '';
			continue;
		}
		c[i].style.visibility = 'hidden';
		c[i].style.position = 'absolute';
	}
	// CONTENTSS //
};
snsContents.setEventHover = function(type,elem){
	if(elem.className == 'active') return false;
	if(type == 'hover') elem.className = 'hover';
	if(type == 'out') elem.className = '';
};

/* senna 連載 json RD:6327 - 120117
-----------------------------------------------------------------------*/
function rensaiCallback(json){
	var parentW = getElementWH(getScriptThis(document).parentNode)['w'];
	if(!getScriptTitleTag(document)){
		json['param'] = {
			'type':'default',
			'column':'1',
			'box':'true',
			'loop':json['data'].length,
			'head':'true',
			'icon':'true',
			'icontype':'default',
			'subtitle':'true',
			'description':'true',
			'dcdate':'true'
		};
	}else{
		var parentW = getElementWH(getScriptThis(document).parentNode)['w'];
		var query = getScriptTitleTag(document);
		var str = '{"' + query.replace(/\=/g,'":"').replace(/\&/g,'","') + '"}';
		json['param'] = eval('(' + str + ')');
		if(isNaN(json['param']['column']) == true || json['param']['column'] > 4) json['param']['column'] = 4;
		if(isNaN(json['param']['loop']) == true || json['param']['loop'] > json['data'].length) json['param']['loop'] = json['data'].length;
	}
	json['param']['parentW'] = parentW;

	if(json['param']['type'] == 'list'){
		document.write(rensaiCallback.templateList(json));
	}else if(json['param']['type'] == 'notfloat'){
		document.write(rensaiCallback.templateNotfloat(json));
	}else if(json['param']['type'] == 'mix'){
		document.write(rensaiCallback.templateMix(json));
	}else{
		document.write(rensaiCallback.templateDefault(json));
	}
};

// 見出し
rensaiCallback.partsHead = function(data,param){
	if(param == 'true') return '<div class="colBoxHead"><h2>' + data + '</h2></div>';
	return '';
};

// アイコン
rensaiCallback.partsIcon = function(data,param){
	if(data['icontype'] == 'small'){
		var a = ' style="width:40px;height:30px;"';
	}else{
		var a = '';
	}
	if(data['iconpath'] != '' && param == 'true') return '<div class="colBoxIcon"><a href="' + data['link'] + '"><img src="' + imgSrv() + data['iconpath'] + '"' + a + '></a></div>';
	return '';
};

// サブタイトル
rensaiCallback.partsSubtitle = function(data,param){
	if(data != '' && param == 'true') return '<div class="colBoxSubTitle"><h5>' + data + '</h5></div>';
	return '';
};

// タイトル
rensaiCallback.partsTitle = function(data){
	if(data['icontype'] == 'small'){
		var a = ' style="font-size:14px;line-height:18px;"';
	}else{
		var a = '';
	}
	return '<div class="colBoxTitle"><h3' + a + '><a href="' + data['link'] + '">' + data['title'] + '</a></h3></div>';
};

// アブストラクト
rensaiCallback.partsDescription = function(data,param){
	if(data != '' && param == 'true') return '<div class="colBoxDescription"><p>' + data + '</p></div>';
	return '';
};

// 更新日時
rensaiCallback.partsDcdate = function(data,param){
	if(data != '' && param == 'true') return '<div class="colBoxInfo"><span class="colBoxDate">（' + data + '）</span></div>';
	return '';
};

// カラム
rensaiCallback.partsIndex = function(data,param){
	if(param['box'] == 'true'){
		var box = 'border:1px solid #CCC;background:#DDD;margin:0 0 10px;padding:10px;';
	}else{
		var box = '';
	}
	if(param['column'] != 1){
		var nodeM = 10;
		var nodeW = Math.floor((param['parentW'] - (nodeM * (param['column'] - 1))) / param['column']);
		if((data + 1) % param['column'] == 0){
			return '<div class="colBoxIndex" style="float:left;width:' + nodeW + 'px;"><div style="' + box + '">';
		}else{
			return '<div class="colBoxIndex" style="float:left;margin-right:' + nodeM + 'px;width:' + nodeW + 'px;"><div style="' + box + '">';
		}
	}else{
		return '<div class="colBoxIndex"><div style="' + box + '">';
	}
};

rensaiCallback.templateDefault = function(json){
	var a = [];
	a.push('<div class="colBox">');
	a.push('<div class="colBoxOuter">');
	a.push(rensaiCallback.partsHead(json['head'],json['param']['head']));
	a.push('<div class="colBoxInner">');
	for(var i = 0; i < json['param']['loop']; i++){
		var data = json['data'][i];
		if(!data['title']) continue;
		a.push(rensaiCallback.partsIndex(i,{'column':json['param']['column'],'parentW':json['param']['parentW'],'box':json['param']['box']}));
		a.push(rensaiCallback.partsIcon({'iconpath':data['iconpath'],'icontype':json['param']['icontype'],'link':data['link']},json['param']['icon']));
		a.push(rensaiCallback.partsSubtitle(data['subtitle'],json['param']['subtitle']));
		a.push(rensaiCallback.partsTitle({'title':data['title'],'link':data['link'],'icontype':json['param']['icontype']}));
		a.push(rensaiCallback.partsDescription(data['description'],json['param']['description']));
		a.push(rensaiCallback.partsDcdate(data['dcdate'],json['param']['dcdate']));
		a.push('<div class="colBoxClear h10px"></div>');
		a.push('</div></div>');
		if((i + 1) % json['param']['column'] == 0) a.push('<div class="colBoxClear"></div>');
	}
	if(json['param']['column'] != 1) a.push('<div class="colBoxClear"></div>');
	a.push('</div>');
	a.push('</div>');
	a.push('</div>');
	return a.join('');
};
rensaiCallback.templateNotfloat = function(json){
	var a = [];
	a.push('<div class="colBox">');
	a.push('<div class="colBoxOuter">');
	a.push(rensaiCallback.partsHead(json['head'],json['param']['head']));
	a.push('<div class="colBoxInner">');
	for(var i = 0; i < json['param']['loop']; i++){
		var data = json['data'][i];
		a.push(rensaiCallback.partsIndex(i,{'column':json['param']['column'],'parentW':json['param']['parentW'],'box':json['param']['box']}));
		if(json['param']['icontype'] == 'small'){
			a.push('<div class="colBoxLeft" style="width:40px;">');
		}else{
			a.push('<div class="colBoxLeft" style="width:80px;">');
		}
		a.push(rensaiCallback.partsIcon({'iconpath':data['iconpath'],'icontype':json['param']['icontype'],'link':data['link']},json['param']['icon']));
		a.push('</div>');
		if(json['param']['icontype'] == 'small'){
			a.push('<div class="colBoxRight" style="margin-left:52px;">');
		}else{
			a.push('<div class="colBoxRight" style="margin-left:92px;">');
		}
		a.push(rensaiCallback.partsSubtitle(data['subtitle'],json['param']['subtitle']));
		a.push(rensaiCallback.partsTitle({'title':data['title'],'link':data['link'],'icontype':json['param']['icontype']}));
		a.push(rensaiCallback.partsDescription(data['description'],json['param']['description']));
		a.push(rensaiCallback.partsDcdate(data['dcdate'],json['param']['dcdate']));
		a.push('</div>');
		a.push('<div class="colBoxClear h10px"></div>');
		a.push('</div></div>');
		if((i + 1) % json['param']['column'] == 0) a.push('<div class="colBoxClear"></div>');
	}
	if(json['param']['column'] != 1) a.push('<div class="colBoxClear"></div>');
	a.push('</div>');
	a.push('</div>');
	a.push('</div>');
	return a.join('');
};
rensaiCallback.templateMix = function(json){
	var data = json['data'][0];
	var a = [];
	a.push('<div class="colBox">');
	a.push('<div class="colBoxOuter">');
	a.push(rensaiCallback.partsHead(json['head'],json['param']['head']));
	a.push('<div class="colBoxInner">');
	a.push(rensaiCallback.partsIndex(i,{'column':json['param']['column'],'parentW':json['param']['parentW'],'box':json['param']['box']}));

	if(json['icontype'] == 'small'){
		a.push('<div class="colBoxLeft" style="width:40px;">');
	}else{
		a.push('<div class="colBoxLeft" style="width:80px;">');
	}
	a.push(rensaiCallback.partsIcon({'iconpath':data['iconpath'],'icontype':json['param']['icontype'],'link':data['link']},json['param']['icon']));
	a.push('</div>');
	if(json['icontype'] == 'small'){
		a.push('<div class="colBoxRight" style="margin-left:52px;">');
	}else{
		a.push('<div class="colBoxRight" style="margin-left:92px;">');
	}
	a.push(rensaiCallback.partsSubtitle(data['subtitle'],json['param']['subtitle']));
	a.push(rensaiCallback.partsTitle({'title':data['title'],'link':data['link'],'icontype':json['param']['icontype']}));
	a.push(rensaiCallback.partsDescription(data['description'],json['param']['description']));
	a.push(rensaiCallback.partsDcdate(data['dcdate'],json['param']['dcdate']));
	a.push('<div class="colBoxClear h5px"></div>');
	a.push('<div class="colBoxUlist"><ul>');
	for(var i = 1; i < json['param']['loop']; i++){
		var data = json['data'][i];
		if(!data['title']) continue;
		a.push('<li><a href="' + data['link'] + '">' + data['title'] + '</a></li>');
	}
	a.push('</ul></div>');
	a.push('</div></div>');
	a.push('<div class="colBoxClear h10px"></div>');

	a.push('</div>');
	a.push('</div>');
	a.push('</div>');
	a.push('</div>');
	return a.join('');
};
rensaiCallback.templateList = function(json){
	var a = [];
	a.push('<div class="colBox">');
	a.push('<div class="colBoxOuter">');
	a.push(rensaiCallback.partsHead(json['head'],json['param']['head']));
	a.push('<div class="colBoxInner">');
	a.push('<div class="colBoxIndex">');
	a.push('<div class="colBoxUlist"><ul>');
	for(var i = 0; i < json['param']['loop']; i++){
		var data = json['data'][i];
		if(!data['title']) continue;
		a.push('<li><a href="' + data['link'] + '">' + data['title'] + '</a></li>');
	}
	a.push('</ul></div>');
	a.push('</div>');
	a.push('<div class="colBoxClear h10px"></div>');
	a.push('</div>');
	a.push('</div>');
	a.push('</div>');
	return a.join('');
};

/* タブ切り替え - 120125 - FD:21600
-----------------------------------------------------------------------*/
function colBoxTabBox(data){

	// id 指定がない場合、指定された id がない場合処理を終了
	if(!data['id'] || !document.getElementById(data['id'])) return false;

	// 処理を行う node を変数に
	var nodes = {};

	// colbox の親
	nodes['wrapBox'] = document.getElementById(data['id']);

	// colbox の親の子供
	nodes['wrapBoxChild'] = nodes['wrapBox'].childNodes;

	// 親の子供から class colbox だけを抜き出す
	nodes['colBox'] = [];
	for(var i = 0; i < nodes['wrapBoxChild'].length; i++){
		if(nodes['wrapBoxChild'][i].className && nodes['wrapBoxChild'][i].className.match('colBox')){
			var box = nodes['wrapBoxChild'][i].getElementsByTagName('div');
			for(var j = 0; j < box.length; j++){
				if(box[j].className && box[j].className.match('colBoxHead')){
					nodes['colBox'].push({'colBox':nodes['wrapBoxChild'][i],'colBoxHead':box[j]});
					break;
				}
			}
		}
	}

	// colbox が 0、1 の場合処理を終了
	if(nodes['colBox'].length <= 1) return false;

	// タブのラッパ作成
	var tabbox = {};
	tabbox['colBox'] = document.createElement('div');
	tabbox['colBox'].className = 'colBox colBoxTab';
	tabbox['colBoxOuter'] = document.createElement('div');
	tabbox['colBoxOuter'].className = 'colBoxOuter';
	tabbox['colBox'].appendChild(tabbox['colBoxOuter']);
	nodes['wrapBox'].insertBefore(tabbox['colBox'],nodes['wrapBox'].firstChild);

	// colBox の colBoxHead をタブのラッパに移動
	for(var i = 0; i < nodes['colBox'].length; i++){
		nodes['colBox'][i]['colBox'].className += ' colBoxTabBox';
		nodes['colBox'][i]['colBox'].title = nodes['colBox'][i]['colBoxHead'].getElementsByTagName('h2')[0].innerHTML;
		tabbox['colBoxOuter'].appendChild(nodes['colBox'][i]['colBoxHead']);

		// タブにイベント設定
		var tab = nodes['colBox'][i]['colBoxHead'];
		if(tab.addEventListener){
			tab.addEventListener('click', function(){
				colBoxTabBox.setEvent({'this':this,'box':nodes['colBox'],'type':'click'});
			}, false);
/*
			tab.addEventListener('mouseover', function(){
				colBoxTabBox.setEvent({'this':this,'box':nodes['colBox'],'type':'over'});
			}, false);
			tab.addEventListener('mouseout', function(){
				colBoxTabBox.setEvent({'this':this,'box':nodes['colBox'],'type':'out'});
			}, false);
*/
		}else if(tab.attachEvent){
			tab.attachEvent('onclick', function(){
				colBoxTabBox.setEvent({'this':event.srcElement.parentNode,'box':nodes['colBox'],'type':'click'});
			});
/*
			tab.attachEvent('onmouseover', function(){
				colBoxTabBox.setEvent({'this':event.srcElement.parentNode,'box':nodes['colBox'],'type':'over'});
			});
			tab.attachEvent('onmouseout', function(){
				colBoxTabBox.setEvent({'this':event.srcElement.parentNode,'box':nodes['colBox'],'type':'out'});
			});
*/
		}
	}

	// デフォルトアクティブタブの指定があり、colBox の length より小さければ指定されたタブをアクティブに
	if(data['active'] && data['active'] <= nodes['colBox'].length){
		colBoxTabBox.setEvent({'this':data['active'],'box':nodes['colBox']});

	// それ以外は 1 つめのタブをアクティブに
	}else{
		colBoxTabBox.setEvent({'this':null,'box':nodes['colBox']});
	}
	return true;
};
colBoxTabBox.setEvent = function(data){

	// デフォルトアクティブ設定
	if(data['this'] == null){
		data['this'] = data['box'][0]['colBoxHead'];
	}else if(isNaN(data['this']) == false){
		data['this'] = data['box'][data['this'] - 1]['colBoxHead'];
	}

	// クリック
	if(!data['type'] || data['type'] == 'click'){
		for(var i = 0; i < data['box'].length; i++){
			if(data['this'].getElementsByTagName('h2')[0].innerHTML == data['box'][i]['colBox'].title){
				data['box'][i]['colBoxHead'].className = 'colBoxHead colBoxActive';
				data['box'][i]['colBox'].style.display = 'block';
			}else{
				data['box'][i]['colBoxHead'].className = 'colBoxHead';
				data['box'][i]['colBox'].style.display = 'none';
			}
		}
		return true;

	// マウスオーバー
	}else if(data['type'] == 'over'){
		if(!data['this'].className.match('colBoxActive')) data['this'].className = 'colBoxHead colBoxHover';
		return true;

	// マウスアウト
	}else if(data['type'] == 'out'){
		if(!data['this'].className.match('colBoxActive')) data['this'].className = 'colBoxHead';
		return true;
	}
	return false;
};

/* CMS/Input Video
---------------------------------------------------------------------*/
function setVideo(param){
	if(param['type'] == 'youtube'){
		document.write(setVideo.youtube(param));
	}else if(param['type'] == 'ustream'){
		document.write(setVideo.ustream(param));
	}else if(param['type'] == 'niconico'){
		document.write(setVideo.niconico(param));
	}else if(param['type'] == 'vine'){
		document.write(setVideo.vine(param));
	}else if(param['type'] == 'vimeo'){
		document.write(setVideo.vimeo(param));
	}else if(param['type'] == 'yvp'){
		document.write(setVideo.yvp(param));
	}else if(param['type'] == 'omnibus'){
		document.write(setVideo.omnibus(param));
	}
	return true;
};
setVideo.youtube = function(param){
	var url = '';
	var html = [];
	if(location.protocol.indexOf('https') != -1){
		url = '//www.youtube-nocookie.com/embed/' + param['id'];
	}else{
		url = '//www.youtube.com/embed/' + param['id'];
	}
	html.push('<div class="cmsVideo cmsVideoYoutube" style="width:' + param['width'] + 'px;">');
	html.push('<div class="cmsVideoContents" style="height:' + param['height'] + 'px;"><iframe width="' + param['width'] + '" height="' + param['height'] + '" src="' + url + '" frameborder="0" allowfullscreen></iframe></div>');
	if(param['caption'] || param['caption'] != '') html.push('<div class="cmsVideoCaption">' + param['caption'] + '</div>');
	html.push('</div>');
	return html.join('');
};
setVideo.ustream = function(param){
	var html = [];
	html.push('<div class="cmsVideo cmsVideoUstream" style="width:' + param['width'] + 'px;">');
	html.push('<div class="cmsVideoContents" style="height:' + param['height'] + 'px;"><iframe width="' + param['width'] + '" height="' + param['height'] + '" src="http://www.ustream.tv/embed/' + param['id'] + '" scrolling="no" frameborder="0" style="border: 0px none transparent;"></iframe></div>');
	if(param['caption'] || param['caption'] != '') html.push('<div class="cmsVideoCaption">' + param['caption'] + '</div>');
	html.push('</div>');
	return html.join('');
};
setVideo.niconico = function(param){
	var html = [];

	// IE のみサムネイル表示（DOM 構築のタイミングのずれ）
	if(navigator.userAgent.indexOf('MSIE') != -1){
		html.push('<div class="cmsVideo cmsVideoNiconico" style="width:312px;">');
		html.push('<div class="cmsVideoContents" style="height:176px;"><iframe width="312" height="176" src="http://ext.nicovideo.jp/thumb/' + param['id'] + '" scrolling="no" frameborder="0"></iframe></div>');

	}else{
		html.push('<div class="cmsVideo cmsVideoNiconico" style="width:' + param['width'] + 'px;">');
		html.push('<div class="cmsVideoContents" style="height:' + param['height'] + 'px;"><script src="http://ext.nicovideo.jp/thumb_watch/' + param['id'] + '?w=' + param['width'] + '&h=' + param['height'] + '"><\/script></div>');
	}

	if(param['caption'] || param['caption'] != '') html.push('<div class="cmsVideoCaption">' + param['caption'] + '</div>');
	html.push('</div>');
	return html.join('');
};
setVideo.vine = function(param){
	var html = [];
	if(!param['width']) param['width'] = 480;
	if(!param['height']) param['height'] = 480;
	if(param['option'] == 'postcard') param['height'] = param['width'];
	if(!param['option'] || !param['option'].match(/^postcard$/)) param['option'] = 'simple';
	html.push('<div class="cmsVideo cmsVideoVine" style="width:' + param['width'] + 'px;">');
	html.push('<div class="cmsVideoContents">');
	html.push('<iframe class="vine-embed" src="https://vine.co/v/' + param['id'] + '/embed/' + param['option'] + '" width="' + param['width'] + '" height="' + param['height'] + '" frameborder="0"></iframe><script async src="//platform.vine.co/static/scripts/embed.js" charset="utf-8"><\/script>');
	html.push('</div>');
	if(param['caption'] || param['caption'] != '') html.push('<div class="cmsVideoCaption">' + param['caption'] + '</div>');
	html.push('</div>');
	return html.join('');
};
setVideo.vimeo = function(param){
	var html = [];
	if(!param['width']) param['width'] = 480;
	if(!param['height']) param['height'] = 480;
	html.push('<div class="cmsVideo cmsVideoVimeo" style="width:' + param['width'] + 'px;">');
	html.push('<div class="cmsVideoContents" style="height:' + param['height'] + 'px;">');
	html.push('<iframe src="//player.vimeo.com/video/' + param['id'] + '?title=0&amp;byline=0&amp;portrait=0&amp;badge=0" width="' + param['width'] + '" height="' + param['height'] + '" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>');
	html.push('</div>');
	if(param['caption'] || param['caption'] != '') html.push('<div class="cmsVideoCaption">' + param['caption'] + '</div>');
	html.push('</div>');
	return html.join('');
};
setVideo.yvp = function(param){
	var html = [];
	html.push('<div class="cmsVideo cmsVideoYVP" style="width:' + param['width'] + 'px;">');
	html.push('<div class="cmsVideoContents" style="height:' + param['height'] + 'px;"><script type="text/javascript" class="yvpub-player" src="http://i.yimg.jp/images/yvpub/player/js/embed.js?contentid=' + param['id'] + '&width=' + param['width'] + '&height=' + param['height'] + '&autostart=0&repeat=0&fullscreen=1"><\/script></div>');
	if(param['caption'] || param['caption'] != '') html.push('<div class="cmsVideoCaption">' + param['caption'] + '</div>');
	html.push('</div>');
	return html.join('');
};
setVideo.omnibus = function(param){
	var html = [];
	html.push('<div class="cmsVideo cmsVideoOmnibus" style="width:' + param['width'] + 'px;">');
	html.push('<div class="cmsVideoContents" style="height:' + param['height'] + 'px;"><iframe title="Necfru video player" class="necfru-player" width="' + param['width'] + '" height="' + param['height'] + '" frameborder="0" scrolling="no" allowFullScreen src="http://itmedia.necfru.jp/embed?' + param['id'] + '"></iframe></div>');
	if(param['caption'] || param['caption'] != '') html.push('<div class="cmsVideoCaption">' + param['caption'] + '</div>');
	html.push('</div>');
	return html.join('');
};

/* CMS/Input Embedded Tweets - 13/03/01 RD:12360 - 13/03/29 FD:27485
---------------------------------------------------------------------*/
function embeddedTweets(url,align,width,caption){
	if(!align) var align = 'left';
	if(!width) var width = 300;
	// min-width = 220
	switch(align){
		case 'center': document.write('<div class="cmsEmbeddedTweets" style="width:' + width + 'px;margin:0 auto 20px;">');break;
		case 'left': document.write('<div class="cmsEmbeddedTweets" style="width:' + width + 'px;margin:0 10px 20px 0;float:' + align + ';">');break;
		case 'right': document.write('<div class="cmsEmbeddedTweets" style="width:' + width + 'px;margin:0 0 20px 10px;float:' + align + ';">');break;
		default: document.write('<div class="cmsEmbeddedTweets" style="width:' + width + 'px;margin:0 auto 20px;">');
	}
	document.write('<blockquote class="twitter-tweet" lang="ja" width="' + width + '"><a href="' + url + '"></a></blockquote>');
	document.write('<script async src="//platform.twitter.com/widgets.js" charset="utf-8"><\/script>');
	if(caption) document.write('<div class="cmsEmbeddedTweetsCaption">' + caption + '</div>');
	document.write('</div>');
	return true;
};

/* CMS/Input Embedded Posts - 13/09/11 RD:17263 - 13/09/11 FD:29881
---------------------------------------------------------------------*/
function embeddedFBposts(url,caption){
	document.write('<div class="cmsEmbeddedFBposts" style="width:552px;margin:0 auto 20px;">');
	document.write('<div class="fb-post" data-href="' + url +'" data-width="550"></div>');
	if(caption) document.write('<div class="cmsEmbeddedFBpostsCaption">' + caption + '</div>');
	document.write('</div>');
	return true;
};

/* CMS/Input embeddedInstagram - 14/05/12 RD:23110 - 14/05/12 FD:33428
---------------------------------------------------------------------*/
function embeddedInstagram(url,align,width,height,caption){
	if(!url) return false;
	var html = '';
	var emburl = url.replace(/^https?:/,'') + 'embed/';
	if(!align) var align = 'left';
	if(!width) var width = 480;
	if(!height) var height = (width-0)+100;
	switch(align){
		case 'center': html += '<div class="cmsEmbeddedInstagram" style="width:' + width + 'px;margin:0 auto 20px;">'; break;
		case 'left': html += '<div class="cmsEmbeddedInstagram" style="width:' + width + 'px;margin:0 10px 20px 0;float:' + align + ';">'; break;
		case 'right': html += '<div class="cmsEmbeddedInstagram" style="width:' + width + 'px;margin:0 0 20px 10px;float:' + align + ';">'; break;
		default: html += '<div class="cmsEmbeddedInstagram" style="width:' + width + 'px;margin:0 auto 20px;">';
	}
	html += '<iframe src="' + emburl + '" width="' + width + '" height="' + height + '" frameborder="0" scrolling="no" allowtransparency="true" style="margin-bottom:5px;"></iframe>';
	if(caption) html += '<div class="cmsEmbeddedInstagramCaption">' + caption + '</div>';
	html += '</div>';
	document.write(html);
	return true;
};

/* CMS/Input embeddedPixiv - 14/12/02 RD:28086 - 14/12/02 FD:36236
---------------------------------------------------------------------*/
function embeddedPixiv(id,size,border){
	if(!id) return false;
	// default（値が適切でない場合もこの値を適用）
	if(!size||!size.match(/^large$|^medium$|^small$/)) size = 'medium';
	if(!border||!border.match(/^on$|^off$/)) border = 'off';
	var width;
	// 対象外の項目を置き換え
	switch(border) {
		case 'on':
			if(size=='large') size = 'medium'; // border 付きの large は 600px を超えるので medium に置き換え。
			break;
		default:
			break;
	}
	var target_id = embeddedPixiv.setTargetID(id,8);
	document.write('<div class="cmsEmbeddedPixiv"><div id="' + target_id + '"></div></div>');
	var options = {
		id    : id,
		size  : size,
		border: border,
		done  : 0
	};
	var __pixiv__ = window.__pixiv__ = window.__pixiv__ || {};
	embeddedPixiv.load(options,target_id);
};
embeddedPixiv.setTargetID = function(id,n) {
	// 同ページ内に同じidで複数記述した場合に上書きされないよう末尾にランダムな文字列を付与
	var CODE_TABLE = '0123456789' + 'ABCDEFGHIJKLMNOPQRSTUVWXYZ' + 'abcdefghijklmnopqrstuvwxyz';
	var target_id = "";
	for (var i = 0, k = CODE_TABLE.length; i < n; i++) {
		target_id += CODE_TABLE.charAt(Math.floor(k * Math.random()));
	}
	target_id = 'cmsEmbeddedPixiv-' + id + '-' + target_id;
	return target_id;
}
embeddedPixiv.load = function(options,target_id) {
	var target = document.getElementById(target_id);
	if (target && options && options.done != 1) {
		options.border == 'on' ?
			embeddedPixiv.showEmbedBorder(target, options) :
			embeddedPixiv.showEmbed(target, options);
	}
};
embeddedPixiv.showEmbedBorder = function(target, options) {
	var iframe, size, id = embeddedPixiv.makeId(),
		sizes = {
			large : [670, 550],
			medium: [360, 300],
			small : [190, 250]
		};
	iframe = document.createElement('iframe');
	iframe.id = id;
	iframe.name = id;
	iframe.src = 'http://embed.pixiv.net' + '/embed_mk2.php?' + embeddedPixiv.param(options);
	size = sizes[options.size];
	iframe.width = size[0] + 30;
	iframe.height = size[1];
	iframe.frameBorder = 0;
	iframe.style.border = 'none';
	target.appendChild(iframe);
	target.setAttribute('data-done', 1);
	target.style.width = iframe.width + 'px';
	target.style.margin = '0 auto 20px';
	iframe.contentWindow.name = id; // IE7
};
embeddedPixiv.showEmbed = function(target, options) {
	var script = document.createElement('script'),
		id = embeddedPixiv.makeId(),
		q = {
			callback : '__pixiv__["' + id + '"]',
			id       : options.id,
			size     : options.size
		};
	script.src = 'http://embed.pixiv.net' + '/embed_json.php?' + embeddedPixiv.param(q);
	script.charset = 'utf-8';
	document.getElementsByTagName('head')[0].appendChild(script);
	target.setAttribute('data-done', 1);
	__pixiv__[id] = function(data) {
		var url = 'http://embed.pixiv.net' + '/embed_mk2.php?' + embeddedPixiv.param(options),
			width = data.img_w + 10;
		target.innerHTML = [
			'<p class="pixiv-embed-illust" style="width:', width, 'px;margin:0 0 5px;padding:5px 0;text-align:center;background-color:#dde6ee;">',
			'<iframe src="', url, '" width="', data.img_w, '" height="', data.img_h, '" frameborder="0" style="vertical-align:middle; border:none;"></iframe>',
			'</p>',
			'<p class="pixiv-embed-title" style="margin:0;font-size:13px;line-height:18px;">',
			'<a href="http://www.pixiv.net/member_illust.php?mode=medium&amp;illust_id=', data.img_id, '" target="_blank">',
			embeddedPixiv.escapeHTML(data.title),
			'</a>',
			'</p>',
			'<p class="pixiv-embed-meta" style="margin:0;font-size:12px;">',
			'<span class="pixiv-embed-author">',
			'by <a href="http://www.pixiv.net/member.php?id=', data.user_id, '" target="_blank">',
			embeddedPixiv.escapeHTML(data.user),
			'</a> ',
			'</span>',
			'<span class="pixiv-embed-datetime">', embeddedPixiv.escapeHTML(data.update), '</span> ',
			'on <a href="http://www.pixiv.net/" target="_blank">pixiv</a>',
			'</p>'
		].join('');
		target.style.width = width + 'px';
		target.style.margin = '0 auto 20px';
		delete __pixiv__[id];
	};
}
embeddedPixiv.makeId = function() {
	return 'pixiv-embed-' + Math.random();
};
embeddedPixiv.param = function(queries) {
	var k, ret = [];
	for (k in queries) {
		ret.push(k + '=' + queries[k]);
	}
	return ret.join('&');
};
embeddedPixiv.escapeHTML = function(str) {
	return str.toString().replace(/&/g, '&amp;').replace(/"/g, '&quot;').replace(/</g, '&lt;').replace(/>/g, '&gt;'); // &"<>を置換
};
