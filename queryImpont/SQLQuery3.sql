WITH RankedAnswers AS (
    SELECT 
        qn.Question,
        qn.Distributor,
        qn.PartyHllcode,
        qn.PartyMasterCode,
        -- Combine Answer and PhotoData into one column, defaulting to "Not Answer" when both are NULL
        CASE
            WHEN tra.Answer IS NULL AND tra.PhotoData IS NULL THEN
                'Not Answer'
            ELSE
                -- If only one of them is NULL, combine the available one, otherwise concatenate both
                COALESCE(tra.Answer, '') + ' ' + COALESCE(CAST(tra.PhotoData AS VARCHAR(MAX)), '')
        END AS CombinedData,
        tra.CreatedAt,
        ROW_NUMBER() OVER (PARTITION BY qn.QuestionId, CONVERT(date, tra.CreatedAt, 101) ORDER BY tra.CreatedAt DESC) AS RowNum
    FROM 
        QuestionsNew qn
    JOIN 
        Tbl_ReviewAnswers tra
        ON qn.QuestionId = tra.QuestionId
    WHERE 
        CONVERT(date, tra.CreatedAt, 101) >= '20250205'
        AND CONVERT(date, tra.CreatedAt, 101) <= '20250205'
        AND tra.Rscode = '801495'
)
-- Select only the first row for each QuestionId and CreatedAt, avoiding duplicates
SELECT 
    Question,
    Distributor,
    PartyHllcode,
    PartyMasterCode,
    CombinedData,
    CreatedAt
FROM 
    RankedAnswers
WHERE 
    RowNum = 1;
