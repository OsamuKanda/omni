CREATE OR REPLACE PACKAGE BODY POMP705 AS
	-------------------------------------------------------------------------------
	-- 【関数名】
	--			物件別原価表発行
	-- 【内容】 
	--
	--
	-- 【引数】
	--				実行事業所コード、実行プログラム、ログインID
	--				事業所コード、作業分類、指定１、指定２
	--
	-- 【戻り値】
	--
	-------------------------------------------------------------------------------
	PROCEDURE	 OMP705
	(
			I_実行事業所CD				IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_更新プログラム			IN		DT_BUKKEN.UDTPG1%TYPE
		,	I_担当者CD					IN		DT_BUKKEN.TANTCD%TYPE
		,	I_事業所CD					IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_作業分類区分				IN		DT_BUKKEN.SAGYOBKBN%TYPE
		,	I_指定1						IN		DT_BUKKEN.SAGYOBKBN%TYPE
		,	I_指定2						IN		DT_BUKKEN.SAGYOBKBN%TYPE
	)
	
	IS

--ワーク項目囲定義
	v_事業所FR				CHAR(2)	DEFAULT NULL ;
	v_事業所TO				CHAR(2)	DEFAULT NULL ;
	V_開始作業分類			CHAR(1)	DEFAULT NULL ;
	V_終了作業分類			CHAR(1)	DEFAULT NULL ;
	v_外注区分				CHAR(1) DEFAULT NULL ;
	v_カウント				NUMBER(1)	DEFAULT 0 ;
	v_前月以前金額			NUMBER(9)	DEFAULT 0 ;
	v_前月金額				NUMBER(9)	DEFAULT 0 ;
	v_当月金額				NUMBER(9)	DEFAULT 0 ;

----
	BEGIN

	-- I_事業所指定が未セットの場合は、全事業所選択とする。
	IF I_事業所CD IS NULL THEN
		v_事業所FR := '01' ;
		v_事業所TO := '88' ;
	ELSE
		v_事業所FR := I_事業所CD ;
		v_事業所TO := I_事業所CD ;
	END IF ;
	-- I_作業分類区分が未セットの場合は、全作業選択とする。
	IF I_作業分類区分 IS NULL THEN
		v_開始作業分類 := '1' ;
		v_終了作業分類 := '5' ;
	ELSE
		v_開始作業分類 := I_作業分類区分 ;
		v_終了作業分類 := I_作業分類区分 ;
	END IF ;
-- 'ﾜｰｸﾌｧｲﾙ削除
	DELETE FROM WK_PRT705 WHERE LOGINID = I_担当者CD AND PROGID = I_更新プログラム ;

