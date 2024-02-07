-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP110 仕入先マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP110
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_SHIRE.SIRCD AS 仕入先CD
            , DM_SHIRE.SIRNM1 AS 仕入先名1
            , DM_SHIRE.SIRNM2 AS 仕入先名2
            , DM_SHIRE.SIRNMR AS 仕入先略称
            , DM_SHIRE.SIRNMX AS 仕入先カナ
            , DM_SHIRE.ZIPCODE AS 郵便番号
            , DM_SHIRE.ADD1 AS 住所1
            , DM_SHIRE.ADD2 AS 住所2
            , DM_SHIRE.TELNO AS 電話番号
            , DM_SHIRE.FAXNO AS ＦＡＸ
            , DM_SHIRE.HASUKBN AS 端数区分
            , DK_HASU.HASUKBNNM AS 端数区分名	
            , DM_SHIRE.ZENZAN AS 前月残高
            , DM_SHIRE.TSIRKIN AS 当月仕入金額
            , DM_SHIRE.TSIRHENKIN AS 当月仕入返品金額
            , DM_SHIRE.TSIRNEBIKI AS 当月仕入値引金額
            , DM_SHIRE.TTAX AS 当月消費税
            , DM_SHIRE.TSHRGENKIN AS 当月支払現金
            , DM_SHIRE.TSHRTEGATA AS 当月支払手形
            , DM_SHIRE.TSHRNEBIKI AS 当月支払値引
            , DM_SHIRE.TSHRSOSAI AS 当月支払相殺
            , DM_SHIRE.TSHRSONOTA AS 当月支払その他
            , DM_SHIRE.TSHRANZENKAIHI AS 当月支払安全協力会費
            , DM_SHIRE.TSHRFURIKOMITESU AS 当月支払振込手数料
		FROM  DM_SHIRE,DK_HASU
		WHERE
             		DM_SHIRE.DELKBN	 = '0'
             AND	DM_SHIRE.HASUKBN = DK_HASU.HASUKBN(+)
 		ORDER BY
					  DM_SHIRE.SIRCD
;
