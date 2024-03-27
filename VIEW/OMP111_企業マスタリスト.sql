-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/01   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP111 ��ƃ}�X�^���X�g
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP111
AS
		SELECT  
              ROWNUM  AS EDANUM
            , DM_KIGYO.KIGYOCD AS ���CD
            , DM_KIGYO.KIGYONM AS ��Ɩ�
            , DM_KIGYO.KIGYONMX AS ��Ɩ��J�i
            , DM_KIGYO.RYAKUSHO AS ����
            , DM_KIGYO.ZIPCODE AS �X�֔ԍ�
            , DM_KIGYO.ADD1 AS �Z��1
            , DM_KIGYO.ADD2 AS �Z��2
            , DM_KIGYO.TELNO AS �d�b�ԍ�
            , DM_KIGYO.FAXNO AS �e�`�w
			, DM_KIGYO.BUSHONM AS ������
			, DM_KIGYO.HACCHUTANTNM AS �����S���Җ�
			, DM_KIGYO.EIGYOTANTCD AS �c�ƒS��CD
			, DM_TANT.TANTNM AS �S���Җ�
			, DM_KIGYO.AREACD AS �n��CD
			, DM_AREA.AREANM AS �n�於
		FROM  DM_KIGYO,DM_TANT,DM_AREA
		WHERE
             		DM_KIGYO.DELKBN	 = '0'
             AND	DM_KIGYO.EIGYOTANTCD = DM_TANT.TANTCD(+)
             AND	DM_KIGYO.AREACD = DM_AREA.AREACD(+)
 		ORDER BY
					  DM_KIGYO.KIGYOCD
;
