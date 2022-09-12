using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter_design_pattern
{
    // Getting the interface you want from the interface you have
    // Implementing an adapter is easy
    // Determine the api you have and the api you need
    // Create a component which aggregates (has a reference to, ...) the adaptee
    // Intermediate representations can pile up: use caching and other optimizations
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach(VectorObject vo in vectorObjects)
            {
                foreach(Line line in vo)
                {
                    var adapter = new LineToPointAdapter(line);                    
                }
            }
        }
        public static void DrawPoint(Point p)
        {
            Console.WriteLine(".");
        }
        private static readonly List<VectorObject> vectorObjects = new List<VectorObject> { new VectorRectangle(1,1,10,10), new VectorRectangle(3,3,6,6)};
    }
    public class Point
    {
        public int X, Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class Line
    {
        public Point Start, End;
        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }
    public class VectorObject : ICollection<Line>
    {
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(Line item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Line item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Line[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Line> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Line item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    public class VectorRectangle : VectorObject
    {
        public VectorRectangle(int x, int y, int width, int height)
        {
            Add(new Line(new Point(x, y), new Point(x + width, y)));
            Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
            Add(new Line(new Point(x, y), new Point(x, y + height)));
            Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
        }
    }
    public class LineToPointAdapter : ICollection<Point>
    {
        public static int count;
        public LineToPointAdapter(Line line)
        {
            Console.WriteLine($"{++count}: Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}] (no caching)");
            int left = Math.Min(line.Start.X, line.End.X);
            int right = Math.Max(line.Start.X, line.End.X);
            int top = Math.Min(line.Start.Y, line.End.Y);
            int bottom = Math.Max(line.Start.Y, line.End.Y);
            int dx = right - left;
            int dy = line.End.Y - line.Start.Y;
            if (dx == 0)
            {
                for (int y = top; y <= bottom; ++y)
                {
                    Add(new Point(left, y));
                }
            }
            else if (dy == 0)
            {
                for (int x = left; x <= right; ++x)
                {
                    Add(new Point(x, top));
                }
            }
        }
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(Point item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Point item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Point[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Point> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Point item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
