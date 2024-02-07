-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP110 �d����}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP110
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_SHIRE.SIRCD AS �d����CD
            , DM_SHIRE.SIRNM1 AS �d���於1
            , DM_SHIRE.SIRNM2 AS �d���於2
            , DM_SHIRE.SIRNMR AS �d���旪��
            , DM_SHIRE.SIRNMX AS �d����J�i
            , DM_SHIRE.ZIPCODE AS �X�֔ԍ�
            , DM_SHIRE.ADD1 AS �Z��1
            , DM_SHIRE.ADD2 AS �Z��2
            , DM_SHIRE.TELNO AS �d�b�ԍ�
            , DM_SHIRE.FAXNO AS �e�`�w
            , DM_SHIRE.HASUKBN AS �[���敪
            , DK_HASU.HASUKBNNM AS �[���敪��	
            , DM_SHIRE.ZENZAN AS �O���c��
            , DM_SHIRE.TSIRKIN AS �����d�����z
            , DM_SHIRE.TSIRHENKIN AS �����d���ԕi���z
            , DM_SHIRE.TSIRNEBIKI AS �����d���l�����z
            , DM_SHIRE.TTAX AS ���������
            , DM_SHIRE.TSHRGENKIN AS �����x������
            , DM_SHIRE.TSHRTEGATA AS �����x����`
            , DM_SHIRE.TSHRNEBIKI AS �����x���l��
            , DM_SHIRE.TSHRSOSAI AS �����x�����E
            , DM_SHIRE.TSHRSONOTA AS �����x�����̑�
            , DM_SHIRE.TSHRANZENKAIHI AS �����x�����S���͉��
            , DM_SHIRE.TSHRFURIKOMITESU AS �����x���U���萔��
		FROM  DM_SHIRE,DK_HASU
		WHERE
             		DM_SHIRE.DELKBN	 = '0'
             AND	DM_SHIRE.HASUKBN = DK_HASU.HASUKBN(+)
 		ORDER BY
					  DM_SHIRE.SIRCD
;
