Use Library


INSERT INTO [Books](Title,DateOfPublication,Author,[Count])
	VALUES
	('��������� �����', '1982.03.07', '���� ������� ', '25'),
	('��� �� �����!', '2016.06.05', '����� ������������,', '25'),
    ('���������. ����� ������', '2013.04.02', '������ ��������', '25'),
	('����� �������', '1831.02.14 ', '��������� ������', '25'),
	('������ ����', '1923.11.23', '����� ������', '25'),
	('���', '1890.12.07', '������� ������', '25')
	go

INSERT INTO [Peoples](Surname,[Name],Lastname,PostId)
	Values
	('admin','admin','admin',1),
	('���������','��������','����������',2),
	('������','�������','�������������',2),
	('����������','������','��������������',2),
	('��������','������','��������',2),
	('���������','�����','��������',2)
	go
INSERT INTO [Users]([Login],[Password],PeopleId)
	Values
	('admin','admin',1)
	Go

INSERT INTO [Posts](Title)
	Values
	('1'),
	('2')
go