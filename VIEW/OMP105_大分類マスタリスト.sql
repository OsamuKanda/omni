-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP105 �啪�ރ}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP105
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_BUNRUID.BUNRUIDCD AS �啪��CD
            , DM_BUNRUID.BUNRUIDNM AS �啪�ޖ�
		FROM  DM_BUNRUID
		WHERE
             		DM_BUNRUID.DELKBN	 = '0'
		ORDER BY
					  DM_BUNRUID.BUNRUIDCD
;
