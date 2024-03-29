-------------------------------------------------------------------------------
--IjeNmVXev[X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP111 éÆ}X^Xg
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP111
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_KIGYO.KIGYOCD AS éÆCD
            , DM_KIGYO.KIGYONM AS éÆ¼
            , DM_KIGYO.KIGYONMX AS éÆ¼Ji
            , DM_KIGYO.RYAKUSHO AS ªÌ
            , DM_KIGYO.ZIPCODE AS XÖÔ
            , DM_KIGYO.ADD1 AS Z1
            , DM_KIGYO.ADD2 AS Z2
            , DM_KIGYO.TELNO AS dbÔ
            , DM_KIGYO.FAXNO AS e`w
			, DM_KIGYO.BUSHONM AS ¼
			, DM_KIGYO.HACCHUTANTNM AS ­SÒ¼
			, DM_KIGYO.EIGYOTANTCD AS cÆSCD
			, DM_TANT.TANTNM AS SÒ¼
			, DM_KIGYO.AREACD AS næCD
			, DM_AREA.AREANM AS næ¼
		FROM  DM_KIGYO,DM_TANT,DM_AREA
		WHERE
             		DM_KIGYO.DELKBN	 = '0'
             AND	DM_KIGYO.EIGYOTANTCD = DM_TANT.TANTCD(+)
             AND	DM_KIGYO.AREACD = DM_AREA.AREACD(+)
 		ORDER BY
					  DM_KIGYO.KIGYOCD
;
