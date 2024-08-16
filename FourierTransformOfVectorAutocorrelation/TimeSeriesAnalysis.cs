using System;
using System.Collections.Generic;
using System.Linq;

namespace FourierTransformOfVectorAutocorrelation
{
    public sealed class TimeSeriesAnalysis
    {
        private readonly IEnumerable<Vector3> _source;
        private int _count;

        public TimeSeriesAnalysis(IEnumerable<Vector3> vectorList)
        {
            _source = vectorList;
            _count = vectorList.Count();
        }

        public double AutoCorr(int lag)
        {
            int n = _count;

            if (n == 0 || lag >= n || lag < 0)
                throw new ArgumentException($"Lag must be between 0 and {n - 1}, and the list cannot be empty.");

            double sum = 0;

            int targetIndex = n - lag;

            for (int i = 0; i < targetIndex; i++)
            {
                var vecA = _source.Skip(i).Take(1).First();

                var vecB = _source.Skip(i + lag).Take(1).First();

                sum += vecA.Dot(vecB);
            }

            return sum / (n - lag);
        }

        public IEnumerable<double> GetCResults(int maxLag, double c0)
        {
            for (int lag = 0; lag <= maxLag; lag++)
            {
                double cValue = 0;

                try
                {
                    cValue = AutoCorr(lag);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    yield break; // Exit the loop if an invalid lag is encountered
                }

                yield return cValue / c0; // Normalization is done here
            }
        }

        public PairReturn<double> GetAutoCorrelationPoints(int maxLag)
        {
            int vectorCount = _count;

            double c0 = AutoCorr(0); // This is the normalization factor

            Console.WriteLine($"Normalization factor: {c0}");

            int validMaxLag = Math.Min(maxLag, _count - 1); // Ensure maxLag does not exceed n-1

            var values = Enumerable.Range(0, validMaxLag + 1).Select(lag => (double)lag);

            return new PairReturn<double>(values, GetCResults(maxLag, c0));
        }

    }
}

