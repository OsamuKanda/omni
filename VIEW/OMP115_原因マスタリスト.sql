-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP115 �����}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP115
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_GENIN.GENINCD AS ����CD
            , DM_GENIN.GENINNAIYO AS �������e
		FROM  DM_GENIN
		WHERE
             		DM_GENIN.DELKBN	 = '0'
		ORDER BY
					  DM_GENIN.GENINCD
;
