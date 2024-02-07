-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/31   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP708 大分類別売上一覧表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP708_2
AS
SELECT
              ROWNUM  AS EDANUM
            , SUBSTR(DM_KANRI.KINENDO,1,4) || '年度' AS 年度								--年度
            , DM_BUNRUID.BUNRUIDCD AS 大分類CD												--大分類コード
            , DM_BUNRUID.BUNRUIDNM AS 大分類名												--大分類名
            , DM_JIGYO.JIGYOCD AS 事業所CD													--事業所コード
            , DM_JIGYO.JIGYONM AS 事業所名													--事業所名
            , DECODE(D1.売上金額,NULL,0,D1.売上金額) AS 売上金額10							--売上金額10
            , DECODE(D2.売上金額,NULL,0,D2.売上金額) AS 売上金額11							--売上金額11
            , DECODE(D3.売上金額,NULL,0,D3.売上金額) AS 売上金額12							--売上金額12
            , DECODE(D4.売上金額,NULL,0,D4.売上金額) AS 売上金額01							--売上金額01
            , DECODE(D5.売上金額,NULL,0,D5.売上金額) AS 売上金額02							--売上金額02
            , DECODE(D6.売上金額,NULL,0,D6.売上金額) AS 売上金額03							--売上金額03
            , DECODE(D7.売上金額,NULL,0,D7.売上金額) AS 売上金額04							--売上金額04
            , DECODE(D8.売上金額,NULL,0,D8.売上金額) AS 売上金額05							--売上金額05
            , DECODE(D9.売上金額,NULL,0,D9.売上金額) AS 売上金額06							--売上金額06
            , DECODE(D10.売上金額,NULL,0,D10.売上金額) AS 売上金額07						--売上金額07
            , DECODE(D11.売上金額,NULL,0,D11.売上金額) AS 売上金額08						--売上金額08
            , DECODE(D12.売上金額,NULL,0,D12.売上金額) AS 売上金額09						--売上金額09
            , DECODE(D1.売上金額,NULL,0,D1.売上金額) + DECODE(D2.売上金額,NULL,0,D2.売上金額) 
             + DECODE(D3.売上金額,NULL,0,D3.売上金額) + DECODE(D4.売上金額,NULL,0,D4.売上金額) 
             + DECODE(D5.売上金額,NULL,0,D5.売上金額) + DECODE(D6.売上金額,NULL,0,D6.売上金額) 
             + DECODE(D7.売上金額,NULL,0,D7.売上金額) + + DECODE(D8.売上金額,NULL,0,D8.売上金額) 
             + DECODE(D9.売上金額,NULL,0,D9.売上金額) + DECODE(D10.売上金額,NULL,0,D10.売上金額) 
             + DECODE(D11.売上金額,NULL,0,D11.売上金額) + DECODE(D12.売上金額,NULL,0,D12.売上金額) AS 年計
		FROM 
			-- 全て
			(  SELECT DT_BUKKEN.BUNRUIDCD					--大分類コード
			 , DT_BUKKEN.JIGYOCD							--事業所コード
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- 管理マスタ
	           WHERE	DM_KANRI.KANRINO = '1'
						--完了日付 <> ALL '0'以外
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--最新請求日 <> ALL '0'以外
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
			   AND		DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD) >=	DM_KANRI.KINENDO
			   AND		DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD) < 	TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,12),'YYYYMMDD')
						--無効区分 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D0,
			-- 10月
			(  SELECT DT_BUKKEN.BUNRUIDCD					--大分類コード
			 , DT_BUKKEN.JIGYOCD							--事業所コード
			 , 10
			 , SUM(DT_BUKKEN.SOUKINGR) AS 売上金額
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- 管理マスタ
	           WHERE	DM_KANRI.KANRINO = '1'
						--完了日付 <> ALL '0'以外
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--最新請求日 <> ALL '0'以外
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(DM_KANRI.KINENDO,1,6)
						--無効区分 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D1,
			-- 11月
			(  SELECT DT_BUKKEN.BUNRUIDCD					--大分類コード
			 , DT_BUKKEN.JIGYOCD							--事業所コード
			 , 11
			 , SUM(DT_BUKKEN.SOUKINGR) AS 売上金額
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- 管理マスタ
	           WHERE	DM_KANRI.KANRINO = '1'
						--完了日付 <> ALL '0'以外
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--最新請求日 <> ALL '0'以外
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,1),'YYYYMMDD'),1,6)
						--無効区分 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D2,
			-- 12月
			(  SELECT DT_BUKKEN.BUNRUIDCD					--大分類コード
			 , DT_BUKKEN.JIGYOCD							--事業所コード
			 , 12
			 , SUM(DT_BUKKEN.SOUKINGR) AS 売上金額
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- 管理マスタ
	           WHERE	DM_KANRI.KANRINO = '1'
						--完了日付 <> ALL '0'以外
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--最新請求日 <> ALL '0'以外
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,2),'YYYYMMDD'),1,6)
						--無効区分 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D3,
			-- 01月
			(  SELECT DT_BUKKEN.BUNRUIDCD					--大分類コード
			 , DT_BUKKEN.JIGYOCD							--事業所コード
			 , 01
			 , SUM(DT_BUKKEN.SOUKINGR) AS 売上金額
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- 管理マスタ
	           WHERE 		DM_KANRI.KANRINO = '1'
						--完了日付 <> ALL '0'以外
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--最新請求日 <> ALL '0'以外
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,3),'YYYYMMDD'),1,6)
						--無効区分 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D4,
			-- 02月
			(  SELECT DT_BUKKEN.BUNRUIDCD					--大分類コード
			 , DT_BUKKEN.JIGYOCD							--事業所コード
			 , 02
			 , SUM(DT_BUKKEN.SOUKINGR) AS 売上金額
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- 管理マスタ
	           WHERE	DM_KANRI.KANRINO = '1'
						--完了日付 <> ALL '0'以外
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--最新請求日 <> ALL '0'以外
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,4),'YYYYMMDD'),1,6)
						--無効区分 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D5,
			-- 03月
			(  SELECT DT_BUKKEN.BUNRUIDCD					--大分類コード
			 , DT_BUKKEN.JIGYOCD							--事業所コード
			 , 03
			 , SUM(DT_BUKKEN.SOUKINGR) AS 売上金額
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- 管理マスタ
	           WHERE	DM_KANRI.KANRINO = '1'
						--完了日付 <> ALL '0'以外
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--最新請求日 <> ALL '0'以外
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,5),'YYYYMMDD'),1,6)
						--無効区分 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D6,
			-- 04月
			(  SELECT DT_BUKKEN.BUNRUIDCD					--大分類コード
			 , DT_BUKKEN.JIGYOCD							--事業所コード
			 , 04
			 , SUM(DT_BUKKEN.SOUKINGR) AS 売上金額
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- 管理マスタ
	           WHERE	DM_KANRI.KANRINO = '1'
						--完了日付 <> ALL '0'以外
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--最新請求日 <> ALL '0'以外
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,6),'YYYYMMDD'),1,6)
						--無効区分 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D7,
			-- 05月
			(  SELECT DT_BUKKEN.BUNRUIDCD					--大分類コード
			 , DT_BUKKEN.JIGYOCD							--事業所コード
			 , 05
			 , SUM(DT_BUKKEN.SOUKINGR) AS 売上金額
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- 管理マスタ
	           WHERE	DM_KANRI.KANRINO = '1'
						--完了日付 <> ALL '0'以外
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--最新請求日 <> ALL '0'以外
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,7),'YYYYMMDD'),1,6)
						--無効区分 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D8,
			-- 06月
			(  SELECT DT_BUKKEN.BUNRUIDCD					--大分類コード
			 , DT_BUKKEN.JIGYOCD							--事業所コード
			 , 06
			 , SUM(DT_BUKKEN.SOUKINGR) AS 売上金額
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- 管理マスタ
	           WHERE	DM_KANRI.KANRINO = '1'
						--完了日付 <> ALL '0'以外
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--最新請求日 <> ALL '0'以外
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,8),'YYYYMMDD'),1,6)
						--無効区分 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D9,
			-- 07月
			(  SELECT DT_BUKKEN.BUNRUIDCD					--大分類コード
			 , DT_BUKKEN.JIGYOCD							--事業所コード
			 , 07
			 , SUM(DT_BUKKEN.SOUKINGR) AS 売上金額
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- 管理マスタ
	           WHERE	DM_KANRI.KANRINO = '1'
						--完了日付 <> ALL '0'以外
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--最新請求日 <> ALL '0'以外
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,9),'YYYYMMDD'),1,6)
						--無効区分 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D10,
			-- 08月
			(  SELECT DT_BUKKEN.BUNRUIDCD					--大分類コード
			 , DT_BUKKEN.JIGYOCD							--事業所コード
			 , 08
			 , SUM(DT_BUKKEN.SOUKINGR) AS 売上金額
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- 管理マスタ
	           WHERE	DM_KANRI.KANRINO = '1'
						--完了日付 <> ALL '0'以外
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--最新請求日 <> ALL '0'以外
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,10),'YYYYMMDD'),1,6)
						--無効区分 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D11,
			-- 09月
			(  SELECT DT_BUKKEN.BUNRUIDCD					--大分類コード
			 , DT_BUKKEN.JIGYOCD							--事業所コード
			 , 09
			 , SUM(DT_BUKKEN.SOUKINGR) AS 売上金額
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- 管理マスタ
	           WHERE	DM_KANRI.KANRINO = '1'
						--完了日付 <> ALL '0'以外
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--最新請求日 <> ALL '0'以外
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,11),'YYYYMMDD'),1,6)
						--無効区分 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D12,
            DM_JIGYO,DM_KANRI,DM_BUNRUID
            WHERE	D0.JIGYOCD		=	D1.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D2.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D3.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D4.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D5.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D6.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D7.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D8.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D9.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D10.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D11.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D12.JIGYOCD(+)
            AND 	DM_JIGYO.JIGYOCD 	= 	D0.JIGYOCD
            AND		D0.BUNRUIDCD		=	D1.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D1.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D2.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D3.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D4.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D5.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D6.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D7.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D8.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D9.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D10.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D11.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D12.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= DM_BUNRUID.BUNRUIDCD(+)
             		-- 管理マスタ
           AND 		DM_KANRI.KANRINO = '1'
;

