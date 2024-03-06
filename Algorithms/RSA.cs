using System.Numerics;
using System.Text;

namespace Hoa7mlishe.Edu.Crypto.Algorithms
{
    
    public static class RSA
    {
        public class EncryptionResult
        {
            /// <summary>
            /// Зашифрованное сообщение
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// Открытый ключ
            /// </summary>
            public PublicKey PublicKey { get; set; }

            /// <summary>
            /// Закрытый ключ
            /// </summary>
            public PrivateKey PrivateKey { get; set; }
        }

        private const string Alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        public class PublicKey
        {
            public int с { get; set; }
            public int ri { get; set; }
        }

        public class PrivateKey
        {
            public int d { get; set; }
            public int ri { get; set; }
        }

        public static EncryptionResult Encrypt(string message, int p, int q)
        {
            if (!IsPrime(p) || !IsPrime(q))
            {
                throw new ArgumentException("Both numbers must be prime");
            }

            int ri = p * q;
            int fn = (p - 1) * (q - 1);
            int с = CalculateCoprime(fn);
            int d = FindMultiplicativeInverse(fn, с);

            var privateKey = new PrivateKey { d = d, ri = ri };
            var publicKey = new PublicKey { с = с, ri = ri };

            string encrypted = EncryptString(message.ToLower(), publicKey);

            return new EncryptionResult() { Message = encrypted, PublicKey = publicKey, PrivateKey = privateKey };
        }

        public static string Decrypt(string message, PrivateKey privateKey)
        {
            StringBuilder result = new();

            foreach (char c in message)
            {
                BigInteger index = BigInteger.Pow(Alphabet.IndexOf(c) + 1, privateKey.d);
                BigInteger decryptedIndex = (index % privateKey.ri) - 1;

                result.Append(Alphabet[(((int)decryptedIndex) + Alphabet.Length) % Alphabet.Length]);
            }

            return result.ToString();
        }

        private static string EncryptString(string message, PublicKey publicKey)
        {
            StringBuilder result = new();

            foreach (char c in message)
            {
                if (!Alphabet.Contains(c))
                {
                    continue;
                }

                int charIndex = Alphabet.IndexOf(c);
                BigInteger index = BigInteger.Pow(charIndex + 1, publicKey.с) % publicKey.ri; 

                result.Append(Alphabet[((int)index - 1 + Alphabet.Length) % Alphabet.Length]);
            }

            return result.ToString();
        }

        private static int FindMultiplicativeInverse(int phri, int coprime)
        {
            for (int i = 1; i < phri; i++)
            {
                if ((coprime * i) % phri == 1)
                {
                    return i;
                }
            }

            return 0;
        }

        private static int CalculateCoprime(int num)
        {
            for (int i = 2; i < num; i++)
            {
                if (GCD(i, num) == 1)
                {
                    return i;
                }
            }

            return 0;
        }

        private static int GCD(int num1, int num2)
        {
            return num2 == 0 ? num1 : GCD(num2, num1 % num2);
        }

        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
