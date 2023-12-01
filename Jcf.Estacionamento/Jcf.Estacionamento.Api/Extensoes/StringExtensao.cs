using System.Text.RegularExpressions;

namespace Jcf.Estacionamento.Api.Extensoes
{
    public static class StringExtensao
    {
        public static string SomenteNumeros(this string? str)
        {
            if (str == null) return string.Empty;
            return Regex.Replace(str, "[^0-9]", "");
        }

        public static string PrimeiraParte(this string str)
        {
            if (str == null) return string.Empty;
            string[] parts = str.Split(' ');
            return parts[0];
        }
    }
}
