-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP120 ����}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP120
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_BUMON.BUMONCD AS ����CD
            , DM_BUMON.BUMONNM AS ���喼
		FROM  DM_BUMON
		WHERE
             		DM_BUMON.DELKBN	 = '0'
		ORDER BY
					  DM_BUMON.BUMONCD
;
