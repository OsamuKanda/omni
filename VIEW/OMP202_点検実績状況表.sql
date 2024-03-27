-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2015/5/11   KAWAHATA
--                                                 Update:
-------------------------------------------------------------------------------
--OMP202 点検実績状況表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW OMNI.V_OMP202
    (事業所CD,事業所名,物件番号,受付日付,納入先CD,納入先名,請求先CD,請求先名,作業担当者CD,
    作業担当者名,大分類CD,大分類名,点検日付,点検台数,条件用受付日付,条件用点検日付,条件用作業分類,請求金額,契約)
AS
SELECT
				 事業所CD
				,事業所名
				,物件番号
				,受付日付
				,納入先CD
				,納入先名
				,請求先CD
				,請求先名
				,作業担当者CD
				,作業担当者名
				,大分類CD
				,大分類名
				,点検日付
				,点検台数
				,条件用受付日付
				,条件用点検日付
				,条件用作業分類
				, SUM(DT_URIAGEM.KING)	 AS 請求金額	
				,DECODE(契約日, NULL, '無','有') AS 契約
FROM																			--請求金額（税抜）
			(SELECT
			  DT_BUKKEN.JIGYOCD AS 事業所CD																			--事業所コード
			, DT_BUKKEN.SAGYOBKBN AS 作業区分
			, DT_BUKKEN.RENNO AS 連
			, MAX(DM_JIGYO.JIGYONM) AS 事業所名																		--事業所名
			, DT_BUKKEN.JIGYOCD || '-' || DT_BUKKEN.SAGYOBKBN || '-' || DT_BUKKEN.RENNO AS 物件番号					--物件番号
			, 日付記号追加(DT_BUKKEN.UKETSUKEYMD) AS 受付日付														--受付日付
			, DT_BUKKEN.NONYUCD AS 納入先CD																			--納入先コード
			, MAX(N1.NONYUNMR) AS 納入先名																				--納入先名
			, DT_BUKKEN.SEIKYUCD AS 請求先CD																		--請求先コード
			, MAX(S1.NONYUNMR) AS 請求先名																				--請求先名
			, DT_BUKKENTANT.SAGYOTANTCD1 AS 作業担当者CD															--作業担当者コード
			, MAX(SAGYO.TANTNM) AS 作業担当者名																			--作業担当者名
			, DT_BUKKEN.BUNRUIDCD AS 大分類CD																		--大分類コード
			, MAX(DM_BUNRUID.BUNRUIDNM) AS 大分類名	
			, 日付記号追加(DT_HTENKENH.TENKENYMD) AS 点検日付																					--点検日付
			, COUNT(DT_HTENKENH.GOUKI) AS 点検台数																			--点検台数
			, DT_BUKKEN.UKETSUKEYMD AS 条件用受付日付
			, DT_HTENKENH.TENKENYMD AS 条件用点検日付
			, DT_BUKKEN.SAGYOBKBN AS 条件用作業分類
			--, DT_BUKKEN.SEIKYUKBN AS 請求状態区分
			, MAX(DM_HOSHU.KEIYAKUYMD) AS 契約日
			FROM DM_JIGYO
				,DT_BUKKEN
				,DT_HTENKENH
				,DM_NONYU N1
				,DM_NONYU S1
				,DM_TANT SAGYO
				,DM_BUNRUID
				,DT_BUKKENTANT
				,DM_HOSHU
			WHERE 
			-- 事業所マスタと結合
            DT_BUKKEN.JIGYOCD = DM_JIGYO.JIGYOCD
            --点検マスタと結合
			AND DT_BUKKEN.JIGYOCD = DT_HTENKENH.JIGYOCD	
			AND DT_BUKKEN.SAGYOBKBN = DT_HTENKENH.SAGYOBKBN
			AND DT_BUKKEN.RENNO = DT_HTENKENH.RENNO
			--納入先マスタ
			 AND	N1.NONYUCD(+) = DT_BUKKEN.NONYUCD
			 AND	N1.SECCHIKBN(+) = '01'
  			-- 請求先マスタ
			 AND	S1.NONYUCD(+) = DT_BUKKEN.SEIKYUCD
		 	AND	S1.SECCHIKBN(+) = '00'
			-- 大分類マスタ
			 AND	DM_BUNRUID.BUNRUIDCD(+) = DT_BUKKEN.BUNRUIDCD
              		-- 物件別作業担当者マスタ
              AND	DT_BUKKEN.JIGYOCD = DT_BUKKENTANT.JIGYOCD(+)
              AND	DT_BUKKEN.SAGYOBKBN = DT_BUKKENTANT.SAGYOBKBN(+)
			  AND	DT_BUKKEN.RENNO = DT_BUKKENTANT.RENNO(+)
			  		--
			  AND	DT_BUKKENTANT.SAGYOTANTCD1 = SAGYO.TANTCD(+)
			--請求区分＝請求済
			AND SEIKYUKBN = '1'
			--保守点検マスタ
			AND	DM_HOSHU.NONYUCD(+) = DT_HTENKENH.NONYUCD
			AND	DM_HOSHU.GOUKI(+) = DT_HTENKENH.GOUKI	
			GROUP BY 
			 DT_BUKKEN.JIGYOCD
			,DT_BUKKEN.SAGYOBKBN
			,DT_BUKKEN.RENNO 
			,DT_BUKKEN.NONYUCD	
			,DT_BUKKEN.SEIKYUCD
			,DT_HTENKENH.TENKENYMD	
			,DT_BUKKENTANT.SAGYOTANTCD1
			,DT_BUKKEN.BUNRUIDCD
			,DT_BUKKEN.UKETSUKEYMD) BUKKEN
				,DT_URIAGEH
				,DT_URIAGEM
			WHERE
			  --売上と結合
			    BUKKEN.事業所CD = DT_URIAGEH.JIGYOCD	
			AND BUKKEN.作業区分 = DT_URIAGEH.SAGYOBKBN
			AND BUKKEN.連 = DT_URIAGEH.RENNO
			AND DT_URIAGEH.SEIKYUSHONO =  DT_URIAGEM.SEIKYUSHONO	
			GROUP BY
			 	事業所CD
				,作業区分
				,連
				,事業所名
				,物件番号
				,受付日付
				,納入先CD
				,納入先名
				,請求先CD
				,請求先名
				,作業担当者CD
				,作業担当者名
				,大分類CD
				,大分類名
				,点検日付
				,点検台数
				,条件用受付日付
				,条件用点検日付
				,条件用作業分類
				,契約日
			ORDER BY
         		事業所CD
				,作業区分
				,連
/