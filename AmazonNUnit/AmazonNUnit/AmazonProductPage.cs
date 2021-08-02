using System;
using System.Threading;
using OpenQA.Selenium;

namespace AmazonNUnit
{
    public class AmazonProductPage
    {
        private IWebDriver driver;

        public AmazonProductPage(IWebDriver driver)
        {
            this.driver = driver;
            Thread.Sleep(3000);
        }
    }
}
