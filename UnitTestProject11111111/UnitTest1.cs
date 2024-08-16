
using System;
using System.Collections.Generic;
using FourierTransformOfVectorAutocorrelation;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestProject11111111
{
    [TestClass]
    public class TimeSeriesAnalysisFFT____unit_test
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

            var timeSeriesAnalysis = new TimeSeriesAnalysisFFT(vect3List);

            double c0 = timeSeriesAnalysis.AutoCorr(0);//33
            double c1 = timeSeriesAnalysis.AutoCorr(1);//30
            double c2 = timeSeriesAnalysis.AutoCorr(2);//26
            double c3 = timeSeriesAnalysis.AutoCorr(3);//21
            double c4 = timeSeriesAnalysis.AutoCorr(4);//15

            Assert.AreEqual(33, Math.Round(c0, 10));
            Assert.AreEqual(30, Math.Round(c1, 10));
            Assert.AreEqual(26, Math.Round(c2, 10));
            Assert.AreEqual(21, Math.Round(c3, 10));
            Assert.AreEqual(15, Math.Round(c4, 10));
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

            var timeSeriesAnalysis = new TimeSeriesAnalysisFFT(vect3List);

            var autocorr = timeSeriesAnalysis.GetAutoCorrelationPoints(10);

            IList<double> tausList = new List<double>(autocorr.Values);
            IList<double> autocorrList = new List<double>(autocorr.Results);

            Assert.AreEqual(5, tausList.Count);
            Assert.AreEqual(5, autocorrList.Count);

            //Assert.AreEqual(Math.Round(1.0, 10), Math.Round(autocorrList[0], 10));
            //Assert.AreEqual(Math.Round(0.90909090909090906, 10), Math.Round(autocorrList[1], 10));
            //Assert.AreEqual(Math.Round(0.78787878787878785, 10), Math.Round(autocorrList[2], 10));
            //Assert.AreEqual(Math.Round(0.63636363636363635, 10), Math.Round(autocorrList[3], 10));
            //Assert.AreEqual(Math.Round(0.45454545454545453, 10), Math.Round(autocorrList[4], 10));
        }
    }
}