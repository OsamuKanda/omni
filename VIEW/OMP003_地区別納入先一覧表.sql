-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP003 �n��ʔ[����ꗗ�\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP003
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_NONYU.JIGYOCD AS ���Ə�CD									--���Ə��R�[�h
            , DM_JIGYO.JIGYONM AS ���Ə���									--���Ə���
            , DM_NONYU.AREACD AS �n��CD										--�n��R�[�h
            , DM_AREA.AREANM AS �n�於										--�n�於
			, DM_NONYU.NONYUCD AS �[����CD									--�[����R�[�h
			, DM_NONYU.NONYUNM1 AS �[���於1								--�[���於�P
			, DM_NONYU.NONYUNM2 AS �[���於2								--�[���於�Q
			, DM_NONYU.EIGYOTANTCD AS �c�ƒS��CD							--�c�ƒS���R�[�h
			, DM_TANT.TANTNM AS �c�ƒS���Җ�								--�c�ƒS���Җ�
			, DM_HOSHU.GOUKI AS ���@										--���@
			, DECODE(DM_HOSHU.SECCHIYMD,NULL,NULL,SUBSTR(DM_HOSHU.SECCHIYMD,1,4) || '/' || SUBSTR(DM_HOSHU.SECCHIYMD,5,2)) AS �ݒu�N��
			, ���t�L���ǉ�(DM_HOSHU.KEIYAKUYMD) AS �_��N����				--�_��N����
			, ���t�L���ǉ�(DM_HOSHU.HOSHUSTARTYMD) AS �ێ�v�Z�J�n��		--�ێ�v�Z�J�n��
			, 1 AS �䐔														--�䐔
			, DECODE(DM_HOSHU.KEIYAKUYMD,NULL,1,0) AS �_��䐔				--�_��䐔
			, DM_HOSHU.TANTCD AS �S��CD										--�S���R�[�h
			, T1.TANTNM AS �S���Җ�											--�S���Җ�
			, DM_HOSHU.SAGYOUTANTCD AS ��ƒS����CD							--��ƒS���҃R�[�h
			, T2.TANTNM AS ��ƒS���Җ�										--��ƒS���Җ�
		FROM DM_NONYU,DM_JIGYO,DM_AREA,DM_TANT,DM_HOSHU,DM_TANT T1,DM_TANT T2
		WHERE
					DM_NONYU.SECCHIKBN = '01'
					-- ���Ə��}�X�^�ƌ���
              AND	DM_NONYU.JIGYOCD = DM_JIGYO.JIGYOCD
					-- �n��}�X�^�ƌ���
              AND	DM_NONYU.AREACD = DM_AREA.AREACD(+)
              		-- �S���҃}�X�^
              AND	DM_TANT.TANTCD(+) = DM_NONYU.EIGYOTANTCD
              		-- �ێ�_���}�X�^
              AND	DM_HOSHU.NONYUCD = DM_NONYU.NONYUCD
              AND	DM_HOSHU.DELKBN	 = '0'
              		-- �S���҃}�X�^
              AND	T1.TANTCD(+) = DM_HOSHU.TANTCD
              		-- ��ƒS���҃}�X�^
              AND	T2.TANTCD(+) = DM_HOSHU.SAGYOUTANTCD
        ORDER BY
        			  DM_NONYU.JIGYOCD
					, DM_NONYU.AREACD
					, DM_NONYU.NONYUCD
					, DM_HOSHU.GOUKI
;
