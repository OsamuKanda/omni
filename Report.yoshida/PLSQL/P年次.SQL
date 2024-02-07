CREATE OR REPLACE PACKAGE BODY P年次 AS
	-------------------------------------------------------------------------------
	-- 【関数名】
	--			OMB004 -- 年次関連パッケージ
	-- 【内容】
	--
	--
	-- 【引数】
	--
	-- 【戻り値】
	--
	-------------------------------------------------------------------------------
	FUNCTION	年次確定更新
	(
			I_事業所CD					IN 			DM_TANT.SYOZOKJIGYOCD%TYPE
		,	I_更新プログラム			IN			DM_TANT.UDTPG1%TYPE
		,	I_担当者CD					IN			DM_TANT.TANTCD%TYPE
	)
	RETURN NUMBER
	IS
--
	V_年次基準日	DM_KANRI.MONYMD%TYPE;
--
	BEGIN
--
		Pログ.ログ出力(
					  I_事業所CD
					, I_担当者CD
					, I_更新プログラム
					, '0'
					, '年次処理の開始'
					, P共通.DIR_NAME
					, P共通.FILE_NAME
				);
--
--
-- ▽2024.01.29 データ削除基準日の変更（５年前の期初日）⇒（１０年前の期初日）
	-- SELECT	SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE(MONYMD),-60),'YYYYMMDD'),1,4) || '0930'
	SELECT	TO_CHAR(ADD_MONTHS(TO_DATE(KINENDO),-120),'YYYYMMDD')
-- △2024.01.29 データ削除基準日の変更（５年前の期初日）⇒（１０年前の期初日）
	INTO	V_年次基準日
	FROM	DM_KANRI
	;
--
		--明細コンデンス
		FOR R_請求書NO IN (
			SELECT
				DT_URIAGEH.SEIKYUSHONO AS SEIKYUSHONO
			,	DT_BUKKEN.JIGYOCD AS JIGYOCD
			,	DT_BUKKEN.SAGYOBKBN AS SAGYOBKBN
			,	DT_BUKKEN.RENNO AS RENNO
			FROM
				DT_BUKKEN
			,	DT_URIAGEH
			WHERE
			-- 	DT_BUKKEN.KANRYOYMD <> '00000000'
			-- AND
--				DT_BUKKEN.KANRYOYMD <=V_年次基準日
				DT_BUKKEN.KANRYOYMD < V_年次基準日
			AND
--				DT_BUKKEN.UKETSUKEYMD <=V_年次基準日
				DT_BUKKEN.UKETSUKEYMD < V_年次基準日
			-- AND
			-- 	DT_BUKKEN.JBKING = 0		-- 次月部品仕入金額
			-- AND
			-- 	DT_BUKKEN.JGKING = 0		-- 次月外注仕入金額
			-- AND
			-- 	DT_BUKKEN.JZKING = 0		-- 次月在庫金額
			-- AND
			-- 	DT_BUKKEN.JSKING = 0		-- 次月諸経費金額
			-- AND
			-- 	DT_BUKKEN.TBKING = 0		-- 当月部品仕入金額
			-- AND
			-- 	DT_BUKKEN.TGKING = 0		-- 当月外注仕入金額
			-- AND
			-- 	DT_BUKKEN.TZKING = 0		-- 当月在庫金額
			-- AND
			-- 	DT_BUKKEN.TSKING = 0		-- 当月諸経費金額
			-- AND
			-- 	DT_BUKKEN.ZBKING = 0		-- 前月部品仕入金額
			-- AND
			-- 	DT_BUKKEN.ZGKING = 0		-- 前月外注仕入金額
			-- AND
			-- 	DT_BUKKEN.ZZKING = 0		-- 前月在庫金額
			-- AND
			-- 	DT_BUKKEN.ZSKING = 0		-- 前月諸経費金額
			-- AND
			-- 	DT_BUKKEN.OLD2BKING = 0		-- 2ヶ月前部品仕入金額
			-- AND
			-- 	DT_BUKKEN.OLD2GKING = 0		-- 2ヶ月前外注仕入金額
			-- AND
			-- 	DT_BUKKEN.OLD2ZKING = 0		-- 2ヶ月前在庫金額
			-- AND
			-- 	DT_BUKKEN.OLD2SKING = 0		-- 2ヶ月前諸経費金額
			-- AND
			-- 	DT_BUKKEN.OLD3BKING = 0		-- 3ヶ月前部品仕入金額
			-- AND
			-- 	DT_BUKKEN.OLD3GKING = 0		-- 3ヶ月前外注仕入金額
			-- AND
			-- 	DT_BUKKEN.OLD3ZKING = 0		-- 3ヶ月前在庫金額
			-- AND
			-- 	DT_BUKKEN.OLD3SKING = 0		-- 3ヶ月前諸経費金額
			-- AND
			-- 	DT_BUKKEN.OLD4BKING = 0		-- 4ヶ月前部品仕入金額
			-- AND
			-- 	DT_BUKKEN.OLD4GKING = 0		-- 4ヶ月前外注仕入金額
			-- AND
			-- 	DT_BUKKEN.OLD4ZKING = 0		-- 4ヶ月前在庫金額
			-- AND
			-- 	DT_BUKKEN.OLD4SKING = 0		-- 4ヶ月前諸経費金額
			AND
				DT_URIAGEH.JIGYOCD(+) = DT_BUKKEN.JIGYOCD
			AND
				DT_URIAGEH.SAGYOBKBN(+) = DT_BUKKEN.SAGYOBKBN
			AND
				DT_URIAGEH.RENNO(+) = DT_BUKKEN.RENNO
			-- AND
			-- 	DT_URIAGEH.OUTKBN(+) = '1'
			AND
				DT_URIAGEH.DELKBN(+) = '0'
		) LOOP
			-- 売り上げデータがあれば消去
			IF R_請求書NO.SEIKYUSHONO IS NOT NULL THEN
