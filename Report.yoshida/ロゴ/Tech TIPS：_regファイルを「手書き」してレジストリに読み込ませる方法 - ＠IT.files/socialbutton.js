// INCLUDE Facebook SDK
// INCLUDE Google +1 SDK
// INCLUDE /css/socialbutton.css
// INCLUDE /js/base.js
function msbBtn(d){
	msbBtn.setting(d);
	msbBtn.render(d);
	return true;
};
/*--------------------------------------------------------------------------------------*/
// SETTING
/*--------------------------------------------------------------------------------------*/
msbBtn.setting = function(d){

	// STYLE（CLASS を /css/socialbutton.css で定義）
	if(d['base_class'] == 'float'){
		this.base_class = 'masterSocialbuttonFloat';
	}else{
		this.base_class = 'masterSocialbuttonDefault';
	}

	// 設置位置（ID／CLASS／CLICK COUNT に使用）
	if(!d['pos']){
		this.pos = 'top';
	}else{
		this.pos = d['pos'];
	}

	// ポップアップ位置（CLASS を /css/socialbutton.css で定義）
	if(d['popup_pos'] == 'upper'){
		this.popup_pos = 'msbGroupUpper';
	}else{
		this.popup_pos = 'msbGroupUnder';
	}

	// 背景色 CLASS 指定（CLASS を /css/socialbutton.css で定義）
	if(d['base_color'] == 'black'){
		this.base_color = 'masterSocialbuttonBlack';
	}else{
		this.base_color = 'masterSocialbuttonWhite';
	}

	// TABLE OR DIV（出力タグ選択）
	if(d['tag'] == 'table'){
		this.tag = ['table','tr','td'];
	}else{
		this.tag = ['div','div','div'];
	}

	return true;
};
/*--------------------------------------------------------------------------------------*/
// RENDERING
/*--------------------------------------------------------------------------------------*/
msbBtn.render = function(d){
//	var name = ''; // ボタン名
//	var id = ''; // ボタン ID
	var data = ''; // ボタングループ
	var line = d['line']; // ボタン行数
	var btn = '';
	var html = []; // HTML

	// HEADER
	html.push(msbBtn.header());

	// LOOP LEVEL 1（行数）
	for(var i = 0; i < line.length; i++){
		if(line[i].length == 0) continue;

		// TABLE OR DIV
		html.push(msbBtn.inheader(i));

		// LOOP LEVEL 2（ボタン数）
		for(var j = 0; j < line[i].length; j++){
			if(!line[i][j]['id']) continue;
//			name = line[i][j]['name'];
//			id = line[i][j]['id'];

			// ボタン内にボタンがネストされている
			if(line[i][j]['data']){
				data = line[i][j]['data'];

				// ネストボタンブロック HEADER
				html.push(msbBtn.groupheader(line[i][j]['name'],line[i][j]['id']));

				// LOOP LEVEL 3（ネストボタン数）
				for(var k = 0; k < data.length; k++){
					if(!data[k]['id']) continue;
					btn = msbBtn[data[k]['id']](data[k]);
					html.push(msbBtn.buttonheader(data[k]['id'],'div')); // BUTTON HEADER
					html.push(btn); // BUTTON TEMPLATE
					html.push(msbBtn.buttonfooter('div')); // BUTTON FOOTER
				}

				// ネストボタンブロック FOOTER
				html.push(msbBtn.groupfooter());

			// ボタン内にボタンがネストされていない
			}else{
				btn = msbBtn[line[i][j]['id']](line[i][j]);
				html.push(msbBtn.buttonheader(line[i][j]['id'])); // BUTTON HEADER
				html.push(btn); // BUTTON TEMPLATE
				html.push(msbBtn.buttonfooter()); //BUTTON FOOTER
			}
		}	

		// /TABLE OR /DIV
		html.push(msbBtn.infooter());
	}	

	// FOOTER
	html.push(msbBtn.footer());
	document.write(html.join(''));
	return true;
};
/*--------------------------------------------------------------------------------------*/
// TEMPLATE
/*--------------------------------------------------------------------------------------*/
msbBtn.header = function(){
	var pos = msbBtn.str1stup(this.pos);
	return '<div id="masterSocialbutton' + pos + '" class="masterSocialbutton ' + this.base_color + ' ' + this.base_class + '"><div class="msbOut" id="msb' + pos + 'Out">\n';
};
msbBtn.footer = function(){
	return '</div></div>\n';
};
msbBtn.inheader = function(line){
	var pos = msbBtn.str1stup(this.pos);
	return '<' + this.tag[0] + ' class="msbIn msbIn' + line + '" id="msb' + pos + 'In"><' + this.tag[1] + '>\n';
};
msbBtn.infooter = function(){
	return '</' + this.tag[1] + '></' + this.tag[0] + '>\n';
};
msbBtn.groupheader = function(name,id){
	id = msbBtn.str1stup(id);
	var html = [];
	html.push('<' + this.tag[2] + ' class="msbGroup ' + this.popup_pos + '"><div class="msbGroupOut">\n');
	html.push('<div class="msbBtn msbBtn' + id + '" onMouseOver="msbBtn.group_block(this.parentNode.getElementsByTagName(\'div\')[1]);" onMouseOut="msbBtn.group_none(this.parentNode.getElementsByTagName(\'div\')[1]);"><a href="javascript:void(0);" class="msbBtntext">' + name + '</a></div>\n');
	html.push('<div class="msbGroupIn" onMouseOver="msbBtn.group_block(this);" onMouseOut="msbBtn.group_none(this);">\n');
//	html.push('<div class="msbBtn msbBtn' + id + '" onMouseOver="this.parentNode.getElementsByTagName(\'div\')[1].style.display = \'block\';" onMouseOut="this.parentNode.getElementsByTagName(\'div\')[1].style.display = \'none\';"><a href="javascript:void(0);" class="msbBtntext">' + name + '</a></div>\n');
//	html.push('<div class="msbGroupIn" onMouseOver="this.style.display = \'block\';" onMouseOut="this.style.display = \'none\';">\n');
	return html.join('');
};
msbBtn.groupfooter = function(){
	return '</div></div></' + this.tag[2] + '>\n';
};
msbBtn.buttonheader = function(name,nest){
	var tag = '';
	var pos = msbBtn.str1stup(this.pos);
	name = msbBtn.str1stup(name);
	if(nest){ // ポップアップ内にネストされている場合
		tag = 'div'; // ポップアップ内は必ず div 
	}else{
		tag = this.tag[2]; // 引数で指定されたタグを使用
	}
	return '<' + tag + ' class="msbBtn msbBtn' + name + '" id="msbBtn' + name + pos + '">\n';
};
msbBtn.buttonfooter = function(nest){
	var tag = '';
	if(nest){
		tag = 'div';
	}else{
		tag = this.tag[2];
	}
	return '</' + tag + '>\n';
};
/*--------------------------------------------------------------------------------------*/
// BUTTON TEMPLATE
/*--------------------------------------------------------------------------------------*/
// 未定義
msbBtn.notfound = function(data){
	var html = '<span style="color:#C00;">未定義</span>';
	return html;
};

