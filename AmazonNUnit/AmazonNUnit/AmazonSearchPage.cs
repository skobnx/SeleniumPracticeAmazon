using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AmazonNUnit
{
    public class AmazonSearchPage
    {
        private IWebDriver driver;

        public AmazonSearchPage(IWebDriver driver)
        {
            this.driver = driver;
            //wait for the next page to load
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"search\"]/span")));
        }

        private IReadOnlyList<IWebElement> getProducts()
        {
            return driver.FindElements(By.ClassName("a-price"));
        }

        public void printProductPrices()
        {
            IReadOnlyList<IWebElement> product_list = this.getProducts();
            //loop through list of elements
            foreach (IWebElement product in product_list)
            {
                //get the price element for the current backpack element
                IWebElement priceText = product.FindElement(By.ClassName("a-offscreen"));
                //print the price element
                Console.WriteLine(priceText.Text);
            }
        }
    }
}
