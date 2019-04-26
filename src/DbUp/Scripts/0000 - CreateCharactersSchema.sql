IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Characters')
BEGIN
EXEC('CREATE SCHEMA Characters')
END