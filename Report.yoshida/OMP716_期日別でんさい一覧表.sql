-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2014/04/30   KAWAHATA
--                                                 Update:2014/05/13   KAWAHATA 
--�����d����A�����Ȗڂ����Z����
-------------------------------------------------------------------------------
--OMP716	�����ʂł񂳂��ꗗ�\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP716
AS
		SELECT  
              SUBSTR(DM_KANRI.MONYMD,1,4) || '�N' ||  SUBSTR(DM_KANRI.MONYMD,5,2) || '���x' AS �������N��			--�������N��
			, ���t�L���ǉ�(DT_SHRB.TEGATAKIJITSU) AS ��`����														--��`����
			, DT_SHRB.SHRGINKOKBN AS ��s�敪																		--��s�敪
			, MAX(DK_SHRGINKO.SHRGINKOKBNNM) AS �x����s��															--�x����s��
			, SUM(DT_SHRB.KING) AS ���z																					--���z
			, DT_SHRH.SIRCD AS �x����CD																				--�x����R�[�h
			, MAX(DM_SHIRE.SIRNM1) AS �x���於																			--�x���於
			, MAX(DM_SHIRE.SIRNMX) AS �d����J�i																			--�d����J�i
			, DT_SHRB.KAMOKUKBN AS �Ȗڋ敪																			--�Ȗڋ敪
			, MAX(DK_KAMOKU.KAMOKUKBNNM) AS �Ȗږ�																		--�Ȗږ�
			, ���t�L���ǉ�(DT_SHRH.SHRYMD) AS �x����																--�x����
			, SUBSTR(DT_SHRB.TEGATAKIJITSU,1,6) AS �����N��															--�����N��
		FROM DT_SHRH,DT_SHRB,DM_KANRI,DK_SHRGINKO,DM_SHIRE,DK_KAMOKU
		WHERE
					-- �x�����ׂƌ���
              		DT_SHRH.JIGYOCD = DT_SHRB.JIGYOCD
              AND	DT_SHRH.SHRNO = DT_SHRB.SHRNO
              		-- �x����s�}�X�^
              AND	DK_SHRGINKO.SHRGINKOKBN(+) = DT_SHRB.SHRGINKOKBN
              		-- �Ǘ��}�X�^
              AND	DM_KANRI.KANRINO = '1'
              		-- �d����}�X�^
              AND	DM_SHIRE.SIRCD(+) = DT_SHRH.SIRCD
              		-- �Ȗڋ敪�}�X�^
              AND	DK_KAMOKU.KAMOKUKBN(+) = DT_SHRB.KAMOKUKBN
              AND	DT_SHRH.DELKBN = '0'
              AND	DT_SHRB.DELKBN = '0'
              --�ł񂳂��̂�
              AND	DT_SHRB.NYUKINKBN = '13'
--			  --�������N�� '01' > �����͑ΏۊO
--			  AND	DT_SHRB.TEGATAKIJITSU >= TO_CHAR(SUBSTR(DM_KANRI.MONYMD,1,4) || SUBSTR(DM_KANRI.MONYMD,5,2) || '01')
			  AND	DT_SHRB.TEGATAKIJITSU > TO_CHAR(DM_KANRI.MONYMD)
--			  --2013/8/20 �����ǉ� �Ώی��x�̃f�[�^�̂�
			  AND	DT_SHRH.SHRYMD <= SUBSTR(DM_KANRI.MONYMD,1,4) || SUBSTR(DM_KANRI.MONYMD,5,2) || '31'
        GROUP BY
        			  SUBSTR(DM_KANRI.MONYMD,1,4)
        			, SUBSTR(DM_KANRI.MONYMD,5,2)
        			, DT_SHRB.TEGATAKIJITSU
        			, DT_SHRB.SHRGINKOKBN
        			, DT_SHRH.SHRYMD
        			, DT_SHRH.SIRCD
        			, DM_SHIRE.SIRNMX
        			, DT_SHRB.KAMOKUKBN
        ORDER BY
                	  SUBSTR(DM_KANRI.MONYMD,1,4)
        			, SUBSTR(DM_KANRI.MONYMD,5,2)
        			, DT_SHRB.TEGATAKIJITSU
        			, DT_SHRB.SHRGINKOKBN
        			, DT_SHRH.SHRYMD
        			, DT_SHRH.SIRCD
        			, DM_SHIRE.SIRNMX
        			, DT_SHRB.KAMOKUKBN
;

