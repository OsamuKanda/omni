-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/10   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP705	�����ʌ����\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP705
AS
			SELECT 
				 SUBSTR(DM_KANRI.MONYMD,1,4) || '�N' ||  SUBSTR(DM_KANRI.MONYMD,5,2) || '���x' AS �������N��
			    ,D1.JIGYOCD AS ���Ə�CD
			    ,DM_JIGYO.JIGYONM AS ���Ə���
			    ,D1.SHIKAKARIKBN AS �d�|�敪
			    ,DECODE(D1.SHIKAKARIKBN,1,'�d�@�|�@',2,'��������','������') AS �d�|�敪��
			    ,D1.SAGYOBKBN AS ��ƕ���
			    ,DK_SAGYO.SAGYOKBNNM AS �啪�ޖ�
			    ,D1.RENNO AS �A��
			    ,D1.�����ԍ�
			    ,DT_BUKKEN.NONYUCD AS �[����CD
			    ,DM_NONYU.NONYUNM1 AS �[���於
			    ,DM_NONYU.SENBUSHONM AS ������
			    ,DT_BUKKEN.BIKO AS ���l
			    ,D1.GAICHUKBN AS �O���敪
			    ,D1.ZENIZENKING AS �O���ȑO
			    ,D1.ZENKING AS �O�����z
			    ,D1.TOUKING AS �������z
			    ,D1.ZENIZENKING + D1.ZENKING + D1.TOUKING AS ���v
			    ,D1.URIKING AS ������z
			    ,D2.GAICHUKBN AS �O���敪1
			    ,D2.ZENIZENKING AS �O���ȑO1
			    ,D2.ZENKING AS �O�����z1
			    ,D2.TOUKING AS �������z1
			    ,D2.ZENIZENKING + D2.ZENKING + D2.TOUKING AS ���v1
			    ,D3.GAICHUKBN AS �O���敪2
			    ,D3.ZENIZENKING AS �O���ȑO2
			    ,D3.ZENKING AS �O�����z2
			    ,D3.TOUKING AS �������z2
			    ,D3.ZENIZENKING + D3.ZENKING + D3.TOUKING AS ���v2
			    ,( D1.ZENIZENKING + D1.ZENKING + D1.TOUKING + D2.ZENIZENKING + D2.ZENKING + D2.TOUKING + D3.ZENIZENKING + D3.ZENKING + D3.TOUKING ) AS �d�����v
			    ,D1.LOGINID
			FROM (
							SELECT  
								  WK_PRT705.JIGYOCD 																			--���Ə��R�[�h
								, WK_PRT705.SHIKAKARIKBN 
					            , WK_PRT705.SAGYOBKBN
					            , WK_PRT705.RENNO 
								, WK_PRT705.JIGYOCD || '-' || WK_PRT705.SAGYOBKBN || '-' || WK_PRT705.RENNO AS �����ԍ�					--�����ԍ�
								, WK_PRT705.GAICHUKBN
								, WK_PRT705.ZENIZENKING 
								, WK_PRT705.ZENKING
								, WK_PRT705.TOUKING
								, WK_PRT705.URIKING
								, WK_PRT705.LOGINID
							FROM WK_PRT705
								WHERE 	WK_PRT705.PROGID = 'OMP705'
					              AND	WK_PRT705.GAICHUKBN = '0' ) D1,
					(
							SELECT  
								  WK_PRT705.JIGYOCD 																			--���Ə��R�[�h
								, WK_PRT705.SHIKAKARIKBN 
					            , WK_PRT705.SAGYOBKBN
					            , WK_PRT705.RENNO 
								, WK_PRT705.JIGYOCD || '-' || WK_PRT705.SAGYOBKBN || '-' || WK_PRT705.RENNO AS �����ԍ�					--�����ԍ�
								, WK_PRT705.GAICHUKBN
								, WK_PRT705.ZENIZENKING 
								, WK_PRT705.ZENKING
								, WK_PRT705.TOUKING
								, WK_PRT705.LOGINID
							FROM WK_PRT705
								WHERE 	WK_PRT705.PROGID = 'OMP705'
					              AND	WK_PRT705.GAICHUKBN = '1' ) D2,
					(
							SELECT  
								  WK_PRT705.JIGYOCD 																			--���Ə��R�[�h
								, WK_PRT705.SHIKAKARIKBN 
					            , WK_PRT705.SAGYOBKBN
					            , WK_PRT705.RENNO 
								, WK_PRT705.JIGYOCD || '-' || WK_PRT705.SAGYOBKBN || '-' || WK_PRT705.RENNO AS �����ԍ�					--�����ԍ�
								, WK_PRT705.GAICHUKBN
								, WK_PRT705.ZENIZENKING 
								, WK_PRT705.ZENKING
								, WK_PRT705.TOUKING
								, WK_PRT705.LOGINID
							FROM WK_PRT705
								WHERE 	WK_PRT705.PROGID = 'OMP705'
					              AND	WK_PRT705.GAICHUKBN = '2' ) D3
              , DM_KANRI,DM_JIGYO,DK_SAGYO,DM_NONYU,DT_BUKKEN
				WHERE D1.�����ԍ� = D2.�����ԍ� 
				AND D1.�����ԍ� = D3.�����ԍ�
				AND D1.LOGINID = D2.LOGINID
				AND D1.LOGINID = D3.LOGINID
				AND DM_KANRI.KANRINO = '1'
						-- ���Ə��}�X�^
				AND	D1.JIGYOCD = DM_JIGYO.JIGYOCD
						-- �啪�ދ敪�}�X�^
				AND	D1.SAGYOBKBN = DK_SAGYO.SAGYOKBN
						-- �����t�@�C��
				AND	D1.JIGYOCD = DT_BUKKEN.JIGYOCD
				AND	D1.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN
				AND	D1.RENNO = DT_BUKKEN.RENNO
						-- �[����}�X�^
--				AND	D1.JIGYOCD = DM_NONYU.JIGYOCD(+)
				AND	DT_BUKKEN.NONYUCD = DM_NONYU.NONYUCD(+)
				AND	'01' = DM_NONYU.SECCHIKBN(+)
;

