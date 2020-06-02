﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BookShop.AcceptanceTests.Drivers.Selenium.PageObjects
{
    class HomePageObject
    {
        private readonly IWebDriver _webDriver;

        public HomePageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public IWebElement SearchTerm => _webDriver.FindElement(By.Id("searchTerm"));

        public IWebElement SearchButton => _webDriver.FindElement(By.Id("searchForm"));

        public IWebElement BookTable => _webDriver.FindElement(By.ClassName("table"));

        public IEnumerable<IWebElement> TableLines => BookTable.FindElements(By.TagName("tr"));

        public IEnumerable<SearchResultEntry> CheapestThreeBooks => TableLines.Skip(1).Select(r => new SearchResultEntry(r));

        public void Search(string term)
        {
            SearchTerm.Clear();
            SearchTerm.SendKeys(term);

            SearchButton.Submit();

            RetryHelper.WaitFor(() => _webDriver.Url.EndsWith("Catalog/Search"));


        }
    }
}
