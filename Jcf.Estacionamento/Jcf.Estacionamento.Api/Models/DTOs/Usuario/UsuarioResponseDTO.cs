namespace Jcf.Estacionamento.Api.Models.DTOs.Usuario
{
    public class UsuarioResponseDTO
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public UsuarioResponseDTO()
        {
            Nome = string.Empty;
            Email = string.Empty;
        }
    }
}
