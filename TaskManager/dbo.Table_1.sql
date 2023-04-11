CREATE TABLE [dbo].[Tasks]
(
	[Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Tytul]     VARCHAR (50) NOT NULL,
    [DataRoz]   VARCHAR (50) NOT NULL,
    [DataZak]   VARCHAR (50) NOT NULL,
    [Priorytet] VARCHAR (10) NOT NULL, 
    [Ukończono] VARCHAR(5) NOT NULL, 
    [Tresc] VARCHAR(50) NULL,
)
