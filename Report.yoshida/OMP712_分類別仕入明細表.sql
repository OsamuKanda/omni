-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/10/31   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP712 ���ޕʎd�����ו\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP712
AS
		SELECT  
              ROWNUM  AS EDANUM
            , WK_PRT712.JIGYOCD AS ���Ə�CD								--���Ə��R�[�h
            , DECODE(WK_PRT712.JIGYOCD,'91','�o��',DECODE(WK_PRT712.JIGYOCD,'90','�݌�',DM_JIGYO.JIGYONM)) AS ���Ə���									--���Ə���
            , WK_PRT712.BUNRUIDCD AS �啪��CD								--�啪�ރR�[�h
            , DM_BUNRUID.BUNRUIDNM AS �啪�ޖ�								--�啪�ޖ�
            , DECODE(WK_PRT712.JIGYOCD,'90',NULL,WK_PRT712.BUNRUICCD) AS ������CD								--�����ރR�[�h
			, DM_BUNRUIC.BUNRUICNM AS �����ޖ�								--�����ޖ�
			, DECODE(WK_PRT712.BUNRUIDCD,NULL,NULL,WK_PRT712.BUNRUIDCD || '-' || WK_PRT712.BUNRUICCD) AS ����CD		--���ރR�[�h
			, WK_PRT712.GAICHUKBN AS �O���敪								--�O���敪
			, CASE WHEN 
					WK_PRT712.GAICHUKBN <= '1' THEN 
						DK_GAICHU.GAICHUKBNNM || '�d��' 
				  ELSE 
				  		DK_GAICHU.GAICHUKBNNM END AS �O���敪��							--�O���敪��
			, DT_SHIREM.BUMONCD AS ����CD									--����R�[�h
			, DM_BUMON.BUMONNM AS ���喼									--���喼
			, DECODE(WK_PRT712.URIAGEKBN,'��',0,DECODE(WK_PRT712.URIAGEKBN,'��',1,2)) AS ����
			, WK_PRT712.URIAGEKBN AS ����敪								--
			, DM_NONYU.NONYUNMR AS �[���旪��								--
			, DT_SHIREM.JIGYOCD || DT_SHIREM.SAGYOBKBN || DT_SHIREM.RENNO AS �����ԍ�		--
			, DT_SHIREH.SIRCD AS �d����CD									--�d����R�[�h
			, DM_SHIRE.SIRNMR AS �d���旪��									--�d���旪��
			, SUBSTR(���t�L���ǉ�(DT_SHIREH.SIRYMD),6,5) AS �d����			--
			, DT_SHIREM.BKIKAKUNM AS �K�i��									--
			, DT_SHIREM.SIRSU AS ����										--
			, DT_SHIREM.SIRTANK AS �d���P��									--
			, DT_SHIREM.SIRKIN AS ���z										--
			, DT_SHIREM.SIRNO AS �d���ԍ�									--
			, DT_SHIREM.GYONO AS �s�ԍ�
			, LOGINID AS ۸޲�ID											--۸޲�ID
		FROM  WK_PRT712,DM_JIGYO,DK_GAICHU,DT_SHIREH,DM_BUNRUID,DM_BUNRUIC
			  ,DT_SHIREM,DM_BUMON,DT_BUKKEN,DM_NONYU,DM_SHIRE
		WHERE
              		WK_PRT712.PROGID = 'OMP712'
			  		-- ���Ə��}�X�^
			  AND	WK_PRT712.JIGYOCD = DM_JIGYO.JIGYOCD(+)
			  		-- �啪�ރ}�X�^
			  AND	WK_PRT712.BUNRUIDCD = DM_BUNRUID.BUNRUIDCD
			  		-- �����ރ}�X�^
			  AND	WK_PRT712.BUNRUICCD = DM_BUNRUIC.BUNRUICCD
			  		-- �O���敪�}�X�^
			  AND	WK_PRT712.GAICHUKBN = DK_GAICHU.GAICHUKBN
			  		-- �d���w�b�_�[
			  AND	WK_PRT712.SIRJIGYOCD = DT_SHIREH.SIRJIGYOCD
			  AND	WK_PRT712.SIRNO = DT_SHIREH.SIRNO
			  		-- �d������
			  AND	WK_PRT712.SIRJIGYOCD = DT_SHIREM.SIRJIGYOCD
			  AND	WK_PRT712.SIRNO = DT_SHIREM.SIRNO
			  AND	WK_PRT712.SIRGYONO = DT_SHIREM.GYONO
			  		-- ����}�X�^
			  AND	DT_SHIREM.BUMONCD = DM_BUMON.BUMONCD(+)
			  		-- �����t�@�C��
			  AND	DT_SHIREM.JIGYOCD = DT_BUKKEN.JIGYOCD(+)
			  AND	DT_SHIREM.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN(+)
			  AND	DT_SHIREM.RENNO = DT_BUKKEN.RENNO(+)
			  		-- �[����}�X�^
			  AND	DT_BUKKEN.NONYUCD = DM_NONYU.NONYUCD(+)
			  AND	'01' = DM_NONYU.SECCHIKBN(+)
			  AND	DT_BUKKEN.JIGYOCD = DM_NONYU.JIGYOCD(+)
			  		-- �d����}�X�^
			  AND	DT_SHIREH.SIRCD = DM_SHIRE.SIRCD
		ORDER BY
				WK_PRT712.JIGYOCD,WK_PRT712.BUNRUIDCD,WK_PRT712.BUNRUICCD,WK_PRT712.GAICHUKBN
				,DT_SHIREM.JIGYOCD || DT_SHIREM.SAGYOBKBN || DT_SHIREM.RENNO
				,DT_SHIREH.SIRYMD
;
