CREATE OR REPLACE PACKAGE BODY POMP702 AS
	-------------------------------------------------------------------------------
	-- 【関数名】
	--			期日払管理表発行	okada
	-- 【内容】
	--
	--
	-- 【引数】
	--
	-- 【戻り値】
	--
	-------------------------------------------------------------------------------
	PROCEDURE	 OMP702
	(
			I_事業所CD					IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_更新プログラム			IN		DT_BUKKEN.UDTPG1%TYPE
		,	I_担当者CD					IN		DT_BUKKEN.TANTCD%TYPE
		,	I_指定年月					IN		DM_JIGYO.HOSHUYMD%TYPE
		,	I_出力指定					IN		DT_SHIREM.SAGYOBKBN%TYPE
		,	I_事業所指定				IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_出力帳票					IN		DT_SHIREM.SAGYOBKBN%TYPE
	)
	
	IS

--ワーク項目囲定義

	v_事業所FR			CHAR(2)	DEFAULT NULL ;
	v_事業所TO			CHAR(2)	DEFAULT NULL ;
	v_判定年月			CHAR(6) DEFAULT NULL ;
	v_入金合計			NUMBER(8)	DEFAULT 0 ;
	v_入金結果			NUMBER(8)	DEFAULT 0 ;
	
	v_前月入金額		NUMBER(9)	DEFAULT 0 ;
	v_現金				NUMBER(9)	DEFAULT 0 ;
	v_値引				NUMBER(9)	DEFAULT 0 ;
	v_手形				NUMBER(9)	DEFAULT 0 ;
	v_手形郵送代		NUMBER(9)	DEFAULT 0 ;
	v_売掛債権			NUMBER(9)	DEFAULT 0 ;
	v_相殺				NUMBER(9)	DEFAULT 0 ;
	v_振込手数料		NUMBER(9)	DEFAULT 0 ;
	v_諸会費			NUMBER(9)	DEFAULT 0 ;
	v_金利				NUMBER(9)	DEFAULT 0 ;
	v_前受分			NUMBER(9)	DEFAULT 0 ;
	v_当月分			NUMBER(9)	DEFAULT 0 ;

----
	BEGIN

	-- I_事業所指定が未セットの場合は、全事業所選択とする。
	IF I_事業所指定 IS NULL THEN
		v_事業所FR := '01' ;
		v_事業所TO := '99' ;
	ELSE
		v_事業所FR := I_事業所指定 ;
		v_事業所TO := I_事業所指定 ;
	END IF ;
	
-- 'ﾜｰｸﾌｧｲﾙ削除
	DELETE FROM WK_PRT701 WHERE LOGINID = I_担当者CD AND PROGID = I_更新プログラム ;

