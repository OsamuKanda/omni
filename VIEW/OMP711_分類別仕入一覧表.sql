-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/31   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP711 分類別仕入一覧表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP711
AS
		SELECT  
              ROWNUM  AS EDANUM
            , WK_PRT711.EIGCD AS 事業所CD									--事業所コード
            , DECODE(WK_PRT711.EIGCD,'91','経費',DECODE(WK_PRT711.EIGCD,'90','在庫',DM_JIGYO.JIGYONM)) AS 事業所名									--事業所名
            , WK_PRT711.BUNRUIDCD AS 大分類CD								--大分類コード
            , DM_BUNRUID.BUNRUIDNM AS 大分類名								--大分類名
            , DECODE(WK_PRT711.EIGCD,'90',NULL,WK_PRT711.BUNRUICCD) AS 中分類CD								--中分類コード
			, DM_BUNRUIC.BUNRUICNM AS 中分類名								--中分類名
			, DECODE(WK_PRT711.BUNRUIDCD,NULL,NULL,WK_PRT711.BUNRUIDCD || '-' || WK_PRT711.BUNRUICCD) AS 分類CD		--分類コード
			, WK_PRT711.GAICHUKBN AS 外注区分								--外注区分
			, CASE WHEN 
					WK_PRT711.GAICHUKBN <= '1' THEN 
						DK_GAICHU.GAICHUKBNNM || '仕入' 
				  ELSE 
				  		DK_GAICHU.GAICHUKBNNM END AS 外注区分名							--外注区分名
			, WK_PRT711.SUDEURIKIN AS 既売上分仕入							--既売上分仕入
			, WK_PRT711.TOUURIKIN AS 当月売上分仕入							--当月売上分仕入
			, WK_PRT711.MIURIKIN AS 未売上分仕入							--未売上分仕入
			, WK_PRT711.SUDEURIKIN + WK_PRT711.TOUURIKIN + WK_PRT711.MIURIKIN AS 合計
			, LOGINID AS ﾛｸﾞｲﾝID											-- ﾛｸﾞｲﾝID
		FROM  WK_PRT711,DM_JIGYO,DM_BUNRUID,DM_BUNRUIC,DK_GAICHU
		WHERE
              		WK_PRT711.PROGID = 'OMP711'
			  		-- 事業所マスタ
			  AND	WK_PRT711.EIGCD = DM_JIGYO.JIGYOCD(+)
			  		-- 大分類マスタ
			  AND	WK_PRT711.BUNRUIDCD = DM_BUNRUID.BUNRUIDCD(+)
			  		-- 中分類マスタ
			  AND	WK_PRT711.BUNRUICCD = DM_BUNRUIC.BUNRUICCD(+)
			  		-- 外注区分マスタ
			  AND	WK_PRT711.GAICHUKBN = DK_GAICHU.GAICHUKBN
		ORDER BY
				WK_PRT711.EIGCD,WK_PRT711.BUNRUIDCD,WK_PRT711.BUNRUICCD,WK_PRT711.GAICHUKBN
;
