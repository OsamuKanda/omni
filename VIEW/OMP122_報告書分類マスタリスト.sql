-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP122 �񍐏����ރ}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP122
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_HBUNRUI.HBUNRUICD AS �񍐏�����CD
            , DM_HBUNRUI.HBUNRUINM AS �񍐏����ޖ�
		FROM  DM_HBUNRUI
		WHERE
             		DM_HBUNRUI.DELKBN	 = '0'
		ORDER BY
					  DM_HBUNRUI.HBUNRUICD
;
