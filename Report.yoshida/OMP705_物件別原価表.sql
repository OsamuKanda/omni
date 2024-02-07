-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/10   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP705	物件別原価表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP705
AS
			SELECT 
				 SUBSTR(DM_KANRI.MONYMD,1,4) || '年' ||  SUBSTR(DM_KANRI.MONYMD,5,2) || '月度' AS 月次締年月
			    ,D1.JIGYOCD AS 事業所CD
			    ,DM_JIGYO.JIGYONM AS 事業所名
			    ,D1.SHIKAKARIKBN AS 仕掛区分
			    ,DECODE(D1.SHIKAKARIKBN,1,'仕　掛　',2,'当月売上','既売上') AS 仕掛区分名
			    ,D1.SAGYOBKBN AS 作業分類
			    ,DK_SAGYO.SAGYOKBNNM AS 大分類名
			    ,D1.RENNO AS 連番
			    ,D1.物件番号
			    ,DT_BUKKEN.NONYUCD AS 納入先CD
			    ,DM_NONYU.NONYUNM1 AS 納入先名
			    ,DM_NONYU.SENBUSHONM AS 部署名
			    ,DT_BUKKEN.BIKO AS 備考
			    ,D1.GAICHUKBN AS 外注区分
			    ,D1.ZENIZENKING AS 前月以前
			    ,D1.ZENKING AS 前月金額
			    ,D1.TOUKING AS 当月金額
			    ,D1.ZENIZENKING + D1.ZENKING + D1.TOUKING AS 合計
			    ,D1.URIKING AS 売上金額
			    ,D2.GAICHUKBN AS 外注区分1
			    ,D2.ZENIZENKING AS 前月以前1
			    ,D2.ZENKING AS 前月金額1
			    ,D2.TOUKING AS 当月金額1
			    ,D2.ZENIZENKING + D2.ZENKING + D2.TOUKING AS 合計1
			    ,D3.GAICHUKBN AS 外注区分2
			    ,D3.ZENIZENKING AS 前月以前2
			    ,D3.ZENKING AS 前月金額2
			    ,D3.TOUKING AS 当月金額2
			    ,D3.ZENIZENKING + D3.ZENKING + D3.TOUKING AS 合計2
			    ,( D1.ZENIZENKING + D1.ZENKING + D1.TOUKING + D2.ZENIZENKING + D2.ZENKING + D2.TOUKING + D3.ZENIZENKING + D3.ZENKING + D3.TOUKING ) AS 仕入合計
			    ,D1.LOGINID
			FROM (
							SELECT  
								  WK_PRT705.JIGYOCD 																			--事業所コード
								, WK_PRT705.SHIKAKARIKBN 
					            , WK_PRT705.SAGYOBKBN
					            , WK_PRT705.RENNO 
								, WK_PRT705.JIGYOCD || '-' || WK_PRT705.SAGYOBKBN || '-' || WK_PRT705.RENNO AS 物件番号					--物件番号
								, WK_PRT705.GAICHUKBN
								, WK_PRT705.ZENIZENKING 
								, WK_PRT705.ZENKING
								, WK_PRT705.TOUKING
								, WK_PRT705.URIKING
								, WK_PRT705.LOGINID
							FROM WK_PRT705
								WHERE 	WK_PRT705.PROGID = 'OMP705'
					              AND	WK_PRT705.GAICHUKBN = '0' ) D1,
					(
							SELECT  
								  WK_PRT705.JIGYOCD 																			--事業所コード
								, WK_PRT705.SHIKAKARIKBN 
					            , WK_PRT705.SAGYOBKBN
					            , WK_PRT705.RENNO 
								, WK_PRT705.JIGYOCD || '-' || WK_PRT705.SAGYOBKBN || '-' || WK_PRT705.RENNO AS 物件番号					--物件番号
								, WK_PRT705.GAICHUKBN
								, WK_PRT705.ZENIZENKING 
								, WK_PRT705.ZENKING
								, WK_PRT705.TOUKING
								, WK_PRT705.LOGINID
							FROM WK_PRT705
								WHERE 	WK_PRT705.PROGID = 'OMP705'
					              AND	WK_PRT705.GAICHUKBN = '1' ) D2,
					(
							SELECT  
								  WK_PRT705.JIGYOCD 																			--事業所コード
								, WK_PRT705.SHIKAKARIKBN 
					            , WK_PRT705.SAGYOBKBN
					            , WK_PRT705.RENNO 
								, WK_PRT705.JIGYOCD || '-' || WK_PRT705.SAGYOBKBN || '-' || WK_PRT705.RENNO AS 物件番号					--物件番号
								, WK_PRT705.GAICHUKBN
								, WK_PRT705.ZENIZENKING 
								, WK_PRT705.ZENKING
								, WK_PRT705.TOUKING
								, WK_PRT705.LOGINID
							FROM WK_PRT705
								WHERE 	WK_PRT705.PROGID = 'OMP705'
					              AND	WK_PRT705.GAICHUKBN = '2' ) D3
              , DM_KANRI,DM_JIGYO,DK_SAGYO,DM_NONYU,DT_BUKKEN
				WHERE D1.物件番号 = D2.物件番号 
				AND D1.物件番号 = D3.物件番号
				AND D1.LOGINID = D2.LOGINID
				AND D1.LOGINID = D3.LOGINID
				AND DM_KANRI.KANRINO = '1'
						-- 事業所マスタ
				AND	D1.JIGYOCD = DM_JIGYO.JIGYOCD
						-- 大分類区分マスタ
				AND	D1.SAGYOBKBN = DK_SAGYO.SAGYOKBN
						-- 物件ファイル
				AND	D1.JIGYOCD = DT_BUKKEN.JIGYOCD
				AND	D1.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN
				AND	D1.RENNO = DT_BUKKEN.RENNO
						-- 納入先マスタ
--				AND	D1.JIGYOCD = DM_NONYU.JIGYOCD(+)
				AND	DT_BUKKEN.NONYUCD = DM_NONYU.NONYUCD(+)
				AND	'01' = DM_NONYU.SECCHIKBN(+)
;

