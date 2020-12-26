using BUKEP.TESTS.DRIVER.Enums;
using BUKEP.TESTS.DRIVER.Extensions;
using BUKEP.TESTS.DRIVER.Managers;
using BUKEP.TESTS.DRIVER.Managers.AsuBukep;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Threading.Tasks;

namespace ASU_BUKEP.Extensions
{
    internal static class ActionExtension
    {
        /// <summary>
        /// Перейти на
        /// </summary>
        /// <param name="url">Url адрес страницы</param>
        /// <param name="driver">Рабочий экземпляр драйвера</param>
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
        /// <param name="text">Текст, который нужно вставить</param>
        public static void Insert(this IWebElement webElement, string text, bool isLogger = true)
        {
            webElement.Clear();
            webElement.SendKeys(text);

            var operation = ErrorLoggingManager.GetModel(webElement, TypeOperation.Enter);
            if (isLogger)
            {
                ErrorLoggingManager.SaveAction(operation);
            }
        }

        /// <summary>
        /// Нажать
        /// </summary>
        /// <param name="webElement">Веб-элемент, на который нужно нажать</param>
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
        /// Раскрыть
        /// </summary>
        /// <param name="webElement">Веб-элемент, который нужно раскрыть</param>
        public static void ToUncover(this IWebElement webElement)
        {
            ErrorLoggingManager.SaveAction(ErrorLoggingManager.GetModel(webElement, TypeOperation.ToUncover));
            webElement.Click();
            ErrorLoggingManager.ErrorCheck();
        }

        /// <summary>
        /// Выбрать
        /// </summary>
        /// <param name="webElement">Веб-элемент, который необходимо выбрать</param>
        public static void Select(this IWebElement webElement)
        {
            var operation = ErrorLoggingManager.GetModel(webElement, TypeOperation.Select);
            ErrorLoggingManager.SaveAction(operation);
            webElement.Click();
            ErrorLoggingManager.ErrorCheck();
        }

        /// <summary>
        /// Раскрыть узел структурного дерева
        /// </summary>
        /// <param name="webElement">Узел дерева</param>
        public static void ExpandStructTreeNode(this IWebElement webElement)
        {
            ErrorLoggingManager.SaveAction(ErrorLoggingManager.GetModel(webElement, TypeOperation.ToUncover,
                JavaScriptManager.GetBranchNameInStructTree(webElement.GetId())));
            webElement.Click();
            ErrorLoggingManager.ErrorCheck();
        }

        public static async void ToDoubleClick2(this IWebElement webElement, ChromeDriver driver)
        {
            
        }
    }
}
