-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP118 種別マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP118
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_SHUBETSU.SHUBETSUCD AS 種別CD
            , DM_SHUBETSU.SHUBETSUNM AS 種別名
		FROM  DM_SHUBETSU
		WHERE
             		DM_SHUBETSU.DELKBN	 = '0'
		ORDER BY
					  DM_SHUBETSU.SHUBETSUCD
;
