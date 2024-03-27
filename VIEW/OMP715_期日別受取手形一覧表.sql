-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:2012/02/15   OKADA
-- �U�o�l���ɃT�}���������ʂ����
-------------------------------------------------------------------------------
--OMP715	�����ʎ���`�ꗗ�\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP715
AS
		SELECT  
			  SUBSTR(DM_KANRI.MONYMD,1,4) || '�N' ||  SUBSTR(DM_KANRI.MONYMD,5,2) || '���x' AS �������N��			--�������N��
			, ���t�L���ǉ�(DT_NYUKINM.TEGATAKIJITSU) AS ��`����													--��`����
--			, DT_URIAGEH.SEIKYUCD AS ������CD																		--������R�[�h
--			, DT_URIAGEH.SEIKYUNM AS �����於																		--�����於
			, SUM(DT_NYUKINM.KING) AS ���z																				--���z
			, DT_NYUKINM.GINKOCD AS ��s�敪																		--��s�敪
			, DM_GINKO.GINKONM AS ��s��																			--��s��
			, DT_NYUKINM.TEGATANO AS ��`�ԍ�																		--��`�ԍ�
			, ���t�L���ǉ�(DT_NYUKINM.HURIYMD) AS �U�o��															--�U�o��
			, DT_NYUKINM.HURIDASHI AS �U�o�l																		--�U�o�l
			, SUBSTR(DT_NYUKINM.TEGATAKIJITSU,1,6) AS �����N��														--�����N��
		FROM DT_NYUKINM,DM_GINKO,DM_KANRI
		WHERE
             		-- �x����s�}�X�^
              		DM_GINKO.GINKOCD(+) = DT_NYUKINM.GINKOCD
              		-- �Ǘ��}�X�^
              AND	DM_KANRI.KANRINO = '1'
              		-- ����w�b�_
--              AND	DT_URIAGEH.SEIKYUSHONO = DT_NYUKINM.SEIKYUSHONO
              AND	DT_NYUKINM.DELKBN = '0'
              --��`�̂�
              AND	DT_NYUKINM.NYUKINKBN = '02'
			  --�������N�� '01' > �����͑ΏۊO
--2012.10.18---------------
------------------	  AND	DT_NYUKINM.TEGATAKIJITSU >= TO_CHAR(SUBSTR(DM_KANRI.MONYMD,1,4) || SUBSTR(DM_KANRI.MONYMD,5,2) || '01')
			  AND	DT_NYUKINM.TEGATAKIJITSU > TO_CHAR(DM_KANRI.MONYMD)
		GROUP BY	  DM_KANRI.MONYMD
					, DT_NYUKINM.TEGATAKIJITSU
					, DT_NYUKINM.GINKOCD
					, DM_GINKO.GINKONM
					, DT_NYUKINM.TEGATANO
					, DT_NYUKINM.HURIYMD
					, DT_NYUKINM.HURIDASHI
        ORDER BY
        			  DT_NYUKINM.TEGATAKIJITSU
--        			, DT_URIAGEH.SEIKYUCD
        			, DT_NYUKINM.HURIYMD
        			, DT_NYUKINM.TEGATANO
;