--
				DELETE
				FROM
					DT_URIAGEM
				WHERE
					SEIKYUSHONO = R_請求書NO.SEIKYUSHONO
				;
	--
				DELETE
				FROM
					DT_NYUKINM
				WHERE
					SEIKYUSHONO = R_請求書NO.SEIKYUSHONO
				;
	--
				DELETE
				FROM
					DT_URIAGEH
				WHERE
					SEIKYUSHONO = R_請求書NO.SEIKYUSHONO
				;
	--
			END IF;

			DELETE
			FROM
				DT_BUKKENTANT
			WHERE
				JIGYOCD = R_請求書NO.JIGYOCD
			AND
				SAGYOBKBN = R_請求書NO.SAGYOBKBN
			AND
				RENNO = R_請求書NO.RENNO
			;
--
			DELETE
			FROM
				DT_BUKKEN
			WHERE
				JIGYOCD = R_請求書NO.JIGYOCD
			AND
				SAGYOBKBN = R_請求書NO.SAGYOBKBN
			AND
				RENNO = R_請求書NO.RENNO
			;
		END LOOP;
-- 古い入金明細を消去する
	DELETE
	FROM
		DT_NYUKINM N 
	WHERE
		N.SEIKYUSHONO IN (SELECT H.SEIKYUSHONO FROM DT_URIAGEH H  WHERE H.KANRYOYMD = '00000000' AND H.SEIKYUYMD < V_年次基準日 )
	;
-- 古い売り上げ明細を消去する
	DELETE
	FROM
		DT_URIAGEM M
	WHERE
		M.SEIKYUSHONO IN (SELECT H.SEIKYUSHONO FROM DT_URIAGEH H WHERE H.KANRYOYMD = '00000000' AND H.SEIKYUYMD < V_年次基準日 )
	;
-- 古い売り上げヘッダを消去する
	DELETE
	FROM
		DT_URIAGEH H
	WHERE
			H.SEIKYUYMD < V_年次基準日
		AND H.KANRYOYMD = '00000000'
	;
-- 期を繰り上げる
		UPDATE	DM_KANRI
		SET
-- ▽2024.01.29 オムニヨシダとの統合により、2303年10月1日～2024年3月31日で締め、期年度を4月1日とする
--			KINENDO = to_CHAR(add_months(KINENDO,12),'YYYYMMDD')
			KINENDO = DECODE(KINENDO,'20231001',TO_CHAR(ADD_MONTHS(KINENDO,6),'YYYYMMDD'),TO_CHAR(add_months(KINENDO,12),'YYYYMMDD'))
-- △2024.01.29 オムニヨシダとの統合により、2303年10月1日～2024年3月31日で締め、期年度を4月1日とする
		,	KISU = KISU + 1
		WHERE KANRINO = 1
		;
--
		Pログ.ログ出力(
				  I_事業所CD
				, I_担当者CD
				, I_更新プログラム
				, '0'
				, '年次処理の正常終了'
				, P共通.DIR_NAME
				, P共通.FILE_NAME
			);
--
	RETURN 0;
	EXCEPTION
	WHEN OTHERS THEN
		Pログ.ログ出力(
				  I_事業所CD
				, I_担当者CD
				, I_更新プログラム
				, '1'
				, '異常終了' || ' ' || sqlcode || ' ' || sqlerrm
				, P共通.DIR_NAME
				, P共通.FILE_NAME
			);
		RAISE;
	--
	END 年次確定更新;
--
END P年次;
