-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP103 �n��}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP103
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_AREA.AREACD AS �n��CD
            , DM_AREA.AREANM AS �n�於
            , DM_AREA.AREANMR AS �n�旪��
		FROM  DM_AREA
		WHERE
             		DM_AREA.DELKBN	 = '0'
		ORDER BY
					  DM_AREA.AREACD
;