// Google ブックマーク
msbBtn.googlebookmark = function(data){
	var name = data['name'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = data['name'];
	}
	var url = 'http://www.google.com/bookmarks/mark?op=edit&bkmk=' + thisDomain() + getUrl()['path_cutpageing_cutparam'] + '&title=' + document.title;
	var evt = '(function(){var btn = \'' + name + '\';var a = location.pathname.split(\'/\')[1] + \'_共通ブクマ_\' + btn + \' pos=' + this.pos + '\';s.prop14 = a;s.tl(this,\'o\',a);s_objectid = a;return true;})();';
	var html = '<a href="' + url + '" target="_blank" title="' + title + '" onClick="' + evt + '" class="msbBtntext"><span>' + name + '</span></a>';
	return html;
};

// Yahoo ブックマーク
msbBtn.yahoobookmark = function(data){
	var name = data['name'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = data['name'];
	}
	var url = 'javascript:void window.open(\'http://bookmarks.yahoo.co.jp/bookmarklet/showpopup?t=' + document.title + '&u=' + encodeURIComponent(thisDomain() + getUrl()['path_cutpageing_cutparam']) + '&opener=bm&ei=UTF-8\',\'popup\',\'width=550px,height=480px,status=1,location=0,resizable=1,scrollbars=0,left=100,top=50\',0);';
	var evt = '(function(){var btn = \'' + name + '\';var a = location.pathname.split(\'/\')[1] + \'_共通ブクマ_\' + btn + \' pos=' + this.pos + '\';s.prop14 = a;s.tl(this,\'o\',a);s_objectid = a;return true;})();';
	var html = '<a href="' + url + '" title="' + title + '" onClick="' + evt + '" class="msbBtntext"><span>' + name + '</span></a>';
	return html;
};

// はてなブックマークボタン（テキスト）
msbBtn.hatenabookmark = function(data){
	var name = data['name'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = data['name'];
	}
	var url = 'http://b.hatena.ne.jp/entry/' + encodeURIComponent(thisDomain() + getUrl()['path_cutpageing_cutparam']);
	var js = 'http://b.st-hatena.com/js/bookmark_button.js';
	var evt = '(function(){var btn = \'はてブ\';var a = location.pathname.split(\'/\')[1] + \'_共通ブクマ_\' + btn + \' pos=' + this.pos + '\';s.prop14 = a;s.tl(this,\'o\',a);s_objectid = a;return true;})();';
	var html = '<a href="' + url + '" class="hatena-bookmark-button msbBtntext" data-hatena-bookmark-title="' + document.title + '" data-hatena-bookmark-layout="simple" title="' + title + '" data-hatena-bookmark-mode="popup" onClick="' + evt + '"><span>' + name + '</span></a><script type="text/javascript" src="' + js + '" charset="utf-8" async="async"><\/script>';
	return html;
};

// はてなブックマークボタン（カウンター付）
msbBtn.hatenabookmarkC = function(data){
	var name = data['name'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = data['name'];
	}
	var url = 'http://b.hatena.ne.jp/entry/' + encodeURIComponent(thisDomain() + getUrl()['path_cutpageing_cutparam']);
	var img = 'http://b.st-hatena.com/images/entry-button/button-only.gif';
	var js = 'http://b.st-hatena.com/js/bookmark_button.js';
	var html = '<a href="' + url + '" class="hatena-bookmark-button" data-hatena-bookmark-layout="standard" title="' + title + '"><img src="' + img + '" alt="' + name + '" width="20" height="20" style="border: none;" /></a><script src="' + js + '" charset="utf-8" async="async"><\/script>';
	return html;
};

