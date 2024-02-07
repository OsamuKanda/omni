-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP703	���|����������ʈꗗ�\
-------------------------------------------------------------------------------
CREATE OR REPLACE FORCE VIEW "OMNI"."V_OMP703" ("EDANUM", "���Ə�CD", "���Ə���", "�������t", "������CD", "�����於", "�[����CD", "�[���於", "�������ԍ�", "�����ԍ�", "�������z", "�݌v�����z", "����\���", "�d�b1", "�d�b2", "�����N��", "�����p�������t", "�����p����\���") AS 
  SELECT
              ROWNUM  AS EDANUM
			, DT_URIAGEH.JIGYOCD AS ���Ə�CD										--���Ə��R�[�h
			, DM_JIGYO.JIGYONM AS ���Ə���											--���Ə���
			, ���t�L���ǉ�(DT_URIAGEH.SEIKYUYMD) AS �������t						--�������t
			, DT_URIAGEH.SEIKYUCD AS ������CD										--������R�[�h
			, DT_URIAGEH.SEIKYUNM AS �����於										--�����於
			, DT_URIAGEH.NONYUCD AS �[����CD										--�[����R�[�h
			, DT_URIAGEH.NONYUNM AS �[���於										--�[���於
			, DT_URIAGEH.SEIKYUSHONO AS �������ԍ�									--�������ԍ�
			, DT_URIAGEH.JIGYOCD || '-' || DT_URIAGEH.SAGYOBKBN || '-' || DT_URIAGEH.RENNO AS �����ԍ�
			, T1.�������z															--�������z
			, DT_URIAGEH.NYUKINR AS �݌v�����z										--�݌v�����z
			, ���t�L���ǉ�(DT_URIAGEH.KAISHUYOTEIYMD) AS ����\���					--����\���
			, DM_NONYU.TELNO1 AS �d�b1												--�d�b1
			, DM_NONYU.TELNO2 AS �d�b2												--�d�b2
			, SUBSTR(DT_URIAGEH.SEIKYUYMD,1,6) AS �����N��							--
			, DECODE(DT_URIAGEH.SEIKYUYMD,NULL,'00000000',DT_URIAGEH.SEIKYUYMD) AS �����p�������t
			, DECODE(DT_URIAGEH.KAISHUYOTEIYMD,NULL,'00000000',DT_URIAGEH.KAISHUYOTEIYMD)  AS �����p����\���
		FROM
				-- ���㖾�ׂ��琿���ԍ����̔�����z���Z�o
			--��2023.09.18 Update Kanda 2023/10/01�ȍ~�̏���Ōv�Z�ɑΉ��j
			--(	SELECT DT_URIAGEH.SEIKYUSHONO,SUM(KING + TAX) AS �������z FROM DT_URIAGEM,DT_URIAGEH
			--	WHERE
			--		DT_URIAGEM.DELKBN = '0'
			--	AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM. SEIKYUSHONO
			--	AND DT_URIAGEH.DELKBN = '0'
			--	AND DT_URIAGEH.DENPYOKBN = '0'
			--	GROUP BY
			--	DT_URIAGEH.SEIKYUSHONO	)
				--��2023.09.18 Update Kanda 2023/10/01�ȍ~�̏���Ōv�Z�ɑΉ��j
			(	SELECT
						DT_URIAGEH.SEIKYUSHONO
					,	SUM(DT_URIAGEM.KING) + (CASE WHEN DT_URIAGEH.SEIKYUYMD < '20231001' THEN SUM(DT_URIAGEM.TAX) ELSE DECODE(DT_URIAGEH.TAXKBN,'0',ROUND(SUM(DT_URIAGEM.KING)/10),0) END)  AS �������z
				FROM
					DT_URIAGEM,DT_URIAGEH
				WHERE
						DT_URIAGEM.DELKBN = '0'
					AND DT_URIAGEH.SEIKYUSHONO = DT_URIAGEM. SEIKYUSHONO
					AND DT_URIAGEH.DELKBN = '0'
					AND DT_URIAGEH.DENPYOKBN = '0'
				GROUP BY
					DT_URIAGEH.SEIKYUSHONO,DT_URIAGEH.TAXKBN,DT_URIAGEH.SEIKYUYMD
				--��2023.09.18 Update Kanda 2023/10/01�ȍ~�̏���Ōv�Z�ɑΉ��j
			)
			T1,
			DT_URIAGEH,DM_NONYU,DM_JIGYO
		WHERE
				--   ��L�Ŏ擾���������ԍ��œ����z���擾��������s���B
					 T1.SEIKYUSHONO		=	DT_URIAGEH. SEIKYUSHONO
				--	 �����z > �݌v�����z
				AND  T1.�������z > DT_URIAGEH.NYUKINR
				--	 �[����}�X�^
				AND  DT_URIAGEH.SEIKYUCD = DM_NONYU.NONYUCD
				AND  '00' = DM_NONYU.SECCHIKBN
				-- ���Ə��}�X�^
				AND  DT_URIAGEH.JIGYOCD = DM_JIGYO.JIGYOCD
        ORDER BY
        			  DT_URIAGEH.SEIKYUYMD
        			, DT_URIAGEH.SEIKYUCD
        			, DT_URIAGEH.SEIKYUSHONO
;