-- 該当売上ヘッダーより対象データ抽出
		FOR R_該当データ IN (
				    SELECT
						  DT_URIAGEH.SEIKYUSHONO					--請求書番号
						, DT_URIAGEH.JIGYOCD						--事業所コード
						, DT_URIAGEH.SAGYOBKBN						--作業分類区分
						, DT_URIAGEH.RENNO							--連番
						, DT_URIAGEH.NYUKINYOTEIYMD					--入金予定日（期日払でセットされる。）
						, DT_URIAGEH.SEIKYUYMD						--請求日付
						, SUM(DT_URIAGEM.KING + DT_URIAGEM.TAX) AS 合計
						, SUM(DT_URIAGEM.KING) AS 金額
						, SUM(DT_URIAGEM.TAX) AS 消費税
						, DT_BUKKEN.KANRYOYMD						--完了日
						, DT_BUKKEN.MAEUKEKBN						--前受区分
                        , CASE WHEN
							DT_URIAGEH.NYUKINYMD <= I_指定年月 || '31' AND DT_URIAGEH.NYUKINR <> 0 THEN 日付記号追加(DT_URIAGEH.NYUKINYMD) 
							ELSE
								NULL
							END AS 入金日
				    FROM    DT_URIAGEH,DT_URIAGEM,DT_BUKKEN
				    -- 事業所コードが範囲指定内
				    WHERE   	DT_URIAGEH.JIGYOCD 		>=  v_事業所FR
				    AND			DT_URIAGEH.JIGYOCD 		<=  v_事業所TO
				    -- 伝票区分が'0'（通常）
				    AND     	DENPYOKBN      			=   '0'
				    -- 出力区分が未セット（入金が終了していないもの）
				    AND			OUTKBN				IS NULL
				    -- 無効区分
					AND			DT_URIAGEH.DELKBN		=	'0'
					-- 売上明細と結合
					AND			DT_URIAGEH.SEIKYUSHONO	=	DT_URIAGEM.SEIKYUSHONO
					AND			DT_URIAGEM.DELKBN		=	'0'
					-- 物件ファイルと結合
					AND			DT_URIAGEH.JIGYOCD		=	DT_BUKKEN.JIGYOCD
					AND			DT_URIAGEH.SAGYOBKBN 	=	DT_BUKKEN.SAGYOBKBN
					AND			DT_URIAGEH.RENNO	 	=	DT_BUKKEN.RENNO
					AND			DT_BUKKEN.DELKBN	 	=	'0'
					-- 売掛金管理表と異なるのは、この部分のみ。--------
					AND			DT_URIAGEH.NYUKINYOTEIYMD IS NOT NULL
					---------------------------------------------------
					GROUP BY
						  DT_URIAGEH.SEIKYUSHONO
						, DT_URIAGEH.JIGYOCD
						, DT_URIAGEH.SAGYOBKBN
						, DT_URIAGEH.RENNO
						, DT_URIAGEH.NYUKINYOTEIYMD
						, DT_URIAGEH.SEIKYUYMD
						, DT_BUKKEN.KANRYOYMD
						, DT_BUKKEN.MAEUKEKBN
						, DT_URIAGEH.NYUKINR
						, DT_URIAGEH.NYUKINYMD
					ORDER BY DT_URIAGEH.SEIKYUSHONO
		)LOOP	
--
		-- 出力帳票が売掛金管理表の場合は、入金予定日が未セットのみ対象。
--		IF	I_出力帳票 = '1' AND R_該当データ.NYUKINYOTEIYMD IS NULL THEN
			-- 出力指定が前月繰越分指定または、全ての場合------------------------------------------------------------------------------------------
			-- 物件ファイル.完了日 <> 0 で且つ物件ファイル.前受区分 = 1 の場合は、完了日をそれ以外は請求日が < 指定年月かを判定する。
			-- 該当データの場合、該当請求書番号で入金データを取得し入金日 <= 指定年月分の入金額をサマリし売上金額＋消費税－入金額のサマリ <> 0の場合
			-- 売掛金管理表対象データとする。
			----------------------------------------------------------------------------------------------------------------------------------------
			 -- 完了日 <> '0' で且つ 前受区分='1'の場合は、完了日をそれ以外は請求日を使用して判定する。
			IF	R_該当データ.KANRYOYMD <> '00000000' AND R_該当データ.MAEUKEKBN = '1' THEN
				v_判定年月 := SUBSTR(R_該当データ.KANRYOYMD,1,6) ;
			ELSE
				v_判定年月 := SUBSTR(R_該当データ.SEIKYUYMD,1,6) ;
			END IF ;

			v_入金合計 := 0 ;

			v_前月入金額 := 0 ;
			v_現金 := 0 ;
			v_値引 := 0 ;
			v_手形 := 0 ;
			v_手形郵送代 := 0 ;
			v_売掛債権 := 0 ;
			v_相殺 := 0 ;
			v_振込手数料 := 0 ;
			v_諸会費 := 0 ;
			v_金利 := 0 ;
			v_前受分 := 0 ;
			v_当月分 := 0 ;

			IF I_出力指定 = '0' OR I_出力指定 = '1' THEN

				-- 判定年月が指定した年月以前の場合、該当請求書番号より入金データを取得し残高があるか判定する。
				IF v_判定年月 < I_指定年月 AND R_該当データ.KANRYOYMD <> '00000000' THEN
					-- 入金データが存在するかのチェック
					DECLARE
					   CURSOR c1 IS SELECT SUM(KING) AS NYUKN FROM 	DT_NYUKINM WHERE	DT_NYUKINM.SEIKYUSHONO		=	R_該当データ.SEIKYUSHONO
																		AND		SUBSTR(DT_NYUKINM.NYUKINYMD,1,6)	<=	I_指定年月
																		AND		DT_NYUKINM.DELKBN					=	'0'
																		GROUP BY SEIKYUSHONO;
					BEGIN
					  OPEN c1;
					    FETCH c1 INTO v_入金結果;
					    IF c1%NOTFOUND THEN  -- fetch failed
					    	v_入金合計 := 0 ;
					    ELSE
					    	v_入金合計 := v_入金結果 ;
					    END IF;
					END ;
