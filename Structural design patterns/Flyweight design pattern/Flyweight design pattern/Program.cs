using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flyweight_design_pattern
{
    // Flyweight desing pattern is used for space optimization
    // A space optimization technique that lets us use less memory by storing externally the data associated with similar objects
    internal class Program
    {
        static void Main(string[] args)
        {            
            var bft = new BetterFormattedText("This is a brave new world");
            bft.GetRange(10, 15).Capitalize = true;
            Console.WriteLine(bft);
            Console.ReadLine();
        }
    }
    public class BetterFormattedText //Better version of a FormattedText class 
    {
        private string plainText;
        private List<TextRange> formatting = new List<TextRange>();
        public BetterFormattedText(string plainText)
        {
            this.plainText = plainText;
        }
        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange { Start = start, End = end };
            formatting.Add(range);
            return range;
        }
        public class TextRange
        {
            public int Start, End;
            public bool Capitalize, Bold, Italic;
            public bool Covers(int position)
            {
                return position >= Start && position < End;
            }
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            for(var i = 0; i < plainText.Length; i++)
            {
                var c = plainText[i];
                foreach(var range in formatting)
                {
                    if(range.Covers(i) && range.Capitalize)
                    {
                        c = char.ToUpper(c);
                    }
                }
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
