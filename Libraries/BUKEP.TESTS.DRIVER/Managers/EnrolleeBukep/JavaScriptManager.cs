using OpenQA.Selenium.Chrome;

namespace BUKEP.TESTS.DRIVER.Managers.EnrolleeBukep
{
    public class JavaScriptManager
    {
        private static ChromeDriver _driver;

        public JavaScriptManager(ChromeDriver chromeDriver)
        {
            _driver = chromeDriver;
        }

        /// <summary>
        /// Получить лэйбл элемента
        /// </summary>
        /// <param name="elementId">Идентификатор элемента</param>
        /// <returns>Название лейбла над элементом</returns>
        public static string GetLabel(string elementId)
        {
            var a = $"return document.querySelector(\"label[for='{ elementId }']\").innerText";

            return _driver.ExecuteScript(a).ToString();
        }
    }
}
