-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP107 ���i���ރ}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP107
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_BBUNRUI.BBUNRUICD AS ���i����CD
            , DM_BBUNRUI.BBUNRUINM AS ���i���ޖ�
		FROM  DM_BBUNRUI
		WHERE
             		DM_BBUNRUI.DELKBN	 = '0'
		ORDER BY
					  DM_BBUNRUI.BBUNRUICD
;
