using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

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

        // for sort_order, 0 represents ascending and 1 represents decending.
        public void sortPrices(int sort_order)
        {

            IWebElement sort_select = driver.FindElement(By.XPath("//*[@id='a-autoid-0-announce']"));
            sort_select.Click();
            if (sort_order == 0)
            {
                IWebElement low_to_high = driver.FindElement(By.XPath("//*[@id='s-result-sort-select_1']"));
                low_to_high.Click();
            }
            else if(sort_order == 1)
            {
                IWebElement low_to_high = driver.FindElement(By.XPath("//*[@id='s-result-sort-select_2']"));
                low_to_high.Click();
            }
            else
            {
                Console.WriteLine("Invalid sort option selected");
            }

            Thread.Sleep(3000);

            this.printProductPrices();
            
            //IReadOnlyList<IWebElement> product_list = this.getProducts();
            //List<String> prices = new List<string>(product_list.Count);
            //foreach (IWebElement product in product_list)
            //{
            //    //get the price element for the current backpack element
            //    IWebElement priceText = product.FindElement(By.ClassName("a-offscreen"));
            //    //print the price element
            //    prices.Add(priceText.Text);
            //}
            //Console.WriteLine(prices[0]);

        }
    }
}
