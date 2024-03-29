-------------------------------------------------------------------------------
--IjeNmVXev[X
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP703	|à¢ñûÊê\
-------------------------------------------------------------------------------
CREATE OR REPLACE FORCE VIEW "OMNI"."V_OMP703" ("EDANUM", "ÆCD", "Æ¼", "¿út", "¿æCD", "¿æ¼", "[üæCD", "[üæ¼", "¿Ô", "¨Ô", "¿àz", "Ývüàz", "ñû\èú", "db1", "db2", "¿N", "ðp¿út", "ðpñû\èú") AS 
  SELECT
              ROWNUM  AS EDANUM
			, DT_URIAGEH.JIGYOCD AS ÆCD										--ÆR[h
			, DM_JIGYO.JIGYONM AS Æ¼											--Æ¼
			, útLÇÁ(DT_URIAGEH.SEIKYUYMD) AS ¿út						--¿út
			, DT_URIAGEH.SEIKYUCD AS ¿æCD										--¿æR[h
			, DT_URIAGEH.SEIKYUNM AS ¿æ¼										--¿æ¼
			, DT_URIAGEH.NONYUCD AS [üæCD										--[üæR[h
			, DT_URIAGEH.NONYUNM AS [üæ¼										--[üæ¼
			, DT_URIAGEH.SEIKYUSHONO AS ¿Ô									--¿Ô
			, DT_URIAGEH.JIGYOCD || '-' || DT_URIAGEH.SAGYOBKBN || '-' || DT_URIAGEH.RENNO AS ¨Ô
			, T1.¿àz															--¿àz
			, DT_URIAGEH.NYUKINR AS Ývüàz										--Ývüàz
			, útLÇÁ(DT_URIAGEH.KAISHUYOTEIYMD) AS ñû\èú					--ñû\èú
			, DM_NONYU.TELNO1 AS db1												--db1
			, DM_NONYU.TELNO2 AS db2												--db2
			, SUBSTR(DT_URIAGEH.SEIKYUYMD,1,6) AS ¿N							--
			, DECODE(DT_URIAGEH.SEIKYUYMD,NULL,'00000000',DT_URIAGEH.SEIKYUYMD) AS ðp¿út
			, DECODE(DT_URIAGEH.KAISHUYOTEIYMD,NULL,'00000000',DT_URIAGEH.KAISHUYOTEIYMD)  AS ðpñû\èú
		FROM
				-- ã¾×©ç¿ÔÌãàzðZo
			--«2023.09.18 Update Kanda 2023/10/01È~ÌÁïÅvZÉÎj
			--(	SELECT DT_URIAGEH.SEIKYUSHONO,SUM(KING + TAX) AS ¿àz FROM DT_URIAGEM,DT_URIAGEH
			--	WHERE
			--		DT_URIAGEM.DELKBN = '0'
			--	AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM. SEIKYUSHONO
			--	AND DT_URIAGEH.DELKBN = '0'
			--	AND DT_URIAGEH.DENPYOKBN = '0'
			--	GROUP BY
			--	DT_URIAGEH.SEIKYUSHONO	)
				--ª2023.09.18 Update Kanda 2023/10/01È~ÌÁïÅvZÉÎj
			(	SELECT
						DT_URIAGEH.SEIKYUSHONO
					,	SUM(DT_URIAGEM.KING) + (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END)  AS ¿àz
				FROM
					DT_URIAGEM,DT_URIAGEH
				WHERE
						DT_URIAGEM.DELKBN = '0'
					AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM. SEIKYUSHONO
					AND DT_URIAGEH.DELKBN = '0'
					AND DT_URIAGEH.DENPYOKBN = '0'
				GROUP BY
					DT_URIAGEH.SEIKYUSHONO,DT_URIAGEH.TAXKBN,DT_URIAGEH.SEIKYUYMD
				--ª2023.09.18 Update Kanda 2023/10/01È~ÌÁïÅvZÉÎj
			)
			T1,
			DT_URIAGEH,DM_NONYU,DM_JIGYO
		WHERE
				--   ãLÅæ¾µ½¿ÔÅüàzðæ¾µ»èðs¤B
					 T1.SEIKYUSHONO		=	DT_URIAGEH. SEIKYUSHONO
				--	 ¿z > Ývüàz
				AND  T1.¿àz > DT_URIAGEH.NYUKINR
				--	 [üæ}X^
				AND  DT_URIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
				AND  '00' = DM_NONYU.SECCHIKBN
				-- Æ}X^
				AND  DT_URIAGEH.JIGYOCD = DM_JIGYO.JIGYOCD
        ORDER BY
        			  DT_URIAGEH.SEIKYUYMD
        			, DT_URIAGEH.SEIKYUCD
        			, DT_URIAGEH.SEIKYUSHONO
;
