using System;
using System.Threading;
using OpenQA.Selenium;

namespace AmazonNUnit
{
    public class AmazonAddedToCartPage
    {
        private IWebDriver driver;
        private IWebElement CartButton;

        public AmazonAddedToCartPage(IWebDriver driver)
        {
            this.driver = driver;
            Thread.Sleep(3000);
            this.CartButton = driver.FindElement(By.XPath("//*[@id='nav-cart-count']"));
        }


        // function for clicking on the cart button, returns cart page
        public AmazonShoppingCartPage goToCart()
        {
            CartButton.Click();
            return new AmazonShoppingCartPage(driver);
        }
    }
}
