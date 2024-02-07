-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP730 点検契約書
-------------------------------------------------------------------------------
CREATE OR REPLACE FORCE EDITIONABLE VIEW "OMNI"."V_OMP730" ("EDANUM", "事業所CD", "事業所名", "納入先CD", "号機", "請求先CD", "請求先名1", "請求先名2", "契約開始日", "契約終了日", "種別CD", "種別名", "機種", "納入先名1", "納入先名2", "住所1", "住所2", "保守回数", "点検月", "契約金額", "請求先住所1", "請求先住所2", "契約書住所1", "契約書住所2", "会社名", "取締役名", "契約条件用", "契約終了", "有無") AS 
  SELECT
              ROWNUM  AS EDANUM
            , DM_NONYU.JIGYOCD AS 事業所CD									--事業所コード
            , DM_JIGYO.JIGYONM AS 事業所名									--事業所名
            , DM_HOSHU.NONYUCD AS 納入先CD									--納入先コード
			, DM_HOSHU.GOUKI AS 号機										--号機
--            , DECODE(DM_HOSHU.SEIKYUSAKICDH,NULL,S.NONYUNM1,S1.NONYUNM1) AS 請求先名1									--
--          , DECODE(DM_HOSHU.SEIKYUSAKICDH,NULL,S.NONYUNM2,S1.NONYUNM2) AS 請求先名2									--
			, S.NONYUCD AS 請求先CD										--
            , S.NONYUNM1 AS 請求先名1									--
            , S.NONYUNM2 AS 請求先名2									--
            , 日付文字追加(DM_HOSHU.KEIYAKUYMD) AS 契約開始日				--
            , DECODE(IS_DATE(DM_HOSHU.KEIYAKUYMD),0,NULL,日付文字追加(TO_CHAR(ADD_MONTHS(TO_DATE(DM_HOSHU.KEIYAKUYMD,'YYYYMMDD')-1,12),'YYYYMMDD')))
 				AS 契約終了日												--
 			, DM_HOSHU.SHUBETSUCD AS 種別CD									--種別コード
 			, DM_SHUBETSU.SHUBETSUNM AS 種別名								--種別名
			, DM_HOSHU.KISHUKATA AS 機種									--機種
            , DM_NONYU.NONYUNM1 AS 納入先名1								--納入先名１
            , DM_NONYU.NONYUNM2 AS 納入先名2								--納入先名２
			, DM_NONYU.ADD1 AS 住所1										--住所１
			, DM_NONYU.ADD2 AS 住所2										--住所２
			, '年' || (DM_HOSHU.HOSHUM1 + DM_HOSHU.HOSHUM2 + DM_HOSHU.HOSHUM3 + DM_HOSHU.HOSHUM4 + DM_HOSHU.HOSHUM5 + DM_HOSHU.HOSHUM6 +
			  DM_HOSHU.HOSHUM7 + DM_HOSHU.HOSHUM8 + DM_HOSHU.HOSHUM9 + DM_HOSHU.HOSHUM10 + DM_HOSHU.HOSHUM11 + DM_HOSHU.HOSHUM12) || '回' AS 保守回数
			, '(' || RTRIM(DECODE(DM_HOSHU.HOSHUM1,0,'','1,') || DECODE(DM_HOSHU.HOSHUM2,0,'','2,') || DECODE(DM_HOSHU.HOSHUM3,0,'','3,') ||
			  DECODE(DM_HOSHU.HOSHUM4,0,'','4,') || DECODE(DM_HOSHU.HOSHUM5,0,'','5,') || DECODE(DM_HOSHU.HOSHUM6,0,'','6,') ||
			  DECODE(DM_HOSHU.HOSHUM7,0,'','7,') || DECODE(DM_HOSHU.HOSHUM8,0,'','8,') || DECODE(DM_HOSHU.HOSHUM9,0,'','9,') ||
			  DECODE(DM_HOSHU.HOSHUM10,0,'','10,') || DECODE(DM_HOSHU.HOSHUM11,0,'','11,') || DECODE(DM_HOSHU.HOSHUM12,0,'','12,'),',') || '月)'AS 点検月
			, DECODE(DM_HOSHU.KEIYAKUKBN,'1',DM_HOSHU.KEIYAKUKING,(DM_HOSHU.HOSHUM1 + DM_HOSHU.HOSHUM2 + DM_HOSHU.HOSHUM3 + DM_HOSHU.HOSHUM4 + DM_HOSHU.HOSHUM5 + DM_HOSHU.HOSHUM6 +
			  DM_HOSHU.HOSHUM7 + DM_HOSHU.HOSHUM8 + DM_HOSHU.HOSHUM9 + DM_HOSHU.HOSHUM10 + DM_HOSHU.HOSHUM11 + DM_HOSHU.HOSHUM12)*DM_HOSHU.KEIYAKUKING) AS 契約金額								--契約金額
