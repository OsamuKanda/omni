-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP109 担当者マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP109
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_TANT.TANTCD AS 担当者CD
            , DM_TANT.TANTNM AS 担当者名
            , DM_TANT.SHANAIKBN AS 社内区分
            , DK_SHANAI.SHANAIKBNNM AS 社内区分名
            , DM_TANT.SYOZOKJIGYOCD AS 所属事業所CD
            , DM_JIGYO.JIGYONM AS 事業所名
            , DM_TANT.KIGYOCD AS 企業CD
            , DM_KIGYO.KIGYONM AS 企業名
            , DM_TANT.UMUKBN AS 作業有無区分
            , DK_UMU.UMUKBNNM AS 有無区分名
		FROM  DM_TANT,DK_SHANAI,DM_JIGYO,DM_KIGYO,DK_UMU
		WHERE
             		DM_TANT.DELKBN	 = '0'
             AND	DM_TANT.SHANAIKBN = DK_SHANAI.SHANAIKBN(+)
             AND	DM_TANT.SYOZOKJIGYOCD = DM_JIGYO.JIGYOCD(+)
             AND	DM_TANT.KIGYOCD = DM_KIGYO.KIGYOCD(+)
             AND	DM_TANT.UMUKBN = DK_UMU.UMUKBN(+)
		ORDER BY
					  DM_TANT.SYOZOKJIGYOCD,DM_TANT.TANTCD
;
