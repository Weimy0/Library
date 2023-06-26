Use Library

DBCC CHECKIDENT ('[Peoples]', RESEED, 0);
GO
DBCC CHECKIDENT ('[Users]', RESEED, 0);
GO
INSERT INTO [Books](Title,DateOfPublication,Author,[Count])
	VALUES
	('Узорчатая парча', '1982.03.07', 'Тэру Миямото ', '25'),
	('Это всё зелье!', '2016.06.05', 'Ольга Миклашевская,', '25'),
    ('Некромант. Такая работа', '2013.04.02', 'Сергей Демьянов', '25'),
	('Борис Годунов', '1831.02.14 ', 'Александр Пушкин', '25'),
	('Король треф', '1923.11.23', 'Агата Кристи', '25'),
	('Тас', '1890.12.07', 'Анатоля Франса', '25')
	go




INSERT INTO [Posts](Title)
	Values
	('1'),
	('2')
go
DBCC CHECKIDENT ('[Peoples]', RESEED, 0);
GO
DBCC CHECKIDENT ('[Users]', RESEED, 0);
GO