// はてなブックマークボタン（vertical）
msbBtn.hatenabookmarkV = function(data){
	var name = data['name'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = data['name'];
	}
	var url = 'http://b.hatena.ne.jp/entry/' + encodeURIComponent(thisDomain() + getUrl()['path_cutpageing_cutparam']);
	var img = 'http://b.st-hatena.com/images/entry-button/button-only.gif';
	var js = 'http://b.st-hatena.com/js/bookmark_button.js';
	var html = '<a href="' + url + '" class="hatena-bookmark-button" data-hatena-bookmark-layout="vertical-balloon" title="' + title + '"><img src="' + img + '" alt="' + name + '" width="20" height="20" style="border: none;" /></a><script src="' + js + '" charset="utf-8" async="async"><\/script>';
	return html;
};

// ブログに書く（kwout）
msbBtn.kwout = function(data){
	var name = data['name'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = data['name'];
	}
	var url = 'javascript:location.href=\'http://itmedia.kwout.com/grab?address=' + encodeURIComponent(thisDomain() + getUrl()['path_cutpageing_cutparam']) + '\'';
	var evt = '(function(){var btn = \'kwout\';var a = location.pathname.split(\'/\')[1] + \'_共通ブクマ_\' + btn + \' pos=' + this.pos + '\';s.prop14 = a;s.tl(this,\'o\',a);s_objectid = a;return true;})();';
	var html = '<a href="' + url + '" title="' + title + '" target="_blank" onClick="' + evt + '" class="msbBtntext"><span>' + name + '</span></a>';
	return html;
};

// メール
msbBtn.mail = function(data){
	var name = data['name'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = data['name'];
	}
	var url = 'mailto:?body=' + escape(thisDomain() + getUrl()['path_cutpageing_cutparam']);
	var evt = '(function(){var btn = \'メール\';var a = location.pathname.split(\'/\')[1] + \'_共通ブクマ_\' + btn + \' pos=' + this.pos + '\';s.prop14 = a;s.tl(this,\'o\',a);s_objectid = a;return true;})();';
	var html = '<a href="' + url + '" title="' + title + '" onClick="' + evt + '" class="msbBtntext"><span>' + name + '</span></a>';
	return html;
};

// Pocket Button（カウンターなし）
msbBtn.pocketbutton = function(data){
	var html = '<a data-pocket-label="pocket" data-pocket-count="none" class="pocket-btn" data-lang="en"></a><script>!function(d,i){if(!d.getElementById(i)){var j=d.createElement("script");j.id=i;j.src="https://widgets.getpocket.com/v1/j/btn.js?v=1";var w=d.getElementById(i);d.body.appendChild(j);}}(document,"pocket-btn-js");<\/script>';
	return html;
};

// Pocket Button（カウンターあり）
msbBtn.pocketbuttonC = function(data){
	var html = '<a data-pocket-label="pocket" data-pocket-count="horizontal" class="pocket-btn" data-lang="en"></a><script>!function(d,i){if(!d.getElementById(i)){var j=d.createElement("script");j.id=i;j.src="https://widgets.getpocket.com/v1/j/btn.js?v=1";var w=d.getElementById(i);d.body.appendChild(j);}}(document,"pocket-btn-js");<\/script>';
	return html;
};

// Pocket Button（vertical）
msbBtn.pocketbuttonV = function(data){
	var html = '<a data-pocket-label="pocket" data-pocket-count="vertical" class="pocket-btn" data-lang="en"></a><script type="text/javascript">!function(d,i){if(!d.getElementById(i)){var j=d.createElement("script");j.id=i;j.src="https://widgets.getpocket.com/v1/j/btn.js?v=1";var w=d.getElementById(i);d.body.appendChild(j);}}(document,"pocket-btn-js");<\/script>';
	return html;
};

// ツイートボタン（Twitter）standard
msbBtn.tweetbutton = function(data){
	var name = data['name'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = data['name'];
	}
	var ac = msbBtn.tweetbutton_ac();
	var url = 'http://twitter.com/share';
	var html = '<a href="' + url + '" class="twitter-share-button" data-url="' + thisDomain() + getUrl()['path_cutpageing_cutparam'] + '"  data-count="horizontal" data-lang="ja"' + ac + '>' + name + '</a><script type="text/javascript" src="http://platform.twitter.com/widgets.js" charset="utf-8"><\/script>';
	return html;
};

// ツイートボタン（Twitter）vertical
msbBtn.tweetbuttonV = function(data){
	var name = data['name'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = data['name'];
	}
	var ac = msbBtn.tweetbutton_ac();
	var url = 'http://twitter.com/share';
	var html = '<a href="' + url + '" class="twitter-share-button" data-url="' + thisDomain() + getUrl()['path_cutpageing_cutparam'] + '"  data-count="vertical" data-lang="ja"' + ac + '>' + name + '</a><script type="text/javascript" src="http://platform.twitter.com/widgets.js" charset="utf-8"><\/script>';
	return html;
};

