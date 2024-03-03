using System.Text;

namespace Hoa7mlishe.Edu.Crypto.Algorithms
{
    public static class Vigener
    {
        private const string Alphabet = "абвгдежзийклмнопрстуфхцчшщыьэюя";

        public static string Encrypt(string message, string key)
        {
            var result = new StringBuilder();
            
            message = message.FormatMe();
            var keyIndexes = KeyIndexes(key);

            for (int i = 0; i < message.Length; ++i)
            {
                int keyIndex = keyIndexes[i % keyIndexes.Length];
                int alphIndex = (keyIndex + Alphabet.IndexOf(message[i])) % Alphabet.Length;

                result.Append(Alphabet[alphIndex]);
            }

            return result.ToString();
        }

        private static int[] KeyIndexes(string key)
        {
            var result = new int[key.Length];

            for (int i = 0; i < key.Length; ++i)
            {
                result[i] = Alphabet.IndexOf(key[i]);
            }

            return result;
        }
    }
}
