-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP120 部門マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP120
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_BUMON.BUMONCD AS 部門CD
            , DM_BUMON.BUMONNM AS 部門名
		FROM  DM_BUMON
		WHERE
             		DM_BUMON.DELKBN	 = '0'
		ORDER BY
					  DM_BUMON.BUMONCD
;
