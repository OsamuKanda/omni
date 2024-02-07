/*
titleCutList.compareExcludeURL = function(urldata){
	var list = [
		"/ait/articles/1304/11/news020.html"
	];
	for(var i=0; i<list.length; i++){
		if(urldata.match(list[i])) return true;
	}
	return false
};
*/

// Reborn
// ========================================================
/* @IT �t�H�[�����f�[�^
------------------------------------------*/
function setForum(){
	return {
		'kw-smartandsocial':'Smart & Social',
		'kw-designhack':'�f�U�C���n�b�N',
		'kw-html5plusux':'HTML5�{UX',
		'kw-serverandstorage':'Server & Storage',
		'kw-windowsserverinsider':'Windows Server Insider',
		'kw-businessappinsider':'�Ɩ��A�v��Insider',
		'kw-insiderdotnet':'Insider.NET',
		'kw-systeminsider':'System Insider',
		'kw-railshub':'Rails Hub',
		'kw-codingedge':'Coding Edge',
		'kw-javaagile':'Java Agile',
		'kw-databaseexpert':'Database Expert',
		'kw-linuxandoss':'Linux �� OSS',
		'kw-masterofipnetwork':'Master of IP Network',
		'kw-securityandtrust':'Security & Trust',
		'kw-systemdesign':'System Design',
		'kw-testandtools':'Test & Tools',
		'kw-ljibun':'�����헪������',
		'kw-lcareer':'�L�����A����������',
		'kw-lskill':'�X�L���n��������',
		'kw-jibun':'�����헪������'
	};
}
function getForum(key){
	if(key == false || !key) return false;
	var a = setForum();
	var b = [];
	for(var i in a){
		if(key.match(i)){
			if(i=='kw-smartandsocial'){
				var path = '/ait/subtop/smart/';
				var forumid = 'smart';
			}else if(i=='kw-designhack'){
				var path = '/ait/subtop/ux/design/';
				var forumid = 'design';
			}else if(i=='kw-html5plusux'){
				var path = '/ait/subtop/ux/';
				var forumid = 'ux';
			}else if(i=='kw-serverandstorage'){
				var path = '/ait/subtop/server/';
				var forumid = 'server';
			}else if(i=='kw-windowsserverinsider'){
				var path = '/ait/subtop/win/';
				var forumid = 'win';
			}else if(i=='kw-businessappinsider'){
				var path = '/ait/subtop/dotnet/app/';
				var forumid = 'app';
			}else if(i=='kw-insiderdotnet'){
				var path = '/ait/subtop/dotnet/';
				var forumid = 'dotnet';
			}else if(i=='kw-systeminsider'){
				var path = '/ait/subtop/sys/';
				var forumid = 'sys';
			}else if(i=='kw-railshub'){
				var path = '/ait/subtop/coding/rails/';
				var forumid = 'rails';
			}else if(i=='kw-codingedge'){
				var path = '/ait/subtop/coding/';
				var forumid = 'coding';
			}else if(i=='kw-javaagile'){
				var path = '/ait/subtop/java/';
				var forumid = 'java';
			}else if(i=='kw-databaseexpert'){
				var path = '/ait/subtop/db/';
				var forumid = 'db';
			}else if(i=='kw-linuxandoss'){
				var path = '/ait/subtop/linux/';
				var forumid = 'linux';
			}else if(i=='kw-masterofipnetwork'){
				var path = '/ait/subtop/network/';
				var forumid = 'network';
			}else if(i=='kw-securityandtrust'){
				var path = '/ait/subtop/security/';
				var forumid = 'security';
			}else if(i=='kw-systemdesign'){
				var path = '/ait/subtop/systemdesign/';
				var forumid = 'systemdesign';
			}else if(i=='kw-testandtools'){
				var path = '/ait/subtop/testtools/';
				var forumid = 'testtools';
			}else if(i=='kw-ljibun'){
				var path = '/ait/subtop/jibun/ljibun/';
				var forumid = 'ljibun';
			}else if(i=='kw-lcareer'){
				var path = '/ait/subtop/jibun/lcareer/';
				var forumid = 'lcareer';
			}else if(i=='kw-lskill'){
				var path = '/ait/subtop/jibun/lskill/';
				var forumid = 'lskill';
			}else if(i=='kw-jibun'){
				var path = '/ait/subtop/jibun/';
				var forumid = 'jibun';
			}else{
				return false;
			}
		b.push({'extraid':i,'path':path,'forumid':forumid,'name':a[i]});
		}
	}
	if(b.length == 0) return false;
	return b;
}

