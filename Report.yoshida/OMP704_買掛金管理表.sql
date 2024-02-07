-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/10/27   OKADA
--                                                 Update:   
--  HIS-124 ����敪�Ɂu�ł񂳂��v�Ɓu�������v��ǉ� 2014/4/30 Kawahata
-------------------------------------------------------------------------------
--OMP704	���|���Ǘ��\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP704
AS
		SELECT  
              ROWNUM  AS EDANUM
			, SUBSTR(DM_KANRI.MONYMD,1,4) || '�N' ||  SUBSTR(DM_KANRI.MONYMD,5,2) || '���x' AS �������N��			--�������N��
			, DM_SHIRE.SIRCD AS �d����CD																			--�d����R�[�h
			, DM_SHIRE.SIRNM1 AS �d���於1																			--�d���於1
			, DM_SHIRE.SIRNM2 AS �d���於2																			--�d���於2
			, DM_SHIRE.SIRNMR AS �d������																			--�d������
			, DM_SHIRE.SIRNMX AS �d����J�i																			--�d����J�i
			, DM_SHIRE.ZENZAN AS �O���c��																			--�O���c��
			, DM_SHIRE.TSHRGENKIN AS ����																			--����
			, DM_SHIRE.TSHRTEGATA AS �x����`																		--�x����`
			, (DM_SHIRE.TSHRNEBIKI + DM_SHIRE.TSHRSOSAI + DM_SHIRE.TSHRSONOTA) AS ���̑�							--���E�E���̑�
			, DM_SHIRE.TSHRANZENKAIHI AS ���S���͉��																--���S���͉��
-->>HIS-124 ����敪�̒ǉ� Start
			, DM_SHIRE.TSHRDENSAI AS �ł񂳂�																		--�ł񂳂�
			, DM_SHIRE.TSHRKIJITSU AS ������																		--������
--<<HIS-124 ����敪�̒ǉ� End
			, DM_SHIRE.TSHRFURIKOMITESU AS �U���萔��																--�U���萔��
-->>HIS-124 ����敪�̒ǉ� Mod Start
		--	, (DM_SHIRE.ZENZAN - DM_SHIRE.TSHRGENKIN - DM_SHIRE.TSHRTEGATA - DM_SHIRE.TSHRNEBIKI - 
		--	   DM_SHIRE.TSHRSOSAI - DM_SHIRE.TSHRSONOTA - DM_SHIRE.TSHRANZENKAIHI - DM_SHIRE.TSHRFURIKOMITESU ) AS �����J�z		--�����J�z
			, (DM_SHIRE.ZENZAN - DM_SHIRE.TSHRGENKIN - DM_SHIRE.TSHRTEGATA - DM_SHIRE.TSHRNEBIKI - 
			   DM_SHIRE.TSHRSOSAI - DM_SHIRE.TSHRSONOTA - DM_SHIRE.TSHRANZENKAIHI - DM_SHIRE.TSHRFURIKOMITESU - DM_SHIRE.TSHRDENSAI - DM_SHIRE.TSHRKIJITSU ) AS �����J�z		--�����J�z
			, (DM_SHIRE.TSIRKIN - DM_SHIRE.TSIRHENKIN - DM_SHIRE.TSIRNEBIKI) AS �����d��							--�����d��
--<<HIS-124 ����敪�̒ǉ� Mod End
			, DM_SHIRE.TTAX AS �����																				--�����
-->>HIS-124 ����敪�̒ǉ� Mod Start
		--	, (DM_SHIRE.ZENZAN - DM_SHIRE.TSHRGENKIN - DM_SHIRE.TSHRTEGATA - DM_SHIRE.TSHRNEBIKI - 
		--	   DM_SHIRE.TSHRSOSAI - DM_SHIRE.TSHRSONOTA - DM_SHIRE.TSHRANZENKAIHI - DM_SHIRE.TSHRFURIKOMITESU )
		--	  + (DM_SHIRE.TSIRKIN - TSIRHENKIN - TSIRNEBIKI) + DM_SHIRE.TTAX AS �������c							--�������c
			, (DM_SHIRE.ZENZAN - DM_SHIRE.TSHRGENKIN - DM_SHIRE.TSHRTEGATA - DM_SHIRE.TSHRNEBIKI - 
			   DM_SHIRE.TSHRSOSAI - DM_SHIRE.TSHRSONOTA - DM_SHIRE.TSHRANZENKAIHI - DM_SHIRE.TSHRFURIKOMITESU - DM_SHIRE.TSHRDENSAI - DM_SHIRE.TSHRKIJITSU )
			  + (DM_SHIRE.TSIRKIN - TSIRHENKIN - TSIRNEBIKI) + DM_SHIRE.TTAX AS �������c							--�������c
--<<HIS-124 ����敪�̒ǉ� Mod End
		FROM DM_SHIRE,DM_KANRI
			WHERE
              		-- �Ǘ��}�X�^
              		DM_KANRI.KANRINO = '1'
              AND	(DM_SHIRE.ZENZAN <> 0 OR DM_SHIRE.TSHRGENKIN <> 0 OR DM_SHIRE.TSHRNEBIKI <> 0 OR
              		 DM_SHIRE.TSHRSOSAI <> 0 OR DM_SHIRE.TSHRSONOTA <> 0 OR DM_SHIRE.TSHRANZENKAIHI <> 0 OR
              		 DM_SHIRE.TSHRFURIKOMITESU <> 0 OR DM_SHIRE.TSIRKIN <> 0 OR DM_SHIRE.TSIRHENKIN <> 0 OR
--HIS-124 ����敪�̒ǉ� Start
              		 DM_SHIRE.TSHRDENSAI <> 0 OR DM_SHIRE.TSHRKIJITSU <> 0 OR
--HIS-124 ����敪�̒ǉ� End
              		 DM_SHIRE.TSIRNEBIKI <> 0 OR DM_SHIRE.TTAX <> 0)
					-- �d����}�X�^.�����敪
			  AND	DM_SHIRE.DELKBN = '0'
        ORDER BY
        			  DM_SHIRE.SIRNMX,DM_SHIRE.SIRCD
;

