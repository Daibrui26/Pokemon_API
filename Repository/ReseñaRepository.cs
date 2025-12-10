using Microsoft.Data.SqlClient;

namespace Pokemon_API.Repositories
{
    public class ReseñaRepository : IReseñaRepository
    {
        private readonly string _connectionString;

        public ReseñaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PokemonDB") ?? "Not found";
        }

        public async Task<List<Reseña>> GetAllAsync()
        {
            var Reseñas = new List<Reseña>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Usuario, Fecha, Texto, Puntuacion FROM Reseña";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Reseña = new Reseña
                            {
                                Id = reader.GetInt32(0),
                                Usuario = reader.GetString(1),
                                Fecha = reader.GetDateTime(2),
                                Texto = reader.GetString(3),
                                Puntuacion = reader.GetInt32(4)
                            }; 

                            Reseñas.Add(Reseña);
                        }
                    }
                }
            }
            return Reseñas;
        }

        public async Task<int> GetTotalReseñas()
        {
            //var Reseñas = new List<Reseña>();
            int Total=0;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                

                string query = "SELECT COUNT(*) FROM Reseña";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                                 Total = reader.GetInt32(0);            
                        }
                    }
                }
            }
            return Total;
        }

        public async Task<List<Reseña>> GetAllFilteredAsync(DateTime? Fecha, int? Puntuacion, string? orderBy, bool ascending)
        {
            var Reseñas = new List<Reseña>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Usuario, Fecha, Texto, Puntuacion FROM Reseña WHERE 1=1";
                var parameters = new List<SqlParameter>();

                // Filtros
                if (Fecha!= null)
                {
                    query += " AND Fecha = @Fecha";
                    parameters.Add(new SqlParameter("@Fecha", Fecha));
                }

                if (Puntuacion!= null)
                {
                    query += " AND Puntuacion = @Puntuacion";
                    parameters.Add(new SqlParameter("@Puntuacion", Puntuacion));
                }

                // Ordenación
                if (!string.IsNullOrWhiteSpace(orderBy))
                {
                    var validColumns = new[] { "usuario", "fecha", "texto", "puntuacion" };
                    var orderByLower = orderBy.ToLower();
                    
                    if (validColumns.Contains(orderByLower))
                    {
                        var direction = ascending ? "ASC" : "DESC";
                        query += $" ORDER BY {orderByLower} {direction}";
                    }else
                {
                    query += " ORDER BY id ASC";
                }
                }
                

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Reseña = new Reseña
                            {
                                Id = reader.GetInt32(0),
                                Usuario = reader.GetString(1),
                                Fecha = reader.GetDateTime(2),
                                Texto = reader.GetString(3),
                                Puntuacion = reader.GetInt32(4),
                            };

                            Reseñas.Add(Reseña);
                        }
                    }
                }
            }
            return Reseñas;
        }

        public async Task<Reseña> GetByIdAsync(int id)
        {
            Reseña Reseña = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Usuario, Fecha, Texto, Puntuacion FROM Reseña WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Reseña = new Reseña
                            {
                                Id = reader.GetInt32(0),
                                Usuario = reader.GetString(1),
                                Fecha = reader.GetDateTime(2),
                                Texto = reader.GetString(3),
                                Puntuacion = reader.GetInt32(4)
                                
                            };
                        }
                    }
                }
            }
            return Reseña;
        }

        public async Task AddAsync(Reseña Reseña)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Reseña (Usuario, Fecha, Texto, Puntuacion) VALUES (@Usuario, @Fecha, @Texto, @Puntuacion)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Usuario", Reseña.Usuario);
                    command.Parameters.AddWithValue("@Fecha", Reseña.Fecha);
                    command.Parameters.AddWithValue("@Texto", Reseña.Texto);
                    command.Parameters.AddWithValue("@Puntuacion", Reseña.Puntuacion);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Reseña Reseña)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Reseña SET Usuario = @Usuario, Fecha = @Fecha, Texto = @Texto, Puntuacion = @Puntuacion WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Reseña.Id);
                    command.Parameters.AddWithValue("@Usuario", Reseña.Usuario);
                    command.Parameters.AddWithValue("@Fecha", Reseña.Fecha);
                    command.Parameters.AddWithValue("@Texto", Reseña.Texto);
                    command.Parameters.AddWithValue("@Puntuacion", Reseña.Puntuacion);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Reseña WHERE Id = @Id";
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
                    INSERT INTO Reseña (Usuario, Fecha, Texto, Puntuacion)
                    VALUES 
                    (@Usuario1, @Fecha1, @Texto1, @Puntuacion1),
                    (@Usuario2, @Fecha2, @Texto2, @Puntuacion2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer Reseña
                    command.Parameters.AddWithValue("@Usuario1", "Pepe22");
                    command.Parameters.AddWithValue("@Fecha1", DateTime.Now);
                    command.Parameters.AddWithValue("@Texto1", "Vaya locuron de Pokemon nano");
                    command.Parameters.AddWithValue("@Puntuacion1", 5);

                    // Parámetros para el segundo Reseña
                    command.Parameters.AddWithValue("@Usuario2", "LaminTamal19");
                    command.Parameters.AddWithValue("@Fecha2", DateTime.Now);
                    command.Parameters.AddWithValue("@Texto2", "Mugron de bicho");
                    command.Parameters.AddWithValue("@Puntuacion2", 1);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}