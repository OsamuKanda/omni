-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:2012/02/22   OKADA
--��ƒS���Җ��̒ǉ�
-------------------------------------------------------------------------------
--OMP201 ��t�󋵕\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP201
AS
SELECT
              ROWNUM  AS EDANUM
            , DT_BUKKEN.JIGYOCD AS ���Ə�CD																			--���Ə��R�[�h
            , DM_JIGYO.JIGYONM AS ���Ə���																			--���Ə���
			, DT_BUKKEN.JIGYOCD || '-' || DT_BUKKEN.SAGYOBKBN || '-' || DT_BUKKEN.RENNO AS �����ԍ�					--�����ԍ�
			, ���t�L���ǉ�(DT_BUKKEN.UKETSUKEYMD) AS ��t���t														--��t���t
			, DT_BUKKEN.NONYUCD AS �[����CD																			--�[����R�[�h
			, N1.NONYUNMR AS �[���於																				--�[���於
			, DT_BUKKEN.SEIKYUCD AS ������CD																		--������R�[�h
			, S1.NONYUNMR AS �����於																				--�����於
			, DT_BUKKEN.TANTCD AS ��t�S����CD																		--��t�S���҃R�[�h
			, DM_TANT.TANTNM AS �S���Җ�																			--�S���Җ�
			, DT_BUKKENTANT.SAGYOTANTCD1 AS ��ƒS����CD															--��ƒS���҃R�[�h
			, SAGYO.TANTNM AS ��ƒS���Җ�																			--��ƒS���Җ�
			, DT_BUKKEN.BUNRUIDCD AS �啪��CD																		--�啪�ރR�[�h
			, DM_BUNRUID.BUNRUIDNM AS �啪�ޖ�																		--�啪�ޖ�
			, DT_BUKKEN.BUNRUICCD AS ������CD																		--�����ރR�[�h
			, DM_BUNRUIC.BUNRUICNM AS �����ޖ�																		--�����ޖ�
			, DT_BUKKEN.UKETSUKEKBN AS ��t�敪																		--��t�敪
			, DK_UKETSUKE.UKETSUKEKBNNM AS ��t�敪��																--��t�敪��
			, DT_BUKKEN.SAGYOKBN AS ��Ƌ敪																		--��Ƌ敪
			, DK_UMU.UMUKBNNM AS �L���敪��																			--�L���敪��
			,	CASE WHEN DT_BUKKEN.CHOKIKBN = '1'  THEN
				 '����'
					 WHEN DT_BUKKEN.CHOKIKBN >= '2'  THEN
				 '�����s��'
					 ELSE
					  DK_SEIKYU.SEIKYUKBNNM
				END		AS �����敪��																				--�����敪��
			, DECODE(DT_BUKKEN.CHOKIKBN,NULL,'0',DT_BUKKEN.CHOKIKBN) AS �����敪										--�����敪
			, DK_CHOKI.CHOKIKBNNM AS �����敪��																		--�����敪��
			, DT_BUKKEN.BIKO AS ���l																				--���l
			, DT_BUKKEN.UKETSUKEYMD AS �����p��t���t
			, DT_BUKKEN.SAGYOBKBN AS �����p��ƕ���
			, DT_BUKKEN.SEIKYUKBN AS ������ԋ敪
		FROM DT_BUKKEN,DM_NONYU N1,DM_NONYU S1,DM_TANT,DM_BUNRUID,DM_BUNRUIC,DK_UKETSUKE,
			 DK_UMU,DK_CHOKI,DK_SEIKYU,DM_JIGYO,DM_TANT SAGYO,DT_BUKKENTANT
		WHERE
					-- ���Ə��}�X�^�ƌ���
              		DT_BUKKEN.JIGYOCD = DM_JIGYO.JIGYOCD
              		-- �[����}�X�^
              AND	N1.NONYUCD(+) = DT_BUKKEN.NONYUCD
              AND	N1.SECCHIKBN(+) = '01'
              		-- ������}�X�^
              AND	S1.NONYUCD(+) = DT_BUKKEN.SEIKYUCD
              AND	S1.SECCHIKBN(+) = '00'
              		-- �S���҃}�X�^
              AND	DM_TANT.TANTCD(+) = DT_BUKKEN.TANTCD
              		-- �啪�ރ}�X�^
              AND	DM_BUNRUID.BUNRUIDCD(+) = DT_BUKKEN.BUNRUIDCD
              		-- �����ރ}�X�^
              AND	DM_BUNRUIC.BUNRUICCD(+) = DT_BUKKEN.BUNRUICCD
              		-- ��t�敪�}�X�^
              AND	DK_UKETSUKE.UKETSUKEKBN(+) = DT_BUKKEN.UKETSUKEKBN
              		-- ��ƗL���敪�}�X�^
              AND	DK_UMU.UMUKBN(+) = DT_BUKKEN.SAGYOKBN
              		-- �����敪�}�X�^
              AND	DK_SEIKYU.SEIKYUKBN(+) = DT_BUKKEN.SEIKYUKBN
              		-- �����敪�}�X�^
              AND	DK_CHOKI.CHOKIKBN(+) = DT_BUKKEN.CHOKIKBN
              AND	DT_BUKKEN.DELKBN = '0'
              		-- �����ʍ�ƒS���҃}�X�^
              AND	DT_BUKKEN.JIGYOCD = DT_BUKKENTANT.JIGYOCD(+)
              AND	DT_BUKKEN.SAGYOBKBN = DT_BUKKENTANT.SAGYOBKBN(+)
			  AND	DT_BUKKEN.RENNO = DT_BUKKENTANT.RENNO(+)
			  		--
			  AND	DT_BUKKENTANT.SAGYOTANTCD1 = SAGYO.TANTCD(+)
         ORDER BY
         			  DT_BUKKEN.JIGYOCD
         			, DT_BUKKEN.SAGYOBKBN
         			, DT_BUKKEN.RENNO
;
