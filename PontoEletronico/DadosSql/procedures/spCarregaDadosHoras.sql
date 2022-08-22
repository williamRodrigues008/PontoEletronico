drop procedure spCarregaDadosHoras

go

create procedure spCarregaDadosHoras
	@idUsuario int
as begin

	select
		isnull((
			select isnull(datediff(hour, Entrada, Saida) - datediff(hour, PausaAlmoco, RetornoAlmoco), 0.0)
			from	Registros
			where	(CONVERT(date, Entrada) = CONVERT(date, getdate()))
			and	(IdUsuario = @idUsuario)
		),0) as Dia,
		isnull((
			select sum(Horas_Diaria)
			from Registros
				inner join TotalHoras on
				Registros.IdRegistro = TotalHoras.IdRegistro

			where	(Entrada between dateadd(day, -5, CONVERT(date, getdate())) and getdate())
			and	(Registros.IdUsuario = @idUsuario)
		
		), 0) as Semana,
		isnull((
			select sum(Horas_Diaria)
			from Registros
				inner join TotalHoras on
				Registros.IdRegistro = TotalHoras.IdRegistro

			where	(Entrada between dateadd(month, -1, CONVERT(date, getdate())) and getdate())
			and	(Registros.IdUsuario = @idUsuario)
		),0) as Mes,
		isnull((
			select	sum(Horas_Diaria)
			from	TotalHoras
			where	TotalHoras.IdUsuario = Registros.IdUsuario
		),0) as Total,
		TotalHoras.Horas_Extras as Extra


	from	TotalHoras

		inner join Registros on
		TotalHoras.IdRegistro = Registros.IdRegistro

		inner join Usuarios on
		Usuarios.Id = Registros.IdUsuario


	where	(Usuarios.Id = @idUsuario)

	order by Mes, Semana, Dia, Extra, Total
End
