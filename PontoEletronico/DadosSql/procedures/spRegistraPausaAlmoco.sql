drop procedure spRegistraPausaAlmoco

go

create procedure spRegistraPausaAlmoco
	@idUsuario int

as begin

	declare @hoje datetime
	set @hoje= convert(date, getdate(), 112)

	if(exists(
		select top 1 * 
		from	Registros
		where	(convert(date, Entrada,112) = @hoje)
		and		(IdUsuario = @idUsuario)
	))
	begin
		update Registros
		set	PausaAlmoco = getdate()
		where	(IdUsuario = @idUsuario)
		and	(convert(date, Entrada,112) = @hoje)
	end
end
