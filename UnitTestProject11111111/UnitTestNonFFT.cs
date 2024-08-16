
using System;
using System.Collections.Generic;
using FourierTransformOfVectorAutocorrelation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject11111111
{
    [TestClass]
    public class TimeSeriesAnalysisVec3AutoCorrelationMemoryLess____unit_test
    {

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        public void GetAutoCorrelationTestMethod()
        {
            IList<Vector3> vect3List = new List<Vector3>()
            {
                new Vector3(1,1,1),
                new Vector3(2,2,2),
                new Vector3(3,3,3),
                new Vector3(4,4,4),
                new Vector3(5,5,5)
            };

            var timeSeriesAnalysis = new TimeSeriesAnalysis(vect3List);

            double c0 = timeSeriesAnalysis.AutoCorr(0);//33
            double c1 = timeSeriesAnalysis.AutoCorr(1);//30
            double c2 = timeSeriesAnalysis.AutoCorr(2);//26
            double c3 = timeSeriesAnalysis.AutoCorr(3);//21
            double c4 = timeSeriesAnalysis.AutoCorr(4);//15

            Assert.AreEqual(33, c0);
            Assert.AreEqual(30, c1);
            Assert.AreEqual(26, c2);
            Assert.AreEqual(21, c3);
            Assert.AreEqual(15, c4);
        }

        [TestMethod]
        public void GetAutoCorrelationPointsTestMethod()
        {
            List<Vector3> vect3List = new List<Vector3>()
            {
                new Vector3(1,1,1),
                new Vector3(2,2,2),
                new Vector3(3,3,3),
                new Vector3(4,4,4),
                new Vector3(5,5,5)
            };

            var timeSeriesAnalysis = new TimeSeriesAnalysis(vect3List);

            var autocorr = timeSeriesAnalysis.GetAutoCorrelationPoints(10);

            IList<double> tausList = new List<double>(autocorr.Values);
            IList<double> autocorrList = new List<double>(autocorr.Results);

            Assert.AreEqual(5, tausList.Count);
            Assert.AreEqual(5, autocorrList.Count);

            Assert.AreEqual(1.0, autocorrList[0]);
            Assert.AreEqual(0.90909090909090906, autocorrList[1]);
            Assert.AreEqual(0.78787878787878785, autocorrList[2]);
            Assert.AreEqual(0.63636363636363635, autocorrList[3]);
            Assert.AreEqual(0.45454545454545453, autocorrList[4]);
        }
    }
}
