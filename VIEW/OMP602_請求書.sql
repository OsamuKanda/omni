-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/07   OKADA
--                                                 Update:2012/04/24   OKADA
--
-- 事業所コード=02の場合は、マスタ内の名称ではなく"関東サービス工場"として印刷する。
--
-------------------------------------------------------------------------------
--OMP602 合計請求書
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP602
AS
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
		FROM
(		SELECT   
			  '1' AS 区分
			, TRUNC(( row_number() over(partition by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID ) -1)/5) AS PAGENO
			, row_number() over(partition by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,DT_GURIAGEM.GYONO,WK_PRT000.LOGINID)  AS 連番
			, DT_GURIAGEH.SEIKYUSHONO AS 請求書番号						--請求書番号
			, 日付記号追加(DT_GURIAGEH.SEIKYUYMD) AS 請求日付			--請求日付
			, DT_GURIAGEH.JIGYOCD || '-' || DT_GURIAGEH.SAGYOBKBN || '-' || DT_GURIAGEH.RENNO AS 物件番号			--物件番号
			, DT_GURIAGEH.ZIPCODE AS 郵便番号							--郵便番号
			, DT_GURIAGEH.ADD1 AS 住所1									--住所1
			, DT_GURIAGEH.ADD2 AS 住所2									--住所2
			, DT_GURIAGEH.SEIKYUNM AS 請求先名							--請求先名
			, DT_GURIAGEH.SENBUSHONM AS 先方部署名						--先方部署名
			, DT_GURIAGEH.SENTANTNM AS 先方担当者名						--先方担当者名
			, DT_GURIAGEH.NONYUNM AS 納入先名							--納入先名
			, 日付記号追加(DT_GURIAGEH.KAISHUYOTEIYMD) AS 回収予定日					--回収予定日
			, DT_GURIAGEM.GYONO AS 行番号								--行番号
			, DECODE(DT_GURIAGEM.MMDD,NULL,NULL,SUBSTR(DT_GURIAGEM.MMDD,1,2) || '/' || SUBSTR(DT_GURIAGEM.MMDD,3,2))  AS 月日
			, DT_GURIAGEM.HINNM1 AS 品名1								--品名1
			, DT_GURIAGEM.HINNM2 AS 品名2								--品名2
			, DT_GURIAGEM.SURYO AS 数量									--数量
			, DT_GURIAGEM.TANINM AS 単位名								--単位名
			, DT_GURIAGEM.TANKA AS 単価									--単価
			, DT_GURIAGEM.KING AS 金額									--金額
			, DT_GURIAGEM.TAX AS 消費税									--消費税
			, DM_JIGYO.ZIPCODE AS 事業所郵便番号						--
			, DM_JIGYO.ADD1 AS 事業所住所1								--
			, DM_JIGYO.ADD2 AS 事業所住所2								--
			, DM_JIGYO.TELNO AS 事業所電話番号							--
			, DM_JIGYO.FAXNO AS 事業所FAX番号							--
			, DECODE(DM_JIGYO.JIGYOCD,'02','関東サービス工場',DM_JIGYO.JIGYONM) AS 事業所名								--
			, DECODE(DM_NONYU.GINKOKBN,0,DM_JIGYO.FURIGINKONM,DM_JIGYO.TOKUGINKONM) AS 銀行名
			, WK_PRT000.LOGINID 										--ログインID
			, WK_PRT000.PROGID
			, WK_PRT000.KINGAKU
			, WK_PRT000.ZEI
			, DT_GURIAGEH.BUKKENMEMO AS メモ
		FROM WK_PRT000,DT_GURIAGEH,DT_GURIAGEM
			,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT000.PROGID = 'OMP602'
			  AND	DT_GURIAGEH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_GURIAGEH.SEIKYUSHONO = WK_PRT000.DENPNO
					-- 明細と結合
              AND	WK_PRT000.DENPNO = DT_GURIAGEM.SEIKYUSHONO
              AND	WK_PRT000.GYONO = DT_GURIAGEM.GYONO
              		-- 事業所マスタ
              AND	DM_JIGYO.JIGYOCD = DT_GURIAGEH.JIGYOCD
					-- 納入先マスタ
			  AND	DT_GURIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00'				= DM_NONYU.SECCHIKBN
              AND	DT_GURIAGEH.DELKBN = '0'
              AND	DT_GURIAGEM.DELKBN = '0'
UNION ALL
--納品書
		SELECT   
			  '2' AS 区分
			, TRUNC(( row_number() over(partition by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID ) -1)/5) AS PAGENO
			, row_number() over(partition by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,DT_GURIAGEM.GYONO,WK_PRT000.LOGINID)  AS 連番
			, DT_GURIAGEH.SEIKYUSHONO AS 請求書番号						--請求書番号
			, 日付記号追加(DT_GURIAGEH.SEIKYUYMD) AS 請求日付			--請求日付
			, DT_GURIAGEH.JIGYOCD || '-' || DT_GURIAGEH.SAGYOBKBN || '-' || DT_GURIAGEH.RENNO AS 物件番号			--物件番号
			, DT_GURIAGEH.ZIPCODE AS 郵便番号							--郵便番号
			, DT_GURIAGEH.ADD1 AS 住所1									--住所1
			, DT_GURIAGEH.ADD2 AS 住所2									--住所2
			, DT_GURIAGEH.SEIKYUNM AS 請求先名							--請求先名
			, DT_GURIAGEH.SENBUSHONM AS 先方部署名						--先方部署名
			, DT_GURIAGEH.SENTANTNM AS 先方担当者名						--先方担当者名
			, DT_GURIAGEH.NONYUNM AS 納入先名							--納入先名
			, 日付記号追加(DT_GURIAGEH.KAISHUYOTEIYMD) AS 回収予定日					--回収予定日
			, DT_GURIAGEM.GYONO AS 行番号								--行番号
			, DECODE(DT_GURIAGEM.MMDD,NULL,NULL,SUBSTR(DT_GURIAGEM.MMDD,1,2) || '/' || SUBSTR(DT_GURIAGEM.MMDD,3,2))  AS 月日
			, DT_GURIAGEM.HINNM1 AS 品名1								--品名1
			, DT_GURIAGEM.HINNM2 AS 品名2								--品名2
			, DT_GURIAGEM.SURYO AS 数量									--数量
			, DT_GURIAGEM.TANINM AS 単位名								--単位名
			, DT_GURIAGEM.TANKA AS 単価									--単価
			, DT_GURIAGEM.KING AS 金額									--金額
			, DT_GURIAGEM.TAX AS 消費税									--消費税
			, DM_JIGYO.ZIPCODE AS 事業所郵便番号						--
			, DM_JIGYO.ADD1 AS 事業所住所1								--
			, DM_JIGYO.ADD2 AS 事業所住所2								--
			, DM_JIGYO.TELNO AS 事業所電話番号							--
			, DM_JIGYO.FAXNO AS 事業所FAX番号							--
			, DECODE(DM_JIGYO.JIGYOCD,'02','関東サービス工場',DM_JIGYO.JIGYONM) AS 事業所名								--
			, NULL AS 銀行名
			, WK_PRT000.LOGINID 										--ログインID
			, WK_PRT000.PROGID
			, WK_PRT000.KINGAKU
			, WK_PRT000.ZEI
			, DT_GURIAGEH.BUKKENMEMO AS メモ
		FROM WK_PRT000,DT_GURIAGEH,DT_GURIAGEM
			,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT000.PROGID = 'OMP602'
			  AND	DT_GURIAGEH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_GURIAGEH.SEIKYUSHONO = WK_PRT000.DENPNO
					-- 明細と結合
              AND	WK_PRT000.DENPNO = DT_GURIAGEM.SEIKYUSHONO
              AND	WK_PRT000.GYONO = DT_GURIAGEM.GYONO
              		-- 事業所マスタ
              AND	DM_JIGYO.JIGYOCD = DT_GURIAGEH.JIGYOCD
					-- 納入先マスタ
			  AND	DT_GURIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00'				= DM_NONYU.SECCHIKBN
              AND	DT_GURIAGEH.DELKBN = '0'
              AND	DT_GURIAGEM.DELKBN = '0'
UNION ALL
--売上伝票
		SELECT   
			  '3' AS 区分
			, TRUNC(( row_number() over(partition by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID ) -1)/5) AS PAGENO
			, row_number() over(partition by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,DT_GURIAGEM.GYONO,WK_PRT000.LOGINID)  AS 連番
			, DT_GURIAGEH.SEIKYUSHONO AS 請求書番号						--請求書番号
			, 日付記号追加(DT_GURIAGEH.SEIKYUYMD) AS 請求日付			--請求日付
			, DT_GURIAGEH.JIGYOCD || '-' || DT_GURIAGEH.SAGYOBKBN || '-' || DT_GURIAGEH.RENNO AS 物件番号			--物件番号
			, DT_GURIAGEH.ZIPCODE AS 郵便番号							--郵便番号
			, DT_GURIAGEH.ADD1 AS 住所1									--住所1
			, DT_GURIAGEH.ADD2 AS 住所2									--住所2
			, DT_GURIAGEH.SEIKYUNM AS 請求先名							--請求先名
			, DT_GURIAGEH.SENBUSHONM AS 先方部署名						--先方部署名
			, DT_GURIAGEH.SENTANTNM AS 先方担当者名						--先方担当者名
			, DT_GURIAGEH.NONYUNM AS 納入先名							--納入先名
			, NULL AS 回収予定日										--回収予定日
			, DT_GURIAGEM.GYONO AS 行番号								--行番号
			, DECODE(DT_GURIAGEM.MMDD,NULL,NULL,SUBSTR(DT_GURIAGEM.MMDD,1,2) || '/' || SUBSTR(DT_GURIAGEM.MMDD,3,2))  AS 月日
			, DT_GURIAGEM.HINNM1 AS 品名1								--品名1
			, DT_GURIAGEM.HINNM2 AS 品名2								--品名2
			, DT_GURIAGEM.SURYO AS 数量									--数量
			, DT_GURIAGEM.TANINM AS 単位名								--単位名
			, DT_GURIAGEM.TANKA AS 単価									--単価
			, DT_GURIAGEM.KING AS 金額									--金額
			, DT_GURIAGEM.TAX AS 消費税									--消費税
			, DM_JIGYO.ZIPCODE AS 事業所郵便番号						--
			, DM_JIGYO.ADD1 AS 事業所住所1								--
			, DM_JIGYO.ADD2 AS 事業所住所2								--
			, DM_JIGYO.TELNO AS 事業所電話番号							--
			, DM_JIGYO.FAXNO AS 事業所FAX番号							--
			, DECODE(DM_JIGYO.JIGYOCD,'02','関東サービス工場',DM_JIGYO.JIGYONM) AS 事業所名								--
			, NULL AS 銀行名
			, WK_PRT000.LOGINID 										--ログインID
			, WK_PRT000.PROGID
			, WK_PRT000.KINGAKU
			, WK_PRT000.ZEI
			, DT_GURIAGEH.BUKKENMEMO AS メモ
		FROM WK_PRT000,DT_GURIAGEH,DT_GURIAGEM
			,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT000.PROGID = 'OMP602'
			  AND	DT_GURIAGEH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_GURIAGEH.SEIKYUSHONO = WK_PRT000.DENPNO
					-- 明細と結合
              AND	WK_PRT000.DENPNO = DT_GURIAGEM.SEIKYUSHONO
              AND	WK_PRT000.GYONO = DT_GURIAGEM.GYONO
              		-- 事業所マスタ
              AND	DM_JIGYO.JIGYOCD = DT_GURIAGEH.JIGYOCD
					-- 納入先マスタ
			  AND	DT_GURIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00'				= DM_NONYU.SECCHIKBN
              AND	DT_GURIAGEH.DELKBN = '0'
              AND	DT_GURIAGEM.DELKBN = '0'
			) WK
		ORDER BY WK.請求書番号,WK.PAGENO,WK.区分,WK.連番
;
