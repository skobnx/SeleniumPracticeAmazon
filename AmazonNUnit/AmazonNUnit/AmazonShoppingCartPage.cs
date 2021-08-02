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

        //returns the string containing the number of items in the cart.
        public string number_of_items_in_cart()
        {
            return driver.FindElement(By.XPath("//*[@id='sc-subtotal-label-buybox']")).Text;
        }
    }
}
