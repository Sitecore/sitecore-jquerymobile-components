using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Text;

namespace SitecoreMobile.Test
{
    [TestClass]
    public class ScanServiceTest1
    {
        System.Net.Http.HttpClient client;
        System.Net.Http.HttpClientHandler handler;

        public ScanServiceTest1()
        {
            handler = new System.Net.Http.HttpClientHandler();
            handler.CookieContainer = new CookieContainer();
            
            client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2054.3 Safari/537.36");
            client.DefaultRequestHeaders.Add("Content-Type", "application/xml");
            client.DefaultRequestHeaders.Add("Accept", "application/xml");


        }

        ~ScanServiceTest1()
        {
            client.Dispose();
            handler.Dispose();

        }

        [TestMethod]
        public void TestGetSampleBatch()
        {
            var url01 = "http://symna.sitecore.net:8081/-/expapi/samplebatch";
            
            var result01 = client.GetAsync(url01).Result;

            Assert.AreEqual(result01.StatusCode, HttpStatusCode.OK);


        }


        [TestMethod]
        public void TestScanBatch02()
        {
            //  [post] scan batch 1 item 1 2 3

            //  [get] scan batch 1 - result item 1 2 3

            //  [get] get scan - result batch 1 item 1
            //  [get] get scan - result batch 1 item 2
            //  [get] get scan - result batch 1 item 3


            // add batch 1 item 1 2 3
            // add batch 2 item 4 5

            //  [get] get scan - result batch 1 item 1

            // add batch 3 item 6

            //  [get] get scan - result batch 1 item 2
            //  [get] get scan - result batch 1 item 3
            //  [get] get scan - result batch 2 item 4
            //  [get] get scan - result batch 2 item 5
            //  [get] get scan - result batch 3 item 6

        }

      

        
        private object ExtractResponse(string p)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(p);
        }

        

    }
}
