-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP106 �����ރ}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP106
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_BUNRUIC.BUNRUICCD AS ������CD
            , DM_BUNRUIC.BUNRUICNM AS �����ޖ�
		FROM  DM_BUNRUIC
		WHERE
             		DM_BUNRUIC.DELKBN	 = '0'
		ORDER BY
					  DM_BUNRUIC.BUNRUICCD
;
