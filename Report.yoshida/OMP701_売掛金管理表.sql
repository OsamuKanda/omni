-------------------------------------------------------------------------------
--IjeNmVXev[X
--                                                 CREATE:2011/11/09   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP701 |àÇ\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP701
AS
		SELECT   
              ROWNUM  AS EDANUM
			, WK_PRT701.JIGYOCD AS ÆCD													--ÆR[h
			, DM_JIGYO.JIGYONM AS Æ¼													--Æ¼
			, SUBSTR(útLÇÁ(DT_URIAGEH.SEIKYUYMD),3,8) AS ¿ú						--¿ú
			, WK_PRT701.JIGYOCD || WK_PRT701.SAGYOBKBN || WK_PRT701.RENNO AS ¨Ô		--¨Ô
			, WK_PRT701.SEIKYUSHONO AS ¿Ô											--¿Ô
			, DT_URIAGEH.SEIKYUCD AS ¿æCD												--¿æR[h
			, SUBSTR(DT_URIAGEH.SEIKYUNM,1,15) AS ¿æ¼									--¿æ¼
			, SUBSTR(DT_URIAGEH.NONYUNM,1,15) AS [üæ¼									--[üæ¼
				-- Oªf[^ÌêAã + ÁïÅ - wèNÈOÌüàzBÈOÍ'0'
			, DECODE(WK_PRT701.OUTKBN,1,WK_PRT701.KING + WK_PRT701.TAX - WK_PRT701.ZENNYUKIN,0) AS OJz
			  	-- Oªf[^ÌêA'0'BÈOÍã - wèNÈOÌüàz
			, DECODE(WK_PRT701.OUTKBN,1,0,WK_PRT701.KING  - WK_PRT701.ZENNYUKIN) AS ã
			  	-- Oªf[^ÌêA'0'BÈOÍÁïÅ
			, DECODE(WK_PRT701.OUTKBN,1,0,WK_PRT701.TAX) AS ÁïÅ
				-- ã + ÁïÅ - wèNÈOÌüàz
			, WK_PRT701.KING + WK_PRT701.TAX - WK_PRT701.ZENNYUKIN AS ¿z
			, WK_PRT701.NYUKINYMD AS üàú
			, WK_PRT701.GENKIN AS »à														--»à
			, WK_PRT701.NEBIKI AS lø														--lø
			, WK_PRT701.TEGATA AS è`														--è`
			, WK_PRT701.YUSODAI AS è`Xã												--è`Xã
			, WK_PRT701.URIKAKESAIKEN AS |Â 											--|Â 
			, WK_PRT701.SOUSAI AS E														--E
			, WK_PRT701.TESURYO AS Uè¿												--Uè¿
			, WK_PRT701.KAIHI AS ïï														--ïï
			, WK_PRT701.KINRI AS à														--àEø
			, WK_PRT701.MAEUKE AS Oóª													--Oóª
			, WK_PRT701.KING + WK_PRT701.TAX - WK_PRT701.ZENNYUKIN - (WK_PRT701.GENKIN + WK_PRT701.NEBIKI
			  + WK_PRT701.TEGATA + WK_PRT701.YUSODAI + WK_PRT701.URIKAKESAIKEN + WK_PRT701.SOUSAI
			  + WK_PRT701.TESURYO + WK_PRT701.KAIHI + WK_PRT701.KINRI + WK_PRT701.MAEUKE ) AS Jz
			, DECODE(DT_URIAGEH.NYUKINYOTEIYMD,NULL,útLÇÁ(DT_URIAGEH.KAISHUYOTEIYMD),útLÇÁ(DT_URIAGEH.NYUKINYOTEIYMD)) AS üà\è
			, DECODE(DT_URIAGEH.NYUKINYOTEIYMD,NULL,NULL,'(úú¥)') AS üàæª
			, DECODE(DT_URIAGEH.HOSHUKBN,1,'¿ª',NULL) AS ¿æª
			, DECODE(DT_URIAGEH.TAXKBN,1,'ñÛÅ',NULL) AS ÛÅæª
			, DECODE(WK_PRT701.OUTKBN,1,'Oª',2,'ª',3,'Oóª') AS ó¶¾
			, WK_PRT701.OUTKBN AS óæª
			, WK_PRT701.LOGINID 															--OCID
			, DM_NONYU.HURIGANA AS tKi
			, SUBSTR(DT_URIAGEH.SEIKYUYMD,1,6) AS ¿N
			, SUBSTR(DT_URIAGEH.SEIKYUYMD,3,2) AS ¿N
		FROM WK_PRT701,DT_URIAGEH,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT701.PROGID = 'OMP701'
--			  AND	WK_PRT701.OUTKBN <= '3'
					-- ãwb_[Æ
			  AND	DT_URIAGEH.SEIKYUSHONO = WK_PRT701.SEIKYUSHONO
			  AND	DT_URIAGEH.JIGYOCD = WK_PRT701.JIGYOCD
			  AND	DT_URIAGEH.SAGYOBKBN = WK_PRT701.SAGYOBKBN
			  AND	DT_URIAGEH.RENNO = WK_PRT701.RENNO
					-- Æ}X^Æ
			  AND	DM_JIGYO.JIGYOCD = WK_PRT701.JIGYOCD
			  		-- [üæ}X^Æi¿æj
			  AND	DT_URIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00' = DM_NONYU.SECCHIKBN
        ORDER BY
        			  WK_PRT701.JIGYOCD
        			, SUBSTR(DT_URIAGEH.SEIKYUYMD,1,6)
        			, DM_NONYU.HURIGANA
        			, WK_PRT701.SEIKYUSHONO
;
