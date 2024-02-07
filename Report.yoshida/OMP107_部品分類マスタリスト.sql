-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP107 部品分類マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP107
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_BBUNRUI.BBUNRUICD AS 部品分類CD
            , DM_BBUNRUI.BBUNRUINM AS 部品分類名
		FROM  DM_BBUNRUI
		WHERE
             		DM_BBUNRUI.DELKBN	 = '0'
		ORDER BY
					  DM_BBUNRUI.BBUNRUICD
;
