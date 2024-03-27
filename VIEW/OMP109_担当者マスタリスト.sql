-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP109 �S���҃}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP109
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_TANT.TANTCD AS �S����CD
            , DM_TANT.TANTNM AS �S���Җ�
            , DM_TANT.SHANAIKBN AS �Г��敪
            , DK_SHANAI.SHANAIKBNNM AS �Г��敪��
            , DM_TANT.SYOZOKJIGYOCD AS �������Ə�CD
            , DM_JIGYO.JIGYONM AS ���Ə���
            , DM_TANT.KIGYOCD AS ���CD
            , DM_KIGYO.KIGYONM AS ��Ɩ�
            , DM_TANT.UMUKBN AS ��ƗL���敪
            , DK_UMU.UMUKBNNM AS �L���敪��
		FROM  DM_TANT,DK_SHANAI,DM_JIGYO,DM_KIGYO,DK_UMU
		WHERE
             		DM_TANT.DELKBN	 = '0'
             AND	DM_TANT.SHANAIKBN = DK_SHANAI.SHANAIKBN(+)
             AND	DM_TANT.SYOZOKJIGYOCD = DM_JIGYO.JIGYOCD(+)
             AND	DM_TANT.KIGYOCD = DM_KIGYO.KIGYOCD(+)
             AND	DM_TANT.UMUKBN = DK_UMU.UMUKBN(+)
		ORDER BY
					  DM_TANT.SYOZOKJIGYOCD,DM_TANT.TANTCD
;
