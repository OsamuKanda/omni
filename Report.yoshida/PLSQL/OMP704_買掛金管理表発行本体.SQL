CREATE OR REPLACE PACKAGE BODY POMP704 AS
	-------------------------------------------------------------------------------
	-- 【関数名】
	--			買掛金管理表発行　
	-- 【内容】 仕入明細・支払明細より該当データを抽出しマスタへサマリ後、印刷
	--
	--
	-- 【引数】
	--
	-- 【戻り値】
	--
	-- 【変更履歴】
	--  HIS-124 取引区分に「でんさい」と「期日払い」を追加 2014/4/30 Kawahata
	-------------------------------------------------------------------------------
	PROCEDURE	 OMP704
	(
			I_事業所CD					IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_更新プログラム			IN		DT_BUKKEN.UDTPG1%TYPE
		,	I_担当者CD					IN		DT_BUKKEN.TANTCD%TYPE
	)
	
	IS

--ワーク項目囲定義

----
	BEGIN
-- 仕入先マスタクリアー
--	FOR R_仕入 IN (
--			SELECT DM_SHIRE.SIRCD FROM DM_SHIRE WHERE DELKBN = '0'
--	)LOOP
			UPDATE DM_SHIRE SET
					TSIRKIN				=	0
			,		TTAX				=	0
			,		TSIRHENKIN			=	0
			,		TSIRNEBIKI			=	0
			,		TSHRGENKIN			=	0
			,		TSHRTEGATA			=	0
			,		TSHRNEBIKI			=	0
			,		TSHRSOSAI			=	0
			,		TSHRSONOTA			=	0
			,		TSHRANZENKAIHI		=	0
			,		TSHRFURIKOMITESU	=	0
--HIS-124 取引区分の追加 Start
			,		TSHRDENSAI			=	0
			,		TSHRKIJITSU			=	0 
--HIS-124 取引区分の追加 END
--			WHERE DM_SHIRE.SIRCD 		=	R_仕入.SIRCD
			;
--	END LOOP;
-- 仕入データを抽出し仕入金額、消費税額をサマリしマスタへ更新。
	FOR R_仕入集計 IN (
			SELECT	DT_SHIREH.SIRCD
			,		SUM(DT_SHIREM.SIRKIN) AS 仕入金額
			,		SUM(DT_SHIREM.TAX)		AS TAX
			FROM	DT_SHIREH
			,		DT_SHIREM
			,		DM_KANRI
			WHERE	DT_SHIREH.SIRJIGYOCD		=		DT_SHIREM.SIRJIGYOCD
			AND		DT_SHIREH.SIRNO				=		DT_SHIREM.SIRNO
            -- 管理マスタ
            AND		DM_KANRI.KANRINO 			= 		'1'
			-- 各無効区分
			AND		DT_SHIREH.DELKBN			=		'0'
			AND		DT_SHIREM.DELKBN			=		'0'
			-- 月次未更新
			AND		DT_SHIREH.GETFLG			<>		'1'
			AND		DT_SHIREH.SIRYMD			<=		DM_KANRI.MONYMD

			GROUP BY DT_SHIREH.SIRCD
	)LOOP

			UPDATE DM_SHIRE SET
					TSIRKIN				=	R_仕入集計.仕入金額
			,		TTAX				=	R_仕入集計.TAX
			,		TSIRHENKIN			=	0
			,		TSIRNEBIKI			=	0
			WHERE		DM_SHIRE.SIRCD		=	R_仕入集計.SIRCD
			;
--
	END LOOP;	

