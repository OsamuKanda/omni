-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/07   OKADA
--                                                 Update:2012/04/24   OKADA
--                                                 Update:2023/09/22   Kanda(�������C���{�C�X�Ή�����юЖ��ύX�j
--
-- ���Ə��R�[�h=02�̏ꍇ�́A�}�X�^���̖��̂ł͂Ȃ�"�֓��T�[�r�X�H��"�Ƃ��Ĉ������B
--
-------------------------------------------------------------------------------
--OMP601 ������
-------------------------------------------------------------------------------
CREATE OR REPLACE FORCE VIEW "OMNI"."V_OMP601" ("�敪", "PAGENO", "�A��", "�������ԍ�", "�������t", "�����ԍ�", "�X�֔ԍ�", "�Z��1", "�Z��2", "�����於", "���������", "����S���Җ�", "�[���於", "����\���", "�s�ԍ�", "����", "�i��1", "�i��2", "����", "�P�ʖ�", "�P��", "���z", "�����", "���Ə��X�֔ԍ�", "���Ə��Z��1", "���Ə��Z��2", "���Ə��d�b�ԍ�", "���Ə�FAX�ԍ�", "���Ə���", "��s��", "LOGINID", "PROGID", "KINGAKU", "ZEI", "����", "�ŋ敪") AS 
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
			, WK.�ŋ敪   -- 2023/08/21 ADD TC KANDA �ŋ敪��0(�ې�)/1(��ې�)�̕\��
		FROM
(		SELECT
			  '1' AS �敪
			, TRUNC(( row_number() over(partition by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID ) -1)/5) AS PAGENO
			, row_number() over(partition by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,DT_URIAGEM.GYONO,WK_PRT000.LOGINID)  AS �A��
			, DT_URIAGEH.SEIKYUSHONO AS �������ԍ�						--�������ԍ�
			, ���t�L���ǉ�(DT_URIAGEH.SEIKYUYMD) AS �������t			--�������t
			, DT_URIAGEH.JIGYOCD || '-' || DT_URIAGEH.SAGYOBKBN || '-' || DT_URIAGEH.RENNO AS �����ԍ�			--�����ԍ�
			, DT_URIAGEH.ZIPCODE AS �X�֔ԍ�							--�X�֔ԍ�
			, DT_URIAGEH.ADD1 AS �Z��1									--�Z��1
			, DT_URIAGEH.ADD2 AS �Z��2									--�Z��2
			, DT_URIAGEH.SEIKYUNM AS �����於							--�����於
			, DT_URIAGEH.SENBUSHONM AS ���������						--���������
			, DT_URIAGEH.SENTANTNM AS ����S���Җ�						--����S���Җ�
			, DT_URIAGEH.NONYUNM AS �[���於							--�[���於
			, ���t�L���ǉ�(DT_URIAGEH.KAISHUYOTEIYMD) AS ����\���					--����\���
			, DT_URIAGEM.GYONO AS �s�ԍ�								--�s�ԍ�
			, DECODE(DT_URIAGEM.MMDD,NULL,NULL,SUBSTR(DT_URIAGEM.MMDD,1,2) || '/' || SUBSTR(DT_URIAGEM.MMDD,3,2))  AS ����
			, DT_URIAGEM.HINNM1 AS �i��1								--�i��1
			, DT_URIAGEM.HINNM2 AS �i��2								--�i��2
			, DT_URIAGEM.SURYO AS ����									--����
			, DT_URIAGEM.TANINM AS �P�ʖ�								--�P�ʖ�
			, DT_URIAGEM.TANKA AS �P��									--�P��
			, DT_URIAGEM.KING AS ���z									--���z
			, DT_URIAGEM.TAX AS �����									--�����
			-- ��2023/08/21 UPDATE TC KANDA ��������2023�N9��30���܂ł̂��̂͋��̎��Ə�����\��
			--, DM_JIGYO.ZIPCODE AS ���Ə��X�֔ԍ�						--
			--, DM_JIGYO.ADD1 AS ���Ə��Z��1								--
			--, DM_JIGYO.ADD2 AS ���Ə��Z��2								--
			--, DM_JIGYO.TELNO AS ���Ə��d�b�ԍ�							--
			--, DM_JIGYO.FAXNO AS ���Ə�FAX�ԍ�							--
			--, DECODE(DM_JIGYO.JIGYOCD,'02','�֓��T�[�r�X�H��',DM_JIGYO.JIGYONM) AS ���Ə���								--
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ZIPCODE ELSE DM_JIGYO.ZIPCODE END) AS ���Ə��X�֔ԍ�
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ADD1 ELSE DM_JIGYO.ADD1 END) AS ���Ə��Z��1
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ADD2 ELSE DM_JIGYO.ADD2 END) AS ���Ə��Z��2
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_TELNO ELSE DM_JIGYO.TELNO END) AS ���Ə��d�b�ԍ�
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_FAXNO ELSE DM_JIGYO.FAXNO END) AS ���Ə�FAX�ԍ�
			,  DECODE(DM_JIGYO.JIGYOCD,'02','�֓��T�[�r�X�H��',(CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_JIGYONM ELSE DM_JIGYO.JIGYONM END)) AS ���Ə���
			-- ��2023/08/21 UPDATE TC KANDA ��������2023�N9��30���܂ł̂��̂͋��̎��Ə�����\��
			, DECODE(DM_NONYU.GINKOKBN,0,DM_JIGYO.FURIGINKONM,DM_JIGYO.TOKUGINKONM) AS ��s��
			, WK_PRT000.LOGINID 										--���O�C��ID
			, WK_PRT000.PROGID
			, WK_PRT000.KINGAKU
			-- ��2023/08/21 UPDATE TC KANDA �ŋ敪��0(�ې�)�̏ꍇ�͍��v�z��10%�̎l�̌ܓ� 1(��ې�)�̏ꍇ�͐Ŋz0
			--, WK_PRT000.ZEI AS ZEI
			--, DT_URIAGEH.BUKKENMEMO AS ����
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN WK_PRT000.ZEI ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(WK_PRT000.KINGAKU/10),0) END) AS ZEI
			, DT_URIAGEH.BUKKENMEMO AS ����
			, DT_URIAGEH.TAXKBN AS �ŋ敪
			-- ��2023/08/21 UPDATE TC KANDA �ŋ敪��0(�ې�)�̏ꍇ�͍��v�z��10%�̎l�̌ܓ� 1(��ې�)�̏ꍇ�͐Ŋz0
		FROM WK_PRT000,DT_URIAGEH,DT_URIAGEM
			,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT000.PROGID = 'OMP601'
			  AND	DT_URIAGEH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_URIAGEH.SEIKYUSHONO = WK_PRT000.DENPNO
					-- ���ׂƌ���
              AND	WK_PRT000.DENPNO = DT_URIAGEM.SEIKYUSHONO
              AND	WK_PRT000.GYONO = DT_URIAGEM.GYONO
              		-- ���Ə��}�X�^
              AND	DM_JIGYO.JIGYOCD = DT_URIAGEH.JIGYOCD
					-- �[����}�X�^
			  AND	DT_URIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00'				= DM_NONYU.SECCHIKBN
              AND	DT_URIAGEH.DELKBN = '0'
              AND	DT_URIAGEM.DELKBN = '0'
