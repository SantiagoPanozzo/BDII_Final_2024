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
            Console.WriteLine($"{cedula}");
            Console.WriteLine($"{contrasena}");

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
                    nombre: row["Nombre"].ToString(),
                    apellido: row["Apellido"].ToString(),
                    cedula: Convert.ToInt32(row["Cedula"]),
                    fechaNacimiento: Convert.ToDateTime(row["Fecha_Nacimiento"]), 
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

        public Task<IUsuario> CreateAsync(IUsuario entity)
        {
            throw new NotImplementedException();
        }

        /*public async Task<IUsuario> CreateAsync(UserRegisterDto registerDto)
        {
             // Hash de la contraseña
            var hashedContrasena = ContrasenaHasher.HashContrasena(registerDto.Contrasena);

            // Crear un nuevo usuario (Alumno en este caso)
            var alumno = new Alumno
            (
                nombre: registerDto.Nombre,
                apellido: registerDto.Apellido,
                cedula: registerDto.Cedula,
                contrasena: hashedContrasena,
                fechaNacimiento: registerDto.FechaNacimiento,
                anioIngreso: registerDto.AnioIngreso,
                semestreIngreso: registerDto.SemestreIngreso,
                puntajeTotal: 0,  // Inicializar con valor predeterminado
                campeon: string.Empty,  // Inicializar con valor predeterminado
                subCampeon: string.Empty  // Inicializar con valor predeterminado
            );

            // Guardar en la base de datos
            var query = @"
                INSERT INTO Alumno (Nombre, Apellido, Cedula, Contrasena, Fecha_Nacimiento, Anio_Ingreso, Semestre_Ingreso, Puntaje_Total, Campeon, Sub_Campeon)
                VALUES (@Nombre, @Apellido, @Cedula, @Contrasena, @FechaNacimiento, @AnioIngreso, @SemestreIngreso, @PuntajeTotal, @Campeon, @SubCampeon)";

            var parameters = new Dictionary<string, object>
            {
                { "Nombre", alumno.Nombre },
                { "Apellido", alumno.Apellido },
                { "Cedula", alumno.Cedula },
                { "Contrasena", alumno.Contrasena },
                { "FechaNacimiento", alumno.FechaNacimiento },
                { "AnioIngreso", alumno.AnioIngreso },
                { "SemestreIngreso", alumno.SemestreIngreso },
                { "PuntajeTotal", alumno.PuntajeTotal },
                { "Campeon", alumno.Campeon },
                { "SubCampeon", alumno.SubCampeon }
            };

            var result = await _dbConnection.QueryAsync(query, parameters);

            if (result.Count > 0)
            {
                return alumno;
            }

            return null;
        }

        public Task<IUsuario> CreateAsync(IUsuario entity)
        {
            throw new NotImplementedException();
        }*/

        public Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtener todas las instancias de usuarios.
        /// </summary>
        /// <returns>Todas las instancias de usuarios.</returns>
        public async Task<IUsuario[]> GetAllAsync()
        {
            var usuarios = new List<IUsuario>();

            // Consultar todos los alumnos
            var query = "SELECT * FROM Alumnos";
            var result = await _dbConnection.QueryAsync(query);
            foreach (var row in result)
            {
                usuarios.Add(new Alumno
                (
                    nombre: row["Nombre"].ToString(),
                    apellido: row["Apellido"].ToString(),
                    cedula: Convert.ToInt32(row["Cedula"]),
                    contrasena: row["Contrasena"].ToString(),
                    fechaNacimiento: Convert.ToDateTime(row["Fecha_Nacimiento"]), 
                    anioIngreso : Convert.ToInt32(row["Anio_Ingreso"]),
                    semestreIngreso : Convert.ToInt32(row["Semestre_Ingreso"]),
                    puntajeTotal : Convert.ToInt32(row["Puntaje_Total"]),
                    campeon : row["Campeon"].ToString(),
                    subCampeon :row["Sub_Campeon"].ToString()
                ));
            }

            // Consultar todos los administradores
            query = "SELECT * FROM Administradores";
            result = await _dbConnection.QueryAsync(query);
            foreach (var row in result)
            {
                usuarios.Add(new Administrador(
                    nombre: row["Nombre"].ToString(),
                    apellido: row["Apellido"].ToString(),
                    cedula: Convert.ToInt32(row["Cedula"]),
                    contrasena: row["Contrasena"].ToString(),
                    fechaNacimiento: Convert.ToDateTime(row["Fecha_Nacimiento"]), 
                    rolUniversidad: row["Rol_Universidad"].ToString()          
                ));
            }

            return usuarios.ToArray();
        }

        public Task<IUsuario> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IUsuario> UpdateAsync(object id, IUsuario entity)
        {
            throw new NotImplementedException();
        }

        // Implementa los métodos restantes de la interfaz IService<IUsuario>
        // según las necesidades de tu aplicación.
    }
}
