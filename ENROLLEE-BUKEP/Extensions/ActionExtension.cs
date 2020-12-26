using BUKEP.TESTS.DRIVER.Enums;
using BUKEP.TESTS.DRIVER.Extensions;
using BUKEP.TESTS.DRIVER.Managers;
using BUKEP.TESTS.DRIVER.Managers.EnrolleeBukep;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace ENROLLEE_BUKEP.Extensions
{
    internal static class ActionExtension
    {
        /// <summary>
        /// Перейти на
        /// </summary>
        /// <param name="url">Url адрес страницы</param>
        /// <param name="driver">Рабочий экземпляр драйвера</param>
        /// <param name="isLogger">True - если логировать действие, иначе - false</param>
        public static void GoTo(this string url, ChromeDriver driver, bool isLogger = true)
        {
            var operation = ErrorLoggingManager.GetModel(TypeOperation.GoTo, url);
            if (isLogger)
            {
                ErrorLoggingManager.SaveAction(operation);
                driver.Navigate().GoToUrl(url);
                ErrorLoggingManager.ErrorCheck();
                return;
            }

            driver.Navigate().GoToUrl(url);
            ErrorLoggingManager.ErrorCheck(operation);
        }

        /// <summary>
        /// Вставить
        /// </summary>
        /// <param name="webElement">Веб-элемент</param>
        /// <param name="text">Вставляемый текст</param>
        /// <param name="isLogger">True - если логировать действие, иначе - false</param>
        public static void Insert(this IWebElement webElement, string text, bool isLogger = true)
        {
            webElement.Clear();
            webElement.SendKeys(text);

            var operation = ErrorLoggingManager.GetModel(webElement, TypeOperation.Enter, JavaScriptManager.GetLabel(webElement.GetId()));
            if (isLogger)
            {
                ErrorLoggingManager.SaveAction(operation);
            }
        }

        /// <summary>
        /// Нажать
        /// </summary>
        /// <param name="webElement">Веб-элемент, на который нужно нажать</param>
        /// <param name="isLogger">True - если логировать действие, иначе - false</param>
        public static void ToClick(this IWebElement webElement, bool isLogger = true)
        {
            var operation = ErrorLoggingManager.GetModel(webElement, TypeOperation.Click);
            if (isLogger)
            {
                ErrorLoggingManager.SaveAction(operation);
                webElement.Click();
                ErrorLoggingManager.ErrorCheck();
                return;
            }

            webElement.Click();
            ErrorLoggingManager.ErrorCheck(operation);
        }

        /// <summary>
        /// Дважды нажать
        /// </summary>
        /// <param name="webElement">Веб-элемент, на который нужно нажать</param>
        public static void ToDoubleCLick(this IWebElement webElement)
        {
            ErrorLoggingManager.SaveAction(ErrorLoggingManager.GetModel(webElement, TypeOperation.DoubleClick));

            webElement.Click();
            Thread.Sleep(100);
            ErrorLoggingManager.ErrorCheck();
            webElement.Click();
            ErrorLoggingManager.ErrorCheck();
        }

        public static async void ToDoubleClick2(this ChromeDriver chromeDriver)
        {
            chromeDriver.FindElementById("update-profile-button").Click();
            chromeDriver.FindElementById("update-profile-button").Click();
        }

        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="browserPage">Страница браузера</param>
        public static void Refresh(this ChromeDriver browserPage)
        {
            ErrorLoggingManager.SaveAction(ErrorLoggingManager.GetModel(TypeOperation.Refresh, string.Empty));
            browserPage.Navigate().Refresh();
            ErrorLoggingManager.ErrorCheck();
        }
    }
}
