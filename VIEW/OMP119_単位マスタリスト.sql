-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP119 �P�ʃ}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP119
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_TANI.TANICD AS �P��CD
            , DM_TANI.TANINM AS �P�ʖ�
		FROM  DM_TANI
		WHERE
             		DM_TANI.DELKBN	 = '0'
		ORDER BY
					  DM_TANI.TANICD
;
