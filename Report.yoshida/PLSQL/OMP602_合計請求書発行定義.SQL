CREATE OR REPLACE PACKAGE POMP602 AS
--***********************************************************************************************************
--  [pPLSQL
--
-- yì¬z
--			2011.11.07		OKADA
-- yXVðz
--
--***********************************************************************************************************
--
	-------------------------------------------------------------------------------
	-- yÖ¼z
	--			v¿­s
	-- yàez
	--
	--
	-- yøz
	--
	-- yßèlz
	--
	-------------------------------------------------------------------------------
	PROCEDURE	OMP602
	(
			I_ÆCD					IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_XVvO			IN		DT_BUKKEN.UDTPG1%TYPE
		,	I_SÒCD					IN		DT_BUKKEN.TANTCD%TYPE
		,	I_­sæª					IN		DT_SHIREM.SAGYOBKBN%TYPE
		,	I_tæª					IN		DT_SHIREM.SAGYOBKBN%TYPE
		,	I_¿FR					IN		DT_SHIREH.SIRNO%TYPE
		,	I_¿TO					IN		DT_SHIREH.SIRNO%TYPE
	)
	;
END POMP602;
/
SHOW ERRORS
