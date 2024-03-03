using System.Text;

namespace Hoa7mlishe.Edu.Crypto.Algorithms
{
    public static class Tritemius
    {
        private const string Alphabet = "абвгдежзиклмнопрстуфхцчшщыьэюя";

        public static string Calculate(string key, string message)
        {
            message = message.FormatMe();

            var result = new StringBuilder();

            foreach (var ch in message)
            {
                int index = Alphabet.IndexOf(ch);
                char keyChar = key[index % key.Length];
                char newChar = Alphabet[(Alphabet.IndexOf(keyChar) + Alphabet.IndexOf(ch)) % Alphabet.Length];

                result.Append(newChar);
            }

            return result.ToString();
        }
    }
}
