using System.Runtime.CompilerServices;

namespace Hoa7mlishe.Edu.Crypto.Algorithms
{
    public static class AnaliticalConversions
    {
        private const string Alphabet = "абвгдеёжзийклмнопрстуфхцчшщьыъэюя";

        public static string Encode(string word, int[][] matrix)
        {
            if (word?.Length % 3 != 0)
            {
                throw new ArgumentException("Word's length must be divisible by 3");
            }


            List<int[]> vectors = [];

            for (int i = 0; i < word.Length; i += 3)
            {
                vectors.Add([
                        Alphabet.IndexOf(word[i]),
                    Alphabet.IndexOf(word[i + 1]),
                    Alphabet.IndexOf(word[i])
                ]);
            }

            List<int> resIndexes = [];

            foreach (var vector in vectors)
                for (int matrixRow = 0; matrixRow < 3; matrixRow++)
                {
                    int index = 0;

                    for (int matrixColumn = 0; matrixColumn < 3; matrixColumn++)
                        index += matrix[matrixRow][matrixColumn] * vector[matrixColumn];

                    resIndexes.Add(index % 32);
                }
                        

            return new string([.. resIndexes.Select(x => Alphabet[x])]);
        }
    }
}
