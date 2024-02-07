/* SiteCatalyst 更新日時 id = update 内の不要な記号を取り除く - 2011/11/01
   パターンが 2 世代存在するため両方に対応する
   1) <div class="inner" id="update"><span class="arrow">&raquo;</span> YYYY年MM月DD日 HH時SS分 UPDATE</div>
   2) <div class="inner"><span class="arrow">&raquo;</span> <span id="update">YYYY年MM月DD日 HH時SS分 UPDATE</span></div>
 結果 <div class="inner"><span class="arrow">&raquo;</span> <span id="update">YYYY年MM月DD日 HH時SS分 更新</span></div>
--------------------------------------------------------*/
(function(){
	if(!document.getElementById('cmsDate')) return false;
	if(!document.getElementById('update')) return false;
	var update = document.getElementById('update');
	if(update.tagName == 'DIV') update.setAttribute('id','');
	var a = update.innerHTML;
	var b = a.replace(/span> /i,'span> <span id="update">').replace('UPDATE','更新</span>');
	update.innerHTML = b;
	return true;
})();

/* cmsBody 内 code 書式 google-code-prettify replace - 2012/12/25
   head 内に
   /js/lib/prettify.css
   /js/lib/prettify.js
   を設置
--------------------------------------------------------*/
(function(){
	if(!document.getElementById('cmsBody')) return false;
	var cmsBody = document.getElementById('cmsBody');
	var pre = cmsBody.getElementsByTagName('pre');
	if(pre.length == 0) return false;
	prettyPrint();
	return true;
})();

/* アクセストップ10 リストに class をつける - 2011/11/07
--------------------------------------------------------*/
(function(){
	if(masterType() != 'article') return false;
	if(!document.getElementById('cmsBody')) return false;
	var cmsBody = document.getElementById('cmsBody');
	var divtag = cmsBody.getElementsByTagName('div');
	for(var i = 0; i < divtag.length; i++){
		if(divtag[i].className.match('colBoxAccessTop10')){
			var li = divtag[i].getElementsByTagName('li');
			for(var j = 0; j < li.length; j++){
				li[j].className = 'rank' + (j + 1);
			}
		}
	}
	return true;
})();

