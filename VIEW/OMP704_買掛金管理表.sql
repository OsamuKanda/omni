-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
--  HIS-124 取引区分に「でんさい」と「期日払」を追加 2014/4/30 Kawahata
-------------------------------------------------------------------------------
--OMP704	買掛金管理表
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP704
AS
		SELECT  
              ROWNUM  AS EDANUM
			, SUBSTR(DM_KANRI.MONYMD,1,4) || '年' ||  SUBSTR(DM_KANRI.MONYMD,5,2) || '月度' AS 月次締年月			--月次締年月
			, DM_SHIRE.SIRCD AS 仕入先CD																			--仕入先コード
			, DM_SHIRE.SIRNM1 AS 仕入先名1																			--仕入先名1
			, DM_SHIRE.SIRNM2 AS 仕入先名2																			--仕入先名2
			, DM_SHIRE.SIRNMR AS 仕入略称																			--仕入略称
			, DM_SHIRE.SIRNMX AS 仕入先カナ																			--仕入先カナ
			, DM_SHIRE.ZENZAN AS 前月残高																			--前月残高
			, DM_SHIRE.TSHRGENKIN AS 現金																			--現金
			, DM_SHIRE.TSHRTEGATA AS 支払手形																		--支払手形
			, (DM_SHIRE.TSHRNEBIKI + DM_SHIRE.TSHRSOSAI + DM_SHIRE.TSHRSONOTA) AS その他							--相殺・その他
			, DM_SHIRE.TSHRANZENKAIHI AS 安全協力会費																--安全協力会費
-->>HIS-124 取引区分の追加 Start
			, DM_SHIRE.TSHRDENSAI AS でんさい																		--でんさい
			, DM_SHIRE.TSHRKIJITSU AS 期日払																		--期日払
--<<HIS-124 取引区分の追加 End
			, DM_SHIRE.TSHRFURIKOMITESU AS 振込手数料																--振込手数料
-->>HIS-124 取引区分の追加 Mod Start
		--	, (DM_SHIRE.ZENZAN - DM_SHIRE.TSHRGENKIN - DM_SHIRE.TSHRTEGATA - DM_SHIRE.TSHRNEBIKI - 
		--	   DM_SHIRE.TSHRSOSAI - DM_SHIRE.TSHRSONOTA - DM_SHIRE.TSHRANZENKAIHI - DM_SHIRE.TSHRFURIKOMITESU ) AS 当月繰越		--当月繰越
			, (DM_SHIRE.ZENZAN - DM_SHIRE.TSHRGENKIN - DM_SHIRE.TSHRTEGATA - DM_SHIRE.TSHRNEBIKI - 
			   DM_SHIRE.TSHRSOSAI - DM_SHIRE.TSHRSONOTA - DM_SHIRE.TSHRANZENKAIHI - DM_SHIRE.TSHRFURIKOMITESU - DM_SHIRE.TSHRDENSAI - DM_SHIRE.TSHRKIJITSU ) AS 当月繰越		--当月繰越
			, (DM_SHIRE.TSIRKIN - DM_SHIRE.TSIRHENKIN - DM_SHIRE.TSIRNEBIKI) AS 当月仕入							--当月仕入
--<<HIS-124 取引区分の追加 Mod End
			, DM_SHIRE.TTAX AS 消費税																				--消費税
-->>HIS-124 取引区分の追加 Mod Start
		--	, (DM_SHIRE.ZENZAN - DM_SHIRE.TSHRGENKIN - DM_SHIRE.TSHRTEGATA - DM_SHIRE.TSHRNEBIKI - 
		--	   DM_SHIRE.TSHRSOSAI - DM_SHIRE.TSHRSONOTA - DM_SHIRE.TSHRANZENKAIHI - DM_SHIRE.TSHRFURIKOMITESU )
		--	  + (DM_SHIRE.TSIRKIN - TSIRHENKIN - TSIRNEBIKI) + DM_SHIRE.TTAX AS 当月末残							--当月末残
			, (DM_SHIRE.ZENZAN - DM_SHIRE.TSHRGENKIN - DM_SHIRE.TSHRTEGATA - DM_SHIRE.TSHRNEBIKI - 
			   DM_SHIRE.TSHRSOSAI - DM_SHIRE.TSHRSONOTA - DM_SHIRE.TSHRANZENKAIHI - DM_SHIRE.TSHRFURIKOMITESU - DM_SHIRE.TSHRDENSAI - DM_SHIRE.TSHRKIJITSU )
			  + (DM_SHIRE.TSIRKIN - TSIRHENKIN - TSIRNEBIKI) + DM_SHIRE.TTAX AS 当月末残							--当月末残
--<<HIS-124 取引区分の追加 Mod End
		FROM DM_SHIRE,DM_KANRI
			WHERE
              		-- 管理マスタ
              		DM_KANRI.KANRINO = '1'
              AND	(DM_SHIRE.ZENZAN <> 0 OR DM_SHIRE.TSHRGENKIN <> 0 OR DM_SHIRE.TSHRNEBIKI <> 0 OR
              		 DM_SHIRE.TSHRSOSAI <> 0 OR DM_SHIRE.TSHRSONOTA <> 0 OR DM_SHIRE.TSHRANZENKAIHI <> 0 OR
              		 DM_SHIRE.TSHRFURIKOMITESU <> 0 OR DM_SHIRE.TSIRKIN <> 0 OR DM_SHIRE.TSIRHENKIN <> 0 OR
--HIS-124 取引区分の追加 Start
              		 DM_SHIRE.TSHRDENSAI <> 0 OR DM_SHIRE.TSHRKIJITSU <> 0 OR
--HIS-124 取引区分の追加 End
              		 DM_SHIRE.TSIRNEBIKI <> 0 OR DM_SHIRE.TTAX <> 0)
					-- 仕入先マスタ.無効区分
			  AND	DM_SHIRE.DELKBN = '0'
        ORDER BY
        			  DM_SHIRE.SIRNMX,DM_SHIRE.SIRCD
;

