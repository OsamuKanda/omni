-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/07   OKADA
--                                                 Update:2012/04/24   OKADA
--                                                 Update:2023/09/22   Kanda(請求書インボイス対応および社名変更）
--
-- 事業所コード=02の場合は、マスタ内の名称ではなく"関東サービス工場"として印刷する。
--
-------------------------------------------------------------------------------
--OMP601 請求書
-------------------------------------------------------------------------------
CREATE OR REPLACE FORCE VIEW "OMNI"."V_OMP601" ("区分", "PAGENO", "連番", "請求書番号", "請求日付", "物件番号", "郵便番号", "住所1", "住所2", "請求先名", "先方部署名", "先方担当者名", "納入先名", "回収予定日", "行番号", "月日", "品名1", "品名2", "数量", "単位名", "単価", "金額", "消費税", "事業所郵便番号", "事業所住所1", "事業所住所2", "事業所電話番号", "事業所FAX番号", "事業所名", "銀行名", "LOGINID", "PROGID", "KINGAKU", "ZEI", "メモ", "税区分") AS 
  SELECT
			  WK.区分
			, WK.PAGENO
			, WK.連番
			, WK.請求書番号												--請求書番号
			, WK.請求日付												--請求日付
			, WK.物件番号												--物件番号
			, WK.郵便番号												--郵便番号
			, WK.住所1													--住所1
			, WK.住所2													--住所2
			, WK.請求先名												--請求先名
			, WK.先方部署名												--先方部署名
			, WK.先方担当者名											--先方担当者名
			, WK.納入先名												--納入先名
			, WK.回収予定日												--回収予定日
			, WK.行番号													--行番号
			, WK.月日
			, WK.品名1								--品名1
			, WK.品名2								--品名2
			, WK.数量									--数量
			, WK.単位名								--単位名
			, WK.単価									--単価
			, WK.金額									--金額
			, WK.消費税									--消費税
			, WK.事業所郵便番号						--
			, WK.事業所住所1								--
			, WK.事業所住所2								--
			, WK.事業所電話番号							--
			, WK.事業所FAX番号							--
			, WK.事業所名								--
			, WK.銀行名
			, WK.LOGINID 										--ログインID
			, WK.PROGID
			, WK.KINGAKU
			, WK.ZEI
			, WK.メモ
			, WK.税区分   -- 2023/08/21 ADD TC KANDA 税区分が0(課税)/1(非課税)の表示
		FROM
(		SELECT
			  '1' AS 区分
			, TRUNC(( row_number() over(partition by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID ) -1)/5) AS PAGENO
			, row_number() over(partition by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,DT_URIAGEM.GYONO,WK_PRT000.LOGINID)  AS 連番
			, DT_URIAGEH.SEIKYUSHONO AS 請求書番号						--請求書番号
			, 日付記号追加(DT_URIAGEH.SEIKYUYMD) AS 請求日付			--請求日付
			, DT_URIAGEH.JIGYOCD || '-' || DT_URIAGEH.SAGYOBKBN || '-' || DT_URIAGEH.RENNO AS 物件番号			--物件番号
			, DT_URIAGEH.ZIPCODE AS 郵便番号							--郵便番号
			, DT_URIAGEH.ADD1 AS 住所1									--住所1
			, DT_URIAGEH.ADD2 AS 住所2									--住所2
			, DT_URIAGEH.SEIKYUNM AS 請求先名							--請求先名
			, DT_URIAGEH.SENBUSHONM AS 先方部署名						--先方部署名
			, DT_URIAGEH.SENTANTNM AS 先方担当者名						--先方担当者名
			, DT_URIAGEH.NONYUNM AS 納入先名							--納入先名
			, 日付記号追加(DT_URIAGEH.KAISHUYOTEIYMD) AS 回収予定日					--回収予定日
			, DT_URIAGEM.GYONO AS 行番号								--行番号
			, DECODE(DT_URIAGEM.MMDD,NULL,NULL,SUBSTR(DT_URIAGEM.MMDD,1,2) || '/' || SUBSTR(DT_URIAGEM.MMDD,3,2))  AS 月日
			, DT_URIAGEM.HINNM1 AS 品名1								--品名1
			, DT_URIAGEM.HINNM2 AS 品名2								--品名2
			, DT_URIAGEM.SURYO AS 数量									--数量
			, DT_URIAGEM.TANINM AS 単位名								--単位名
			, DT_URIAGEM.TANKA AS 単価									--単価
			, DT_URIAGEM.KING AS 金額									--金額
			, DT_URIAGEM.TAX AS 消費税									--消費税
			-- ↓2023/08/21 UPDATE TC KANDA 請求日が2023年9月30日までのものは旧の事業所情報を表示
			--, DM_JIGYO.ZIPCODE AS 事業所郵便番号						--
			--, DM_JIGYO.ADD1 AS 事業所住所1								--
			--, DM_JIGYO.ADD2 AS 事業所住所2								--
			--, DM_JIGYO.TELNO AS 事業所電話番号							--
			--, DM_JIGYO.FAXNO AS 事業所FAX番号							--
			--, DECODE(DM_JIGYO.JIGYOCD,'02','関東サービス工場',DM_JIGYO.JIGYONM) AS 事業所名								--
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ZIPCODE ELSE DM_JIGYO.ZIPCODE END) AS 事業所郵便番号
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ADD1 ELSE DM_JIGYO.ADD1 END) AS 事業所住所1
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ADD2 ELSE DM_JIGYO.ADD2 END) AS 事業所住所2
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_TELNO ELSE DM_JIGYO.TELNO END) AS 事業所電話番号
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_FAXNO ELSE DM_JIGYO.FAXNO END) AS 事業所FAX番号
			,  DECODE(DM_JIGYO.JIGYOCD,'02','関東サービス工場',(CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_JIGYONM ELSE DM_JIGYO.JIGYONM END)) AS 事業所名
			-- ↑2023/08/21 UPDATE TC KANDA 請求日が2023年9月30日までのものは旧の事業所情報を表示
			, DECODE(DM_NONYU.GINKOKBN,0,DM_JIGYO.FURIGINKONM,DM_JIGYO.TOKUGINKONM) AS 銀行名
			, WK_PRT000.LOGINID 										--ログインID
			, WK_PRT000.PROGID
			, WK_PRT000.KINGAKU
			-- ↓2023/08/21 UPDATE TC KANDA 税区分が0(課税)の場合は合計額の10%の四捨五入 1(非課税)の場合は税額0
			--, WK_PRT000.ZEI AS ZEI
			--, DT_URIAGEH.BUKKENMEMO AS メモ
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN WK_PRT000.ZEI ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(WK_PRT000.KINGAKU/10),0) END) AS ZEI
			, DT_URIAGEH.BUKKENMEMO AS メモ
			, DT_URIAGEH.TAXKBN AS 税区分
			-- ↑2023/08/21 UPDATE TC KANDA 税区分が0(課税)の場合は合計額の10%の四捨五入 1(非課税)の場合は税額0
		FROM WK_PRT000,DT_URIAGEH,DT_URIAGEM
			,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT000.PROGID = 'OMP601'
			  AND	DT_URIAGEH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_URIAGEH.SEIKYUSHONO = WK_PRT000.DENPNO
					-- 明細と結合
              AND	WK_PRT000.DENPNO = DT_URIAGEM.SEIKYUSHONO
              AND	WK_PRT000.GYONO = DT_URIAGEM.GYONO
              		-- 事業所マスタ
              AND	DM_JIGYO.JIGYOCD = DT_URIAGEH.JIGYOCD
					-- 納入先マスタ
			  AND	DT_URIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00'				= DM_NONYU.SECCHIKBN
              AND	DT_URIAGEH.DELKBN = '0'
              AND	DT_URIAGEM.DELKBN = '0'
