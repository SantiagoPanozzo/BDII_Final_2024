namespace PencaAPI.DTOs
{
    /// <summary>
    /// DTO para el registro de nuevos usuarios.
    /// </summary>
    public class UserRegisterDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Cedula { get; set; }
        public string Contrasena { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int AnioIngreso { get; set; }
        public int SemestreIngreso { get; set; }
    }
}
