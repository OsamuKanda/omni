(function(){
	// 現行 SP ビュー対象端末処理
	// if(navigator.userAgent.indexOf('iPod') != -1) return false;
	// if(navigator.userAgent.indexOf('iPhone') == -1 && navigator.userAgent.indexOf('Android') == -1) return false;

	// path get
	var path = location.pathname;

	// useragent
	var ua = navigator.userAgent;

	// iPad 除外
	if(ua.indexOf('iPad') != -1) return false;

	// Android Tablet 除外
	if(ua.indexOf('Android') != -1 && ua.indexOf('Mobile') == -1) return false;

	// 対象端末だったら
	if((ua.indexOf('iPhone') != -1 || ua.indexOf('iPod') != -1 || ua.indexOf('Android') != -1 || ua.indexOf('Windows Phone') != -1)){

		// 記事／キーワードインデックス／SPV 対応した編集特集以外は処理しない
//		if(!location.pathname.match(/\/.+\/(articles|news)\/\d\d\d\d\/\d\d\/news/) && !location.pathname.match(/^\/keywords\//) && swspv != '1') return false;

		var s = path.split('/');

		// 総合トップ
		if(path.match(/^\/$/) || path.match(/^\/root\/$/) || path.match(/^\/root\/index[0-9]?\.html$/)){
			console.log('総合トップです');

		// チャンネルトップ
		}else if(s.length == 3 || (s.length == 4 && path.match('root'))){
			console.log('チャンネルトップです' + s.length);

		// 記事
		}else if(path.match(/\/.+\/(articles|news)\/\d\d\d\d\/\d\d\/news/)){
			console.log('記事です');

		// キーワードインデックス
		}else if(path.match(/^\/keywords\//)){
			console.log('キーワードインデックスです');

		// サブトップ（ツール）
		}else if(swspv == '1'){
			console.log('サブトップです');

		}else{
			console.log('該当なし' + s.length);
			return false;
		}

		var target = document.getElementsByTagName('body')[0];

		var stylesheet = document.createElement('link');
		stylesheet.setAttribute('rel','stylesheet');
		stylesheet.setAttribute('type','text/css');
		stylesheet.setAttribute('media','all');
		stylesheet.setAttribute('href','/js/itmid/smartPhoneHeadLink.css');

		var div = document.createElement('div');
		div.setAttribute('id','spvLinkTop');
		div.className = 'spvLink';
		div.innerHTML = '<div class="spvLinkIn"><a href="javascript:void(0);" onClick="movePC2SP({\'name\':\'pc2sp\',\'click\':true});"><span>スマートフォン用表示に変更</span></a></div>';
		target.insertBefore(div,target.childNodes[0]);
		var div2 = div.cloneNode(true);
		div2.setAttribute('id','spvLinkBtm');

		window.onload = function(){
			target.insertBefore(stylesheet,target.childNodes[0]);
			target.appendChild(div2);
		}

		return true;
	}
})();