/* News , Enterprise , Executive 再出力暫定対応 2011/04/21 ～ 2011/10/01 まで
--------------------------------------------------------*/
(function(){
	if(!document.domain.match('preview')) return false;
	if(!location.pathname.match(/\/.+\/remake\//)) return false;
	if(!document.getElementById('cmsBody')) return false;
	var a = document.getElementById('cmsBody');
	if(!a.getElementsByTagName('img')[0]) return false;
	var b = a.getElementsByTagName('img');
/*	var c = location.pathname.replace('remake/','').replace(/news\d\d\d(\_\d)?\.html/,'');*/
	for(var i = 0; i < b.length; i++){
		b[i].src = b[i].src.replace('remake/','');
	}
	return true;
})();

/* $ICON がない場合ラップしてる colBoxIcon を削除
--------------------------------------------------------*/
(function(){
var a = document.getElementsByTagName('div');
for(var i = 0; i < a.length; i++){
	if(a[i].className == 'colBoxIcon' && !a[i].getElementsByTagName('img')[0]) a[i].parentNode.removeChild(a[i]);
}
return true;
})();

/* /$CHANNEL$/subtop/archive/ カレンダーアクティブ処理
                              カレンダー数取得＆CLASS セット
--------------------------------------------------------*/
(function(){
if(!location.pathname.match(/^\/.+\/subtop\/archive\//)) return false; // バックナンバーでなければなにもしない
var a = document.getElementById('masterMain');
var b = a.getElementsByTagName('div');
var len = 0;
for(var i = 0; i < b.length; i++){
	if(b[i].className.match('colBoxCalendar')){
		b[i].className += ' colBoxCalendar' + len++;
		var c = b[i].getElementsByTagName('li');
		for(var j = 0; j < c.length; j++){
			if(c[j].getElementsByTagName('a')[0].href.match(location.pathname)){
				c[j].className = 'colBoxActive';
				break;
			}
		}
	}
}
return true;
})();

/* PR クレジット表示 PR or special でなければ非表示
--------------------------------------------------------*/
(function(){
	if(!document.getElementById('cmsPrcredit')) return false;
	var a = document.getElementById('cmsPrcredit');
	if(!a.getElementsByTagName('div')[0]) return false;
	var b = a.getElementsByTagName('div')[0];
	if(b.innerHTML == '') a.style.display = 'none';
	return true;
})();

/* 記事面 box 内に画像がある場合は画像の width を取得し box width に代入する（box 幅をぴったりに）
--------------------------------------------------------*/
(function(){
	if(!document.getElementById('cmsBody')) return false;
	var a = document.getElementById('cmsBody');
	var b = a.getElementsByTagName('div');
	for(var i = 0; i < b.length; i++){
		if(b[i].id.match(/^col\d\d\d/)){
			if(!b[i].getElementsByTagName('img')[0]) continue;
			var c = b[i].getElementsByTagName('img');
			var d = 0;
			for(var j = 0; j < c.length; j++){
				if(!c[j].width) continue;
				if(j == c.length - 1){
					c[j].style.marginRight = '0px';
					d += c[j].width + 6;
				}else{
					d += c[j].width + 9;
				}
			}
			if(d == 0) continue;
			b[i].style.width = d + 'px';
		}
	}
	return true;
})();

/* 記事面透明 box 内に box がある場合は box の width を取得し trBox width に代入する（透明 box 幅をぴったりに） RD:7823
--------------------------------------------------------*/
(function(){
	if(masterChannel() == 'makoto' || masterChannel() == 'bizid') return false;
	if(!document.getElementById('cmsBody')) return false;
	var a = document.getElementById('cmsBody');
	var b = a.getElementsByTagName('div');
	for(var i = 0; i < b.length; i++){
		if(b[i].className.match(/^trBoxWidth/)){

			// 透明 BOX の親が cmsBox だったら処理を行わない
			// if(b[i].parentNode.parentNode.className.match('cmsBox')) continue;

			// 透明 BOX の幅が％指定だったら処理を行わない
			if(b[i].style.width.match('%')) continue;

			var c = b[i].childNodes;
			var d = [];
			for(var j = 0; j < c.length; j++){
				if(c[j].id && c[j].id.match(/^col\d\d\d/)) d.push(c[j]);
			}
			if(d.length == 0) return false;
			var e = [];
			for(var j = 0; j < d.length; j++){
				if(d[j].style.width) e.push(d[j].style.width);
			}
			if(d.length != e.length) return false;
			var f = 0;
			var g = 0;
			for(var j = 0; j < e.length; j++){
				if(j == e.length - 1) d[j].style.marginRight = 0;
				f += parseInt(e[j].replace('px'));
			}
			g = 10 * (e.length - 1);
			b[i].style.width = (f + g) + 'px';
		}
	}
	return true;
})();

/* 記事面 box 内の br clear all が余計な余白をつけて邪魔なので削除
--------------------------------------------------------*/
(function(){
	if(!document.getElementById('cmsBody')) return false;
	var a = document.getElementById('cmsBody');
	var b = a.getElementsByTagName('div');
	var c = [];
	for(var i = 0; i < b.length; i++){
		if(b[i].className.match('cmsBox')){
			if(b[i].lastChild.tagName == 'SPAN') b[i].removeChild(b[i].lastChild,b[i]);
			if(b[i].lastChild.tagName == 'BR') b[i].removeChild(b[i].lastChild,b[i]);
			c[i] = document.createElement('div');
			c[i].className = 'colBoxClear';
			b[i].appendChild(c[i]);
		}
	}
	return true;
})();

/* MONOist 暫定処置 (1) - 記事上バナーの処理 - 110215
--------------------------------------------------------*/
(function(){
	if(!getUrl()['path'].match(/^\/mn\/articles\//)) return false;
	if(!document.getElementById('cmsBody')) return false;
	var a = document.getElementById('cmsBody').getElementsByTagName('div')[0];
	var b = a.getElementsByTagName('img');
	var flag = false;
	for(var i = 0; i < b.length; i++){
		if(b[i].src.match('/mn/articles/1102/15/mn_ebook_banner.gif')){
			var c = b[i];
			var flag = true;
			break;
		}
	}
	if(flag == false) return false;
	var d = c.parentNode.parentNode;
	var div = document.createElement('div');
	div.className = 'colBoxClearLeft';
	a.insertBefore(div,d.nextSibling);
	return true;
})();

/* 記事面 AMAZON-ITEM 幅調整 - 110404
--------------------------------------------------------*/
// 120718 同一ページ複数対応
(function(){
	if(!document.getElementById('cmsBody')) return false;
	if(!document.getElementById('amazon-item')) return false;
	var cmsBody = document.getElementById('cmsBody');
	var div = cmsBody.getElementsByTagName('div');
	for(var i = 0; i < div.length; i++){
		if(div[i].getAttribute('id') == 'amazon-item'){
			var a = div[i];
			var b = a.getElementsByTagName('table')[0];
			var c = b.getElementsByTagName('tbody')[0];
			var d = c.getElementsByTagName('tr')[0];
			var e = d.getElementsByTagName('td');
			if(e.length == 3) return false;
			if(e.length == 2) a.style.width = '66%';
			if(e.length == 1) a.style.width = '33%';
		}
	}
	return true;
})();

/* バックナンバー（/$CHANNEL$/subtop/archive/）記事タイプソート - 110318
--------------------------------------------------------*/
(function(){
	if(!getUrl()['path'].match(/^\/.+\/subtop\/archive\//)) return false;
	if(!document.getElementById('masterMain')) return false;
	var a = document.getElementById('masterMain');
	var b = a.getElementsByTagName('div');
	var c = [];
	for(var i = 0; i < b.length; i++){
		if(b[i].className.match('colBoxBacknumber')){
			c.push(b[i]);
			b[i].innerHTML = b[i].innerHTML.replace(/\n/g,'');
			break;
		}
	}
	if(c.length == 0) return false;
	var d = c[0].getElementsByTagName('li');
	var e = [];
	for(var i = 0; i < d.length; i++){
		var f = d[i].getElementsByTagName('span')[0];
		e.push(f.innerHTML);
		for(var j = 0; j < i; j++){
			if(f.innerHTML == d[j].getElementsByTagName('span')[0].innerHTML){
				e.pop();
				break;
			}
		}
	}
	var div = document.createElement('div');
	div.className = 'colBox colBoxButtons';
	var btn = [];
	btn.push('<li onClick="archiveArticleSort(this,\'全て\');" class="active">全て</li>');
	for(var i = 0; i < e.length; i++){
		btn.push('<li onClick="archiveArticleSort(this,\'' + e[i] + '\');">' + e[i] + '</li>');
	}
	var html = [];
	html.push('<div class="colBoxOuter">');
	html.push('<div class="colBoxInner">');
	html.push('<div class="colBoxIndex">');
	html.push('<div class="colBoxUlist"><ul>');
	html.push(btn.join(''));
	html.push('</ul></div>');
	html.push('<div class="colBoxClear"></div></div>');
	html.push('</div>');
	html.push('</div>');
	div.innerHTML = html.join('');
	a.insertBefore(div,c[0]);
	return true;
})();
function archiveArticleSort(elem,str){
	if(!getUrl()['path'].match(/^\/.+\/subtop\/archive\//)) return false;
	if(!document.getElementById('masterMain')) return false;
	var buttons = elem.parentNode.getElementsByTagName('li');
	for(var i = 0; i < buttons.length; i++){
		if(buttons[i].innerHTML == elem.innerHTML){
			buttons[i].className = 'active';
		}else{
			buttons[i].className = '';
		}
	}
	var a = document.getElementById('masterMain');
	var b = a.getElementsByTagName('div');
	var c = [];
	for(var i = 0; i < b.length; i++){
		if(b[i].className.match('colBoxBacknumber')){
			c.push(b[i]);
			break;
		}
	}
	if(c.length == 0) return false;
	var d = c[0].getElementsByTagName('li');
	var e = [];
	if(str == '全て'){
		for(var i = 0; i < d.length; i++){
			var f = d[i].getElementsByTagName('span')[0];
			f.parentNode.parentNode.parentNode.parentNode.previousSibling.style.display = '';
			f.parentNode.parentNode.parentNode.nextSibling.style.display = '';
			f.parentNode.style.display = '';
			if(d[i].getElementsByTagName('span')[2]){
				var g = d[i].getElementsByTagName('span')[2];
				g.style.display = 'none';
			}
		}
		return true;
	}else{
		for(var i = 0; i < d.length; i++){
			var f = d[i].getElementsByTagName('span')[0];
			f.parentNode.parentNode.parentNode.parentNode.previousSibling.style.display = 'none';
			f.parentNode.parentNode.parentNode.nextSibling.style.display = 'none';
			if(d[i].getElementsByTagName('span')[2]){
				var g = d[i].getElementsByTagName('span')[2];
				g.style.display = 'inline';
			}
			if(f.innerHTML == str){
				f.parentNode.style.display = 'block';
			}else{
				f.parentNode.style.display = 'none';
			}
		}
		return true;
	}
}

/* サブトップタイトルエリア移動 - 11/07/07
--------------------------------------------------------*/
(function(){
	if(masterType() != 'subtop') return false;
	if(!document.getElementById('subtopTitleRemove')) return false;
	if(!document.getElementById('subtopTitle')) return false;
	if(!document.getElementById('masterMainRight')) return false;
	if(document.getElementById('subtopDescription')){
		document.getElementById('masterMainRight').insertBefore(document.getElementById('subtopDescription'),document.getElementById('masterMainRight').childNodes[0]);	
	}
	document.getElementById('masterMainRight').insertBefore(document.getElementById('subtopTitle'),document.getElementById('masterMainRight').childNodes[0]);	
	return true;
})();

/* 連載アラート 記事インライン - 11/07/14
--------------------------------------------------------*/
(function(){
	var targetWrap = (function(){
		if(document.getElementById('cmsBody')) return document.getElementById('cmsBody');
		return document.body;
	})();

	var div = targetWrap.getElementsByTagName('div');
	var target = [];
	for(var i = 0; i < div.length; i++){
		if(div[i].className == 'cmsBodyItmidSeriesButtonRegist') target.push(div[i]);
	}

	if(target.length == 0) return false;

	var site = location.pathname.split('/')[1];
	var sctag = 'var s = s_gi(ThisSite); ';
	sctag += 's.prop14 = \'' + site + '_ITMID_serialtext\'; ';
	sctag += 's.events = \'event20\'; ';
	sctag += 's.eVar17 = \'' + site + '_ITMID_serialtext\'; ';
	sctag += 's.tl(this,\'o\',\'' + site + '_ITMID_serialtext\')';
	var template = '<a href="javascript:void(0);" onClick="document.getElementById(\'ITMIDalert2\').click();">この連載を「連載記事アラート」に登録する</a> <span class="colBoxNewtxt">New</span>';
//	var template = '<a href="javascript:void(0);" onClick="document.getElementById(\'ITMIDalert2\').click();' + sctag + '">この連載を「連載記事アラート」に登録する</a> <span class="colBoxNewtxt">New</span>';
	for(var i=  0; i < target.length; i++){
		target[i].innerHTML = template;
	}
//	jQuery(document).ready(function(){
//		designCnt('ITMID_serialtext_show');
//	});
})();

/* localNavigation アクティブ化 - 11/10/26
--------------------------------------------------------*/
(function(){
if(masterType() == 'top') return false;
if(!document.getElementById('localNavigation')) return false;

// PATH 完全一致フラグ
var exactmatch = getUrl()['path_cutparam'];

// location.pathname を / で split
var primaryFlag = getUrl()['path_cutparam'].split('/');
if(primaryFlag.length <= 4) return false;

// 配列の最後はファイル名になる
// ナビゲーションのリンクと一致させるためファイル名は不要
// ディレクトリ以下は active にする
primaryFlag[primaryFlag.length - 1] = '';
primaryFlag = primaryFlag.join('/');
var secondaryFlag = '';
if(getMETA('keywords') == false || getMETA('keywords') == ''){
	secondaryFlag = '';
}else if(masterType() != 'article'){
	secondaryFlag = '';
}else if(masterChannel() == 'mn'){
	secondaryFlag = getMETA('keywords').content.replace('カーエレクトロニクス','オートモーティブ').replace('生産管理','製造マネジメント').replace('Factory Automation','FA');
}else if(masterChannel() == 'ait'){
	var key = getMETA('keywords').content.split(',');
	var keylength = key.length - 1;
	secondaryFlag = key[keylength].replace('Windows Server Insider','Windows').replace('Insider.NET','.NET').replace('System Insider','System').replace('Server ＆ Storage','Server＆Storage').replace('HTML5 ＋ UX','HTML5＋UX').replace('Smart ＆ Social','Smart＆Social').replace('Java Agile','Java').replace('Database Expert','DB').replace('Linux ＆ OSS','Linux').replace('Master of IP Network','IP Network').replace('Security ＆ Trust','Security');
}else{
	secondaryFlag = getMETA('keywords').content;
}

// KEYWORDS 完全一致フラグ
var exactmatch2 = secondaryFlag.split(',');

var navid = ['lnavMain','lnavSub','lnavSubSub'];
for(var i = 0; i < navid.length; i++){
	if(document.getElementById(navid[i])){
		var lnav = document.getElementById(navid[i]);
		var lnavBtn = lnav.childNodes;
		for(var j = 0; j < lnavBtn.length; j++){
			if(lnavBtn[j].tagName != 'DIV') continue;
			if(!lnavBtn[j].getElementsByTagName('a')[0]) continue;
			var a = lnavBtn[j].getElementsByTagName('a')[0];
			var b = a.getAttribute('href');
			if(b.match(/#$/)) b = 'IE';

			// PATH とナビゲーション href が完全一致の場合
			if(b == exactmatch) lnavBtn[j].className += ' lnavExactmatch';

			// KEYWORDS とナビゲーション文字列が完全一致の場合
			for(var k = 0; k < exactmatch2.length; k++){
				if(a.innerHTML == exactmatch2[k]){
					lnavBtn[j].className += ' lnavExactmatch2';
				}
			}

			if(b.match(primaryFlag) || secondaryFlag.match(a.innerHTML)){
				lnavBtn[j].className += ' lnavBtnActive';
			}
		}
	}
}
return true;
})();

/* アドタグない場合広告表示エリア確認のため空 div を生成 PREVIEW のみ動作 - 11/07/15
--------------------------------------------------------*/
(function(){
if(!document.domain.match(/preview|localhost/)) return false;
if(document.getElementById('ITN')){
	if(document.getElementById('ITN').innerHTML.match(/\[an error occurred while processing this directive\]/)){
		document.getElementById('ITN').innerHTML = '<div style="width:728px;height:90px;background:#666;color:#FFF;font-size:12px;">' + masterChannel() + 'のposition:ITNが未作成です</div>';
	}
}
if(document.getElementById('ITR')){
	if(document.getElementById('ITR').innerHTML.match(/\[an error occurred while processing this directive\]/)){
		document.getElementById('ITR').innerHTML = '<div style="width:300px;height:300px;background:#666;color:#FFF;font-size:12px;">' + masterChannel() + 'のposition:ITRが未作成です</div>';
	}
}
if(document.getElementById('ITSQ')){
	if(document.getElementById('ITSQ').innerHTML.match(/\[an error occurred while processing this directive\]/)){
		document.getElementById('ITSQ').innerHTML = '<div style="width:200px;height:200px;background:#666;color:#FFF;font-size:12px;">' + masterChannel() + 'のposition:ITSQが未作成です</div>';
	}
}
return true;
})();
