using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace AmazonNUnit
{
    public class AmazonHomePage
    {
        private IWebDriver driver;
        private IWebElement MainSearchBar;
        private IWebElement MainSearchBarButton;

        public AmazonHomePage(IWebDriver driver)
        {
            this.driver = driver;
            this.MainSearchBar = driver.FindElement(By.XPath("//*[@id=\"twotabsearchtextbox\"]"));
            this.MainSearchBarButton = driver.FindElement(By.XPath("//*[@id=\"nav-search-submit-button\"]"));
        }

        public AmazonSearchPage searchFor(String search_key)
        {
            MainSearchBar.SendKeys(search_key);
            MainSearchBarButton.Click();
            return new AmazonSearchPage(driver);
        }
    }

}
