-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP106 中分類マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP106
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_BUNRUIC.BUNRUICCD AS 中分類CD
            , DM_BUNRUIC.BUNRUICNM AS 中分類名
		FROM  DM_BUNRUIC
		WHERE
             		DM_BUNRUIC.DELKBN	 = '0'
		ORDER BY
					  DM_BUNRUIC.BUNRUICCD
;