--
					DECLARE
					   CURSOR c2 IS SELECT SUM(KING) AS NYUKN FROM 	DT_NYUKINM WHERE	DT_NYUKINM.SEIKYUSHONO		=	R_該当データ.SEIKYUSHONO
																		AND		SUBSTR(DT_NYUKINM.NYUKINYMD,1,6)	=	I_指定年月
																		AND		DT_NYUKINM.DELKBN					=	'0'
																		GROUP BY SEIKYUSHONO;
					BEGIN
					  OPEN c2;
					    FETCH c2 INTO v_入金結果;
					    IF c2%NOTFOUND THEN  -- fetch failed
							v_現金 := 0 ;
							v_値引 := 0 ;
							v_手形 := 0 ;
							v_手形郵送代 := 0 ;
							v_売掛債権 := 0 ;
							v_相殺 := 0 ;
							v_振込手数料 := 0 ;
							v_諸会費 := 0 ;
							v_金利 := 0 ;
							v_前受分 := 0 ;
							v_当月分 := 0 ;
					    ELSE
					    	SELECT 
					    			  SUM(WK.GENKIN)
					    			, SUM(WK.TEGATA)
					    			, SUM(WK.NEBIKI)
					    			, SUM(WK.SOUSAI)
					    			, SUM(WK.URIKAKE)
					    			, SUM(WK.FURIKOMI)
					    			, SUM(WK.KAIHI)
					    			, SUM(WK.YUSODAI)
					    			, SUM(WK.KINRI)
					    			, SUM(WK.MAEUKE)
					    			, SUM(WK.TOUGETU)
					    	INTO
									  v_現金
									, v_手形
									, v_値引
									, v_相殺
									, v_売掛債権
									, v_振込手数料
									, v_諸会費
									, v_手形郵送代
									, v_金利
									, v_前受分
									, v_当月分
					    	FROM
					    	(
								    	SELECT 
								    			  SEIKYUSHONO AS NO
								    			, DECODE(NYUKINKBN,'01',SUM(KING),0)	AS 	GENKIN			--現金
								    			, DECODE(NYUKINKBN,'02',SUM(KING),0)	AS	TEGATA			--手形
								    			, DECODE(NYUKINKBN,'03',SUM(KING),0)	AS	NEBIKI			--値引
								    			, DECODE(NYUKINKBN,'04',SUM(KING),'11',SUM(KING),0)	AS	SOUSAI			--相殺
								    			, DECODE(NYUKINKBN,'05',SUM(KING),0)	AS	URIKAKE			--売掛債権
								    			, DECODE(NYUKINKBN,'06',SUM(KING),0)	AS	FURIKOMI		--振込手数料
								    			, DECODE(NYUKINKBN,'07',SUM(KING),0)	AS	KAIHI			--諸会費
								    			, DECODE(NYUKINKBN,'08',SUM(KING),0)	AS	YUSODAI			--手形郵送代
								    			, DECODE(NYUKINKBN,'09',SUM(KING),0)	AS	KINRI			--金利・割引
								    			, DECODE(NYUKINKBN,'10',SUM(KING),0)	AS	MAEUKE			--前受分
								    			, SUM(KING)		AS	TOUGETU									--当月分
								    	FROM	DT_NYUKINM
								    	WHERE	DT_NYUKINM.SEIKYUSHONO 				= 	R_該当データ.SEIKYUSHONO
										AND		SUBSTR(DT_NYUKINM.NYUKINYMD,1,6) 	= 	I_指定年月
										AND		DT_NYUKINM.DELKBN					=	'0'
										GROUP BY SEIKYUSHONO,NYUKINKBN ) WK
							GROUP BY WK.NO ;
						END IF;
					END ;
