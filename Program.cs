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
            driver.FindElement(By.Id("BoxHeader1")).Click();
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

            //Step 5 - Grab all of the placeholder text. Take that text and then enter it in the respective textbox, then submit form and capture the result
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

            //Step 6
            var captureResult = driver.FindElement(By.Id("formResult")).Text;
            var getLineNumVal = driver.FindElement(By.Id("lineNum")).Text;

            var insertRow = driver.FindElement(By.XPath($"//*[@id='inputTable']/tbody/tr[{getLineNumVal}]/td[2]/input"));
            insertRow.Clear();
            insertRow.SendKeys($"{captureResult}");
            insertRow.SendKeys(Keys.Enter);
            alert.Accept();

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

            //Step 7 - Click on the box and wait for delay. Then accept the alert. These last four blocks need to be refactored into a loop
            IWebElement boxSeven = driver.FindElement(By.Id("BoxParagraph7"));
            boxSeven.Click();
            var awaitStepSeven = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            awaitStepSeven.Until(drv => IsAlertShown(drv));
            alert.Accept();

            //Step 8
            IWebElement boxEight = driver.FindElement(By.Id("BoxParagraph8"));
            boxEight.Click();
            var awaitStepEight = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            awaitStepEight.Until(drv => IsAlertShown(drv));
            alert.Accept();

            //Step 9
            IWebElement boxNine = driver.FindElement(By.Id("BoxParagraph9"));
            boxNine.Click();
            var awaitStepNine = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            awaitStepNine.Until(drv => IsAlertShown(drv));
            alert.Accept();

            //Step 10
            IWebElement boxTen = driver.FindElement(By.Id("BoxParagraph10"));
            boxTen.Click();
            var awaitStepTen = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            awaitStepTen.Until(drv => IsAlertShown(drv));
            alert.Accept();
        }
    }
}