UNION ALL
--納品書
		SELECT
			  '2' AS 区分
			, TRUNC(( row_number() over(partition by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID ) -1)/5) AS PAGENO
			, row_number() over(partition by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,DT_URIAGEM.GYONO,WK_PRT000.LOGINID)  AS 連番
			, DT_URIAGEH.SEIKYUSHONO AS 請求書番号						--請求書番号
			, 日付記号追加(DT_URIAGEH.SEIKYUYMD) AS 請求日付			--請求日付
			, DT_URIAGEH.JIGYOCD || '-' || DT_URIAGEH.SAGYOBKBN || '-' || DT_URIAGEH.RENNO AS 物件番号			--物件番号
			, DT_URIAGEH.ZIPCODE AS 郵便番号							--郵便番号
			, DT_URIAGEH.ADD1 AS 住所1									--住所1
			, DT_URIAGEH.ADD2 AS 住所2									--住所2
			, DT_URIAGEH.SEIKYUNM AS 請求先名							--請求先名
			, DT_URIAGEH.SENBUSHONM AS 先方部署名						--先方部署名
			, DT_URIAGEH.SENTANTNM AS 先方担当者名						--先方担当者名
			, DT_URIAGEH.NONYUNM AS 納入先名							--納入先名
			, 日付記号追加(DT_URIAGEH.KAISHUYOTEIYMD) AS 回収予定日					--回収予定日
			, DT_URIAGEM.GYONO AS 行番号								--行番号
			, DECODE(DT_URIAGEM.MMDD,NULL,NULL,SUBSTR(DT_URIAGEM.MMDD,1,2) || '/' || SUBSTR(DT_URIAGEM.MMDD,3,2))  AS 月日
			, DT_URIAGEM.HINNM1 AS 品名1								--品名1
			, DT_URIAGEM.HINNM2 AS 品名2								--品名2
			, DT_URIAGEM.SURYO AS 数量									--数量
			, DT_URIAGEM.TANINM AS 単位名								--単位名
			, DT_URIAGEM.TANKA AS 単価									--単価
			, DT_URIAGEM.KING AS 金額									--金額
			, DT_URIAGEM.TAX AS 消費税									--消費税
			-- ↓2023/08/21 UPDATE TC KANDA 請求日が2023年9月30日までのものは旧の事業所情報を表示
			--, DM_JIGYO.ZIPCODE AS 事業所郵便番号						--
			--, DM_JIGYO.ADD1 AS 事業所住所1								--
			--, DM_JIGYO.ADD2 AS 事業所住所2								--
			--, DM_JIGYO.TELNO AS 事業所電話番号							--
			--, DM_JIGYO.FAXNO AS 事業所FAX番号							--
			--, DECODE(DM_JIGYO.JIGYOCD,'02','関東サービス工場',DM_JIGYO.JIGYONM) AS 事業所名								--
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ZIPCODE ELSE DM_JIGYO.ZIPCODE END) AS 事業所郵便番号
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ADD1 ELSE DM_JIGYO.ADD1 END) AS 事業所住所1
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ADD2 ELSE DM_JIGYO.ADD2 END) AS 事業所住所2
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_TELNO ELSE DM_JIGYO.TELNO END) AS 事業所電話番号
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_FAXNO ELSE DM_JIGYO.FAXNO END) AS 事業所FAX番号
			,  DECODE(DM_JIGYO.JIGYOCD,'02','関東サービス工場',(CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_JIGYONM ELSE DM_JIGYO.JIGYONM END)) AS 事業所名
			-- ↑2023/08/21 UPDATE TC KANDA 請求日が2023年9月30日までのものは旧の事業所情報を表示
			, NULL AS 銀行名
			, WK_PRT000.LOGINID 										--ログインID
			, WK_PRT000.PROGID
			, WK_PRT000.KINGAKU
			-- ↓2023/08/21 UPDATE TC KANDA 税区分が0(課税)の場合は合計額の10%の四捨五入 1(非課税)の場合は税額0
			--, WK_PRT000.ZEI AS ZEI
			--, DT_URIAGEH.BUKKENMEMO AS メモ
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN WK_PRT000.ZEI ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(WK_PRT000.KINGAKU/10),0) END) AS ZEI
			, DT_URIAGEH.BUKKENMEMO AS メモ
			, DT_URIAGEH.TAXKBN AS 税区分
			-- ↑2023/08/21 UPDATE TC KANDA 税区分が0(課税)の場合は合計額の10%の四捨五入 1(非課税)の場合は税額0
		FROM WK_PRT000,DT_URIAGEH,DT_URIAGEM
			,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT000.PROGID = 'OMP601'
			  AND	DT_URIAGEH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_URIAGEH.SEIKYUSHONO = WK_PRT000.DENPNO
					-- 明細と結合
              AND	WK_PRT000.DENPNO = DT_URIAGEM.SEIKYUSHONO
              AND	WK_PRT000.GYONO = DT_URIAGEM.GYONO
              		-- 事業所マスタ
              AND	DM_JIGYO.JIGYOCD = DT_URIAGEH.JIGYOCD
					-- 納入先マスタ
			  AND	DT_URIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00'				= DM_NONYU.SECCHIKBN
              AND	DT_URIAGEH.DELKBN = '0'
              AND	DT_URIAGEM.DELKBN = '0'
