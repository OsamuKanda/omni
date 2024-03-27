-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2014/04/30   KAWAHATA
--                                                 Update:2014/05/13   KAWAHATA 
--同じ仕入先、同じ科目を合算する
-------------------------------------------------------------------------------
--OMP716	期日別でんさい一覧表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP716
AS
		SELECT  
              SUBSTR(DM_KANRI.MONYMD,1,4) || '年' ||  SUBSTR(DM_KANRI.MONYMD,5,2) || '月度' AS 月次締年月			--月次締年月
			, 日付記号追加(DT_SHRB.TEGATAKIJITSU) AS 手形期日														--手形期日
			, DT_SHRB.SHRGINKOKBN AS 銀行区分																		--銀行区分
			, MAX(DK_SHRGINKO.SHRGINKOKBNNM) AS 支払銀行名															--支払銀行名
			, SUM(DT_SHRB.KING) AS 金額																					--金額
			, DT_SHRH.SIRCD AS 支払先CD																				--支払先コード
			, MAX(DM_SHIRE.SIRNM1) AS 支払先名																			--支払先名
			, MAX(DM_SHIRE.SIRNMX) AS 仕入先カナ																			--仕入先カナ
			, DT_SHRB.KAMOKUKBN AS 科目区分																			--科目区分
			, MAX(DK_KAMOKU.KAMOKUKBNNM) AS 科目名																		--科目名
			, 日付記号追加(DT_SHRH.SHRYMD) AS 支払日																--支払日
			, SUBSTR(DT_SHRB.TEGATAKIJITSU,1,6) AS 期日年月															--期日年月
		FROM DT_SHRH,DT_SHRB,DM_KANRI,DK_SHRGINKO,DM_SHIRE,DK_KAMOKU
		WHERE
					-- 支払明細と結合
              		DT_SHRH.JIGYOCD = DT_SHRB.JIGYOCD
              AND	DT_SHRH.SHRNO = DT_SHRB.SHRNO
              		-- 支払銀行マスタ
              AND	DK_SHRGINKO.SHRGINKOKBN(+) = DT_SHRB.SHRGINKOKBN
              		-- 管理マスタ
              AND	DM_KANRI.KANRINO = '1'
              		-- 仕入先マスタ
              AND	DM_SHIRE.SIRCD(+) = DT_SHRH.SIRCD
              		-- 科目区分マスタ
              AND	DK_KAMOKU.KAMOKUKBN(+) = DT_SHRB.KAMOKUKBN
              AND	DT_SHRH.DELKBN = '0'
              AND	DT_SHRB.DELKBN = '0'
              --でんさいのみ
              AND	DT_SHRB.NYUKINKBN = '13'
--			  --月次締年月 '01' > 期日は対象外
--			  AND	DT_SHRB.TEGATAKIJITSU >= TO_CHAR(SUBSTR(DM_KANRI.MONYMD,1,4) || SUBSTR(DM_KANRI.MONYMD,5,2) || '01')
			  AND	DT_SHRB.TEGATAKIJITSU > TO_CHAR(DM_KANRI.MONYMD)
--			  --2013/8/20 条件追加 対象月度のデータのみ
			  AND	DT_SHRH.SHRYMD <= SUBSTR(DM_KANRI.MONYMD,1,4) || SUBSTR(DM_KANRI.MONYMD,5,2) || '31'
        GROUP BY
        			  SUBSTR(DM_KANRI.MONYMD,1,4)
        			, SUBSTR(DM_KANRI.MONYMD,5,2)
        			, DT_SHRB.TEGATAKIJITSU
        			, DT_SHRB.SHRGINKOKBN
        			, DT_SHRH.SHRYMD
        			, DT_SHRH.SIRCD
        			, DM_SHIRE.SIRNMX
        			, DT_SHRB.KAMOKUKBN
        ORDER BY
                	  SUBSTR(DM_KANRI.MONYMD,1,4)
        			, SUBSTR(DM_KANRI.MONYMD,5,2)
        			, DT_SHRB.TEGATAKIJITSU
        			, DT_SHRB.SHRGINKOKBN
        			, DT_SHRH.SHRYMD
        			, DT_SHRH.SIRCD
        			, DM_SHIRE.SIRNMX
        			, DT_SHRB.KAMOKUKBN
;

