
namespace PointSample
{
    public class Point(int x, int y)
    {
        private int _x = x;
        private int _y = y;

        public int GetX()
        {
            return _x;
        }

        public void SetX(int x)
        {
            _x = x;
        }

        public int GetY()
        {
            return _y;
        }

        public void SetY(int y)
        {
            _y = y;
        }
    }

    public class DistanceCalculator
    {
        public static double CalculateDistanceBetween2Points(Point point1, Point point2)
        {
            if (point1 == null)
            {
                throw new ArgumentNullException(nameof(point1), "Point 1 cannot be null");
            }

            if (point2 == null)
            {
                throw new ArgumentNullException(nameof(point2), "Point 2 cannot be null");
            }

            int deltaX = point1.GetX() - point2.GetX();
            int deltaY = point1.GetY() - point2.GetY();

            return Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Point point1 = new Point(1, 1);
            Point point2 = new Point(4, 5);

            double distance = DistanceCalculator.CalculateDistanceBetween2Points(point1, point2);

            Console.WriteLine($"Distance between point1 and point2: {distance}");
        }
    }
}


