-------------------------------------------------------------------------------
--IjeNmVXev[X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP730 __ñ
-------------------------------------------------------------------------------
CREATE OR REPLACE FORCE EDITIONABLE VIEW "OMNI"."V_OMP730" ("EDANUM", "ÆCD", "Æ¼", "[üæCD", "@", "¿æCD", "¿æ¼1", "¿æ¼2", "_ñJnú", "_ñI¹ú", "íÊCD", "íÊ¼", "@í", "[üæ¼1", "[üæ¼2", "Z1", "Z2", "Ûçñ", "_", "_ñàz", "¿æZ1", "¿æZ2", "_ñZ1", "_ñZ2", "ïÐ¼", "æ÷ð¼", "_ñðp", "_ñI¹", "L³") AS 
  SELECT
              ROWNUM  AS EDANUM
            , DM_NONYU.JIGYOCD AS ÆCD									--ÆR[h
            , DM_JIGYO.JIGYONM AS Æ¼									--Æ¼
            , DM_HOSHU.NONYUCD AS [üæCD									--[üæR[h
			, DM_HOSHU.GOUKI AS @										--@
--            , DECODE(DM_HOSHU.SEIKYUSAKICDH,NULL,S.NONYUNM1,S1.NONYUNM1) AS ¿æ¼1									--
--          , DECODE(DM_HOSHU.SEIKYUSAKICDH,NULL,S.NONYUNM2,S1.NONYUNM2) AS ¿æ¼2									--
			, S.NONYUCD AS ¿æCD										--
            , S.NONYUNM1 AS ¿æ¼1									--
            , S.NONYUNM2 AS ¿æ¼2									--
            , út¶ÇÁ(DM_HOSHU.KEIYAKUYMD) AS _ñJnú				--
            , DECODE(IS_DATE(DM_HOSHU.KEIYAKUYMD),0,NULL,út¶ÇÁ(TO_CHAR(ADD_MONTHS(TO_DATE(DM_HOSHU.KEIYAKUYMD,'YYYYMMDD')-1,12),'YYYYMMDD')))
 				AS _ñI¹ú												--
 			, DM_HOSHU.SHUBETSUCD AS íÊCD									--íÊR[h
 			, DM_SHUBETSU.SHUBETSUNM AS íÊ¼								--íÊ¼
			, DM_HOSHU.KISHUKATA AS @í									--@í
            , DM_NONYU.NONYUNM1 AS [üæ¼1								--[üæ¼P
            , DM_NONYU.NONYUNM2 AS [üæ¼2								--[üæ¼Q
			, DM_NONYU.ADD1 AS Z1										--ZP
			, DM_NONYU.ADD2 AS Z2										--ZQ
			, 'N' || (DM_HOSHU.HOSHUM1 + DM_HOSHU.HOSHUM2 + DM_HOSHU.HOSHUM3 + DM_HOSHU.HOSHUM4 + DM_HOSHU.HOSHUM5 + DM_HOSHU.HOSHUM6 +
			  DM_HOSHU.HOSHUM7 + DM_HOSHU.HOSHUM8 + DM_HOSHU.HOSHUM9 + DM_HOSHU.HOSHUM10 + DM_HOSHU.HOSHUM11 + DM_HOSHU.HOSHUM12) || 'ñ' AS Ûçñ
			, '(' || RTRIM(DECODE(DM_HOSHU.HOSHUM1,0,'','1,') || DECODE(DM_HOSHU.HOSHUM2,0,'','2,') || DECODE(DM_HOSHU.HOSHUM3,0,'','3,') ||
			  DECODE(DM_HOSHU.HOSHUM4,0,'','4,') || DECODE(DM_HOSHU.HOSHUM5,0,'','5,') || DECODE(DM_HOSHU.HOSHUM6,0,'','6,') ||
			  DECODE(DM_HOSHU.HOSHUM7,0,'','7,') || DECODE(DM_HOSHU.HOSHUM8,0,'','8,') || DECODE(DM_HOSHU.HOSHUM9,0,'','9,') ||
			  DECODE(DM_HOSHU.HOSHUM10,0,'','10,') || DECODE(DM_HOSHU.HOSHUM11,0,'','11,') || DECODE(DM_HOSHU.HOSHUM12,0,'','12,'),',') || ')'AS _
			, DECODE(DM_HOSHU.KEIYAKUKBN,'1',DM_HOSHU.KEIYAKUKING,(DM_HOSHU.HOSHUM1 + DM_HOSHU.HOSHUM2 + DM_HOSHU.HOSHUM3 + DM_HOSHU.HOSHUM4 + DM_HOSHU.HOSHUM5 + DM_HOSHU.HOSHUM6 +
			  DM_HOSHU.HOSHUM7 + DM_HOSHU.HOSHUM8 + DM_HOSHU.HOSHUM9 + DM_HOSHU.HOSHUM10 + DM_HOSHU.HOSHUM11 + DM_HOSHU.HOSHUM12)*DM_HOSHU.KEIYAKUKING) AS _ñàz								--_ñàz
--			, DECODE(DM_HOSHU.SEIKYUSAKICDH,NULL,S.ADD1,S1.ADD1) AS ¿æZ1											--¿æZP
--			, DECODE(DM_HOSHU.SEIKYUSAKICDH,NULL,S.ADD2,S1.ADD2) AS ¿æZ2											--¿æZQ
			, S.ADD1 AS ¿æZ1											--¿æZP
			, S.ADD2 AS ¿æZ2											--¿æZQ
			, (CASE WHEN DM_HOSHU.KEIYAKUYMD < '20231001' THEN DM_KANRI.OLD_ADD1 ELSE DM_KANRI.ADD1 END) AS _ñZ1		--_ñZP
			, (CASE WHEN DM_HOSHU.KEIYAKUYMD < '20231001' THEN DM_KANRI.OLD_ADD2 ELSE DM_KANRI.ADD2 END) AS _ñZ2		--_ñZQ
			, (CASE WHEN DM_HOSHU.KEIYAKUYMD < '20231001' THEN DM_KANRI.OLD_KAISYANM ELSE DM_KANRI.KAISYANM END)AS ïÐ¼		--ïÐ¼
			, (CASE WHEN DM_HOSHU.KEIYAKUYMD < '20231001' THEN DM_KANRI.OLD_TORINAM ELSE DM_KANRI.TORINAM END)AS æ÷ð¼		--æ÷ð¼
			, DM_HOSHU.KEIYAKUYMD AS _ñðp
			, DECODE(IS_DATE(DM_HOSHU.KEIYAKUYMD),0,NULL,TO_CHAR(ADD_MONTHS(TO_DATE(DM_HOSHU.KEIYAKUYMD,'YYYYMMDD')-1,12),'YYYYMMDD')) AS _ñI¹
			, DM_HOSHU.HOSHUM1 || DM_HOSHU.HOSHUM2 || DM_HOSHU.HOSHUM3 || DM_HOSHU.HOSHUM4 || DM_HOSHU.HOSHUM5 || DM_HOSHU.HOSHUM6 			--L³
			  || DM_HOSHU.HOSHUM7 || DM_HOSHU.HOSHUM8 || DM_HOSHU.HOSHUM9 || DM_HOSHU.HOSHUM10 || DM_HOSHU.HOSHUM11 || DM_HOSHU.HOSHUM12 AS L³
--		FROM  DM_NONYU,DM_JIGYO,DM_HOSHU,DM_NONYU S,DM_NONYU S1,DM_SHUBETSU,DM_KANRI
		FROM  DM_NONYU,DM_JIGYO,DM_HOSHU,DM_NONYU S,DM_SHUBETSU,DM_KANRI
		WHERE
              		-- [üæ}X^
              		DM_HOSHU.NONYUCD = DM_NONYU.NONYUCD
			  AND	DM_NONYU.SECCHIKBN = '01'
              		-- ¿æ}X^
              AND	DM_NONYU.SEIKYUSAKICDH = S.NONYUCD
			  AND	S.SECCHIKBN = '00'
--              AND   DM_NONYU.JIGYOCD = S.JIGYOCD
              		-- ¿æ}X^
--              AND	DM_HOSHU.SEIKYUSAKICDH = S1.NONYUCD(+)
--			  AND	S1.SECCHIKBN = '00'
--             AND   DM_NONYU.JIGYOCD(+) = S1.JIGYOCD
			  		-- Æ}X^
			  AND	DM_NONYU.JIGYOCD = DM_JIGYO.JIGYOCD
					-- Ûç_}X^
			  AND	DM_HOSHU.KEIYAKUYMD IS NOT Null
			  AND	DM_HOSHU.KEIYAKUYMD <> 0
			  		-- íÊ}X^
			  AND	DM_HOSHU.SHUBETSUCD = DM_SHUBETSU.SHUBETSUCD(+)
			  		-- Ç}X^
			  AND	DM_KANRI.KANRINO = '1'
              AND	DM_HOSHU.DELKBN	 = '0'
		ORDER BY
					  DM_NONYU.JIGYOCD
					, DM_HOSHU.NONYUCD
					, DM_HOSHU.GOUKI
;
