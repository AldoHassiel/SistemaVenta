/* PROCESOS PARA REGISTRAR UNA COMPRA */

CREATE TYPE [dbo].[EDetalle_Compra] AS TABLE (
	[IdProducto] int NULL,
	[PrecioCompra] decimal(18,2) NULL,
	[PrecioVenta] decimal (18,2) NULL,
	[Cantidad] int NULL,
	[MontoTotal] decimal(18,2) NULL
)

GO

CREATE PROCEDURE sp_RegistrarCompra(
@IdUsuario int,
@IdProveedor int,
@TipoDocumento varchar(500),
@NumeroDocumento varchar(500),
@MontoTotal decimal (18,2),
@DetalleCompra [EDetalle_Compra] READONLY,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	begin try 
		declare @idcompra int = 0
		set @Resultado = 1
		set @Mensaje = ''

		begin transaction registro

		insert into COMPRA(IdUsuario,IdProveedor,TipoDocumento,NumeroDocumento,MontoTotal)
		values (@IdUsuario,@IdProveedor,@TipoDocumento,@NumeroDocumento,@MontoTotal)

		set @idcompra = SCOPE_IDENTITY()

		insert into DETALLE_COMPRA(IdCompra,IdProducto,PrecioCompra,PrecioVenta,Cantidad,MontoTotal)
		select @idcompra,IdProducto,PrecioCompra,PrecioVenta,Cantidad,MontoTotal from @DetalleCompra

		update p set p.Stock = p.Stock + dc.Cantidad,
		p.PrecioCompra = dc.PrecioCompra,
		p.PrecioVenta = dc.PrecioVenta
		from PRODUCTO p
		inner join @DetalleCompra dc on dc.IdProducto = p.IdProducto			
		commit transaction registro
	end try
	begin catch
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro
	end catch
end
