-------------------------------------------------------------------------------
--IjeNmVXev[X
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:2013/8/20    KAWAHATA ðÇÁ ÎÛxÌf[^ÌÝ
-------------------------------------------------------------------------------
--OMP607 âsÊx¥è`ê\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP607
AS
		SELECT  
              ROWNUM  AS EDANUM
			, SUBSTR(DM_KANRI.MONYMD,1,4) || 'N' ||  SUBSTR(DM_KANRI.MONYMD,5,2) || 'x' AS ÷N			--÷N
			, útLÇÁ(DT_SHRH.SHRYMD) AS x¥ú																--x¥ú
			, DT_SHRB.SHRGINKOKBN AS âsæª																		--âsæª
			, DK_SHRGINKO.SHRGINKOKBNNM AS x¥âs¼																--x¥âs¼
			, útLÇÁ(DT_SHRB.TEGATAKIJITSU) AS è`úú														--è`úú
			, DT_SHRB.TEGATANO AS è`Ô																			--è`Ô
			, DT_SHRB.KING AS àz																					--àz
			, DT_SHRH.SIRCD AS x¥æCD																				--x¥æR[h
			, DM_SHIRE.SIRNM1 AS x¥æ¼																			--x¥æ¼
			, DT_SHRH.KAMKKBN AS ÈÚæª																			--ÈÚæª
			, DK_KAMOKU.KAMOKUKBNNM AS ÈÚ¼																		--ÈÚ¼
		FROM DT_SHRH,DT_SHRB,DM_KANRI,DK_SHRGINKO,DM_SHIRE,DK_KAMOKU
		WHERE
					-- x¥¾×Æ
              		DT_SHRH.JIGYOCD = DT_SHRB.JIGYOCD
              AND	DT_SHRH.SHRNO = DT_SHRB.SHRNO
              		-- x¥âs}X^
              AND	DK_SHRGINKO.SHRGINKOKBN(+) = DT_SHRB.SHRGINKOKBN
              		-- Ç}X^
              AND	DM_KANRI.KANRINO = '1'
              		-- düæ}X^
              AND	DM_SHIRE.SIRCD(+) = DT_SHRH.SIRCD
              		-- ÈÚæª}X^
              --2014/04/30 ¾×ÌÈÚæªÆ
              --AND	DK_KAMOKU.KAMOKUKBN(+) = DT_SHRH.KAMKKBN
              AND	DK_KAMOKU.KAMOKUKBN(+) = DT_SHRB.KAMOKUKBN
              AND	DT_SHRH.DELKBN = '0'
              AND	DT_SHRB.DELKBN = '0'
              --è`ÌÝ
              AND	DT_SHRB.NYUKINKBN = '02'
			  --÷N '01' > úúÍÎÛO
--			  AND	DT_SHRB.TEGATAKIJITSU >= SUBSTR(DM_KANRI.MONYMD,1,4) || SUBSTR(DM_KANRI.MONYMD,5,2) || '01'
			  AND	DT_SHRH.SHRYMD >= SUBSTR(DM_KANRI.MONYMD,1,4) || SUBSTR(DM_KANRI.MONYMD,5,2) || '01'
			  --2013/8/20 ðÇÁ ÎÛxÌf[^ÌÝ
			  AND	DT_SHRH.SHRYMD <= SUBSTR(DM_KANRI.MONYMD,1,4) || SUBSTR(DM_KANRI.MONYMD,5,2) || '31'
        ORDER BY
        			  DT_SHRH.SHRYMD
        			, DT_SHRB.SHRGINKOKBN
        			, DT_SHRB.TEGATAKIJITSU
;