--			, DECODE(DM_HOSHU.SEIKYUSAKICDH,NULL,S.ADD1,S1.ADD1) AS 請求先住所1											--請求先住所１
--			, DECODE(DM_HOSHU.SEIKYUSAKICDH,NULL,S.ADD2,S1.ADD2) AS 請求先住所2											--請求先住所２
			, S.ADD1 AS 請求先住所1											--請求先住所１
			, S.ADD2 AS 請求先住所2											--請求先住所２
			, (CASE WHEN DM_HOSHU.KEIYAKUYMD < '20231001' THEN DM_KANRI.OLD_ADD1 ELSE DM_KANRI.ADD1 END) AS 契約書住所1		--契約書住所１
			, (CASE WHEN DM_HOSHU.KEIYAKUYMD < '20231001' THEN DM_KANRI.OLD_ADD2 ELSE DM_KANRI.ADD2 END) AS 契約書住所2		--契約書住所２
			, (CASE WHEN DM_HOSHU.KEIYAKUYMD < '20231001' THEN DM_KANRI.OLD_KAISYANM ELSE DM_KANRI.KAISYANM END)AS 会社名		--会社名
			, (CASE WHEN DM_HOSHU.KEIYAKUYMD < '20231001' THEN DM_KANRI.OLD_TORINAM ELSE DM_KANRI.TORINAM END)AS 取締役名		--取締役名
			, DM_HOSHU.KEIYAKUYMD AS 契約条件用
			, DECODE(IS_DATE(DM_HOSHU.KEIYAKUYMD),0,NULL,TO_CHAR(ADD_MONTHS(TO_DATE(DM_HOSHU.KEIYAKUYMD,'YYYYMMDD')-1,12),'YYYYMMDD')) AS 契約終了
			, DM_HOSHU.HOSHUM1 || DM_HOSHU.HOSHUM2 || DM_HOSHU.HOSHUM3 || DM_HOSHU.HOSHUM4 || DM_HOSHU.HOSHUM5 || DM_HOSHU.HOSHUM6 			--有無
			  || DM_HOSHU.HOSHUM7 || DM_HOSHU.HOSHUM8 || DM_HOSHU.HOSHUM9 || DM_HOSHU.HOSHUM10 || DM_HOSHU.HOSHUM11 || DM_HOSHU.HOSHUM12 AS 有無
--		FROM  DM_NONYU,DM_JIGYO,DM_HOSHU,DM_NONYU S,DM_NONYU S1,DM_SHUBETSU,DM_KANRI
		FROM  DM_NONYU,DM_JIGYO,DM_HOSHU,DM_NONYU S,DM_SHUBETSU,DM_KANRI
		WHERE
              		-- 納入先マスタ
              		DM_HOSHU.NONYUCD = DM_NONYU.NONYUCD
			  AND	DM_NONYU.SECCHIKBN = '01'
              		-- 請求先マスタ
              AND	DM_NONYU.SEIKYUSAKICDH = S.NONYUCD
			  AND	S.SECCHIKBN = '00'
--              AND   DM_NONYU.JIGYOCD = S.JIGYOCD
              		-- 請求先マスタ
--              AND	DM_HOSHU.SEIKYUSAKICDH = S1.NONYUCD(+)
--			  AND	S1.SECCHIKBN = '00'
--             AND   DM_NONYU.JIGYOCD(+) = S1.JIGYOCD
			  		-- 事業所マスタ
			  AND	DM_NONYU.JIGYOCD = DM_JIGYO.JIGYOCD
					-- 保守点検マスタ
			  AND	DM_HOSHU.KEIYAKUYMD IS NOT Null
			  AND	DM_HOSHU.KEIYAKUYMD <> 0
			  		-- 種別マスタ
			  AND	DM_HOSHU.SHUBETSUCD = DM_SHUBETSU.SHUBETSUCD(+)
			  		-- 管理マスタ
			  AND	DM_KANRI.KANRINO = '1'
              AND	DM_HOSHU.DELKBN	 = '0'
		ORDER BY
					  DM_NONYU.JIGYOCD
					, DM_HOSHU.NONYUCD
					, DM_HOSHU.GOUKI
;