--
					    v_前月入金額 := v_入金合計 - v_当月分;
--
--						IF ( R_該当データ.合計 - v_入金合計 ) <> 0 THEN
						IF  ( R_該当データ.合計 - v_入金合計 ) <> 0 OR v_当月分 <> 0 THEN
							--ワークへ前月分として出力
								INSERT INTO WK_PRT701 (
										 LOGINID
										,PROGID
										,SEIKYUSHONO
										,JIGYOCD
										,SAGYOBKBN
										,RENNO
										,OUTKBN
										,KING
										,TAX
										,ZENNYUKIN
										,GENKIN
										,NEBIKI
										,TEGATA
										,YUSODAI
										,URIKAKESAIKEN
										,SOUSAI
										,TESURYO
										,KAIHI
										,KINRI
										,MAEUKE
										,NYUKINYMD)
								VALUES (
									  I_担当者CD
									, I_更新プログラム
									, R_該当データ.SEIKYUSHONO
									, R_該当データ.JIGYOCD
									, R_該当データ.SAGYOBKBN
									, R_該当データ.RENNO
									, '1' 
									, R_該当データ.金額
									, R_該当データ.消費税
									, v_前月入金額
									, v_現金
									, v_値引
									, v_手形
									, v_手形郵送代
									, v_売掛債権
									, v_相殺
									, v_振込手数料
									, v_諸会費
									, v_金利
									, v_前受分
									, SUBSTR(R_該当データ.入金日,6,5)) ;
						END IF ;
				END IF ;
			END IF;
			-----------------------------------------------------------------------------------------------------------------------------------
			-- 出力指定が当月分指定または、全ての場合------------------------------------------------------------------------------------------
			-- 物件ファイル.完了日 <> 0 で且つ物件ファイル.前受区分 = 1 の場合は、完了日をそれ以外は請求日が = 指定年月かを判定する。
			-- 売掛金管理表対象データとする。
			-----------------------------------------------------------------------------------------------------------------------------------
			IF I_出力指定 = '0' OR I_出力指定 = '2' THEN
				 -- 完了日 <> '0' で且つ 前受区分='1'の場合は、完了日をそれ以外は請求日を使用して判定する。
--				IF	R_該当データ.KANRYOYMD <> '00000000' AND R_該当データ.MAEUKEKBN = '1' THEN
--					v_判定年月 := SUBSTR(R_該当データ.KANRYOYMD,1,6) ;
--				ELSE
--					v_判定年月 := SUBSTR(R_該当データ.SEIKYUYMD,1,6) ;
--				END IF ;
				-- 判定年月=指定した年月の場合
				IF v_判定年月 = I_指定年月 AND R_該当データ.KANRYOYMD <> '00000000' THEN
--
					-- 入金データが存在するかのチェック
					DECLARE
					   CURSOR c1 IS SELECT SUM(KING) AS NYUKN FROM 	DT_NYUKINM WHERE	DT_NYUKINM.SEIKYUSHONO		=	R_該当データ.SEIKYUSHONO
																		AND		SUBSTR(DT_NYUKINM.NYUKINYMD,1,6)	<=	I_指定年月
																		AND		DT_NYUKINM.DELKBN					=	'0'
																		GROUP BY SEIKYUSHONO;
					BEGIN
					  OPEN c1;
					    FETCH c1 INTO v_入金結果;
					    IF c1%NOTFOUND THEN  -- fetch failed
					    	v_入金合計 := 0 ;
					    ELSE
					    	v_入金合計 := v_入金結果 ;
					    END IF;
					END ;
