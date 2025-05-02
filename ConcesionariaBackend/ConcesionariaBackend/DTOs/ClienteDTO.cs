public class ClienteDTO
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public string? DNI { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
}

