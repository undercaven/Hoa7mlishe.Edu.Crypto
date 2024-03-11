using Hoa7mlishe.Edu.Crypto.Structures;

namespace Hoa7mlishe.Edu.Crypto.Algorithms
{
    public static class ShannonFano
    {
        public static string[] Calculate(string message)
        {
            var processedNodes = ShannonFanoCalculator.CreateTree(message);

            List<string> result = [.. processedNodes.Select(x => $"{x.Key}: {x.Value}"), .. CalculateStatistics(processedNodes, message)];

            return [.. result];
        }

        private static List<string> CalculateStatistics(IDictionary<char, string> nodes, string message)
        {
            List<string> result = [];

            double entropy = CountEntropy(message);
            double avgBits = CountAverageBits(nodes);

            result.Add($"Entropy: {entropy}");
            result.Add($"Average Bits: {avgBits}");
            result.Add($"Redundancy: {1 - entropy / avgBits}");

            return result;
        }

        private static double CountEntropy(string message)
        {
            var uniqueSymbols = string.Concat(message.GroupBy(x => x).Select(x => x.Key.ToString()));

            double entropy = 0;
            foreach (var symbol in uniqueSymbols)
            {
                double probability = (double)message.Count(x => x == symbol) / message.Length;
                entropy -= Math.Log2(probability) * probability;
            }

            return entropy;
        }

        private static double CountAverageBits(IDictionary<char, string> symbols) 
            => (double)symbols.Sum(x => x.Value.Length) / symbols.Count;
    }
}
