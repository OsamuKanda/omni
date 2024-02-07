CREATE OR REPLACE PACKAGE BODY POMP601 AS
	-------------------------------------------------------------------------------
	-- 【関数名】
	--			請求書表発行	okada
	-- 【内容】
	--
	--
	-- 【引数】
	--
	-- 【戻り値】
	--
	-------------------------------------------------------------------------------
	PROCEDURE	 OMP601
	(
			I_事業所CD					IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_更新プログラム			IN		DT_BUKKEN.UDTPG1%TYPE
		,	I_担当者CD					IN		DT_BUKKEN.TANTCD%TYPE
		,	I_発行区分					IN		DT_SHIREM.SAGYOBKBN%TYPE
		,	I_据付区分					IN		DT_SHIREM.SAGYOBKBN%TYPE
		,	I_請求FR					IN		DT_SHIREH.SIRNO%TYPE
		,	I_請求TO					IN		DT_SHIREH.SIRNO%TYPE
	)
	
	IS

--ワーク項目囲定義

----
	BEGIN

-- 'ﾜｰｸﾌｧｲﾙ削除
	DELETE FROM WK_PRT000 WHERE LOGINID = I_担当者CD AND PROGID = I_更新プログラム ;

-- 伝票発行ワーク出力
	-- 未発行分（通常）
	IF	I_発行区分 =  '0' 	THEN
		-- 据付発行区分（しない）
		IF 	I_据付区分 = '0'	THEN
			INSERT INTO WK_PRT000
				SELECT
					I_担当者CD										-- ﾛｸﾞｲﾝﾕｰｻﾞID
					, I_更新プログラム								-- ﾌﾟﾛｸﾞﾗﾑID
					, I_事業所CD									-- 営業所ｺｰﾄﾞ
					, DT_URIAGEH.SEIKYUSHONO						-- 見出し.請求書番号
					, DT_URIAGEM.GYONO								-- 明細.行番号
					, 0
					, 0
				FROM DT_URIAGEH,DT_URIAGEM

				WHERE 	DT_URIAGEH.JIGYOCD			= 	I_事業所CD
					--	明細と結合
					AND	DT_URIAGEH.SEIKYUSHONO		=	DT_URIAGEM.SEIKYUSHONO
					--	ヘッダー.印刷ﾌﾗｸﾞ
					AND DT_URIAGEH.PRINTKBN			=	'0'
					-- 	見出し.無効区分
					AND	DT_URIAGEH.DELKBN			=	'0'
					-- 	明細.無効区分
					AND	DT_URIAGEM.DELKBN			=	'0'
					--	入金完了は対象外
					AND	DT_URIAGEH.OUTKBN 			IS NULL
					--	据付分は対象外
					AND	DT_URIAGEH.SAGYOBKBN		<>	'3'
					--  保守請求分は対象外
					AND	DT_URIAGEH.HOSHUKBN			<>	'1'
				ORDER BY DT_URIAGEH.SEIKYUSHONO,TO_NUMBER(DT_URIAGEM.GYONO) ;
		-- 据付発行区分（する）
		ELSE
			INSERT INTO WK_PRT000
				SELECT
					I_担当者CD										-- ﾛｸﾞｲﾝﾕｰｻﾞID
					, I_更新プログラム								-- ﾌﾟﾛｸﾞﾗﾑID
					, I_事業所CD									-- 営業所ｺｰﾄﾞ
					, DT_URIAGEH.SEIKYUSHONO						-- 見出し.請求書番号
					, DT_URIAGEM.GYONO								-- 明細.行番号
					, 0
					, 0
				FROM DT_URIAGEH,DT_URIAGEM

				WHERE 	DT_URIAGEH.JIGYOCD			= 	I_事業所CD
					--	明細と結合
					AND	DT_URIAGEH.SEIKYUSHONO		=	DT_URIAGEM.SEIKYUSHONO
					--	ヘッダー.印刷ﾌﾗｸﾞ
					AND DT_URIAGEH.PRINTKBN			=	'0'
					-- 	見出し.無効区分
					AND	DT_URIAGEH.DELKBN			=	'0'
					-- 	明細.無効区分
					AND	DT_URIAGEM.DELKBN			=	'0'
					--	入金完了は対象外
					AND	DT_URIAGEH.OUTKBN 			IS NULL
					--  保守請求分は対象外
					AND	DT_URIAGEH.HOSHUKBN			<>	'1'
				ORDER BY DT_URIAGEH.SEIKYUSHONO,TO_NUMBER(DT_URIAGEM.GYONO) ;
		END IF ;

	-- 未発行で且つ保守点検毎月請求分のみ対象
	ELSIF I_発行区分 =  '1' 	THEN
			INSERT INTO WK_PRT000
				SELECT
					I_担当者CD										-- ﾛｸﾞｲﾝﾕｰｻﾞID
					, I_更新プログラム								-- ﾌﾟﾛｸﾞﾗﾑID
					, I_事業所CD									-- 営業所ｺｰﾄﾞ
					, DT_URIAGEH.SEIKYUSHONO						-- 見出し.請求書番号
					, DT_URIAGEM.GYONO								-- 明細.行番号
					, 0
					, 0
				FROM DT_URIAGEH,DT_URIAGEM

				WHERE 	DT_URIAGEH.JIGYOCD			= 	I_事業所CD
					--	明細と結合
					AND	DT_URIAGEH.SEIKYUSHONO		=	DT_URIAGEM.SEIKYUSHONO
					--	ヘッダー.印刷ﾌﾗｸﾞ
					AND DT_URIAGEH.PRINTKBN			=	'0'
					-- 	見出し.無効区分
					AND	DT_URIAGEH.DELKBN			=	'0'
					-- 	明細.無効区分
					AND	DT_URIAGEM.DELKBN			=	'0'
					--	入金完了は対象外
					AND	DT_URIAGEH.OUTKBN 			IS NULL
					--  保守請求分のみ対象
					AND	DT_URIAGEH.HOSHUKBN			=	'1'
				ORDER BY DT_URIAGEH.SEIKYUSHONO,TO_NUMBER(DT_URIAGEM.GYONO) ;
	ELSE
		-- 再発行
		INSERT INTO WK_PRT000
			SELECT
					I_担当者CD										-- ﾛｸﾞｲﾝﾕｰｻﾞID
					, I_更新プログラム								-- ﾌﾟﾛｸﾞﾗﾑID
					, I_事業所CD									-- 営業所ｺｰﾄﾞ
					, DT_URIAGEH.SEIKYUSHONO						-- 見出し.請求書番号
					, DT_URIAGEM.GYONO								-- 明細.行番号
					, 0
					, 0
				FROM DT_URIAGEH,DT_URIAGEM

				WHERE 	DT_URIAGEH.JIGYOCD			= 	I_事業所CD
					--	明細と結合
					AND	DT_URIAGEH.SEIKYUSHONO		=	DT_URIAGEM.SEIKYUSHONO
					--	ヘッダー.印刷ﾌﾗｸﾞ
					AND DT_URIAGEH.PRINTKBN			=	'1'
					-- 	見出し.無効区分
					AND	DT_URIAGEH.DELKBN			=	'0'
					-- 	明細.無効区分
					AND	DT_URIAGEM.DELKBN			=	'0'
					--	入金完了は対象外
					AND	DT_URIAGEH.OUTKBN 			IS NULL
					--  指定請求書番号の範囲内
					AND	DT_URIAGEH.SEIKYUSHONO		>=	I_請求FR
					AND	DT_URIAGEH.SEIKYUSHONO		<=	I_請求TO
				ORDER BY DT_URIAGEH.SEIKYUSHONO,TO_NUMBER(DT_URIAGEM.GYONO) ;
	END IF ;

