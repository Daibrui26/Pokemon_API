using Microsoft.Data.SqlClient;

namespace Pokemon_API.Repositories
{
    //Implementa el IRepository
    public class OpinionRepository : IOpinionRepository
    {
        private readonly string _connectionString;

        public OpinionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PokemonDB") ?? "Not found";
        }

        public async Task<List<Opinion>> GetAllAsync()  //Obtiene las habilidades de la base
        {
            var opiniones = new List<Opinion>();

            using (var connection = new SqlConnection(_connectionString))  //Cierra la conexion al terminar
            {
                await connection.OpenAsync(); //Abre conexion a la BD

                string query = "SELECT Id, Usuario, Comentario, Calificacion, Fecha FROM Opinion";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var opinion = new Opinion //Creamos un objeto con los siguientes datos
                            {
                                Id = reader.GetInt32(0),
                                Usuario = reader.GetString(1),
                                Comentario = reader.GetString(2),
                                Calificacion = reader.GetDouble(3),
                                Fecha = reader.GetString(4),

                            }; 

                            opiniones.Add(opinion);
                        }
                    }
                }
            }
            return opiniones;
        }

        public async Task<List<Opinion>> GetAllFilteredAsync(string? Usuario, double? Calificacion, string? orderBy, bool ascending)
{
    var opinions = new List<Opinion>();

    using (var connection = new SqlConnection(_connectionString))
    {
        await connection.OpenAsync();

        string query = "SELECT Id, Usuario, Comentario, Calificacion, Fecha FROM Opinion WHERE 1=1";
        var parameters = new List<SqlParameter>();

        // Filtros
        if (Calificacion.HasValue)
        {
            query += " AND Calificacion = @Calificacion";
            parameters.Add(new SqlParameter("@Calificacion", Calificacion.Value));
        }

        if (!string.IsNullOrWhiteSpace(Usuario))
        {
            query += " AND Usuario = @Usuario";
            parameters.Add(new SqlParameter("@Usuario", Usuario));
        }

        // Ordenación
        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            var validColumns = new[] { "id", "usuario", "comentario", "calificacion", "fecha" };
            var orderByLower = orderBy.ToLower();
            
            if (validColumns.Contains(orderByLower))
            {
                var direction = ascending ? "ASC" : "DESC";
                query += $" ORDER BY {orderByLower} {direction}";
            }
            else
            {
                query += " ORDER BY usuario ASC";
            }
        }

        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddRange(parameters.ToArray());

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var opinion = new Opinion
                    {
                        Id = reader.GetInt32(0),
                        Usuario = reader.GetString(1),
                        Comentario = reader.GetString(2),
                        Calificacion = reader.GetDouble(3),
                        Fecha = reader.GetString(4),
                    };

                    opinions.Add(opinion);
                }
            }
        }
    }
    return opinions;
}

        public async Task<Opinion> GetByIdAsync(int id)  
        {
            Opinion opinion = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Usuario, Comentario, Calificacion, Fecha FROM Opinion WHERE Id = @Id"; 
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {

                        if (await reader.ReadAsync()) 
                        {
                            opinion = new Opinion
                            {
                                Id = reader.GetInt32(0),
                                Usuario = reader.GetString(1),
                                Comentario = reader.GetString(2),
                                Calificacion = reader.GetDouble(3),
                                Fecha = reader.GetString(4),
                            };
                        }
                    }
                }
            }
            return opinion;
        }

        public async Task AddAsync(Opinion opinion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Opinion (Usuario, Comentario, Calificacion, Fecha) VALUES (@Usuario, @Comentario, @Calificacion, @Fecha)";  //Consulta para insertar datos
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Usuario", opinion.Usuario);
                    command.Parameters.AddWithValue("@Comentario", opinion.Comentario);
                    command.Parameters.AddWithValue("@Calificacion", opinion.Calificacion);
                    command.Parameters.AddWithValue("@Fecha", opinion.Fecha);

                    await command.ExecuteNonQueryAsync();  //Ejecuta la consulta
                }
            }
        }

        public async Task UpdateAsync(Opinion opinion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Opinion SET Usuario = @Usuario, Comentario = @Comentario, Calificacion = @Calificacion, Fecha = @Fecha WHERE Id = @Id";  //Consulta para actualizar las habilidades
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", opinion.Id);
                    command.Parameters.AddWithValue("@Usuario", opinion.Usuario);
                    command.Parameters.AddWithValue("@Comentario", opinion.Comentario);
                    command.Parameters.AddWithValue("@Calificacion", opinion.Calificacion);
                    command.Parameters.AddWithValue("@Fecha", opinion.Fecha);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Opinion WHERE Id = @Id"; 
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task InicializarDatosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Comando SQL para insertar datos iniciales
                var query = @"
                    INSERT INTO Opinion (Usuario, Comentario, Calificacion)
                    VALUES 
                    (@Usuario1, @Comentario1, @Calificacion1, @Fecha1),
                    (@Usuario2, @Comentario2, @Calificacion2, @Fecha2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para la primera habilidad
                    command.Parameters.AddWithValue("@Usuario1", "Daibrui26");
                    command.Parameters.AddWithValue("@Comentario1", "Se puede mejorar");
                    command.Parameters.AddWithValue("@Calificacion1", 3.5);
                    command.Parameters.AddWithValue("@Fecha1", "22/06/2015");

                    // Parámetros para la segunda habilidad
                    command.Parameters.AddWithValue("@Usuario2", "Adaptable");
                    command.Parameters.AddWithValue("@Comentario2", "Es perfecto");
                    command.Parameters.AddWithValue("@Calificacion2", 5);
                    command.Parameters.AddWithValue("@Fecha2", "22/03/2015");

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        
    }

}