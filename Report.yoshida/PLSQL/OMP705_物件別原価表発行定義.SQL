CREATE OR REPLACE PACKAGE POMP705 AS
--***********************************************************************************************************
--  [pPLSQL
--
-- yì¬z
--			2011.10.31		OKADA
-- yXVðz
--
--***********************************************************************************************************
--
	-------------------------------------------------------------------------------
	-- yÖ¼z
	--			¨Ê´¿\­s
	-- yàez
	--
	--
	-- yøz
	--
	-- yßèlz
	--
	-------------------------------------------------------------------------------
	PROCEDURE	OMP705
	(
			I_ÀsÆCD				IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_XVvO			IN		DT_BUKKEN.UDTPG1%TYPE
		,	I_SÒCD					IN		DT_BUKKEN.TANTCD%TYPE
		,	I_ÆCD					IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_ìÆªÞæª				IN		DT_BUKKEN.SAGYOBKBN%TYPE
		,	I_wè1						IN		DT_BUKKEN.SAGYOBKBN%TYPE
		,	I_wè2						IN		DT_BUKKEN.SAGYOBKBN%TYPE
	)
	;
END POMP705;
/
SHOW ERRORS