// Like Button（Facebook）standard
msbBtn.likebutton = function(){
	var ua = navigator.userAgent;
	var url = 'http://www.facebook.com/plugins/like.php?href=' + encodeURIComponent(thisDomain() + getUrl()['path_cutpageing_cutparam']) + '&amp;layout=button_count&amp;show_faces=true&amp;width=10&amp;action=like&amp;font=verdana&amp;colorscheme=light&amp;height=64';
	var html = [];

	// IE のみ iFrame
	if(ua.indexOf('MSIE') != -1){
		html.push('<!-- IE のみ iFrame -->');
		html.push('<iframe src="' + url + '" scrolling="no" frameborder="0" style="border:none;overflow:hidden;width:110px;height:20px;"allowTransparency="true"></iframe>');

	// XFBML
	}else{
		html.push('<!-- XFBML -->');
		html.push('<fb:like href="' + thisDomain() + getUrl()['path_cutpageing_cutparam'] + '" send="false" layout="button_count" width="110" show_faces="true"></fb:like>');

	}
	return html.join('');
};

// Like + Share Button（Facebook）button_count
msbBtn.likesharebutton = function(){
	var url = thisDomain() + getUrl()['path_cutpageing_cutparam'];
	var html = [];
	html.push('<div class="fb-like" data-href="' + url + '" data-layout="button_count" data-action="like" data-show-faces="false" data-share="true"></div>');
	return html.join('');
};

// Like Button（Facebook）boxcount
msbBtn.likebuttonV = function(){
	var url = thisDomain() + getUrl()['path_cutpageing_cutparam'];
	var html = [];
	html.push('<div class="fb-like" data-href="' + url + '" data-layout="box_count" data-action="like" data-show-faces="false" data-share="false"></div>');
	return html.join('');
};

// Share Button（Facebook）standard
msbBtn.sharebutton = function(){
	var url = thisDomain() + getUrl()['path_cutpageing_cutparam'];
	var html = [];
	html.push('<!-- HTML5 -->');
	html.push('<div class="fb-share-button" data-href="' + url + '" data-type="button_count"></div>');
	return html.join('');
};

// Share Button（Facebook）boxcount
msbBtn.sharebuttonV = function(){
	var url = thisDomain() + getUrl()['path_cutpageing_cutparam'];
	var html = [];
	html.push('<div class="fb-share-button" data-href="' + url + '" data-type="box_count"></div>');
	return html.join('');
};

// Send Button（Facebook）
msbBtn.sendbutton = function(){
	return '<div class="fb-send" data-href="' + encodeURIComponent(thisDomain() + getUrl()['path_cutpageing_cutparam']) + '"></div>';
};

// Google +1 Button（サイズ：small）
msbBtn.googleplusone = function(){
	return '<g:plusone size="small"></g:plusone>';
};

// Google +1 Button（サイズ：medium）
msbBtn.googleplusoneM = function(){
	return '<g:plusone size="midium"></g:plusone>';
};

// Google +1 Button（サイズ：tall バルーン）
msbBtn.googleplusoneV = function(){
	var url = thisDomain() + getUrl()['path_cutpageing_cutparam'];
	return '<div class="g-plusone" data-size="tall" data-href="' + url + '"></div>';
};

// mixiチェック
msbBtn.mixicheck = function(data){
	var name = data['name'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = data['name'];
	}
	var key = msbBtn.mixicheck_key();
	var url = 'http://mixi.jp/share.pl';
	var evt = '(function(){var btn = \'mixiチェック\';var a = location.pathname.split(\'/\')[1] + \'_共通ブクマ_\' + btn + \' pos=' + this.pos + '\';s.prop14 = a;s.tl(this,\'o\',a);s_objectid = a;return true;})();';
	var html = '<a href="' + url + '" class="mixi-check-button" data-key="' + key + '" data-url="' + thisDomain() + getUrl()['path_cutpageing_cutparam'] + '" title="' + title + '" onClick="' + evt + '"></a><script type="text/javascript" src="http://static.mixi.jp/js/share.js"><\/script>';
	return html;
};

