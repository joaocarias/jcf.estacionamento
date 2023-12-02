using System.Text;

namespace Jcf.Estacionamento.Api.Utils
{
    public static class PlacaUtil
    {
        private static Random random = new Random();

        public static string Gerar()
        {
            string letras = GerarLetras();
            string digitos = GerarDigitos();

            return letras + digitos;
        }

        private static string GerarLetras()
        {
            const string alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder letras = new StringBuilder();

            for (int i = 0; i < 3; i++)
            {
                int indice = random.Next(alfabeto.Length);
                letras.Append(alfabeto[indice]);
            }

            return letras.ToString();
        }

        private static string GerarDigitos()
        {
            StringBuilder digitos = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                digitos.Append(random.Next(10).ToString());
            }

            return digitos.ToString();
        }
    }
}
