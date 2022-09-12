using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_design_pattern
{
    internal class Program
    {
        // System behaviour partially specified at runtime
        // Enables the exact behaviour of a system to be selected either at run-time (dynamic) or compile-time (static)
        // Summary:
        // Define an algorithm at a high level
        // Define the interface you expect each strategy to follow
        // Provide for either dynamic or static composition of strategy in the overall algorithm
        static void Main(string[] args)
        {
            var tp = new TextProcessor();
            tp.SetOutputFormat(OutputFormat.Markdown);
            tp.AppendList(new [] {"foo", "bar", "baz"});
            Console.WriteLine(tp);
            
            tp.Clear();
            tp.SetOutputFormat(OutputFormat.Html);
            tp.AppendList(new [] {"foo", "bar", "baz"});
            Console.WriteLine(tp);
            Console.ReadLine();
        }
    }
    public enum OutputFormat
    {
        Markdown, Html
    }
    public interface IListStrategy
    {
        void Start(StringBuilder sb);
        void End(StringBuilder sb);
        void AddListItem(StringBuilder sb, string item);
    }
    public class HtmlStrategy : IListStrategy
    {
        public void Start(StringBuilder sb)
        {
            sb.AppendLine("<ul>");            
        }        

        public void End(StringBuilder sb)
        {
            sb.AppendLine("</ul>");
        }
        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($"  <li>{item}</li>");
        }

    }
    public class MarkdownListStrategy : IListStrategy
    {
        public void Start(StringBuilder sb)
        {
            
        }
        public void End(StringBuilder sb)
        {
            
        }
        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($" * {item}");
        }                
    }
    public class TextProcessor
    {
        private StringBuilder sb = new StringBuilder();
        private IListStrategy listStrategy;
        public void SetOutputFormat(OutputFormat format)
        {
            switch(format)
            {
                case OutputFormat.Markdown:
                    listStrategy = new MarkdownListStrategy();
                    break;
                case OutputFormat.Html:
                    listStrategy = new HtmlStrategy();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        public void AppendList(IEnumerable<string> items)
        {
            listStrategy.Start(sb);
            foreach (string item in items)
            {
                listStrategy.AddListItem(sb, item);

            }
            listStrategy.End(sb);
        }
        public StringBuilder Clear()
        {
            return sb.Clear();
        }
        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
