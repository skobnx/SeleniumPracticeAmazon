using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;

namespace AmazonNUnit
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new SafariDriver();
        }

        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl(@"https://www.amazon.com");
            driver.Manage().Window.Maximize();
            AmazonHomePage homepage = new AmazonHomePage(driver);
            AmazonSearchPage searchPage = homepage.searchFor("backpacks");
            //searchPage.printProductPrices();
            String page_title = driver.Title;
            //searchPage.printProductPrices();
            searchPage.sortPrices(0);
            List<double> prices = searchPage.get_list_of_prices();
            List<double> sorted_prices = new List<double>(prices);
            sorted_prices.Sort();
            driver.Quit();

            foreach (double price in prices)
            {
                Console.WriteLine(price);
            }

            Assert.AreEqual(page_title, "Amazon.com : backpacks");
            Assert.AreEqual(prices, sorted_prices);

        }

        [Test]
        public void Test2()
        {
            driver.Navigate().GoToUrl(@"https://www.amazon.com");
            driver.Manage().Window.Maximize();
            AmazonHomePage homepage = new AmazonHomePage(driver);
            AmazonSearchPage searchPage = homepage.searchFor("backpacks");
            searchPage.sortPrices(1);
            List<double> prices = searchPage.get_list_of_prices();
            List<double> sorted_prices = new List<double>(prices);
            sorted_prices.Sort();
            sorted_prices.Reverse();
            driver.Quit();

            Assert.AreEqual(prices, sorted_prices);
        }
    }
}