UNION ALL
--�[�i��
		SELECT
			  '2' AS �敪
			, TRUNC(( row_number() over(partition by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID ) -1)/5) AS PAGENO
			, row_number() over(partition by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,DT_URIAGEM.GYONO,WK_PRT000.LOGINID)  AS �A��
			, DT_URIAGEH.SEIKYUSHONO AS �������ԍ�						--�������ԍ�
			, ���t�L���ǉ�(DT_URIAGEH.SEIKYUYMD) AS �������t			--�������t
			, DT_URIAGEH.JIGYOCD || '-' || DT_URIAGEH.SAGYOBKBN || '-' || DT_URIAGEH.RENNO AS �����ԍ�			--�����ԍ�
			, DT_URIAGEH.ZIPCODE AS �X�֔ԍ�							--�X�֔ԍ�
			, DT_URIAGEH.ADD1 AS �Z��1									--�Z��1
			, DT_URIAGEH.ADD2 AS �Z��2									--�Z��2
			, DT_URIAGEH.SEIKYUNM AS �����於							--�����於
			, DT_URIAGEH.SENBUSHONM AS ���������						--���������
			, DT_URIAGEH.SENTANTNM AS ����S���Җ�						--����S���Җ�
			, DT_URIAGEH.NONYUNM AS �[���於							--�[���於
			, ���t�L���ǉ�(DT_URIAGEH.KAISHUYOTEIYMD) AS ����\���					--����\���
			, DT_URIAGEM.GYONO AS �s�ԍ�								--�s�ԍ�
			, DECODE(DT_URIAGEM.MMDD,NULL,NULL,SUBSTR(DT_URIAGEM.MMDD,1,2) || '/' || SUBSTR(DT_URIAGEM.MMDD,3,2))  AS ����
			, DT_URIAGEM.HINNM1 AS �i��1								--�i��1
			, DT_URIAGEM.HINNM2 AS �i��2								--�i��2
			, DT_URIAGEM.SURYO AS ����									--����
			, DT_URIAGEM.TANINM AS �P�ʖ�								--�P�ʖ�
			, DT_URIAGEM.TANKA AS �P��									--�P��
			, DT_URIAGEM.KING AS ���z									--���z
			, DT_URIAGEM.TAX AS �����									--�����
			-- ��2023/08/21 UPDATE TC KANDA ��������2023�N9��30���܂ł̂��̂͋��̎��Ə�����\��
			--, DM_JIGYO.ZIPCODE AS ���Ə��X�֔ԍ�						--
			--, DM_JIGYO.ADD1 AS ���Ə��Z��1								--
			--, DM_JIGYO.ADD2 AS ���Ə��Z��2								--
			--, DM_JIGYO.TELNO AS ���Ə��d�b�ԍ�							--
			--, DM_JIGYO.FAXNO AS ���Ə�FAX�ԍ�							--
			--, DECODE(DM_JIGYO.JIGYOCD,'02','�֓��T�[�r�X�H��',DM_JIGYO.JIGYONM) AS ���Ə���								--
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ZIPCODE ELSE DM_JIGYO.ZIPCODE END) AS ���Ə��X�֔ԍ�
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ADD1 ELSE DM_JIGYO.ADD1 END) AS ���Ə��Z��1
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ADD2 ELSE DM_JIGYO.ADD2 END) AS ���Ə��Z��2
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_TELNO ELSE DM_JIGYO.TELNO END) AS ���Ə��d�b�ԍ�
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_FAXNO ELSE DM_JIGYO.FAXNO END) AS ���Ə�FAX�ԍ�
			,  DECODE(DM_JIGYO.JIGYOCD,'02','�֓��T�[�r�X�H��',(CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_JIGYONM ELSE DM_JIGYO.JIGYONM END)) AS ���Ə���
			-- ��2023/08/21 UPDATE TC KANDA ��������2023�N9��30���܂ł̂��̂͋��̎��Ə�����\��
			, NULL AS ��s��
			, WK_PRT000.LOGINID 										--���O�C��ID
			, WK_PRT000.PROGID
			, WK_PRT000.KINGAKU
			-- ��2023/08/21 UPDATE TC KANDA �ŋ敪��0(�ې�)�̏ꍇ�͍��v�z��10%�̎l�̌ܓ� 1(��ې�)�̏ꍇ�͐Ŋz0
			--, WK_PRT000.ZEI AS ZEI
			--, DT_URIAGEH.BUKKENMEMO AS ����
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN WK_PRT000.ZEI ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(WK_PRT000.KINGAKU/10),0) END) AS ZEI
			, DT_URIAGEH.BUKKENMEMO AS ����
			, DT_URIAGEH.TAXKBN AS �ŋ敪
			-- ��2023/08/21 UPDATE TC KANDA �ŋ敪��0(�ې�)�̏ꍇ�͍��v�z��10%�̎l�̌ܓ� 1(��ې�)�̏ꍇ�͐Ŋz0
		FROM WK_PRT000,DT_URIAGEH,DT_URIAGEM
			,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT000.PROGID = 'OMP601'
			  AND	DT_URIAGEH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_URIAGEH.SEIKYUSHONO = WK_PRT000.DENPNO
					-- ���ׂƌ���
              AND	WK_PRT000.DENPNO = DT_URIAGEM.SEIKYUSHONO
              AND	WK_PRT000.GYONO = DT_URIAGEM.GYONO
              		-- ���Ə��}�X�^
              AND	DM_JIGYO.JIGYOCD = DT_URIAGEH.JIGYOCD
					-- �[����}�X�^
			  AND	DT_URIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00'				= DM_NONYU.SECCHIKBN
              AND	DT_URIAGEH.DELKBN = '0'
              AND	DT_URIAGEM.DELKBN = '0'
