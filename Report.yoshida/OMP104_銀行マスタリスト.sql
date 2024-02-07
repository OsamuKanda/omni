-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP104 銀行マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP104
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_GINKO.GINKOCD AS 銀行CD
            , DM_GINKO.GINKONM AS 銀行名
		FROM  DM_GINKO
		WHERE
             		DM_GINKO.DELKBN	 = '0'
		ORDER BY
					  DM_GINKO.GINKOCD
;