--
					DECLARE
					   CURSOR c2 IS SELECT SUM(KING) AS NYUKN FROM 	DT_NYUKINM WHERE	DT_NYUKINM.SEIKYUSHONO		=	R_該当データ.SEIKYUSHONO
																		AND		SUBSTR(DT_NYUKINM.NYUKINYMD,1,6)	=	I_指定年月
																		AND		DT_NYUKINM.DELKBN					=	'0'
																		GROUP BY SEIKYUSHONO;
					BEGIN
					  OPEN c2;
					    FETCH c2 INTO v_入金結果;
					    IF c2%NOTFOUND THEN  -- fetch failed
							v_現金 := 0 ;
							v_値引 := 0 ;
							v_手形 := 0 ;
							v_手形郵送代 := 0 ;
							v_売掛債権 := 0 ;
							v_相殺 := 0 ;
							v_振込手数料 := 0 ;
							v_諸会費 := 0 ;
							v_金利 := 0 ;
							v_前受分 := 0 ;
							v_当月分 := 0 ;
					    ELSE
					    	SELECT 
					    			  SUM(WK.GENKIN)
					    			, SUM(WK.TEGATA)
					    			, SUM(WK.NEBIKI)
					    			, SUM(WK.SOUSAI)
					    			, SUM(WK.URIKAKE)
					    			, SUM(WK.FURIKOMI)
					    			, SUM(WK.KAIHI)
					    			, SUM(WK.YUSODAI)
					    			, SUM(WK.KINRI)
					    			, SUM(WK.MAEUKE)
					    			, SUM(WK.TOUGETU)
					    	INTO
									  v_現金
									, v_手形
									, v_値引
									, v_相殺
									, v_売掛債権
									, v_振込手数料
									, v_諸会費
									, v_手形郵送代
									, v_金利
									, v_前受分
									, v_当月分
					    	FROM
					    	(
								    	SELECT 
								    			  SEIKYUSHONO AS NO
								    			, DECODE(NYUKINKBN,'01',SUM(KING),0)	AS 	GENKIN			--現金
								    			, DECODE(NYUKINKBN,'02',SUM(KING),0)	AS	TEGATA			--手形
								    			, DECODE(NYUKINKBN,'03',SUM(KING),0)	AS	NEBIKI			--値引
								    			, DECODE(NYUKINKBN,'04',SUM(KING),'11',SUM(KING),0)	AS	SOUSAI			--相殺
								    			, DECODE(NYUKINKBN,'05',SUM(KING),0)	AS	URIKAKE			--売掛債権
								    			, DECODE(NYUKINKBN,'06',SUM(KING),0)	AS	FURIKOMI		--振込手数料
								    			, DECODE(NYUKINKBN,'07',SUM(KING),0)	AS	KAIHI			--諸会費
								    			, DECODE(NYUKINKBN,'08',SUM(KING),0)	AS	YUSODAI			--手形郵送代
								    			, DECODE(NYUKINKBN,'09',SUM(KING),0)	AS	KINRI			--金利・割引
								    			, DECODE(NYUKINKBN,'10',SUM(KING),0)	AS	MAEUKE			--前受分
								    			, SUM(KING)		AS	TOUGETU									--当月分
								    	FROM	DT_NYUKINM
								    	WHERE	DT_NYUKINM.SEIKYUSHONO 				= 	R_該当データ.SEIKYUSHONO
										AND		SUBSTR(DT_NYUKINM.NYUKINYMD,1,6) 	= 	I_指定年月
										AND		DT_NYUKINM.DELKBN					=	'0'
										GROUP BY SEIKYUSHONO,NYUKINKBN ) WK
							GROUP BY WK.NO ;
						END IF;
					END ;
--
					    v_前月入金額 := v_入金合計 - v_当月分;
