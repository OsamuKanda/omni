function dispSerialBackNumber(json){

	/**
	* ＠IT連載バックナンバー設置確認
	* ＠IT CMS 移行
	*
	* 前提条件
	* 共通テンプレートを使用していること
	*/

	// 設置確認用
/*
	var str = getScriptThis(document).src.split('?');
	var path = str[1].split('&');
	document.write(path[0]);
	return true;
*/

	// 連載 API パーサ
	if(json['articles'].length == 0) return false;
	var header = [];
	header.push('<div class="backNumBoxRap"><div class="backNumBox">');
	header.push('<strong>「' + json['serial_name'] + '」最新記事バックナンバー</strong>');
	header.push('<div class="backNumBoxIn"><ul>');

	var contents = [];
	for(var i = 0; i < json['articles'].length; i++){
		var data = json['articles'][i];
		if(!data['title']) continue;
		contents.push('<li class="bn' + data['uri'] + '"><span><a href="' + data['uri'] + '">' + data['title'] + '</a></span></li>');
	}

	var footer = [];
	footer.push('</ul></div></div></div>');

	document.write(header.join('\n') + contents.join('\n') + footer.join('\n'));
	return true;
};

// 記事下ウィジェット用
function setBtmTwTab() {
	var fdata = getForum(getMETA('extraid').content)[0];
	if(!fdata) {
		var params = getSnsOption();
	} else {
		var params = getSnsOption(fdata.path);
	}
	var html = '<a class="twitter-timeline" href="https://twitter.com/' + params.twitterid + '" data-widget-id="' + params.tw_widget_id + '" width="637" height="350">@' + params.twitterid + ' からのツイート</a>' + 
			   '<script>!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0];if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src="//platform.twitter.com/widgets.js";fjs.parentNode.insertBefore(js,fjs);}}(document,"script","twitter-wjs");<\/script>';
	document.write(html);
};
function setBtmFbTab() {
	// fbのDOMに記事URLの1ページ目を設定
	jQuery('#artBtmSnsFb').children('div.fb-comments').attr({'data-href': "http://www.atmarkit.co.jp" + getUrl()['path_cutpageing_cutparam'] });
	
	// fbコメントの実行処理
	(function(d, s, id) {
		var js, fjs = d.getElementsByTagName(s)[0];
			if (d.getElementById(id)) return;
		js = d.createElement(s); js.id = id;
		js.src = "//connect.facebook.net/ja_JP/all.js#xfbml=1&appId=221728961311992";
	fjs.parentNode.insertBefore(js, fjs);
	}(document, 'script', 'facebook-jssdk'));
};
//バックナンバー枠処理テスト

jQuery(window).load(function(){
	// バックナンバーボックスアクティブ表示
	if(jQuery('.backNumBox li').length != 0) {
		jQuery('.backNumBox li').each(function(){
			if(location.pathname.match(jQuery('a',this).attr('href').replace(location.host,'').replace('.html',''))) {
				jQuery(this).addClass('backNumBoxListActive');
			} else if(location.pathname.match(jQuery('a',this).attr('href').replace("http://www.atmarkit.co.jp",'').replace('.html',''))){
				jQuery(this).addClass('backNumBoxListActive');
			}
		
		})

		if(jQuery('.backNumBox ul').height() > 200){
			jQuery('.backNumBox ul').addClass('backNumBoxScroll');
		} else {
			//
		}

	} else {
		return false;
	};
	// 旧バックナンバーボックス用 CSS を削除
	if(jQuery('#backNumBox').length != 0) {
		jQuery('#backNumBox').prev().remove();
	} else {
		return false;
	};
});




/* 記事ヘッダ表示用
--------------------------------------------------------------------- */
function setArticlHeader(json) {
	if(!json) return false;
	var metaTag = getMETA('keywords');
	if(!metaTag) return false;
	var kwdText = metaTag.getAttribute('content');
	var kwdList = metaTag.getAttribute('content').split(',');
	if((kwdText.match(/PR/))&&(!kwdText.match(/Editors Eye/))) return false;
	//if(((kwdText.match(/業務アプリInsider/))||(kwdText.match(/PR/)))&&(!kwdText.match(/Editors Eye/))) return false;
	var data = json['data'];
	for(var i = 0; i < data.length; i++) {
		if(!data[i]['KEYWORDS']) continue;
		for(var j = 0; j <  kwdList.length; j++) {
			if(data[i]['KEYWORDS'] != kwdList[j]) continue;
			var tgt = document.getElementById('tmplNewsIn');
			var headerIcon = document.createElement('div');
			headerIcon.className = 'colBox colBoxArticleHeader';
			headerIcon.innerHTML = '<div class="colBoxOuter"><div class="colBoxInner"><div class="colBoxIndex"><div class="colBoxIcon">' +
									'<a href="' + data[i]['URL'] + '" title="' + data[i]['NAME'] + '"><img src="' + imgSrv() + data[i]['PATH'] + '" alt="' + data[i]['NAME'] + '"></a>' +
									'</div></div></div></div>';
			tgt.insertBefore(headerIcon,tgt.firstChild);
			return true;
		}
	}
	return false;
}