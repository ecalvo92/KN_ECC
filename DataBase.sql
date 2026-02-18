USE [master]
GO

CREATE DATABASE [KN_DB]
GO

USE [KN_DB]
GO

CREATE TABLE [dbo].[tUsuario](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](15) NOT NULL,
	[Contrasenna] [varchar](15) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[CorreoElectronico] [varchar](100) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_tUsuario] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[tUsuario] ON 
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Contrasenna], [Nombre], [CorreoElectronico], [Estado]) VALUES (5, N'305520310', N'20310', N'ALFARO SALAZAR FRANCINNI DE LOS ANGELES', N'falfaro20310@ufide.ac.cr', 1)
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Contrasenna], [Nombre], [CorreoElectronico], [Estado]) VALUES (6, N'113470086', N'70086', N'CONTRERAS ANGULO STEPHANIE PAMELA', N'scontreras70086@ufide.ac.cr', 1)
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Contrasenna], [Nombre], [CorreoElectronico], [Estado]) VALUES (7, N'304590415', N'90415', N'CALVO CASTILLO EDUARDO JOSE', N'ecalvo90415@ufide.ac.cr', 1)
GO
SET IDENTITY_INSERT [dbo].[tUsuario] OFF
GO

ALTER TABLE [dbo].[tUsuario] ADD  CONSTRAINT [UK_tUsuario_Identificacion] UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE PROCEDURE [dbo].[IniciarSesion]
	@CorreoElectronico varchar(100),
	@Contrasenna varchar(15)
AS
BEGIN
	
	SELECT	Consecutivo,
			Identificacion,
			Nombre
	FROM	tUsuario
	WHERE	CorreoElectronico = @CorreoElectronico
		AND Contrasenna = @Contrasenna
		AND Estado = 1

END
GO

CREATE PROCEDURE [dbo].[RegistrarUsuario]
	@Identificacion varchar(15),
	@Contrasenna varchar(15),
	@Nombre varchar(200),
	@CorreoElectronico varchar(100)
AS
BEGIN
	
    INSERT INTO dbo.tUsuario (Identificacion,Contrasenna,Nombre,CorreoElectronico,Estado)
    VALUES (@Identificacion,@Contrasenna,@Nombre,@CorreoElectronico,1)

END
GO