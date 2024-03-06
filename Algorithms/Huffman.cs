using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Collections;
using System.Linq;
using System.Text;

namespace Hoa7mlishe.Edu.Crypto.Algorithms
{
    public class Node
    {
        public char Symbol { get; set; }
        public int Frequency { get; set; }
        public Node Right { get; set; }
        public Node Left { get; set; }

        public List<bool> Traverse(char symbol, List<bool> data)
        {
            if (Right == null && Left == null)
            {
                return symbol.Equals(this.Symbol) ? data : null;
            }
            else
            {
                List<bool> left = null;
                List<bool> right = null;

                if (Left != null)
                {
                    List<bool> leftPath = [.. data, false];
                    left = Left.Traverse(symbol, leftPath);
                }

                if (Right != null)
                {
                    List<bool> rightPath = [.. data, true];
                    right = Right.Traverse(symbol, rightPath);
                }

                return left != null ? left : right;
            }
        }

        public bool IsLeaf() => (Left == null && Right == null);
    }

    public class HuffmanTree
    {
        private readonly List<Node> nodes = [];
        public Node Root { get; set; }
        public Dictionary<char, int> Frequencies = [];

        public HuffmanTree(string message)
        {
            Build(message);
        }

        private void Build(string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                Frequencies.TryAdd(source[i], 0);

                Frequencies[source[i]]++;
            }

            foreach (var symbol in Frequencies)
            {
                nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

                if (orderedNodes.Count >= 2)
                {
                    List<Node> taken = orderedNodes.Take(2).ToList<Node>();

                    Node parent = new Node()
                    {
                        Symbol = '*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();
            }
        }

        public BitArray Encode(string message)
        {
            List<bool> encodedSource = new List<bool>();

            for (int i = 0; i < message.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.Traverse(message[i], new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
        }

        public string EncodeNeatly(string message)
        {
            var array = Encode(message);
            var result = new StringBuilder();
            foreach (bool bit in array)
            {
                result.Append(bit ? "1" : "0");
            }

            return result.ToString();
        }

        public string Decode(BitArray bits)
        {
            Node current = this.Root;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (current.IsLeaf())
                {
                    decoded += current.Symbol;
                    current = this.Root;
                }
            }

            return decoded;
        }
    }
}