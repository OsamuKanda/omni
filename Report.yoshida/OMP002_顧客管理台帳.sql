-------------------------------------------------------------------------------
--IjeNmVXev[X
--                                                 CREATE:2011/10/31   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP002 ÚqÇä 
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP002
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_NONYU.JIGYOCD AS ÆCD									--ÆR[h
            , DM_JIGYO.JIGYONM AS Æ¼									--Æ¼
			, DM_NONYU.KIGYOCD AS éÆCD									--éÆR[h
			, DECODE(DM_KIGYO.KIGYONM,NULL,'éÆ}X^É³µ',DM_KIGYO.KIGYONM) AS éÆ¼									--éÆ¼
			, DECODE(DM_NONYU.SEIKYUSAKICD1,NULL,NULL,'Ìá1') AS Ìá1		--
            , DM_NONYU.SEIKYUSAKICD1 AS ÌáC¿æCD11					--ÌáC¿æPR[hP
            , K1.NONYUNM1 AS ÌáC¿æ¼11								--ÌáC¿æP¼P
			, K1.ZIPCODE AS ÌáC¿æXÖÔ1							--ÌáC¿æPXÖÔP
			, K1.ADD1 AS ÌáC¿æZ11								--ÌáC¿æPZP
			, K1.ADD2 AS ÌáC¿æZ12								--ÌáC¿æPZQ
			, K1.TELNO1 AS ÌáC¿ædbÔ11							--ÌáC¿æPdbÔP
			, K1.TELNO2 AS ÌáC¿ædbÔ12							--ÌáC¿æPdbÔQ
			, DECODE(DM_NONYU.SEIKYUSAKICD2,NULL,NULL,'Ìá2') AS Ìá2		--
            , DM_NONYU.SEIKYUSAKICD2 AS ÌáC¿æCD21					--ÌáC¿æQR[hQ
            , K2.NONYUNM1 AS ÌáC¿æ¼21								--ÌáC¿æQ¼Q
			, K2.ZIPCODE AS ÌáC¿æXÖÔ21						--ÌáC¿æQXÖÔP
			, K2.ADD1 AS ÌáC¿æZ21								--ÌáC¿æQZP
			, K2.ADD2 AS ÌáC¿æZ22								--ÌáC¿æQZQ
			, K2.TELNO1 AS ÌáC¿ædbÔ21							--ÌáC¿æQdbÔP
			, K2.TELNO2 AS ÌáC¿ædbÔ22							--ÌáC¿æQdbÔQ
			, DECODE(DM_NONYU.SEIKYUSAKICD3,NULL,NULL,'Ìá3') AS Ìá3		--
            , DM_NONYU.SEIKYUSAKICD3 AS ÌáC¿æCD31					--ÌáC¿æRR[hR
            , K3.NONYUNM1 AS ÌáC¿æ¼31								--ÌáC¿æR¼R
			, K3.ZIPCODE AS ÌáC¿æXÖÔ31						--ÌáC¿æRXÖÔP
			, K3.ADD1 AS ÌáC¿æZ31								--ÌáC¿æRZP
			, K3.ADD2 AS ÌáC¿æZ32								--ÌáC¿æRZQ
			, K3.TELNO1 AS ÌáC¿ædbÔ31							--ÌáC¿æRdbÔP
			, K3.TELNO2 AS ÌáC¿ædbÔ32							--ÌáC¿æRdbÔQ
			, DECODE(DM_NONYU.SEIKYUSAKICDH,NULL,NULL,'Ûç') AS Ûç		--
            , DM_NONYU.SEIKYUSAKICDH AS Ûç_¿æCD					--Ûç_¿æRR[hR
            , H1.NONYUNM1 AS Ûç_¿æ¼								--Ûç_¿æR¼R
			, H1.ZIPCODE AS Ûç_¿æXÖÔ							--Ûç_¿æRXÖÔP
			, H1.ADD1 AS Ûç_¿æZ1								--Ûç_¿æRZP
			, H1.ADD2 AS Ûç_¿æZ2								--Ûç_¿æRZQ
			, H1.TELNO1 AS Ûç_¿ædbÔ1							--Ûç_¿æRdbÔP
			, H1.TELNO2 AS Ûç_¿ædbÔ2							--Ûç_¿æRdbÔQ
			, DECODE(DM_NONYU.NONYUCD,NULL,NULL,'[ü') AS [ü				--
			, DM_NONYU.NONYUCD AS [üæCD									--[üæR[h
			, DM_NONYU.NONYUNM1 AS [üæ¼									--[üæ¼
			, DM_NONYU.ZIPCODE AS XÖÔ									--XÖÔ
			, DM_NONYU.ADD1 AS Z1										--ZP
			, DM_NONYU.ADD2 AS Z2										--ZQ
			, DM_NONYU.TELNO1 AS dbÔ1									--dbÔP
			, DM_NONYU.TELNO2 AS dbÔ2									--dbÔQ
			, DM_NONYU.MOCHINUSHI AS ¨¿å								--¨¿å
			, DM_KIGYO.BUSHONM AS ¼									--¼
			, DM_KIGYO.HACCHUTANTNM AS ÇSÒ¼							--ÇSÒ¼
			, DM_TANT.TANTNM AS SÒ¼									--SÒ¼
			, DM_HOSHU.GOUKI AS @										--@
			, DM_HOSHU.KISHUKATA AS @í									--@í
			, DM_HOSHU.YOSHIDANO AS V_HÔ								--V_HÔ
			, DECODE(DM_HOSHU.SECCHIYMD,NULL,NULL,SUBSTR(DM_HOSHU.SECCHIYMD,1,4) || '/' || SUBSTR(DM_HOSHU.SECCHIYMD,5,2)) AS ÝuN
			, RTRIM(DECODE(DM_HOSHU.HOSHUM1,0,'','1,') || DECODE(DM_HOSHU.HOSHUM2,0,'','2,') || DECODE(DM_HOSHU.HOSHUM3,0,'','3,') ||
			  DECODE(DM_HOSHU.HOSHUM4,0,'','4,') || DECODE(DM_HOSHU.HOSHUM5,0,'','5,') || DECODE(DM_HOSHU.HOSHUM6,0,'','6,') ||
			  DECODE(DM_HOSHU.HOSHUM7,0,'','7,') || DECODE(DM_HOSHU.HOSHUM8,0,'','8,') || DECODE(DM_HOSHU.HOSHUM9,0,'','9,') ||
			  DECODE(DM_HOSHU.HOSHUM10,0,'','10,') || DECODE(DM_HOSHU.HOSHUM11,0,'','11,') || DECODE(DM_HOSHU.HOSHUM12,0,'','12,'),',') AS ÛçÎ
			, oßN(DM_HOSHU.SECCHIYMD) AS oßN						--
			, útLÇÁ(DM_HOSHU.KEIYAKUYMD) AS _ñNú				--
			, DM_HOSHU.KEIYAKUKING AS _ñàz								--
			, DK_HOSHU.HOSHUKBNNM AS vZû@								--
			, DECODE(DM_HOSHU.KEIYAKUYMD,NULL,'0','1') AS _ñæª			--
			, DM_NONYU.HURIGANA AS tKi
		FROM  DM_NONYU,DM_JIGYO,
			 DM_NONYU K1,DM_NONYU K2,DM_NONYU K3,
			 DM_NONYU H1,DM_KIGYO,DM_HOSHU,DM_TANT,DK_HOSHU
		WHERE
					DM_NONYU.SECCHIKBN = '01'
					-- Æ}X^Æ
              AND	DM_NONYU.JIGYOCD = DM_JIGYO.JIGYOCD
              		-- ÌáC¿æP
              AND	K1.JIGYOCD(+) = DM_NONYU.JIGYOCD
              AND	K1.NONYUCD(+) = DM_NONYU.SEIKYUSAKICD1
              AND	K1.SECCHIKBN(+) = '00'
              		-- ÌáC¿æQ
              AND	K2.JIGYOCD(+) = DM_NONYU.JIGYOCD
              AND	K2.NONYUCD(+) = DM_NONYU.SEIKYUSAKICD2
              AND	K2.SECCHIKBN(+) = '00'              
              		-- ÌáC¿æR
              AND	K3.JIGYOCD(+) = DM_NONYU.JIGYOCD
              AND	K3.NONYUCD(+) = DM_NONYU.SEIKYUSAKICD3
              AND	K3.SECCHIKBN(+) = '00'              
              		-- Ûç_¿æ
              AND	H1.JIGYOCD(+) = DM_NONYU.JIGYOCD
              AND	H1.NONYUCD(+) = DM_NONYU.SEIKYUSAKICDH
              AND	H1.SECCHIKBN(+) = '00'
              		-- éÆ}X^
              AND	DM_KIGYO.KIGYOCD(+) = DM_NONYU.KIGYOCD
              AND	DM_KIGYO.DELKBN(+) = '0'
              		-- SÒ}X^
              AND	DM_TANT.TANTCD(+) = DM_KIGYO.EIGYOTANTCD
              		-- Ûç_}X^
              AND	DM_HOSHU.NONYUCD = DM_NONYU.NONYUCD
              AND	DM_HOSHU.DELKBN	 = '0'
              		-- ÛçvZæª}X^
              AND	DK_HOSHU.HOSHUKBN = DM_HOSHU.HOSHUKBN
		ORDER BY
					  DM_NONYU.JIGYOCD
					, DM_NONYU.NONYUCD
					, DM_HOSHU.GOUKI
;
