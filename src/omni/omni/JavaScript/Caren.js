function CLD_show(dateCtlId, dayOfStart,e) {
    
    var elm;
    for (i = 0; i < objclient.length; i++) {
        if (dateCtlId == objclient[i][1]) {
            elm = document.getElementById(objclient[i][0]);
            break;
        }
    }
    var cld = CLD_get();
    var d = new Date(elm.value);
    if (isNaN(d)) {
        d = new Date();
        d = new Date(d.getFullYear(), d.getMonth(), d.getDate());
    }

    cld.dateCtl = elm;
    cld.startDate = new Date(d);
    cld.showDate = new Date(d.getFullYear(), d.getMonth(), 1);
    if (!dayOfStart) cld.dayOfStart = 0;
    else cld.dayOfStart = dayOfStart;

    var pos = new CLD_AbsPos(elm);
    var topPos = 0;
    cld.style.left = pos.left;
    topPos = pos.top + elm.offsetHeight + 1 - pos.scrolltop;
    if (topPos > 510) {
        cld.style.top = pos.top - 104 - pos.scrolltop;
    } else {
        cld.style.top = pos.top + elm.offsetHeight + 1 - pos.scrolltop;
    }
    CLD_render();

    cld.style.display = 'block';
    cld.focus();

    return false;
}

function CLD_get() {
    var cld = document.getElementById('CLD__TABLE');
    if (cld) return cld;

    var dv = document.createElement('div');
    dv.innerHTML =
      '<table id="CLD__TABLE">'
    + '<col class="CLD_col" /><col class="CLD_col" /><col class="CLD_col" /><col class="CLD_col" /><col class="CLD_col" /><col class="CLD_col" /><col class="CLD_col" />'
    + '<tr class="CLD_HeadRow">'
    + '  <td class="CLD_HeadNavi">&lt;&lt;</td>'
    + '  <td class="CLD_HeadNavi">&lt;</td>'
    + '  <td class="CLD_HeadDate" colspan="3"></td>'
    + '  <td class="CLD_HeadNavi">&gt;</td>'
    + '  <td class="CLD_HeadNavi">&gt;&gt;</td>'
    + '</tr>'
    + '<tr class="CLD_DayRow"><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>'
    + '<tr class="CLD_DateRow"><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>'
    + '<tr class="CLD_DateRow"><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>'
    + '<tr class="CLD_DateRow"><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>'
    + '<tr class="CLD_DateRow"><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>'
    + '<tr class="CLD_DateRow"><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>'
    + '<tr class="CLD_DateRow"><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>'
    + '</table>'
    ;

    cld = dv.getElementsByTagName('TABLE').item(0);
    cld.onclick = function () { if (this.style.display != 'none') this.focus(); }
    cld.onblur = function () { if (!this.onmouse) this.style.display = 'none'; }
    cld.onmouseover = function () { this.onmouse = true; }
    cld.onmouseout = function () { this.onmouse = false; }
    cld.onselectstart = function () { return false; }

    cld.style.display = 'none'
    cld.style.position = 'absolute'
    cld.style.zIndex = '10'

    cld.rows(0).cells(0).onclick = CLD_render; cld.rows(0).cells(0).ondblclick = CLD_render; cld.rows(0).cells(0).direction = 'py';
    cld.rows(0).cells(1).onclick = CLD_render; cld.rows(0).cells(1).ondblclick = CLD_render; cld.rows(0).cells(1).direction = 'pm';
    cld.rows(0).cells(3).onclick = CLD_render; cld.rows(0).cells(3).ondblclick = CLD_render; cld.rows(0).cells(3).direction = 'nm';
    cld.rows(0).cells(4).onclick = CLD_render; cld.rows(0).cells(4).ondblclick = CLD_render; cld.rows(0).cells(4).direction = 'ny';

    document.body.appendChild(cld);
    return cld;
}

function CLD_render() {

    var cld = CLD_get();

    switch (window.event.srcElement.direction) {
        case 'py': cld.showDate.setYear(cld.showDate.getFullYear() - 1); break;
        case 'ny': cld.showDate.setYear(cld.showDate.getFullYear() + 1); break;
        case 'pm': cld.showDate.setMonth(cld.showDate.getMonth() - 1); break;
        case 'nm': cld.showDate.setMonth(cld.showDate.getMonth() + 1); break;
    }

    cld.rows(0).cells(2).innerText = cld.showDate.getFullYear() + '年 ' + (cld.showDate.getMonth() + 1) + '月'

    for (i = 0; i < 7; i++) {

        var cell = cld.rows(1).cells(i)

        switch ((i + cld.dayOfStart) % 7) {
            case 0: cell.innerText = '日'; cell.className = 'CLD_DaySun'; break;
            case 1: cell.innerText = '月'; cell.className = 'CLD_DayWeek'; break;
            case 2: cell.innerText = '火'; cell.className = 'CLD_DayWeek'; break;
            case 3: cell.innerText = '水'; cell.className = 'CLD_DayWeek'; break;
            case 4: cell.innerText = '木'; cell.className = 'CLD_DayWeek'; break;
            case 5: cell.innerText = '金'; cell.className = 'CLD_DayWeek'; break;
            case 6: cell.innerText = '土'; cell.className = 'CLD_DaySat'; break;
        }
    }

    // 描画開始日付を求める
    var d = new Date(cld.showDate.getYear(), cld.showDate.getMonth(), 1);
    var w = d.getDay();
    w -= cld.dayOfStart;
    if (w < 0) w += 7;
    d.setDate(d.getDate() - w);

    for (i = 0; i < (7 * 6); i++) {

        var cell = cld.rows(2 + Math.floor(i / 7)).cells(i % 7)

        if (d.toString() == cld.startDate.toString()) cell.className = 'CLD_DateStart';
        else if (d.getMonth() != cld.showDate.getMonth()) cell.className = 'CLD_DateOtherMonth';
        else if (d.getDay() == 6) cell.className = 'CLD_DateSat';
        else if (d.getDay() == 0) cell.className = 'CLD_DateSun';
        else cell.className = 'CLD_DateWeek';

        cell.date = new Date(d);
        cell.onclick = CLD_return;
        cell.innerText = d.getDate();

        d.setDate(d.getDate() + 1);
    }

}

function CLD_return() {
    var cld = CLD_get();
    var d = window.event.srcElement.date;
    var dt = cld.dateCtl;

    var yyyy = d.getFullYear();
    if ((d.getMonth() + 1) < 10) {
        var mm = "0" + (d.getMonth() + 1);
    } else {
        var mm = (d.getMonth() + 1);
    }
    if (d.getDate() < 10) {
        var dd = "0" + (d.getDate());
    } else {
        var dd = d.getDate();
    }
    dt.value = yyyy + '/' + mm + '/' + dd;
    cld.style.display = 'none';
    // 次のエレメントにフォーカス移動
    var NowElmID = getNowFocusElmID(dt.id);
    var elm = getNextFocusElm(NowElmID + 1);
    NGFocus = false;
    dt.onblur();
    elm.focus();
    
}

function CLD_AbsPos(obj) {
    this.left = 0;
    this.top = 0;
    this.scrolltop = 0;
    this.rewind = function (obj, start) {
        if (obj == document.body) return;
        if (obj == null) return;
        this.left += obj.offsetLeft;
        this.top += obj.offsetTop;
        this.scrolltop += obj.scrollTop;
        if (!start && (obj.tagName == 'TD' || obj.tagName == 'TH')) {
            this.left += obj.clientLeft;
            this.top += obj.clientTop;
        }
        this.rewind(obj.offsetParent, false);
    }
    this.rewind(obj, true);
}
