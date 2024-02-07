CREATE OR REPLACE PACKAGE POMP604 AS
--***********************************************************************************************************
-- ’ •[—pPLSQL
--
-- yì¬z
--			2011.10.27		OKADA
-- yXV—š—ðz
--
--***********************************************************************************************************
--
	-------------------------------------------------------------------------------
	-- yŠÖ”–¼z
	--			’•¶‘”­s
	-- y“à—ez
	--
	--
	-- yˆø”z
	--
	-- y–ß‚è’lz
	--
	-------------------------------------------------------------------------------
	PROCEDURE	OMP604
	(
			I_Ž–‹ÆŠCD					IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_XVƒvƒƒOƒ‰ƒ€			IN		DT_BUKKEN.UDTPG1%TYPE
		,	I_’S“–ŽÒCD					IN		DT_BUKKEN.TANTCD%TYPE
		,	I_“`•[FR					IN		DT_SHIREH.SIRNO%TYPE
		,	I_“`•[TO					IN		DT_SHIREH.SIRNO%TYPE
		,	I_‹æ•ª						IN		DT_SHIREM.SAGYOBKBN%TYPE
	)
	;
END POMP604;
/
SHOW ERRORS
