-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP116 �Ώ��}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP116
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_TAISHO.TAISHOCD AS �Ώ�CD
            , DM_TAISHO.TAISHONAIYO AS �Ώ����e
		FROM  DM_TAISHO
		WHERE
             		DM_TAISHO.DELKBN	 = '0'
		ORDER BY
					  DM_TAISHO.TAISHOCD
;
