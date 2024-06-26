using Npgsql;
using PencaAPI.DatabaseConnection;
using PencaAPI.DTOs;
using PencaAPI.Models;

namespace PencaAPI.Services
{
    /// <summary>
    /// Servicio para la gestión de usuarios.
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly PgDatabaseConnection _dbConnection;

        /// <summary>
        /// Constructor del servicio de usuario.
        /// </summary>
        /// <param name="dbConnection">Conexión a la base de datos PostgreSQL.</param>
        public UsuarioService(PgDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Autentica a un usuario por su cédula y contraseña.
        /// </summary>
        /// <param name="cedula">Cédula del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>El usuario autenticado, o null si la autenticación falla.</returns>
        public async Task<UsuarioDTO> Authenticate(int cedula, string contrasena)
        {
            try{

                // Consultar en la tabla Alumnos
                var query = "SELECT * FROM Alumno WHERE Cedula = @Cedula";
                var parameters = new Dictionary<string, object>
                {
                    { "Cedula", cedula }
                };

                var result = await _dbConnection.QueryAsync(query, parameters);
                if (result.Count > 0)
                {
                    var alumno= result[0];
                    var alumnoADevolver = new UsuarioDTO(
                                nombre: (string)alumno["nombre"],
                                apellido: (string)alumno["apellido"],
                                cedula: (int)alumno["cedula"],
                                fechaNacimiento: (DateTime)alumno["fecha_nacimiento"],
                                rol: "alumno"
                            );
                    string contrasenaBase = (string)alumno["contrasena"];

                    //Verificar la contraseña
                    if (ContrasenaHasher.VerifyContrasena(contrasena,contrasenaBase))
                    {
                        return alumnoADevolver;
                    }
                }

                // Consultar en la tabla Administradores
                query = "SELECT * FROM Administrador WHERE Cedula = @Cedula";
                result = await _dbConnection.QueryAsync(query, parameters);
                if (result.Count > 0)
                {
                    var row = result[0];
            
                    var admin = new UsuarioDTO(
                        nombre: row["nombre"].ToString(),
                        apellido: row["apellido"].ToString(),
                        cedula: Convert.ToInt32(row["cedula"]),
                        fechaNacimiento: Convert.ToDateTime(row["fecha_nacimiento"]), 
                        rol: "admnin"        
                    );
                    string contrasenaBase = (string)row["contrasena"];
                    //Verificar la contraseña
                    if (ContrasenaHasher.VerifyContrasena(contrasena,contrasenaBase))
                    {
                        return admin;
                    }
            
                }

                // Si no se encuentra ningún usuario con la cédula y contraseña proporcionadas
                return null;
            }
            catch (PostgresException e)
            {
                throw new ArgumentException("Ocurrió un error al acceder a la base de datos.", e);
            }
        }


        
       

        
    }
}
