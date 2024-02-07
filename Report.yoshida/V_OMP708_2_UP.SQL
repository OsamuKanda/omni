CREATE OR REPLACE VIEW V_OMP708_2_UP AS
SELECT
        SUBSTR(DM_KANRI.KINENDO,1,4) AS 年度
    ,   DM_JIGYO.JIGYOCD AS 事業所CD
    ,   DM_JIGYO.JIGYONM AS 事業所名
    ,   DM_BUNRUID.BUNRUIDCD AS 大分類CD
    ,   DM_BUNRUID.BUNRUIDNM AS 大分類名
        ,   SUM(
            DECODE(
                SUBSTR(
                    DECODE(
                        DT_BUKKEN.MAEUKEKBN
                        ,1
                        ,DT_BUKKEN.KANRYOYMD
                        ,DT_BUKKEN.SEIKYUYMD
                    )
                    ,1
                    ,6
                )
                ,SUBSTR(
                        TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,0),'YYYYMMDD')
                        ,1
                        ,6
                )
                ,DT_BUKKEN.SOUKINGR
                ,0
            )
        ) AS 月0計
    ,   SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,0),'YYYYMMDD'),1,6)  AS  月0年月
    ,   SUM(
            DECODE(
                SUBSTR(
                    DECODE(
                        DT_BUKKEN.MAEUKEKBN
                        ,1
                        ,DT_BUKKEN.KANRYOYMD
                        ,DT_BUKKEN.SEIKYUYMD
                    )
                    ,1
                    ,6
                )
                ,SUBSTR(
                        TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,1),'YYYYMMDD')
                        ,1
                        ,6
                )
                ,DT_BUKKEN.SOUKINGR
                ,0
            )
        ) AS 月1計
    ,   SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,1),'YYYYMMDD'),1,6)  AS  月1年月
    ,   SUM(
            DECODE(
                SUBSTR(
                    DECODE(
                        DT_BUKKEN.MAEUKEKBN
                        ,1
                        ,DT_BUKKEN.KANRYOYMD
                        ,DT_BUKKEN.SEIKYUYMD
                    )
                    ,1
                    ,6
                )
                ,SUBSTR(
                        TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,2),'YYYYMMDD')
                        ,1
                        ,6
                )
                ,DT_BUKKEN.SOUKINGR
                ,0
            )
        ) AS 月2計
    ,   SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,2),'YYYYMMDD'),1,6)  AS  月2年月
    ,   SUM(
            DECODE(
                SUBSTR(
                    DECODE(
                        DT_BUKKEN.MAEUKEKBN
                        ,1
                        ,DT_BUKKEN.KANRYOYMD
                        ,DT_BUKKEN.SEIKYUYMD
                    )
                    ,1
                    ,6
                )
                ,SUBSTR(
                        TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,3),'YYYYMMDD')
                        ,1
                        ,6
                )
                ,DT_BUKKEN.SOUKINGR
                ,0
            )
        ) AS 月3計
    ,   SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,3),'YYYYMMDD'),1,6)  AS  月3年月
    ,   SUM(
            DECODE(
                SUBSTR(
                    DECODE(
                        DT_BUKKEN.MAEUKEKBN
                        ,1
                        ,DT_BUKKEN.KANRYOYMD
                        ,DT_BUKKEN.SEIKYUYMD
                    )
                    ,1
                    ,6
                )
                ,SUBSTR(
                        TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,4),'YYYYMMDD')
                        ,1
                        ,6
                )
                ,DT_BUKKEN.SOUKINGR
                ,0
            )
        ) AS 月4計
    ,   SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,4),'YYYYMMDD'),1,6)  AS  月4年月
    ,   SUM(
            DECODE(
                SUBSTR(
                    DECODE(
                        DT_BUKKEN.MAEUKEKBN
                        ,1
                        ,DT_BUKKEN.KANRYOYMD
                        ,DT_BUKKEN.SEIKYUYMD
                    )
                    ,1
                    ,6
                )
                ,SUBSTR(
                        TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,5),'YYYYMMDD')
                        ,1
                        ,6
                )
                ,DT_BUKKEN.SOUKINGR
                ,0
            )
        ) AS 月5計
    ,   SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,5),'YYYYMMDD'),1,6)  AS  月5年月
    ,   SUM(
            DECODE(
                SUBSTR(
                    DECODE(
                        DT_BUKKEN.MAEUKEKBN
                        ,1
                        ,DT_BUKKEN.KANRYOYMD
                        ,DT_BUKKEN.SEIKYUYMD
                    )
                    ,1
                    ,6
                )
                ,SUBSTR(
                        TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,6),'YYYYMMDD')
                        ,1
                        ,6
                )
                ,DT_BUKKEN.SOUKINGR
                ,0
            )
        ) AS 月6計
    ,   SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,6),'YYYYMMDD'),1,6)  AS  月6年月
    ,   SUM(
            DECODE(
                SUBSTR(
                    DECODE(
                        DT_BUKKEN.MAEUKEKBN
                        ,1
                        ,DT_BUKKEN.KANRYOYMD
                        ,DT_BUKKEN.SEIKYUYMD
                    )
                    ,1
                    ,6
                )
                ,SUBSTR(
                        TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,7),'YYYYMMDD')
                        ,1
                        ,6
                )
                ,DT_BUKKEN.SOUKINGR
                ,0
            )
        ) AS 月7計
    ,   SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,7),'YYYYMMDD'),1,6)  AS  月7年月
    ,   SUM(
            DECODE(
                SUBSTR(
                    DECODE(
                        DT_BUKKEN.MAEUKEKBN
                        ,1
                        ,DT_BUKKEN.KANRYOYMD
                        ,DT_BUKKEN.SEIKYUYMD
                    )
                    ,1
                    ,6
                )
                ,SUBSTR(
                        TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,8),'YYYYMMDD')
                        ,1
                        ,6
                )
                ,DT_BUKKEN.SOUKINGR
                ,0
            )
        ) AS 月8計
    ,   SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,8),'YYYYMMDD'),1,6)  AS  月8年月
    ,   SUM(
            DECODE(
                SUBSTR(
                    DECODE(
                        DT_BUKKEN.MAEUKEKBN
                        ,1
                        ,DT_BUKKEN.KANRYOYMD
                        ,DT_BUKKEN.SEIKYUYMD
                    )
                    ,1
                    ,6
                )
                ,SUBSTR(
                        TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,9),'YYYYMMDD')
                        ,1
                        ,6
                )
                ,DT_BUKKEN.SOUKINGR
                ,0
            )
        ) AS 月9計
    ,   SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,9),'YYYYMMDD'),1,6)  AS  月9年月
    ,   SUM(
            DECODE(
                SUBSTR(
                    DECODE(
                        DT_BUKKEN.MAEUKEKBN
                        ,1
                        ,DT_BUKKEN.KANRYOYMD
                        ,DT_BUKKEN.SEIKYUYMD
                    )
                    ,1
                    ,6
                )
                ,SUBSTR(
                        TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,10),'YYYYMMDD')
                        ,1
                        ,6
                )
                ,DT_BUKKEN.SOUKINGR
                ,0
            )
        ) AS 月10計
    ,   SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,10),'YYYYMMDD'),1,6)  AS  月10年月
    ,   SUM(
            DECODE(
                SUBSTR(
                    DECODE(
                        DT_BUKKEN.MAEUKEKBN
                        ,1
                        ,DT_BUKKEN.KANRYOYMD
                        ,DT_BUKKEN.SEIKYUYMD
                    )
                    ,1
                    ,6
                )
                ,SUBSTR(
                        TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,11),'YYYYMMDD')
                        ,1
                        ,6
                )
                ,DT_BUKKEN.SOUKINGR
                ,0
            )
        ) AS 月11計
    ,   SUBSTR(TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,11),'YYYYMMDD'),1,6)  AS  月11年月
    ,   SUM(DT_BUKKEN.SOUKINGR)   AS 年計
