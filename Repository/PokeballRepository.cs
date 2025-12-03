using Microsoft.Data.SqlClient;

namespace Pokemon_API.Repositories
{
    public class PokeballRepository : IPokeballRepository
    {
        private readonly string _connectionString;

        public PokeballRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PokemonDB") ?? "Not found";
        }

        public async Task<List<Pokeball>> GetAllAsync()
        {
            var pokeballs = new List<Pokeball>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Ratio, Precio, Color, Efecto FROM Pokeball";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var pokeball = new Pokeball
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Ratio = reader.GetDouble(2),
                                Precio = reader.GetDouble(3),
                                Color = reader.GetString(4),
                                Efecto = reader.GetString(5)
                            }; 

                            pokeballs.Add(pokeball);
                        }
                    }
                }
            }
            return pokeballs;
        }

        public async Task<Pokeball> GetByIdAsync(int id)
        {
            Pokeball pokeball = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Ratio, Precio, Color, Efecto FROM Pokeball WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            pokeball = new Pokeball
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Ratio = reader.GetDouble(2),
                                Precio = reader.GetDouble(3),
                                Color = reader.GetString(4),
                                Efecto = reader.GetString(5)
                            };
                        }
                    }
                }
            }
            return pokeball;
        }

        public async Task AddAsync(Pokeball pokeball)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Pokeball (Nombre, Ratio, Precio, Color, Efecto) VALUES (@Nombre, @Ratio, @Precio, @Color, @Efecto)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", pokeball.Nombre);
                    command.Parameters.AddWithValue("@Ratio", pokeball.Ratio);
                    command.Parameters.AddWithValue("@Precio", pokeball.Precio);
                    command.Parameters.AddWithValue("@Color", pokeball.Color);
                    command.Parameters.AddWithValue("@Efecto", pokeball.Efecto);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Pokeball pokeball)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Pokeball SET Nombre = @Nombre, Ratio = @Ratio, Precio = @Precio, Color = @Color, Efecto = @Efecto WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", pokeball.Id);
                    command.Parameters.AddWithValue("@Nombre", pokeball.Nombre);
                    command.Parameters.AddWithValue("@Ratio", pokeball.Ratio);
                    command.Parameters.AddWithValue("@Precio", pokeball.Precio);
                    command.Parameters.AddWithValue("@Color", pokeball.Color);
                    command.Parameters.AddWithValue("@Efecto", pokeball.Efecto);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Pokeball WHERE Id = @Id";
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
                    INSERT INTO Pokeball (Nombre, Ratio, Precio, Color, Efecto)
                    VALUES 
                    (@Nombre1, @Ratio1, @Precio1, @Color1, @Efecto1),
                    (@Nombre2, @Ratio2, @Precio2, @Color2, @Efecto2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para la primera pokeball
                    command.Parameters.AddWithValue("@Nombre1", "Pokeball");
                    command.Parameters.AddWithValue("@Ratio1", 1.0);
                    command.Parameters.AddWithValue("@Precio1", 200.0);
                    command.Parameters.AddWithValue("@Color1", "Roja y blanca");
                    command.Parameters.AddWithValue("@Efecto1", "Captura estándar");

                    // Parámetros para la segunda pokeball
                    command.Parameters.AddWithValue("@Nombre2", "Ultraball");
                    command.Parameters.AddWithValue("@Ratio2", 2.0);
                    command.Parameters.AddWithValue("@Precio2", 1200.0);
                    command.Parameters.AddWithValue("@Color2", "Negra y amarilla");
                    command.Parameters.AddWithValue("@Efecto2", "Mayor ratio de captura");

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}