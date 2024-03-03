using Hoa7mlishe.Edu.Crypto.Structures;

namespace Hoa7mlishe.Edu.Crypto.Algorithms
{
    public static class ShannonFano
    {
        public static string Calculate(string message)
        {
            var nodes = message.GroupBy(x => x)
                .Select(x => new ShannonFanoNode(x.Key, x.Count() / (double)message.Length))
                .OrderByDescending(x => x.Probability);

            var processedNodes = new ShannonFanoNodeCollection([.. nodes]);

            return processedNodes.ToString();
        }
    }
}
