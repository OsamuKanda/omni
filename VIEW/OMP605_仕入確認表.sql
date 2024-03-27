-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP605 仕入確認表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP605
AS
		SELECT   
              ROWNUM  AS EDANUM
			, DT_SHIREH.SIRJIGYOCD AS 仕入事業所CD						--仕入事業所コード
			, DT_SHIREH.SIRNO AS 仕入番号								--仕入番号
			, DT_SHIREH.SIRTORICD AS 取引区分							--取引区分
			, DK_SIRTORI.SIRTORICDNM AS 取引区分名						--取引区分名
			, 日付記号追加(DT_SHIREH.SIRYMD) AS 仕入日付				--仕入日付
			, DT_SHIREH.SIRCD AS 仕入先CD								--仕入先コード
			, DM_SHIRE.SIRNM1 AS 仕入先名								--仕入先名
--			, TO_NUMBER(DT_SHIREM.GYONO) AS 行番号						--行番号
			, ROW_NUMBER() OVER(PARTITION BY DT_SHIREH.SIRJIGYOCD,DT_SHIREH.SIRNO,WK_PRT000.LOGINID ORDER BY  DT_SHIREH.SIRJIGYOCD,DT_SHIREH.SIRNO,DT_SHIREM.GYONO)  AS 行番号						--行番号
			, DT_SHIREM.BBUNRUICD || DT_SHIREM.BKIKAKUCD AS 部品CD		--部品コード
			, DT_SHIREM.BKIKAKUNM AS 部品名								--部品名
			, DT_SHIREM.SIRSU AS 数量									--数量
			, DT_SHIREM.TANICD AS 単位CD								--単位コード
			, DECODE(DT_SHIREH.SIRTORICD,2,NULL,DM_TANI.TANINM) AS 単位名									--単位名
			, DT_SHIREM.SIRTANK AS 単価									--単価
			, DT_SHIREM.SIRKIN AS 金額									--金額
			, DT_SHIREM.TAX AS 消費税									--消費税
			, (DT_SHIREM.SIRKIN + DT_SHIREM.TAX ) AS 合計			--合計
			, DT_SHIREM.BUMONCD AS 部門CD								--部門コード
			, DM_BUMON.BUMONNM AS 部門名								--部門名
			, DECODE(DT_SHIREH.SIRTORICD,2,NULL,DT_SHIREM.JIGYOCD || '-' || DT_SHIREM.SAGYOBKBN || '-' || DT_SHIREM.RENNO) AS 物件番号			--物件番号
			, WK_PRT000.LOGINID 										--ログインID
		FROM WK_PRT000,DT_SHIREH,DT_SHIREM,DM_SHIRE,DM_TANI,DM_BUMON,DK_SIRTORI
		WHERE
					WK_PRT000.PROGID = 'OMP605'
			  AND	DT_SHIREH.SIRJIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_SHIREH.SIRNO = WK_PRT000.DENPNO
					-- 明細と結合
              AND	DT_SHIREH.SIRJIGYOCD = DT_SHIREM.SIRJIGYOCD
              AND	DT_SHIREH.SIRNO = DT_SHIREM.SIRNO
              AND	WK_PRT000.GYONO = DT_SHIREM.GYONO
              		-- 仕入先マスタ
              AND	DM_SHIRE.SIRCD(+) = DT_SHIREH.SIRCD
              		-- 単位マスタ
              AND	DM_TANI.TANICD(+) = DT_SHIREM.TANICD
              		-- 部門マスタ
              AND	DM_BUMON.BUMONCD(+) = DT_SHIREM.BUMONCD
              		-- 仕入取引区分マスタ
              AND	DK_SIRTORI.SIRTORICD = DT_SHIREH.SIRTORICD
              AND	DT_SHIREH.DELKBN = '0'
              AND	DT_SHIREM.DELKBN = '0'
        ORDER BY
        			  DT_SHIREH.SIRJIGYOCD
        			, DT_SHIREH.SIRNO
        			, DT_SHIREH.SIRYMD
        			, TO_NUMBER(DT_SHIREM.GYONO)
;
