CREATE OR REPLACE PACKAGE BODY POMP711 AS
	-------------------------------------------------------------------------------
	-- 【関数名】
	--			分類別仕入一覧表発行
	-- 【内容】 
	--
	--
	-- 【引数】
	--				実行事業所コード、実行プログラム、ログインID
	--				事業所コードFROM-TO、分類コードFROM-TO、指定年月
	--
	-- 【戻り値】
	--
	-------------------------------------------------------------------------------
	PROCEDURE	 OMP711
	(
			I_事業所CD					IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_更新プログラム			IN		DT_BUKKEN.UDTPG1%TYPE
		,	I_担当者CD					IN		DT_BUKKEN.TANTCD%TYPE
		,	I_開始事業所CD				IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_終了事業所CD				IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_開始大分類CD				IN		DT_BUKKEN.BUNRUIDCD%TYPE
		,	I_終了大分類CD				IN		DT_BUKKEN.BUNRUIDCD%TYPE
		,	I_開始中分類CD				IN		DT_BUKKEN.BUNRUICCD%TYPE
		,	I_終了中分類CD				IN		DT_BUKKEN.BUNRUICCD%TYPE
		,	I_指定年月					IN		DT_BUKKEN.TANTCD%TYPE	)
	
	IS

--ワーク項目囲定義

----
	BEGIN

-- 'ﾜｰｸﾌｧｲﾙ削除
	DELETE FROM WK_PRT711 WHERE LOGINID = I_担当者CD AND PROGID = I_更新プログラム ;

