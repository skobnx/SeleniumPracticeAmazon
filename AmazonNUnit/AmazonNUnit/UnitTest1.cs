﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using System;

namespace AmazonNUnit
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new SafariDriver();
        }

        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl(@"https://www.amazon.com");
            driver.Manage().Window.Maximize();
            AmazonHomePage homepage = new AmazonHomePage(driver);
            AmazonSearchPage searchPage = homepage.searchFor("backpacks");
            //searchPage.printProductPrices();
            String page_title = driver.Title;
            //searchPage.printProductPrices();
            searchPage.sortPrices(1);
            driver.Quit();
            Assert.AreEqual(page_title, "Amazon.com : backpacks");
        }
    }
}