-- 物件データ抽出
	FOR R_物件 IN (
			SELECT 
				  WK.JIGYOCD
				, WK.SAGYOBKBN
				, WK.RENNO
				, WK.SEIKYUYMD
				, WK.KANRYOYMD
				, WK.MAEUKEKBN
				, WK.指定月
				, WK.仕掛区分
				, WK.SOUKINGR
				FROM
				(
					SELECT
		    			  DT_BUKKEN.JIGYOCD										--事業所CD
		    			, DT_BUKKEN.SAGYOBKBN									--作業分類区分
		    			, DT_BUKKEN.RENNO										--連番
		    			, DT_BUKKEN.SEIKYUYMD									--請求日
		    			, DT_BUKKEN.KANRYOYMD									--完了日
		    			, DT_BUKKEN.MAEUKEKBN									--前受区分
		    			, SUBSTR(DM_KANRI.MONYMD,1,6) AS 指定月 
						, CASE 
								-- 完了日='00000000' は仕掛中
								WHEN DT_BUKKEN.KANRYOYMD = '00000000'  THEN '1'
								-- 売上で且つ請求日 NULL は仕掛中
								WHEN DT_BUKKEN.MAEUKEKBN = '0' AND SUBSTR(DT_BUKKEN.SEIKYUYMD,1,6) IS NULL THEN '1'
								-- 売上で且つ請求日 < 月次締日付の年月 は既売上
								WHEN DT_BUKKEN.MAEUKEKBN = '0' AND SUBSTR(DT_BUKKEN.SEIKYUYMD,1,6) < SUBSTR(DM_KANRI.MONYMD,1,6) THEN '3'
								-- 売上で且つ請求日 = 月次締日付の年月 は当月売上
								WHEN DT_BUKKEN.MAEUKEKBN = '0' AND SUBSTR(DT_BUKKEN.SEIKYUYMD,1,6) = SUBSTR(DM_KANRI.MONYMD,1,6) THEN '2'
								-- 売上で且つ請求日 > 月次締日付の年月 は仕掛中
								WHEN DT_BUKKEN.MAEUKEKBN = '0' AND SUBSTR(DT_BUKKEN.SEIKYUYMD,1,6) > SUBSTR(DM_KANRI.MONYMD,1,6)  THEN '1' 
								-- 前受で且つ完了日 < 月次締日付の年月 は既売上
								WHEN DT_BUKKEN.MAEUKEKBN = '1' AND SUBSTR(DECODE(DT_BUKKEN.KANRYOYMD,'00000000',DT_BUKKEN.SEIKYUYMD,DT_BUKKEN.KANRYOYMD),1,6)  < SUBSTR(DM_KANRI.MONYMD,1,6) THEN '3'
								-- 前受で且つ完了日 = 月次締日付の年月 は当月売上
								WHEN DT_BUKKEN.MAEUKEKBN = '1' AND SUBSTR(DECODE(DT_BUKKEN.KANRYOYMD,'00000000',DT_BUKKEN.SEIKYUYMD,DT_BUKKEN.KANRYOYMD),1,6)  = SUBSTR(DM_KANRI.MONYMD,1,6) THEN '2'
								-- 前受で且つ完了日 > 月次締日付の年月 は仕掛中
								WHEN DT_BUKKEN.MAEUKEKBN = '1' AND SUBSTR(DECODE(DT_BUKKEN.KANRYOYMD,'00000000',DT_BUKKEN.SEIKYUYMD,DT_BUKKEN.KANRYOYMD),1,6)  > SUBSTR(DM_KANRI.MONYMD,1,6)  THEN '1' 
						  END AS 仕掛区分
						, DT_BUKKEN.SOUKINGR 
					FROM DT_BUKKEN,DM_KANRI
					WHERE 
							-- 無効区分
							DT_BUKKEN.DELKBN = '0'
							-- 事業所コード
						AND DT_BUKKEN.JIGYOCD < 89
							-- 受付日
						AND DT_BUKKEN.UKETSUKEYMD >= 20010101
							-- 長期区分がｸﾚｰﾑ・ｻｰﾋﾞｽ以外
						AND ( DT_BUKKEN.CHOKIKBN is null OR DT_BUKKEN.CHOKIKBN = '1' )
							-- 管理マスタ
						AND DM_KANRI.KANRINO = '1'
							-- 作業分類区分
						AND	DT_BUKKEN.SAGYOBKBN >= v_開始作業分類
						AND	DT_BUKKEN.SAGYOBKBN <= v_終了作業分類
							-- 事業所コード
						AND DT_BUKKEN.JIGYOCD >= v_事業所FR
						AND DT_BUKKEN.JIGYOCD <= v_事業所TO
				) WK 
				WHERE	WK.仕掛区分 = I_指定1
       	)LOOP

		v_カウント := 0 ;
		
		FOR i IN 1..3 LOOP
			IF i <> 1 THEN 
				R_物件.SOUKINGR := 0 ; 
			END IF;
		-- 該当物件データを元に仕入明細より仕入金額を取得する。
				DECLARE
					CURSOR c1 IS
					SELECT	
							  WK.GAICHUKBN
							, SUM(WK.前月以前金額)
							, SUM(WK.前月金額)
							, SUM(WK.当月金額)
					FROM (
							SELECT 
								  DM_BKIKAKU.GAICHUKBN				--外注区分
								  -- 管理マスタ内の月次日付-1ヶ月以前のデータを外注区分毎にサマリ
								, CASE 
								    WHEN SUBSTR(DT_SHIREH.SIRYMD,1,6) < SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.MONYMD,-1),'YYYYMMDD'),1,6) AND DM_BKIKAKU.GAICHUKBN = '0' THEN SUM(SIRKIN) 
								    WHEN SUBSTR(DT_SHIREH.SIRYMD,1,6) < SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.MONYMD,-1),'YYYYMMDD'),1,6) AND DM_BKIKAKU.GAICHUKBN = '1' THEN SUM(SIRKIN) 
								    WHEN SUBSTR(DT_SHIREH.SIRYMD,1,6) < SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.MONYMD,-1),'YYYYMMDD'),1,6) AND DM_BKIKAKU.GAICHUKBN <> '0' AND DM_BKIKAKU.GAICHUKBN <> '1'  THEN SUM(SIRKIN) 
								  END AS 前月以前金額
								  -- 管理マスタ内の月次日付-1ヶ月のデータを外注区分毎にサマリ
								,CASE 
								    WHEN SUBSTR(DT_SHIREH.SIRYMD,1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.MONYMD,-1),'YYYYMMDD'),1,6) AND DM_BKIKAKU.GAICHUKBN = '0' THEN SUM(SIRKIN) 
								    WHEN SUBSTR(DT_SHIREH.SIRYMD,1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.MONYMD,-1),'YYYYMMDD'),1,6) AND DM_BKIKAKU.GAICHUKBN = '1' THEN SUM(SIRKIN) 
								    WHEN SUBSTR(DT_SHIREH.SIRYMD,1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.MONYMD,-1),'YYYYMMDD'),1,6) AND DM_BKIKAKU.GAICHUKBN <> '0' AND DM_BKIKAKU.GAICHUKBN <> '1'  THEN SUM(SIRKIN) 
								  END AS 前月金額
								  -- 管理マスタ内の月次日付のデータを外注区分毎にサマリ
								,CASE 
								    WHEN SUBSTR(DT_SHIREH.SIRYMD,1,6) = SUBSTR(DM_KANRI.MONYMD,1,6) AND DM_BKIKAKU.GAICHUKBN = '0' THEN SUM(SIRKIN) 
								    WHEN SUBSTR(DT_SHIREH.SIRYMD,1,6) = SUBSTR(DM_KANRI.MONYMD,1,6) AND DM_BKIKAKU.GAICHUKBN = '1' THEN SUM(SIRKIN) 
								    WHEN SUBSTR(DT_SHIREH.SIRYMD,1,6) = SUBSTR(DM_KANRI.MONYMD,1,6) AND DM_BKIKAKU.GAICHUKBN <> '0' AND DM_BKIKAKU.GAICHUKBN <> '1'  THEN SUM(SIRKIN) 
								  END AS 当月金額
							FROM DT_SHIREM,DT_SHIREH,DM_BKIKAKU,DM_KANRI
							WHERE
							    DT_SHIREH.SIRJIGYOCD = DT_SHIREM.SIRJIGYOCD 
							AND DT_SHIREH.SIRNO = DT_SHIREM.SIRNO
							AND DT_SHIREH.DELKBN = '0'
							AND DT_SHIREM.DELKBN = '0'
							AND DT_SHIREM.BBUNRUICD = DM_BKIKAKU.BBUNRUICD
							AND DT_SHIREM.BKIKAKUCD = DM_BKIKAKU.BKIKAKUCD
							AND DM_KANRI.KANRINO = '1'
							-- 物件データ
							AND	DT_SHIREM.JIGYOCD = R_物件.JIGYOCD
							AND	DT_SHIREM.SAGYOBKBN = R_物件.SAGYOBKBN
							AND	DT_SHIREM.RENNO = R_物件.RENNO
							AND DM_BKIKAKU.GAICHUKBN = v_カウント
							GROUP BY
								DT_SHIREM.JIGYOCD,DT_SHIREM.SAGYOBKBN,DT_SHIREM.RENNO,DM_BKIKAKU.GAICHUKBN,DM_KANRI.MONYMD,DT_SHIREH.SIRYMD
						) WK GROUP BY WK.GAICHUKBN ;
					-- 仕入が取得できた場合
					BEGIN
					  OPEN c1;
					    FETCH c1 INTO v_外注区分,v_前月以前金額,v_前月金額,v_当月金額;
					    IF c1%NOTFOUND THEN  -- fetch failed
