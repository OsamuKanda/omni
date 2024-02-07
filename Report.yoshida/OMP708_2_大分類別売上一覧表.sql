-------------------------------------------------------------------------------
--�I���j�e�N�m�V�X�e�����v���[�X
--                                                 CREATE:2011/10/31   OKADA
--                                                 Update:   
-------------------------------------------------------------------------------
--OMP708 �啪�ޕʔ���ꗗ�\
-------------------------------------------------------------------------------
CREATE OR REPLACE VIEW V_OMP708_2
AS
SELECT
              ROWNUM  AS EDANUM
            , SUBSTR(DM_KANRI.KINENDO,1,4) || '�N�x' AS �N�x								--�N�x
            , DM_BUNRUID.BUNRUIDCD AS �啪��CD												--�啪�ރR�[�h
            , DM_BUNRUID.BUNRUIDNM AS �啪�ޖ�												--�啪�ޖ�
            , DM_JIGYO.JIGYOCD AS ���Ə�CD													--���Ə��R�[�h
            , DM_JIGYO.JIGYONM AS ���Ə���													--���Ə���
            , DECODE(D1.������z,NULL,0,D1.������z) AS ������z10							--������z10
            , DECODE(D2.������z,NULL,0,D2.������z) AS ������z11							--������z11
            , DECODE(D3.������z,NULL,0,D3.������z) AS ������z12							--������z12
            , DECODE(D4.������z,NULL,0,D4.������z) AS ������z01							--������z01
            , DECODE(D5.������z,NULL,0,D5.������z) AS ������z02							--������z02
            , DECODE(D6.������z,NULL,0,D6.������z) AS ������z03							--������z03
            , DECODE(D7.������z,NULL,0,D7.������z) AS ������z04							--������z04
            , DECODE(D8.������z,NULL,0,D8.������z) AS ������z05							--������z05
            , DECODE(D9.������z,NULL,0,D9.������z) AS ������z06							--������z06
            , DECODE(D10.������z,NULL,0,D10.������z) AS ������z07						--������z07
            , DECODE(D11.������z,NULL,0,D11.������z) AS ������z08						--������z08
            , DECODE(D12.������z,NULL,0,D12.������z) AS ������z09						--������z09
            , DECODE(D1.������z,NULL,0,D1.������z) + DECODE(D2.������z,NULL,0,D2.������z) 
             + DECODE(D3.������z,NULL,0,D3.������z) + DECODE(D4.������z,NULL,0,D4.������z) 
             + DECODE(D5.������z,NULL,0,D5.������z) + DECODE(D6.������z,NULL,0,D6.������z) 
             + DECODE(D7.������z,NULL,0,D7.������z) + + DECODE(D8.������z,NULL,0,D8.������z) 
             + DECODE(D9.������z,NULL,0,D9.������z) + DECODE(D10.������z,NULL,0,D10.������z) 
             + DECODE(D11.������z,NULL,0,D11.������z) + DECODE(D12.������z,NULL,0,D12.������z) AS �N�v
		FROM 
			-- �S��
			(  SELECT DT_BUKKEN.BUNRUIDCD					--�啪�ރR�[�h
			 , DT_BUKKEN.JIGYOCD							--���Ə��R�[�h
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- �Ǘ��}�X�^
	           WHERE	DM_KANRI.KANRINO = '1'
						--�������t <> ALL '0'�ȊO
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--�ŐV������ <> ALL '0'�ȊO
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--�O��敪 = 1 �̏ꍇ�͊�����������ȊO�́A�ŐV�������������Ɏg�p����.
			   AND		DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD) >=	DM_KANRI.KINENDO
			   AND		DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD) < 	TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,12),'YYYYMMDD')
						--�����敪 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D0,
			-- 10��
			(  SELECT DT_BUKKEN.BUNRUIDCD					--�啪�ރR�[�h
			 , DT_BUKKEN.JIGYOCD							--���Ə��R�[�h
			 , 10
			 , SUM(DT_BUKKEN.SOUKINGR) AS ������z
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- �Ǘ��}�X�^
	           WHERE	DM_KANRI.KANRINO = '1'
						--�������t <> ALL '0'�ȊO
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--�ŐV������ <> ALL '0'�ȊO
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--�O��敪 = 1 �̏ꍇ�͊�����������ȊO�́A�ŐV�������������Ɏg�p����.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(DM_KANRI.KINENDO,1,6)
						--�����敪 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D1,
			-- 11��
			(  SELECT DT_BUKKEN.BUNRUIDCD					--�啪�ރR�[�h
			 , DT_BUKKEN.JIGYOCD							--���Ə��R�[�h
			 , 11
			 , SUM(DT_BUKKEN.SOUKINGR) AS ������z
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- �Ǘ��}�X�^
	           WHERE	DM_KANRI.KANRINO = '1'
						--�������t <> ALL '0'�ȊO
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--�ŐV������ <> ALL '0'�ȊO
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--�O��敪 = 1 �̏ꍇ�͊�����������ȊO�́A�ŐV�������������Ɏg�p����.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,1),'YYYYMMDD'),1,6)
						--�����敪 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D2,
			-- 12��
			(  SELECT DT_BUKKEN.BUNRUIDCD					--�啪�ރR�[�h
			 , DT_BUKKEN.JIGYOCD							--���Ə��R�[�h
			 , 12
			 , SUM(DT_BUKKEN.SOUKINGR) AS ������z
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- �Ǘ��}�X�^
	           WHERE	DM_KANRI.KANRINO = '1'
						--�������t <> ALL '0'�ȊO
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--�ŐV������ <> ALL '0'�ȊO
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--�O��敪 = 1 �̏ꍇ�͊�����������ȊO�́A�ŐV�������������Ɏg�p����.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,2),'YYYYMMDD'),1,6)
						--�����敪 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D3,
			-- 01��
			(  SELECT DT_BUKKEN.BUNRUIDCD					--�啪�ރR�[�h
			 , DT_BUKKEN.JIGYOCD							--���Ə��R�[�h
			 , 01
			 , SUM(DT_BUKKEN.SOUKINGR) AS ������z
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- �Ǘ��}�X�^
	           WHERE 		DM_KANRI.KANRINO = '1'
						--�������t <> ALL '0'�ȊO
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--�ŐV������ <> ALL '0'�ȊO
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--�O��敪 = 1 �̏ꍇ�͊�����������ȊO�́A�ŐV�������������Ɏg�p����.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,3),'YYYYMMDD'),1,6)
						--�����敪 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D4,
			-- 02��
			(  SELECT DT_BUKKEN.BUNRUIDCD					--�啪�ރR�[�h
			 , DT_BUKKEN.JIGYOCD							--���Ə��R�[�h
			 , 02
			 , SUM(DT_BUKKEN.SOUKINGR) AS ������z
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- �Ǘ��}�X�^
	           WHERE	DM_KANRI.KANRINO = '1'
						--�������t <> ALL '0'�ȊO
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--�ŐV������ <> ALL '0'�ȊO
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--�O��敪 = 1 �̏ꍇ�͊�����������ȊO�́A�ŐV�������������Ɏg�p����.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,4),'YYYYMMDD'),1,6)
						--�����敪 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D5,
			-- 03��
			(  SELECT DT_BUKKEN.BUNRUIDCD					--�啪�ރR�[�h
			 , DT_BUKKEN.JIGYOCD							--���Ə��R�[�h
			 , 03
			 , SUM(DT_BUKKEN.SOUKINGR) AS ������z
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- �Ǘ��}�X�^
	           WHERE	DM_KANRI.KANRINO = '1'
						--�������t <> ALL '0'�ȊO
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--�ŐV������ <> ALL '0'�ȊO
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--�O��敪 = 1 �̏ꍇ�͊�����������ȊO�́A�ŐV�������������Ɏg�p����.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,5),'YYYYMMDD'),1,6)
						--�����敪 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D6,
			-- 04��
			(  SELECT DT_BUKKEN.BUNRUIDCD					--�啪�ރR�[�h
			 , DT_BUKKEN.JIGYOCD							--���Ə��R�[�h
			 , 04
			 , SUM(DT_BUKKEN.SOUKINGR) AS ������z
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- �Ǘ��}�X�^
	           WHERE	DM_KANRI.KANRINO = '1'
						--�������t <> ALL '0'�ȊO
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--�ŐV������ <> ALL '0'�ȊO
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--�O��敪 = 1 �̏ꍇ�͊�����������ȊO�́A�ŐV�������������Ɏg�p����.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,6),'YYYYMMDD'),1,6)
						--�����敪 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D7,
			-- 05��
			(  SELECT DT_BUKKEN.BUNRUIDCD					--�啪�ރR�[�h
			 , DT_BUKKEN.JIGYOCD							--���Ə��R�[�h
			 , 05
			 , SUM(DT_BUKKEN.SOUKINGR) AS ������z
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- �Ǘ��}�X�^
	           WHERE	DM_KANRI.KANRINO = '1'
						--�������t <> ALL '0'�ȊO
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--�ŐV������ <> ALL '0'�ȊO
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--�O��敪 = 1 �̏ꍇ�͊�����������ȊO�́A�ŐV�������������Ɏg�p����.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,7),'YYYYMMDD'),1,6)
						--�����敪 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D8,
			-- 06��
			(  SELECT DT_BUKKEN.BUNRUIDCD					--�啪�ރR�[�h
			 , DT_BUKKEN.JIGYOCD							--���Ə��R�[�h
			 , 06
			 , SUM(DT_BUKKEN.SOUKINGR) AS ������z
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- �Ǘ��}�X�^
	           WHERE	DM_KANRI.KANRINO = '1'
						--�������t <> ALL '0'�ȊO
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--�ŐV������ <> ALL '0'�ȊO
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--�O��敪 = 1 �̏ꍇ�͊�����������ȊO�́A�ŐV�������������Ɏg�p����.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,8),'YYYYMMDD'),1,6)
						--�����敪 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D9,
			-- 07��
			(  SELECT DT_BUKKEN.BUNRUIDCD					--�啪�ރR�[�h
			 , DT_BUKKEN.JIGYOCD							--���Ə��R�[�h
			 , 07
			 , SUM(DT_BUKKEN.SOUKINGR) AS ������z
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- �Ǘ��}�X�^
	           WHERE	DM_KANRI.KANRINO = '1'
						--�������t <> ALL '0'�ȊO
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--�ŐV������ <> ALL '0'�ȊO
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--�O��敪 = 1 �̏ꍇ�͊�����������ȊO�́A�ŐV�������������Ɏg�p����.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,9),'YYYYMMDD'),1,6)
						--�����敪 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D10,
			-- 08��
			(  SELECT DT_BUKKEN.BUNRUIDCD					--�啪�ރR�[�h
			 , DT_BUKKEN.JIGYOCD							--���Ə��R�[�h
			 , 08
			 , SUM(DT_BUKKEN.SOUKINGR) AS ������z
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- �Ǘ��}�X�^
	           WHERE	DM_KANRI.KANRINO = '1'
						--�������t <> ALL '0'�ȊO
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--�ŐV������ <> ALL '0'�ȊO
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--�O��敪 = 1 �̏ꍇ�͊�����������ȊO�́A�ŐV�������������Ɏg�p����.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,10),'YYYYMMDD'),1,6)
						--�����敪 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D11,
			-- 09��
			(  SELECT DT_BUKKEN.BUNRUIDCD					--�啪�ރR�[�h
			 , DT_BUKKEN.JIGYOCD							--���Ə��R�[�h
			 , 09
			 , SUM(DT_BUKKEN.SOUKINGR) AS ������z
			   FROM 	DT_BUKKEN,DM_KANRI
	              		-- �Ǘ��}�X�^
	           WHERE	DM_KANRI.KANRINO = '1'
						--�������t <> ALL '0'�ȊO
			   AND		DT_BUKKEN.KANRYOYMD <> '00000000'
						--�ŐV������ <> ALL '0'�ȊO
			   AND		DT_BUKKEN.SEIKYUYMD <> '00000000' AND DT_BUKKEN.SEIKYUYMD IS NOT NULL
               			--�O��敪 = 1 �̏ꍇ�͊�����������ȊO�́A�ŐV�������������Ɏg�p����.
			   AND		SUBSTR(DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD),1,6) = SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,11),'YYYYMMDD'),1,6)
						--�����敪 = '0'
			   AND		DT_BUKKEN.DELKBN	=	'0'
               GROUP BY
              			DT_BUKKEN.BUNRUIDCD,DT_BUKKEN.JIGYOCD ) D12,
            DM_JIGYO,DM_KANRI,DM_BUNRUID
            WHERE	D0.JIGYOCD		=	D1.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D2.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D3.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D4.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D5.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D6.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D7.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D8.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D9.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D10.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D11.JIGYOCD(+)
            AND		D0.JIGYOCD	=	D12.JIGYOCD(+)
            AND 	DM_JIGYO.JIGYOCD 	= 	D0.JIGYOCD
            AND		D0.BUNRUIDCD		=	D1.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D1.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D2.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D3.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D4.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D5.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D6.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D7.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D8.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D9.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D10.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D11.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= D12.BUNRUIDCD(+)
            AND		D0.BUNRUIDCD	= DM_BUNRUID.BUNRUIDCD(+)
             		-- �Ǘ��}�X�^
           AND 		DM_KANRI.KANRINO = '1'
;