--
					--ワークへ当月分として出力
					INSERT INTO WK_PRT701 (
							 LOGINID
							,PROGID
							,SEIKYUSHONO
							,JIGYOCD
							,SAGYOBKBN
							,RENNO
							,OUTKBN
							,KING
							,TAX
							,ZENNYUKIN
							,GENKIN
							,NEBIKI
							,TEGATA
							,YUSODAI
							,URIKAKESAIKEN
							,SOUSAI
							,TESURYO
							,KAIHI
							,KINRI
							,MAEUKE
							,NYUKINYMD)
					VALUES (
						  I_担当者CD
						, I_更新プログラム
						, R_該当データ.SEIKYUSHONO
						, R_該当データ.JIGYOCD
						, R_該当データ.SAGYOBKBN
						, R_該当データ.RENNO
						, '2' 
						, R_該当データ.金額
						, R_該当データ.消費税
						, v_前月入金額
						, v_現金
						, v_値引
						, v_手形
						, v_手形郵送代
						, v_売掛債権
						, v_相殺
						, v_振込手数料
						, v_諸会費
						, v_金利
						, v_前受分
						, SUBSTR(R_該当データ.入金日,6,5)) ;
				END IF ;
			END IF;
			-----------------------------------------------------------------------------------------------------------------------------------
			-- 出力指定が前受分指定または、全ての場合------------------------------------------------------------------------------------------
			-- 物件ファイル.完了日 <> 0 で且つ物件ファイル.前受区分 = 1 の場合は、完了日をそれ以外は請求日が > 指定年月かを判定する。
			-- 売掛金管理表対象データとする。
			-----------------------------------------------------------------------------------------------------------------------------------
			IF I_出力指定 = '0' OR I_出力指定 = '3'  THEN
				 -- 完了日 <> '0' で且つ 前受区分='1'の場合は、完了日をそれ以外は請求日を使用して判定する。
--				IF	R_該当データ.KANRYOYMD <> '00000000' AND R_該当データ.MAEUKEKBN = '1' THEN
--					v_判定年月 := SUBSTR(R_該当データ.KANRYOYMD,1,6) ;
--				ELSIF R_該当データ.KANRYOYMD <> '00000000' AND R_該当データ.MAEUKEKBN = '0' OR R_該当データ.KANRYOYMD = '00000000' THEN
--					v_判定年月 := SUBSTR(R_該当データ.SEIKYUYMD,1,6) ;
--				END IF ;
				IF R_該当データ.KANRYOYMD = '00000000' THEN
					v_判定年月 := SUBSTR(R_該当データ.SEIKYUYMD,1,6) ;
				END IF ;
				-- 判定年月 > 指定した年月の場合
--				IF v_判定年月 > I_指定年月 OR R_該当データ.KANRYOYMD = '00000000' THEN --20120314
				IF  R_該当データ.KANRYOYMD = '00000000' AND  v_判定年月 <= I_指定年月  THEN
--
					-- 入金データが存在するかのチェック
					DECLARE
					   CURSOR c1 IS SELECT SUM(KING) AS NYUKN FROM 	DT_NYUKINM WHERE	DT_NYUKINM.SEIKYUSHONO		=	R_該当データ.SEIKYUSHONO
																		AND		SUBSTR(DT_NYUKINM.NYUKINYMD,1,6)	<=	I_指定年月
																		AND		DT_NYUKINM.DELKBN					=	'0'
																		GROUP BY SEIKYUSHONO;
					BEGIN
					  OPEN c1;
					    FETCH c1 INTO v_入金結果;
					    IF c1%NOTFOUND THEN  -- fetch failed
					    	v_入金合計 := 0 ;
					    ELSE
					    	v_入金合計 := v_入金結果 ;
					    END IF;
					END ;
