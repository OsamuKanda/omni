-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP001 納入先順顧客台帳
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP001
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_NONYU.JIGYOCD AS 事業所CD									--事業所コード
            , DM_JIGYO.JIGYONM AS 事業所名									--事業所名
			, DECODE(DM_NONYU.SEIKYUSAKICD1,NULL,NULL,'故障1') AS 故障1		--
            , DM_NONYU.SEIKYUSAKICD1 AS 故障修理請求先CD11					--故障修理請求先１コード１
            , K1.NONYUNM1 AS 故障修理請求先名11								--故障修理請求先１名１
			, K1.ZIPCODE AS 故障修理請求先郵便番号1							--故障修理請求先１郵便番号１
			, K1.ADD1 AS 故障修理請求先住所11								--故障修理請求先１住所１
			, K1.ADD2 AS 故障修理請求先住所12								--故障修理請求先１住所２
			, K1.TELNO1 AS 故障修理請求先電話番号11							--故障修理請求先１電話番号１
			, K1.TELNO2 AS 故障修理請求先電話番号12							--故障修理請求先１電話番号２
			, DECODE(DM_NONYU.SEIKYUSAKICD2,NULL,NULL,'故障2') AS 故障2		--
            , DM_NONYU.SEIKYUSAKICD2 AS 故障修理請求先CD21					--故障修理請求先２コード２
            , K2.NONYUNM1 AS 故障修理請求先名21								--故障修理請求先２名２
			, K2.ZIPCODE AS 故障修理請求先郵便番号21						--故障修理請求先２郵便番号１
			, K2.ADD1 AS 故障修理請求先住所21								--故障修理請求先２住所１
			, K2.ADD2 AS 故障修理請求先住所22								--故障修理請求先２住所２
			, K2.TELNO1 AS 故障修理請求先電話番号21							--故障修理請求先２電話番号１
			, K2.TELNO2 AS 故障修理請求先電話番号22							--故障修理請求先２電話番号２
			, DECODE(DM_NONYU.SEIKYUSAKICD3,NULL,NULL,'故障3') AS 故障3		--
            , DM_NONYU.SEIKYUSAKICD3 AS 故障修理請求先CD31					--故障修理請求先３コード３
            , K3.NONYUNM1 AS 故障修理請求先名31								--故障修理請求先３名３
			, K3.ZIPCODE AS 故障修理請求先郵便番号31						--故障修理請求先３郵便番号１
			, K3.ADD1 AS 故障修理請求先住所31								--故障修理請求先３住所１
			, K3.ADD2 AS 故障修理請求先住所32								--故障修理請求先３住所２
			, K3.TELNO1 AS 故障修理請求先電話番号31							--故障修理請求先３電話番号１
			, K3.TELNO2 AS 故障修理請求先電話番号32							--故障修理請求先３電話番号２
			, DECODE(DM_NONYU.SEIKYUSAKICDH,NULL,NULL,'保守') AS 保守		--
            , DM_NONYU.SEIKYUSAKICDH AS 保守点検請求先CD					--保守点検請求先３コード３
            , H1.NONYUNM1 AS 保守点検請求先名								--保守点検請求先３名３
			, H1.ZIPCODE AS 保守点検請求先郵便番号							--保守点検請求先３郵便番号１
			, H1.ADD1 AS 保守点検請求先住所1								--保守点検請求先３住所１
			, H1.ADD2 AS 保守点検請求先住所2								--保守点検請求先３住所２
			, H1.TELNO1 AS 保守点検請求先電話番号1							--保守点検請求先３電話番号１
			, H1.TELNO2 AS 保守点検請求先電話番号2							--保守点検請求先３電話番号２
			, DECODE(DM_NONYU.NONYUCD,NULL,NULL,'納入') AS 納入				--
			, DM_NONYU.NONYUCD AS 納入先CD									--納入先コード
			, DM_NONYU.NONYUNM1 AS 納入先名									--納入先名
			, DM_NONYU.ZIPCODE AS 郵便番号									--郵便番号
			, DM_NONYU.ADD1 AS 住所1										--住所１
			, DM_NONYU.ADD2 AS 住所2										--住所２
			, DM_NONYU.TELNO1 AS 電話番号1									--電話番号１
			, DM_NONYU.TELNO2 AS 電話番号2									--電話番号２
			, DM_NONYU.MOCHINUSHI AS 建物持ち主								--建物持ち主
			, DM_NONYU.KIGYOCD AS 企業CD									--企業コード
			, DECODE(DM_KIGYO.KIGYONM,NULL,'企業マスタに無し',DM_KIGYO.KIGYONM) AS 企業名									--企業名
			, DM_KIGYO.BUSHONM AS 部署名									--部署名
			, DM_KIGYO.HACCHUTANTNM AS 管理担当者名							--管理担当者名
			, DM_TANT.TANTNM AS 担当者名									--担当者名
			, DM_HOSHU.GOUKI AS 号機										--号機
			, DM_HOSHU.KISHUKATA AS 機種									--機種
			, DM_HOSHU.YOSHIDANO AS ヨシダ工番								--ヨシダ工番
			, DECODE(DM_HOSHU.SECCHIYMD,NULL,NULL,SUBSTR(DM_HOSHU.SECCHIYMD,1,4) || '/' || SUBSTR(DM_HOSHU.SECCHIYMD,5,2)) AS 設置年月
			, RTRIM(DECODE(DM_HOSHU.HOSHUM1,0,'','1,') || DECODE(DM_HOSHU.HOSHUM2,0,'','2,') || DECODE(DM_HOSHU.HOSHUM3,0,'','3,') ||
			  DECODE(DM_HOSHU.HOSHUM4,0,'','4,') || DECODE(DM_HOSHU.HOSHUM5,0,'','5,') || DECODE(DM_HOSHU.HOSHUM6,0,'','6,') ||
			  DECODE(DM_HOSHU.HOSHUM7,0,'','7,') || DECODE(DM_HOSHU.HOSHUM8,0,'','8,') || DECODE(DM_HOSHU.HOSHUM9,0,'','9,') ||
			  DECODE(DM_HOSHU.HOSHUM10,0,'','10,') || DECODE(DM_HOSHU.HOSHUM11,0,'','11,') || DECODE(DM_HOSHU.HOSHUM12,0,'','12,'),',') AS 保守対応
			, 経過年月(DM_HOSHU.SECCHIYMD) AS 経過年月						--
			, 日付記号追加(DM_HOSHU.KEIYAKUYMD) AS 契約年月日				--
			, DM_HOSHU.KEIYAKUKING AS 契約金額								--
			, DK_HOSHU.HOSHUKBNNM AS 計算方法								--
			, DECODE(DM_HOSHU.KEIYAKUYMD,NULL,'0','1') AS 契約区分			--
			, DM_NONYU.HURIGANA AS フリガナ
		FROM DM_NONYU,DM_JIGYO,
			 DM_NONYU K1,DM_NONYU K2,DM_NONYU K3,
			 DM_NONYU H1,DM_KIGYO,DM_HOSHU,DM_TANT,DK_HOSHU
		WHERE
					DM_NONYU.SECCHIKBN = '01'
					-- 事業所マスタと結合
              AND	DM_NONYU.JIGYOCD = DM_JIGYO.JIGYOCD
              		-- 故障修理請求先１
              AND	K1.JIGYOCD(+) = DM_NONYU.JIGYOCD
              AND	K1.NONYUCD(+) = DM_NONYU.SEIKYUSAKICD1
              AND	K1.SECCHIKBN(+) = '00'
              		-- 故障修理請求先２
              AND	K2.JIGYOCD(+) = DM_NONYU.JIGYOCD
              AND	K2.NONYUCD(+) = DM_NONYU.SEIKYUSAKICD2
              AND	K2.SECCHIKBN(+) = '00'              
              		-- 故障修理請求先３
              AND	K3.JIGYOCD(+) = DM_NONYU.JIGYOCD
              AND	K3.NONYUCD(+) = DM_NONYU.SEIKYUSAKICD3
              AND	K3.SECCHIKBN(+) = '00'              
              		-- 保守点検請求先
              AND	H1.JIGYOCD(+) = DM_NONYU.JIGYOCD
              AND	H1.NONYUCD(+) = DM_NONYU.SEIKYUSAKICDH
              AND	H1.SECCHIKBN(+) = '00'
              		-- 企業マスタ
              AND	DM_KIGYO.KIGYOCD(+) = DM_NONYU.KIGYOCD
              AND	DM_KIGYO.DELKBN(+) = '0'
              		-- 担当者マスタ
              AND	DM_TANT.TANTCD(+) = DM_KIGYO.EIGYOTANTCD
              		-- 保守点検マスタ
              AND	DM_HOSHU.NONYUCD = DM_NONYU.NONYUCD
              AND	DM_HOSHU.DELKBN	 = '0'
              		-- 保守計算区分マスタ
              AND	DK_HOSHU.HOSHUKBN = DM_HOSHU.HOSHUKBN
		ORDER BY
					  DM_NONYU.JIGYOCD
					, DM_NONYU.NONYUCD
					, DM_HOSHU.GOUKI
;
