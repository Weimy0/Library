Use Library

DBCC CHECKIDENT ('[Peoples]', RESEED, 0);
GO
DBCC CHECKIDENT ('[Users]', RESEED, 0);
GO
INSERT INTO [Books](Title,DateOfPublication,Author,[Count])
	VALUES
	('��������� �����', '1982.03.07', '���� ������� ', '25'),
	('��� �� �����!', '2016.06.05', '����� ������������,', '25'),
    ('���������. ����� ������', '2013.04.02', '������ ��������', '25'),
	('����� �������', '1831.02.14 ', '��������� ������', '25'),
	('������ ����', '1923.11.23', '����� ������', '25'),
	('���', '1890.12.07', '������� ������', '25')
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