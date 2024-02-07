-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP123 報告書パターンマスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP123
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_HPATAN.PATANCD AS CD
            , DM_HPATAN.PATANNM AS 名称
            , DM_HPATAN.GYONO AS 行番号
            , DM_HPATAN.HBUNRUICD AS 報告書分類CD
            , DM_HBUNRUI.HBUNRUINM AS 報告書分類名
            , DM_HPATAN.HSYOSAIMONG AS 詳細文言
            , DM_HPATAN.INPUTUMU AS 入力有無
            , DM_HPATAN.INPUTNAIYOU AS 入力内容
		FROM  DM_HBUNRUI,DM_HPATAN
		WHERE
             		DM_HBUNRUI.DELKBN	 = '0'
             AND	DM_HPATAN.DELKBN	 = '0'
             AND	DM_HPATAN.HBUNRUICD = DM_HBUNRUI.HBUNRUICD
		ORDER BY
					  DM_HPATAN.PATANCD,DM_HPATAN.GYONO	
;
