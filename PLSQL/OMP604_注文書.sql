-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP604 注文書
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP604
AS
		SELECT   
              ROWNUM  AS EDANUM
			, DT_HACCHUH.HACCHUJIGYOCD AS 発注事業所CD					--発注事業所コード
			, DT_HACCHUH.HACCHUJIGYOCD || '-' || DT_HACCHUH.HACCHUNO AS 発注番号							--発注番号
			, DT_HACCHUH.SIRCD AS 仕入先CD								--仕入先コード
			, DM_SHIRE.SIRNM1 AS 仕入先名1								--仕入先名1
			, DM_SHIRE.SIRNM2 AS 仕入先名2								--仕入先名2
			, DT_HACCHUH.SENTANTNM AS 先方担当者 				--先方担当者
			, 日付記号追加(DT_HACCHUH.HACCHUYMD) AS 発注日				--発注日
			, DM_JIGYO.JIGYONM AS 事業所名								--事業所名
			, DM_JIGYO.ZIPCODE AS 郵便番号								--郵便番号
			, DM_JIGYO.ADD1 AS 住所1									--住所1
			, DM_JIGYO.ADD2 AS 住所2									--住所2
			, DM_JIGYO.TELNO AS 電話番号								--電話番号
			, DM_JIGYO.FAXNO AS FAX番号									--FAX番号
			, DT_HACCHUH.TANTCD AS 発注担当者CD							--発注担当者CD
			, DM_TANT.TANTNM AS 担当者名								--担当者名
			, TO_NUMBER(DT_HACCHUM.GYONO) AS 行番号						--行番号
			, DT_HACCHUM.BBUNRUICD AS 分類CD							--分類コード
			, DT_HACCHUM.BBUNRUINM AS 分類名							--分類名
			, DT_HACCHUM.BKIKAKUCD AS 規格CD							--規格コード
			, DT_HACCHUM.BKIKAKUNM AS 規格名							--規格名
			, DT_HACCHUM.HACCHUSU AS 数量								--数量
			, DT_HACCHUM.TANICD AS 単位CD								--単位コード
			, DM_TANI.TANINM AS 単位名									--単位名
			, DT_HACCHUM.NONYUKBN AS 納入場所区分						--納入場所区分
			, DK_NONYU.NONYUKBNNM AS 納入場所							--納入場所
			, 日付記号追加(DT_HACCHUM.NONYUYMD) AS 納期日付				--納期日付
			, DT_HACCHUM.NOKIKBN AS 納期区分							--納期区分
			, DK_NOKI.NOKIKBNNM AS 納期区分名							--納期区分名
			, DT_HACCHUM.BUKKENNM AS 物件名								--物件名
			, 日付記号追加(DT_HACCHUM.KOJIYOTEIYMD) AS 工事予定日		--工事予定日
			, DT_HACCHUM.JIGYOCD || '-' || DT_HACCHUM.SAGYOBKBN || '-' || DT_HACCHUM.RENNO AS 物件番号
			, DT_HACCHUH.BIKO AS 備考									--備考
			, DT_HACCHUH.BIKO1 AS 備考１								--備考
			, DT_HACCHUH.BIKO2 AS 備考２								--備考
			, WK_PRT000.LOGINID 										--ログインID
		FROM WK_PRT000,DT_HACCHUH,DT_HACCHUM
			,DM_SHIRE,DM_JIGYO,DM_TANT,DM_TANI
			,DK_NONYU,DK_NOKI
		WHERE
					WK_PRT000.PROGID = 'OMP604'
			  AND	DT_HACCHUH.HACCHUJIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_HACCHUH.HACCHUNO = WK_PRT000.DENPNO
					-- 明細と結合
              AND	DT_HACCHUH.HACCHUJIGYOCD = DT_HACCHUM.HACCHUJIGYOCD
              AND	DT_HACCHUH.HACCHUNO = DT_HACCHUM.HACCHUNO
              AND	WK_PRT000.GYONO = DT_HACCHUM.GYONO
              		-- 仕入先マスタ
              AND	DM_SHIRE.SIRCD(+) = DT_HACCHUH.SIRCD
              		-- 事業所マスタ
              AND	DM_JIGYO.JIGYOCD = DT_HACCHUH.HACCHUJIGYOCD
              		-- 担当者マスタ
              AND	DM_TANT.TANTCD = DT_HACCHUH.TANTCD
              		-- 単位マスタ
              AND	DM_TANI.TANICD(+) = DT_HACCHUM.TANICD
              		-- 納入場所区分マスタ
              AND	DK_NONYU.NONYUKBN(+) = DT_HACCHUM.NONYUKBN
              		-- 納期区分マスタ
              AND	DK_NOKI.NOKIKBN(+) = DT_HACCHUM.NOKIKBN
              AND	DT_HACCHUH.DELKBN = '0'
              AND	DT_HACCHUM.DELKBN = '0'
        ORDER BY
        			  DT_HACCHUH.HACCHUJIGYOCD
        			, DT_HACCHUH.HACCHUNO
        			, DT_HACCHUH.HACCHUYMD
        			, TO_NUMBER(DT_HACCHUM.GYONO)
;
