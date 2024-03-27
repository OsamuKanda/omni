-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP115 原因マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP115
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_GENIN.GENINCD AS 原因CD
            , DM_GENIN.GENINNAIYO AS 原因内容
		FROM  DM_GENIN
		WHERE
             		DM_GENIN.DELKBN	 = '0'
		ORDER BY
					  DM_GENIN.GENINCD
;