-- 仕入データ抽出
	FOR R_仕入 IN (
		SELECT  
--              DT_SHIREH.SIRJIGYOCD AS 事業所CD								--事業所コード
			  CASE 
			  	WHEN DT_SHIREM.JIGYOCD = '90' AND DT_SHIREM.RENNO = '0000001' THEN '90'
			  	WHEN DT_SHIREM.JIGYOCD = '90' AND DT_SHIREM.RENNO > '0000001' THEN '91'
                ELSE DT_SHIREM.JIGYOCD END AS 事業所CD								--事業所コード
            , DM_JIGYO.JIGYONM AS 事業所名									--事業所名
            , DT_BUKKEN.BUNRUIDCD AS 大分類CD								--
            , DM_BUNRUID.BUNRUIDNM AS 大分類名								--
            , DT_BUKKEN.BUNRUICCD AS 中分類CD								--
			, DM_BUNRUIC.BUNRUICNM AS 中分類名								--
			, DM_BKIKAKU.GAICHUKBN AS 外注区分								--
			, DT_BUKKEN.CHOKIKBN AS 長期区分
			, DT_BUKKEN.KANRYOYMD AS 完了日
			, DT_BUKKEN.MAEUKEKBN AS 前受区分
			, DT_BUKKEN.SEIKYUYMD AS 最新請求日
			, DT_SHIREM.SIRKIN AS 仕入金額
		FROM  DM_JIGYO,DT_SHIREH,DT_SHIREM,DM_BKIKAKU,DT_BUKKEN,DM_BUNRUID,DM_BUNRUIC
		WHERE
              		-- 仕入明細
              		DT_SHIREH.SIRJIGYOCD = DT_SHIREM.SIRJIGYOCD
			  AND	DT_SHIREH.SIRNO = DT_SHIREM.SIRNO
			  AND	DT_SHIREH.SIRYMD >= TO_CHAR(I_指定年月 || '01')
			  AND	DT_SHIREH.SIRYMD <= TO_CHAR(I_指定年月 || '31')
			  		-- 事業所マスタ
--			  AND	DT_SHIREH.SIRJIGYOCD = DM_JIGYO.JIGYOCD
--			  AND	DT_SHIREH.SIRJIGYOCD >= I_開始事業所CD
--			  AND	DT_SHIREH.SIRJIGYOCD <= I_終了事業所CD
			  AND	DT_SHIREM.JIGYOCD = DM_JIGYO.JIGYOCD
			  AND	DT_SHIREM.JIGYOCD >= I_開始事業所CD
			  AND	DT_SHIREM.JIGYOCD <= I_終了事業所CD
			  		-- 部品規格マスタ
			  AND	DT_SHIREM.BBUNRUICD = DM_BKIKAKU.BBUNRUICD
			  AND	DT_SHIREM.BKIKAKUCD = DM_BKIKAKU.BKIKAKUCD
			  AND	DM_BKIKAKU.GAICHUKBN <= '2'
			  		-- 物件ファイル
			  AND	DT_SHIREM.JIGYOCD	= DT_BUKKEN.JIGYOCD
			  AND	DT_SHIREM.SAGYOBKBN	= DT_BUKKEN.SAGYOBKBN
			  AND	DT_SHIREM.RENNO		= DT_BUKKEN.RENNO
			  		-- 大分類マスタ
			  AND	DT_BUKKEN.BUNRUIDCD = DM_BUNRUID.BUNRUIDCD(+)
			  AND	DT_BUKKEN.BUNRUIDCD >= I_開始大分類CD
			  AND	DT_BUKKEN.BUNRUIDCD <= I_終了大分類CD
			  		-- 中分類マスタ
			  AND	DT_BUKKEN.BUNRUICCD = DM_BUNRUIC.BUNRUICCD(+)
--			  AND	DT_BUKKEN.BUNRUICCD >= I_開始中分類CD
--			  AND	DT_BUKKEN.BUNRUICCD <= I_終了中分類CD
              AND	DT_SHIREH.DELKBN	 = '0'
              AND	DT_SHIREM.DELKBN	 = '0'
              AND	DT_BUKKEN.DELKBN	 = '0'
              		-- 月次フラグ
              AND	DT_SHIREH.GETFLG	 <> '1'
       	)LOOP
		
		-- 長期区分=2(ｸﾚｰﾑ) or =3（ｻｰﾋﾞｽ）の場合は、仕入金額を当月分へ加算。
		IF R_仕入.長期区分 = '2' OR R_仕入.長期区分 = '3' THEN
				MERGE INTO WK_PRT711
					 USING (SELECT
							 		R_仕入.事業所CD "JIGYOCD"
								,	R_仕入.大分類CD "BUNRUIDCD"
								,	R_仕入.中分類CD "BUNRUICCD"
								, 	R_仕入.外注区分 "GAICHUKBN"
							FROM DUAL
							) PHANTOM
					 ON ( WK_PRT711.LOGINID = I_担当者CD AND WK_PRT711.PROGID = 'OMP711' AND WK_PRT711.EIGCD = PHANTOM.JIGYOCD
					 AND WK_PRT711.BUNRUIDCD = PHANTOM.BUNRUIDCD AND WK_PRT711.BUNRUICCD = PHANTOM.BUNRUICCD AND WK_PRT711.GAICHUKBN = PHANTOM.GAICHUKBN)
				WHEN MATCHED THEN
					UPDATE	SET
							TOUURIKIN		=	TOUURIKIN + R_仕入.仕入金額
				WHEN NOT MATCHED THEN
				INSERT 
				(
					LOGINID
				,	PROGID
				,	EIGCD
				,	BUNRUIDCD
				,	BUNRUICCD
				,	GAICHUKBN
				,	SUDEURIKIN
				,	TOUURIKIN
				,	MIURIKIN
				)VALUES(
					I_担当者CD
				,	'OMP711'
				,	PHANTOM.JIGYOCD
				,	PHANTOM.BUNRUIDCD
				,	PHANTOM.BUNRUICCD
				,	PHANTOM.GAICHUKBN
				,	0
				,	R_仕入.仕入金額
				,	0
				);
		ELSE
				-- 物件ファイル.完了日 <> 0 AND 前受区分 ='1'の場合 AND 完了日の年月 < 指定年月の場合、仕入金額を既売上分仕入へ加算。
			IF R_仕入.完了日 <> '00000000' AND R_仕入.前受区分 = '1'  THEN
				IF	SUBSTR(R_仕入.完了日,1,6) < I_指定年月 THEN
						MERGE INTO WK_PRT711
							 USING (SELECT
									 		R_仕入.事業所CD "JIGYOCD"
										,	R_仕入.大分類CD "BUNRUIDCD"
										,	R_仕入.中分類CD "BUNRUICCD"
										, 	R_仕入.外注区分 "GAICHUKBN"
									FROM DUAL
									) PHANTOM
							 ON ( WK_PRT711.LOGINID = I_担当者CD AND WK_PRT711.PROGID = 'OMP711' AND WK_PRT711.EIGCD = PHANTOM.JIGYOCD AND 
							 	  WK_PRT711.BUNRUIDCD = PHANTOM.BUNRUIDCD AND WK_PRT711.BUNRUICCD = PHANTOM.BUNRUICCD AND WK_PRT711.GAICHUKBN = PHANTOM.GAICHUKBN)
						WHEN MATCHED THEN
							UPDATE	SET
									SUDEURIKIN		=	SUDEURIKIN + R_仕入.仕入金額
						WHEN NOT MATCHED THEN
						INSERT 
						(
							LOGINID
						,	PROGID
						,	EIGCD
						,	BUNRUIDCD
						,	BUNRUICCD
						,	GAICHUKBN
						,	SUDEURIKIN
						,	TOUURIKIN
						,	MIURIKIN
						)VALUES(
							I_担当者CD
						,	'OMP711'
						,	PHANTOM.JIGYOCD
						,	PHANTOM.BUNRUIDCD
						,	PHANTOM.BUNRUICCD
						,	PHANTOM.GAICHUKBN
						,	R_仕入.仕入金額
						,	0
						,	0
						);
				 -- 物件ファイル.完了日 <> 0 AND 前受区分 ='1'の場合 AND 完了日の年月 = 指定年月の場合、当月売上分仕入へ加算。
				ELSIF SUBSTR(R_仕入.完了日,1,6) = I_指定年月 THEN
						MERGE INTO WK_PRT711
							 USING (SELECT
									 		R_仕入.事業所CD "JIGYOCD"
										,	R_仕入.大分類CD "BUNRUIDCD"
										,	R_仕入.中分類CD "BUNRUICCD"
										, 	R_仕入.外注区分 "GAICHUKBN"
									FROM DUAL
									) PHANTOM
							 ON ( WK_PRT711.LOGINID = I_担当者CD AND WK_PRT711.PROGID = 'OMP711' AND WK_PRT711.EIGCD = PHANTOM.JIGYOCD AND 
							 	  WK_PRT711.BUNRUIDCD = PHANTOM.BUNRUIDCD AND WK_PRT711.BUNRUICCD = PHANTOM.BUNRUICCD AND WK_PRT711.GAICHUKBN = PHANTOM.GAICHUKBN)
						WHEN MATCHED THEN
							UPDATE	SET
									TOUURIKIN		=	TOUURIKIN + R_仕入.仕入金額
						WHEN NOT MATCHED THEN
						INSERT 
						(
							LOGINID
						,	PROGID
						,	EIGCD
						,	BUNRUIDCD
						,	BUNRUICCD
						,	GAICHUKBN
						,	SUDEURIKIN
						,	TOUURIKIN
						,	MIURIKIN
						)VALUES(
							I_担当者CD
						,	'OMP711'
						,	PHANTOM.JIGYOCD
						,	PHANTOM.BUNRUIDCD
						,	PHANTOM.BUNRUICCD
						,	PHANTOM.GAICHUKBN
						,	0
						,	R_仕入.仕入金額
						,	0
						);	
				 -- 物件ファイル.完了日 <> 0 AND 前受区分 ='1'の場合 AND 完了日の年月 > 指定年月の場合、未売上分仕入へ加算。
				ELSIF SUBSTR(R_仕入.完了日,1,6) > I_指定年月 THEN
						MERGE INTO WK_PRT711
							 USING (SELECT
									 		R_仕入.事業所CD "JIGYOCD"
										,	R_仕入.大分類CD "BUNRUIDCD"
										,	R_仕入.中分類CD "BUNRUICCD"
										, 	R_仕入.外注区分 "GAICHUKBN"
									FROM DUAL
									) PHANTOM
							 ON ( WK_PRT711.LOGINID = I_担当者CD AND WK_PRT711.PROGID = 'OMP711' AND WK_PRT711.EIGCD = PHANTOM.JIGYOCD AND 
							 	  WK_PRT711.BUNRUIDCD = PHANTOM.BUNRUIDCD AND WK_PRT711.BUNRUICCD = PHANTOM.BUNRUICCD AND WK_PRT711.GAICHUKBN = PHANTOM.GAICHUKBN)
						WHEN MATCHED THEN
							UPDATE	SET
									MIURIKIN		=	MIURIKIN + R_仕入.仕入金額
						WHEN NOT MATCHED THEN
						INSERT 
						(
							LOGINID
						,	PROGID
						,	EIGCD
						,	BUNRUIDCD
						,	BUNRUICCD
						,	GAICHUKBN
						,	SUDEURIKIN
						,	TOUURIKIN
						,	MIURIKIN
						)VALUES(
							I_担当者CD
						,	'OMP711'
						,	PHANTOM.JIGYOCD
						,	PHANTOM.BUNRUIDCD
						,	PHANTOM.BUNRUICCD
						,	PHANTOM.GAICHUKBN
						,	0
						,	0
						,	R_仕入.仕入金額
						);	
				END IF;
