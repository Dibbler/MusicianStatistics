using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicianStatisticsCore;

namespace MusicianStatistics.Test
{
    [TestClass]
    public class CalculatorTests
    {

        [TestMethod]
        public void Variance_ShouldCalculatePositiveValuesCorrectly()
        {
            double expected = 36.76;
            double[] testValues = { 1.0, 2.0, 5.5, 6.25, 18 };
            double actual = Calculator.Variance(testValues);
            Assert.AreEqual(expected, actual, 0.0005);
        }

        [TestMethod]
        public void Variance_ShouldCalculateNegativeValuesCorrectly()
        {
            double expected = 36.76;
            double[] testValues = { -1.0, -2.0, -5.5, -6.25, -18 };
            double actual = Calculator.Variance(testValues);
            Assert.AreEqual(expected, actual, 0.0005);
        }

        [TestMethod]
        public void Variance_ShouldCalculateZeroValuesCorrectly()
        {
            double expected = 0.0;
            double[] testValues = { 0, 0, 0, 0 };
            double actual = Calculator.Variance(testValues);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StandardDeviation_ShouldCalculatePositiveValuesCorrectly()
        {
            double expected = 6.0630025564896;
            double[] testValues = { 1.0, 2.0, 5.5, 6.25, 18 };
            double actual = Calculator.StandardDeviation(testValues);
            Assert.AreEqual(expected, actual, 0.0005);
        }

        [TestMethod]
        public void StandardDeviation_ShouldCalculateNegativeValuesCorrectly()
        {
            double expected = 6.0630025564896;
            double[] testValues = { -1.0, -2.0, -5.5, -6.25, -18 };
            double actual = Calculator.StandardDeviation(testValues);
            Assert.AreEqual(expected, actual, 0.0005);
        }

        [TestMethod]
        public void StandardDeviation_ShouldCalculateZeroValuesCorrectly()
        {
            double expected = 0.0;
            double[] testValues = { 0, 0, 0, 0 };
            double actual = Calculator.StandardDeviation(testValues);
            Assert.AreEqual(expected, actual);
        }
    }
}