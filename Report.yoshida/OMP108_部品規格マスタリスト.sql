-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP108 部品規格マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP108
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_BBUNRUI.BBUNRUICD AS 部品分類CD
            , DM_BBUNRUI.BBUNRUINM AS 部品分類名
            , DM_BKIKAKU.BKIKAKUCD AS 部品規格CD
            , DM_BKIKAKU.BKIKAKUNM AS 部品規格名
            , DM_BKIKAKU.TANICD AS 単位CD
            , DM_TANI.TANINM AS 単位名
            , DM_BKIKAKU.SIRTANK AS 仕入単価
            , DM_BKIKAKU.URIAGETANK AS 売上単価
            , DM_BKIKAKU.GAICHUKBN AS 外注区分
            , DK_GAICHU.GAICHUKBNNM AS 外注区分名
		FROM  DM_BBUNRUI,DM_BKIKAKU,DM_TANI,DK_GAICHU
		WHERE
             		DM_BBUNRUI.DELKBN	 = '0'
             AND	DM_BKIKAKU.DELKBN 	 = '0'
             AND	DM_BBUNRUI.BBUNRUICD = DM_BKIKAKU.BBUNRUICD
             AND	DM_BKIKAKU.TANICD = DM_TANI.TANICD(+)
             AND	DM_BKIKAKU.GAICHUKBN = DK_GAICHU.GAICHUKBN(+)
		ORDER BY
					  DM_BBUNRUI.BBUNRUICD,DM_BKIKAKU.BKIKAKUCD
;
