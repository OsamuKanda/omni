-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP123 �񍐏��p�^�[���}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP123
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_HPATAN.PATANCD AS CD
            , DM_HPATAN.PATANNM AS ����
            , DM_HPATAN.GYONO AS �s�ԍ�
            , DM_HPATAN.HBUNRUICD AS �񍐏�����CD
            , DM_HBUNRUI.HBUNRUINM AS �񍐏����ޖ�
            , DM_HPATAN.HSYOSAIMONG AS �ڍו���
            , DM_HPATAN.INPUTUMU AS ���͗L��
            , DM_HPATAN.INPUTNAIYOU AS ���͓��e
		FROM  DM_HBUNRUI,DM_HPATAN
		WHERE
             		DM_HBUNRUI.DELKBN	 = '0'
             AND	DM_HPATAN.DELKBN	 = '0'
             AND	DM_HPATAN.HBUNRUICD = DM_HBUNRUI.HBUNRUICD
		ORDER BY
					  DM_HPATAN.PATANCD,DM_HPATAN.GYONO	
;
