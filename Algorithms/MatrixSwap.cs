using System.Text;

namespace Hoa7mlishe.Edu.Crypto.Algorithms
{
    public static class MatrixSwap
    {
        private static string[] StringifyMatrix(char[][] matrix)
        {
            string[] result = new string[matrix.Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                StringBuilder sb = new();
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    sb.Append(matrix[i][j].ToString().PadRight(3, ' '));
                }

                result[i] = sb.ToString().Trim();
            }

            return result;
        }

        public class SwapResult
        {
            public string[] OriginalMatrix { get; set; }
            public string[] NewMatrix { get; set; }
            public string ResultMessage { get; set; }
        }

        public static SwapResult Decrypt1()
        {
            char[][] matrix = [
                ['Ж', 'В', 'Н', 'О', 'А'],
                ['Н', '_', 'А', 'Т', 'З'],
                ['О', 'Ь', 'С', 'Н', '_'],
                ['Ы', 'О', '_', 'Ф', 'В'],
                ['И', 'И', 'К', 'И', 'З'],
            ];

            char[][] newMatrix = SwapColumns(matrix, [1, 4, 0, 2, 3]);

            StringBuilder sb = new();
            for (int i = 0; i < newMatrix.Length; i++)
            {
                for (int j = 0; j < newMatrix[i].Length; j++)
                {
                    sb.Append(newMatrix[i][j]);
                }
            }

            return new SwapResult { NewMatrix = StringifyMatrix(newMatrix), OriginalMatrix = StringifyMatrix(matrix), ResultMessage = sb.ToString() };
        }

        private static char[][] SwapColumns(char[][] matrix, int[] columnOrder)
        {
            var result = new char[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++)
            {
                result[i] = new char[matrix[i].Length];
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    result[i][j] = matrix[i][columnOrder[j]];
                }
            }

            return result;
        }

        private static char[][] SwapRows(char[][] matrix, int[] rowOrder)
        {
            var result = new char[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++)
            {
                result[i] = new char[matrix[i].Length];
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    result[i][j] = matrix[rowOrder[i]][j];
                }
            }

            return result;
        }

        public static SwapResult Decrypt2()
        {
            char[][] matrix = [
                ['_', 'Е', 'А', 'Л', 'Я'],
                ['Р', 'А', 'Н', 'В', 'Я'],
                ['А', 'Ч', 'Д', 'А', '_'],
                ['Е', 'Р', 'П', 'Е', 'С'],
                ['А', 'Н', 'В', '_', 'Ч'],
            ];

            var newMatrix = SwapColumns(matrix, [2, 3, 1, 0, 4]);
            newMatrix = SwapRows(newMatrix, [3, 2, 4, 0, 1]);

            StringBuilder sb = new();
            for (int i = 0; i < newMatrix.Length; i++)
            {
                for (int j = 0; j < newMatrix[i].Length; j++)
                {
                    sb.Append(newMatrix[i][j]);
                }
            }

            return new SwapResult
            {
                NewMatrix = StringifyMatrix(newMatrix),
                OriginalMatrix = StringifyMatrix(matrix),
                ResultMessage = sb.ToString()
            };
        }
    }
}