FROM
        DT_BUKKEN
    ,   DM_KANRI
    ,   DM_JIGYO
    ,   DM_BUNRUID
WHERE
        DM_KANRI.KANRINO = '1'
    --完了日付 <> ALL '0'以外
    AND DT_BUKKEN.KANRYOYMD <> '00000000'
    --最新請求日 <> ALL '0'以外
    AND DT_BUKKEN.SEIKYUYMD <> '00000000'
    --前受区分 = 1 の場合は完了日をそれ以外は、最新請求日を条件に使用する.
    AND	DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD)   >=	DM_KANRI.KINENDO
    AND	DECODE(DT_BUKKEN.MAEUKEKBN,1,DT_BUKKEN.KANRYOYMD,DT_BUKKEN.SEIKYUYMD)   < 	TO_CHAR(ADD_MONTHS(DM_KANRI.KINENDO,12),'YYYYMMDD')
    --無効区分 = '0'
    AND	DT_BUKKEN.DELKBN = '0'
    -- 管理マスタ
    AND DM_KANRI.KANRINO = '1'
    AND DM_JIGYO.JIGYOCD = DT_BUKKEN.JIGYOCD
    AND DM_BUNRUID.BUNRUIDCD = DT_BUKKEN.BUNRUIDCD
GROUP BY
        DM_JIGYO.JIGYOCD
    ,   DM_JIGYO.JIGYONM
    ,   DM_KANRI.KINENDO
    ,   DM_BUNRUID.BUNRUIDCD
    ,   DM_BUNRUID.BUNRUIDNM
