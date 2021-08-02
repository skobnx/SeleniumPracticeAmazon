using System;
using System.Threading;
using OpenQA.Selenium;

namespace AmazonNUnit
{
    public class AmazonProductPage
    {
        private IWebDriver driver;
        private IWebElement addToCartButton;


        public AmazonProductPage(IWebDriver driver)
        {
            this.driver = driver;
            Thread.Sleep(3000);
            this.addToCartButton = driver.FindElement(By.XPath("//*[@id='add-to-cart-button']"));
        }

        // presses the add to cart button for the current item
        public AmazonAddedToCartPage addItemToCart()
        {
            this.addToCartButton.Click();
            return new AmazonAddedToCartPage(driver);
        }
    }
}
