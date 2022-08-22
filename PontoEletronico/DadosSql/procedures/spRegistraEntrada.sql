drop procedure spRegistraEntrada

go

create procedure spRegistraEntrada
	@idUsuario int
as begin

	declare @hoje datetime
	set @hoje= convert(date, getdate(), 112)

	if(not exists(
		select top 1 * 
		from	Registros
		where	(convert(date, Entrada,112) = @hoje)
		and		(IdUsuario = @idUsuario)
	))
	begin
		insert into Registros (Entrada, IdUsuario) 
		values	(getdate(), @idUsuario)
	end
end
