using Jcf.Estacionamento.Api.Utils;

namespace Jcf.Estacionamento.Test.Unidade
{
    public class PlacaUtilTestes
    {
        [Fact]
        public void GerarPlaca_DeveRetornarPlacaValida()
        {
            string placa = PlacaUtil.Gerar();
            Assert.True(EhPlacaValida(placa));
        }

        private bool EhPlacaValida(string placa)
        {           
            if (placa.Length != 7)
                return false;
                      
            for (int i = 0; i < 3; i++)
            {
                if (!char.IsLetter(placa[i]))
                    return false;
            }
                       
            for (int i = 3; i < 7; i++)
            {
                if (!char.IsDigit(placa[i]))
                    return false;
            }

            return true;
        }
    }
}
