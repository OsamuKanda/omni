-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP703	売掛金未回収月別一覧表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP703
AS
		SELECT  
              ROWNUM  AS EDANUM
			, DT_URIAGEH.JIGYOCD AS 事業所CD										--事業所コード
			, DM_JIGYO.JIGYONM AS 事業所名											--事業所名
			, 日付記号追加(DT_URIAGEH.SEIKYUYMD) AS 請求日付						--請求日付
			, DT_URIAGEH.SEIKYUCD AS 請求先CD										--請求先コード
			, DT_URIAGEH.SEIKYUNM AS 請求先名										--請求先名
			, DT_URIAGEH.NONYUCD AS 納入先CD										--納入先コード
			, DT_URIAGEH.NONYUNM AS 納入先名										--納入先名
			, DT_URIAGEH.SEIKYUSHONO AS 請求書番号									--請求書番号
			, DT_URIAGEH.JIGYOCD || '-' || DT_URIAGEH.SAGYOBKBN || '-' || DT_URIAGEH.RENNO AS 物件番号
			, T1.請求金額															--請求金額
			, DT_URIAGEH.NYUKINR AS 累計入金額										--累計入金額
			, 日付記号追加(DT_URIAGEH.KAISHUYOTEIYMD) AS 回収予定日					--回収予定日
			, DM_NONYU.TELNO1 AS 電話1												--電話1
			, DM_NONYU.TELNO2 AS 電話2												--電話2
			, SUBSTR(DT_URIAGEH.SEIKYUYMD,1,6) AS 請求年月							--
			, DECODE(DT_URIAGEH.SEIKYUYMD,NULL,'00000000',DT_URIAGEH.SEIKYUYMD) AS 条件用請求日付
			, DECODE(DT_URIAGEH.KAISHUYOTEIYMD,NULL,'00000000',DT_URIAGEH.KAISHUYOTEIYMD)  AS 条件用回収予定日
		FROM
				-- 売上明細から請求番号毎の売上金額を算出
			(	SELECT DT_URIAGEH.SEIKYUSHONO,SUM(KING + TAX) AS 請求金額 FROM DT_URIAGEM,DT_URIAGEH
				WHERE 
					DT_URIAGEM.DELKBN = '0'
				AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM. SEIKYUSHONO
				AND DT_URIAGEH.DELKBN = '0'
				AND DT_URIAGEH.DENPYOKBN = '0'
				GROUP BY
				DT_URIAGEH.SEIKYUSHONO	)
				T1,	
			DT_URIAGEH,DM_NONYU,DM_JIGYO
		WHERE
				--   上記で取得した請求番号で入金額を取得し判定を行う。
					 T1.SEIKYUSHONO		=	DT_URIAGEH. SEIKYUSHONO
				--	 請求額 > 累計入金額
				AND  T1.請求金額 > DT_URIAGEH.NYUKINR
				--	 納入先マスタ
				AND  DT_URIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
				AND  '00' = DM_NONYU.SECCHIKBN
				-- 事業所マスタ
				AND  DT_URIAGEH.JIGYOCD = DM_JIGYO.JIGYOCD
        ORDER BY
        			  DT_URIAGEH.SEIKYUYMD
        			, DT_URIAGEH.SEIKYUCD
        			, DT_URIAGEH.SEIKYUSHONO
;

