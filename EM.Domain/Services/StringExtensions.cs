using System.Globalization;
namespace EM.Domain.Services
{
    public static class StringExtensions
    {
        public static string ToPadraoFormal(this string texto)
        {

            if (string.IsNullOrEmpty(texto))
                return string.Empty;

            texto = texto.Trim();

            char[] letras = texto.ToCharArray();

            letras[0] = char.ToUpper(letras[0]);

            for (int i = 0; i < letras.Length; i++)
            {

                if (letras[i] == ' ')
                {
                    letras[i+1] = char.ToUpper(letras[i+1]);
                }
            }
            return new string(letras);
        }
    }
}
