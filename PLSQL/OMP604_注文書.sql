-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP604 ������
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP604
AS
		SELECT   
              ROWNUM  AS EDANUM
			, DT_HACCHUH.HACCHUJIGYOCD AS �������Ə�CD					--�������Ə��R�[�h
			, DT_HACCHUH.HACCHUJIGYOCD || '-' || DT_HACCHUH.HACCHUNO AS �����ԍ�							--�����ԍ�
			, DT_HACCHUH.SIRCD AS �d����CD								--�d����R�[�h
			, DM_SHIRE.SIRNM1 AS �d���於1								--�d���於1
			, DM_SHIRE.SIRNM2 AS �d���於2								--�d���於2
			, DT_HACCHUH.SENTANTNM AS ����S���� 				--����S����
			, ���t�L���ǉ�(DT_HACCHUH.HACCHUYMD) AS ������				--������
			, DM_JIGYO.JIGYONM AS ���Ə���								--���Ə���
			, DM_JIGYO.ZIPCODE AS �X�֔ԍ�								--�X�֔ԍ�
			, DM_JIGYO.ADD1 AS �Z��1									--�Z��1
			, DM_JIGYO.ADD2 AS �Z��2									--�Z��2
			, DM_JIGYO.TELNO AS �d�b�ԍ�								--�d�b�ԍ�
			, DM_JIGYO.FAXNO AS FAX�ԍ�									--FAX�ԍ�
			, DT_HACCHUH.TANTCD AS �����S����CD							--�����S����CD
			, DM_TANT.TANTNM AS �S���Җ�								--�S���Җ�
			, TO_NUMBER(DT_HACCHUM.GYONO) AS �s�ԍ�						--�s�ԍ�
			, DT_HACCHUM.BBUNRUICD AS ����CD							--���ރR�[�h
			, DT_HACCHUM.BBUNRUINM AS ���ޖ�							--���ޖ�
			, DT_HACCHUM.BKIKAKUCD AS �K�iCD							--�K�i�R�[�h
			, DT_HACCHUM.BKIKAKUNM AS �K�i��							--�K�i��
			, DT_HACCHUM.HACCHUSU AS ����								--����
			, DT_HACCHUM.TANICD AS �P��CD								--�P�ʃR�[�h
			, DM_TANI.TANINM AS �P�ʖ�									--�P�ʖ�
			, DT_HACCHUM.NONYUKBN AS �[���ꏊ�敪						--�[���ꏊ�敪
			, DK_NONYU.NONYUKBNNM AS �[���ꏊ							--�[���ꏊ
			, ���t�L���ǉ�(DT_HACCHUM.NONYUYMD) AS �[�����t				--�[�����t
			, DT_HACCHUM.NOKIKBN AS �[���敪							--�[���敪
			, DK_NOKI.NOKIKBNNM AS �[���敪��							--�[���敪��
			, DT_HACCHUM.BUKKENNM AS ������								--������
			, ���t�L���ǉ�(DT_HACCHUM.KOJIYOTEIYMD) AS �H���\���		--�H���\���
			, DT_HACCHUM.JIGYOCD || '-' || DT_HACCHUM.SAGYOBKBN || '-' || DT_HACCHUM.RENNO AS �����ԍ�
			, DT_HACCHUH.BIKO AS ���l									--���l
			, DT_HACCHUH.BIKO1 AS ���l�P								--���l
			, DT_HACCHUH.BIKO2 AS ���l�Q								--���l
			, WK_PRT000.LOGINID 										--���O�C��ID
		FROM WK_PRT000,DT_HACCHUH,DT_HACCHUM
			,DM_SHIRE,DM_JIGYO,DM_TANT,DM_TANI
			,DK_NONYU,DK_NOKI
		WHERE
					WK_PRT000.PROGID = 'OMP604'
			  AND	DT_HACCHUH.HACCHUJIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_HACCHUH.HACCHUNO = WK_PRT000.DENPNO
					-- ���ׂƌ���
              AND	DT_HACCHUH.HACCHUJIGYOCD = DT_HACCHUM.HACCHUJIGYOCD
              AND	DT_HACCHUH.HACCHUNO = DT_HACCHUM.HACCHUNO
              AND	WK_PRT000.GYONO = DT_HACCHUM.GYONO
              		-- �d����}�X�^
              AND	DM_SHIRE.SIRCD(+) = DT_HACCHUH.SIRCD
              		-- ���Ə��}�X�^
              AND	DM_JIGYO.JIGYOCD = DT_HACCHUH.HACCHUJIGYOCD
              		-- �S���҃}�X�^
              AND	DM_TANT.TANTCD = DT_HACCHUH.TANTCD
              		-- �P�ʃ}�X�^
              AND	DM_TANI.TANICD(+) = DT_HACCHUM.TANICD
              		-- �[���ꏊ�敪�}�X�^
              AND	DK_NONYU.NONYUKBN(+) = DT_HACCHUM.NONYUKBN
              		-- �[���敪�}�X�^
              AND	DK_NOKI.NOKIKBN(+) = DT_HACCHUM.NOKIKBN
              AND	DT_HACCHUH.DELKBN = '0'
              AND	DT_HACCHUM.DELKBN = '0'
        ORDER BY
        			  DT_HACCHUH.HACCHUJIGYOCD
        			, DT_HACCHUH.HACCHUNO
        			, DT_HACCHUH.HACCHUYMD
        			, TO_NUMBER(DT_HACCHUM.GYONO)
;
