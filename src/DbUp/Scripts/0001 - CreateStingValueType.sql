IF NOT EXISTS (SELECT 1 FROM systypes st WHERE st.name = 'StringValues')
BEGIN
  CREATE TYPE StringValues AS TABLE ( 
    Id INT NULL,
    StringItem NVARCHAR(max) NULL
)
END

