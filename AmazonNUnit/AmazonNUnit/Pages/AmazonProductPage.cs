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

        // updates the qty of an item on the product page
        public void updateQty(int qty)
        {
            driver.FindElement(By.XPath("//*[@data-action='a-dropdown-button']")).Click();
            driver.FindElement(By.XPath($"//a[@id='quantity_{qty - 1}']")).Click();
        }
    }
}
