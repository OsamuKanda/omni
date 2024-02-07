-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP122 報告書分類マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP122
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_HBUNRUI.HBUNRUICD AS 報告書分類CD
            , DM_HBUNRUI.HBUNRUINM AS 報告書分類名
		FROM  DM_HBUNRUI
		WHERE
             		DM_HBUNRUI.DELKBN	 = '0'
		ORDER BY
					  DM_HBUNRUI.HBUNRUICD
;
