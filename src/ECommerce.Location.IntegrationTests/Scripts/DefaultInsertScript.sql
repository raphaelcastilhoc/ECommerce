INSERT [dbo].[Country] ([Name]) VALUES (N'Brazil')
GO
INSERT [dbo].[State] ([Name], [CountryId]) VALUES (N'Minas Gerais', (SELECT TOP 1 Id from Country))
GO
INSERT [dbo].[City] ([Name], [StateId]) VALUES (N'Juiz de Fora', (SELECT TOP 1 Id from State))
GO
INSERT [dbo].[Address] ([StreetName], [Number], [ZipCode], [CityId]) VALUES (N'Diogo Alvares', 777, 36090320, (SELECT TOP 1 Id from City))
GO
INSERT [dbo].[Address] ([StreetName], [Number], [ZipCode], [CityId]) VALUES (N'Diogo Alvares', 888, 36090320, (SELECT TOP 1 Id from City))
GO
INSERT [dbo].[Address] ([StreetName], [Number], [ZipCode], [CityId]) VALUES ( N'Diogo Alvares', 999, 36090320, (SELECT TOP 1 Id from City))