--					    	v_前月以前金額 := 0 ;
--					    	v_前月金額 :=0 ;
--					    	v_当月金額 := 0 ;
							v_外注区分 := v_カウント ;
							
							INSERT INTO WK_PRT705
								   (	  LOGINID
								   		, PROGID
								   		, JIGYOCD
								   		, SAGYOBKBN
								   		, RENNO
								   		, SHIKAKARIKBN
								   		, GAICHUKBN
								   		, URIKING)
							VALUES (
										  I_担当者CD
										, I_更新プログラム
										, R_物件.JIGYOCD
										, R_物件.SAGYOBKBN
										, R_物件.RENNO
										, R_物件.仕掛区分
										, v_カウント
										, R_物件.SOUKINGR) ;
					    ELSE
							INSERT INTO WK_PRT705
							VALUES (
										  I_担当者CD
										, I_更新プログラム
										, R_物件.JIGYOCD
										, R_物件.SAGYOBKBN
										, R_物件.RENNO
										, R_物件.仕掛区分
										, v_カウント
										, DECODE(v_前月以前金額,NULL,0,v_前月以前金額)
										, DECODE(v_前月金額,NULL,0,v_前月金額)
										, DECODE(v_当月金額,NULL,0,v_当月金額)
										, R_物件.SOUKINGR
									) ; 
					    END IF;
					END ;
			v_カウント := v_カウント + 1;
		END LOOP;
