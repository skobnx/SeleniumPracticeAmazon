using System;
using System.Threading;
using OpenQA.Selenium;

namespace AmazonNUnit
{
    public class AmazonShoppingCartPage
    {
        private IWebDriver driver;

        public AmazonShoppingCartPage(IWebDriver driver)
        {
            this.driver = driver;
            Thread.Sleep(3000);
        }

    }
}
