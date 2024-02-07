-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP001 �[���揇�ڋq�䒠
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP001
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_NONYU.JIGYOCD AS ���Ə�CD									--���Ə��R�[�h
            , DM_JIGYO.JIGYONM AS ���Ə���									--���Ə���
			, DECODE(DM_NONYU.SEIKYUSAKICD1,NULL,NULL,'�̏�1') AS �̏�1		--
            , DM_NONYU.SEIKYUSAKICD1 AS �̏�C��������CD11					--�̏�C��������P�R�[�h�P
            , K1.NONYUNM1 AS �̏�C�������於11								--�̏�C��������P���P
			, K1.ZIPCODE AS �̏�C��������X�֔ԍ�1							--�̏�C��������P�X�֔ԍ��P
			, K1.ADD1 AS �̏�C��������Z��11								--�̏�C��������P�Z���P
			, K1.ADD2 AS �̏�C��������Z��12								--�̏�C��������P�Z���Q
			, K1.TELNO1 AS �̏�C��������d�b�ԍ�11							--�̏�C��������P�d�b�ԍ��P
			, K1.TELNO2 AS �̏�C��������d�b�ԍ�12							--�̏�C��������P�d�b�ԍ��Q
			, DECODE(DM_NONYU.SEIKYUSAKICD2,NULL,NULL,'�̏�2') AS �̏�2		--
            , DM_NONYU.SEIKYUSAKICD2 AS �̏�C��������CD21					--�̏�C��������Q�R�[�h�Q
            , K2.NONYUNM1 AS �̏�C�������於21								--�̏�C��������Q���Q
			, K2.ZIPCODE AS �̏�C��������X�֔ԍ�21						--�̏�C��������Q�X�֔ԍ��P
			, K2.ADD1 AS �̏�C��������Z��21								--�̏�C��������Q�Z���P
			, K2.ADD2 AS �̏�C��������Z��22								--�̏�C��������Q�Z���Q
			, K2.TELNO1 AS �̏�C��������d�b�ԍ�21							--�̏�C��������Q�d�b�ԍ��P
			, K2.TELNO2 AS �̏�C��������d�b�ԍ�22							--�̏�C��������Q�d�b�ԍ��Q
			, DECODE(DM_NONYU.SEIKYUSAKICD3,NULL,NULL,'�̏�3') AS �̏�3		--
            , DM_NONYU.SEIKYUSAKICD3 AS �̏�C��������CD31					--�̏�C��������R�R�[�h�R
            , K3.NONYUNM1 AS �̏�C�������於31								--�̏�C��������R���R
			, K3.ZIPCODE AS �̏�C��������X�֔ԍ�31						--�̏�C��������R�X�֔ԍ��P
			, K3.ADD1 AS �̏�C��������Z��31								--�̏�C��������R�Z���P
			, K3.ADD2 AS �̏�C��������Z��32								--�̏�C��������R�Z���Q
			, K3.TELNO1 AS �̏�C��������d�b�ԍ�31							--�̏�C��������R�d�b�ԍ��P
			, K3.TELNO2 AS �̏�C��������d�b�ԍ�32							--�̏�C��������R�d�b�ԍ��Q
			, DECODE(DM_NONYU.SEIKYUSAKICDH,NULL,NULL,'�ێ�') AS �ێ�		--
            , DM_NONYU.SEIKYUSAKICDH AS �ێ�_��������CD					--�ێ�_��������R�R�[�h�R
            , H1.NONYUNM1 AS �ێ�_�������於								--�ێ�_��������R���R
			, H1.ZIPCODE AS �ێ�_��������X�֔ԍ�							--�ێ�_��������R�X�֔ԍ��P
			, H1.ADD1 AS �ێ�_��������Z��1								--�ێ�_��������R�Z���P
			, H1.ADD2 AS �ێ�_��������Z��2								--�ێ�_��������R�Z���Q
			, H1.TELNO1 AS �ێ�_��������d�b�ԍ�1							--�ێ�_��������R�d�b�ԍ��P
			, H1.TELNO2 AS �ێ�_��������d�b�ԍ�2							--�ێ�_��������R�d�b�ԍ��Q
			, DECODE(DM_NONYU.NONYUCD,NULL,NULL,'�[��') AS �[��				--
			, DM_NONYU.NONYUCD AS �[����CD									--�[����R�[�h
			, DM_NONYU.NONYUNM1 AS �[���於									--�[���於
			, DM_NONYU.ZIPCODE AS �X�֔ԍ�									--�X�֔ԍ�
			, DM_NONYU.ADD1 AS �Z��1										--�Z���P
			, DM_NONYU.ADD2 AS �Z��2										--�Z���Q
			, DM_NONYU.TELNO1 AS �d�b�ԍ�1									--�d�b�ԍ��P
			, DM_NONYU.TELNO2 AS �d�b�ԍ�2									--�d�b�ԍ��Q
			, DM_NONYU.MOCHINUSHI AS ����������								--����������
			, DM_NONYU.KIGYOCD AS ���CD									--��ƃR�[�h
			, DECODE(DM_KIGYO.KIGYONM,NULL,'��ƃ}�X�^�ɖ���',DM_KIGYO.KIGYONM) AS ��Ɩ�									--��Ɩ�
			, DM_KIGYO.BUSHONM AS ������									--������
			, DM_KIGYO.HACCHUTANTNM AS �Ǘ��S���Җ�							--�Ǘ��S���Җ�
			, DM_TANT.TANTNM AS �S���Җ�									--�S���Җ�
			, DM_HOSHU.GOUKI AS ���@										--���@
			, DM_HOSHU.KISHUKATA AS �@��									--�@��
			, DM_HOSHU.YOSHIDANO AS ���V�_�H��								--���V�_�H��
			, DECODE(DM_HOSHU.SECCHIYMD,NULL,NULL,SUBSTR(DM_HOSHU.SECCHIYMD,1,4) || '/' || SUBSTR(DM_HOSHU.SECCHIYMD,5,2)) AS �ݒu�N��
			, RTRIM(DECODE(DM_HOSHU.HOSHUM1,0,'','1,') || DECODE(DM_HOSHU.HOSHUM2,0,'','2,') || DECODE(DM_HOSHU.HOSHUM3,0,'','3,') ||
			  DECODE(DM_HOSHU.HOSHUM4,0,'','4,') || DECODE(DM_HOSHU.HOSHUM5,0,'','5,') || DECODE(DM_HOSHU.HOSHUM6,0,'','6,') ||
			  DECODE(DM_HOSHU.HOSHUM7,0,'','7,') || DECODE(DM_HOSHU.HOSHUM8,0,'','8,') || DECODE(DM_HOSHU.HOSHUM9,0,'','9,') ||
			  DECODE(DM_HOSHU.HOSHUM10,0,'','10,') || DECODE(DM_HOSHU.HOSHUM11,0,'','11,') || DECODE(DM_HOSHU.HOSHUM12,0,'','12,'),',') AS �ێ�Ή�
			, �o�ߔN��(DM_HOSHU.SECCHIYMD) AS �o�ߔN��						--
			, ���t�L���ǉ�(DM_HOSHU.KEIYAKUYMD) AS �_��N����				--
			, DM_HOSHU.KEIYAKUKING AS �_����z								--
			, DK_HOSHU.HOSHUKBNNM AS �v�Z���@								--
			, DECODE(DM_HOSHU.KEIYAKUYMD,NULL,'0','1') AS �_��敪			--
			, DM_NONYU.HURIGANA AS �t���K�i
		FROM DM_NONYU,DM_JIGYO,
			 DM_NONYU K1,DM_NONYU K2,DM_NONYU K3,
			 DM_NONYU H1,DM_KIGYO,DM_HOSHU,DM_TANT,DK_HOSHU
		WHERE
					DM_NONYU.SECCHIKBN = '01'
					-- ���Ə��}�X�^�ƌ���
              AND	DM_NONYU.JIGYOCD = DM_JIGYO.JIGYOCD
              		-- �̏�C��������P
              AND	K1.JIGYOCD(+) = DM_NONYU.JIGYOCD
              AND	K1.NONYUCD(+) = DM_NONYU.SEIKYUSAKICD1
              AND	K1.SECCHIKBN(+) = '00'
              		-- �̏�C��������Q
              AND	K2.JIGYOCD(+) = DM_NONYU.JIGYOCD
              AND	K2.NONYUCD(+) = DM_NONYU.SEIKYUSAKICD2
              AND	K2.SECCHIKBN(+) = '00'              
              		-- �̏�C��������R
              AND	K3.JIGYOCD(+) = DM_NONYU.JIGYOCD
              AND	K3.NONYUCD(+) = DM_NONYU.SEIKYUSAKICD3
              AND	K3.SECCHIKBN(+) = '00'              
              		-- �ێ�_��������
              AND	H1.JIGYOCD(+) = DM_NONYU.JIGYOCD
              AND	H1.NONYUCD(+) = DM_NONYU.SEIKYUSAKICDH
              AND	H1.SECCHIKBN(+) = '00'
              		-- ��ƃ}�X�^
              AND	DM_KIGYO.KIGYOCD(+) = DM_NONYU.KIGYOCD
              AND	DM_KIGYO.DELKBN(+) = '0'
              		-- �S���҃}�X�^
              AND	DM_TANT.TANTCD(+) = DM_KIGYO.EIGYOTANTCD
              		-- �ێ�_���}�X�^
              AND	DM_HOSHU.NONYUCD = DM_NONYU.NONYUCD
              AND	DM_HOSHU.DELKBN	 = '0'
              		-- �ێ�v�Z�敪�}�X�^
              AND	DK_HOSHU.HOSHUKBN = DM_HOSHU.HOSHUKBN
		ORDER BY
					  DM_NONYU.JIGYOCD
					, DM_NONYU.NONYUCD
					, DM_HOSHU.GOUKI
;
