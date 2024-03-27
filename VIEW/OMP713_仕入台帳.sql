-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/11/10   OKADA
--                                                 Update:2012/10/11
-------------------------------------------------------------------------------
--OMP713 �d���䒠
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP713
AS
	SELECT
		  WK.�d����CD
		, WK.�d���於1
		, WK.�d���於2
		, WK.�d����J�i
		, WK.�O���c��
		, WK.���t
		, WK.�d�����Ə�CD
		, WK.�d���ԍ�
		, WK.�s�ԍ�
		, WK.�����ԍ�
		, WK.�[����CD
		, WK.�[���旪��
		, WK.���iCD
		, WK.�K�i��
		, WK.�d������
		, WK.�P�ʖ�
		, WK.�P��
		, WK.���z
		, WK.�����
		, WK.���v
		, WK.�d�����t����
		, WK.�敪
		, SUBSTR(DM_KANRI.MONYMD,1,4) || '�N' ||  SUBSTR(DM_KANRI.MONYMD,5,2) || '���x' AS �������N��
	FROM
(		SELECT  
              DM_SHIRE.SIRCD AS �d����CD									--
            , DM_SHIRE.SIRNM1 AS �d���於1									--
            , DM_SHIRE.SIRNM2 AS �d���於2									--
            , DM_SHIRE.SIRNMX AS �d����J�i									--
            , DM_SHIRE.ZENZAN AS �O���c��									--
            , ���t�L���ǉ�(DT_SHIREH.SIRYMD) AS ���t						--
            , DT_SHIREH.SIRJIGYOCD AS �d�����Ə�CD							--
            , DT_SHIREH.SIRNO AS �d���ԍ�									--
            , DT_SHIREM.GYONO AS �s�ԍ�										--
            , DT_SHIREM.JIGYOCD || '-' || DT_SHIREM.SAGYOBKBN || '-' || DT_SHIREM.RENNO AS �����ԍ�
            , DT_BUKKEN.NONYUCD AS �[����CD									--
            , DM_NONYU.NONYUNMR AS �[���旪��
            , DT_SHIREM.BBUNRUICD || '-' || DT_SHIREM.BKIKAKUCD AS ���iCD
            , DT_SHIREM.BKIKAKUNM AS �K�i��
            , DT_SHIREM.SIRSU AS �d������
            , DM_TANI.TANINM AS �P�ʖ�
            , DT_SHIREM.SIRTANK AS �P��
            , DT_SHIREM.SIRKIN AS ���z
            , DT_SHIREM.TAX AS �����
            , NULL AS ���v
            , DT_SHIREH.SIRYMD AS �d�����t����
            , 1 AS �敪
		FROM  DM_SHIRE,DT_SHIREH,DT_SHIREM,DT_BUKKEN,DM_TANI,DM_NONYU
		WHERE
			  		-- �d������
			  		DT_SHIREM.SIRJIGYOCD = DT_SHIREH.SIRJIGYOCD
			  AND	DT_SHIREM.SIRNO = DT_SHIREH.SIRNO
			  		-- �����t���O
			  AND	DT_SHIREH.GETFLG <> '1'
			  		-- �����t�@�C��
			  AND	DT_SHIREM.JIGYOCD = DT_BUKKEN.JIGYOCD(+)
			  AND	DT_SHIREM.SAGYOBKBN = DT_BUKKEN.SAGYOBKBN(+)
			  AND	DT_SHIREM.RENNO = DT_BUKKEN.RENNO(+)
			  		-- �[����}�X�^
			  AND	DT_BUKKEN.NONYUCD = DM_NONYU.NONYUCD(+)
			  AND	'01' = DM_NONYU.SECCHIKBN(+)
--2012.10.11-------------------------------------------------------
--			  AND	DT_BUKKEN.JIGYOCD = DM_NONYU.JIGYOCD(+)
			  		-- �P�ʃ}�X�^
			  AND	DT_SHIREM.TANICD = DM_TANI.TANICD(+)
			  		-- �d����}�X�^
			  AND	DT_SHIREH.SIRCD = DM_SHIRE.SIRCD(+)
			  AND	DT_SHIREH.DELKBN = '0'
			  AND	DT_SHIREM.DELKBN = '0'
UNION ALL
		SELECT  
              DM_SHIRE.SIRCD AS �d����CD									--
            , DM_SHIRE.SIRNM1 AS �d���於1									--
            , DM_SHIRE.SIRNM2 AS �d���於2									--
            , DM_SHIRE.SIRNMX AS �d����J�i									--
            , DM_SHIRE.ZENZAN AS �O���c��									--
            , ���t�L���ǉ�(DT_SHRH.SHRYMD) AS ���t							--
            , DT_SHRH.JIGYOCD AS �x�����Ə�CD								--
            , DT_SHRH.SHRNO AS �x���ԍ�										--
            , DT_SHRB.GYONO AS �s�ԍ�										--
            , NULL
            , NULL
            , NULL
            , NULL
            , DK_NYUKIN.NYUKINKBNNM
            , NULL
            , NULL
            , NULL
            , NULL										--
			, NULL
			, DT_SHRB.KING AS ���v
            , DT_SHRH.SHRYMD AS �d�����t����								--
            , 2 AS �敪
		FROM  DM_SHIRE,DT_SHRH,DT_SHRB,DK_NYUKIN
		WHERE
					(DT_SHRH.JIGYOCD = DT_SHRB.JIGYOCD
			  AND	DT_SHRH.SHRNO = DT_SHRB.SHRNO
			  AND	DT_SHRB.NYUKINKBN = DK_NYUKIN.NYUKINKBN
			  		-- �d����}�X�^
			  AND	DT_SHRH.SIRCD = DM_SHIRE.SIRCD
			  AND	DT_SHRH.GETFLG <> '1'
			  AND	DT_SHRB.NYUKINKBN <> '02')
			  OR 	(DT_SHRB.NYUKINKBN = '02' 
			  AND 	DT_SHRB.KAMOKUKBN = '1'
			  AND	DT_SHRH.JIGYOCD = DT_SHRB.JIGYOCD
			  AND	DT_SHRH.SHRNO = DT_SHRB.SHRNO
			  AND	DT_SHRB.NYUKINKBN = DK_NYUKIN.NYUKINKBN
			  		-- �d����}�X�^
			  AND	DT_SHRH.SIRCD = DM_SHIRE.SIRCD
			  AND	DT_SHRH.GETFLG <> '1')
) WK,DM_KANRI
	WHERE 	
		DM_KANRI.KANRINO = '1'
		ORDER BY
				  WK.�d����J�i
				, WK.�d����CD
				, WK.���t
				, WK.�d�����Ə�CD
				, WK.�d���ԍ�
				, WK.�s�ԍ�
;
