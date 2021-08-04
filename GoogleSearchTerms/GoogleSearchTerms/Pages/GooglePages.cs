using System;
using System.Threading;
using OpenQA.Selenium;

namespace GoogleSearchTerms.Pages
{
    public class Homepage
    {
        private IWebDriver driver;
        private IWebElement searchBar;

        public Homepage(IWebDriver driver)
        {
            this.driver = driver;
            searchBar = driver.FindElement(By.Name("q"));
        }

        public SearchResultsPage searchForSomething(string query)
        {
            searchBar.SendKeys(query);
            searchBar.SendKeys(Keys.Return);
            return new SearchResultsPage(driver);
        }
    }

    public class SearchResultsPage
    {
        private IWebDriver _driver;
        public SearchResultsPage(IWebDriver driver)
        {
            _driver = driver;
            Thread.Sleep(2000);
        }
    }
}