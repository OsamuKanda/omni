-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/10   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP710	�����ʌ����ݐϖ��ו\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP710
AS
			SELECT 
				  --�O��敪�̏ꍇ�́A�������B����ȊO�́A�ŐV�������𒊏o�����Ŏg�p����B
				  SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) AS ���t��r
				, DT_SHIREH.SIRYMD AS ���ד��t��r�p
				, DT_BUKKEN.JIGYOCD AS ���Ə�CD
				, DT_BUKKEN.SAGYOBKBN AS ��ƕ��ދ敪
				, DT_BUKKEN.RENNO AS �A��
				, DT_BUKKEN.JIGYOCD || '-' || DT_BUKKEN.SAGYOBKBN || '-' || DT_BUKKEN.RENNO AS �����ԍ�
			    , DT_BUKKEN.NONYUCD AS �[����CD
			    , DM_NONYU.NONYUNM1 AS �[���於
				, DT_BUKKEN.BUNRUIDCD AS �啪��CD
				, DM_BUNRUID.BUNRUIDNM AS �啪�ޖ�
				, DT_BUKKEN.BUNRUICCD AS ������CD
				, DM_BUNRUIC.BUNRUICNM AS �����ޖ�
				, ���t�L���ǉ�(DT_BUKKEN.KANRYOYMD) AS ������
				, DT_BUKKEN.SOUKINGR AS ������z
				--�O���敪 <= '2' �̏ꍇ�͎d����R�[�h�͔�\��
				--0:���i 1:�O�� 2:���o�� 3:�݌� 4:�H��
				,CASE 
					WHEN DM_BKIKAKU.GAICHUKBN <= '2' THEN	DT_SHIREH.SIRCD
					ELSE NULL END AS �d����CD
				,CASE 
					WHEN DM_BKIKAKU.GAICHUKBN <= '2' THEN	DM_SHIRE.SIRNM1
					WHEN DM_BKIKAKU.GAICHUKBN = '3' THEN	'�݌Ɏg�p'
					ELSE '�H�@�@��' END AS �d���於1
				,CASE 
					WHEN DM_BKIKAKU.GAICHUKBN <= '2' THEN	DM_SHIRE.SIRNM2
					ELSE NULL END AS �d���於2
				,CASE 
					WHEN DM_BKIKAKU.GAICHUKBN <= '2' THEN	DM_SHIRE.SIRNMR
					ELSE NULL END AS �d���於����
				, ���t�L���ǉ�(DT_SHIREH.SIRYMD) AS �d�����t
				, DT_SHIREM.SIRNO AS �d���ԍ�
				, DT_SHIREM.GYONO AS �s�ԍ�
				, DT_SHIREM.BBUNRUICD AS ���i����CD
				, DT_SHIREM.BBUNRUINM AS ���i���ޖ�
				, DT_SHIREM.BKIKAKUCD AS ���i�K�iCD
				, DT_SHIREM.BKIKAKUNM AS ���i�K�i��
				, DM_BKIKAKU.GAICHUKBN AS �O���敪
				, DT_SHIREM.SIRSU AS ����
				, DM_TANI.TANINM AS �P�ʖ�
				, DT_SHIREM.SIRTANK AS �P��
				, DT_SHIREM.SIRKIN AS ���z
				, DT_SHIREM.TAX AS �����
			FROM DT_SHIREH,DT_SHIREM,DT_BUKKEN,DM_TANI,DM_NONYU,DM_BKIKAKU,DM_BUNRUID,DM_BUNRUIC,DM_SHIRE
			WHERE
						-- �[����}�X�^
--2012.10.11-----------------------------------------------
--					DT_BUKKEN.JIGYOCD = DM_NONYU.JIGYOCD
					DT_BUKKEN.NONYUCD = DM_NONYU.NONYUCD
				AND	'01' = DM_NONYU.SECCHIKBN
						-- �啪�ރ}�X�^
				AND	DT_BUKKEN.BUNRUIDCD = DM_BUNRUID.BUNRUIDCD
						-- �����ރ}�X�^
				AND	DT_BUKKEN.BUNRUICCD = DM_BUNRUIC.BUNRUICCD
						-- �����t�@�C��
				AND	DT_SHIREM.JIGYOCD = DT_BUKKEN.JIGYOCD
				AND	DT_SHIREM.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN
				AND	DT_SHIREM.RENNO = DT_BUKKEN.RENNO
						-- �d������
				AND	DT_SHIREH.SIRJIGYOCD = DT_SHIREM.SIRJIGYOCD
				AND	DT_SHIREH.SIRNO = DT_SHIREM.SIRNO
						-- �d����}�X�^
				AND	DT_SHIREH.SIRCD = DM_SHIRE.SIRCD(+)
						-- ���i�K�i�}�X�^
				AND	DT_SHIREM.BBUNRUICD = DM_BKIKAKU.BBUNRUICD(+)
				AND	DT_SHIREM.BKIKAKUCD = DM_BKIKAKU.BKIKAKUCD(+)
						-- �P�ʃ}�X�^
				AND	DT_SHIREM.TANICD = DM_TANI.TANICD(+)
				AND	DT_SHIREM.DELKBN = '0'
				AND DT_SHIREH.DELKBN = '0'
				AND	DT_BUKKEN.DELKBN = '0'
;

