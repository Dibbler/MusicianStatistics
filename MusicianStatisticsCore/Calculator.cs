using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicianStatisticsCore
{
    public static class Calculator
    {
        /// <summary>
        /// Calculate the standard deviation of an array of doubles
        /// </summary>
        /// <returns></returns>
        public static Double StandardDeviation(IEnumerable<double> numbers)
        {
            if (numbers.Count() == 0)
            {
                return 0.0;
            }

            double variance = Variance(numbers);

            //Return the standard deviation
            return Math.Sqrt(variance);
        }


        /// <summary>
        /// Calculate the variance of an array of doubles
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static double Variance(IEnumerable<double> numbers)
        {
            if (numbers.Count() == 0)
            {
                return 0;
            }

            double mean = numbers.Average();
            double deviationTotal = 0.0;
            double[] deviations = new double[numbers.Count()];

            //First calculate the deviation and square it for each number
            for (int i = 0; i < numbers.Count(); i++)
            {
                double deviation = (numbers.ElementAt(i) - mean);
                deviations[i] = deviation * deviation;
                deviationTotal += deviations[i];
            }

            //Calculate the variance as the mean of the deviations squared
            return deviationTotal / numbers.Count();
        }
    }
}
