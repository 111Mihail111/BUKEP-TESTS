using OpenQA.Selenium;

namespace BUKEP.TESTS.DRIVER.Extensions
{
    public static class WebAttributeExtension
    {
        /// <summary>
        /// Получить value элемента
        /// </summary>
        /// <param name="webElement">Вэб-элемент</param>
        /// <returns>Значение атрибута</returns>
        public static string GetValue(this IWebElement webElement)
        {
            return webElement.GetAttribute("value");
        }

        /// <summary>
        /// Получить id элемента
        /// </summary>
        /// <param name="webElement">Вэб-элемент</param>
        /// <returns>Значение атрибута</returns>
        public static string GetId(this IWebElement webElement)
        {
            return webElement.GetAttribute("id");
        }

        /// <summary>
        /// Получить innerText элемента
        /// </summary>
        /// <param name="webElement">Вэб-элемент</param>
        /// <returns>Значение атрибута</returns>
        public static string GetInnerText(this IWebElement webElement)
        {
            return webElement.GetAttribute("innerText");
        }
    }
}
