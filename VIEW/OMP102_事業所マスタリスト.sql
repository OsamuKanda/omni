-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP102 ���Ə��}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP102
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_JIGYO.JIGYOCD AS ���Ə�CD									--���Ə��R�[�h
            , DM_JIGYO.JIGYONM AS ���Ə���									--���Ə���
            , DM_JIGYO.ZIPCODE AS �X�֔ԍ�
            , DM_JIGYO.ADD1 AS �Z��1
            , DM_JIGYO.ADD2 AS �Z��2
            , DM_JIGYO.TELNO AS �d�b�ԍ�
            , DM_JIGYO.FAXNO AS �e�`�w
            , DM_JIGYO.FURIGINKONM AS �������U����s��
            , DM_JIGYO.TOKUGINKONM AS �����������s��
            , DM_JIGYO.BUKKENNO AS �����ԍ�
            , DM_JIGYO.SEIKYUSHONO AS �������ԍ�
            , DM_JIGYO.NYUKINNO AS �����ԍ�
            , DM_JIGYO.HACCHUNO AS �����ԍ�
            , DM_JIGYO.SIRNO AS �d���ԍ�
            , DM_JIGYO.SHRNO AS �x���ԍ�
		FROM  DM_JIGYO
		WHERE
             		DM_JIGYO.DELKBN	 = '0'
		ORDER BY
					  DM_JIGYO.JIGYOCD
;
