using Microsoft.Data.SqlClient;

namespace Pokemon_API.Repositories
{
    public class TipoRepository : ITipoRepository
    {
        private readonly string _connectionString;

        public TipoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Tipo>> GetAllAsync()
        {
            var Tipos = new List<Tipo>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT ID, Nombre, Debilidades, Resistencias, Inmunidad, SuperEfectivo, PocoEfectivo, Inutilidad FROM Tipo";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Tipo = new Tipo
                            {
                                ID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Debilidades = reader.GetString(2),
                                Resistencias = reader.GetString(3),
                                Inmunidad = reader.GetString(4),
                                SuperEfectivo = reader.GetString(5),
                                PocoEfectivo = reader.GetString(6),
                                Inutilidad = reader.GetString(7)
                            }; 

                            Tipos.Add(Tipo);
                        }
                    }
                }
            }
            return Tipos;
        }

        public async Task<Tipo> GetByIdAsync(int id)
        {
            Tipo Tipo = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Debilidades, Resistencias, Inmunidad, SuperEfectivo, PocoEfectivo, Inutilidad FROM Tipo WHERE ID = @ID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Tipo = new Tipo
                            {
                                ID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Debilidades = reader.GetString(2),
                                Resistencias = reader.GetString(3),
                                Inmunidad = reader.GetString(4),
                                SuperEfectivo = reader.GetString(5),
                                PocoEfectivo = reader.GetString(6),
                                Inutilidad = reader.GetString(7)
                            };
                        }
                    }
                }
            }
            return Tipo;
        }

        public async Task AddAsync(Tipo Tipo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Tipo (Nombre, Debilidades, Resistencias, Inmunidad, SuperEfectivo, PocoEfectivo, Inutilidad) VALUES (@Nombre, @Debilidades, @Resistencias, @Inmunidad, @SuperEfectivo, @PocoEfectivo, @Inutilidad)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", Tipo.Nombre);
                    command.Parameters.AddWithValue("@Debilidades", Tipo.Debilidades);
                    command.Parameters.AddWithValue("@Resistencias", Tipo.Resistencias);
                    command.Parameters.AddWithValue("@Inmunidad", Tipo.Inmunidad);
                    command.Parameters.AddWithValue("@SuperEfectivo", Tipo.SuperEfectivo);
                    command.Parameters.AddWithValue("@PocoEfectivo", Tipo.PocoEfectivo);
                    command.Parameters.AddWithValue("@Inutilidad", Tipo.Inutilidad);
                    

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Tipo Tipo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Tipo SET Nombre = @Nombre, Debilidades = @Debilidades, Resistencias = @Resistencias, Inmunidad = @Inmunidad, SuperEfectivo = @SuperEfectivo, PocoEfectivo = @PocoEfectivo, Inutilidad = @Inutilidad WHERE ID = @ID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", Tipo.ID);
                    command.Parameters.AddWithValue("@Nombre", Tipo.Nombre);
                    command.Parameters.AddWithValue("@Debilidades", Tipo.Debilidades);
                    command.Parameters.AddWithValue("@Resistencias", Tipo.Resistencias);
                    command.Parameters.AddWithValue("@Inmunidad", Tipo.Inmunidad);
                    command.Parameters.AddWithValue("@SuperEfectivo", Tipo.SuperEfectivo);
                    command.Parameters.AddWithValue("@PocoEfectivo", Tipo.PocoEfectivo);
                    command.Parameters.AddWithValue("@Inutilidad", Tipo.Inutilidad);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Tipo WHERE ID = @ID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

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
                    INSERT INTO Tipo (Nombre, Debilidades, Resistencias, Inmunidad, SuperEfectivo, PocoEfectivo, Inutilidad)
                    VALUES 
                    (@Nombre1, @Debilidades1, @Resistencias1, @Inmunidad1, @SuperEfectivo1, @PocoEfectivo1, @Inutilidad1),
                    (@Nombre2, @Debilidades2, @Resistencias2, @Inmunidad2, @SuperEfectivo2, @PocoEfectivo2, @Inutilidad2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para la primera Tipo
                    command.Parameters.AddWithValue("@Nombre1", "Agua");
                    command.Parameters.AddWithValue("@Debilidades1", "Electrico, Planta");
                    command.Parameters.AddWithValue("@Resistencias1", "Acero, Fuego, Hielo, Agua");
                    command.Parameters.AddWithValue("@Inmunidad1", "");
                    command.Parameters.AddWithValue("@SuperEfectivo1", "Fuego, Tierra, Roca");
                    command.Parameters.AddWithValue("@PocoEfectivo1", "Dragon, Planta, Agua");
                    command.Parameters.AddWithValue("@Inutilidad1", "Fuego, Tierra, Roca");
                    // Parámetros para la segunda Tipo
                    command.Parameters.AddWithValue("@Nombre2", "Fuego");
                    command.Parameters.AddWithValue("@Debilidades2", "Tierra, Roca, Agua");
                    command.Parameters.AddWithValue("@Resistencias2", "Acero, Bicho, Hielo, Hada, Planta, Fuego");
                    command.Parameters.AddWithValue("@Inmunidad2", "");
                    command.Parameters.AddWithValue("@SuperEfectivo2", "Acero, Bicho, Hielo, Planta");
                    command.Parameters.AddWithValue("@PocoEfectivo2", "Dragon, Roca, Agua, Fuego");
                    command.Parameters.AddWithValue("@Inutilidad2", "");
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}