CREATE OR REPLACE PACKAGE BODY POMP606 AS
	-------------------------------------------------------------------------------
	-- 【関数名】
	--			支払確認表発行　
	-- 【内容】
	--
	--
	-- 【引数】
	--
	-- 【戻り値】
	--
	-------------------------------------------------------------------------------
	PROCEDURE	 OMP606
	(
			I_事業所CD					IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_更新プログラム			IN		DT_BUKKEN.UDTPG1%TYPE
		,	I_担当者CD					IN		DT_BUKKEN.TANTCD%TYPE
		,	I_伝票FR					IN		DT_SHIREH.SIRNO%TYPE
		,	I_伝票TO					IN		DT_SHIREH.SIRNO%TYPE
		,	I_区分						IN		DT_SHIREM.SAGYOBKBN%TYPE
	)
	
	IS

--ワーク項目囲定義

----
	BEGIN

-- 'ﾜｰｸﾌｧｲﾙ削除
	DELETE FROM WK_PRT000 WHERE LOGINID = I_担当者CD AND PROGID = I_更新プログラム ;

-- 伝票発行ワーク出力

	IF	I_区分 =  '0'		THEN

		INSERT INTO WK_PRT000
			SELECT
				I_担当者CD										-- ﾛｸﾞｲﾝﾕｰｻﾞID
				, I_更新プログラム								-- ﾌﾟﾛｸﾞﾗﾑID
				, I_事業所CD									-- 営業所ｺｰﾄﾞ
				, DT_SHRH.SHRNO									-- 見出し.伝票番号
				, DT_SHRB.GYONO									-- 明細.行番号
				, 0
				, 0
			FROM DT_SHRH,DT_SHRB

			WHERE 	DT_SHRH.JIGYOCD			= 	I_事業所CD
				--	明細と結合
				AND	DT_SHRH.JIGYOCD			=	DT_SHRB.JIGYOCD
				AND	DT_SHRH.SHRNO			=	DT_SHRB.SHRNO
				--	明細.印刷ﾌﾗｸﾞ
				AND DT_SHRH.PRINTKBN		=	'0'
				--	見出し.入力担当者
--				AND	DECODE(DT_SHIREH.INPUTCD,NULL,'000',DT_SHIREH.INPUTCD) >= I_担当者CD
--				AND DECODE(DT_SHIREH.INPUTCD,NULL,'000',DT_SHIREH.INPUTCD) <= I_担当者CD
				-- 	見出し.無効区分
				AND	DT_SHRH.DELKBN	=	'0'
				-- 	明細.無効区分
				AND	DT_SHRB.DELKBN	=	'0'
				--	月次フラグ
				AND DT_SHRH.GETFLG <> '1'

			ORDER BY DT_SHRH.JIGYOCD,DT_SHRH.SHRNO,TO_NUMBER(DT_SHRB.GYONO) ;
	ELSE

		-- 再発行の場合は、入金日が入力日付で且つプリント済み分のみ
		INSERT INTO WK_PRT000
			SELECT
				I_担当者CD										-- ﾛｸﾞｲﾝﾕｰｻﾞID
				, I_更新プログラム								-- ﾌﾟﾛｸﾞﾗﾑID
				, I_事業所CD									-- 営業所ｺｰﾄﾞ
				, DT_SHRH.SHRNO									-- 見出し.伝票番号
				, DT_SHRB.GYONO									-- 明細.行番号
				, 0
				, 0
			FROM DT_SHRH,DT_SHRB

			WHERE 	DT_SHRH.JIGYOCD			= 	I_事業所CD
				--	明細と結合
				AND	DT_SHRH.JIGYOCD			=	DT_SHRB.JIGYOCD
				AND	DT_SHRH.SHRNO			=	DT_SHRB.SHRNO
				--	明細.印刷ﾌﾗｸﾞ
				AND DT_SHRH.PRINTKBN		=	'1'
				--	見出し.入力担当者
--				AND	DECODE(DT_SHRH.INPUTCD,NULL,'000000',DT_SHRH.INPUTCD) >= I_担当者CD
--				AND DECODE(DT_SHRH.INPUTCD,NULL,'000000',DT_SHRH.INPUTCD) <= I_担当者CD
				-- 伝票番号
				AND	DT_SHRH.SHRNO			>=	I_伝票FR
				AND	DT_SHRH.SHRNO			<=	I_伝票TO
				-- 	見出し.無効区分
				AND	DT_SHRH.DELKBN		=	'0'
				-- 	明細.無効区分
				AND	DT_SHRB.DELKBN		=	'0'
				--	月次フラグ
				AND DT_SHRH.GETFLG <> '1'

			ORDER BY DT_SHRH.JIGYOCD,DT_SHRH.SHRNO,TO_NUMBER(DT_SHRB.GYONO) ;
	END IF ;

--dbms_output.put_line('ｸﾞﾙｰﾌﾟ番号の付け込み');
--
--該当データの印刷ﾌﾗｸﾞ更新

		IF	I_区分 =  '0' THEN

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


				UPDATE	DT_SHRH

				SET	PRINTKBN			=	'1'
				WHERE 
				 		JIGYOCD			= 	R_該当データ.EIGCD
				AND 	SHRNO			= 	R_該当データ.DENPNO
			;
--
			END LOOP;
		END IF ;

--

	END OMP606;
END POMP606;
/
SHOW ERRORS