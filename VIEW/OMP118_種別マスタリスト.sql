-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP118 ��ʃ}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP118
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_SHUBETSU.SHUBETSUCD AS ���CD
            , DM_SHUBETSU.SHUBETSUNM AS ��ʖ�
		FROM  DM_SHUBETSU
		WHERE
             		DM_SHUBETSU.DELKBN	 = '0'
		ORDER BY
					  DM_SHUBETSU.SHUBETSUCD
;