// �L�����X�g�A�C���f�b�N�X��Ԃ�
function outputColIndexParts(json){
	if(!json) return false;
	var html = outputColIndexParts[json.type](json.data,json.num,json.cutnum);
	for(var i=0; i<json.id.length; i++){
		if(document.getElementById(json.id[i])) document.getElementById(json.id[i]).innerHTML = html;
	}
	return true;
};
// ���X�g�`��
outputColIndexParts.list = function(data,num,cutnum){
	for(var i=0, html="", len=data.length; i<num && i<len; i++){
		var title = cutnum ? cutString(data[i].title,40) : data[i].title;
		html += '<li class="num' + (i+1) + '"><a href="' + data[i].link + '" title="' + data[i].title + '">' + title + '</a></li>';
	}
	return html;
};
// colBox�`��
outputColIndexParts.defaultBox = function(){};

// �C�x���g�J�����_�[�p
function eventCal(json){
	if(!json) return false;
	var html = ""
	for(var i=0, html="", len=json.data.length; i<5 && i<len; i++){
		var title = cutString(json.data[i].title,40);
		html += '<li class="rank' + (i+1) + '"><a href="' + json.data[i].link + '" title="' + json.data[i].title + '">' + title + '</a></li>';
	}
	// �E�J�����p
	if(document.getElementById("js-rcolCalendarIndex")) document.getElementById("js-rcolCalendarIndex").innerHTML = html;
	// TOP�p���C���J�����p
	if(document.body.id.match("masterType-top") && document.getElementById("js-topCalendarIndex")) document.getElementById("js-topCalendarIndex").innerHTML = html;
};

// �^�u�؂�ւ�
function setTabAction(id){
	if(!id || !document.getElementById(id)) return false;
	var tgt = document.getElementById(id);
	var tab = tgt.getElementsByTagName('h2');
	for(var i=0,len=tab.length; i<len; i++){
		tab[i].onclick = setTabAction.changeTab;
	}
};
setTabAction.changeTab = function(){
	var self = jQuery(this);
	self.parent().children("h2").removeClass("active");
	self.addClass("active");
	var tgt = self.parent().parent().children("div.colBoxInner").children("div.colBoxIndex");
	tgt.removeClass('active');
	jQuery(tgt[self.index()]).addClass('active');
	/*
	var tgt1 = this.parentNode.parentNode.children;
	var tgt2;
	for(var i=0,len=tgt1.length; i<len; i++){
		if(tgt1[i].className.match("colBoxInner")){
			tgt2 = tgt1[i].children;
			break;
		}
	}
	if(!tgt2) return false;
	for(var i=0,len=tgt2.length; i<len; i++){
		tgt2[i]
	}
	*/
};

// �t�H�[�����ʂ̃��S���o��
function outputForumLogo(){
	var metadata = getForum(getMETA('extraid').content),
		tgt = jQuery("#colBoxSubChannelLogo"),
		aTag = jQuery(document.createElement("a"));
	if(metadata == false) return false;
	for(var i = 0; i < metadata.length; i++){
		aTag.attr("href", metadata[i]['path']).attr("title", metadata[i]['name']).html(metadata[i]['name']).css("background-image","url(" + imgSrv() + "/ait/images/title_forum_small_" + metadata[i]['forumid'] + ".gif)");
		tgt.append(aTag);
		if(metadata[i]['path'].match('/ait/subtop/dotnet/app/')){
			var divTag = jQuery(document.createElement("div")).addClass("colBoxSubChannelSponsor").html("<h2>Supported by �O���[�v�V�e�B</h2>");
			tgt.append(divTag);
		}
		break;
	}
	return true;
};

