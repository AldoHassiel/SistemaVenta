CREATE PROC SP_REGISTRARUSUARIO(
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@IdUsuarioResultado int output,
@Mensaje varchar(500) output
)
as
begin
	set @IdUsuarioResultado = 0
	set @Mensaje = ''

	if not exists(select * from USUARIO where Documento = @Documento)
	begin
		insert into usuario(Documento, NombreCompleto, Correo, Clave, IdRol, Estado) values
		(@Documento, @NombreCompleto, @Correo, @Clave, @IdRol, @Estado)

		set @IdUsuarioResultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'No se puede repetir el documento para m�s de un usuario'
end

go

CREATE PROC SP_EDITARUSUARIO(
@IdUsuario int,
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''

	if not exists(select * from USUARIO where Documento = @Documento and idUsuario != @IdUsuario)
	begin
		update usuario set
		Documento = @Documento, 
		NombreCompleto = @NombreCompleto, 
		Correo = @Correo, 
		Clave = @Clave, 
		IdRol = @IdRol, 
		Estado = @Estado
		where IdUsuario = @IdUsuario

		set @Respuesta = 1
	end
	else
		set @Mensaje = 'No se puede repetir el documento para m�s de un usuario'
end



declare @respuesta bit
declare @mensaje varchar(500)

exec SP_EDITARUSUARIO 3, '123', 'pruebas 2', 'test@gmail.com', '456', 2, 1, @respuesta output, @mensaje output

select @respuesta
select @mensaje

select * from USUARIO