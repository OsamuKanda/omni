-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2015/5/11   KAWAHATA
--                                                 Update:
-------------------------------------------------------------------------------
--OMP202 �_�����я󋵕\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW OMNI.V_OMP202
    (���Ə�CD,���Ə���,�����ԍ�,��t���t,�[����CD,�[���於,������CD,�����於,��ƒS����CD,
    ��ƒS���Җ�,�啪��CD,�啪�ޖ�,�_�����t,�_���䐔,�����p��t���t,�����p�_�����t,�����p��ƕ���,�������z,�_��)
AS
SELECT
				 ���Ə�CD
				,���Ə���
				,�����ԍ�
				,��t���t
				,�[����CD
				,�[���於
				,������CD
				,�����於
				,��ƒS����CD
				,��ƒS���Җ�
				,�啪��CD
				,�啪�ޖ�
				,�_�����t
				,�_���䐔
				,�����p��t���t
				,�����p�_�����t
				,�����p��ƕ���
				, SUM(DT_URIAGEM.KING)	 AS �������z	
				,DECODE(�_���, NULL, '��','�L') AS �_��
FROM																			--�������z�i�Ŕ��j
			(SELECT
			  DT_BUKKEN.JIGYOCD AS ���Ə�CD																			--���Ə��R�[�h
			, DT_BUKKEN.SAGYOBKBN AS ��Ƌ敪
			, DT_BUKKEN.RENNO AS �A
			, MAX(DM_JIGYO.JIGYONM) AS ���Ə���																		--���Ə���
			, DT_BUKKEN.JIGYOCD || '-' || DT_BUKKEN.SAGYOBKBN || '-' || DT_BUKKEN.RENNO AS �����ԍ�					--�����ԍ�
			, ���t�L���ǉ�(DT_BUKKEN.UKETSUKEYMD) AS ��t���t														--��t���t
			, DT_BUKKEN.NONYUCD AS �[����CD																			--�[����R�[�h
			, MAX(N1.NONYUNMR) AS �[���於																				--�[���於
			, DT_BUKKEN.SEIKYUCD AS ������CD																		--������R�[�h
			, MAX(S1.NONYUNMR) AS �����於																				--�����於
			, DT_BUKKENTANT.SAGYOTANTCD1 AS ��ƒS����CD															--��ƒS���҃R�[�h
			, MAX(SAGYO.TANTNM) AS ��ƒS���Җ�																			--��ƒS���Җ�
			, DT_BUKKEN.BUNRUIDCD AS �啪��CD																		--�啪�ރR�[�h
			, MAX(DM_BUNRUID.BUNRUIDNM) AS �啪�ޖ�	
			, ���t�L���ǉ�(DT_HTENKENH.TENKENYMD) AS �_�����t																					--�_�����t
			, COUNT(DT_HTENKENH.GOUKI) AS �_���䐔																			--�_���䐔
			, DT_BUKKEN.UKETSUKEYMD AS �����p��t���t
			, DT_HTENKENH.TENKENYMD AS �����p�_�����t
			, DT_BUKKEN.SAGYOBKBN AS �����p��ƕ���
			--, DT_BUKKEN.SEIKYUKBN AS ������ԋ敪
			, MAX(DM_HOSHU.KEIYAKUYMD) AS �_���
			FROM DM_JIGYO
				,DT_BUKKEN
				,DT_HTENKENH
				,DM_NONYU N1
				,DM_NONYU S1
				,DM_TANT SAGYO
				,DM_BUNRUID
				,DT_BUKKENTANT
				,DM_HOSHU
			WHERE 
			-- ���Ə��}�X�^�ƌ���
            DT_BUKKEN.JIGYOCD = DM_JIGYO.JIGYOCD
            --�_���}�X�^�ƌ���
			AND DT_BUKKEN.JIGYOCD = DT_HTENKENH.JIGYOCD	
			AND DT_BUKKEN.SAGYOBKBN = DT_HTENKENH.SAGYOBKBN
			AND DT_BUKKEN.RENNO = DT_HTENKENH.RENNO
			--�[����}�X�^
			 AND	N1.NONYUCD(+) = DT_BUKKEN.NONYUCD
			 AND	N1.SECCHIKBN(+) = '01'
  			-- ������}�X�^
			 AND	S1.NONYUCD(+) = DT_BUKKEN.SEIKYUCD
		 	AND	S1.SECCHIKBN(+) = '00'
			-- �啪�ރ}�X�^
			 AND	DM_BUNRUID.BUNRUIDCD(+) = DT_BUKKEN.BUNRUIDCD
              		-- �����ʍ�ƒS���҃}�X�^
              AND	DT_BUKKEN.JIGYOCD = DT_BUKKENTANT.JIGYOCD(+)
              AND	DT_BUKKEN.SAGYOBKBN = DT_BUKKENTANT.SAGYOBKBN(+)
			  AND	DT_BUKKEN.RENNO = DT_BUKKENTANT.RENNO(+)
			  		--
			  AND	DT_BUKKENTANT.SAGYOTANTCD1 = SAGYO.TANTCD(+)
			--�����敪��������
			AND SEIKYUKBN = '1'
			--�ێ�_���}�X�^
			AND	DM_HOSHU.NONYUCD(+) = DT_HTENKENH.NONYUCD
			AND	DM_HOSHU.GOUKI(+) = DT_HTENKENH.GOUKI	
			GROUP BY 
			 DT_BUKKEN.JIGYOCD
			,DT_BUKKEN.SAGYOBKBN
			,DT_BUKKEN.RENNO 
			,DT_BUKKEN.NONYUCD	
			,DT_BUKKEN.SEIKYUCD
			,DT_HTENKENH.TENKENYMD	
			,DT_BUKKENTANT.SAGYOTANTCD1
			,DT_BUKKEN.BUNRUIDCD
			,DT_BUKKEN.UKETSUKEYMD) BUKKEN
				,DT_URIAGEH
				,DT_URIAGEM
			WHERE
			  --����ƌ���
			    BUKKEN.���Ə�CD = DT_URIAGEH.JIGYOCD	
			AND BUKKEN.��Ƌ敪 = DT_URIAGEH.SAGYOBKBN
			AND BUKKEN.�A = DT_URIAGEH.RENNO
			AND DT_URIAGEH.SEIKYUSHONO =  DT_URIAGEM.SEIKYUSHONO	
			GROUP BY
			 	���Ə�CD
				,��Ƌ敪
				,�A
				,���Ə���
				,�����ԍ�
				,��t���t
				,�[����CD
				,�[���於
				,������CD
				,�����於
				,��ƒS����CD
				,��ƒS���Җ�
				,�啪��CD
				,�啪�ޖ�
				,�_�����t
				,�_���䐔
				,�����p��t���t
				,�����p�_�����t
				,�����p��ƕ���
				,�_���
			ORDER BY
         		���Ə�CD
				,��Ƌ敪
				,�A
/