// ITMID アラート BUTTON
msbBtn.alertbutton = function(data){
	var name = data['name'];
	var path = location.pathname;
	var site = path.split('/')[1];
	var sctag = '';
	var sc = '';
	var lc = '';
	var ac = '8b70865e13a2d61cf96c34172b9312018dffa6d8d496609bf9fd8314fd091e1a';
	var article_type = 1;
	var article_url = msbBtn.path2domain() + path;
	var return_url = encodeURIComponent('https://id.itmedia.jp/app/alert/regist_setting?url=' + article_url + '&type=' + article_type);
	var encoding = 'shift_jis';
	var title = '関連記事アラート（ID対応）';
	var url = '';
	var html = '';
	sctag += 'var s = s_gi(ThisSite);';
	sctag += 's.prop14 = \'' + site + '_ITMID_alart_pos=' + this.pos + '\'; ';
	sctag += 's.events = \'event6\'; ';
	sctag += 's.eVar17 = \'' + site + '_ITMID_alart_pos=' + this.pos + '\'; ';
	sctag += 's.tl(this,\'o\',\'' + site + '_ITMID_alart_pos=' + this.pos + '\');';
	if(site == 'news'){
		sc = 'f296867839c8befafed32b55a7c11ab4ad14387d2434b970a55237d537bc9353';
		lc = '025ca19d7b07d7f554fd7cd060b628e628d0f607afa3a6fde17c4488f35716d2';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'enterprise'){
		sc = 'f296867839c8befafed32b55a7c11ab4ad14387d2434b970a55237d537bc9353';
		lc = 'cdad86ca9450d1c143675a8436131cabaf55905c114fa4524bf6a9ec5662cad7';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'makoto' || site == 'business'){
		sc = '3e34b5dc434bcf3186f089d362691cfac1b17231601f2f402dc79015be878d83';
		lc = '2f1987bf98c09d2f5d2a23a6ae29fa53b9aec8f07ed1330bd439122f5a1a2c2c';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'bizid'){
		sc = '340ab11db8d1a7435cb4b4a0492a9eee7b8e388e3e4a1714bcd3b69df3d8f1e1';
		lc = 'cdad86ca9450d1c143675a8436131cabaf55905c114fa4524bf6a9ec5662cad7';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'style'){
		sc = '3e34b5dc434bcf3186f089d362691cfac1b17231601f2f402dc79015be878d83';
		lc = '2f1987bf98c09d2f5d2a23a6ae29fa53b9aec8f07ed1330bd439122f5a1a2c2c';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'ait'){
		sc = 'f00f2e7bca65e9f8409fdb3bcddfa031664224255d7bd2f6b3de8ff11ababe20';
		lc = 'c0aa4a0be7ba28399b09a68835a21755f442e25f8e0971b1d1ea3a6c749f0385';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else{
		url = 'https://id.itmedia.jp/isentry?return_url=' + return_url + '&article_url=' + article_url + '&encoding=' + encoding + '&ac=' + ac;
	}
	html = '<a href="' + url + '" target="_blank" title="' + title + '" class="msbBtntext" id="ITMIDalert"><span>' + name + '</span></a>';
	return html;
};

// ITMID 連載アラート BUTTON
msbBtn.alertseries = function(data){
	if(msbBtn.alertseriesMeta() == '' || msbBtn.alertseriesMeta() == false) return '';
	var name = data['name'];
	var path = location.pathname;
	var site = path.split('/')[1];
	var sctag = '';
	var sc = '';
	var lc = '';
	var ac = '8b70865e13a2d61cf96c34172b9312018dffa6d8d496609bf9fd8314fd091e1a';
	var article_type = 2;
	var article_url = msbBtn.path2domain() + path;
	var return_url = encodeURIComponent('https://id.itmedia.jp/app/alert/regist_setting?url=' + article_url + '&type=' + article_type);
	var encoding = 'shift_jis';
	var title = msbBtn.alertseriesMeta();
	var text = '';
	if(title.length > 23){
		text = name.replace('％','<strong>' + title.slice(0,23) + '...</strong>');
	}else{
		text = name.replace('％','<strong>' + title + '</strong>');
	}
	var url = '';
	var html = '';
	sctag += 'var s = s_gi(ThisSite); ';
	sctag += 's.prop14 = \'' + site + '_ITMID_serial_pos=' + this.pos + '\'; ';
	sctag += 's.events = \'event2\'; ';
	sctag += 's.eVar17 = \'' + site + '_ITMID_serial_pos=' + this.pos + '\'; ';
	sctag += 's.tl(this,\'o\',\'' + site + '_ITMID_serial_pos=' + this.pos + '\');';
	if(site == 'news'){
		sc = 'f296867839c8befafed32b55a7c11ab4ad14387d2434b970a55237d537bc9353';
		lc = '025ca19d7b07d7f554fd7cd060b628e628d0f607afa3a6fde17c4488f35716d2';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'enterprise'){
		sc = 'f296867839c8befafed32b55a7c11ab4ad14387d2434b970a55237d537bc9353';
		lc = 'cdad86ca9450d1c143675a8436131cabaf55905c114fa4524bf6a9ec5662cad7';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'makoto' || site == 'business'){
		sc = '3e34b5dc434bcf3186f089d362691cfac1b17231601f2f402dc79015be878d83';
		lc = '2f1987bf98c09d2f5d2a23a6ae29fa53b9aec8f07ed1330bd439122f5a1a2c2c';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'bizid'){
		sc = '340ab11db8d1a7435cb4b4a0492a9eee7b8e388e3e4a1714bcd3b69df3d8f1e1';
		lc = 'cdad86ca9450d1c143675a8436131cabaf55905c114fa4524bf6a9ec5662cad7';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'style'){
		sc = '3e34b5dc434bcf3186f089d362691cfac1b17231601f2f402dc79015be878d83';
		lc = '2f1987bf98c09d2f5d2a23a6ae29fa53b9aec8f07ed1330bd439122f5a1a2c2c';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'ait'){
		sc = 'f00f2e7bca65e9f8409fdb3bcddfa031664224255d7bd2f6b3de8ff11ababe20';
		lc = 'c0aa4a0be7ba28399b09a68835a21755f442e25f8e0971b1d1ea3a6c749f0385';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else{
		url = 'https://id.itmedia.jp/isentry?return_url=' + return_url + '&article_url=' + article_url + '&encoding=' + encoding + '&ac=' + ac;
	}
	html = '<a href="' + url + '" target="_blank" title="' + title + '" class="msbBtntext" id="ITMIDalert2"><span>' + text + '</span></a>';
	return html;
};

// ITMID プリント BUTTON
msbBtn.printbutton = function(data){
	var name = data['name'];
	var path = location.pathname;
	var site = path.split('/')[1];
	var sctag = '';
	var sc = '';
	var lc = '';
	var ac = 'e8cb9106baa7e37eb9feb877b9f0a27ddaf48b95ba02da49cbb3a8247ee7fec4'; // プリント用デフォルト
	var article_url = '';
	var return_url = 'http://ids.itmedia.jp/print' + path;
	var encoding = 'shift_jis';
	var title = 'この記事を印刷する';
	var url = '';
	var html = '';
	sctag += 'var s = s_gi(ThisSite); ';
	sctag += 's.prop14 = \'' + site + '_ITMID_print_pos=' + this.pos + '\'; ';
	sctag += 's.events = \'event1\'; ';
	sctag += 's.eVar17 = \'' + site + '_ITMID_print_pos=' + this.pos + '\'; ';
	sctag += 's.tl(this,\'o\',\'' + site + '_ITMID_print_pos=' + this.pos + '\')';
	if(site == 'tt'){
		sc = '165940940a02a187e4463ff467090930038c5af8fc26107bf301e714f599a1da';
		lc = '582c0168ba17eac49642bc85ae623204069e8d6ea06cf45af11e7de46ea31d18';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'mm'){
		sc = 'b0ab628c9e14621846c58b4eb35060ef3885253a457d2d76136716d4850bad45';
		lc = '8c6c42f379f08f03b79653a3230abd5e8079999435030fd8ca703ae35fe9b37a';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'ait'){ // 201308 release
		sc = 'f00f2e7bca65e9f8409fdb3bcddfa031664224255d7bd2f6b3de8ff11ababe20';
		lc = 'c0aa4a0be7ba28399b09a68835a21755f442e25f8e0971b1d1ea3a6c749f0385';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'news'){
		sc = 'f296867839c8befafed32b55a7c11ab4ad14387d2434b970a55237d537bc9353';
		lc = '025ca19d7b07d7f554fd7cd060b628e628d0f607afa3a6fde17c4488f35716d2';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'enterprise'){
		sc = 'f296867839c8befafed32b55a7c11ab4ad14387d2434b970a55237d537bc9353';
		lc = 'cdad86ca9450d1c143675a8436131cabaf55905c114fa4524bf6a9ec5662cad7';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'makoto' || site == 'business'){
		sc = '3e34b5dc434bcf3186f089d362691cfac1b17231601f2f402dc79015be878d83';
		lc = '2f1987bf98c09d2f5d2a23a6ae29fa53b9aec8f07ed1330bd439122f5a1a2c2c';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'bizid'){
		sc = '340ab11db8d1a7435cb4b4a0492a9eee7b8e388e3e4a1714bcd3b69df3d8f1e1';
		lc = 'cdad86ca9450d1c143675a8436131cabaf55905c114fa4524bf6a9ec5662cad7';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else if(site == 'style'){
		sc = '3e34b5dc434bcf3186f089d362691cfac1b17231601f2f402dc79015be878d83';
		lc = '2f1987bf98c09d2f5d2a23a6ae29fa53b9aec8f07ed1330bd439122f5a1a2c2c';
		url = 'https://id.itmedia.jp/isentry/contents?sc=' + sc + '&lc=' + lc + '&return_url=' + return_url + '&encoding=' + encoding + '&ac=' + ac;
	}else{
		article_url = msbBtn.path2domain() + path;
		url = 'https://id.itmedia.jp/isentry?return_url=' + return_url + '&article_url=' + article_url + '&encoding=' + encoding + '&ac=' + ac;
	}
	html = '<a href="' + url + '" target="_blank" title="' + title + '" class="msbBtntext"><span>' + name + '</span></a>';
	return html;
};
/*--------------------------------------------------------------------------------------*/
// ITMID 連載アラートチェック
/*--------------------------------------------------------------------------------------*/
msbBtn.alertseriesMeta = function(){
	var metaname = 'itmid:series';
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
	return target[0].content;
};
/*--------------------------------------------------------------------------------------*/
// BUTTON PARAMETAR
/*--------------------------------------------------------------------------------------*/
msbBtn.tweetbutton_ac = function(){
	var ac = (function(){
		if(typeof masterChannel != "function") return '';
		switch(masterChannel()){
			case 'mn':return ' data-via="monoist_today"';break;
			case 'ee':return ' data-via="eetimes_jp"';break;
			case 'nl':return ' data-via="itm_nlab"';break;
			case 'makoto':return ' data-via="itm_business"';break;
			case 'pcuser':return ' data-via="itm_pcuser"';break;
			case 'mobile':return ' data-via="itm_mobile"';break;
			case 'dc':return ' data-via="digicameplus"';break;
			case 'lifestyle':return ' data-via="itm_lifestyle"';break;
			case 'ebook':return ' data-via="itm_ebookuser"';break;
			case 'hc':return ' data-via="itm_healthcare"';break;
			default:return '';break;
		};
	})();
	return ac;
};
msbBtn.mixicheck_key = function(){
	var key = [];
	var keylist = [
	{'url':'/tt/','key':'a8c543b37ec3996f3231eaaa4d964643f6552919'},
	{'url':'/news/','key':'bdda1b567a7fb345f25cf6edce689f3a22fcdb57'},
	{'url':'/enterprise/','key':'087f96ca984652ac7b195355bb9e60308859f909'},
	{'url':'/executive/','key':'087f96ca984652ac7b195355bb9e60308859f909'},
	{'url':'/promobile/','key':'17f69492dd231c56f17a9cf36e061cb91e59313e'},
	{'url':'/mobile/','key':'cc82b5e31a6d299a9183dd1ad3b1279b86a89f2c'},
	{'url':'/pcuser/','key':'326a4cc0287d699ded051c569c00d5a9abda5d86'},
	{'url':'/lifestyle/','key':'081e70bd813db4fb62f2a112713d1c55663823a3'},
	{'url':'/dc/','key':'7b1134d7662eaa72d0f53886c84bb1c992097453'},
	{'url':'/ebook/','key':'dd3dc8ff5ac1d1138c32cfd1cd069b626c14f0ff'},
	{'url':'/gg/','key':'ad3dad18bc6ad29879ce9727379b4ca474cc9367'},
	{'url':'/nl/','key':'3b56f29301c4b81a0fc4577cdff2e22db1e63e91'},
	{'url':'/makoto/','key':'039f4e39e0f0e94300b70b6ea4bc083eb3725363'},
	{'url':'/bizid/','key':'4b4f4b07f1b085befbfafba6d7aa0218da73406c'},
	{'url':'/style/','key':'adf0214204f9342c2ea96410c32a94be2d7db389'},
	{'url':'/mn/','key':'338aa2623b31bec3157e5fcd31006e99c5702fdd'},
	{'url':'/ee/','key':'bce66124c8d3f69b5a7f50b67c2ad641fbb6af7c'},
	{'url':'/edn/','key':'c050a38926436163f4a7dc26aa35f462cd3b34d8'},
	{'url':'/smartjapan/','key':'bc8228b32e002616d4503bd3bf77a370df3a1653'},
	{'url':'/mm/','key':'9b2dd1f9a3961ddeeadca6a58d41ec3395b39d97'}
	];
	for(var i = 0; i < keylist.length; i++){
		if(getUrl()['url'].match(keylist[i]['url'])){
			key.push(keylist[i]['key']);
			break;
		}
	}
	if(key.length == 0) key[0] = '33d5e6b20bc3453638ed91452604d69116cdf9d9'; // テスト用
	return key[0];
};
/*--------------------------------------------------------------------------------------*/
// UTILITY
/*--------------------------------------------------------------------------------------*/
// 最初の 1 文字を大文字変換
msbBtn.str1stup = function(str){
	return str.substr(0,1).toUpperCase() + str.substr(1).toLowerCase();
};
// SCRIPT タグ重複チェック
msbBtn.scriptcheck = function(js){
	var head = document.getElementsByTagName('head')[0];
	var script = head.getElementsByTagName('script');
	var newscript = 0;
	var counter = [];
	for(var i = 0; i  < script.length; i++){
		if(script[i].getAttribute('src') == js){
			counter.push(script[i]);
			break;
		}
	}
	if(counter.length == 0){
		newscript = document.createElement('script');
		newscript.setAttribute('src',js);
		head.appendChild(newscript);
		return true;
	}
	return false;
};
// PATH から DOMAIN を返す
msbBtn.path2domain = function(){
	var a = location.pathname;
	if(a.match(/^\/(makoto|style|d-style|bizmobile)\//)){
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
	}else if(a.match(/^\/edn\//)){
		var b = 'http://ednjapan.com';
	}else if(a.match(/^\/gg\//)){
		var b = 'http://gadget.itmedia.co.jp';
	}else if(a.match(/^\/nl\//)){
		var b = 'http://nlab.itmedia.co.jp';
	}else if(a.match(/^\/mm\//)){
		var b = 'http://marketing.itmedia.co.jp';
	}else if(a.match(/^\/tt\//)){
		var b = 'http://techtarget.itmedia.co.jp';
	}else if(a.match(/^\/ait\//)){
		var b = 'http://www.atmarkit.co.jp';
	}else if(a.match(/^\/review\//)){
		var b = 'http://review.itmedia.co.jp';
	}else if(a.match(/^\/hc\//)){
		var b = 'http://healthcare.itmedia.co.jp';
	}else{
		var b = 'http://www.itmedia.co.jp';
	}
	return b;
};
/*--------------------------------------------------------------------------------------*/
// EVENT
/*--------------------------------------------------------------------------------------*/
msbBtn.group_block = function(e){
	e.className = 'msbGroupIn msbGroupInBlock';
	return true;
};
msbBtn.group_none = function(e){
	e.className = 'msbGroupIn';
	return true;
};

/*---------------------------------------------------------*/
// TWITTER ORIGINAL
msbBtn.tweetbutton_org = function(data){
	var name = data['name'];
	var id = data['id'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = data['name'];
	}
	var ac = msbBtn.tweetbutton_ac();
	var o_url = thisDomain() + getUrl()['path_cutpageing_cutparam'];
	var p_title = encodeURIComponent(document.title);
	var p_url = encodeURIComponent(o_url);
	var count_class = id + 'Count';
	var count_id = count_class + this.pos;
	var html = '<a title="' + title + '" href="javascript:void(0);" onClick="window.open(\'http://twitter.com/share?text=' + p_title + '&amp;url=' + p_url + '\',\'TwitterTweet\',\'width=600,height=600\');"><span class="msbBtnCount ' + count_class + '" id="' + count_id + '"><span class="msbBtnJsonloader"></span></span></a>';
	tweetCount(o_url,count_id);
	return html;
};
var tweetCount = function (_url,_id){
	var pageURL = (_url)?_url:location.href;
	jQuery.ajax({
		type:'GET',
		url:'http://urls.api.twitter.com/1/urls/count.json',
		data:{
			url:encodeURI(pageURL),
			noncache:new Date()
		},
		dataType:'jsonp',
		success:function(data){
			var count = data.count;
			var e = document.getElementById(_id);
			e.className += ' success';
			setTimeout(function(){
				e.innerHTML = count;
			},1000);
		}
	});
};
/*---------------------------------------------------------*/
// FACEBOOK ORIGINAL
msbBtn.likebutton_org = function(data){
	var name = data['name'];
	var id = data['id'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = name;
	}
	var o_url = thisDomain() + getUrl()['path_cutpageing_cutparam'];
	var p_title = encodeURIComponent(document.title);
	var p_url = encodeURIComponent(o_url);
	var count_class = id + 'Count';
	var count_id = count_class + this.pos;
	var html = '<a title="' + title + '" href="javascript:void(0);" onClick="window.open(\'http://www.facebook.com/sharer.php?u=' + p_url + '&amp;t=' + p_title + '\',\'FacebookShare\',\'width=600,height=600\');"><span class="msbBtnCount ' + count_class + '" id="' + count_id + '"><span class="msbBtnJsonloader"></span></span></a>';
	likeCount(o_url,count_id);
	return html;
};
var likeCount = function(_url,_id){
	var pageURL = (_url)?_url:location.href;
	jQuery.ajax({
		type:'GET',
		url:'https://api.facebook.com/method/fql.query?format=json&query=select%20%20like_count%20,comment_count%20,share_count%20from%20link_stat%20where%20url=%22' + pageURL + '%22',
		dataType:'jsonp',
		success:function(data){
			var count = data[0]['like_count'] + data[0]['share_count'] + data[0]['comment_count'];
			var e = document.getElementById(_id);
			e.className += ' success';
			setTimeout(function(){
				e.innerHTML = count;
			},1000);
		}
	});
};
/*---------------------------------------------------------*/
// HATENA ORIGINAL
msbBtn.hatenabookmark_org = function(data){
	var name = data['name'];
	var id = data['id'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = name;
	}
	var o_url = thisDomain() + getUrl()['path_cutpageing_cutparam'];
	var p_title = encodeURIComponent(document.title);
	var p_url = encodeURIComponent(o_url);
	var count_class = id + 'Count';
	var count_id = count_class + this.pos;
	var html = '<a title="' + title + '" href="http://b.hatena.ne.jp/entry/' + p_url + '" class="hatena-bookmark-button" data-hatena-bookmark-title="' + p_title + '" data-hatena-bookmark-layout="simple" data-hatena-bookmark-mode="popup"><span class="msbBtnCount ' + count_class + '" id="' + count_id + '"><span class="msbBtnJsonloader"></span></span></a><script type="text/javascript" src="http://b.st-hatena.com/js/bookmark_button.js" charset="utf-8" async="async"><\/script>';
	hatenaCount(o_url,count_id);
	return html;
};
var hatenaCount = function(_url,_id){
	var pageURL = (_url)?_url:location.href;
	jQuery.ajax({
		type:'GET',
		url:'http://api.b.st-hatena.com/entry.count',
		data:{
			url:pageURL
		},
		dataType:'jsonp',
		success:function(data){
			var count = (data)?data:0;
			var e = document.getElementById(_id);
			e.className += ' success';
			setTimeout(function(){
				e.innerHTML = count;
			},1000);
		}
	});
};
/*---------------------------------------------------------*/
// POCKET ORIGINAL
msbBtn.pocket_org = function(data){
	var name = data['name'];
	var title = '';
	if(data['title']){
		title = data['title'];
	}else{
		title = name;
	}
	var o_url = thisDomain() + getUrl()['path_cutpageing_cutparam'];
	var p_title = encodeURIComponent(document.title);
	var p_url = encodeURIComponent(o_url);
	var html = '<a title="' + title + '" href="http://getpocket.com/edit?url=' + p_url + '&title=' + p_title + '" onClick="window.open(this.href,\'Pocket\',\'width=600,height=600,menubar=no,toolbar=no,scrollbars=yes\');return false;"><span class="msbBtnCount">Pocket</span></a>';
	return html;
};
