using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;


namespace AmazonNUnit
{
    [TestFixture("safari")]
    [TestFixture("firefox")]
    [TestFixture("chrome")]
    public class AmazonTests
    {
        private IWebDriver driver;
        private string browser;

        public AmazonTests(string browser)
        {
            this.browser = browser;
        }
        

        [SetUp]
        public void Setup()
        {
            switch (browser)
            {
                case ("chrome"):
                    this.driver = new ChromeDriver();
                    break;
                case ("safari"):
                    this.driver = new SafariDriver();
                    break;
                case ("firefox"):
                    this.driver = new FirefoxDriver();
                    break;
                default:
                    Console.WriteLine("Invalid Browser");
                    break;
            }
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void TestAscending()
        {
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
        public void TestDecending()
        {
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
        public void TestCartPage()
        {
            driver.Navigate().GoToUrl(@"https://www.amazon.com");
            AmazonHomePage homePage = new AmazonHomePage(driver);
            AmazonShoppingCartPage cartPage = homePage.goToCart();
            string page_title = driver.Title;
            Assert.AreEqual(page_title, "Amazon.com Shopping Cart");

        }

        [Test]
        public void TestProductPage()
        {
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
        public void TestAddProductToCart()
        {
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
