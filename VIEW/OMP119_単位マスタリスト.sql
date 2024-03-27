-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP119 単位マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP119
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_TANI.TANICD AS 単位CD
            , DM_TANI.TANINM AS 単位名
		FROM  DM_TANI
		WHERE
             		DM_TANI.DELKBN	 = '0'
		ORDER BY
					  DM_TANI.TANICD
;
