-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/31   OKADA
--                                                 Update:2012/02/22   OKADA
--物件番号の表示方法を変更(2012/02/22)
-------------------------------------------------------------------------------
--OMP720 月例点検日程表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP720
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_NONYU.JIGYOCD AS 事業所CD									--事業所コード
            , DM_JIGYO.JIGYONM AS 事業所名									--事業所名
			, DM_HOSHU.SAGYOUTANTCD AS 作業担当者CD							--作業担当者コード
			, DM_TANT.TANTNM AS 作業担当者名								--作業担当者名
			, DM_HOSHU.GOUKI AS 号機										--号機
			, DM_NONYU.NONYUCD AS 納入先CD									--納入先コード
			, DM_NONYU.NONYUNM1 AS 納入先名1								--納入先名１
			, DM_NONYU.NONYUNM2 AS 納入先名2								--納入先名２
			, DM_NONYU.ADD1 AS 住所1										--住所１
			, DM_NONYU.ADD2 AS 住所2										--住所２
			, DM_NONYU.TELNO1 AS 電話番号1									--電話番号１
			, DM_HOSHU.KISHUKATA AS 機種									--機種
			, DECODE(DM_HOSHU.TENKEN1BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN1BNO) AS 物件番号1								--1月
			, DECODE(DM_HOSHU.TENKEN2BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN2BNO) AS 物件番号2								--2月
			, DECODE(DM_HOSHU.TENKEN3BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN3BNO) AS 物件番号3								--3月
			, DECODE(DM_HOSHU.TENKEN4BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN4BNO) AS 物件番号4								--4月
			, DECODE(DM_HOSHU.TENKEN5BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN5BNO) AS 物件番号5								--5月
			, DECODE(DM_HOSHU.TENKEN6BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN6BNO) AS 物件番号6								--6月
			, DECODE(DM_HOSHU.TENKEN7BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN7BNO) AS 物件番号7								--7月
			, DECODE(DM_HOSHU.TENKEN8BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN8BNO) AS 物件番号8								--8月
			, DECODE(DM_HOSHU.TENKEN9BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN9BNO) AS 物件番号9								--9月
			, DECODE(DM_HOSHU.TENKEN10BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN10BNO) AS 物件番号10							--10月
			, DECODE(DM_HOSHU.TENKEN11BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN11BNO) AS 物件番号11							--11月
			, DECODE(DM_HOSHU.TENKEN12BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN12BNO) AS 物件番号12							--12月
			, DM_HOSHU.HOSHUM1 AS 保守月1									--1月
			, DM_HOSHU.HOSHUM2 AS 保守月2									--2月
			, DM_HOSHU.HOSHUM3 AS 保守月3									--3月
			, DM_HOSHU.HOSHUM4 AS 保守月4									--4月
			, DM_HOSHU.HOSHUM5 AS 保守月5									--5月
			, DM_HOSHU.HOSHUM6 AS 保守月6									--6月
			, DM_HOSHU.HOSHUM7 AS 保守月7									--7月
			, DM_HOSHU.HOSHUM8 AS 保守月8									--8月
			, DM_HOSHU.HOSHUM9 AS 保守月9									--9月
			, DM_HOSHU.HOSHUM10 AS 保守月10									--10月
			, DM_HOSHU.HOSHUM11 AS 保守月11									--11月
			, DM_HOSHU.HOSHUM12 AS 保守月12									--12月
		FROM  DM_NONYU,DM_JIGYO,DM_HOSHU,DM_TANT
		WHERE
              		-- 保守点検マスタ
              		DM_HOSHU.NONYUCD = DM_NONYU.NONYUCD
			  AND	DM_NONYU.SECCHIKBN = '01'
			  		-- 事業所マスタ
			  AND	DM_NONYU.JIGYOCD = DM_JIGYO.JIGYOCD
			  		-- 作業担当者マスタ
			  AND	DM_HOSHU.SAGYOUTANTCD = DM_TANT.TANTCD(+)
              AND	DM_HOSHU.DELKBN	 = '0'
		ORDER BY
					  DM_NONYU.JIGYOCD
					, DM_HOSHU.SAGYOUTANTCD
					, DM_NONYU.NONYUCD
					, DM_HOSHU.GOUKI
;
