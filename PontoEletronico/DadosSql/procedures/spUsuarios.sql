drop procedure spUsuarios

go

create procedure spUsuarios
	@email varchar(50),
	@senha varchar(10)
as begin
	--execu��o da procedure de login
	select *
	from	Usuarios
	where	(Email = @email)
	and	(Senha = @senha)

end
