using Microsoft.Data.SqlClient;

namespace Pokemon_API.Repositories
{
    public class ObjetoRepository : IObjetoRepository
    {
        private readonly string _connectionString;

        public ObjetoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PokemonDB") ?? "Not found";
        }

        public async Task<List<Objeto>> GetAllAsync()
        {
            var Objetos = new List<Objeto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Descripcion, Precio, Unico, Efecto FROM Objeto";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Objeto = new Objeto
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                Precio = reader.GetDouble(3),
                                Unico = reader.GetBoolean(4),
                                Efecto = reader.GetString(5)
                            }; 

                            Objetos.Add(Objeto);
                        }
                    }
                }
            }
            return Objetos;
        }

        public async Task<Objeto> GetByIdAsync(int id)
        {
            Objeto Objeto = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Descripcion, Precio, Unico, Efecto FROM Objeto WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Objeto = new Objeto
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                Precio = reader.GetDouble(3),
                                Unico = reader.GetBoolean(4),
                                Efecto = reader.GetString(5)
                            };
                        }
                    }
                }
            }
            return Objeto;
        }

        public async Task AddAsync(Objeto Objeto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Objeto (Nombre, Descripcion, Precio, Unico, Efecto) VALUES (@Nombre, @Descripcion, @Precio, @Unico, @Efecto)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", Objeto.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", Objeto.Descripcion);
                    command.Parameters.AddWithValue("@Precio", Objeto.Precio);
                    command.Parameters.AddWithValue("@Unico", Objeto.Unico);
                    command.Parameters.AddWithValue("@Efecto", Objeto.Efecto);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Objeto Objeto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Objeto SET Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, Unico = @Unico, Efecto = @Efecto WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Objeto.Id);
                    command.Parameters.AddWithValue("@Nombre", Objeto.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", Objeto.Descripcion);
                    command.Parameters.AddWithValue("@Precio", Objeto.Precio);
                    command.Parameters.AddWithValue("@Unico", Objeto.Unico);
                    command.Parameters.AddWithValue("@Efecto", Objeto.Efecto);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Objeto WHERE Id = @Id";
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
                    INSERT INTO Objeto (Nombre, Descripcion, Precio, Unico, Efecto)
                    VALUES 
                    (@Nombre1, @Descripcion1, @Precio1, @Unico1, @Efecto1),
                    (@Nombre2, @Descripcion2, @Precio2, @Unico2, @Efecto2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para la primera Objeto
                    command.Parameters.AddWithValue("@Nombre1", "Cinta Elegida");
                    command.Parameters.AddWithValue("@Descripcion1", "Cinta amarilla obtenida en los dojos");
                    command.Parameters.AddWithValue("@Precio1", 200.0);
                    command.Parameters.AddWithValue("@Unico1", 0);
                    command.Parameters.AddWithValue("@Efecto1", "Aumenta en 1.5 la estadistica de ataque pero solo podras usar un movimiento");

                    // Parámetros para la segunda Objeto
                    command.Parameters.AddWithValue("@Nombre2", "Blazikenita");
                    command.Parameters.AddWithValue("@Descripcion2", "Megapiedra de Blaziken");
                    command.Parameters.AddWithValue("@Precio2", 0);
                    command.Parameters.AddWithValue("@Unico2", 1);
                    command.Parameters.AddWithValue("@Efecto2", "Permite a Blaziken Megaevolucionar");

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}