INSERT [dbo].[Country] ([Name]) VALUES (N'Brazil')
GO
INSERT [dbo].[State] ([Name], [CountryId]) VALUES (N'Minas Gerais', (SELECT TOP 1 Id from Country))
GO
INSERT [dbo].[City] ([Name], [StateId]) VALUES (N'Juiz de Fora', (SELECT TOP 1 Id from State))
