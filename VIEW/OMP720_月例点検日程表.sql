-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/10/31   OKADA
--                                                 Update:2012/02/22   OKADA
--�����ԍ��̕\�����@��ύX(2012/02/22)
-------------------------------------------------------------------------------
--OMP720 ����_�������\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP720
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_NONYU.JIGYOCD AS ���Ə�CD									--���Ə��R�[�h
            , DM_JIGYO.JIGYONM AS ���Ə���									--���Ə���
			, DM_HOSHU.SAGYOUTANTCD AS ��ƒS����CD							--��ƒS���҃R�[�h
			, DM_TANT.TANTNM AS ��ƒS���Җ�								--��ƒS���Җ�
			, DM_HOSHU.GOUKI AS ���@										--���@
			, DM_NONYU.NONYUCD AS �[����CD									--�[����R�[�h
			, DM_NONYU.NONYUNM1 AS �[���於1								--�[���於�P
			, DM_NONYU.NONYUNM2 AS �[���於2								--�[���於�Q
			, DM_NONYU.ADD1 AS �Z��1										--�Z���P
			, DM_NONYU.ADD2 AS �Z��2										--�Z���Q
			, DM_NONYU.TELNO1 AS �d�b�ԍ�1									--�d�b�ԍ��P
			, DM_HOSHU.KISHUKATA AS �@��									--�@��
			, DECODE(DM_HOSHU.TENKEN1BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN1BNO) AS �����ԍ�1								--1��
			, DECODE(DM_HOSHU.TENKEN2BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN2BNO) AS �����ԍ�2								--2��
			, DECODE(DM_HOSHU.TENKEN3BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN3BNO) AS �����ԍ�3								--3��
			, DECODE(DM_HOSHU.TENKEN4BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN4BNO) AS �����ԍ�4								--4��
			, DECODE(DM_HOSHU.TENKEN5BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN5BNO) AS �����ԍ�5								--5��
			, DECODE(DM_HOSHU.TENKEN6BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN6BNO) AS �����ԍ�6								--6��
			, DECODE(DM_HOSHU.TENKEN7BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN7BNO) AS �����ԍ�7								--7��
			, DECODE(DM_HOSHU.TENKEN8BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN8BNO) AS �����ԍ�8								--8��
			, DECODE(DM_HOSHU.TENKEN9BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN9BNO) AS �����ԍ�9								--9��
			, DECODE(DM_HOSHU.TENKEN10BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN10BNO) AS �����ԍ�10							--10��
			, DECODE(DM_HOSHU.TENKEN11BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN11BNO) AS �����ԍ�11							--11��
			, DECODE(DM_HOSHU.TENKEN12BNO,NULL,NULL,DM_NONYU.JIGYOCD || '-2-' || DM_HOSHU.TENKEN12BNO) AS �����ԍ�12							--12��
			, DM_HOSHU.HOSHUM1 AS �ێ猎1									--1��
			, DM_HOSHU.HOSHUM2 AS �ێ猎2									--2��
			, DM_HOSHU.HOSHUM3 AS �ێ猎3									--3��
			, DM_HOSHU.HOSHUM4 AS �ێ猎4									--4��
			, DM_HOSHU.HOSHUM5 AS �ێ猎5									--5��
			, DM_HOSHU.HOSHUM6 AS �ێ猎6									--6��
			, DM_HOSHU.HOSHUM7 AS �ێ猎7									--7��
			, DM_HOSHU.HOSHUM8 AS �ێ猎8									--8��
			, DM_HOSHU.HOSHUM9 AS �ێ猎9									--9��
			, DM_HOSHU.HOSHUM10 AS �ێ猎10									--10��
			, DM_HOSHU.HOSHUM11 AS �ێ猎11									--11��
			, DM_HOSHU.HOSHUM12 AS �ێ猎12									--12��
		FROM  DM_NONYU,DM_JIGYO,DM_HOSHU,DM_TANT
		WHERE
              		-- �ێ�_���}�X�^
              		DM_HOSHU.NONYUCD = DM_NONYU.NONYUCD
			  AND	DM_NONYU.SECCHIKBN = '01'
			  		-- ���Ə��}�X�^
			  AND	DM_NONYU.JIGYOCD = DM_JIGYO.JIGYOCD
			  		-- ��ƒS���҃}�X�^
			  AND	DM_HOSHU.SAGYOUTANTCD = DM_TANT.TANTCD(+)
              AND	DM_HOSHU.DELKBN	 = '0'
		ORDER BY
					  DM_NONYU.JIGYOCD
					, DM_HOSHU.SAGYOUTANTCD
					, DM_NONYU.NONYUCD
					, DM_HOSHU.GOUKI
;
