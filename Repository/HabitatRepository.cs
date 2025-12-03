using Microsoft.Data.SqlClient;

namespace Pokemon_API.Repositories
{
    public class HabitatRepository : IHabitatRepository
    {
        private readonly string _connectionString;

        public HabitatRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PokemonDB") ?? "Not found";
        }

        public async Task<List<Habitat>> GetAllAsync()
        {
            var habitats = new List<Habitat>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Region, Clima, Temperatura, Descripcion FROM Habitat";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var habitat = new Habitat
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Region = reader.GetString(2),
                                Clima = reader.GetString(3),
                                Temperatura = reader.GetInt32(4),
                                Descripcion = reader.GetString(5)
                            }; 

                            habitats.Add(habitat);
                        }
                    }
                }
            }
            return habitats;
        }

        public async Task<Habitat> GetByIdAsync(int id)
        {
            Habitat habitat = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Region, Clima, Temperatura, Descripcion FROM Habitat WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            habitat = new Habitat
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Region = reader.GetString(2),
                                Clima = reader.GetString(3),
                                Temperatura = reader.GetInt32(4),
                                Descripcion = reader.GetString(5)
                            };
                        }
                    }
                }
            }
            return habitat;
        }

        public async Task AddAsync(Habitat habitat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Habitat (Nombre, Region, Clima, Temperatura, Descripcion) VALUES (@Nombre, @Region, @Clima, @Temperatura, @Descripcion)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", habitat.Nombre);
                    command.Parameters.AddWithValue("@Region", habitat.Region);
                    command.Parameters.AddWithValue("@Clima", habitat.Clima);
                    command.Parameters.AddWithValue("@Temperatura", habitat.Temperatura);
                    command.Parameters.AddWithValue("@Descripcion", habitat.Descripcion);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Habitat habitat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Habitat SET Nombre = @Nombre, Region = @Region, Clima = @Clima, Temperatura = @Temperatura, Descripcion = @Descripcion WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", habitat.Id);
                    command.Parameters.AddWithValue("@Nombre", habitat.Nombre);
                    command.Parameters.AddWithValue("@Region", habitat.Region);
                    command.Parameters.AddWithValue("@Clima", habitat.Clima);
                    command.Parameters.AddWithValue("@Temperatura", habitat.Temperatura);
                    command.Parameters.AddWithValue("@Descripcion", habitat.Descripcion);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Habitat WHERE Id = @Id";
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
                    INSERT INTO Habitat (Nombre, Region, Clima, Temperatura, Descripcion)
                    VALUES 
                    (@Nombre1, @Region1, @Clima1, @Temperatura1, @Descripcion1),
                    (@Nombre2, @Region2, @Clima2, @Temperatura2, @Descripcion2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer habitat
                    command.Parameters.AddWithValue("@Nombre1", "Bosque Verdanturf");
                    command.Parameters.AddWithValue("@Region1", "Hoenn");
                    command.Parameters.AddWithValue("@Clima1", "Templado");
                    command.Parameters.AddWithValue("@Temperatura1", 22);
                    command.Parameters.AddWithValue("@Descripcion1", "Bosque frondoso con abundante vegetación");

                    // Parámetros para el segundo habitat
                    command.Parameters.AddWithValue("@Nombre2", "Cueva Helada");
                    command.Parameters.AddWithValue("@Region2", "Sinnoh");
                    command.Parameters.AddWithValue("@Clima2", "Frío");
                    command.Parameters.AddWithValue("@Temperatura2", -5);
                    command.Parameters.AddWithValue("@Descripcion2", "Cueva congelada con estalactitas de hielo");

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}