CREATE OR REPLACE PACKAGE BODY POMP602 AS
	-------------------------------------------------------------------------------
	-- 【関数名】
	--			合計請求書表発行	okada
	-- 【内容】
	--
	--
	-- 【引数】
	--
	-- 【戻り値】
	--
	-------------------------------------------------------------------------------
	PROCEDURE	 OMP602
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
--		IF 	I_据付区分 = '1'	THEN
			INSERT INTO WK_PRT000
				SELECT
					I_担当者CD										-- ﾛｸﾞｲﾝﾕｰｻﾞID
					, I_更新プログラム								-- ﾌﾟﾛｸﾞﾗﾑID
					, I_事業所CD									-- 営業所ｺｰﾄﾞ
					, DT_GURIAGEH.SEIKYUSHONO						-- 見出し.請求書番号
					, DT_GURIAGEM.GYONO								-- 明細.行番号
					, 0
					, 0
				FROM DT_GURIAGEH,DT_GURIAGEM

				WHERE 	DT_GURIAGEH.JIGYOCD			= 	I_事業所CD
					--	明細と結合
					AND	DT_GURIAGEH.SEIKYUSHONO		=	DT_GURIAGEM.SEIKYUSHONO
					--	ヘッダー.印刷ﾌﾗｸﾞ
					AND DT_GURIAGEH.PRINTKBN			=	'0'
					-- 	見出し.無効区分
					AND	DT_GURIAGEH.DELKBN			=	'0'
					-- 	明細.無効区分
					AND	DT_GURIAGEM.DELKBN			=	'0'
					--	入金完了は対象外
--					AND	DT_GURIAGEH.OUTKBN 			IS NULL
					--	据付分は対象外
--					AND	DT_GURIAGEH.SAGYOBKBN		<>	'3'
					--  保守請求分は対象外
--					AND	DT_GURIAGEH.HOSHUKBN			<>	'1'
				ORDER BY DT_GURIAGEH.SEIKYUSHONO,TO_NUMBER(DT_GURIAGEM.GYONO) ;
		-- 据付発行区分（する）
--		ELSE
--			INSERT INTO WK_PRT000
--				SELECT
--					I_担当者CD										-- ﾛｸﾞｲﾝﾕｰｻﾞID
--					, I_更新プログラム								-- ﾌﾟﾛｸﾞﾗﾑID
--					, I_事業所CD									-- 営業所ｺｰﾄﾞ
--					, DT_GURIAGEH.SEIKYUSHONO						-- 見出し.請求書番号
--					, DT_GURIAGEM.GYONO								-- 明細.行番号
--				FROM DT_GURIAGEH,DT_GURIAGEM
--
--				WHERE 	DT_GURIAGEH.JIGYOCD			= 	I_事業所CD
--					--	明細と結合
--					AND	DT_GURIAGEH.SEIKYUSHONO		=	DT_GURIAGEM.SEIKYUSHONO
--					--	ヘッダー.印刷ﾌﾗｸﾞ
--					AND DT_GURIAGEH.PRINTKBN			=	'0'
--					-- 	見出し.無効区分
--					AND	DT_GURIAGEH.DELKBN			=	'0'
--					-- 	明細.無効区分
--					AND	DT_GURIAGEM.DELKBN			=	'0'
--					--	入金完了は対象外
--					AND	DT_GURIAGEH.OUTKBN 			IS NULL
--					--  保守請求分は対象外
--					AND	DT_GURIAGEH.HOSHUKBN			<>	'1'
--				ORDER BY DT_GURIAGEH.SEIKYUSHONO,TO_NUMBER(DT_GURIAGEM.GYONO) ;
--		END IF ;

	-- 未発行で且つ保守点検毎月請求分のみ対象
