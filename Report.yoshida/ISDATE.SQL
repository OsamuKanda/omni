CREATE OR REPLACE FUNCTION IS_DATE(
  P_DATE      IN VARCHAR,
  P_FORMAT    IN VARCHAR := 'RR/MM/DD HH24:MI:SS' ,
  P_EXTRACT   IN BOOLEAN := FALSE
)
RETURN NUMBER
AS
--  vDateFormat    VARCHAR2(40) NOT NULL :=  P_FORMAT;
--                     上の記述では NULL の入力を例外処理できない。
  vDateFormat    VARCHAR2(40) NOT NULL := 'RR/MM/DD HH24:MI:SS';
  vDate          DATE;
BEGIN
  vDateFormat := P_FORMAT;
  IF (P_EXTRACT) THEN
    vDateFormat := 'FX' || vDateFormat;
  END IF;
  vDate := TO_DATE(P_DATE, vDateFormat, q'{NLS_CALENDAR = 'GREGORIAN'}' );
--  vDate := TO_DATE(P_DATE, vDateFormat, 'NLS_CALENDAR = ''GREGORIAN''' );
  RETURN 1;
EXCEPTION
  WHEN OTHERS THEN
    RETURN 0;
END;
/