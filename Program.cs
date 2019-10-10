using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace HPABot
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize driver
            IWebDriver driver = new ChromeDriver();

            //Navigate to URL
            driver.Navigate().GoToUrl("https://hpadevtest.azurewebsites.net/");

            //Maximize browser window
            driver.Manage().Window.Maximize();

            //Step 1 - Click on Step 1 and then accept the alert
            driver.FindElement(By.Id("Box1")).Click();
            var alert = driver.SwitchTo().Alert();
            alert.Accept();

            //Step 2 - Click on Step 2, send tab key, and then accept the alert
            driver.FindElement(By.Id("Box3")).Click();
            driver.FindElement(By.Id("Box3")).SendKeys(Keys.Tab);
            alert.Accept();

            //Step 3 - Determine which radio button is being requested, then click the requested radio button, accept the alert
            var getRadioVal = driver.FindElement(By.Id("optionVal")).Text;
            IWebElement radioButton = driver.FindElement(By.XPath($"//*[@type='radio'][{getRadioVal}]"));
            radioButton.Click();
            alert.Accept();

            //Step 4 - Determine which drop down option is being requested. Click the desired dropdown option, and then accept the alert
            var getDropdownVal = driver.FindElement(By.Id("selectionVal")).Text;
            SelectElement oSelect = new SelectElement(driver.FindElement(By.TagName("select")));
            oSelect.SelectByValue($"{getDropdownVal}");
            alert.Accept();

            //Step 5 - Grab all of the placeholder text from each element. Take that text and enter it in the respective textbox then submit form and capture the result

            List<IWebElement> dateTextBoxes = driver.FindElements(By.Id("formDate")).ToList();
            foreach (var date in dateTextBoxes)
            {
                var dateVal = date.GetAttribute("placeholder").ToString();
                date.SendKeys($"{dateVal}");
            }

            List<IWebElement> cityTextBoxes = driver.FindElements(By.Id("formCity")).ToList();
            foreach (var city in cityTextBoxes)
            {
                var cityVal = city.GetAttribute("placeholder").ToString();
                city.SendKeys($"{cityVal}");
            }

            List<IWebElement> stateTextBoxes = driver.FindElements(By.Id("formState")).ToList();
            foreach (var state in stateTextBoxes)
            {
                var stateVal = state.GetAttribute("placeholder").ToString();
                state.SendKeys($"{stateVal}");
            }

            List<IWebElement> countryTextBoxes = driver.FindElements(By.Id("formCountry")).ToList();
            foreach (var country in countryTextBoxes)
            {
                var countryVal = country.GetAttribute("placeholder").ToString();
                country.SendKeys($"{countryVal}");
            }

            var getSubmitButton = driver.FindElement(By.TagName("button"));
            getSubmitButton.Click();
            alert.Accept();

            //Step 6 - Capture the result from step 5. Find the requested line number to insert the result into. 
            var captureResult = driver.FindElement(By.Id("formResult")).Text;
            var getLineNumVal = driver.FindElement(By.Id("lineNum")).Text;

            //Find the requested element by id. Clear the element. Insert the result in desired element. Hit enter and then accept the alert
            var insertRow = driver.FindElement(By.XPath($"//*[@id='inputTable']/tbody/tr[{getLineNumVal}]/td[2]/input"));
            insertRow.Clear();
            insertRow.SendKeys($"{captureResult}");
            insertRow.SendKeys(Keys.Enter);
            alert.Accept();

            //Steps 7 - 10 - Click each element, wait for the delay, then accept the alert
            //Handle whether alert is present
            bool IsAlertShown(IWebDriver drv)
            {
                try
                {
                    drv.SwitchTo().Alert();
                }
                catch (NoAlertPresentException ex)
                {
                    return false;
                }
                return true;
            }
            
            //Build a list to hold the desired box numbers
            List<int> nums = new List<int>()
            {
                7, 8, 9, 10
            };

            //Run through a foreach for each number that is in the list
            foreach(var num in nums)
            {
                //Find each element by id ex. Box7, Box8, Box9, Box10
                var boxes = driver.FindElements(By.Id($"Box{num}"));

                //Run through another foreach loop to click on each element, await the delay, then accept the alert
                foreach (var box in boxes)
                {
                    box.Click();
                    var awaitAlert = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                    awaitAlert.Until(drv => IsAlertShown(drv));
                    alert.Accept();
                }
            }
        }
    }
}