UNION ALL
--売上伝票
		SELECT
			  '3' AS 区分
			, TRUNC(( row_number() over(partition by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID ) -1)/5) AS PAGENO
			, row_number() over(partition by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,DT_URIAGEM.GYONO,WK_PRT000.LOGINID)  AS 連番
			, DT_URIAGEH.SEIKYUSHONO AS 請求書番号						--請求書番号
			, 日付記号追加(DT_URIAGEH.SEIKYUYMD) AS 請求日付			--請求日付
			, DT_URIAGEH.JIGYOCD || '-' || DT_URIAGEH.SAGYOBKBN || '-' || DT_URIAGEH.RENNO AS 物件番号			--物件番号
			, DT_URIAGEH.ZIPCODE AS 郵便番号							--郵便番号
			, DT_URIAGEH.ADD1 AS 住所1									--住所1
			, DT_URIAGEH.ADD2 AS 住所2									--住所2
			, DT_URIAGEH.SEIKYUNM AS 請求先名							--請求先名
			, DT_URIAGEH.SENBUSHONM AS 先方部署名						--先方部署名
			, DT_URIAGEH.SENTANTNM AS 先方担当者名						--先方担当者名
			, DT_URIAGEH.NONYUNM AS 納入先名							--納入先名
			, NULL AS 回収予定日										--回収予定日
			, DT_URIAGEM.GYONO AS 行番号								--行番号
			, DECODE(DT_URIAGEM.MMDD,NULL,NULL,SUBSTR(DT_URIAGEM.MMDD,1,2) || '/' || SUBSTR(DT_URIAGEM.MMDD,3,2))  AS 月日
			, DT_URIAGEM.HINNM1 AS 品名1								--品名1
			, DT_URIAGEM.HINNM2 AS 品名2								--品名2
			, DT_URIAGEM.SURYO AS 数量									--数量
			, DT_URIAGEM.TANINM AS 単位名								--単位名
			, DT_URIAGEM.TANKA AS 単価									--単価
			, DT_URIAGEM.KING AS 金額									--金額
			, DT_URIAGEM.TAX AS 消費税									--消費税
			-- ↓2023/08/21 UPDATE TC KANDA 請求日が2023年9月30日までのものは旧の事業所情報を表示
			--, DM_JIGYO.ZIPCODE AS 事業所郵便番号						--
			--, DM_JIGYO.ADD1 AS 事業所住所1								--
			--, DM_JIGYO.ADD2 AS 事業所住所2								--
			--, DM_JIGYO.TELNO AS 事業所電話番号							--
			--, DM_JIGYO.FAXNO AS 事業所FAX番号							--
			--, DECODE(DM_JIGYO.JIGYOCD,'02','関東サービス工場',DM_JIGYO.JIGYONM) AS 事業所名								--
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ZIPCODE ELSE DM_JIGYO.ZIPCODE END) AS 事業所郵便番号
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ADD1 ELSE DM_JIGYO.ADD1 END) AS 事業所住所1
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ADD2 ELSE DM_JIGYO.ADD2 END) AS 事業所住所2
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_TELNO ELSE DM_JIGYO.TELNO END) AS 事業所電話番号
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_FAXNO ELSE DM_JIGYO.FAXNO END) AS 事業所FAX番号
			,  DECODE(DM_JIGYO.JIGYOCD,'02','関東サービス工場',(CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_JIGYONM ELSE DM_JIGYO.JIGYONM END)) AS 事業所名
			-- ↑2023/08/21 UPDATE TC KANDA 請求日が2023年9月30日までのものは旧の事業所情報を表示
			, NULL AS 銀行名
			, WK_PRT000.LOGINID 										--ログインID
			, WK_PRT000.PROGID
			, WK_PRT000.KINGAKU
			-- ↓2023/08/21 UPDATE TC KANDA 税区分が0(課税)の場合は合計額の10%の四捨五入 1(非課税)の場合は税額0
			--, WK_PRT000.ZEI AS ZEI
			--, DT_URIAGEH.BUKKENMEMO AS メモ
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN WK_PRT000.ZEI ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(WK_PRT000.KINGAKU/10),0) END) AS ZEI
			, DT_URIAGEH.BUKKENMEMO AS メモ
			, DT_URIAGEH.TAXKBN AS 税区分
			-- ↑2023/08/21 UPDATE TC KANDA 税区分が0(課税)の場合は合計額の10%の四捨五入 1(非課税)の場合は税額0
		FROM WK_PRT000,DT_URIAGEH,DT_URIAGEM
			,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT000.PROGID = 'OMP601'
			  AND	DT_URIAGEH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_URIAGEH.SEIKYUSHONO = WK_PRT000.DENPNO
					-- 明細と結合
              AND	WK_PRT000.DENPNO = DT_URIAGEM.SEIKYUSHONO
              AND	WK_PRT000.GYONO = DT_URIAGEM.GYONO
              		-- 事業所マスタ
              AND	DM_JIGYO.JIGYOCD = DT_URIAGEH.JIGYOCD
					-- 納入先マスタ
			  AND	DT_URIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00'				= DM_NONYU.SECCHIKBN
              AND	DT_URIAGEH.DELKBN = '0'
              AND	DT_URIAGEM.DELKBN = '0'
			) WK
		ORDER BY WK.請求書番号,WK.PAGENO,WK.区分,WK.連番
;
