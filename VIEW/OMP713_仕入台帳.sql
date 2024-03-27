-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/10   OKADA
--                                                 Update:2012/10/11
-------------------------------------------------------------------------------
--OMP713 仕入台帳
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP713
AS
	SELECT
		  WK.仕入先CD
		, WK.仕入先名1
		, WK.仕入先名2
		, WK.仕入先カナ
		, WK.前月残高
		, WK.日付
		, WK.仕入事業所CD
		, WK.仕入番号
		, WK.行番号
		, WK.物件番号
		, WK.納入先CD
		, WK.納入先略称
		, WK.部品CD
		, WK.規格名
		, WK.仕入数量
		, WK.単位名
		, WK.単価
		, WK.金額
		, WK.消費税
		, WK.合計
		, WK.仕入日付条件
		, WK.区分
		, SUBSTR(DM_KANRI.MONYMD,1,4) || '年' ||  SUBSTR(DM_KANRI.MONYMD,5,2) || '月度' AS 月次締年月
	FROM
(		SELECT  
              DM_SHIRE.SIRCD AS 仕入先CD									--
            , DM_SHIRE.SIRNM1 AS 仕入先名1									--
            , DM_SHIRE.SIRNM2 AS 仕入先名2									--
            , DM_SHIRE.SIRNMX AS 仕入先カナ									--
            , DM_SHIRE.ZENZAN AS 前月残高									--
            , 日付記号追加(DT_SHIREH.SIRYMD) AS 日付						--
            , DT_SHIREH.SIRJIGYOCD AS 仕入事業所CD							--
            , DT_SHIREH.SIRNO AS 仕入番号									--
            , DT_SHIREM.GYONO AS 行番号										--
            , DT_SHIREM.JIGYOCD || '-' || DT_SHIREM.SAGYOBKBN || '-' || DT_SHIREM.RENNO AS 物件番号
            , DT_BUKKEN.NONYUCD AS 納入先CD									--
            , DM_NONYU.NONYUNMR AS 納入先略称
            , DT_SHIREM.BBUNRUICD || '-' || DT_SHIREM.BKIKAKUCD AS 部品CD
            , DT_SHIREM.BKIKAKUNM AS 規格名
            , DT_SHIREM.SIRSU AS 仕入数量
            , DM_TANI.TANINM AS 単位名
            , DT_SHIREM.SIRTANK AS 単価
            , DT_SHIREM.SIRKIN AS 金額
            , DT_SHIREM.TAX AS 消費税
            , NULL AS 合計
            , DT_SHIREH.SIRYMD AS 仕入日付条件
            , 1 AS 区分
		FROM  DM_SHIRE,DT_SHIREH,DT_SHIREM,DT_BUKKEN,DM_TANI,DM_NONYU
		WHERE
			  		-- 仕入明細
			  		DT_SHIREM.SIRJIGYOCD = DT_SHIREH.SIRJIGYOCD
			  AND	DT_SHIREM.SIRNO = DT_SHIREH.SIRNO
			  		-- 月次フラグ
			  AND	DT_SHIREH.GETFLG <> '1'
			  		-- 物件ファイル
			  AND	DT_SHIREM.JIGYOCD = DT_BUKKEN.JIGYOCD(+)
			  AND	DT_SHIREM.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN(+)
			  AND	DT_SHIREM.RENNO = DT_BUKKEN.RENNO(+)
			  		-- 納入先マスタ
			  AND	DT_BUKKEN.NONYUCD = DM_NONYU.NONYUCD(+)
			  AND	'01' = DM_NONYU.SECCHIKBN(+)
--2012.10.11-------------------------------------------------------
--			  AND	DT_BUKKEN.JIGYOCD = DM_NONYU.JIGYOCD(+)
			  		-- 単位マスタ
			  AND	DT_SHIREM.TANICD = DM_TANI.TANICD(+)
			  		-- 仕入先マスタ
			  AND	DT_SHIREH.SIRCD = DM_SHIRE.SIRCD(+)
			  AND	DT_SHIREH.DELKBN = '0'
			  AND	DT_SHIREM.DELKBN = '0'
UNION ALL
		SELECT  
              DM_SHIRE.SIRCD AS 仕入先CD									--
            , DM_SHIRE.SIRNM1 AS 仕入先名1									--
            , DM_SHIRE.SIRNM2 AS 仕入先名2									--
            , DM_SHIRE.SIRNMX AS 仕入先カナ									--
            , DM_SHIRE.ZENZAN AS 前月残高									--
            , 日付記号追加(DT_SHRH.SHRYMD) AS 日付							--
            , DT_SHRH.JIGYOCD AS 支払事業所CD								--
            , DT_SHRH.SHRNO AS 支払番号										--
            , DT_SHRB.GYONO AS 行番号										--
            , NULL
            , NULL
            , NULL
            , NULL
            , DK_NYUKIN.NYUKINKBNNM
            , NULL
            , NULL
            , NULL
            , NULL										--
			, NULL
			, DT_SHRB.KING AS 合計
            , DT_SHRH.SHRYMD AS 仕入日付条件								--
            , 2 AS 区分
		FROM  DM_SHIRE,DT_SHRH,DT_SHRB,DK_NYUKIN
		WHERE
					(DT_SHRH.JIGYOCD = DT_SHRB.JIGYOCD
			  AND	DT_SHRH.SHRNO = DT_SHRB.SHRNO
			  AND	DT_SHRB.NYUKINKBN = DK_NYUKIN.NYUKINKBN
			  		-- 仕入先マスタ
			  AND	DT_SHRH.SIRCD = DM_SHIRE.SIRCD
			  AND	DT_SHRH.GETFLG <> '1'
			  AND	DT_SHRB.NYUKINKBN <> '02')
			  OR 	(DT_SHRB.NYUKINKBN = '02' 
			  AND 	DT_SHRB.KAMOKUKBN = '1'
			  AND	DT_SHRH.JIGYOCD = DT_SHRB.JIGYOCD
			  AND	DT_SHRH.SHRNO = DT_SHRB.SHRNO
			  AND	DT_SHRB.NYUKINKBN = DK_NYUKIN.NYUKINKBN
			  		-- 仕入先マスタ
			  AND	DT_SHRH.SIRCD = DM_SHIRE.SIRCD
			  AND	DT_SHRH.GETFLG <> '1')
) WK,DM_KANRI
	WHERE 	
		DM_KANRI.KANRINO = '1'
		ORDER BY
				  WK.仕入先カナ
				, WK.仕入先CD
				, WK.日付
				, WK.仕入事業所CD
				, WK.仕入番号
				, WK.行番号
;
