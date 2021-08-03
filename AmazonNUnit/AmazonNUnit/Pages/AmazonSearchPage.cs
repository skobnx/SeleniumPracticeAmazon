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
            Thread.Sleep(3000);

        }

        // gets all of the products and returns them as a list of web elements
        private IReadOnlyList<IWebElement> getProducts()
        {
            IReadOnlyList<IWebElement> product_list = driver.FindElements(By.XPath("//*[@data-component-type='s-search-result'][not(.//span[text()='Sponsored'])]//*[contains(@class,'a-offscreen')]"));
            return product_list;
        }

        // prints out the prices of the products on the search result page to the console
        public void printProductPrices()
        {
            IReadOnlyList<IWebElement> product_list = this.getProducts();
            //loop through list of elements
            foreach (IWebElement product in product_list)
            {
                //print the price element
                Console.WriteLine(product.Text);
            }
        }


        // uses the page's function to sort the items on the page.
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
        }

        // gets the list of prices for all of the items on the search results page
        public List<double> get_list_of_prices()
        {
            IReadOnlyList<IWebElement> product_list = this.getProducts();

            List<double> price_list = new List<double>(product_list.Count + 2);
            //loop through list of elements
            foreach (IWebElement product in product_list)
            {
                string price_string = product.Text;
                price_string = price_string.Replace("$", "");
                price_string = price_string.Replace(",", "");
                double double_price = Convert.ToDouble(price_string);
                price_list.Add(double_price);
            }

            return price_list;


        }

        // Takes an index number and clicks on the nth product on the search results page
        // bringing the driver to the product page for that product
        public AmazonProductPage GoToProductPage(int product_number)
        {
            try
            {
                driver.FindElements(By.XPath("//img[@class='s-image']"))[product_number].Click();
            }
            catch
            {
                Console.Write("Not Clickable index\n");
                driver.Quit();
            }
            return new AmazonProductPage(driver);
        }
    }
}
