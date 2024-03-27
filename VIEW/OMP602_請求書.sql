-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/07   OKADA
--                                                 Update:2012/04/24   OKADA
--
-- ���Ə��R�[�h=02�̏ꍇ�́A�}�X�^���̖��̂ł͂Ȃ�"�֓��T�[�r�X�H��"�Ƃ��Ĉ������B
--
-------------------------------------------------------------------------------
--OMP602 ���v������
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP602
AS
		SELECT
			  WK.�敪
			, WK.PAGENO
			, WK.�A��
			, WK.�������ԍ�												--�������ԍ�
			, WK.�������t												--�������t
			, WK.�����ԍ�												--�����ԍ�
			, WK.�X�֔ԍ�												--�X�֔ԍ�
			, WK.�Z��1													--�Z��1
			, WK.�Z��2													--�Z��2
			, WK.�����於												--�����於
			, WK.���������												--���������
			, WK.����S���Җ�											--����S���Җ�
			, WK.�[���於												--�[���於
			, WK.����\���												--����\���
			, WK.�s�ԍ�													--�s�ԍ�
			, WK.����
			, WK.�i��1								--�i��1
			, WK.�i��2								--�i��2
			, WK.����									--����
			, WK.�P�ʖ�								--�P�ʖ�
			, WK.�P��									--�P��
			, WK.���z									--���z
			, WK.�����									--�����
			, WK.���Ə��X�֔ԍ�						--
			, WK.���Ə��Z��1								--
			, WK.���Ə��Z��2								--
			, WK.���Ə��d�b�ԍ�							--
			, WK.���Ə�FAX�ԍ�							--
			, WK.���Ə���								--
			, WK.��s��
			, WK.LOGINID 										--���O�C��ID
			, WK.PROGID
			, WK.KINGAKU
			, WK.ZEI
			, WK.����
		FROM
(		SELECT   
			  '1' AS �敪
			, TRUNC(( row_number() over(partition by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID ) -1)/5) AS PAGENO
			, row_number() over(partition by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,DT_GURIAGEM.GYONO,WK_PRT000.LOGINID)  AS �A��
			, DT_GURIAGEH.SEIKYUSHONO AS �������ԍ�						--�������ԍ�
			, ���t�L���ǉ�(DT_GURIAGEH.SEIKYUYMD) AS �������t			--�������t
			, DT_GURIAGEH.JIGYOCD || '-' || DT_GURIAGEH.SAGYOBKBN || '-' || DT_GURIAGEH.RENNO AS �����ԍ�			--�����ԍ�
			, DT_GURIAGEH.ZIPCODE AS �X�֔ԍ�							--�X�֔ԍ�
			, DT_GURIAGEH.ADD1 AS �Z��1									--�Z��1
			, DT_GURIAGEH.ADD2 AS �Z��2									--�Z��2
			, DT_GURIAGEH.SEIKYUNM AS �����於							--�����於
			, DT_GURIAGEH.SENBUSHONM AS ���������						--���������
			, DT_GURIAGEH.SENTANTNM AS ����S���Җ�						--����S���Җ�
			, DT_GURIAGEH.NONYUNM AS �[���於							--�[���於
			, ���t�L���ǉ�(DT_GURIAGEH.KAISHUYOTEIYMD) AS ����\���					--����\���
			, DT_GURIAGEM.GYONO AS �s�ԍ�								--�s�ԍ�
			, DECODE(DT_GURIAGEM.MMDD,NULL,NULL,SUBSTR(DT_GURIAGEM.MMDD,1,2) || '/' || SUBSTR(DT_GURIAGEM.MMDD,3,2))  AS ����
			, DT_GURIAGEM.HINNM1 AS �i��1								--�i��1
			, DT_GURIAGEM.HINNM2 AS �i��2								--�i��2
			, DT_GURIAGEM.SURYO AS ����									--����
			, DT_GURIAGEM.TANINM AS �P�ʖ�								--�P�ʖ�
			, DT_GURIAGEM.TANKA AS �P��									--�P��
			, DT_GURIAGEM.KING AS ���z									--���z
			, DT_GURIAGEM.TAX AS �����									--�����
			, DM_JIGYO.ZIPCODE AS ���Ə��X�֔ԍ�						--
			, DM_JIGYO.ADD1 AS ���Ə��Z��1								--
			, DM_JIGYO.ADD2 AS ���Ə��Z��2								--
			, DM_JIGYO.TELNO AS ���Ə��d�b�ԍ�							--
			, DM_JIGYO.FAXNO AS ���Ə�FAX�ԍ�							--
			, DECODE(DM_JIGYO.JIGYOCD,'02','�֓��T�[�r�X�H��',DM_JIGYO.JIGYONM) AS ���Ə���								--
			, DECODE(DM_NONYU.GINKOKBN,0,DM_JIGYO.FURIGINKONM,DM_JIGYO.TOKUGINKONM) AS ��s��
			, WK_PRT000.LOGINID 										--���O�C��ID
			, WK_PRT000.PROGID
			, WK_PRT000.KINGAKU
			, WK_PRT000.ZEI
			, DT_GURIAGEH.BUKKENMEMO AS ����
		FROM WK_PRT000,DT_GURIAGEH,DT_GURIAGEM
			,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT000.PROGID = 'OMP602'
			  AND	DT_GURIAGEH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_GURIAGEH.SEIKYUSHONO = WK_PRT000.DENPNO
					-- ���ׂƌ���
              AND	WK_PRT000.DENPNO = DT_GURIAGEM.SEIKYUSHONO
              AND	WK_PRT000.GYONO = DT_GURIAGEM.GYONO
              		-- ���Ə��}�X�^
              AND	DM_JIGYO.JIGYOCD = DT_GURIAGEH.JIGYOCD
					-- �[����}�X�^
			  AND	DT_GURIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00'				= DM_NONYU.SECCHIKBN
              AND	DT_GURIAGEH.DELKBN = '0'
              AND	DT_GURIAGEM.DELKBN = '0'
UNION ALL
--�[�i��
		SELECT   
			  '2' AS �敪
			, TRUNC(( row_number() over(partition by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID ) -1)/5) AS PAGENO
			, row_number() over(partition by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,DT_GURIAGEM.GYONO,WK_PRT000.LOGINID)  AS �A��
			, DT_GURIAGEH.SEIKYUSHONO AS �������ԍ�						--�������ԍ�
			, ���t�L���ǉ�(DT_GURIAGEH.SEIKYUYMD) AS �������t			--�������t
			, DT_GURIAGEH.JIGYOCD || '-' || DT_GURIAGEH.SAGYOBKBN || '-' || DT_GURIAGEH.RENNO AS �����ԍ�			--�����ԍ�
			, DT_GURIAGEH.ZIPCODE AS �X�֔ԍ�							--�X�֔ԍ�
			, DT_GURIAGEH.ADD1 AS �Z��1									--�Z��1
			, DT_GURIAGEH.ADD2 AS �Z��2									--�Z��2
			, DT_GURIAGEH.SEIKYUNM AS �����於							--�����於
			, DT_GURIAGEH.SENBUSHONM AS ���������						--���������
			, DT_GURIAGEH.SENTANTNM AS ����S���Җ�						--����S���Җ�
			, DT_GURIAGEH.NONYUNM AS �[���於							--�[���於
			, ���t�L���ǉ�(DT_GURIAGEH.KAISHUYOTEIYMD) AS ����\���					--����\���
			, DT_GURIAGEM.GYONO AS �s�ԍ�								--�s�ԍ�
			, DECODE(DT_GURIAGEM.MMDD,NULL,NULL,SUBSTR(DT_GURIAGEM.MMDD,1,2) || '/' || SUBSTR(DT_GURIAGEM.MMDD,3,2))  AS ����
			, DT_GURIAGEM.HINNM1 AS �i��1								--�i��1
			, DT_GURIAGEM.HINNM2 AS �i��2								--�i��2
			, DT_GURIAGEM.SURYO AS ����									--����
			, DT_GURIAGEM.TANINM AS �P�ʖ�								--�P�ʖ�
			, DT_GURIAGEM.TANKA AS �P��									--�P��
			, DT_GURIAGEM.KING AS ���z									--���z
			, DT_GURIAGEM.TAX AS �����									--�����
			, DM_JIGYO.ZIPCODE AS ���Ə��X�֔ԍ�						--
			, DM_JIGYO.ADD1 AS ���Ə��Z��1								--
			, DM_JIGYO.ADD2 AS ���Ə��Z��2								--
			, DM_JIGYO.TELNO AS ���Ə��d�b�ԍ�							--
			, DM_JIGYO.FAXNO AS ���Ə�FAX�ԍ�							--
			, DECODE(DM_JIGYO.JIGYOCD,'02','�֓��T�[�r�X�H��',DM_JIGYO.JIGYONM) AS ���Ə���								--
			, NULL AS ��s��
			, WK_PRT000.LOGINID 										--���O�C��ID
			, WK_PRT000.PROGID
			, WK_PRT000.KINGAKU
			, WK_PRT000.ZEI
			, DT_GURIAGEH.BUKKENMEMO AS ����
		FROM WK_PRT000,DT_GURIAGEH,DT_GURIAGEM
			,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT000.PROGID = 'OMP602'
			  AND	DT_GURIAGEH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_GURIAGEH.SEIKYUSHONO = WK_PRT000.DENPNO
					-- ���ׂƌ���
              AND	WK_PRT000.DENPNO = DT_GURIAGEM.SEIKYUSHONO
              AND	WK_PRT000.GYONO = DT_GURIAGEM.GYONO
              		-- ���Ə��}�X�^
              AND	DM_JIGYO.JIGYOCD = DT_GURIAGEH.JIGYOCD
					-- �[����}�X�^
			  AND	DT_GURIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00'				= DM_NONYU.SECCHIKBN
              AND	DT_GURIAGEH.DELKBN = '0'
              AND	DT_GURIAGEM.DELKBN = '0'
UNION ALL
--����`�[
		SELECT   
			  '3' AS �敪
			, TRUNC(( row_number() over(partition by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID ) -1)/5) AS PAGENO
			, row_number() over(partition by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_GURIAGEH.JIGYOCD,DT_GURIAGEH.SEIKYUSHONO,DT_GURIAGEM.GYONO,WK_PRT000.LOGINID)  AS �A��
			, DT_GURIAGEH.SEIKYUSHONO AS �������ԍ�						--�������ԍ�
			, ���t�L���ǉ�(DT_GURIAGEH.SEIKYUYMD) AS �������t			--�������t
			, DT_GURIAGEH.JIGYOCD || '-' || DT_GURIAGEH.SAGYOBKBN || '-' || DT_GURIAGEH.RENNO AS �����ԍ�			--�����ԍ�
			, DT_GURIAGEH.ZIPCODE AS �X�֔ԍ�							--�X�֔ԍ�
			, DT_GURIAGEH.ADD1 AS �Z��1									--�Z��1
			, DT_GURIAGEH.ADD2 AS �Z��2									--�Z��2
			, DT_GURIAGEH.SEIKYUNM AS �����於							--�����於
			, DT_GURIAGEH.SENBUSHONM AS ���������						--���������
			, DT_GURIAGEH.SENTANTNM AS ����S���Җ�						--����S���Җ�
			, DT_GURIAGEH.NONYUNM AS �[���於							--�[���於
			, NULL AS ����\���										--����\���
			, DT_GURIAGEM.GYONO AS �s�ԍ�								--�s�ԍ�
			, DECODE(DT_GURIAGEM.MMDD,NULL,NULL,SUBSTR(DT_GURIAGEM.MMDD,1,2) || '/' || SUBSTR(DT_GURIAGEM.MMDD,3,2))  AS ����
			, DT_GURIAGEM.HINNM1 AS �i��1								--�i��1
			, DT_GURIAGEM.HINNM2 AS �i��2								--�i��2
			, DT_GURIAGEM.SURYO AS ����									--����
			, DT_GURIAGEM.TANINM AS �P�ʖ�								--�P�ʖ�
			, DT_GURIAGEM.TANKA AS �P��									--�P��
			, DT_GURIAGEM.KING AS ���z									--���z
			, DT_GURIAGEM.TAX AS �����									--�����
			, DM_JIGYO.ZIPCODE AS ���Ə��X�֔ԍ�						--
			, DM_JIGYO.ADD1 AS ���Ə��Z��1								--
			, DM_JIGYO.ADD2 AS ���Ə��Z��2								--
			, DM_JIGYO.TELNO AS ���Ə��d�b�ԍ�							--
			, DM_JIGYO.FAXNO AS ���Ə�FAX�ԍ�							--
			, DECODE(DM_JIGYO.JIGYOCD,'02','�֓��T�[�r�X�H��',DM_JIGYO.JIGYONM) AS ���Ə���								--
			, NULL AS ��s��
			, WK_PRT000.LOGINID 										--���O�C��ID
			, WK_PRT000.PROGID
			, WK_PRT000.KINGAKU
			, WK_PRT000.ZEI
			, DT_GURIAGEH.BUKKENMEMO AS ����
		FROM WK_PRT000,DT_GURIAGEH,DT_GURIAGEM
			,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT000.PROGID = 'OMP602'
			  AND	DT_GURIAGEH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_GURIAGEH.SEIKYUSHONO = WK_PRT000.DENPNO
					-- ���ׂƌ���
              AND	WK_PRT000.DENPNO = DT_GURIAGEM.SEIKYUSHONO
              AND	WK_PRT000.GYONO = DT_GURIAGEM.GYONO
              		-- ���Ə��}�X�^
              AND	DM_JIGYO.JIGYOCD = DT_GURIAGEH.JIGYOCD
					-- �[����}�X�^
			  AND	DT_GURIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00'				= DM_NONYU.SECCHIKBN
              AND	DT_GURIAGEH.DELKBN = '0'
              AND	DT_GURIAGEM.DELKBN = '0'
			) WK
		ORDER BY WK.�������ԍ�,WK.PAGENO,WK.�敪,WK.�A��
;
