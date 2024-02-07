-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP117 品名マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP117
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_HINNM.HINCD AS 品CD
            , DM_HINNM.HINNM1 AS 品名1
            , DM_HINNM.HINNM2 AS 品名2
            , DM_HINNM.SURYO AS 数量
            , DM_HINNM.TANICD AS 単位CD
            , DM_TANI.TANINM AS 単位名
		FROM  DM_HINNM,DM_TANI
		WHERE
             		DM_HINNM.DELKBN	 = '0'
             AND	DM_HINNM.TANICD = DM_TANI.TANICD(+)
		ORDER BY
					  DM_HINNM.HINCD
;
