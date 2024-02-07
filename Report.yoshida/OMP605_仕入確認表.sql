-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP605 �d���m�F�\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP605
AS
		SELECT   
              ROWNUM  AS EDANUM
			, DT_SHIREH.SIRJIGYOCD AS �d�����Ə�CD						--�d�����Ə��R�[�h
			, DT_SHIREH.SIRNO AS �d���ԍ�								--�d���ԍ�
			, DT_SHIREH.SIRTORICD AS ����敪							--����敪
			, DK_SIRTORI.SIRTORICDNM AS ����敪��						--����敪��
			, ���t�L���ǉ�(DT_SHIREH.SIRYMD) AS �d�����t				--�d�����t
			, DT_SHIREH.SIRCD AS �d����CD								--�d����R�[�h
			, DM_SHIRE.SIRNM1 AS �d���於								--�d���於
--			, TO_NUMBER(DT_SHIREM.GYONO) AS �s�ԍ�						--�s�ԍ�
			, ROW_NUMBER() OVER(PARTITION BY DT_SHIREH.SIRJIGYOCD,DT_SHIREH.SIRNO,WK_PRT000.LOGINID ORDER BY  DT_SHIREH.SIRJIGYOCD,DT_SHIREH.SIRNO,DT_SHIREM.GYONO)  AS �s�ԍ�						--�s�ԍ�
			, DT_SHIREM.BBUNRUICD || DT_SHIREM.BKIKAKUCD AS ���iCD		--���i�R�[�h
			, DT_SHIREM.BKIKAKUNM AS ���i��								--���i��
			, DT_SHIREM.SIRSU AS ����									--����
			, DT_SHIREM.TANICD AS �P��CD								--�P�ʃR�[�h
			, DECODE(DT_SHIREH.SIRTORICD,2,NULL,DM_TANI.TANINM) AS �P�ʖ�									--�P�ʖ�
			, DT_SHIREM.SIRTANK AS �P��									--�P��
			, DT_SHIREM.SIRKIN AS ���z									--���z
			, DT_SHIREM.TAX AS �����									--�����
			, (DT_SHIREM.SIRKIN + DT_SHIREM.TAX ) AS ���v			--���v
			, DT_SHIREM.BUMONCD AS ����CD								--����R�[�h
			, DM_BUMON.BUMONNM AS ���喼								--���喼
			, DECODE(DT_SHIREH.SIRTORICD,2,NULL,DT_SHIREM.JIGYOCD || '-' || DT_SHIREM.SAGYOBKBN || '-' || DT_SHIREM.RENNO) AS �����ԍ�			--�����ԍ�
			, WK_PRT000.LOGINID 										--���O�C��ID
		FROM WK_PRT000,DT_SHIREH,DT_SHIREM,DM_SHIRE,DM_TANI,DM_BUMON,DK_SIRTORI
		WHERE
					WK_PRT000.PROGID = 'OMP605'
			  AND	DT_SHIREH.SIRJIGYOCD = WK_PRT000.EIGCD
			  AND 	DT_SHIREH.SIRNO = WK_PRT000.DENPNO
					-- ���ׂƌ���
              AND	DT_SHIREH.SIRJIGYOCD = DT_SHIREM.SIRJIGYOCD
              AND	DT_SHIREH.SIRNO = DT_SHIREM.SIRNO
              AND	WK_PRT000.GYONO = DT_SHIREM.GYONO
              		-- �d����}�X�^
              AND	DM_SHIRE.SIRCD(+) = DT_SHIREH.SIRCD
              		-- �P�ʃ}�X�^
              AND	DM_TANI.TANICD(+) = DT_SHIREM.TANICD
              		-- ����}�X�^
              AND	DM_BUMON.BUMONCD(+) = DT_SHIREM.BUMONCD
              		-- �d������敪�}�X�^
              AND	DK_SIRTORI.SIRTORICD = DT_SHIREH.SIRTORICD
              AND	DT_SHIREH.DELKBN = '0'
              AND	DT_SHIREM.DELKBN = '0'
        ORDER BY
        			  DT_SHIREH.SIRJIGYOCD
        			, DT_SHIREH.SIRNO
        			, DT_SHIREH.SIRYMD
        			, TO_NUMBER(DT_SHIREM.GYONO)
;
