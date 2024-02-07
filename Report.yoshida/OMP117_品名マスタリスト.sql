-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP117 �i���}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP117
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_HINNM.HINCD AS �iCD
            , DM_HINNM.HINNM1 AS �i��1
            , DM_HINNM.HINNM2 AS �i��2
            , DM_HINNM.SURYO AS ����
            , DM_HINNM.TANICD AS �P��CD
            , DM_TANI.TANINM AS �P�ʖ�
		FROM  DM_HINNM,DM_TANI
		WHERE
             		DM_HINNM.DELKBN	 = '0'
             AND	DM_HINNM.TANICD = DM_TANI.TANICD(+)
		ORDER BY
					  DM_HINNM.HINCD
;