--
					DECLARE
					   CURSOR c2 IS SELECT SUM(KING) AS NYUKN FROM 	DT_NYUKINM WHERE	DT_NYUKINM.SEIKYUSHONO		=	R_該当データ.SEIKYUSHONO
																		AND		SUBSTR(DT_NYUKINM.NYUKINYMD,1,6)	=	I_指定年月
																		AND		DT_NYUKINM.DELKBN					=	'0'
																		GROUP BY SEIKYUSHONO;
					BEGIN
					  OPEN c2;
					    FETCH c2 INTO v_入金結果;
					    IF c2%NOTFOUND THEN  -- fetch failed
							v_現金 := 0 ;
							v_値引 := 0 ;
							v_手形 := 0 ;
							v_手形郵送代 := 0 ;
							v_売掛債権 := 0 ;
							v_相殺 := 0 ;
							v_振込手数料 := 0 ;
							v_諸会費 := 0 ;
							v_金利 := 0 ;
							v_前受分 := 0 ;
							v_当月分 := 0 ;
					    ELSE
					    	SELECT 
					    			  SUM(WK.GENKIN)
					    			, SUM(WK.TEGATA)
					    			, SUM(WK.NEBIKI)
					    			, SUM(WK.SOUSAI)
					    			, SUM(WK.URIKAKE)
					    			, SUM(WK.FURIKOMI)
					    			, SUM(WK.KAIHI)
					    			, SUM(WK.YUSODAI)
					    			, SUM(WK.KINRI)
					    			, SUM(WK.MAEUKE)
					    			, SUM(WK.TOUGETU)
					    	INTO
									  v_現金
									, v_手形
									, v_値引
									, v_相殺
									, v_売掛債権
									, v_振込手数料
									, v_諸会費
									, v_手形郵送代
									, v_金利
									, v_前受分
									, v_当月分
					    	FROM
					    	(
								    	SELECT 
								    			  SEIKYUSHONO AS NO
								    			, DECODE(NYUKINKBN,'01',SUM(KING),0)	AS 	GENKIN			--現金
								    			, DECODE(NYUKINKBN,'02',SUM(KING),0)	AS	TEGATA			--手形
								    			, DECODE(NYUKINKBN,'03',SUM(KING),0)	AS	NEBIKI			--値引
								    			, DECODE(NYUKINKBN,'04',SUM(KING),'11',SUM(KING),0)	AS	SOUSAI			--相殺
								    			, DECODE(NYUKINKBN,'05',SUM(KING),0)	AS	URIKAKE			--売掛債権
								    			, DECODE(NYUKINKBN,'06',SUM(KING),0)	AS	FURIKOMI		--振込手数料
								    			, DECODE(NYUKINKBN,'07',SUM(KING),0)	AS	KAIHI			--諸会費
								    			, DECODE(NYUKINKBN,'08',SUM(KING),0)	AS	YUSODAI			--手形郵送代
								    			, DECODE(NYUKINKBN,'09',SUM(KING),0)	AS	KINRI			--金利・割引
								    			, DECODE(NYUKINKBN,'10',SUM(KING),0)	AS	MAEUKE			--前受分
								    			, SUM(KING)		AS	TOUGETU									--当月分
								    	FROM	DT_NYUKINM
								    	WHERE	DT_NYUKINM.SEIKYUSHONO 				= 	R_該当データ.SEIKYUSHONO
										AND		SUBSTR(DT_NYUKINM.NYUKINYMD,1,6) 	= 	I_指定年月
										AND		DT_NYUKINM.DELKBN					=	'0'
										GROUP BY SEIKYUSHONO,NYUKINKBN ) WK
							GROUP BY WK.NO ;
						END IF;
					END ;
--
					    v_前月入金額 := v_入金合計 - v_当月分;
--
					--ワークへ前受分として出力
					INSERT INTO WK_PRT701 (
							 LOGINID
							,PROGID
							,SEIKYUSHONO
							,JIGYOCD
							,SAGYOBKBN
							,RENNO
							,OUTKBN
							,KING
							,TAX
							,ZENNYUKIN
							,GENKIN
							,NEBIKI
							,TEGATA
							,YUSODAI
							,URIKAKESAIKEN
							,SOUSAI
							,TESURYO
							,KAIHI
							,KINRI
							,MAEUKE
							,NYUKINYMD)
					VALUES (
						  I_担当者CD
						, I_更新プログラム
						, R_該当データ.SEIKYUSHONO
						, R_該当データ.JIGYOCD
						, R_該当データ.SAGYOBKBN
						, R_該当データ.RENNO
						, '3' 
						, R_該当データ.金額
						, R_該当データ.消費税
						, v_前月入金額
						, v_現金
						, v_値引
						, v_手形
						, v_手形郵送代
						, v_売掛債権
						, v_相殺
						, v_振込手数料
						, v_諸会費
						, v_金利
						, v_前受分
						, SUBSTR(R_該当データ.入金日,6,5)) ;
				END IF ;
			END IF ;
--		END IF ;

		END LOOP;


	END OMP702;
END POMP702;
/
SHOW ERRORS