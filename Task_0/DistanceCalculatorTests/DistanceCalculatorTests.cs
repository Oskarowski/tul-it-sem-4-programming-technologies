using PointSample;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PointSample.DistanceCalculatorTests
{

    [TestClass]
    public class DistanceCalculatorTests
    {
        [TestMethod]
        public void CalculateDistanceBetween2Points_SamePoint_ReturnsZero()
        {
            Point point1 = new Point(3, 4);
            Point point2 = new Point(3, 4);

            double distance = DistanceCalculator.CalculateDistanceBetween2Points(point1, point2);

            Assert.AreEqual(0, distance);
        }

        [TestMethod]
        public void CalculateDistanceBetween2Points_ValidPoints_ReturnsCorrectDistance()
        {
            Point point1 = new Point(1, 1);
            Point point2 = new Point(4, 5);

            double distance = DistanceCalculator.CalculateDistanceBetween2Points(point1, point2);

            Assert.AreEqual(5, distance);
        }

        [TestMethod]
        public void CalculateDistanceBetween2Points_NullPoint1_ThrowsArgumentNullException()
        {
            Point point1 = null;
            Point point2 = new Point(4, 5);

            Assert.ThrowsException<ArgumentNullException>(() => DistanceCalculator.CalculateDistanceBetween2Points(point1, point2));
        }

        [TestMethod]
        public void CalculateDistanceBetween2Points_NullPoint2_ThrowsArgumentNullException()
        {
            Point point1 = new Point(1, 1);
            Point point2 = null;

            Assert.ThrowsException<ArgumentNullException>(() => DistanceCalculator.CalculateDistanceBetween2Points(point1, point2));
        }
    }
}
