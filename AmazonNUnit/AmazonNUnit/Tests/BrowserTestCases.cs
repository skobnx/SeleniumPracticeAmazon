using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;


namespace AmazonNUnit
{

    public class UnitTestsWithTestCases
    {
        IWebDriver driver;
        public String browser;

        public IWebDriver getWebDriver(string browser)
        {
            switch (browser)
            {
                case ("chrome"):
                    return new ChromeDriver();
                case ("safari"):
                    return new SafariDriver();
                case ("firefox"):
                    return new FirefoxDriver();
                default:
                    Console.WriteLine("Invalid Browser");
                    return null;
            }
        }

        [Test]
        [TestCase("safari")]
        [TestCase("chrome")]
        public void TestAscending(string browser)
        {
            this.driver = this.getWebDriver(browser);
            driver.Navigate().GoToUrl(@"https://www.amazon.com");
            AmazonHomePage homepage = new AmazonHomePage(driver);
            AmazonSearchPage searchPage = homepage.searchFor("backpacks");
            searchPage.sortPrices(0);
            List<double> prices = searchPage.get_list_of_prices();
            List<double> sorted_prices = new List<double>(prices);
            sorted_prices.Sort();

            Assert.AreEqual(prices, sorted_prices);

        }

        [Test]
        [TestCase("safari")]
        [TestCase("chrome")]
        public void TestDecending(string browser)
        {
            this.driver = this.getWebDriver(browser);
            driver.Navigate().GoToUrl(@"https://www.amazon.com");
            AmazonHomePage homepage = new AmazonHomePage(driver);
            AmazonSearchPage searchPage = homepage.searchFor("backpacks");
            searchPage.sortPrices(1);
            List<double> prices = searchPage.get_list_of_prices();
            List<double> sorted_prices = new List<double>(prices);
            sorted_prices.Sort();
            sorted_prices.Reverse();

            Assert.AreEqual(prices, sorted_prices);
        }

        [Test]
        [TestCase("safari")]
        [TestCase("chrome")]
        public void TestCartPage(string browser)
        {
            this.driver = this.getWebDriver(browser);
            driver.Navigate().GoToUrl(@"https://www.amazon.com");
            AmazonHomePage homePage = new AmazonHomePage(driver);
            AmazonShoppingCartPage cartPage = homePage.goToCart();
            string page_title = driver.Title;
            Assert.AreEqual(page_title, "Amazon.com Shopping Cart");

        }

        [Test]
        [TestCase("safari")]
        [TestCase("chrome")]
        public void TestProductPage(string browser)
        {
            this.driver = this.getWebDriver(browser);
            driver.Navigate().GoToUrl(@"https://www.amazon.com");
            AmazonHomePage homepage = new AmazonHomePage(driver);
            AmazonSearchPage searchPage = homepage.searchFor("backpacks");
            searchPage.sortPrices(1);
            AmazonProductPage productPage = searchPage.GoToProductPage(1);
            string page_title = driver.Title;
            Assert.AreEqual(page_title, "Amazon.com: Gucci Unisex Beige/Blue Bloom GG Coated Canvas Small Backpack With Box 427042 8493: Clothing");

        }

        // 1) Searches for an item
        // 2) sorts the items by price
        // 3) clicks on cheapest item
        // 4) updates the qty of the item
        // 5) adds it to the cart
        // 6) goes to the cart page
        // 7) checks that the cart has the correct number of items in it
        [Test]
        [TestCase("safari")]
        [TestCase("chrome")]
        public void TestAddProductToCart(string browser)
        {
            this.driver = this.getWebDriver(browser);
            driver.Navigate().GoToUrl(@"https://www.amazon.com");
            AmazonHomePage homepage = new AmazonHomePage(driver);
            AmazonSearchPage searchPage = homepage.searchFor("backpacks");
            searchPage.sortPrices(0);
            AmazonProductPage productPage = searchPage.GoToProductPage(0);
            productPage.updateQty(2);
            AmazonAddedToCartPage addedToCartPage = productPage.addItemToCart();
            AmazonShoppingCartPage cartPage = addedToCartPage.goToCart();
            string subtotal_string = cartPage.number_of_items_in_cart();

            string[] substrings = subtotal_string.Split(' ');
            string number_part = substrings[1];
            number_part = number_part.Replace("(", "");

            Assert.AreEqual(number_part, "2");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}