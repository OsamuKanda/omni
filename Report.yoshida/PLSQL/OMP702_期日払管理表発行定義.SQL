CREATE OR REPLACE PACKAGE POMP702 AS
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
	--			úú¥Ç\­s
	-- yàez
	--
	--
	-- yøz
	--
	-- yßèlz
	--
	-------------------------------------------------------------------------------
	PROCEDURE	OMP702
	(
			I_ÆCD					IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_XVvO			IN		DT_BUKKEN.UDTPG1%TYPE
		,	I_SÒCD					IN		DT_BUKKEN.TANTCD%TYPE
		,	I_wèN					IN		DM_JIGYO.HOSHUYMD%TYPE
		,	I_oÍwè					IN		DT_SHIREM.SAGYOBKBN%TYPE
		,	I_Æwè				IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_oÍ [					IN		DT_SHIREM.SAGYOBKBN%TYPE
	)
	;
END POMP702;
/
SHOW ERRORS
