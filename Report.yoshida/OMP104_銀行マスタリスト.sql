-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP104 ��s�}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP104
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_GINKO.GINKOCD AS ��sCD
            , DM_GINKO.GINKONM AS ��s��
		FROM  DM_GINKO
		WHERE
             		DM_GINKO.DELKBN	 = '0'
		ORDER BY
					  DM_GINKO.GINKOCD
;