--
	END LOOP;	
	
-- 出力指定２よりワークより削除する項目を判定する。
	--出力指定２=１の場合は、仕入金額があるもののみ残す。
	--　　　　　=２の場合は、仕入金額がないもののみ残す。
	--　　　　　=３の場合は、全て。
	FOR R_該当データ IN (
					SELECT 
							  WK1.LOGINID
 							, WK1.PROGID
 							, WK1.JIGYOCD
 							, WK1.SAGYOBKBN
 							, WK1.RENNO
 							, WK1.ZENI
 							, WK1.ZEN
 							, WK1.TOU
 							, WK1.合計
 					FROM(
							SELECT LOGINID,PROGID,JIGYOCD,SAGYOBKBN,RENNO,SUM(ZENIZENKING) AS ZENI,SUM(ZENKING) AS ZEN,SUM(TOUKING) AS TOU
									, SUM(ZENIZENKING+ZENKING+TOUKING) AS 合計
							FROM WK_PRT705
							GROUP BY LOGINID,PROGID,JIGYOCD,SAGYOBKBN,RENNO) WK1
	)LOOP
--	
		IF ( I_指定2 = '1' AND R_該当データ.合計 = 0 ) OR ( I_指定2 = '2' AND R_該当データ.合計 <> 0 ) THEN
			DELETE WK_PRT705
			WHERE	WK_PRT705.LOGINID = R_該当データ.LOGINID
			AND		WK_PRT705.PROGID = R_該当データ.PROGID
			AND		WK_PRT705.JIGYOCD = R_該当データ.JIGYOCD
			AND		WK_PRT705.SAGYOBKBN = R_該当データ.SAGYOBKBN
			AND		WK_PRT705.RENNO = R_該当データ.RENNO ;
		END IF;
	END LOOP ;
	END OMP705;
END POMP705;
/
SHOW ERRORS