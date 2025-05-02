namespace ConcesionariaBackend.Models
{
    public class Cliente
    {
        public int Id { get; set; } // idCliente
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? DNI { get; set; }
    }
}


