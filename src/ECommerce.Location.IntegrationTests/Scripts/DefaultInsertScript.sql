﻿INSERT [dbo].[Country] ([Name]) VALUES (N'Brazil')
GO
INSERT [dbo].[State] ([Name], [CountryId]) VALUES (N'Minas Gerais', 1)
GO
INSERT [dbo].[City] ([Name], [StateId]) VALUES (N'Juiz de Fora', 1)
GO
INSERT [dbo].[Address] ([StreetName], [Number], [ZipCode], [CityId]) VALUES (N'Diogo Alvares', 777, 36090320, 1)
GO
INSERT [dbo].[Address] ([StreetName], [Number], [ZipCode], [CityId]) VALUES (N'Diogo Alvares', 888, 36090320, 1)
GO
INSERT [dbo].[Address] ([StreetName], [Number], [ZipCode], [CityId]) VALUES ( N'Diogo Alvares', 999, 36090320, 1)