--	ELSIF I_発行区分 =  '2' 	THEN
--			INSERT INTO WK_PRT000
--				SELECT
--					I_担当者CD										-- ﾛｸﾞｲﾝﾕｰｻﾞID
--					, I_更新プログラム								-- ﾌﾟﾛｸﾞﾗﾑID
--					, I_事業所CD									-- 営業所ｺｰﾄﾞ
--					, DT_GURIAGEH.SEIKYUSHONO						-- 見出し.請求書番号
--					, DT_GURIAGEM.GYONO								-- 明細.行番号
--				FROM DT_GURIAGEH,DT_GURIAGEM
--
--				WHERE 	DT_GURIAGEH.JIGYOCD			= 	I_事業所CD
--					--	明細と結合
--					AND	DT_GURIAGEH.SEIKYUSHONO		=	DT_GURIAGEM.SEIKYUSHONO
--					--	ヘッダー.印刷ﾌﾗｸﾞ
--					AND DT_GURIAGEH.PRINTKBN			=	'0'
--					-- 	見出し.無効区分
--					AND	DT_GURIAGEH.DELKBN			=	'0'
--					-- 	明細.無効区分
--					AND	DT_GURIAGEM.DELKBN			=	'0'
--					--	入金完了は対象外
--					AND	DT_GURIAGEH.OUTKBN 			IS NULL
--					--  保守請求分のみ対象
--					AND	DT_GURIAGEH.HOSHUKBN			=	'1'
--				ORDER BY DT_GURIAGEH.SEIKYUSHONO,TO_NUMBER(DT_GURIAGEM.GYONO) ;
	ELSE
		-- 再発行
		INSERT INTO WK_PRT000
			SELECT
					I_担当者CD										-- ﾛｸﾞｲﾝﾕｰｻﾞID
					, I_更新プログラム								-- ﾌﾟﾛｸﾞﾗﾑID
					, I_事業所CD									-- 営業所ｺｰﾄﾞ
					, DT_GURIAGEH.SEIKYUSHONO						-- 見出し.請求書番号
					, DT_GURIAGEM.GYONO								-- 明細.行番号
					, 0
					, 0
				FROM DT_GURIAGEH,DT_GURIAGEM

				WHERE 	DT_GURIAGEH.JIGYOCD			= 	I_事業所CD
					--	明細と結合
					AND	DT_GURIAGEH.SEIKYUSHONO		=	DT_GURIAGEM.SEIKYUSHONO
					--	ヘッダー.印刷ﾌﾗｸﾞ
					AND DT_GURIAGEH.PRINTKBN			=	'1'
					-- 	見出し.無効区分
					AND	DT_GURIAGEH.DELKBN			=	'0'
					-- 	明細.無効区分
					AND	DT_GURIAGEM.DELKBN			=	'0'
					--	入金完了は対象外
--					AND	DT_GURIAGEH.OUTKBN 			IS NULL
					--  指定請求書番号の範囲内
					AND	DT_GURIAGEH.SEIKYUSHONO		>=	I_請求FR
					AND	DT_GURIAGEH.SEIKYUSHONO		<=	I_請求TO
				ORDER BY DT_GURIAGEH.SEIKYUSHONO,TO_NUMBER(DT_GURIAGEM.GYONO) ;
	END IF ;
--dbms_output.put_line('ｸﾞﾙｰﾌﾟ番号の付け込み');
--
	FOR R_該当データ IN (
				    SELECT
				    		  DT_GURIAGEH.SEIKYUSHONO
				    		, SUM(KING) AS KING
							, SUM(TAX) AS TAX
					FROM DT_GURIAGEH,DT_GURIAGEM,WK_PRT000
				WHERE 	DT_GURIAGEH.JIGYOCD			= 	I_事業所CD
					--	明細と結合
					AND	DT_GURIAGEH.SEIKYUSHONO		=	DT_GURIAGEM.SEIKYUSHONO
					--
					AND	WK_PRT000.PROGID			=	'OMP602'
					--
					AND	WK_PRT000.LOGINID			=	I_担当者CD
					--
					AND	WK_PRT000.EIGCD				=	I_事業所CD
					--
					AND	WK_PRT000.DENPNO			=	DT_GURIAGEM.SEIKYUSHONO
					-- 	見出し.無効区分
					AND	DT_GURIAGEH.DELKBN			=	'0'
					-- 	明細.無効区分
					AND	DT_GURIAGEM.DELKBN			=	'0'
					--
					AND	WK_PRT000.GYONO			=	DT_GURIAGEM.GYONO
					GROUP BY  DT_GURIAGEH.SEIKYUSHONO

	) LOOP
--
		UPDATE WK_PRT000
			SET	KINGAKU			=	R_該当データ.KING
			,	ZEI				=	R_該当データ.TAX
		WHERE	LOGINID			=	I_担当者CD
		AND		PROGID			=	'OMP602'
		AND		EIGCD			=	I_事業所CD 
		AND		DENPNO			=	R_該当データ.SEIKYUSHONO ;
	END LOOP ;
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


				UPDATE	DT_GURIAGEH

				SET	PRINTKBN			=	'1'
				WHERE 
					 	SEIKYUSHONO			= 	R_該当データ.DENPNO
			;
--
			END LOOP;
		END IF ;

--

	END OMP602;
END POMP602;
/
SHOW ERRORS