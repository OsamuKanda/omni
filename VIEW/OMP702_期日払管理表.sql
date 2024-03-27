-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/09   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP702 �������Ǘ��\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP702
AS
		SELECT   
              ROWNUM  AS EDANUM
			, WK_PRT701.JIGYOCD AS ���Ə�CD													--���Ə��R�[�h
			, DM_JIGYO.JIGYONM AS ���Ə���													--���Ə���
			, SUBSTR(���t�L���ǉ�(DT_URIAGEH.SEIKYUYMD),3,8) AS ������						--������
			, WK_PRT701.JIGYOCD || WK_PRT701.SAGYOBKBN || WK_PRT701.RENNO AS �����ԍ�		--�����ԍ�
			, WK_PRT701.SEIKYUSHONO AS �������ԍ�											--�������ԍ�
			, DT_URIAGEH.SEIKYUCD AS ������CD												--������R�[�h
			, SUBSTR(DT_URIAGEH.SEIKYUNM,1,15) AS �����於									--�����於
			, SUBSTR(DT_URIAGEH.NONYUNM,1,15) AS �[���於									--�[���於
				-- �O�����f�[�^�̏ꍇ�A���� + ����� - �w��N���ȑO�̓����z�B�ȊO��'0'
			, DECODE(WK_PRT701.OUTKBN,1,WK_PRT701.KING + WK_PRT701.TAX - WK_PRT701.ZENNYUKIN,0) AS �O���J�z
			  	-- �O�����f�[�^�̏ꍇ�A'0'�B�ȊO�͔��� - �w��N���ȑO�̓����z
			, DECODE(WK_PRT701.OUTKBN,1,0,WK_PRT701.KING  - WK_PRT701.ZENNYUKIN) AS ����
			  	-- �O�����f�[�^�̏ꍇ�A'0'�B�ȊO�͏����
			, DECODE(WK_PRT701.OUTKBN,1,0,WK_PRT701.TAX) AS �����
				-- ���� + ����� - �w��N���ȑO�̓����z
			, WK_PRT701.KING + WK_PRT701.TAX - WK_PRT701.ZENNYUKIN AS �����z
			, WK_PRT701.NYUKINYMD AS ������
			, WK_PRT701.GENKIN AS ����														--����
			, WK_PRT701.NEBIKI AS �l��														--�l��
			, WK_PRT701.TEGATA AS ��`														--��`
			, WK_PRT701.YUSODAI AS ��`�X����												--��`�X����
			, WK_PRT701.URIKAKESAIKEN AS ���|��											--���|��
			, WK_PRT701.SOUSAI AS ���E														--���E
			, WK_PRT701.TESURYO AS �U���萔��												--�U���萔��
			, WK_PRT701.KAIHI AS �����														--�����
			, WK_PRT701.KINRI AS ����														--�����E����
			, WK_PRT701.MAEUKE AS �O��													--�O��
			, WK_PRT701.KING + WK_PRT701.TAX - WK_PRT701.ZENNYUKIN - (WK_PRT701.GENKIN + WK_PRT701.NEBIKI
			  + WK_PRT701.TEGATA + WK_PRT701.YUSODAI + WK_PRT701.URIKAKESAIKEN + WK_PRT701.SOUSAI
			  + WK_PRT701.TESURYO + WK_PRT701.KAIHI + WK_PRT701.KINRI + WK_PRT701.MAEUKE ) AS �����J�z
			, DECODE(DT_URIAGEH.NYUKINYOTEIYMD,NULL,���t�L���ǉ�(DT_URIAGEH.KAISHUYOTEIYMD),���t�L���ǉ�(DT_URIAGEH.NYUKINYOTEIYMD)) AS �����\��
			, DECODE(DT_URIAGEH.NYUKINYOTEIYMD,NULL,NULL,'(������)') AS �����敪
			, DECODE(DT_URIAGEH.HOSHUKBN,1,'����������',NULL) AS �����敪
			, DECODE(DT_URIAGEH.TAXKBN,1,'��ې�',NULL) AS �ېŋ敪
			, DECODE(WK_PRT701.OUTKBN,1,'�O����',2,'������',3,'�O��') AS �󎚕���
			, WK_PRT701.OUTKBN AS �󎚋敪
			, WK_PRT701.LOGINID 															--���O�C��ID
			, DM_NONYU.HURIGANA AS �t���K�i
			, SUBSTR(DT_URIAGEH.SEIKYUYMD,1,6) AS �����N��
			, SUBSTR(DT_URIAGEH.SEIKYUYMD,3,2) AS �����N
		FROM WK_PRT701,DT_URIAGEH,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT701.PROGID = 'OMP702'
--			  AND	WK_PRT701.OUTKBN <= '3'
					-- ����w�b�_�[�ƌ���
			  AND	DT_URIAGEH.SEIKYUSHONO = WK_PRT701.SEIKYUSHONO
			  AND	DT_URIAGEH.JIGYOCD = WK_PRT701.JIGYOCD
			  AND	DT_URIAGEH.SAGYOBKBN = WK_PRT701.SAGYOBKBN
			  AND	DT_URIAGEH.RENNO = WK_PRT701.RENNO
					-- ���Ə��}�X�^�ƌ���
			  AND	DM_JIGYO.JIGYOCD = WK_PRT701.JIGYOCD
			  		-- �[����}�X�^�ƌ����i������j
			  AND	DT_URIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00' = DM_NONYU.SECCHIKBN
        ORDER BY
        			  WK_PRT701.JIGYOCD
        			, SUBSTR(DT_URIAGEH.SEIKYUYMD,1,6)
        			, DM_NONYU.HURIGANA
        			, WK_PRT701.SEIKYUSHONO
;