// �t�H�[�����L�������L���O�p��json�𕪊�
function setForumRanking(){
	if(masterType() == "top") return false;
	if(masterType() == "article") {
		var fdata = getForum(getMETA('extraid').content)[0];
		if(!fdata) return false;
		var forum = fdata.forumid;
	} else if(masterType() == "subtop"){
		var a = getUrl()['path'];
		var forum = "";
		if(a.match('/ait/subtop/smart/')){
			forum = 'smart';
		}else if(a.match('/ait/subtop/ux/design/')){
			forum = 'design';
		}else if(a.match('/ait/subtop/ux/')){
			forum = 'ux';
		}else if(a.match('/ait/subtop/server/')){
			forum = 'server';
		}else if(a.match('/ait/subtop/win/')){
			forum ='win';
		}else if(a.match('/ait/subtop/dotnet/app/')){
			forum = 'app';
		}else if(a.match('/ait/subtop/dotnet/')){
			forum = 'dotnet';
		}else if(a.match('/ait/subtop/sys/')){
			forum = 'sys';
		}else if(a.match('/ait/subtop/coding/rails/')){
			forum = 'rails';
		}else if(a.match('/ait/subtop/coding/')){
			forum = 'coding';
		}else if(a.match('/ait/subtop/java/')){
			forum = 'java';
		}else if(a.match('/ait/subtop/db/')){
			forum = 'db';
		}else if(a.match('/ait/subtop/linux/')){
			forum = 'linux';
		}else if(a.match('/ait/subtop/network/')){
			forum = 'network';
		}else if(a.match('/ait/subtop/security/')){
			forum = 'security';
		}else if(a.match('/ait/subtop/systemdesign/')){
			forum = 'systemdesign';
		}else if(a.match('/ait/subtop/testtools/')){
			forum = 'testtools';
		}else if(a.match('/ait/subtop/jibun/ljibun/')){
			forum = 'ljibun';
		}else if(a.match('/ait/subtop/jibun/lcareer/')){
			forum = 'lcareer';
		}else if(a.match('/ait/subtop/jibun/lskill/')){
			forum = 'lskill';
		}else if(a.match('/ait/subtop/jibun/')){
			forum = 'jibun';
		}
	}
	if(forum) document.write('<script type="text/javascript" src="/json/ait/rss_reborn_' + forum +'_ranking.json"><\/script>');
	return true;
};

// �t�H�[�����L�������L���O�o��
function outputForumRanking(json){
	if(!json) return false;
	var data = json.data,
		html = "";
	for(var i=0,len=data.length; i<len; i++){
		if(!data[i].link) continue;
		html += '<li class="rank' + (i+1) + '"><a href="' + data[i].link + '" title="' + data[i].title + '">' + data[i].title + '</a></li>';
	}
	document.getElementById("forumRanking").innerHTML = '<div class="colBoxOuter">'
														+ '<div class="colBoxHead"><h2>�L�������L���O</h2></div>'
														+ '<div class="colBoxInner"><div class="colBoxIndex"><div class="colBoxUlist"><ul>'
														+ html
														+ '</ul></div></div></div>'
														+ '</div>';
	return true;
};

// �g���� HTML�G�X�P�[�v�΍�
function outputTrain(json){
	if(!json) return false;
	var data = json.data,
		html = "";
	
	for(var i=0; i<data.length; i++){
		if(!data[i].link) continue;
		html += '<div class="colBoxTitle"><h3><a href="' + data[i].link + '" title="' + data[i].category + '">' + data[i].category + '</a></h3></div>'
				+ '<div class="colBoxDescription"><p>' + data[i].description.replace(/</g,'&lt').replace(/>/g,'&gt') + '<span class="colBoxDate">�i' + data[i].yyyy + '/' + data[i].mm + '/' + data[i].dd + '�j</span></p></div>'
				+ '<div class="colBoxAnsBtn"><a href="' + data[i].link + '" title="���ɒ��킷��">���ɒ��킷��</a></div>'
				+ '<div class="colBoxClear"></div>';
	};
	document.getElementById('colBoxITTRAIN').innerHTML = html;
	return true;
};

// �}�[�W�����L���O
function margeArtRanking(json){
	// �f�[�^������ꍇ�ɂ͔z��ɒǉ�
	if(json) return margeArtRanking.setAry(json.data);
	if(margeArtRanking.artObj.length == 0) return false;
	
	// �L������t���Ƀ\�[�g
	margeArtRanking.sortAry();
	
	// �����L���O���o��
	margeArtRanking.output();
};
margeArtRanking.artObj = [];
margeArtRanking.setAry = function(data){
	for(var i=0; i<data.length; i++){
		if(data[i].link) margeArtRanking.artObj.push(data[i]);
	}
	return true;
};
margeArtRanking.sortAry = function(){
	margeArtRanking.artObj.sort(
		function(a,b){
			return a.date > b.date ? -1 : 1;
		}
	);
};
margeArtRanking.output = function(){
	var html = "",
		count = 0;
	for(var i=0,len=margeArtRanking.artObj.length; i<len && count<10; i++){
		if(!margeArtRanking.artObj[i].link) continue;
		// �^�C�v�ʕ���
		if(margeArtRanking.artObj[i].subject == 'matome') {
			var typename="type-matome";
		} else if(margeArtRanking.artObj[i].subject == 'event') {
			var typename="type-event";
		} else if(margeArtRanking.artObj[i].subject == 'news') {
			var typename="type-news";
		}
		html += '<li class="' + typename + '"><a href="' + margeArtRanking.artObj[i].link + '" title="' + margeArtRanking.artObj[i].title + '"  onclick="designCnt(\'margeRanking\',\'' + typename + '\');">' + margeArtRanking.artObj[i].title + '</a></li>';
		count++;
	}
	document.getElementById("margeRanking").innerHTML = '<div class="colBoxOuter">'
														+ '<div class="colBoxHead"><h2>News/�܂Ƃ߁�IT/�C�x���g���O</h2></div>'
														+ '<div class="colBoxInner"><div class="colBoxIndex"><div class="colBoxUlist"><ul>'
														+ html
														+ '</ul></div></div></div>'
														+ '</div>';
	return true;
};

