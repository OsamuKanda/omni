CREATE OR REPLACE PACKAGE BODY POMP604 AS
	-------------------------------------------------------------------------------
	-- 【関数名】
	--			注文書表発行	okada
	-- 【内容】
	--
	--
	-- 【引数】
	--
	-- 【戻り値】
	--
	-------------------------------------------------------------------------------
	PROCEDURE	 OMP604
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
				, DT_HACCHUH.HACCHUNO							-- 見出し.伝票番号
				, DT_HACCHUM.GYONO								-- 明細.行番号
				, 0
				, 0
			FROM DT_HACCHUH,DT_HACCHUM

			WHERE 	DT_HACCHUH.HACCHUJIGYOCD	= 	I_事業所CD
				--	仕入明細と結合
				AND	DT_HACCHUH.HACCHUJIGYOCD	=	DT_HACCHUM.HACCHUJIGYOCD
				AND	DT_HACCHUH.HACCHUNO			=	DT_HACCHUM.HACCHUNO
				--	明細.印刷ﾌﾗｸﾞ
				AND DT_HACCHUH.PRINTKBN			=	'0'
				--	見出し.入力担当者
--				AND	DECODE(DT_HACCHUH.TANTCD,NULL,'000',DT_HACCHUH.TANTCD) >= I_担当者CD
--				AND DECODE(DT_HACCHUH.TANTCD,NULL,'000',DT_HACCHUH.TANTCD) <= I_担当者CD
				-- 	見出し.無効区分
				AND	DT_HACCHUH.DELKBN	=	'0'
				-- 	明細.無効区分
				AND	DT_HACCHUM.DELKBN	=	'0'
				--	月次フラグ
--				AND DT_SHIREH.GETFLG <> '1'
				--	仕入入力後は対象外
				AND	DT_HACCHUM.SIRSUR 	=	0
			ORDER BY DT_HACCHUH.HACCHUJIGYOCD,DT_HACCHUH.HACCHUNO,TO_NUMBER(DT_HACCHUM.GYONO) ;
	ELSE

		-- 再発行の場合は、入金日が入力日付で且つプリント済み分のみ
		INSERT INTO WK_PRT000
			SELECT
				I_担当者CD										-- ﾛｸﾞｲﾝﾕｰｻﾞID
				, I_更新プログラム								-- ﾌﾟﾛｸﾞﾗﾑID
				, I_事業所CD									-- 営業所ｺｰﾄﾞ
				, DT_HACCHUH.HACCHUNO							-- 見出し.伝票番号
				, DT_HACCHUM.GYONO								-- 明細.行番号
				, 0
				, 0
			FROM DT_HACCHUH,DT_HACCHUM

			WHERE 	DT_HACCHUH.HACCHUJIGYOCD	= 	I_事業所CD
				--	仕入明細と結合
				AND	DT_HACCHUH.HACCHUJIGYOCD	=	DT_HACCHUM.HACCHUJIGYOCD
				AND	DT_HACCHUH.HACCHUNO			=	DT_HACCHUM.HACCHUNO
				--	明細.印刷ﾌﾗｸﾞ
				AND DT_HACCHUH.PRINTKBN		=	'1'
				--	見出し.入力担当者
--				AND	DECODE(DT_HACCHUH.TANTCD,NULL,'000000',DT_HACCHUH.TANTCD) >= I_担当者CD
--				AND DECODE(DT_HACCHUH.TANTCD,NULL,'000000',DT_HACCHUH.TANTCD) <= I_担当者CD
				-- 伝票番号
				AND	DT_HACCHUH.HACCHUNO			>=	I_伝票FR
				AND	DT_HACCHUH.HACCHUNO			<=	I_伝票TO
				-- 	見出し.無効区分
				AND	DT_HACCHUH.DELKBN	=	'0'
				-- 	明細.無効区分
				AND	DT_HACCHUM.DELKBN	=	'0'
				--	月次フラグ
--				AND DT_SHIREH.GETFLG <> '1'
				--	仕入入力後は対象外
				AND	DT_HACCHUM.SIRSUR 	=	0
			ORDER BY DT_HACCHUH.HACCHUJIGYOCD,DT_HACCHUH.HACCHUNO,TO_NUMBER(DT_HACCHUM.GYONO) ;
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


				UPDATE	DT_HACCHUH

				SET	PRINTKBN			=	'1'
				WHERE 
				 		HACCHUJIGYOCD		= 	R_該当データ.EIGCD
				AND 	HACCHUNO			= 	R_該当データ.DENPNO
			;
--
			END LOOP;
		END IF ;

--

	END OMP604;
END POMP604;
/
SHOW ERRORS