using Microsoft.Data.SqlClient;

namespace Pokemon_API.Repositories
{
    //Implementa el IRepository
    public class HabilidadRepository : IHabilidadRepository
    {
        private readonly string _connectionString;

        public HabilidadRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PokemonDB") ?? "Not found";
        }

        public async Task<List<Habilidad>> GetAllAsync()  //Obtiene las habilidades de la base
        {
            var habilidades = new List<Habilidad>();

            using (var connection = new SqlConnection(_connectionString))  //Cierra la conexion al terminar
            {
                await connection.OpenAsync(); //Abre conexion a la BD

                string query = "SELECT Id, Nombre, Descripcion, Beneficiosa, Oculta, Unica FROM Habilidad";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var habilidad = new Habilidad //Creamos un objeto con los siguientes datos
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                Beneficiosa = reader.GetBoolean(3),
                                Oculta = reader.GetBoolean(4),
                                Unica = reader.GetBoolean(5)

                            }; 

                            habilidades.Add(habilidad);
                        }
                    }
                }
            }
            return habilidades;
        }

        public async Task<Habilidad> GetByIdAsync(int id)  //Pillamos Habilidad por ID
        {
            Habilidad habilidad = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Descripcion, Beneficiosa, Oculta, Unica FROM Habilidad WHERE Id = @Id"; //Consulta para mostrar las habilidades
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {

                        if (await reader.ReadAsync()) // Devuelve true si hay datos
                        {
                            habilidad = new Habilidad
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                Beneficiosa = reader.GetBoolean(3),
                                Oculta = reader.GetBoolean(4),
                                Unica = reader.GetBoolean(5)
                            };
                        }
                    }
                }
            }
            return habilidad;
        }

        public async Task AddAsync(Habilidad habilidad)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Habilidad (Nombre, Descripcion, Beneficiosa, Oculta, Unica) VALUES (@Nombre, @Descripcion, @Beneficiosa, @Oculta, @Unica)";  //Consulta para insertar datos
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", habilidad.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", habilidad.Descripcion);
                    command.Parameters.AddWithValue("@Beneficiosa", habilidad.Beneficiosa);
                    command.Parameters.AddWithValue("@Oculta", habilidad.Oculta);
                    command.Parameters.AddWithValue("@Unica", habilidad.Unica);

                    await command.ExecuteNonQueryAsync();  //Ejecuta la consulta
                }
            }
        }

        public async Task UpdateAsync(Habilidad habilidad)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Habilidad SET Nombre = @Nombre, Descripcion = @Descripcion, Beneficiosa = @Beneficiosa, Oculta = @Oculta, Unica = @Unica WHERE Id = @Id";  //Consulta para actualizar las habilidades
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", habilidad.Id);
                    command.Parameters.AddWithValue("@Nombre", habilidad.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", habilidad.Descripcion);
                    command.Parameters.AddWithValue("@Beneficiosa", habilidad.Beneficiosa);
                    command.Parameters.AddWithValue("@Oculta", habilidad.Oculta);
                    command.Parameters.AddWithValue("@Unica", habilidad.Unica);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Habilidad WHERE Id = @Id"; //Consulta para eliminar habilidad
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
                    INSERT INTO Habilidad (Nombre, Descripcion, Beneficiosa)
                    VALUES 
                    (@Nombre1, @Descripcion1, @Beneficiosa1, @Oculta1, @Unica1),
                    (@Nombre2, @Descripcion2, @Beneficiosa2, @Oculta2, @Unica2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para la primera habilidad
                    command.Parameters.AddWithValue("@Nombre1", "Intimidación");
                    command.Parameters.AddWithValue("@Descripcion1", "Reduce el ataque del rival");
                    command.Parameters.AddWithValue("@Beneficiosa1", true);
                    command.Parameters.AddWithValue("@Oculta1", true);
                    command.Parameters.AddWithValue("@Unica1", false);

                    // Parámetros para la segunda habilidad
                    command.Parameters.AddWithValue("@Nombre2", "Adaptable");
                    command.Parameters.AddWithValue("@Descripcion2", "Aumenta el daño de movimientos del mismo tipo");
                    command.Parameters.AddWithValue("@Beneficiosa2", true);
                    command.Parameters.AddWithValue("@Oculta2", true);
                    command.Parameters.AddWithValue("@Unica2", false);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}