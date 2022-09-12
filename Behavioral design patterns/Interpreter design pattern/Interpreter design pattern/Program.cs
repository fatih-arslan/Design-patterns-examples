using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter_design_pattern
{
    // Turning strings into OOP based structures in a complicated process
    // Some examples are programming language compilers, interpreters and IDEs
    // An interpreter is a component that processes structured text data. Does so by turning it into seperate lexical tokens (lexing)
    // and then interpreting sequences of said tokens (parsing)
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "(13+4)-(12+1)";
            var Tokens = Lex(input);
            Console.WriteLine(String.Join("\t",Tokens));
            var parsed = Parse(Tokens);
            Console.WriteLine($"{input} = {parsed.Value}");
            Console.ReadLine();
        }
        // Lexing
        static List<Token> Lex(string input)
        {
            var result = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                switch(input[i])
                {
                    case '+':
                        result.Add(new Token(Token.Type.Plus, "+"));
                        break;
                    case '-':
                        result.Add(new Token(Token.Type.Minus, "-"));
                        break;
                    case '(':
                        result.Add(new Token(Token.Type.Lparen, "("));
                        break;
                    case ')':
                        result.Add(new Token(Token.Type.Rparen, ")"));
                        break;
                    default: // Integer case
                        var sb = new StringBuilder(input[i].ToString());
                        for(int j = i + 1; j < input.Length; ++j)
                        {
                            if(char.IsDigit(input[j])) // We keep going until the number ends
                            {
                                sb.Append(input[j]);
                                ++i;
                            } 
                            else 
                            {
                                result.Add(new Token(Token.Type.Integer, sb.ToString())); // Adding the number to the list
                                break;
                            }
                        }
                        break;
                }
            }
            return result;
        }
        // Parsing
        static IElement Parse(IReadOnlyList<Token> tokens)
        {
            var result = new BinaryOperation();
            bool haveLHS = false;
            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];
                switch (token.MyType)
                {
                    case Token.Type.Integer:
                        var integer = new Integer(int.Parse(token.Text));
                        if(!haveLHS)
                        {
                            result.Left = integer;
                            haveLHS = true;
                        }
                        else
                        {
                            result.Right = integer;
                        }
                        break;
                    case Token.Type.Plus:
                        result.MyType = BinaryOperation.Type.Addition;
                        break;
                    case Token.Type.Minus:
                        result.MyType = BinaryOperation.Type.Subtraction;
                        break;
                    case Token.Type.Lparen:
                        int j = i;
                        for(; j < tokens.Count; ++j)                        
                            if (tokens[j].MyType == Token.Type.Rparen)                            
                                break;
                        var subexpression = tokens.Skip(i + 1).Take(j - i - 1).ToList();
                        var element = Parse(subexpression);
                        if (!haveLHS)
                        {
                            result.Left = element;
                            haveLHS = true;
                        }
                        else
                        {
                            result.Right = element;
                        }
                        i = j;
                        break;
                    default: // there is no Rparen case beacuse we find the right paranthesises in the left parathesis case anyway
                        throw new ArgumentException();
                }
            }
            return result;
        }
    }
    public interface IElement
    {
        int Value { get; }
    }
    public class Integer : IElement
    {
        public Integer(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
    public class BinaryOperation : IElement
    {
        public enum Type
        {
            Addition, Subtraction
        }
        public Type MyType;
        public IElement Left, Right; //The left and right-hand side of the operation
        public int Value 
        { get
            {
                switch(MyType)
                {
                    case Type.Addition:
                        return Left.Value + Right.Value;                        
                    case Type.Subtraction:
                        return Left.Value - Right.Value;
                    default:
                        throw new NotImplementedException();
                }
            }
        
        }
    }
    public class Token
    {
        public enum Type
        {
            Integer, Plus, Minus, Lparen, Rparen
        }
        public Type MyType;
        public string Text;
        public Token(Type myType, string text)
        {
            MyType = myType;
            Text = text;
        }
        public override string ToString()
        {
            return $"'{Text}'";
        }
    }
}
