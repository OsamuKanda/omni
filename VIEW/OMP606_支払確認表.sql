-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP606 �x���m�F�\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP606
AS
		SELECT   
              ROWNUM  AS EDANUM
			, DT_SHRH.JIGYOCD AS ���Ə�CD								--���Ə��R�[�h
			, DT_SHRH.SHRNO AS �x���ԍ�									--�x���ԍ�
			, DT_SHRB.NYUKINKBN AS �����敪								--�����敪
			, DK_NYUKIN.NYUKINKBNNM AS �����敪��						--�����敪��
			, DT_SHRB.KAMOKUKBN AS �Ȗڋ敪								--�Ȗڋ敪
			, DK_KAMOKU.KAMOKUKBNNM AS �Ȗڋ敪��						--�Ȗڋ敪��
			, ���t�L���ǉ�(DT_SHRH.SHRYMD) AS �x�����t					--�x�����t
			, DT_SHRH.SIRCD AS �d����CD									--�d����R�[�h
			, DM_SHIRE.SIRNM1 AS �d���於								--�d���於
--			, TO_NUMBER(DT_SHRB.GYONO) AS �s�ԍ�						--�s�ԍ�
			, ROW_NUMBER() OVER(PARTITION BY DT_SHRH.JIGYOCD,DT_SHRH.SHRNO,WK_PRT000.LOGINID ORDER BY DT_SHRH.JIGYOCD,DT_SHRH.SHRNO,DT_SHRB.GYONO)  AS �s�ԍ�						--�s�ԍ�
			, DT_SHRB.KING AS ���z										--���z
			, DT_SHRH.BIKO AS ���l										--���l
			, DT_SHRB.TEGATANO AS ��`�ԍ�								--��`�ԍ�
			, ���t�L���ǉ�(DT_SHRB.TEGATAKIJITSU) AS ��`����			--��`����
			, DT_SHRB.SHRGINKOKBN AS ��s�敪							--��s�敪
			, DK_SHRGINKO.SHRGINKOKBNNM AS �x����s��					--�x����s��
			, WK_PRT000.LOGINID 										--���O�C��ID
		FROM WK_PRT000,DT_SHRH,DT_SHRB,DM_SHIRE,DK_NYUKIN,DK_KAMOKU,DK_SHRGINKO
		WHERE
					WK_PRT000.PROGID = 'OMP606'
			  AND	DT_SHRH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_SHRH.SHRNO = WK_PRT000.DENPNO
					-- ���ׂƌ���
              AND	DT_SHRH.JIGYOCD = DT_SHRB.JIGYOCD
              AND	DT_SHRH.SHRNO = DT_SHRB.SHRNO
              AND	WK_PRT000.GYONO = DT_SHRB.GYONO
              		-- �����敪
              AND	DK_NYUKIN.NYUKINKBN(+) = DT_SHRB.NYUKINKBN
              		-- �Ȗڋ敪
              AND	DK_KAMOKU.KAMOKUKBN(+) = DT_SHRB.KAMOKUKBN
              		-- �d����}�X�^
              AND	DM_SHIRE.SIRCD(+) = DT_SHRH.SIRCD
              		-- �x����s�}�X�^
              AND	DK_SHRGINKO.SHRGINKOKBN(+) = DT_SHRB.SHRGINKOKBN
              AND	DT_SHRH.DELKBN = '0'
              AND	DT_SHRB.DELKBN = '0'
        ORDER BY
        			  DT_SHRH.JIGYOCD
        			, DT_SHRH.SHRNO
        			, DT_SHRH.SHRYMD
        			, TO_NUMBER(DT_SHRB.GYONO)
;
