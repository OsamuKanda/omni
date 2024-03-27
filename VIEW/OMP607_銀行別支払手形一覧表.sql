-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:2013/8/20    KAWAHATA 条件追加 対象月度のデータのみ
-------------------------------------------------------------------------------
--OMP607 銀行別支払手形一覧表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP607
AS
		SELECT  
              ROWNUM  AS EDANUM
			, SUBSTR(DM_KANRI.MONYMD,1,4) || '年' ||  SUBSTR(DM_KANRI.MONYMD,5,2) || '月度' AS 月次締年月			--月次締年月
			, 日付記号追加(DT_SHRH.SHRYMD) AS 支払日																--支払日
			, DT_SHRB.SHRGINKOKBN AS 銀行区分																		--銀行区分
			, DK_SHRGINKO.SHRGINKOKBNNM AS 支払銀行名																--支払銀行名
			, 日付記号追加(DT_SHRB.TEGATAKIJITSU) AS 手形期日														--手形期日
			, DT_SHRB.TEGATANO AS 手形番号																			--手形番号
			, DT_SHRB.KING AS 金額																					--金額
			, DT_SHRH.SIRCD AS 支払先CD																				--支払先コード
			, DM_SHIRE.SIRNM1 AS 支払先名																			--支払先名
			, DT_SHRH.KAMKKBN AS 科目区分																			--科目区分
			, DK_KAMOKU.KAMOKUKBNNM AS 科目名																		--科目名
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
              --2014/04/30 明細の科目区分と結合
              --AND	DK_KAMOKU.KAMOKUKBN(+) = DT_SHRH.KAMKKBN
              AND	DK_KAMOKU.KAMOKUKBN(+) = DT_SHRB.KAMOKUKBN
              AND	DT_SHRH.DELKBN = '0'
              AND	DT_SHRB.DELKBN = '0'
              --手形のみ
              AND	DT_SHRB.NYUKINKBN = '02'
			  --月次締年月 '01' > 期日は対象外
--			  AND	DT_SHRB.TEGATAKIJITSU >= SUBSTR(DM_KANRI.MONYMD,1,4) || SUBSTR(DM_KANRI.MONYMD,5,2) || '01'
			  AND	DT_SHRH.SHRYMD >= SUBSTR(DM_KANRI.MONYMD,1,4) || SUBSTR(DM_KANRI.MONYMD,5,2) || '01'
			  --2013/8/20 条件追加 対象月度のデータのみ
			  AND	DT_SHRH.SHRYMD <= SUBSTR(DM_KANRI.MONYMD,1,4) || SUBSTR(DM_KANRI.MONYMD,5,2) || '31'
        ORDER BY
        			  DT_SHRH.SHRYMD
        			, DT_SHRB.SHRGINKOKBN
        			, DT_SHRB.TEGATAKIJITSU
;

