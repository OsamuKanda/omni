CREATE OR REPLACE FUNCTION 経過年月(
			
			I_設置			IN		 DM_JIGYO.HOSHUYMD%TYPE
) RETURN CHAR
IS
	V_月1			NUMBER (007);
	V_月2			NUMBER (007);
	V_差			NUMBER (007);
	V_経過年		NUMBER (004);
	V_経過月		NUMBER (002);
	V_結果			CHAR(020);
	
BEGIN
	V_経過年 := 0 ;
	
	IF RTRIM ( I_設置 ) IS NULL THEN
		RETURN ( '          ' );
	ELSIF RTRIM ( I_設置 ) = '000000' THEN
		RETURN ( '          ' );
	ELSE
		V_月1 := (TO_NUMBER(SUBSTR(TO_CHAR(SYSDATE,'YYYYMMDD'),1,4)) * 12) +  TO_NUMBER(SUBSTR(TO_CHAR(SYSDATE,'YYYYMMDD'),5,2));
		V_月2 := (TO_NUMBER(SUBSTR(I_設置,1,4)) * 12) +  TO_NUMBER(SUBSTR(I_設置,5,2));
		V_差 := V_月1 - V_月2;
		IF V_差 >= 12 THEN
			V_経過年 := TRUNC(V_差 / 12 );
		END IF ;
		V_経過月 := V_差 - (V_経過年 * 12 );
		V_結果 := V_経過年 || '年' || V_経過月 || 'ヶ月' ;
		RETURN ( V_結果 );
	END IF;
END ;
/
