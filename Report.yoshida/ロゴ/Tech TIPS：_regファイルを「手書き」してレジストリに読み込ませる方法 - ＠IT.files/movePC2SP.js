/* COOKIE 読み込み
-----------------------------------------------------------------------*/
movePC2SP.getCookie = function(name){
	if(!name) return '';
	var cookies = document.cookie.split('; ');
	for(var i = 0; i < cookies.length; i++){
		var str = cookies[i].split('=');
		if (str[0] != name) continue;
		return unescape(str[1]);
	}
	return '';
};

/* COOKIE 書き込み（param {'name':COOKIE名,'value':値,'domain':HOST,'path':PATH,'expires':有効期限,'secure':0 or 1（0 = 無効、1 = 有効）}）
-----------------------------------------------------------------------*/
movePC2SP.setCookie = function(param){
	if(!param['name']) return false;
	var str = param['name'] + '=' + escape(param['value']);
	if(param['domain']){
		if(param['domain'] == 1) param['domain'] = location.hostname;
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

/* move2PC2SP 対象端末判定
-----------------------------------------------------------------------*/
movePC2SP.checkUA = function(){

	// useragent
	var ua = navigator.userAgent;

	// iPad は非対象端末
	if(ua.indexOf('iPad') != -1) return false;

	// Android Tablet は非対象端末
	if(ua.indexOf('Android') != -1 && ua.indexOf('Mobile') == -1) return false;

	// 対象端末（iPod Touch と Windows Phone 追加、Andoid（Mobile 文字列があるかどうかの判定））（01B の文字化けは気にしない）
	if((ua.indexOf('iPhone') != -1 || ua.indexOf('iPod') != -1 || ua.indexOf('Android') != -1 || ua.indexOf('Windows Phone') != -1)) return true;

	// 非対象端末
	return false;
};

/* movePC2SP debug
-----------------------------------------------------------------------*/
movePC2SP.debug = function(str){
	alert(str);
//	console.log();
	return;
};

/* movePC2SP setting
-----------------------------------------------------------------------*/
movePC2SP.setting = function(){

	// デバッグ用
	this.alertFlag = false; // true | false
	
	// url get
	this.url = document.URL;

	// path get
	this.path = location.pathname;

	// sp 記事判定
	this.sparticle = this.path.match(/spv\/\d\d\d\d\/\d\d\//);

	// pc 記事判定
	this.pcarticle = this.path.match(/articles\/\d\d\d\d\/\d\d\//);

	// path cut
	this.pathcut = this.path.split('/');

	// sp or pc
	this.mysite = (function(flag){
		if(flag.match('/spv/')) return 'sp';
		return 'pc';
	})(this.path);

	// useragent
	this.ua = movePC2SP.checkUA();

};

/* movePC2SP PC SP URL 対比リスト
-----------------------------------------------------------------------*/
movePC2SP.urllist = [
	{'dir':'news','url':'http://www.itmedia.co.jp/news/'},
	{'dir':'news','url':'http://www.itmedia.co.jp/news/index.html'},
	{'dir':'promobile','url':'http://www.itmedia.co.jp/promobile/'},
	{'dir':'promobile','url':'http://www.itmedia.co.jp/promobile/index.html'},
	{'dir':'enterprise','url':'http://www.itmedia.co.jp/enterprise/'},
	{'dir':'enterprise','url':'http://www.itmedia.co.jp/enterprise/index.html'},
	{'dir':'executive','url':'http://mag.executive.itmedia.co.jp/'},
	{'dir':'executive','url':'http://mag.executive.itmedia.co.jp/index.html'},
	{'dir':'executive','url':'http://mag.executive.itmedia.co.jp/root/index.html'},
	{'dir':'dc','url':'http://camera.itmedia.co.jp/'},
	{'dir':'dc','url':'http://camera.itmedia.co.jp/index.html'},
	{'dir':'dc','url':'http://camera.itmedia.co.jp/root/index.html'},
	{'dir':'gg','url':'http://gadget.itmedia.co.jp/'},
	{'dir':'gg','url':'http://gadget.itmedia.co.jp/index.html'},
	{'dir':'gg','url':'http://gadget.itmedia.co.jp/root/index.html'},
	{'dir':'ebook','url':'http://ebook.itmedia.co.jp/'},
	{'dir':'ebook','url':'http://ebook.itmedia.co.jp/index.html'},
	{'dir':'ebook','url':'http://ebook.itmedia.co.jp/root/index.html'},
	{'dir':'lifestyle','url':'http://www.itmedia.co.jp/lifestyle/'},
	{'dir':'lifestyle','url':'http://www.itmedia.co.jp/lifestyle/index.html'},
	{'dir':'pcuser','url':'http://www.itmedia.co.jp/pcuser/'},
	{'dir':'pcuser','url':'http://www.itmedia.co.jp/pcuser/index.html'},
	{'dir':'mobile','url':'http://www.itmedia.co.jp/mobile/'},
	{'dir':'mobile','url':'http://www.itmedia.co.jp/mobile/index.html'},
	{'dir':'makoto','url':'http://bizmakoto.jp/'},
	{'dir':'makoto','url':'http://bizmakoto.jp/index.html'},
	{'dir':'makoto','url':'http://bizmakoto.jp/root/index.html'},
	{'dir':'bizid','url':'http://bizmakoto.jp/bizid/'},
	{'dir':'bizid','url':'http://bizmakoto.jp/bizid/index.html'},
	{'dir':'style','url':'http://bizmakoto.jp/style/'},
	{'dir':'style','url':'http://bizmakoto.jp/style/index.html'},
	{'dir':'mn','url':'http://monoist.atmarkit.co.jp/'},
	{'dir':'mn','url':'http://monoist.atmarkit.co.jp/index.html'},
	{'dir':'mn','url':'http://monoist.atmarkit.co.jp/root/index.html'},
	{'dir':'ee','url':'http://eetimes.jp/'},
	{'dir':'ee','url':'http://eetimes.jp/index.html'},
	{'dir':'ee','url':'http://eetimes.jp/root/index.html'},
	{'dir':'edn','url':'http://ednjapan.com/'},
	{'dir':'edn','url':'http://ednjapan.com/index.html'},
	{'dir':'edn','url':'http://ednjapan.com/root/index.html'},
	{'dir':'nl','url':'http://nlab.itmedia.co.jp/'},
	{'dir':'nl','url':'http://nlab.itmedia.co.jp/index.html'},
	{'dir':'nl','url':'http://nlab.itmedia.co.jp/root/index.html'},
	{'dir':'smartjapan','url':'http://www.itmedia.co.jp/smartjapan/'},
	{'dir':'smartjapan','url':'http://www.itmedia.co.jp/smartjapan/index.html'},
	{'dir':'mm','url':'http://marketing.itmedia.co.jp/'},
	{'dir':'mm','url':'http://marketing.itmedia.co.jp/mm/index.html'},
	{'dir':'mm','url':'http://marketing.itmedia.co.jp/mm/root/index.html'},
	{'dir':'ait','url':'http://www.atmarkit.co.jp/'},
	{'dir':'ait','url':'http://www.atmarkit.co.jp/ait/index.html'},
	{'dir':'ait','url':'http://www.atmarkit.co.jp/ait/root/index.html'},
	{'dir':'review','url':'http://review.itmedia.co.jp/'},
	{'dir':'review','url':'http://review.itmedia.co.jp/review/index.html'},
	{'dir':'review','url':'http://review.itmedia.co.jp/review/root/index.html'},
	{'dir':'tt','url':'http://techtarget.itmedia.co.jp/'},
	{'dir':'tt','url':'http://techtarget.itmedia.co.jp/tt/index.html'},
	{'dir':'tt','url':'http://techtarget.itmedia.co.jp/tt/root/index.html'}
];

/* movePC2SP リダイレクト
-----------------------------------------------------------------------*/
movePC2SP.redirect = function(data){

		// preview、broom、localhost では何もしない
		if(this.url.match('preview') || this.url.match('broom') || this.url.match('localhost')) return false;
//		if(this.url.match('broom')) return false;

		// クリック時
		if(data['click'] == true){
			if(!data['type'] && this.mysite == 'sp'){
				data['type'] = 'pc';
			}else if(!data['type'] && this.mysite == 'pc'){
				data['type'] = 'sp';
			}

			// 確認用 log
			if(this.alertFlag == true) movePC2SP.debug(data['type'] + ' 用リンクがクリックされました。フラグを ' + data['type'] + ' に変更し ' + data['type'] + ' に移動します。');

			// フラグセット（閲覧ページが PC ビューだったら「sp」、SP ビューだったら「pc」）
			movePC2SP.setCookie({'name':data['name'],'value':data['type'],'domain':1,'path':'/','expires':7,'secure':0});
		}

		var redirect_referrer = document.referrer.replace('http://','').replace('https://','').split('/')[0];

		if(typeof swspv != 'undefined'){
		}else{
			swspv = '';
		}

		// SP ビュー
		if(this.mysite == 'sp'){

			// 非対象端末
			if(this.ua == false){

			// 以下リダイレクト処理 SP 側の処理なので masterChannel , masterType , erFlag でリダイレクト先を設定
			/*------------------------------------------------------------------------------------------------------------------------------*/
				// 総合トップだったら総合トップへ
				if(masterChannel == 'top'){
					if(this.alertFlag == true) movePC2SP.debug('SPV 非対象端末です。SPV 総合トップから PCV の総合トップにリダイレクトします。');
					location.href = '/';

				// ITM KW だったら ITM KW へ
				}else if(masterChannel == 'keywords'){
					if(this.alertFlag == true) movePC2SP.debug('SPV 非対象端末です。SPV のキーワードインデックスから PCV のキーワードインデックスにリダイレクトします。');
					location.href = this.path.replace('/spv','') + location.search;

				// TT KW だったら TT KW へ
				}else if(masterChannel == 'tt' && masterType == 'keywords' && this.url.indexOf('keywords') != -1){
					if(this.alertFlag == true) movePC2SP.debug('SPV 非対象端末です。SPV の TT KW から PCV の TT KW にリダイレクトします。');
					location.href = this.path.replace('/spv','') + location.search;

				// TT WPKW だったら TT WPKW へ
				}else if(masterChannel == 'tt' && masterType == 'keywords' && this.url.indexOf('wpkw') != -1){
					if(this.alertFlag == true) movePC2SP.debug('SPV 非対象端末です。SPV の TT WPKW から PCV の TT WPKW にリダイレクトします。');
					location.href = this.path.replace('/spv','') + location.search;

				// 編集特集で SPV 対応している場合編集特集へ
				}else if(masterType == 'subtop' && swspv == '1'){
					if(this.alertFlag == true) movePC2SP.debug('SPV 非対象端末です。SPV の編集特集から PCV の編集特集にリダイレクトします。');
					location.href = this.path.replace('/spv','');

				// 企画ミドルで SPV 対応している場合企画ミドルへ
				}else if(masterType == 'special' && swspv == '1'){
					if(this.alertFlag == true) movePC2SP.debug('SPV 非対象端末です。SPV の企画ミドルから PCV の企画ミドルにリダイレクトします。');
					location.href = this.path.replace('/spv','');

				// チャンネルの記事以外だったらチャンネルトップへ
				}else if(masterChannel != 'top' && masterType != 'article'){
					if(this.alertFlag == true) movePC2SP.debug('SPV 非対象端末です。SPV の' + masterChannel + 'チャンネルから PCV の' + masterChannel + 'チャンネルトップにリダイレクトします。');
					location.href = '/' + masterChannel + '/';

				// 記事だったら記事へ
				}else if(masterType == 'article'){
					if(this.alertFlag == true) movePC2SP.debug('SPV 非対象端末です。SPV の記事から PCV の記事にリダイレクトします。');

					// TT ルール
					if(masterChannel == 'tt'){
						location.href = this.path.replace('/spv','/news');
					}else{
						location.href = this.path.replace('/spv','/articles');
					}

				// エラーページだったら総合トップへ
				}else if(erFlag == 'true'){
					if(this.alertFlag == true) movePC2SP.debug('SPV 非対象端末です。SPV のエラーページから PCV の総合トップにリダイレクトします。');
					location.href = '/';

				// 上記以外のページだったら総合トップへ
				}else{
					if(this.alertFlag == true) movePC2SP.debug('SPV 非対象端末です。SPV のその他のページから PCV の総合トップにリダイレクトします。');
					location.href = '/';

				}
			/*------------------------------------------------------------------------------------------------------------------------------*/

			// 対象端末
			}else if(this.ua == true){
				
				// 初回アクセスもしくはフラグがない
				if(!movePC2SP.getCookie(data['name'])){
					
					// フラグ「sp」セット
					movePC2SP.setCookie({'name':data['name'],'value':'sp','domain':1,'path':'/','expires':7,'secure':0});
					if(this.alertFlag == true) movePC2SP.debug('SPV 対象端末です。初回アクセスです。なにもしません。');

				// フラグあり
				}else if(movePC2SP.getCookie(data['name'])){

					// フラグがSPビューの場合
					if(movePC2SP.getCookie(data['name']) == 'sp'){
						if(this.alertFlag == true) movePC2SP.debug('SPV 対象端末です。フラグが「sp」なのでなにもしません。');

					// フラグがPCビューの場合
					}else if(movePC2SP.getCookie(data['name']) == 'pc'){

					// 以下リダイレクト処理 SP 側の処理なので masterChannel , masterType , erFlag でリダイレクト先を設定
					/*------------------------------------------------------------------------------------------------------------------------------*/
						// 総合トップだったら総合トップへ
						if(masterChannel == 'top'){
							if(this.alertFlag == true) movePC2SP.debug('SPV 対象端末ですがフラグが「pc」です。SPV の総合トップから PCV の総合トップにリダイレクトします。');
							location.href = '/';
		
						// ITM KW だったら ITM KW へ
						}else if(masterChannel == 'keywords'){
							if(this.alertFlag == true) movePC2SP.debug('SPV 対象端末ですがフラグが「pc」です。SPV の ITM KW から PCV の ITM KW にリダイレクトします。');
							location.href = this.path.replace('/spv','') + location.search;

						// TT KW だったら TT KW へ
						}else if(masterChannel == 'tt' && masterType == 'keywords' && this.url.indexOf('keywords') != -1){
							if(this.alertFlag == true) movePC2SP.debug('SPV 対象端末ですがフラグが「pc」です。SPV の TT KW から PCV の TT KW にリダイレクトします。');
							location.href = this.path.replace('/spv','') + location.search;

						// TT WPKW だったら TT WPKW へ
						}else if(masterChannel == 'tt' && masterType == 'keywords' && this.url.indexOf('wpkw') != -1){
							if(this.alertFlag == true) movePC2SP.debug('SPV 対象端末ですがフラグが「pc」です。SPV の TT WPKW から PCV の TT WPKW にリダイレクトします。');
							location.href = this.path.replace('/spv','') + location.search;

						// 編集特集で SPV 対応している場合編集特集へ
						}else if(masterType == 'subtop' && swspv == '1'){
							if(this.alertFlag == true) movePC2SP.debug('SPV 対象端末ですがフラグが「pc」です。SPV の編集特集から PCV の編集特集にリダイレクトします。');
							location.href = this.path.replace('/spv','');

						// 企画ミドルで SPV 対応している場合企画ミドルへ
						}else if(masterType == 'special' && swspv == '1'){
							if(this.alertFlag == true) movePC2SP.debug('SPV 対象端末ですがフラグが「pc」です。SPV の企画ミドルから PCV 企画ミドルにリダイレクトします。');
							location.href = this.path.replace('/spv','');

						// チャンネルの記事以外だったらチャンネルトップへ
						}else if(masterChannel != 'top' && masterType != 'article'){
							if(this.alertFlag == true) movePC2SP.debug('SPV 対象端末ですがフラグが「pc」です。SPV の' + masterChannel + 'チャンネルから PCV の' + masterChannel + 'チャンネルトップにリダイレクトします。');
							location.href = '/' + masterChannel + '/';
		
						// 記事だったら記事へ
						}else if(masterType == 'article'){
							if(this.alertFlag == true) movePC2SP.debug('SPV 対象端末ですがフラグが「pc」です。SPV の記事から PCV の記事にリダイレクトします。');

							// TT ルール
							if(masterChannel == 'tt'){
								location.href = this.path.replace('/spv','/news');
							}else{
								location.href = this.path.replace('/spv','/articles');
							}

						// エラーページだったら総合トップへ
						}else if(erFlag == 'true'){
							if(this.alertFlag == true) movePC2SP.debug('SPV 対象端末ですがフラグが「pc」です。SPV のエラーページから PCV の総合トップにリダイレクトします。');
							location.href = '/';
		
						// 上記以外のページだったら総合トップへ
						}else{
							if(this.alertFlag == true) movePC2SP.debug('SPV 対象端末ですがフラグが「pc」です。SPV のその他のページから PCV の総合トップにリダイレクトします。');
							location.href = '/';
		
						}
					/*------------------------------------------------------------------------------------------------------------------------------*/

					}
				}

			}

		// PC ビュー
		}else if(this.mysite == 'pc'){

			// 非対象端末
			if(this.ua == false){
				if(this.alertFlag == true) movePC2SP.debug('PCV を PC で閲覧しています。なにもしません。');
				return false;

			// 対象端末
			}else if(this.ua == true){

				// サーバ種類
				if(this.url.match(/preview|localhost/)){
					var myServer = 'pre';
				}else{
					var myServer = 'www';
				}

				// ページタイプパターン
				if(myServer == 'pre'){
					var pcTop = this.path.match(/^\/(root\/)?(index[0-9]?\.html)?$/);
					var pcChTop = this.path.match(/^\/.+\/(root\/)?(index[0-9]?\.html)?$/);

					// TT ルール
					if(this.path.match(/^\/tt\//)){
						var pcChArticle = this.path.match(/news\/\d\d\d\d\/\d\d\//);
					}else{
						var pcChArticle = this.path.match(/articles\/\d\d\d\d\/\d\d\//);
					}

					var pcKeywords = this.path.match(/^\/keywords\//);
					var pcTTKW = this.path.match(/^\/tt\/keywords\//);
					var pcTTWPKW = this.path.match(/^\/tt\/wpkw\//);
					var myCh = this.pathcut[1];
				}else{
					var pcTop = this.url.match(/www\.itmedia\.co\.jp\/(index\.html)?$/);
					var ptype = (function(a){
						var list = movePC2SP.urllist;
						for(var i = 0;i < list.length; i++){
							if(list[i]['url'] == a){
								return {'url':true,'dir':list[i]['dir']};
							}
						}
						return {'url':false,'dir':''};
					})(this.url);
					var pcChTop = ptype['url'];

					// TT ルール
					if(this.path.match(/^\/tt\//)){
						var pcChArticle = this.path.match(/news\/\d\d\d\d\/\d\d\//);
					}else{
						var pcChArticle = this.path.match(/articles\/\d\d\d\d\/\d\d\//);
					}

					var pcKeywords = this.path.match(/^\/keywords\//);
					var pcTTKW = this.path.match(/^\/tt\/keywords\//);
					var pcTTWPKW = this.path.match(/^\/tt\/wpkw\//);
					var myCh = ptype['dir'];
				}

				// 初回アクセスもしくはフラグがない
				if(!movePC2SP.getCookie(data['name'])){

					// フラグ「sp」セット
					movePC2SP.setCookie({'name':data['name'],'value':'sp','domain':1,'path':'/','expires':7,'secure':0});

					// referrer をセットし sp 側の SC s.prop34 に代入する
					movePC2SP.setCookie({'name':'pc2sp_referrer','value':redirect_referrer,'domain':1,'path':'/','expires':7,'secure':0});

				// 以下リダイレクト処理 PC 側の処理なので path からリダイレクト先を設定
				/*------------------------------------------------------------------------------------------------------------------------------*/

					// 総合トップだったら総合トップへ
					if(pcTop){
						if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。初回アクセスです。PCV の総合トップから SPV の総合トップへリダイレクトします。');
						location.href = 'http://www.itmedia.co.jp/spv/';

					// ITM KW だったら ITM KW へ
					}else if(pcKeywords){
						if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。初回アクセスです。PCV の ITM KW から SPV の ITM KW へリダイレクトします。');
						location.href = this.path.replace('/keywords','/spv/keywords') + location.search;

					// TT KW だったら TT KW へ
					}else if(pcTTKW){
						if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。初回アクセスです。PCV の TT KW から SPV の TT KW へリダイレクトします。');
						location.href = this.path.replace('/tt/keywords','/tt/spv/keywords') + location.search;

					// TT WPKW だったら TT WPKW へ
					}else if(pcTTWPKW){
						if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。初回アクセスです。PCV の TT WPKW から SPV の TT WPKW へリダイレクトします。');
						location.href = this.path.replace('/tt/wpkw','/tt/spv/wpkw') + location.search;

					// 編集特集だったら編集特集へ
					}else if(swspv == '1'){
						if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。初回アクセスです。PCV の編集特集から SPV の編集特集へリダイレクトします。');
						var a = this.path.split('/');
						var b = [];
						for(var i = 0; i < a.length; i++){
							if(i == a.length - 1) b.push('spv');
							b.push(a[i]);
						}
						location.href = b.join('/');

					// チャンネルトップだったらのチャンネルトップへ
					}else if(pcChTop){
						if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。初回アクセスです。PCV のチャンネルトップから SPV のチャンネルトップへリダイレクトします。');
						location.href = '/' + myCh + '/spv/';

					// 記事だったら記事へ
					}else if(pcChArticle){
						if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。初回アクセスです。PCV の記事から SPV の記事へリダイレクトします。');

						// TT ルール
						if(this.path.match(/^\/tt\//)){
							location.href = this.path.replace(/\/news\//,'/spv/');
						}else{
							location.href = this.path.replace(/\/articles\//,'/spv/');
						}

					// 上記以外のページだったら PC ビューを見せる
					}else{
						if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。フラグが「sp」ですが対応するページがないためなにもしません。');
						return false;

					}
				/*------------------------------------------------------------------------------------------------------------------------------*/

				// フラグあり
				}else if(movePC2SP.getCookie(data['name'])){

					// フラグがSPビューの場合
					if(movePC2SP.getCookie(data['name']) == 'sp'){

						// referrer をセットし sp 側の SC s.prop34 に代入する
						movePC2SP.setCookie({'name':'pc2sp_referrer','value':redirect_referrer,'domain':1,'path':'/','expires':7,'secure':0});

					// 以下リダイレクト処理 PC 側の処理なので path からリダイレクト先を設定
					/*------------------------------------------------------------------------------------------------------------------------------*/

						// 総合トップだったら総合トップへ
						if(pcTop){
							if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。フラグが「sp」なので PCV の総合トップから SPV の総合トップへリダイレクトします。');
							location.href = 'http://www.itmedia.co.jp/spv/';

						// ITM KW だったら ITM KW へ
						}else if(pcKeywords){
							if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。フラグが「sp」なので PCV の ITM KW から SPV の ITM KW へリダイレクトします。');
							location.href = this.path.replace('/keywords','/spv/keywords') + location.search;

						// TT KW だったら TT KW へ
						}else if(pcTTKW){
							if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。フラグが「sp」なので PCV の TT KW から SPV の TT KW へリダイレクトします。');
							location.href = this.path.replace('/tt/keywords','/tt/spv/keywords') + location.search;
	
						// TT WPKW だったら TT WPKW へ
						}else if(pcTTWPKW){
							if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。フラグが「sp」なので PCV の TT WPKW から SPV の TT WPKW へリダイレクトします。');
							location.href = this.path.replace('/tt/wpkw','/tt/spv/wpkw') + location.search;

						// 編集特集だったら編集特集へ
						}else if(swspv == '1'){
							if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。フラグが「sp」なので PCV の編集特集から SPV の編集特集へリダイレクトします。');
							var a = this.path.split('/');
							var b = [];
							for(var i = 0; i < a.length; i++){
								if(i == a.length - 1) b.push('spv');
								b.push(a[i]);
							}
							location.href = b.join('/');

						// チャンネルトップだったらのチャンネルトップへ
						}else if(pcChTop){
							if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。フラグが「sp」なので PCV のチャンネルトップから SPV のチャンネルトップへリダイレクトします。');
							location.href = '/' + myCh + '/spv/';

						// 記事だったら記事へ
						}else if(pcChArticle){
							if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。フラグが「sp」なので PCV の記事から SPV の記事へリダイレクトします。');

							// TT ルール
							if(this.path.match(/^\/tt\//)){
								location.href = this.path.replace(/\/news\//,'/spv/');
							}else{
								location.href = this.path.replace(/\/articles\//,'/spv/');
							}

						// 上記以外のページだったら PC ビューを見せる
						}else{
							if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。フラグが「sp」ですが対応するページがないためなにもしません。');
							return false;

						}
					/*------------------------------------------------------------------------------------------------------------------------------*/

					// フラグがPCビューの場合
					}else if(movePC2SP.getCookie(data['name']) == 'pc'){
						if(this.alertFlag == true) movePC2SP.debug('PCV を SP で閲覧しています。フラグが「pc」なのでなにもしません。');

					}
				}
			}
		}
		return false;

};

/* movePC2SP 120209
-----------------------------------------------------------------------*/
function movePC2SP(data){
	movePC2SP.setting();
	movePC2SP.redirect(data);
	return false;
};

/* 実行
-----------------------------------------------------------------------*/
movePC2SP({'name':'pc2sp','loading':true});
