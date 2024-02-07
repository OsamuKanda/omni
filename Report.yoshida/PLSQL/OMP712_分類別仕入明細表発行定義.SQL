CREATE OR REPLACE PACKAGE POMP712 AS
--***********************************************************************************************************
-- 帳票用PLSQL
--
-- 【作成】
--			2011.10.31		OKADA
-- 【更新履歴】
--
--***********************************************************************************************************
--
	-------------------------------------------------------------------------------
	-- 【関数名】
	--			分類別仕入一覧表発行
	-- 【内容】
	--
	--
	-- 【引数】
	--
	-- 【戻り値】
	--
	-------------------------------------------------------------------------------
	PROCEDURE	OMP712
	(
			I_事業所CD					IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_更新プログラム			IN		DT_BUKKEN.UDTPG1%TYPE
		,	I_担当者CD					IN		DT_BUKKEN.TANTCD%TYPE
		,	I_開始事業所CD				IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_終了事業所CD				IN		DT_BUKKEN.JIGYOCD%TYPE
		,	I_開始大分類CD				IN		DT_BUKKEN.BUNRUIDCD%TYPE
		,	I_終了大分類CD				IN		DT_BUKKEN.BUNRUIDCD%TYPE
		,	I_開始中分類CD				IN		DT_BUKKEN.BUNRUICCD%TYPE
		,	I_終了中分類CD				IN		DT_BUKKEN.BUNRUICCD%TYPE
		,	I_指定年月					IN		DT_BUKKEN.TANTCD%TYPE
	)
	;
END POMP712;
/
SHOW ERRORS
