namespace Jcf.Estacionamento.Api.Models.DTOs.Usuario
{
    public class LoginResponseDTO
    {
        public bool Autenticado { get; set; } = true;
        public UsuarioResponseDTO? Usuario { get; set; } 

        public string? Token { get; set; }

        public string? Mensagem { get; set; } = "Usuário Autenticado";
    }
}
