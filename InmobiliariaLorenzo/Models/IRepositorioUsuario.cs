using MySql.Data.MySqlClient;
using System.Data;
namespace InmobiliariaLorenzo.Models;

using System;
using Microsoft.Extensions.Configuration;

	public interface IRepositorioUsuario : IRepositorio<Usuario>
	{
		Usuario ObtenerPorEmail(string email);
	} 