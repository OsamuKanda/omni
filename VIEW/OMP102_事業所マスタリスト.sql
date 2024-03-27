-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP102 事業所マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP102
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_JIGYO.JIGYOCD AS 事業所CD									--事業所コード
            , DM_JIGYO.JIGYONM AS 事業所名									--事業所名
            , DM_JIGYO.ZIPCODE AS 郵便番号
            , DM_JIGYO.ADD1 AS 住所1
            , DM_JIGYO.ADD2 AS 住所2
            , DM_JIGYO.TELNO AS 電話番号
            , DM_JIGYO.FAXNO AS ＦＡＸ
            , DM_JIGYO.FURIGINKONM AS 請求書振込銀行名
            , DM_JIGYO.TOKUGINKONM AS 請求書特定銀行名
            , DM_JIGYO.BUKKENNO AS 物件番号
            , DM_JIGYO.SEIKYUSHONO AS 請求書番号
            , DM_JIGYO.NYUKINNO AS 入金番号
            , DM_JIGYO.HACCHUNO AS 発注番号
            , DM_JIGYO.SIRNO AS 仕入番号
            , DM_JIGYO.SHRNO AS 支払番号
		FROM  DM_JIGYO
		WHERE
             		DM_JIGYO.DELKBN	 = '0'
		ORDER BY
					  DM_JIGYO.JIGYOCD
;
