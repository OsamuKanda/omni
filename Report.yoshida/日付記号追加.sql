CREATE OR REPLACE FUNCTION ���t�L���ǉ�(
			I_���t			IN		 DM_JIGYO.HOSHUJIKKOYMD%TYPE
) RETURN CHAR
IS
	V_���t			CHAR ( 010 );
	VC_���t����		NUMBER ( 001 )		:=8;
BEGIN
	IF RTRIM ( I_���t ) IS NULL THEN
		RETURN ( '          ' );
	ELSIF RTRIM ( I_���t ) IS NULL THEN
		RETURN ( '          ' );
	ELSIF RTRIM ( I_���t ) = '0' THEN
		RETURN ( '          ' );
	ELSIF LENGTH ( I_���t ) <> VC_���t���� THEN
		RETURN ( I_���t );
	ELSE
		V_���t := SUBSTR ( I_���t , 1 , 4 ) || '/' || SUBSTR ( I_���t , 5 , 2 ) || '/' || SUBSTR ( I_���t , 7 , 2 );
		RETURN ( V_���t );
	END IF;
END ;
/
