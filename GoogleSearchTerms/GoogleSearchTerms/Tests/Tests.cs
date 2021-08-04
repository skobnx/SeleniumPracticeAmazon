using System;
using System.Collections.Generic;
using System.IO;
using GoogleSearchTerms.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace GoogleSearchTerms.Tests
{
    [TestFixture]
    public class GoogleTests
    {
        private IWebDriver _driver;
        // change the browser type here before running the tests
        private string browser = "safari";

        public List<string> loadData(string filePath)
        {
            var reader = new StreamReader(File.OpenRead(filePath));
            List<string> animals = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                animals.Add(line);
            }
            return animals;
        }
        
        [SetUp]
        public void Setup()
        {
            _driver = new DriverFactory(browser).getDriver();
            _driver.Manage().Window.Maximize();

        }
        

        [Test]
        public void TestSearchTerms()
        {
            List<string> searchTerms = loadData(@"../../Data/animals.txt");

            foreach (string animal in searchTerms)
            {
                _driver.Navigate().GoToUrl("https://google.com/");
                Homepage homepage = new Homepage(_driver);
                SearchResultsPage searchResultsPage = homepage.searchForSomething(animal);
                Assert.AreEqual(_driver.Title, $"{animal} - Google Search");
            }
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }

    // class for constructing a web driver object
    public class DriverFactory
    {
        private IWebDriver driver;

        // Constructor
        public DriverFactory(string browserType)
        {
            switch (browserType)
            {
                case "safari":
                    driver = new SafariDriver();
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                default:
                    Console.WriteLine("Unsupported browser");
                    break;
            }
        }
        public IWebDriver getDriver()
        {
            return driver;
        }
        
    }
}