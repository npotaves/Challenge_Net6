namespace Challenge_Net6.DTO
{
    public class RegisterDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = String.Empty;

        public string Apellido { get; set; } = String.Empty;

        public string Domicilio { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        public CiudadDTO Ciudad { get; set; }

        public bool Habilitado { get; set; }
    }
}
