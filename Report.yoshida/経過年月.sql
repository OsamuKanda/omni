CREATE OR REPLACE FUNCTION �o�ߔN��(
			
			I_�ݒu			IN		 DM_JIGYO.HOSHUYMD%TYPE
) RETURN CHAR
IS
	V_��1			NUMBER (007);
	V_��2			NUMBER (007);
	V_��			NUMBER (007);
	V_�o�ߔN		NUMBER (004);
	V_�o�ߌ�		NUMBER (002);
	V_����			CHAR(020);
	
BEGIN
	V_�o�ߔN := 0 ;
	
	IF RTRIM ( I_�ݒu ) IS NULL THEN
		RETURN ( '          ' );
	ELSIF RTRIM ( I_�ݒu ) = '000000' THEN
		RETURN ( '          ' );
	ELSE
		V_��1 := (TO_NUMBER(SUBSTR(TO_CHAR(SYSDATE,'YYYYMMDD'),1,4)) * 12) +  TO_NUMBER(SUBSTR(TO_CHAR(SYSDATE,'YYYYMMDD'),5,2));
		V_��2 := (TO_NUMBER(SUBSTR(I_�ݒu,1,4)) * 12) +  TO_NUMBER(SUBSTR(I_�ݒu,5,2));
		V_�� := V_��1 - V_��2;
		IF V_�� >= 12 THEN
			V_�o�ߔN := TRUNC(V_�� / 12 );
		END IF ;
		V_�o�ߌ� := V_�� - (V_�o�ߔN * 12 );
		V_���� := V_�o�ߔN || '�N' || V_�o�ߌ� || '����' ;
		RETURN ( V_���� );
	END IF;
END ;
/
