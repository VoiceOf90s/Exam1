CREATE TABLE [dbo].[Пассажир]
(
	[Id] INT NOT NULL IDENTITY(1,1), 
    [Имя] NCHAR(10) NOT NULL, 
    [Id_рейса] INT NOT NULL		
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Номер_рейса] FOREIGN KEY ([Id_рейса]) REFERENCES [dbo].[Flight] ([Id])
)
