-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP108 ���i�K�i�}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP108
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_BBUNRUI.BBUNRUICD AS ���i����CD
            , DM_BBUNRUI.BBUNRUINM AS ���i���ޖ�
            , DM_BKIKAKU.BKIKAKUCD AS ���i�K�iCD
            , DM_BKIKAKU.BKIKAKUNM AS ���i�K�i��
            , DM_BKIKAKU.TANICD AS �P��CD
            , DM_TANI.TANINM AS �P�ʖ�
            , DM_BKIKAKU.SIRTANK AS �d���P��
            , DM_BKIKAKU.URIAGETANK AS ����P��
            , DM_BKIKAKU.GAICHUKBN AS �O���敪
            , DK_GAICHU.GAICHUKBNNM AS �O���敪��
		FROM  DM_BBUNRUI,DM_BKIKAKU,DM_TANI,DK_GAICHU
		WHERE
             		DM_BBUNRUI.DELKBN	 = '0'
             AND	DM_BKIKAKU.DELKBN 	 = '0'
             AND	DM_BBUNRUI.BBUNRUICD = DM_BKIKAKU.BBUNRUICD
             AND	DM_BKIKAKU.TANICD = DM_TANI.TANICD(+)
             AND	DM_BKIKAKU.GAICHUKBN = DK_GAICHU.GAICHUKBN(+)
		ORDER BY
					  DM_BBUNRUI.BBUNRUICD,DM_BKIKAKU.BKIKAKUCD
;
