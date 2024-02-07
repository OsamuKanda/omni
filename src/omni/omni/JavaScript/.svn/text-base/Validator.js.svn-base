var Validator = {
    check: function(e, reg) {
        var res = true;
        var vali = this.vali;
        vali.field = e;
        vali.value = e.value;

        // textbox,selectbox,textarea以外はチェックしない
        if ((e.type != "text") && (e.tagName != "SELECT") && (e.tagName != 'TEXTAREA')) {
            var retVal = new Array;
            retVal[0] = res;
            retVal[1] = vali.value;
            retVal[2] = vali.help;
            return retVal;
        }

        if (reg == '') {
            res = vali.input();
        } else {
            // 必須チェック
            if (!reg.match(/^[!#]/)) {
                if (reg.match(/date/g)) {
                    // 規定値入力
                    // 日付入力の場合は、当日をセットするようにする。
                    if (vali.value == '') {
                        res = true;
                        vali.value = ' ';
                    }
                } else if (reg.match(/allzero/g)) {
                    // 規定値入力
                    // 0埋めの場合は、ALL0をセットするようにする。
                    if (vali.value == '') {
                        vali.value = '0';
                        res = true;
                    }
                    reg = reg.replace(/allzero /g, '');
                } else {
                    res = vali.input();
                }
            }
        }

        // ValiDate Check
        if ((reg != '') && (res == true) && (vali.value != '')) {
            reg = reg.replace(/^[!#]/, '');

            var mode = reg.split(/\s+/);
            var str;
            for (var i = 0; str = mode[i]; i++) {
                str = str.replace(/([\_\_][\w\-\_\#\.]+)$/, '');
                res = vali[str](RegExp.$1);
                if (res == false) {
                    break;
                }
            }
        }
        var retVal = new Array;
        retVal[0] = res;
        if (res == true) {
            retVal[1] = vali.value;
        } else {
            retVal[1] = e.value;
        }
        return retVal;
    }
}


Validator.vali = {

    // input NULL check
    input: function() {
        if (this.value == '') {
            return false;
        }
        return true;
    },

    // byteCount check
    bytecount: function(arg) {
        var strVal = this.value;
        var nCount = 0;
        var i;
        for (i = 0; i < strVal.length; i++) {
            var code = strVal.charCodeAt(i);
            // Shift-JIS : 0x0 ～ 0x80, 0x00a1 ～ 0x00df (0x00a0,0x00fd～0x00fe)
            // Unicode   : 0x0 ～ 0x80, 0xff61 ～ 0xff9f (0xf8f0～0xf8f2)
            if ((code >= 0x0 && code <= 0x80) || (code >= 0xff61 && code <= 0xff9f)) {
                nCount += 1;
            } else {
                nCount += 2;
            }
        }

        var nByte = arg.replace(/\_/g, '');
        if (nCount > parseInt(nByte)) {
            return false;
        }

        return true;
    },

    // num check
    num: function(arg) {
        // function funcfNum(e, lenu, lend, fmt, aj) {
        var i;
        var strVal = this.value;
        var strArg;
        var bRet = true;

        arg = arg.slice(2);
        strArg = arg.split('_');
        var lenu = strArg[0].substr(0, 2);
        var lend = strArg[0].substr(2, 2);
        var fmtF = strArg[0].substr(4, 1);
        var fmtC = strArg[0].substr(5, 1);

        // 符号を取得する
        var strF = '';
        if (strVal.substring(0, 1) == '-') {
            strF = '-';
            if (fmtF != '1') {
                bRet = false;
            }
            strVal = strVal.slice(1);
            if (strVal.length < 1) {
                bRet = false;
            }
        }


        // 少数部と分ける
        var strNum = '';
        var strTmp;
        if (lend > 0) {
            strTmp = strVal.split('.');
            if (strTmp.length > 2) {
                bRet = false;
            } else if (strTmp.length == 1) {
                strTmp[1] = '0';
            }
            strNum = strTmp[0];
        } else {
            strNum = strVal;
        }


        // 不正文字のチェックを行う
        //区切り文字を削除
        strNum = strNum.replace(/[,]/g, '');
        //　整数桁が数値のみか確認
        if (strNum.match(/[^0-9]/)) {
            bRet = false;
        } else {
            // 先頭の0を削除
            strNum = strNum.replace(/^0+/g, '');
            if (strNum.length == 0) {
                strNum = '0';
            }
        }

        if (lend > 0) {
            if (strTmp[1].match(/[^0-9]/)) {
                bRet = false;
            }
        }

        // 入力桁チェック
        if (lenu < strNum.length) {
            bRet = false;
        }

        // 少数桁をあわせる
        strTmp2 = '';
        if (lend > 0) {
            var nTmp2 = 0;
            var nTmp = strTmp[1].length;
            if (lend < nTmp) {
                bRet = false;
            } else if (lend > nTmp) {
                nTmp2 = lend - nTmp;
                for (i = 0; i < nTmp2; i++) {
                    strTmp2 += '0';
                }
                strTmp[1] = strTmp[1] + strTmp2;
            }
        }
        if (bRet == false) {
            return bRet;
        }

        // 最終の補正を行う
        if (fmtC == '1') {
            // 3桁区切り対応
            while (strNum != (strNum = strNum.replace(/^(\d+)(\d{3})/, '$1,$2')));
        }

        //文字列を結合
        if (lend > 0) {
            this.value = strF + strNum + '.' + strTmp[1];
        } else {
            this.value = strF + strNum;
        }
        return this._numrange(strArg[1], this.value);
    },

    // inZero check
    numzero: function(arg) {
        // valueが空白の場合はALL0を返す
        if (!this.value.match(/^[\d]+$/)) {
            return false;
        }
        var nLength = parseInt(arg.replace(/[\_]/g, ''));
        var strVal = this.value;
        var strZero = '';
        if (nLength > strVal.length) {
            var nNum;
            nNum = nLength - strVal.length;
            for (i = 0; i < nNum; i++) {
                strZero += '0';
            }
            this.value = strZero + strVal;
        }
        return true;
    },

    // alphabet string check
    alphabet: function() {
        if (!this.value.match(/^[a-zA-Z\d]+$/)) {
            return false;
        }
        return true;
    },

    // hankaku string check
    han: function(arg) {
        var strVal = this.value;
        var nCount = 0;
        var i;
        var bRet = true;
        for (i = 0; i < strVal.length; i++) {
            var code = strVal.charCodeAt(i);
            // Shift-JIS : 0x0 ～ 0x80, 0x00a1 ～ 0x00df (0x00a0,0x00fd～0x00fe)
            // Unicode   : 0x0 ～ 0x80, 0xff61 ～ 0xff9f (0xf8f0～0xf8f2)
            if ((code >= 0x0 && code <= 0x80) || (code >= 0xff61 && code <= 0xff9f)) {
                nCount += 1;
            } else {
                nCount += 2;
                bRet = false;
            }
        }
        var nByte = arg.replace(/\_/g, '');
        if (nCount > parseInt(nByte)) {
            bRet = false;
        }
        return bRet;
    },


    // date check
    date: function(arg) {
        var i;
        var bRet = true;
        var strVal = this.value;
        var bCheck = true;
        // valueが空白の場合は当日を返す
        var strW, valy, valm, vald;
        if (strVal == ' ') {

            var today = new Date();
            valy = String(today.getFullYear());
            valm = String(today.getMonth() + 1);
            vald = String(today.getDate());
        } else {
            if (strVal.match(/[^0-9\/]/)) {
                bRet = false;
                return bRet;
            } else {
                strW = strVal.split('/');
                valy = strW[0] || '';
                valm = strW[1] || '';
                vald = strW[2] || '';
            }

            if (valy.length > 4) {
                if (valm != '') { bCheck = false; }
            }

            if (valy.length == 1) {
                if ((valm != '') && (vald == '')) {
                    // m_dd
                    vald = valm;
                    valm = valy;
                    valy = this._getdateYear(valm);
                }
            } else if (valy.length == 2) {
                if ((valm != '') && (vald != '')) {
                    // yy_mm_dd
                    if (parseInt(valy) >= 70) {
                        valy = '19' + valy;
                    } else {
                        valy = '20' + valy;
                    }
                } else {
                    // mm_dd
                    vald = valm;
                    valm = valy;
                    valy = this._getdateYear(valm);
                }

            } else if (valy.length == 4) {
                // yyyy_mm_dd
                if (valm == '') {
                    // mmdd
                    var today = new Date();
                    valm = valy.substr(0, 2);
                    vald = valy.substr(2, 2);
                    valy = this._getdateYear(valm);
                }
            } else if (valy.length == 6) {
                // yymmdd
                valm = valy.substr(2, 2);
                vald = valy.substr(4, 2);
                valy = valy.substr(0, 2);
                if (parseInt(valy) >= 70) {
                    valy = '19' + valy;
                } else {
                    valy = '20' + valy;
                }
            } else if (valy.length == 8) {
                // yyyymmdd
                vald = valy.substr(6, 2);
                valm = valy.substr(4, 2);
                valy = valy.substr(0, 4);
            } else {
                // error
                bCheck = false;
            }

            if ((valm == '') || (vald == '')) {
                bCheck = false;
            } else {
                valy = parseInt(valy, 10);
                valm = parseInt(valm, 10);
                vald = parseInt(vald, 10);
            }
            if (bCheck == false) {
                bRet = false;
            }

            // 日付チェック
            if (valm > 12 || valm < 1) {
                // month check
                bRet = false;
            }

            if (vald > 31 || vald < 1) {
                // day check
                bRet = false;
            }

            if ((valm == 4 || valm == 6 || valm == 9 || valm == 11) && vald > 30) {
                // day check
                bRet = false;
            }

            if (valm == 2 && vald > 28) {
                if ((valy % 100) == 0) {
                    bRet = false;
                } else if ((valy % 4) != 0) {
                    bRet = false;
                } else {
                    if (vald != 29) {
                        bRet = false;
                    }
                }
            }
        }
        if (bRet == true) {
            valy = String(valy);
            valm = String(valm);
            vald = String(vald);
            if (valm.length == 1) {
                valm = '0' + valm;
            }
            if (vald.length == 1) {
                vald = '0' + vald;
            }
        } else {
            return false;
        }

        this.value = valy + '/' + valm + '/' + vald;
        return this._daterange(arg, this.value);
    },
    // 日付年月チェック
    dateYYMM: function(arg) {
        var i;
        var bRet = true;
        var strVal = this.value;
        var bCheck = true;
        // valueが空白の場合は当月を返す
        var strW, valy, valm, vald;
        if (strVal == ' ') {
            var today = new Date();
            valy = String(today.getFullYear());
            valm = String(today.getMonth() + 1);
        } else {
            if (strVal.match(/[^0-9\/]/)) {
                bRet = false;
                return bRet;
            } else {
                strW = strVal.split('/');
                valy = strW[0] || '';
                valm = strW[1] || '';
            }

            if (valy.length > 4) {
                if (valm != '') { bCheck = false; }
            }

            if (valy.length == 2) {
                if (valm != '') {
                    // yy_mm
                    if (parseInt(valy) >= 70) {
                        valy = '19' + valy;
                    } else {
                        valy = '20' + valy;
                    }
                }
            } else if (valy.length == 4) {
                if (valm == '') {
                    // mmdd
                    var today = new Date();
                    valm = valy.substr(2, 2);
                    valy = valy.substr(0, 2);
                    if (parseInt(valy) >= 70) {
                        valy = '19' + valy;
                    } else {
                        valy = '20' + valy;
                    }
                }
            } else if (valy.length == 6) {
                // yyyymm
                valm = valy.substr(4, 2);
                valy = valy.substr(0, 4);
            } else {
                // error
                bCheck = false;
            }

            if (valm == '') {
                bCheck = false;
            } else {
                valy = parseInt(valy, 10);
                valm = parseInt(valm, 10);
            }
            if (bCheck == false) {
                bRet = false;
            }

            // 日付チェック
            if (valm > 12 || valm < 1) {
                // month check
                bRet = false;
            }
            // 年チェック
            if (valy > 2099 || valy < 1970) {
                // year check
                bRet = false;
            }
        }
        if (bRet == true) {
            valy = String(valy);
            valm = String(valm);
            if (valm.length == 1) {
                valm = '0' + valm;
            }
        } else {
            return false;
        }

        this.value = valy + '/' + valm;
        return true;
    },
    // 日付年月チェック
    dateMMDD: function(arg) {
        var i;
        var bRet = true;
        var strVal = this.value;
        var bCheck = true;
        // valueが空白の場合は当日を返す
        var strW, valy, valm, vald;
        if (strVal == ' ') {
            var today = new Date();
            valy = String(today.getFullYear());
            valm = String(today.getMonth() + 1);
            vald = String(today.getDate());
        } else {
            if (strVal.match(/[^0-9\/]/)) {
                bRet = false;
                return bRet;
            } else {
                strW = strVal.split('/');
                valm = strW[0] || '';
                vald = strW[1] || '';
            }

            if (valm.length == 1) {
                // m_dd

            } else if (valm.length == 2) {
                //mm_dd
            } else if (valm.length == 3) {
                // mdd
                if (vald == '') {
                    vald = valm.substr(1, 2);
                    valm = valm.substr(0, 1);
                }
            } else if (valm.length == 4) {
                // mmdd
                if (vald == '') {
                    vald = valm.substr(2, 2);
                    valm = valm.substr(0, 2);
                }
            } else {
                // error
                bCheck = false;
            }

            if (vald == '') {
                bCheck = false;
            } else {
                valm = parseInt(valm, 10);
                vald = parseInt(vald, 10);
            }
            if (bCheck == false) {
                bRet = false;
            }

            // 日付チェック
            if (valm > 12 || valm < 1) {
                // month check
                bRet = false;
            }

            if (vald > 31 || vald < 1) {
                // day check
                bRet = false;
            }

            if ((valm == 4 || valm == 6 || valm == 9 || valm == 11) && vald > 30) {
                // day check
                bRet = false;
            }

            if (valm == 2 && vald > 29) {
                bRet = false;
            }
        }
        if (bRet == true) {
            valm = String(valm);
            vald = String(vald);
            if (valm.length == 1) {
                valm = '0' + valm;
            }
            if (vald.length == 1) {
                vald = '0' + vald;
            }
        } else {
            return false;
        }

        this.value = valm + '/' + vald;
        return true;
    },
    // time check
    time: function(arg) {
        var i;
        var bRet = true;
        var strVal = this.value;
        var bCheck = true;

        var strW, valy, valm, vald;
        if (strVal.match(/[^0-9\:]/)) {
            bRet = false;
            return bRet;
        } else {
            strW = strVal.split(':');
            valh = strW[0] || '00';
            valm = strW[1] || '00';
        }

        if (valh.length == 1) {
            // h_mm
            if (valm == '') {
                bRet = false;
            } else {
                valh = '0' + valh;
                if (valm.length == 1) {
                    valm = '0' + valm;
                } else if (valm >= 3) {
                    bRet = false;
                }
            }
        } else if (valh.length == 2) {
            // hh_mm
            if (valm == '') {
                bRet = false;
            } else {
                if (valm.length == 1) {
                    valm = '0' + valm;
                } else if (valm.length >= 3) {
                    bRet = false;
                }
            }
        } else if (valh.length == 3) {
            if (valm == '00') {
                // hmm
                valm = valh.substr(1, 2);
                valh = '0' + valh.substr(0, 1);
            }
        } else if (valh.length == 4) {
            // hhmm
            valm = valh.substr(2, 2);
            valh = valh.substr(0, 2);
        } else {
            bRet = false;
        }
        // 分のチェック
        if (valm >= 60) {
            bRet = false;
        }

        if (bRet == false) {
            return false;
        }
        this.value = valh + ':' + valm;
        return true;
        //return this._daterange(arg, this.value);
    },
    //郵便番号
    zipcode: function(arg) {
        var strVal = this.value;
        var bCheck = true;
        // valueが空白の場合は当日を返す
        var strW, valy, valf;
        if (strVal.match(/[^0-9\-]/)) {
            return false;
        } else {
            strW = strVal.replace('-', '');
        }

        if (strW.length == 7) {
            // yyy_ffff
            valy = strW.substr(0, 3);
            valf = strW.substr(3, 4);
            this.value = valy + '-' + valf;
            return true;
        } else {
            return false;
        }
    },
    _getdateYear: function(strMonth) {
        var rety = '';
        var today = new Date();

//(HIS-106)>>
//        if ((parseInt(strMonth) <= 2) && ((today.getMonth() + 1) >= 10)) {
//            rety = String(today.getFullYear() + 1);
//        } else {
//            rety = String(today.getFullYear());
//        }
            rety = String(today.getFullYear());
//<<(HIS-106)

        return rety;
    },
    _daterange: function(range, value) {

        var nMax, nMin;
        var oRange;
        var nRange = range.replace(/\_\_/, '');
        if (range == '__') {
            nMin = 19700101;
            nMax = 20991231;
        } else {
            oRange = ('' + nRange).split(/\-/);
            nMin = parseInt(oRange[0]) || 19700101;
            nMax = parseInt(oRange[1]) || 20991231;

            if (nMin == NaN) {
                nMin = this._dategetnum(oRange[0], 0);
            }
            if (nMax == NaN) {
                nMax = this._dategetnum(oRange[1], 1);
            }
        }
        if ((isNaN(nMin)) || (nMin <= 19700101) || (nMin >= 20991231)) {
            nMin = 19700101;
        }
        if ((isNaN(nMin)) || (nMax >= 20991231) || (nMax <= 19700101)) {
            nMax = 20991231;
        }
        if (nMax < nMin) {
            nMin = nMax;
        }
        var bRet = true;
        value = parseInt(value.replace(/[\/]/g, ''));
        if (value < nMin || value > nMax) {
            bRet = false;
        }
        return bRet;
    },

    _dategetnum: function(arg, maxmin) {
        var elm = getElm(arg);
        var retVal = 0;
        if (elm != null) {
            var elmVal = elm.value;
            elmVal = elmVal.replace(/[\/]/g, '');
            if (!isNaN(elmVal)) {
                retVal = parseInt(elmVal);
            }
        }
        if (retVal == 0) {
            if (maxmin == 0) {
                retVal = 19700101;
            } else {
                retVal = 20991231;
            }
        }

        return retVal;

    },

    _numrange: function(range, value) {
        var bRet = true;
        if (!range) {
            return bRet;
        }
        var nRange = (' ' + range).split(/\-/);
        var nMin = nRange[0] || 'null';
        var nMax = nRange[1] || 'null';

        if (nMin != 'null') {
            nMin = nMin.replace(/\s/, '');
            if (nMin.substring(0, 1) == '#') {
                nMin = nMin.replace(/#/, '-');
            }
        }
        if (nMax != 'null') {
            if (nMax.substring(0, 1) == '#') {
                nMax = nMax.replace(/#/, '-');
            }
        }

        value = value.replace(/\,/, '');
        value = parseFloat(value);
        nMin = parseFloat(nMin);
        nMax = parseFloat(nMax);
        if (nMin == 'null' && value > nMax) {
            bRet = false;
        } else if (nMax == 'null' && value < nMin) {
            bRet = false;
        } else if (value < nMin || value > nMax) {
            bRet = false;
        }
        return bRet;
    }
};

