using System.Net;

namespace Jcf.Estacionamento.Api.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErroMensagens = new List<string>();
            Links = new List<string>();
        }

        public bool Sucesso { get; set; } = true;
        public Object? Resultado { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public List<string> ErroMensagens { get; set; }
        public List<string> Links { get; set; }

        public void Erro(List<string> erroMensagens, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Sucesso = false;
            StatusCode = statusCode;
            ErroMensagens = erroMensagens;
        }
    }
}
