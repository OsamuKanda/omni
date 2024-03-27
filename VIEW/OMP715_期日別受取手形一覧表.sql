-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:2012/02/15   OKADA
-- 振出人毎にサマリした結果を印刷
-------------------------------------------------------------------------------
--OMP715	期日別受取手形一覧表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP715
AS
		SELECT  
			  SUBSTR(DM_KANRI.MONYMD,1,4) || '年' ||  SUBSTR(DM_KANRI.MONYMD,5,2) || '月度' AS 月次締年月			--月次締年月
			, 日付記号追加(DT_NYUKINM.TEGATAKIJITSU) AS 手形期日													--手形期日
--			, DT_URIAGEH.SEIKYUCD AS 請求先CD																		--請求先コード
--			, DT_URIAGEH.SEIKYUNM AS 請求先名																		--請求先名
			, SUM(DT_NYUKINM.KING) AS 金額																				--金額
			, DT_NYUKINM.GINKOCD AS 銀行区分																		--銀行区分
			, DM_GINKO.GINKONM AS 銀行名																			--銀行名
			, DT_NYUKINM.TEGATANO AS 手形番号																		--手形番号
			, 日付記号追加(DT_NYUKINM.HURIYMD) AS 振出日															--振出日
			, DT_NYUKINM.HURIDASHI AS 振出人																		--振出人
			, SUBSTR(DT_NYUKINM.TEGATAKIJITSU,1,6) AS 期日年月														--期日年月
		FROM DT_NYUKINM,DM_GINKO,DM_KANRI
		WHERE
             		-- 支払銀行マスタ
              		DM_GINKO.GINKOCD(+) = DT_NYUKINM.GINKOCD
              		-- 管理マスタ
              AND	DM_KANRI.KANRINO = '1'
              		-- 売上ヘッダ
--              AND	DT_URIAGEH.SEIKYUSHONO = DT_NYUKINM.SEIKYUSHONO
              AND	DT_NYUKINM.DELKBN = '0'
              --手形のみ
              AND	DT_NYUKINM.NYUKINKBN = '02'
			  --月次締年月 '01' > 期日は対象外
--2012.10.18---------------
------------------	  AND	DT_NYUKINM.TEGATAKIJITSU >= TO_CHAR(SUBSTR(DM_KANRI.MONYMD,1,4) || SUBSTR(DM_KANRI.MONYMD,5,2) || '01')
			  AND	DT_NYUKINM.TEGATAKIJITSU > TO_CHAR(DM_KANRI.MONYMD)
		GROUP BY	  DM_KANRI.MONYMD
					, DT_NYUKINM.TEGATAKIJITSU
					, DT_NYUKINM.GINKOCD
					, DM_GINKO.GINKONM
					, DT_NYUKINM.TEGATANO
					, DT_NYUKINM.HURIYMD
					, DT_NYUKINM.HURIDASHI
        ORDER BY
        			  DT_NYUKINM.TEGATAKIJITSU
--        			, DT_URIAGEH.SEIKYUCD
        			, DT_NYUKINM.HURIYMD
        			, DT_NYUKINM.TEGATANO
;

