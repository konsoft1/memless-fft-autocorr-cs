using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourierTransformOfVectorAutocorrelation
{
    public class PairReturn<T>
    {
        public IEnumerable<T> Values { get; }
        public IEnumerable<T> Results { get; }

        public PairReturn(IEnumerable<T> values, IEnumerable<T> results)
        {
            Values = values;
            Results = results;
        }
    }
}
