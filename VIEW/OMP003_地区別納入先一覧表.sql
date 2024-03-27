-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP003 地区別納入先一覧表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP003
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_NONYU.JIGYOCD AS 事業所CD									--事業所コード
            , DM_JIGYO.JIGYONM AS 事業所名									--事業所名
            , DM_NONYU.AREACD AS 地区CD										--地区コード
            , DM_AREA.AREANM AS 地区名										--地区名
			, DM_NONYU.NONYUCD AS 納入先CD									--納入先コード
			, DM_NONYU.NONYUNM1 AS 納入先名1								--納入先名１
			, DM_NONYU.NONYUNM2 AS 納入先名2								--納入先名２
			, DM_NONYU.EIGYOTANTCD AS 営業担当CD							--営業担当コード
			, DM_TANT.TANTNM AS 営業担当者名								--営業担当者名
			, DM_HOSHU.GOUKI AS 号機										--号機
			, DECODE(DM_HOSHU.SECCHIYMD,NULL,NULL,SUBSTR(DM_HOSHU.SECCHIYMD,1,4) || '/' || SUBSTR(DM_HOSHU.SECCHIYMD,5,2)) AS 設置年月
			, 日付記号追加(DM_HOSHU.KEIYAKUYMD) AS 契約年月日				--契約年月日
			, 日付記号追加(DM_HOSHU.HOSHUSTARTYMD) AS 保守計算開始日		--保守計算開始日
			, 1 AS 台数														--台数
			, DECODE(DM_HOSHU.KEIYAKUYMD,NULL,1,0) AS 契約台数				--契約台数
			, DM_HOSHU.TANTCD AS 担当CD										--担当コード
			, T1.TANTNM AS 担当者名											--担当者名
			, DM_HOSHU.SAGYOUTANTCD AS 作業担当者CD							--作業担当者コード
			, T2.TANTNM AS 作業担当者名										--作業担当者名
		FROM DM_NONYU,DM_JIGYO,DM_AREA,DM_TANT,DM_HOSHU,DM_TANT T1,DM_TANT T2
		WHERE
					DM_NONYU.SECCHIKBN = '01'
					-- 事業所マスタと結合
              AND	DM_NONYU.JIGYOCD = DM_JIGYO.JIGYOCD
					-- 地区マスタと結合
              AND	DM_NONYU.AREACD = DM_AREA.AREACD(+)
              		-- 担当者マスタ
              AND	DM_TANT.TANTCD(+) = DM_NONYU.EIGYOTANTCD
              		-- 保守点検マスタ
              AND	DM_HOSHU.NONYUCD = DM_NONYU.NONYUCD
              AND	DM_HOSHU.DELKBN	 = '0'
              		-- 担当者マスタ
              AND	T1.TANTCD(+) = DM_HOSHU.TANTCD
              		-- 作業担当者マスタ
              AND	T2.TANTCD(+) = DM_HOSHU.SAGYOUTANTCD
        ORDER BY
        			  DM_NONYU.JIGYOCD
					, DM_NONYU.AREACD
					, DM_NONYU.NONYUCD
					, DM_HOSHU.GOUKI
;
