-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/09   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP702 期日払管理表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP702
AS
		SELECT   
              ROWNUM  AS EDANUM
			, WK_PRT701.JIGYOCD AS 事業所CD													--事業所コード
			, DM_JIGYO.JIGYONM AS 事業所名													--事業所名
			, SUBSTR(日付記号追加(DT_URIAGEH.SEIKYUYMD),3,8) AS 請求日						--請求日
			, WK_PRT701.JIGYOCD || WK_PRT701.SAGYOBKBN || WK_PRT701.RENNO AS 物件番号		--物件番号
			, WK_PRT701.SEIKYUSHONO AS 請求書番号											--請求書番号
			, DT_URIAGEH.SEIKYUCD AS 請求先CD												--請求先コード
			, SUBSTR(DT_URIAGEH.SEIKYUNM,1,15) AS 請求先名									--請求先名
			, SUBSTR(DT_URIAGEH.NONYUNM,1,15) AS 納入先名									--納入先名
				-- 前月分データの場合、売上 + 消費税 - 指定年月以前の入金額。以外は'0'
			, DECODE(WK_PRT701.OUTKBN,1,WK_PRT701.KING + WK_PRT701.TAX - WK_PRT701.ZENNYUKIN,0) AS 前月繰越
			  	-- 前月分データの場合、'0'。以外は売上 - 指定年月以前の入金額
			, DECODE(WK_PRT701.OUTKBN,1,0,WK_PRT701.KING  - WK_PRT701.ZENNYUKIN) AS 売上
			  	-- 前月分データの場合、'0'。以外は消費税
			, DECODE(WK_PRT701.OUTKBN,1,0,WK_PRT701.TAX) AS 消費税
				-- 売上 + 消費税 - 指定年月以前の入金額
			, WK_PRT701.KING + WK_PRT701.TAX - WK_PRT701.ZENNYUKIN AS 請求額
			, WK_PRT701.NYUKINYMD AS 入金日
			, WK_PRT701.GENKIN AS 現金														--現金
			, WK_PRT701.NEBIKI AS 値引														--値引
			, WK_PRT701.TEGATA AS 手形														--手形
			, WK_PRT701.YUSODAI AS 手形郵送代												--手形郵送代
			, WK_PRT701.URIKAKESAIKEN AS 売掛債権											--売掛債権
			, WK_PRT701.SOUSAI AS 相殺														--相殺
			, WK_PRT701.TESURYO AS 振込手数料												--振込手数料
			, WK_PRT701.KAIHI AS 諸会費														--諸会費
			, WK_PRT701.KINRI AS 金利														--金利・割引
			, WK_PRT701.MAEUKE AS 前受分													--前受分
			, WK_PRT701.KING + WK_PRT701.TAX - WK_PRT701.ZENNYUKIN - (WK_PRT701.GENKIN + WK_PRT701.NEBIKI
			  + WK_PRT701.TEGATA + WK_PRT701.YUSODAI + WK_PRT701.URIKAKESAIKEN + WK_PRT701.SOUSAI
			  + WK_PRT701.TESURYO + WK_PRT701.KAIHI + WK_PRT701.KINRI + WK_PRT701.MAEUKE ) AS 翌月繰越
			, DECODE(DT_URIAGEH.NYUKINYOTEIYMD,NULL,日付記号追加(DT_URIAGEH.KAISHUYOTEIYMD),日付記号追加(DT_URIAGEH.NYUKINYOTEIYMD)) AS 入金予定
			, DECODE(DT_URIAGEH.NYUKINYOTEIYMD,NULL,NULL,'(期日払)') AS 入金区分
			, DECODE(DT_URIAGEH.HOSHUKBN,1,'毎月請求分',NULL) AS 請求区分
			, DECODE(DT_URIAGEH.TAXKBN,1,'非課税',NULL) AS 課税区分
			, DECODE(WK_PRT701.OUTKBN,1,'前月分',2,'当月分',3,'前受分') AS 印字文言
			, WK_PRT701.OUTKBN AS 印字区分
			, WK_PRT701.LOGINID 															--ログインID
			, DM_NONYU.HURIGANA AS フリガナ
			, SUBSTR(DT_URIAGEH.SEIKYUYMD,1,6) AS 請求年月
			, SUBSTR(DT_URIAGEH.SEIKYUYMD,3,2) AS 請求年
		FROM WK_PRT701,DT_URIAGEH,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT701.PROGID = 'OMP702'
--			  AND	WK_PRT701.OUTKBN <= '3'
					-- 売上ヘッダーと結合
			  AND	DT_URIAGEH.SEIKYUSHONO = WK_PRT701.SEIKYUSHONO
			  AND	DT_URIAGEH.JIGYOCD = WK_PRT701.JIGYOCD
			  AND	DT_URIAGEH.SAGYOBKBN = WK_PRT701.SAGYOBKBN
			  AND	DT_URIAGEH.RENNO = WK_PRT701.RENNO
					-- 事業所マスタと結合
			  AND	DM_JIGYO.JIGYOCD = WK_PRT701.JIGYOCD
			  		-- 納入先マスタと結合（請求先）
			  AND	DT_URIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00' = DM_NONYU.SECCHIKBN
        ORDER BY
        			  WK_PRT701.JIGYOCD
        			, SUBSTR(DT_URIAGEH.SEIKYUYMD,1,6)
        			, DM_NONYU.HURIGANA
        			, WK_PRT701.SEIKYUSHONO
;
