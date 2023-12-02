using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jcf.Estacionamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : MeuController
    {
        public HomeController() { }

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult TestarConexao()
        {
            return Ok();
        }
    }
}
