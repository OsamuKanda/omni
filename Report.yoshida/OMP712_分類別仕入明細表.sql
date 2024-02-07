-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/31   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP712 分類別仕入明細表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP712
AS
		SELECT  
              ROWNUM  AS EDANUM
            , WK_PRT712.JIGYOCD AS 事業所CD								--事業所コード
            , DECODE(WK_PRT712.JIGYOCD,'91','経費',DECODE(WK_PRT712.JIGYOCD,'90','在庫',DM_JIGYO.JIGYONM)) AS 事業所名									--事業所名
            , WK_PRT712.BUNRUIDCD AS 大分類CD								--大分類コード
            , DM_BUNRUID.BUNRUIDNM AS 大分類名								--大分類名
            , DECODE(WK_PRT712.JIGYOCD,'90',NULL,WK_PRT712.BUNRUICCD) AS 中分類CD								--中分類コード
			, DM_BUNRUIC.BUNRUICNM AS 中分類名								--中分類名
			, DECODE(WK_PRT712.BUNRUIDCD,NULL,NULL,WK_PRT712.BUNRUIDCD || '-' || WK_PRT712.BUNRUICCD) AS 分類CD		--分類コード
			, WK_PRT712.GAICHUKBN AS 外注区分								--外注区分
			, CASE WHEN 
					WK_PRT712.GAICHUKBN <= '1' THEN 
						DK_GAICHU.GAICHUKBNNM || '仕入' 
				  ELSE 
				  		DK_GAICHU.GAICHUKBNNM END AS 外注区分名							--外注区分名
			, DT_SHIREM.BUMONCD AS 部門CD									--部門コード
			, DM_BUMON.BUMONNM AS 部門名									--部門名
			, DECODE(WK_PRT712.URIAGEKBN,'既',0,DECODE(WK_PRT712.URIAGEKBN,'当',1,2)) AS 売上
			, WK_PRT712.URIAGEKBN AS 売上区分								--
			, DM_NONYU.NONYUNMR AS 納入先略称								--
			, DT_SHIREM.JIGYOCD || DT_SHIREM.SAGYOBKBN || DT_SHIREM.RENNO AS 物件番号		--
			, DT_SHIREH.SIRCD AS 仕入先CD									--仕入先コード
			, DM_SHIRE.SIRNMR AS 仕入先略称									--仕入先略称
			, SUBSTR(日付記号追加(DT_SHIREH.SIRYMD),6,5) AS 仕入日			--
			, DT_SHIREM.BKIKAKUNM AS 規格名									--
			, DT_SHIREM.SIRSU AS 数量										--
			, DT_SHIREM.SIRTANK AS 仕入単価									--
			, DT_SHIREM.SIRKIN AS 金額										--
			, DT_SHIREM.SIRNO AS 仕入番号									--
			, DT_SHIREM.GYONO AS 行番号
			, LOGINID AS ﾛｸﾞｲﾝID											--ﾛｸﾞｲﾝID
		FROM  WK_PRT712,DM_JIGYO,DK_GAICHU,DT_SHIREH,DM_BUNRUID,DM_BUNRUIC
			  ,DT_SHIREM,DM_BUMON,DT_BUKKEN,DM_NONYU,DM_SHIRE
		WHERE
              		WK_PRT712.PROGID = 'OMP712'
			  		-- 事業所マスタ
			  AND	WK_PRT712.JIGYOCD = DM_JIGYO.JIGYOCD(+)
			  		-- 大分類マスタ
			  AND	WK_PRT712.BUNRUIDCD = DM_BUNRUID.BUNRUIDCD
			  		-- 中分類マスタ
			  AND	WK_PRT712.BUNRUICCD = DM_BUNRUIC.BUNRUICCD
			  		-- 外注区分マスタ
			  AND	WK_PRT712.GAICHUKBN = DK_GAICHU.GAICHUKBN
			  		-- 仕入ヘッダー
			  AND	WK_PRT712.SIRJIGYOCD = DT_SHIREH.SIRJIGYOCD
			  AND	WK_PRT712.SIRNO = DT_SHIREH.SIRNO
			  		-- 仕入明細
			  AND	WK_PRT712.SIRJIGYOCD = DT_SHIREM.SIRJIGYOCD
			  AND	WK_PRT712.SIRNO = DT_SHIREM.SIRNO
			  AND	WK_PRT712.SIRGYONO = DT_SHIREM.GYONO
			  		-- 部門マスタ
			  AND	DT_SHIREM.BUMONCD = DM_BUMON.BUMONCD(+)
			  		-- 物件ファイル
			  AND	DT_SHIREM.JIGYOCD = DT_BUKKEN.JIGYOCD(+)
			  AND	DT_SHIREM.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN(+)
			  AND	DT_SHIREM.RENNO = DT_BUKKEN.RENNO(+)
			  		-- 納入先マスタ
			  AND	DT_BUKKEN.NONYUCD = DM_NONYU.NONYUCD(+)
			  AND	'01' = DM_NONYU.SECCHIKBN(+)
			  AND	DT_BUKKEN.JIGYOCD = DM_NONYU.JIGYOCD(+)
			  		-- 仕入先マスタ
			  AND	DT_SHIREH.SIRCD = DM_SHIRE.SIRCD
		ORDER BY
				WK_PRT712.JIGYOCD,WK_PRT712.BUNRUIDCD,WK_PRT712.BUNRUICCD,WK_PRT712.GAICHUKBN
				,DT_SHIREM.JIGYOCD || DT_SHIREM.SAGYOBKBN || DT_SHIREM.RENNO
				,DT_SHIREH.SIRYMD
;
