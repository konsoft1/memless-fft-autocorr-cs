using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;


namespace FourierTransformOfVectorAutocorrelation
{
    public sealed class TimeSeriesAnalysisFFT
    {
        private readonly List<Vector3> _source;
        private int _count;

        public TimeSeriesAnalysisFFT(IEnumerable<Vector3> vectorList)
        {
            _source = vectorList.ToList();
            _count = _source.Count;
        }

        public double AutoCorr(int lag)
        {
            int n = _count;

            if (n == 0 || lag >= n || lag < 0)
                throw new ArgumentException($"Lag must be between 0 and {n - 1}, and the list cannot be empty.");

            var autocorr3d = new double[n];

            for (int k = 0; k < 3; k++)
            {
                var realValues = _source.Select(v => k == 0 ? v.X : k == 1 ? v.Y : v.Z).ToArray();

                var nPadding = (int)Math.Pow(2, Math.Ceiling(Math.Log(2 * realValues.Length - 1, 2)));

                // Zero-padding to twice the length
                var complexValues = new Complex[n + 2 * nPadding];
                for (int i = nPadding; i < n + nPadding; i++)
                {
                    complexValues[i] = new Complex(realValues[i - nPadding], 0);
                }

                // Forward FFT
                Fourier.Forward(complexValues, FourierOptions.Matlab);

                // Compute power spectrum (magnitude squared)
                for (int i = 0; i < n + 2 * nPadding; i++)
                {
                    complexValues[i] *= Complex.Conjugate(complexValues[i]);
                }

                // Inverse FFT to get the autocorrelation
                Fourier.Inverse(complexValues, FourierOptions.Matlab);

                complexValues = complexValues.Take(n).ToArray();

                // Extract the real part and normalize by number of samples
                var j = n;
                var autocorr = complexValues.Select(c => {
                    var val = c.Real / j;
                    autocorr3d[n - (j--)] += val;
                    return val;
                    }).ToArray();
            }

            // Return the normalized value for the given lag
            /*double variance = realValues.Sum(x => x * x) / n;

            if (variance == 0)
                return 0;*/

            return autocorr3d[lag];
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
