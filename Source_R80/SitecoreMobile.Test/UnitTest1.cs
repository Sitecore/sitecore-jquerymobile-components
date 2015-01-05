using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Text;

namespace SitecoreMobile.Test
{
    [TestClass]
    public class UnitTest1
    {
        System.Net.Http.HttpClient client;
        System.Net.Http.HttpClientHandler handler;

        public UnitTest1()
        {
            handler = new System.Net.Http.HttpClientHandler();
            handler.CookieContainer = new CookieContainer();
            
            client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2054.3 Safari/537.36");

        }

        ~UnitTest1()
        {
            client.Dispose();
            handler.Dispose();

        }

        [TestMethod]
        public void TestMethod1()
        {
            var url01 = "http://sitecore75_03.localhost/";
            var url02 = "http://sitecore75_03.localhost/layouts/system/VisitorIdentificationCSS.aspx?" + DateTime.UtcNow.ToFileTimeUtc().ToString();

            var result01 = client.GetAsync(url01).Result;
            var result02 = client.GetAsync(url02).Result;

            Assert.AreEqual(result01.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(result02.StatusCode, HttpStatusCode.OK);


        }


        [TestMethod]
        public void TestMethod2()
        {
            TestMethod1();

            var url01 = "http://sitecore75_03.localhost/test/TestSessionDeviceAddCustomValue/sitecore/content/home/?echo=Test01";
            var result01 = client.GetAsync(url01).Result;
            
            Assert.AreEqual(result01.StatusCode, HttpStatusCode.OK);

            var resultExtract = ExtractResponse(result01.Content.ReadAsStringAsync().Result);



            var url02 = "http://sitecore75_03.localhost/test/TestSessionDeviceGetCustomValue/sitecore/content/home/";
            var result02 = client.GetAsync(url02).Result;

            Assert.AreEqual(result02.StatusCode, HttpStatusCode.OK);

            var resultExtract2 = ExtractResponse(result02.Content.ReadAsStringAsync().Result);

        }

        [TestMethod]
        public void TestMethod3()
        {
            TestMethod1();

            var url01 = "http://sitecore75_03.localhost/test/TestContactAddTestModelValue/sitecore/content/home/?echo=TestMethod03";
            var result01 = client.GetAsync(url01).Result;

            Assert.AreEqual(result01.StatusCode, HttpStatusCode.OK);

            var resultExtract = ExtractResponse(result01.Content.ReadAsStringAsync().Result);



            var url02 = "http://sitecore75_03.localhost/test/TestContactGetTestModelValue/sitecore/content/home/";
            var result02 = client.GetAsync(url02).Result;

            Assert.AreEqual(result02.StatusCode, HttpStatusCode.OK);

            var resultExtract2 = ExtractResponse(result02.Content.ReadAsStringAsync().Result);

        }

        
        private object ExtractResponse(string p)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(p);
        }

        

    }
}
