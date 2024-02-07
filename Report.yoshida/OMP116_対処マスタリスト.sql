-------------------------------------------------------------------------------
--オムニテクノシステムリプレース
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP116 対処マスタリスト
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP116
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_TAISHO.TAISHOCD AS 対処CD
            , DM_TAISHO.TAISHONAIYO AS 対処内容
		FROM  DM_TAISHO
		WHERE
             		DM_TAISHO.DELKBN	 = '0'
		ORDER BY
					  DM_TAISHO.TAISHOCD
;
