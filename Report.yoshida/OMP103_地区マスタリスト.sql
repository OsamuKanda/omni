-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP103 地区マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP103
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_AREA.AREACD AS 地区CD
            , DM_AREA.AREANM AS 地区名
            , DM_AREA.AREANMR AS 地区略称
		FROM  DM_AREA
		WHERE
             		DM_AREA.DELKBN	 = '0'
		ORDER BY
					  DM_AREA.AREACD
;
