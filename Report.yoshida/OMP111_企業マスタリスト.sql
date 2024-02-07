-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP111 企業マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP111
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_KIGYO.KIGYOCD AS 企業CD
            , DM_KIGYO.KIGYONM AS 企業名
            , DM_KIGYO.KIGYONMX AS 企業名カナ
            , DM_KIGYO.RYAKUSHO AS 略称
            , DM_KIGYO.ZIPCODE AS 郵便番号
            , DM_KIGYO.ADD1 AS 住所1
            , DM_KIGYO.ADD2 AS 住所2
            , DM_KIGYO.TELNO AS 電話番号
            , DM_KIGYO.FAXNO AS ＦＡＸ
			, DM_KIGYO.BUSHONM AS 部署名
			, DM_KIGYO.HACCHUTANTNM AS 発注担当者名
			, DM_KIGYO.EIGYOTANTCD AS 営業担当CD
			, DM_TANT.TANTNM AS 担当者名
			, DM_KIGYO.AREACD AS 地区CD
			, DM_AREA.AREANM AS 地区名
		FROM  DM_KIGYO,DM_TANT,DM_AREA
		WHERE
             		DM_KIGYO.DELKBN	 = '0'
             AND	DM_KIGYO.EIGYOTANTCD = DM_TANT.TANTCD(+)
             AND	DM_KIGYO.AREACD = DM_AREA.AREACD(+)
 		ORDER BY
					  DM_KIGYO.KIGYOCD
;
