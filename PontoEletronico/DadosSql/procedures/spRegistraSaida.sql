drop procedure spRegistraSaida

go

create procedure spRegistraSaida
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
		set	Saida = getdate()
		where	(IdUsuario = @idUsuario)
		and	(convert(date, Entrada,112) = @hoje)

		insert into TotalHoras(Horas_Diaria, Horas_Pausas, Horas_Extras, IdRegistro, IdUsuario)
		Select	(datediff(hour, Entrada, Saida) - datediff(hour, PausaAlmoco, RetornoAlmoco)),
			(datediff(hour, PausaAlmoco, RetornoAlmoco)),
			((datediff(hour, Entrada, Saida) - datediff(hour, PausaAlmoco, RetornoAlmoco)) - 8),
			IdRegistro,
			IdUsuario
		from	Registros
		where	(IdUsuario = @idUsuario)
		and	(convert(date, Entrada,112) = @hoje)
	end
end