--
				  -- 物件ファイル.完了日 <> 0 AND 前受区分 ='0'の場合、
			ELSIF R_仕入.完了日 <> '00000000' AND R_仕入.前受区分 = '0'   THEN
					--最新請求日 = ALL '0' OR 最新請求日 がNULL の場合、仕入金額を未売上分仕入へ加算。
					IF R_仕入.最新請求日 = '00000000' OR R_仕入.最新請求日 IS NULL THEN
						MERGE INTO WK_PRT711
							 USING (SELECT
									 		R_仕入.事業所CD "JIGYOCD"
										,	R_仕入.大分類CD "BUNRUIDCD"
										,	R_仕入.中分類CD "BUNRUICCD"
										, 	R_仕入.外注区分 "GAICHUKBN"
									FROM DUAL
									) PHANTOM
							 ON ( WK_PRT711.LOGINID = I_担当者CD AND WK_PRT711.PROGID = 'OMP711' AND WK_PRT711.EIGCD = PHANTOM.JIGYOCD AND 
							 	  WK_PRT711.BUNRUIDCD = PHANTOM.BUNRUIDCD AND WK_PRT711.BUNRUICCD = PHANTOM.BUNRUICCD AND WK_PRT711.GAICHUKBN = PHANTOM.GAICHUKBN)
						WHEN MATCHED THEN
							UPDATE	SET
									MIURIKIN		=	MIURIKIN + R_仕入.仕入金額
						WHEN NOT MATCHED THEN
						INSERT 
						(
							LOGINID
						,	PROGID
						,	EIGCD
						,	BUNRUIDCD
						,	BUNRUICCD
						,	GAICHUKBN
						,	SUDEURIKIN
						,	TOUURIKIN
						,	MIURIKIN
						)VALUES(
							I_担当者CD
						,	'OMP711'
						,	PHANTOM.JIGYOCD
						,	PHANTOM.BUNRUIDCD
						,	PHANTOM.BUNRUICCD
						,	PHANTOM.GAICHUKBN
						,	0
						,	0
						,	R_仕入.仕入金額
						);
					--最新請求日 <> ALL '0' で且つ最新請求日 < I_指定年月の場合、仕入金額を既売上分仕入へ加算。
					ELSIF R_仕入.最新請求日 <> '00000000'  AND SUBSTR(R_仕入.最新請求日,1,6) < I_指定年月 THEN
						MERGE INTO WK_PRT711
							 USING (SELECT
									 		R_仕入.事業所CD "JIGYOCD"
										,	R_仕入.大分類CD "BUNRUIDCD"
										,	R_仕入.中分類CD "BUNRUICCD"
										, 	R_仕入.外注区分 "GAICHUKBN"
									FROM DUAL
									) PHANTOM
							 ON ( WK_PRT711.LOGINID = I_担当者CD AND WK_PRT711.PROGID = 'OMP711' AND WK_PRT711.EIGCD = PHANTOM.JIGYOCD AND 
							 	  WK_PRT711.BUNRUIDCD = PHANTOM.BUNRUIDCD AND WK_PRT711.BUNRUICCD = PHANTOM.BUNRUICCD AND WK_PRT711.GAICHUKBN = PHANTOM.GAICHUKBN)
						WHEN MATCHED THEN
							UPDATE	SET
									SUDEURIKIN		=	SUDEURIKIN + R_仕入.仕入金額
						WHEN NOT MATCHED THEN
						INSERT 
						(
							LOGINID
						,	PROGID
						,	EIGCD
						,	BUNRUIDCD
						,	BUNRUICCD
						,	GAICHUKBN
						,	SUDEURIKIN
						,	TOUURIKIN
						,	MIURIKIN
						)VALUES(
							I_担当者CD
						,	'OMP711'
						,	PHANTOM.JIGYOCD
						,	PHANTOM.BUNRUIDCD
						,	PHANTOM.BUNRUICCD
						,	PHANTOM.GAICHUKBN
						,	R_仕入.仕入金額
						,	0
						,	0
						);
					--最新請求日 <> ALL '0' で且つ最新請求日 = I_指定年月の場合、仕入金額を当月売上分仕入へ加算。
					ELSIF R_仕入.最新請求日 <> '00000000'  AND SUBSTR(R_仕入.最新請求日,1,6) = I_指定年月 THEN
						MERGE INTO WK_PRT711
							 USING (SELECT
									 		R_仕入.事業所CD "JIGYOCD"
										,	R_仕入.大分類CD "BUNRUIDCD"
										,	R_仕入.中分類CD "BUNRUICCD"
										, 	R_仕入.外注区分 "GAICHUKBN"
									FROM DUAL
									) PHANTOM
							 ON ( WK_PRT711.LOGINID = I_担当者CD AND WK_PRT711.PROGID = 'OMP711' AND WK_PRT711.EIGCD = PHANTOM.JIGYOCD AND 
							 	  WK_PRT711.BUNRUIDCD = PHANTOM.BUNRUIDCD AND WK_PRT711.BUNRUICCD = PHANTOM.BUNRUICCD AND WK_PRT711.GAICHUKBN = PHANTOM.GAICHUKBN)
						WHEN MATCHED THEN
							UPDATE	SET
									TOUURIKIN		=	TOUURIKIN + R_仕入.仕入金額
						WHEN NOT MATCHED THEN
						INSERT 
						(
							LOGINID
						,	PROGID
						,	EIGCD
						,	BUNRUIDCD
						,	BUNRUICCD
						,	GAICHUKBN
						,	SUDEURIKIN
						,	TOUURIKIN
						,	MIURIKIN
						)VALUES(
							I_担当者CD
						,	'OMP711'
						,	PHANTOM.JIGYOCD
						,	PHANTOM.BUNRUIDCD
						,	PHANTOM.BUNRUICCD
						,	PHANTOM.GAICHUKBN
						,	0
						,	R_仕入.仕入金額
						,	0
						);
					--最新請求日 <> ALL '0' で且つ最新請求日 > I_指定年月の場合、仕入金額を未売上分仕入へ加算。
					ELSIF R_仕入.最新請求日 <> '00000000'  AND SUBSTR(R_仕入.最新請求日,1,6) > I_指定年月 THEN
						MERGE INTO WK_PRT711
							 USING (SELECT
									 		R_仕入.事業所CD "JIGYOCD"
										,	R_仕入.大分類CD "BUNRUIDCD"
										,	R_仕入.中分類CD "BUNRUICCD"
										, 	R_仕入.外注区分 "GAICHUKBN"
									FROM DUAL
									) PHANTOM
							 ON ( WK_PRT711.LOGINID = I_担当者CD AND WK_PRT711.PROGID = 'OMP711' AND WK_PRT711.EIGCD = PHANTOM.JIGYOCD AND 
							 	  WK_PRT711.BUNRUIDCD = PHANTOM.BUNRUIDCD AND WK_PRT711.BUNRUICCD = PHANTOM.BUNRUICCD AND WK_PRT711.GAICHUKBN = PHANTOM.GAICHUKBN)
						WHEN MATCHED THEN
							UPDATE	SET
									MIURIKIN		=	MIURIKIN + R_仕入.仕入金額
						WHEN NOT MATCHED THEN
						INSERT 
						(
							LOGINID
						,	PROGID
						,	EIGCD
						,	BUNRUIDCD
						,	BUNRUICCD
						,	GAICHUKBN
						,	SUDEURIKIN
						,	TOUURIKIN
						,	MIURIKIN
						)VALUES(
							I_担当者CD
						,	'OMP711'
						,	PHANTOM.JIGYOCD
						,	PHANTOM.BUNRUIDCD
						,	PHANTOM.BUNRUICCD
						,	PHANTOM.GAICHUKBN
						,	0
						,	0
						,	R_仕入.仕入金額
						);
					END IF ;
			ELSE
						-- 完了日 = ALL '0' の場合は、仕入金額を未売上分仕入へ加算。
						MERGE INTO WK_PRT711
							 USING (SELECT
									 		R_仕入.事業所CD "JIGYOCD"
										,	R_仕入.大分類CD "BUNRUIDCD"
										,	R_仕入.中分類CD "BUNRUICCD"
										, 	R_仕入.外注区分 "GAICHUKBN"
									FROM DUAL
									) PHANTOM
							 ON ( WK_PRT711.LOGINID = I_担当者CD AND WK_PRT711.PROGID = 'OMP711' AND WK_PRT711.EIGCD = PHANTOM.JIGYOCD AND 
							 	  WK_PRT711.BUNRUIDCD = PHANTOM.BUNRUIDCD AND WK_PRT711.BUNRUICCD = PHANTOM.BUNRUICCD AND WK_PRT711.GAICHUKBN = PHANTOM.GAICHUKBN)
						WHEN MATCHED THEN
							UPDATE	SET
									MIURIKIN		=	MIURIKIN + R_仕入.仕入金額
						WHEN NOT MATCHED THEN
						INSERT 
						(
							LOGINID
						,	PROGID
						,	EIGCD
						,	BUNRUIDCD
						,	BUNRUICCD
						,	GAICHUKBN
						,	SUDEURIKIN
						,	TOUURIKIN
						,	MIURIKIN
						)VALUES(
							I_担当者CD
						,	'OMP711'
						,	PHANTOM.JIGYOCD
						,	PHANTOM.BUNRUIDCD
						,	PHANTOM.BUNRUICCD
						,	PHANTOM.GAICHUKBN
						,	0
						,	0
						,	R_仕入.仕入金額
						);
			END IF ;
		END IF;
--
	END LOOP;	

	END OMP711;
END POMP711;
/
SHOW ERRORS