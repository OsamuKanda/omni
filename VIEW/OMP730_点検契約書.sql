-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP730 �_���_��
-------------------------------------------------------------------------------
CREATE OR REPLACE FORCE EDITIONABLE VIEW "OMNI"."V_OMP730" ("EDANUM", "���Ə�CD", "���Ə���", "�[����CD", "���@", "������CD", "�����於1", "�����於2", "�_��J�n��", "�_��I����", "���CD", "��ʖ�", "�@��", "�[���於1", "�[���於2", "�Z��1", "�Z��2", "�ێ��", "�_����", "�_����z", "������Z��1", "������Z��2", "�_�񏑏Z��1", "�_�񏑏Z��2", "��Ж�", "�����", "�_������p", "�_��I��", "�L��") AS 
  SELECT
              ROWNUM  AS EDANUM
            , DM_NONYU.JIGYOCD AS ���Ə�CD									--���Ə��R�[�h
            , DM_JIGYO.JIGYONM AS ���Ə���									--���Ə���
            , DM_HOSHU.NONYUCD AS �[����CD									--�[����R�[�h
			, DM_HOSHU.GOUKI AS ���@										--���@
--            , DECODE(DM_HOSHU.SEIKYUSAKICDH,NULL,S.NONYUNM1,S1.NONYUNM1) AS �����於1									--
--          , DECODE(DM_HOSHU.SEIKYUSAKICDH,NULL,S.NONYUNM2,S1.NONYUNM2) AS �����於2									--
			, S.NONYUCD AS ������CD										--
            , S.NONYUNM1 AS �����於1									--
            , S.NONYUNM2 AS �����於2									--
            , ���t�����ǉ�(DM_HOSHU.KEIYAKUYMD) AS �_��J�n��				--
            , DECODE(IS_DATE(DM_HOSHU.KEIYAKUYMD),0,NULL,���t�����ǉ�(TO_CHAR(ADD_MONTHS(TO_DATE(DM_HOSHU.KEIYAKUYMD,'YYYYMMDD')-1,12),'YYYYMMDD')))
 				AS �_��I����												--
 			, DM_HOSHU.SHUBETSUCD AS ���CD									--��ʃR�[�h
 			, DM_SHUBETSU.SHUBETSUNM AS ��ʖ�								--��ʖ�
			, DM_HOSHU.KISHUKATA AS �@��									--�@��
            , DM_NONYU.NONYUNM1 AS �[���於1								--�[���於�P
            , DM_NONYU.NONYUNM2 AS �[���於2								--�[���於�Q
			, DM_NONYU.ADD1 AS �Z��1										--�Z���P
			, DM_NONYU.ADD2 AS �Z��2										--�Z���Q
			, '�N' || (DM_HOSHU.HOSHUM1 + DM_HOSHU.HOSHUM2 + DM_HOSHU.HOSHUM3 + DM_HOSHU.HOSHUM4 + DM_HOSHU.HOSHUM5 + DM_HOSHU.HOSHUM6 +
			  DM_HOSHU.HOSHUM7 + DM_HOSHU.HOSHUM8 + DM_HOSHU.HOSHUM9 + DM_HOSHU.HOSHUM10 + DM_HOSHU.HOSHUM11 + DM_HOSHU.HOSHUM12) || '��' AS �ێ��
			, '(' || RTRIM(DECODE(DM_HOSHU.HOSHUM1,0,'','1,') || DECODE(DM_HOSHU.HOSHUM2,0,'','2,') || DECODE(DM_HOSHU.HOSHUM3,0,'','3,') ||
			  DECODE(DM_HOSHU.HOSHUM4,0,'','4,') || DECODE(DM_HOSHU.HOSHUM5,0,'','5,') || DECODE(DM_HOSHU.HOSHUM6,0,'','6,') ||
			  DECODE(DM_HOSHU.HOSHUM7,0,'','7,') || DECODE(DM_HOSHU.HOSHUM8,0,'','8,') || DECODE(DM_HOSHU.HOSHUM9,0,'','9,') ||
			  DECODE(DM_HOSHU.HOSHUM10,0,'','10,') || DECODE(DM_HOSHU.HOSHUM11,0,'','11,') || DECODE(DM_HOSHU.HOSHUM12,0,'','12,'),',') || '��)'AS �_����
			, DECODE(DM_HOSHU.KEIYAKUKBN,'1',DM_HOSHU.KEIYAKUKING,(DM_HOSHU.HOSHUM1 + DM_HOSHU.HOSHUM2 + DM_HOSHU.HOSHUM3 + DM_HOSHU.HOSHUM4 + DM_HOSHU.HOSHUM5 + DM_HOSHU.HOSHUM6 +
			  DM_HOSHU.HOSHUM7 + DM_HOSHU.HOSHUM8 + DM_HOSHU.HOSHUM9 + DM_HOSHU.HOSHUM10 + DM_HOSHU.HOSHUM11 + DM_HOSHU.HOSHUM12)*DM_HOSHU.KEIYAKUKING) AS �_����z								--�_����z
--			, DECODE(DM_HOSHU.SEIKYUSAKICDH,NULL,S.ADD1,S1.ADD1) AS ������Z��1											--������Z���P
--			, DECODE(DM_HOSHU.SEIKYUSAKICDH,NULL,S.ADD2,S1.ADD2) AS ������Z��2											--������Z���Q
			, S.ADD1 AS ������Z��1											--������Z���P
			, S.ADD2 AS ������Z��2											--������Z���Q
			, (CASE WHEN DM_HOSHU.KEIYAKUYMD < '20231001' THEN DM_KANRI.OLD_ADD1 ELSE DM_KANRI.ADD1 END) AS �_�񏑏Z��1		--�_�񏑏Z���P
			, (CASE WHEN DM_HOSHU.KEIYAKUYMD < '20231001' THEN DM_KANRI.OLD_ADD2 ELSE DM_KANRI.ADD2 END) AS �_�񏑏Z��2		--�_�񏑏Z���Q
			, (CASE WHEN DM_HOSHU.KEIYAKUYMD < '20231001' THEN DM_KANRI.OLD_KAISYANM ELSE DM_KANRI.KAISYANM END)AS ��Ж�		--��Ж�
			, (CASE WHEN DM_HOSHU.KEIYAKUYMD < '20231001' THEN DM_KANRI.OLD_TORINAM ELSE DM_KANRI.TORINAM END)AS �����		--�����
			, DM_HOSHU.KEIYAKUYMD AS �_������p
			, DECODE(IS_DATE(DM_HOSHU.KEIYAKUYMD),0,NULL,TO_CHAR(ADD_MONTHS(TO_DATE(DM_HOSHU.KEIYAKUYMD,'YYYYMMDD')-1,12),'YYYYMMDD')) AS �_��I��
			, DM_HOSHU.HOSHUM1 || DM_HOSHU.HOSHUM2 || DM_HOSHU.HOSHUM3 || DM_HOSHU.HOSHUM4 || DM_HOSHU.HOSHUM5 || DM_HOSHU.HOSHUM6 			--�L��
			  || DM_HOSHU.HOSHUM7 || DM_HOSHU.HOSHUM8 || DM_HOSHU.HOSHUM9 || DM_HOSHU.HOSHUM10 || DM_HOSHU.HOSHUM11 || DM_HOSHU.HOSHUM12 AS �L��
--		FROM  DM_NONYU,DM_JIGYO,DM_HOSHU,DM_NONYU S,DM_NONYU S1,DM_SHUBETSU,DM_KANRI
		FROM  DM_NONYU,DM_JIGYO,DM_HOSHU,DM_NONYU S,DM_SHUBETSU,DM_KANRI
		WHERE
              		-- �[����}�X�^
              		DM_HOSHU.NONYUCD = DM_NONYU.NONYUCD
			  AND	DM_NONYU.SECCHIKBN = '01'
              		-- ������}�X�^
              AND	DM_NONYU.SEIKYUSAKICDH = S.NONYUCD
			  AND	S.SECCHIKBN = '00'
--              AND   DM_NONYU.JIGYOCD = S.JIGYOCD
              		-- ������}�X�^
--              AND	DM_HOSHU.SEIKYUSAKICDH = S1.NONYUCD(+)
--			  AND	S1.SECCHIKBN = '00'
--             AND   DM_NONYU.JIGYOCD(+) = S1.JIGYOCD
			  		-- ���Ə��}�X�^
			  AND	DM_NONYU.JIGYOCD = DM_JIGYO.JIGYOCD
					-- �ێ�_���}�X�^
			  AND	DM_HOSHU.KEIYAKUYMD IS NOT Null
			  AND	DM_HOSHU.KEIYAKUYMD <> 0
			  		-- ��ʃ}�X�^
			  AND	DM_HOSHU.SHUBETSUCD = DM_SHUBETSU.SHUBETSUCD(+)
			  		-- �Ǘ��}�X�^
			  AND	DM_KANRI.KANRINO = '1'
              AND	DM_HOSHU.DELKBN	 = '0'
		ORDER BY
					  DM_NONYU.JIGYOCD
					, DM_HOSHU.NONYUCD
					, DM_HOSHU.GOUKI
;
