USE [master]
GO

CREATE DATABASE [KN_DB]
GO

USE [KN_DB]
GO

CREATE TABLE [dbo].[tRol](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tRol] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tServicio](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Estado] [int] NOT NULL,
	[Imagen] [varchar](100) NOT NULL,
 CONSTRAINT [PK_tServicio] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tUsuario](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](15) NOT NULL,
	[Contrasenna] [varchar](15) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[CorreoElectronico] [varchar](100) NOT NULL,
	[Estado] [int] NOT NULL,
	[ConsecutivoRol] [int] NOT NULL,
 CONSTRAINT [PK_tUsuario] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[tRol] ON 
GO
INSERT [dbo].[tRol] ([Consecutivo], [Descripcion]) VALUES (1, N'Médico')
GO
INSERT [dbo].[tRol] ([Consecutivo], [Descripcion]) VALUES (2, N'Cliente')
GO
SET IDENTITY_INSERT [dbo].[tRol] OFF
GO

SET IDENTITY_INSERT [dbo].[tServicio] ON 
GO
INSERT [dbo].[tServicio] ([Consecutivo], [Nombre], [Descripcion], [Precio], [Estado], [Imagen]) VALUES (2, N'Terapia de Lenguaje', N'La terapia de lenguaje es un proceso profesional dirigido a personas con dificultades en la comunicación, ya sea en habla, comprensión, lectura o escritura. Su objetivo es mejorar la capacidad de expresar ideas, entender el lenguaje y comunicarse eficazmente, mediante ejercicios personalizados y estrategias de intervención según las necesidades de cada individuo.', CAST(20000.00 AS Decimal(18, 2)), 1, N'----')
GO
INSERT [dbo].[tServicio] ([Consecutivo], [Nombre], [Descripcion], [Precio], [Estado], [Imagen]) VALUES (3, N'Terapia de Lenguaje', N'La terapia de lenguaje es un proceso profesional dirigido a personas con dificultades en la comunicación, ya sea en habla, comprensión, lectura o escritura. Su objetivo es mejorar la capacidad de expresar ideas, entender el lenguaje y comunicarse eficazmente, mediante ejercicios personalizados y estrategias de intervención según las necesidades de cada individuo.', CAST(20000.00 AS Decimal(18, 2)), 1, N'----')
GO
SET IDENTITY_INSERT [dbo].[tServicio] OFF
GO

SET IDENTITY_INSERT [dbo].[tUsuario] ON 
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Contrasenna], [Nombre], [CorreoElectronico], [Estado], [ConsecutivoRol]) VALUES (5, N'305520310', N'ZDJRSUNA', N'ALFARO SALAZAR FRANCINNI DE LOS ANGELES', N'falfaro20310@ufide.ac.cr', 1, 1)
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Contrasenna], [Nombre], [CorreoElectronico], [Estado], [ConsecutivoRol]) VALUES (6, N'113470086', N'70086', N'CONTRERAS ANGULO STEPHANIE PAMELA', N'scontreras70086@ufide.ac.cr', 1, 1)
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Contrasenna], [Nombre], [CorreoElectronico], [Estado], [ConsecutivoRol]) VALUES (7, N'304590415', N'90415', N'CALVO CASTILLO EDUARDO JOSE', N'ecalvo90415@ufide.ac.cr', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[tUsuario] OFF
GO

ALTER TABLE [dbo].[tUsuario] ADD  CONSTRAINT [UK_tUsuario_Identificacion] UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tUsuario]  WITH CHECK ADD  CONSTRAINT [FK_tUsuario_tRol] FOREIGN KEY([ConsecutivoRol])
REFERENCES [dbo].[tRol] ([Consecutivo])
GO
ALTER TABLE [dbo].[tUsuario] CHECK CONSTRAINT [FK_tUsuario_tRol]
GO

CREATE PROCEDURE [dbo].[ActualizarContrasenna]
	@Contrasenna VARCHAR(15),
	@Consecutivo INT
AS
BEGIN
	
	UPDATE	dbo.tUsuario
	SET		Contrasenna = @Contrasenna
	WHERE	Consecutivo = @Consecutivo

END
GO

CREATE PROCEDURE [dbo].[IniciarSesion]
	@CorreoElectronico varchar(100),
	@Contrasenna varchar(15)
AS
BEGIN
	
	SELECT	U.Consecutivo,
			Identificacion,
			Nombre,
			CorreoElectronico,
			ConsecutivoRol,
			R.Descripcion
	FROM	tUsuario U
	INNER	JOIN tRol R ON U.ConsecutivoRol = R.Consecutivo
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

	Declare @RolCliente INT = 1,
			@UsuarioActivo INT = 1
	
    INSERT INTO dbo.tUsuario (Identificacion,Contrasenna,Nombre,CorreoElectronico,Estado,ConsecutivoRol)
    VALUES (@Identificacion,@Contrasenna,@Nombre,@CorreoElectronico,@UsuarioActivo,@RolCliente)

END
GO

CREATE PROCEDURE [dbo].[ValidarCorreo]
	@CorreoElectronico varchar(100)
AS
BEGIN
	
	SELECT	Consecutivo,
			Identificacion,
			Nombre,
			CorreoElectronico
	FROM	tUsuario
	WHERE	CorreoElectronico = @CorreoElectronico
		AND Estado = 1

END
GO