INSERT [dbo].[Country] ([Id], [Name]) VALUES (1, N'Brazil')
GO
INSERT [dbo].[State] ([Id], [Name], [CountryId]) VALUES (1, N'Minas Gerais', 1)
GO
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (1, N'Juiz de Fora', 1)
GO
INSERT [dbo].[Address] ([Id], [StreetName], [Number], [ZipCode], [CityId]) VALUES (1, N'Rua Diogo Alvares', 777, 36090320, 1)
GO
INSERT [dbo].[Address] ([Id], [StreetName], [Number], [ZipCode], [CityId]) VALUES (2, N'Rua Diogo Alvares', 888, 36090320, 1)
GO
INSERT [dbo].[Address] ([Id], [StreetName], [Number], [ZipCode], [CityId]) VALUES (3, N'Rua Diogo Alvares', 999, 36090320, 1)