--dbms_output.put_line('ｸﾞﾙｰﾌﾟ番号の付け込み');
--
	FOR R_該当データ IN (
				    SELECT
				    		  DT_URIAGEH.SEIKYUSHONO
				    		, SUM(KING) AS KING
							, SUM(TAX) AS TAX
					FROM DT_URIAGEH,DT_URIAGEM,WK_PRT000
				WHERE 	DT_URIAGEH.JIGYOCD			= 	I_事業所CD
					--	明細と結合
					AND	DT_URIAGEH.SEIKYUSHONO		=	DT_URIAGEM.SEIKYUSHONO
					--
					AND	WK_PRT000.PROGID			=	'OMP601'
					--
					AND	WK_PRT000.LOGINID			=	I_担当者CD
					--
					AND	WK_PRT000.EIGCD				=	I_事業所CD
					--
					AND	WK_PRT000.DENPNO			=	DT_URIAGEM.SEIKYUSHONO
					-- 	見出し.無効区分
					AND	DT_URIAGEH.DELKBN			=	'0'
					-- 	明細.無効区分
					AND	DT_URIAGEM.DELKBN			=	'0'
					--
					AND	WK_PRT000.GYONO			=	DT_URIAGEM.GYONO
					GROUP BY  DT_URIAGEH.SEIKYUSHONO

	) LOOP
--
		UPDATE WK_PRT000
			SET	KINGAKU			=	R_該当データ.KING
			,	ZEI				=	R_該当データ.TAX
		WHERE	LOGINID			=	I_担当者CD
		AND		PROGID			=	'OMP601'
		AND		EIGCD			=	I_事業所CD 
		AND		DENPNO			=	R_該当データ.SEIKYUSHONO ;
	END LOOP ;
--
--該当データの印刷ﾌﾗｸﾞ更新

		IF	I_発行区分 <>  '2' THEN

			FOR R_該当データ IN (
					    SELECT
							  LOGINID
							, PROGID
							, EIGCD
							, DENPNO

					    FROM    WK_PRT000

					    WHERE   	LOGINID     		=  	I_担当者CD
					    AND     	PROGID       		=   I_更新プログラム
					    AND			EIGCD				=	I_事業所CD

					    GROUP BY LOGINID,PROGID,EIGCD,DENPNO
			)LOOP	


				UPDATE	DT_URIAGEH

				SET	PRINTKBN			=	'1'
				WHERE 
					 	SEIKYUSHONO			= 	R_該当データ.DENPNO
			;
--
			END LOOP;
		END IF ;

--

	END OMP601;
END POMP601;
/
SHOW ERRORS