-- 支払データを抽出し入金区分毎に金額をサマリしマスタへ更新。
-- また支払手形'02'の場合は科目コード'1'買掛金のみ抽出。
	FOR R_支払集計 IN (
			SELECT	DT_SHRH.SIRCD
			,		SUM(DECODE(DT_SHRB.NYUKINKBN ,01, DT_SHRB.KING,0)) AS 現金
			,		CASE WHEN DT_SHRB.KAMOKUKBN = '1'  THEN
						SUM(DECODE(DT_SHRB.NYUKINKBN ,02, DT_SHRB.KING,0)) ELSE 0 END AS 支払手形
			,		SUM(DECODE(DT_SHRB.NYUKINKBN ,03, DT_SHRB.KING,0)) AS 値引
			,		SUM(DECODE(DT_SHRB.NYUKINKBN ,04, DT_SHRB.KING,0)) AS 相殺
			,		SUM(DECODE(DT_SHRB.NYUKINKBN ,11, DT_SHRB.KING,0)) AS その他
			,		SUM(DECODE(DT_SHRB.NYUKINKBN ,12, DT_SHRB.KING,0)) AS 安全協力会費
			,		SUM(DECODE(DT_SHRB.NYUKINKBN ,06, DT_SHRB.KING,0)) AS 振込手数料
--HIS-124 取引区分の追加 Start
			--,		SUM(DECODE(DT_SHRB.NYUKINKBN ,13, DT_SHRB.KING,0)) AS でんさい
			--,		SUM(DECODE(DT_SHRB.NYUKINKBN ,14, DT_SHRB.KING,0)) AS 期日払い
			,		CASE WHEN DT_SHRB.KAMOKUKBN = '1'  THEN
						SUM(DECODE(DT_SHRB.NYUKINKBN ,13, DT_SHRB.KING,0)) ELSE 0 END AS でんさい
			,		CASE WHEN DT_SHRB.KAMOKUKBN = '1'  THEN
						SUM(DECODE(DT_SHRB.NYUKINKBN ,14, DT_SHRB.KING,0)) ELSE 0 END AS 期日払
--HIS-124 取引区分の追加 END
			FROM	DT_SHRH
			,		DT_SHRB
			,		DM_KANRI
			WHERE	DT_SHRH.JIGYOCD				=		DT_SHRB.JIGYOCD
			AND		DT_SHRH.SHRNO				=		DT_SHRB.SHRNO
            -- 管理マスタ
            AND		DM_KANRI.KANRINO 			= 		'1'
			-- 各無効区分
			AND		DT_SHRH.DELKBN				=		'0'
			AND		DT_SHRB.DELKBN				=		'0'
			-- 月次未更新
			AND		DT_SHRH.GETFLG				<>		'1'
			AND		DT_SHRH.SHRYMD				<=		DM_KANRI.MONYMD


			GROUP BY DT_SHRH.SIRCD,DT_SHRB.KAMOKUKBN
	)LOOP

			UPDATE DM_SHIRE SET
					TSHRGENKIN			=	TSHRGENKIN + R_支払集計.現金
			,		TSHRTEGATA			=	TSHRTEGATA + R_支払集計.支払手形
			,		TSHRNEBIKI			=	TSHRNEBIKI + R_支払集計.値引
			,		TSHRSOSAI			=	TSHRSOSAI + R_支払集計.相殺
			,		TSHRSONOTA			=	TSHRSONOTA + R_支払集計.その他
			,		TSHRANZENKAIHI		=	TSHRANZENKAIHI + R_支払集計.安全協力会費
			,		TSHRFURIKOMITESU	=	TSHRFURIKOMITESU + R_支払集計.振込手数料
--HIS-124 取引区分の追加 Start
			,		TSHRDENSAI			=	TSHRDENSAI + R_支払集計.でんさい
			,		TSHRKIJITSU			=	TSHRKIJITSU + R_支払集計.期日払
--HIS-124 取引区分の追加 END
			WHERE		DM_SHIRE.SIRCD		=	R_支払集計.SIRCD
			;
--
	END LOOP;	
	
-- 管理マスタ出力済フラグ更新
			UPDATE DM_KANRI SET
					PRINTKBN			=	'1'
			WHERE		KANRINO				=	'1'
			;

	END OMP704;
END POMP704;
/
SHOW ERRORS