using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BUKEP.TESTS.DRIVER.Managers
{
    public class FindElementManager
    {
        private ChromeDriver _driver;

        public FindElementManager(ChromeDriver chromeDriver)
        {
            _driver = chromeDriver; 
        }

        /// <summary>
        /// Получить элемент по Id
        /// </summary>
        /// <param name="webElementId">Идентификатор веб-элемента</param>
        /// <param name="waitingTimeSecond">Время ожидания элемента</param>
        /// <returns>Веб-элемент</returns>
        public IWebElement GetElementById(string webElementId, int waitingTimeSecond = 5)
        {
            _driver.ExecuteScript("console.clear();");
            return ExpectationElement(webElementId, waitingTimeSecond);
        }
        /// <summary>
        /// Получить коллекцию элементов по Id
        /// </summary>
        /// <param name="webElementId">Идентификатор веб-элемента</param>
        /// <returns>Коллекция веб-элементов</returns>
        public IEnumerable<IWebElement> GetElementsById(string webElementId)
        {
            _driver.ExecuteScript("console.clear();");
            return _driver.FindElementsById(webElementId);
        }
        /// <summary>
        /// Получить колличество элементов по Id
        /// </summary>
        /// <param name="webElementId">Ид элемента</param>
        /// <returns>Колличество элементов</returns>
        public int GetCountElementsById(string webElementId)
        {
            return _driver.FindElementsById(webElementId).Count;
        }



        /// <summary>
        /// Получить элемент по имени класса
        /// </summary>
        /// <param name="webElementClass">Класс веб-элемента</param>
        /// <param name="waitingTimeSecond">Время ожидание элемента</param>
        /// <returns>Веб-элемент</returns>
        public IWebElement GetElementByClass(string webElementClass, int waitingTimeSecond = 5)
        {
            _driver.ExecuteScript("console.clear();");
            return ExpectationElement(webElementClass, waitingTimeSecond);
        }
        /// <summary>
        /// Получить коллекцию элементов по имени класса
        /// </summary>
        /// <param name="webElementClass">Класс веб-элемента</param>
        /// <returns>Коллекция веб-элементов</returns>
        public IEnumerable<IWebElement> GetElementsByClass(string webElementClass)
        {
            _driver.ExecuteScript("console.clear();");
            return _driver.FindElementsByClassName(webElementClass);
        }



        /// <summary>
        /// Получить элемент по xPath
        /// </summary>
        /// <param name="xPath">XPath путь в DOM страницы</param>
        /// <returns>Веб-элемент</returns>
        public IWebElement GetElementByXPath(string xPath)
        {
            _driver.ExecuteScript("console.clear();");
            return _driver.FindElementByXPath(xPath);
        }
        /// <summary>
        /// Получить коллекцию элементов по xPath
        /// </summary>
        /// <param name="xPath">xPath путь в DOM страницы</param>
        /// <returns>Коллекция веб-элементов</returns>
        public IEnumerable<IWebElement> GetElementsByXPath(string xPath)
        {
            _driver.ExecuteScript("console.clear();");
            return _driver.FindElementsByXPath(xPath).ToList();
        }
        /// <summary>
        /// Получить колличество элементов по xPath
        /// </summary>
        /// <param name="xPath">Путь к элементу</param>
        /// <returns>Количество элементов</returns>
        public int GetCountElementsByXPath(string xPath)
        {
            return GetElementsByXPath(xPath).Count();
        }



        /// <summary>
        /// Ожидание элемента на странице
        /// </summary>
        /// <param name="attributeElement">Атрибут, по которому искать элемент</param>
        /// <param name="waitingTimeSecond">Время ожидания элемента</param>
        /// <returns>Веб-элемент</returns>
        protected IWebElement ExpectationElement(string attributeElement, int waitingTimeSecond)
        {
            while (waitingTimeSecond != 0)
            {
                var webElementClass = GetElementsByClass(attributeElement);
                if (webElementClass.Any())
                {
                    return webElementClass.First();
                }

                var webElementId = GetElementsById(attributeElement);
                if (webElementId.Any())
                {
                    return webElementId.First();
                }

                Thread.Sleep(1000);
                waitingTimeSecond--;
            }

            //TODO: ОБЕРНУТЬ В TRY CATCH И ОЖИДАТЬ ОШИБКУ ЗДЕСЬ, если элемент не найден
            return null;
        }
    }
}
