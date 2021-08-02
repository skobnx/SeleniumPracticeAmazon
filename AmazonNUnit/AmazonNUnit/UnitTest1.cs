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
        public void TestAscending()
        {
            driver.Navigate().GoToUrl(@"https://www.amazon.com");
            driver.Manage().Window.Maximize();
            AmazonHomePage homepage = new AmazonHomePage(driver);
            AmazonSearchPage searchPage = homepage.searchFor("backpacks");
            searchPage.sortPrices(0);
            List<double> prices = searchPage.get_list_of_prices();
            List<double> sorted_prices = new List<double>(prices);
            sorted_prices.Sort();
            driver.Quit();

            Assert.AreEqual(prices, sorted_prices);

        }

        [Test]
        public void TestDecending()
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

            //foreach (double price in prices)
            //{
            //    Console.WriteLine(price);
            //}

            Assert.AreEqual(prices, sorted_prices);
        }

        [Test]
        public void TestCartPage()
        {
            driver.Navigate().GoToUrl(@"https://www.amazon.com");
            driver.Manage().Window.Maximize();
            AmazonHomePage homePage = new AmazonHomePage(driver);
            AmazonShoppingCartPage cartPage = homePage.goToCart();
            string page_title = driver.Title;
            driver.Quit();
            Assert.AreEqual(page_title, "Amazon.com Shopping Cart");

        }

        [Test]
        public void TestProductPage()
        {
            driver.Navigate().GoToUrl(@"https://www.amazon.com");
            driver.Manage().Window.Maximize();
            AmazonHomePage homepage = new AmazonHomePage(driver);
            AmazonSearchPage searchPage = homepage.searchFor("backpacks");
            searchPage.sortPrices(1);
            AmazonProductPage productPage = searchPage.GoToProductPage(0);
            string page_title = driver.Title;
            Assert.AreEqual(page_title, "Amazon.com: Gucci Unisex Beige/Blue Bloom GG Coated Canvas Small Backpack With Box 427042 8493: Clothing");

        }
    }
}
