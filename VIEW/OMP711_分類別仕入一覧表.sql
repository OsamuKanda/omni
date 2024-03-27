-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/10/31   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP711 ���ޕʎd���ꗗ�\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP711
AS
		SELECT  
              ROWNUM  AS EDANUM
            , WK_PRT711.EIGCD AS ���Ə�CD									--���Ə��R�[�h
            , DECODE(WK_PRT711.EIGCD,'91','�o��',DECODE(WK_PRT711.EIGCD,'90','�݌�',DM_JIGYO.JIGYONM)) AS ���Ə���									--���Ə���
            , WK_PRT711.BUNRUIDCD AS �啪��CD								--�啪�ރR�[�h
            , DM_BUNRUID.BUNRUIDNM AS �啪�ޖ�								--�啪�ޖ�
            , DECODE(WK_PRT711.EIGCD,'90',NULL,WK_PRT711.BUNRUICCD) AS ������CD								--�����ރR�[�h
			, DM_BUNRUIC.BUNRUICNM AS �����ޖ�								--�����ޖ�
			, DECODE(WK_PRT711.BUNRUIDCD,NULL,NULL,WK_PRT711.BUNRUIDCD || '-' || WK_PRT711.BUNRUICCD) AS ����CD		--���ރR�[�h
			, WK_PRT711.GAICHUKBN AS �O���敪								--�O���敪
			, CASE WHEN 
					WK_PRT711.GAICHUKBN <= '1' THEN 
						DK_GAICHU.GAICHUKBNNM || '�d��' 
				  ELSE 
				  		DK_GAICHU.GAICHUKBNNM END AS �O���敪��							--�O���敪��
			, WK_PRT711.SUDEURIKIN AS �����㕪�d��							--�����㕪�d��
			, WK_PRT711.TOUURIKIN AS �������㕪�d��							--�������㕪�d��
			, WK_PRT711.MIURIKIN AS �����㕪�d��							--�����㕪�d��
			, WK_PRT711.SUDEURIKIN + WK_PRT711.TOUURIKIN + WK_PRT711.MIURIKIN AS ���v
			, LOGINID AS ۸޲�ID											-- ۸޲�ID
		FROM  WK_PRT711,DM_JIGYO,DM_BUNRUID,DM_BUNRUIC,DK_GAICHU
		WHERE
              		WK_PRT711.PROGID = 'OMP711'
			  		-- ���Ə��}�X�^
			  AND	WK_PRT711.EIGCD = DM_JIGYO.JIGYOCD(+)
			  		-- �啪�ރ}�X�^
			  AND	WK_PRT711.BUNRUIDCD = DM_BUNRUID.BUNRUIDCD(+)
			  		-- �����ރ}�X�^
			  AND	WK_PRT711.BUNRUICCD = DM_BUNRUIC.BUNRUICCD(+)
			  		-- �O���敪�}�X�^
			  AND	WK_PRT711.GAICHUKBN = DK_GAICHU.GAICHUKBN
		ORDER BY
				WK_PRT711.EIGCD,WK_PRT711.BUNRUIDCD,WK_PRT711.BUNRUICCD,WK_PRT711.GAICHUKBN
;