UNION ALL
--����`�[
		SELECT
			  '3' AS �敪
			, TRUNC(( row_number() over(partition by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID ) -1)/5) AS PAGENO
			, row_number() over(partition by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,WK_PRT000.LOGINID  order by DT_URIAGEH.JIGYOCD,DT_URIAGEH.SEIKYUSHONO,DT_URIAGEM.GYONO,WK_PRT000.LOGINID)  AS �A��
			, DT_URIAGEH.SEIKYUSHONO AS �������ԍ�						--�������ԍ�
			, ���t�L���ǉ�(DT_URIAGEH.SEIKYUYMD) AS �������t			--�������t
			, DT_URIAGEH.JIGYOCD || '-' || DT_URIAGEH.SAGYOBKBN || '-' || DT_URIAGEH.RENNO AS �����ԍ�			--�����ԍ�
			, DT_URIAGEH.ZIPCODE AS �X�֔ԍ�							--�X�֔ԍ�
			, DT_URIAGEH.ADD1 AS �Z��1									--�Z��1
			, DT_URIAGEH.ADD2 AS �Z��2									--�Z��2
			, DT_URIAGEH.SEIKYUNM AS �����於							--�����於
			, DT_URIAGEH.SENBUSHONM AS ���������						--���������
			, DT_URIAGEH.SENTANTNM AS ����S���Җ�						--����S���Җ�
			, DT_URIAGEH.NONYUNM AS �[���於							--�[���於
			, NULL AS ����\���										--����\���
			, DT_URIAGEM.GYONO AS �s�ԍ�								--�s�ԍ�
			, DECODE(DT_URIAGEM.MMDD,NULL,NULL,SUBSTR(DT_URIAGEM.MMDD,1,2) || '/' || SUBSTR(DT_URIAGEM.MMDD,3,2))  AS ����
			, DT_URIAGEM.HINNM1 AS �i��1								--�i��1
			, DT_URIAGEM.HINNM2 AS �i��2								--�i��2
			, DT_URIAGEM.SURYO AS ����									--����
			, DT_URIAGEM.TANINM AS �P�ʖ�								--�P�ʖ�
			, DT_URIAGEM.TANKA AS �P��									--�P��
			, DT_URIAGEM.KING AS ���z									--���z
			, DT_URIAGEM.TAX AS �����									--�����
			-- ��2023/08/21 UPDATE TC KANDA ��������2023�N9��30���܂ł̂��̂͋��̎��Ə�����\��
			--, DM_JIGYO.ZIPCODE AS ���Ə��X�֔ԍ�						--
			--, DM_JIGYO.ADD1 AS ���Ə��Z��1								--
			--, DM_JIGYO.ADD2 AS ���Ə��Z��2								--
			--, DM_JIGYO.TELNO AS ���Ə��d�b�ԍ�							--
			--, DM_JIGYO.FAXNO AS ���Ə�FAX�ԍ�							--
			--, DECODE(DM_JIGYO.JIGYOCD,'02','�֓��T�[�r�X�H��',DM_JIGYO.JIGYONM) AS ���Ə���								--
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ZIPCODE ELSE DM_JIGYO.ZIPCODE END) AS ���Ə��X�֔ԍ�
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ADD1 ELSE DM_JIGYO.ADD1 END) AS ���Ə��Z��1
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_ADD2 ELSE DM_JIGYO.ADD2 END) AS ���Ə��Z��2
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_TELNO ELSE DM_JIGYO.TELNO END) AS ���Ə��d�b�ԍ�
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_FAXNO ELSE DM_JIGYO.FAXNO END) AS ���Ə�FAX�ԍ�
			,  DECODE(DM_JIGYO.JIGYOCD,'02','�֓��T�[�r�X�H��',(CASE WHEN DT_URIAGEH.SEIKYUYMD < DM_JIGYO.NEWJIGYOYMD THEN DM_JIGYO.OLD_JIGYONM ELSE DM_JIGYO.JIGYONM END)) AS ���Ə���
			-- ��2023/08/21 UPDATE TC KANDA ��������2023�N9��30���܂ł̂��̂͋��̎��Ə�����\��
			, NULL AS ��s��
			, WK_PRT000.LOGINID 										--���O�C��ID
			, WK_PRT000.PROGID
			, WK_PRT000.KINGAKU
			-- ��2023/08/21 UPDATE TC KANDA �ŋ敪��0(�ې�)�̏ꍇ�͍��v�z��10%�̎l�̌ܓ� 1(��ې�)�̏ꍇ�͐Ŋz0
			--, WK_PRT000.ZEI AS ZEI
			--, DT_URIAGEH.BUKKENMEMO AS ����
			, (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN WK_PRT000.ZEI ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(WK_PRT000.KINGAKU/10),0) END) AS ZEI
			, DT_URIAGEH.BUKKENMEMO AS ����
			, DT_URIAGEH.TAXKBN AS �ŋ敪
			-- ��2023/08/21 UPDATE TC KANDA �ŋ敪��0(�ې�)�̏ꍇ�͍��v�z��10%�̎l�̌ܓ� 1(��ې�)�̏ꍇ�͐Ŋz0
		FROM WK_PRT000,DT_URIAGEH,DT_URIAGEM
			,DM_JIGYO,DM_NONYU
		WHERE
					WK_PRT000.PROGID = 'OMP601'
			  AND	DT_URIAGEH.JIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_URIAGEH.SEIKYUSHONO = WK_PRT000.DENPNO
					-- ���ׂƌ���
              AND	WK_PRT000.DENPNO = DT_URIAGEM.SEIKYUSHONO
              AND	WK_PRT000.GYONO = DT_URIAGEM.GYONO
              		-- ���Ə��}�X�^
              AND	DM_JIGYO.JIGYOCD = DT_URIAGEH.JIGYOCD
					-- �[����}�X�^
			  AND	DT_URIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
			  AND	'00'				= DM_NONYU.SECCHIKBN
              AND	DT_URIAGEH.DELKBN = '0'
              AND	DT_URIAGEM.DELKBN = '0'
			) WK
		ORDER BY WK.�������ԍ�,WK.PAGENO,WK.�敪,WK.�A��
;
