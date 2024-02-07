-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP606 支払確認表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP606
AS
		SELECT   
              ROWNUM  AS EDANUM
			, DT_SHRH.JIGYOCD AS 事業所CD								--事業所コード
			, DT_SHRH.SHRNO AS 支払番号									--支払番号
			, DT_SHRB.NYUKINKBN AS 入金区分								--入金区分
			, DK_NYUKIN.NYUKINKBNNM AS 入金区分名						--入金区分名
			, DT_SHRB.KAMOKUKBN AS 科目区分								--科目区分
			, DK_KAMOKU.KAMOKUKBNNM AS 科目区分名						--科目区分名
			, 日付記号追加(DT_SHRH.SHRYMD) AS 支払日付					--支払日付
			, DT_SHRH.SIRCD AS 仕入先CD									--仕入先コード
			, DM_SHIRE.SIRNM1 AS 仕入先名								--仕入先名
--			, TO_NUMBER(DT_SHRB.GYONO) AS 行番号						--行番号
			, ROW_NUMBER() OVER(PARTITION BY DT_SHRH.JIGYOCD,DT_SHRH.SHRNO,WK_PRT000.LOGINID ORDER BY DT_SHRH.JIGYOCD,DT_SHRH.SHRNO,DT_SHRB.GYONO)  AS 行番号						--行番号
			, DT_SHRB.KING AS 金額										--金額
			, DT_SHRH.BIKO AS 備考										--備考
			, DT_SHRB.TEGATANO AS 手形番号								--手形番号
			, 日付記号追加(DT_SHRB.TEGATAKIJITSU) AS 手形期日			--手形期日
			, DT_SHRB.SHRGINKOKBN AS 銀行区分							--銀行区分
			, DK_SHRGINKO.SHRGINKOKBNNM AS 支払銀行名					--支払銀行名
			, WK_PRT000.LOGINID 										--ログインID
		FROM WK_PRT000,DT_SHRH,DT_SHRB,DM_SHIRE,DK_NYUKIN,DK_KAMOKU,DK_SHRGINKO
		WHERE
					WK_PRT000.PROGID = 'OMP606'
			  AND	DT_SHRH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_SHRH.SHRNO = WK_PRT000.DENPNO
					-- 明細と結合
              AND	DT_SHRH.JIGYOCD = DT_SHRB.JIGYOCD
              AND	DT_SHRH.SHRNO = DT_SHRB.SHRNO
              AND	WK_PRT000.GYONO = DT_SHRB.GYONO
              		-- 入金区分
              AND	DK_NYUKIN.NYUKINKBN(+) = DT_SHRB.NYUKINKBN
              		-- 科目区分
              AND	DK_KAMOKU.KAMOKUKBN(+) = DT_SHRB.KAMOKUKBN
              		-- 仕入先マスタ
              AND	DM_SHIRE.SIRCD(+) = DT_SHRH.SIRCD
              		-- 支払銀行マスタ
              AND	DK_SHRGINKO.SHRGINKOKBN(+) = DT_SHRB.SHRGINKOKBN
              AND	DT_SHRH.DELKBN = '0'
              AND	DT_SHRB.DELKBN = '0'
        ORDER BY
        			  DT_SHRH.JIGYOCD
        			, DT_SHRH.SHRNO
        			, DT_SHRH.SHRYMD
        			, TO_NUMBER(DT_SHRB.GYONO)
;
