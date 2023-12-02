using Jcf.Estacionamento.Api.Extensoes;
using Microsoft.IdentityModel.Tokens;

namespace Jcf.Estacionamento.Test.Unidade
{
    public class StringsExtensaoTestes
    {
        [Theory]
        [InlineData("(84) 99682-8298")]
        [InlineData("+55 (84) 99682-8298")]
        [InlineData("(84) 99682_8298")]
        [InlineData("056.845.451-84")]
        [InlineData("07/07/2023")]
        [InlineData("12 anos")]
        [InlineData("Joao 1827")]
        public void Only_Number_ReturnTrue(string str)
        {
            var numerosString = str.SomenteNumeros();
            _ = long.TryParse(numerosString, out long numero);
            Assert.True(numero > 0, $" {numero} é um número");
        }

        [Theory]
        [InlineData("antonia fransicao")]
        [InlineData("antonio maria")]
        [InlineData("")]
        public void Only_Number_ReturnFalse(string str)
        {
            var somenteNumero = str.SomenteNumeros();
            _ = long.TryParse(somenteNumero, out long numero);
            Assert.False(numero > 0, $" {numero} não é um número");
        }

        [Theory]
        [InlineData(null)]
        public void Only_Number_String_Null_ReturnTrue(string str)
        {
            Assert.True(str.SomenteNumeros().IsNullOrEmpty(), $"Não é um número, é um valor null ou vazio");
        }

        [Theory]
        [InlineData(null)]
        public void Only_Number_String_Null_ReturnNotNull(string str)
        {
            Assert.NotNull(str.SomenteNumeros());
        }

        [Theory]
        [InlineData("João Carias de França")]
        [InlineData("Messias Targino")]
        [InlineData("Ana Maria")]
        public void First_Part_More_Than_One_ReturnTrue(string str)
        {
            Assert.True(str.Length > str.PrimeiraParte().Length, $" {str.PrimeiraParte()} é menor que a string completa");
        }

        [Theory]
        [InlineData("João")]
        [InlineData("Messias")]
        [InlineData("Ana")]
        public void First_Part_Only_One_ReturnTrue(string str)
        {
            Assert.True(str.Length.Equals(str.PrimeiraParte().Length), $" {str.PrimeiraParte()} é igual a string completa");
        }
    }
}
