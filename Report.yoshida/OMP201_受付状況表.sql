-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:2012/02/22   OKADA
--作業担当者名の追加
-------------------------------------------------------------------------------
--OMP201 受付状況表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP201
AS
SELECT
              ROWNUM  AS EDANUM
            , DT_BUKKEN.JIGYOCD AS 事業所CD																			--事業所コード
            , DM_JIGYO.JIGYONM AS 事業所名																			--事業所名
			, DT_BUKKEN.JIGYOCD || '-' || DT_BUKKEN.SAGYOBKBN || '-' || DT_BUKKEN.RENNO AS 物件番号					--物件番号
			, 日付記号追加(DT_BUKKEN.UKETSUKEYMD) AS 受付日付														--受付日付
			, DT_BUKKEN.NONYUCD AS 納入先CD																			--納入先コード
			, N1.NONYUNMR AS 納入先名																				--納入先名
			, DT_BUKKEN.SEIKYUCD AS 請求先CD																		--請求先コード
			, S1.NONYUNMR AS 請求先名																				--請求先名
			, DT_BUKKEN.TANTCD AS 受付担当者CD																		--受付担当者コード
			, DM_TANT.TANTNM AS 担当者名																			--担当者名
			, DT_BUKKENTANT.SAGYOTANTCD1 AS 作業担当者CD															--作業担当者コード
			, SAGYO.TANTNM AS 作業担当者名																			--作業担当者名
			, DT_BUKKEN.BUNRUIDCD AS 大分類CD																		--大分類コード
			, DM_BUNRUID.BUNRUIDNM AS 大分類名																		--大分類名
			, DT_BUKKEN.BUNRUICCD AS 中分類CD																		--中分類コード
			, DM_BUNRUIC.BUNRUICNM AS 中分類名																		--中分類名
			, DT_BUKKEN.UKETSUKEKBN AS 受付区分																		--受付区分
			, DK_UKETSUKE.UKETSUKEKBNNM AS 受付区分名																--受付区分名
			, DT_BUKKEN.SAGYOKBN AS 作業区分																		--作業区分
			, DK_UMU.UMUKBNNM AS 有無区分名																			--有無区分名
			,	CASE WHEN DT_BUKKEN.CHOKIKBN = '1'  THEN
				 '長期'
					 WHEN DT_BUKKEN.CHOKIKBN >= '2'  THEN
				 '請求不可'
					 ELSE
					  DK_SEIKYU.SEIKYUKBNNM
				END		AS 請求区分名																				--請求区分名
			, DECODE(DT_BUKKEN.CHOKIKBN,NULL,'0',DT_BUKKEN.CHOKIKBN) AS 長期区分										--長期区分
			, DK_CHOKI.CHOKIKBNNM AS 長期区分名																		--長期区分名
			, DT_BUKKEN.BIKO AS 備考																				--備考
			, DT_BUKKEN.UKETSUKEYMD AS 条件用受付日付
			, DT_BUKKEN.SAGYOBKBN AS 条件用作業分類
			, DT_BUKKEN.SEIKYUKBN AS 請求状態区分
		FROM DT_BUKKEN,DM_NONYU N1,DM_NONYU S1,DM_TANT,DM_BUNRUID,DM_BUNRUIC,DK_UKETSUKE,
			 DK_UMU,DK_CHOKI,DK_SEIKYU,DM_JIGYO,DM_TANT SAGYO,DT_BUKKENTANT
		WHERE
					-- 事業所マスタと結合
              		DT_BUKKEN.JIGYOCD = DM_JIGYO.JIGYOCD
              		-- 納入先マスタ
              AND	N1.NONYUCD(+) = DT_BUKKEN.NONYUCD
              AND	N1.SECCHIKBN(+) = '01'
              		-- 請求先マスタ
              AND	S1.NONYUCD(+) = DT_BUKKEN.SEIKYUCD
              AND	S1.SECCHIKBN(+) = '00'
              		-- 担当者マスタ
              AND	DM_TANT.TANTCD(+) = DT_BUKKEN.TANTCD
              		-- 大分類マスタ
              AND	DM_BUNRUID.BUNRUIDCD(+) = DT_BUKKEN.BUNRUIDCD
              		-- 中分類マスタ
              AND	DM_BUNRUIC.BUNRUICCD(+) = DT_BUKKEN.BUNRUICCD
              		-- 受付区分マスタ
              AND	DK_UKETSUKE.UKETSUKEKBN(+) = DT_BUKKEN.UKETSUKEKBN
              		-- 作業有無区分マスタ
              AND	DK_UMU.UMUKBN(+) = DT_BUKKEN.SAGYOKBN
              		-- 請求区分マスタ
              AND	DK_SEIKYU.SEIKYUKBN(+) = DT_BUKKEN.SEIKYUKBN
              		-- 長期区分マスタ
              AND	DK_CHOKI.CHOKIKBN(+) = DT_BUKKEN.CHOKIKBN
              AND	DT_BUKKEN.DELKBN = '0'
              		-- 物件別作業担当者マスタ
              AND	DT_BUKKEN.JIGYOCD = DT_BUKKENTANT.JIGYOCD(+)
              AND	DT_BUKKEN.SAGYOBKBN = DT_BUKKENTANT.SAGYOBKBN(+)
			  AND	DT_BUKKEN.RENNO = DT_BUKKENTANT.RENNO(+)
			  		--
			  AND	DT_BUKKENTANT.SAGYOTANTCD1 = SAGYO.TANTCD(+)
         ORDER BY
         			  DT_BUKKEN.JIGYOCD
         			, DT_BUKKEN.SAGYOBKBN
         			, DT_BUKKEN.RENNO
;
