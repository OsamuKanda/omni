-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/10   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP710	物件別原価累積明細表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP710
AS
			SELECT 
				  --前受区分の場合は、完了日。それ以外は、最新請求日を抽出条件で使用する。
				  SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) AS 日付比較
				, DT_SHIREH.SIRYMD AS 明細日付比較用
				, DT_BUKKEN.JIGYOCD AS 事業所CD
				, DT_BUKKEN.SAGYOBKBN AS 作業分類区分
				, DT_BUKKEN.RENNO AS 連番
				, DT_BUKKEN.JIGYOCD || '-' || DT_BUKKEN.SAGYOBKBN || '-' || DT_BUKKEN.RENNO AS 物件番号
			    , DT_BUKKEN.NONYUCD AS 納入先CD
			    , DM_NONYU.NONYUNM1 AS 納入先名
				, DT_BUKKEN.BUNRUIDCD AS 大分類CD
				, DM_BUNRUID.BUNRUIDNM AS 大分類名
				, DT_BUKKEN.BUNRUICCD AS 中分類CD
				, DM_BUNRUIC.BUNRUICNM AS 中分類名
				, 日付記号追加(DT_BUKKEN.KANRYOYMD) AS 完了日
				, DT_BUKKEN.SOUKINGR AS 売上金額
				--外注区分 <= '2' の場合は仕入先コードは非表示
				--0:部品 1:外注 2:諸経費 3:在庫 4:工賃
				,CASE 
					WHEN DM_BKIKAKU.GAICHUKBN <= '2' THEN	DT_SHIREH.SIRCD
					ELSE NULL END AS 仕入先CD
				,CASE 
					WHEN DM_BKIKAKU.GAICHUKBN <= '2' THEN	DM_SHIRE.SIRNM1
					WHEN DM_BKIKAKU.GAICHUKBN = '3' THEN	'在庫使用'
					ELSE '工　　賃' END AS 仕入先名1
				,CASE 
					WHEN DM_BKIKAKU.GAICHUKBN <= '2' THEN	DM_SHIRE.SIRNM2
					ELSE NULL END AS 仕入先名2
				,CASE 
					WHEN DM_BKIKAKU.GAICHUKBN <= '2' THEN	DM_SHIRE.SIRNMR
					ELSE NULL END AS 仕入先名略称
				, 日付記号追加(DT_SHIREH.SIRYMD) AS 仕入日付
				, DT_SHIREM.SIRNO AS 仕入番号
				, DT_SHIREM.GYONO AS 行番号
				, DT_SHIREM.BBUNRUICD AS 部品分類CD
				, DT_SHIREM.BBUNRUINM AS 部品分類名
				, DT_SHIREM.BKIKAKUCD AS 部品規格CD
				, DT_SHIREM.BKIKAKUNM AS 部品規格名
				, DM_BKIKAKU.GAICHUKBN AS 外注区分
				, DT_SHIREM.SIRSU AS 数量
				, DM_TANI.TANINM AS 単位名
				, DT_SHIREM.SIRTANK AS 単価
				, DT_SHIREM.SIRKIN AS 金額
				, DT_SHIREM.TAX AS 消費税
			FROM DT_SHIREH,DT_SHIREM,DT_BUKKEN,DM_TANI,DM_NONYU,DM_BKIKAKU,DM_BUNRUID,DM_BUNRUIC,DM_SHIRE
			WHERE
						-- 納入先マスタ
--2012.10.11-----------------------------------------------
--					DT_BUKKEN.JIGYOCD = DM_NONYU.JIGYOCD
					DT_BUKKEN.NONYUCD = DM_NONYU.NONYUCD
				AND	'01' = DM_NONYU.SECCHIKBN
						-- 大分類マスタ
				AND	DT_BUKKEN.BUNRUIDCD = DM_BUNRUID.BUNRUIDCD
						-- 中分類マスタ
				AND	DT_BUKKEN.BUNRUICCD = DM_BUNRUIC.BUNRUICCD
						-- 物件ファイル
				AND	DT_SHIREM.JIGYOCD = DT_BUKKEN.JIGYOCD
				AND	DT_SHIREM.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN
				AND	DT_SHIREM.RENNO = DT_BUKKEN.RENNO
						-- 仕入明細
				AND	DT_SHIREH.SIRJIGYOCD = DT_SHIREM.SIRJIGYOCD
				AND	DT_SHIREH.SIRNO = DT_SHIREM.SIRNO
						-- 仕入先マスタ
				AND	DT_SHIREH.SIRCD = DM_SHIRE.SIRCD(+)
						-- 部品規格マスタ
				AND	DT_SHIREM.BBUNRUICD = DM_BKIKAKU.BBUNRUICD(+)
				AND	DT_SHIREM.BKIKAKUCD = DM_BKIKAKU.BKIKAKUCD(+)
						-- 単位マスタ
				AND	DT_SHIREM.TANICD = DM_TANI.TANICD(+)
				AND	DT_SHIREM.DELKBN = '0'
				AND DT_SHIREH.DELKBN = '0'
				AND	DT_BUKKEN.DELKBN = '0'
;