// base.js��snsContents�֐��̃��b�p�[
function snsTrigger(boxwidth){
	var type = masterType();
	if(!type) return false;
	
	if(type == "top") {
		var a = getForum(getMETA('extraid').content).path;
	} else if(type == "article") {
		var a = getForum(getMETA('extraid').content)[0].path;
	} else if(type == "subtop"){
		var a = getUrl()['path'];
	}
	
	// �eSNS�̃I�v�V�������擾
	var params = getSnsOption(a);
	
	snsContents({
		width:boxwidth+"px",
		likebox: params.likebox,
		activitydomain: params.activitydomain,
		activityfilter: params.activityfilter,
		twitterid: params.twitterid,
		tw_widget_id: params.tw_widget_id
	});
	
	return true;
};

// SNS�̃t�H�[�������Ƃ́ASNS�I�v�V������Ԃ�
function getSnsOption(a){
	var params = {};
	if(!a) {
		params.likebox = 'atmarkit';
		params.activitydomain = 'http://www.atmarkit.co.jp/';
		params.activityfilter = '';
		params.twitterid = 'atmark_it';
		params.tw_widget_id = '306296222484926464';
	} else if(a.match('/ait/subtop/win/')) {
		params.likebox = 'WindowsInsider';
		params.activitydomain = 'http://www.atmarkit.co.jp/fwin2k/';
		params.activityfilter = '';
		params.twitterid = 'atmark_it';
		params.tw_widget_id = '306296222484926464';
	} else if(a.match('/ait/subtop/smart/')) {
		params.likebox = 'atmarkit.smaso';
		params.activitydomain = 'http://www.atmarkit.co.jp/fsmart/';
		params.activityfilter = '';
		params.twitterid = 'sma_so';
		params.tw_widget_id = '306252324928618497';
	} else if(a.match('/ait/subtop/ux/')) {
		params.likebox = 'Html5Ux';
		params.activitydomain = 'http://www.atmarkit.co.jp/fwcr/';
		params.activityfilter = '';
		params.twitterid = 'd89meeting';
		params.tw_widget_id = '306304603518017536';
	} else if(a.match('/ait/subtop/ux/design/')) {
		params.likebox = 'Html5Ux';
		params.activitydomain = 'http://www.atmarkit.co.jp/fwcr/';
		params.activityfilter = '';
		params.twitterid = 'd89meeting';
		params.tw_widget_id = '306304603518017536';
	} else if(a.match('/ait/subtop/dotnet/')) {
		params.likebox = 'devchu';
		params.activitydomain = 'http://www.atmarkit.co.jp/fdotnet/chushin/';
		params.activityfilter = '';
		params.twitterid = 'devchu';
		params.tw_widget_id = '303798345053376512';
	} else if(a.match('/ait/subtop/dotnet/app/')) {
		params.likebox = 'devchu';
		params.activitydomain = 'http://www.atmarkit.co.jp/fdotnet/chushin/';
		params.activityfilter = '';
		params.twitterid = 'devchu';
		params.tw_widget_id = '303798345053376512';
	} else {
		params.likebox = 'atmarkit';
		params.activitydomain = 'http://www.atmarkit.co.jp/';
		params.activityfilter = '';
		params.twitterid = 'atmark_it';
		params.tw_widget_id = '306296222484926464';
	}
	return params;
};

function designCnt(pos,opt) {
	if(designCnt.exc_list[pos] != true) return false;

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
	s.tl(this,'e',a);
	s_objectid = a;

};

designCnt.exc_list = {
	'alertBtnTest201312':true /* �A�ڃA���[�g */
};

