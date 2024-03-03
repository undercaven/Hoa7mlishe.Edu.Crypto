using System.Text;

namespace Hoa7mlishe.Edu.Crypto.Structures
{
    public class ShannonFanoNodeCollection
    {
        private List<ShannonFanoNode> _nodes;

        public ShannonFanoNodeCollection(List<ShannonFanoNode> nodes)
        {
            PopulateNodes(0, nodes.Count, nodes);
            _nodes = nodes;
        }

        private static void PopulateNodes(int start, int end, List<ShannonFanoNode> nodes)
        {
            if (start >= end - 1)
                return;

            double sum = nodes.GetRange(start, end - start - 1).Sum(x => x.Probability);
            double minDiff = 1;
            int midIndex = -1;

            double tempSum = 0;
            for (int i = start; i < end; i++)
            {
                tempSum += nodes[i].Probability;

                var tempDiff = Math.Abs(tempSum - sum / 2);

                if (tempDiff < minDiff)
                {
                    minDiff = tempDiff;
                    midIndex = i;
                }
                else break;
            }

            for (int i = start; i < end; ++i)
            {
                if (i <= midIndex)
                {
                    nodes[i].BitCode.Append('0');
                }
                else
                {
                    nodes[i].BitCode.Append('1');
                }
            }

            PopulateNodes(start, midIndex + 1, nodes);
            PopulateNodes(midIndex + 1, end, nodes);
        }

        public override string ToString()
        {
            StringBuilder result = new();

            foreach (var node in _nodes)
            {
                result.AppendLine($"Symbol: '{node.Symbol}' Probability: {node.Probability} BitCode: {node.BitCode}\n");
            }

            return result.ToString();
        }
    }

    public class ShannonFanoNode
    {
        public ShannonFanoNode(char symbol, double probability)
        {
            Symbol = symbol;
            Probability = probability;
        }

        public char Symbol { get; set; }
        public double Probability { get; set; }
        public StringBuilder BitCode { get; set; } = new();
        public ShannonFanoNode Left { get; set; } = null;
        public ShannonFanoNode Right { get; set; } = null;
    }
}
