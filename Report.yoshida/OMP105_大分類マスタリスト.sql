-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP105 大分類マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP105
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_BUNRUID.BUNRUIDCD AS 大分類CD
            , DM_BUNRUID.BUNRUIDNM AS 大分類名
		FROM  DM_BUNRUID
		WHERE
             		DM_BUNRUID.DELKBN	 = '0'
		ORDER BY
					  DM_BUNRUID.BUNRUIDCD
;
