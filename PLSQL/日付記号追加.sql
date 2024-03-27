CREATE OR REPLACE FUNCTION 日付記号追加(
			I_日付			IN		 DM_JIGYO.HOSHUJIKKOYMD%TYPE
) RETURN CHAR
IS
	V_日付			CHAR ( 010 );
	VC_日付桁数		NUMBER ( 001 )		:=8;
BEGIN
	IF RTRIM ( I_日付 ) IS NULL THEN
		RETURN ( '          ' );
	ELSIF RTRIM ( I_日付 ) IS NULL THEN
		RETURN ( '          ' );
	ELSIF RTRIM ( I_日付 ) = '0' THEN
		RETURN ( '          ' );
	ELSIF LENGTH ( I_日付 ) <> VC_日付桁数 THEN
		RETURN ( I_日付 );
	ELSE
		V_日付 := SUBSTR ( I_日付 , 1 , 4 ) || '/' || SUBSTR ( I_日付 , 5 , 2 ) || '/' || SUBSTR ( I_日付 , 7 , 2 );
		RETURN ( V_日付 );
	END IF;
END ;
/
