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

go

CREATE PROC SP_ELIMINARUSUARIO(
@IdUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	declare @pasoreglas bit = 1

	IF EXISTS(SELECT * FROM COMPRA C
	INNER JOIN USUARIO U ON U.IdUsuario = C.IdUsuario
	WHERE U.IDUSUARIO = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el suuario se encuentra relacionado a una COMPRA\n'
	END

	IF EXISTS(SELECT * FROM VENTA V
	INNER JOIN USUARIO U ON U.IdUsuario = V.IdUsuario
	WHERE U.IDUSUARIO = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el suuario se encuentra relacionado a una VENTA\n'
	END

	if(@pasoreglas = 1)
	begin
		delete from USUARIO where IdUsuario = @IdUsuario
		set @Respuesta = 1
	end

end


declare @respuesta bit
declare @mensaje varchar(500)

exec SP_EDITARUSUARIO 3, '123', 'pruebas 2', 'test@gmail.com', '456', 2, 1, @respuesta output, @mensaje output

select @respuesta
select @mensaje

select * from USUARIO