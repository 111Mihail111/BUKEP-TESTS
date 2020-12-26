using BUKEP.TESTS.DRIVER.Enums;
using OpenQA.Selenium.Chrome;

namespace BUKEP.TESTS.DRIVER.Managers.AsuBukep
{
    public class JavaScriptManager
    {
        private static ChromeDriver _driver;

        public JavaScriptManager(ChromeDriver chromeDriver)
        {
            _driver = chromeDriver;
        }

        /// <summary>
        /// Получить название узла в структурном дереве
        /// </summary>
        /// <param name="elementId">Идентификатор узла</param>
        /// <returns>Наименование узла</returns>
        public static string GetBranchNameInStructTree(string elementId)
        {
            return _driver.ExecuteScript($"return document.getElementById('{elementId}')" +
                ".parentElement.nextElementSibling.innerText").ToString();
        }
        /// <summary>
        /// Получить зоголовок фрейма
        /// </summary>
        /// <param name="classNames">Классы элемента</param>
        /// <returns>Наименование заголовка</returns>
        public string GetTitleFrame(string classNames)
        {
            return _driver.ExecuteScript($"return document.getElementsByClassName('{classNames}')" +
                $"[0].previousElementSibling.previousElementSibling.textContent").ToString();
        }
        /// <summary>
        /// Имитация нажатия кнопки Отмена, в alert уведомлении
        /// </summary>
        public void AlertToDismiss()
        {
            ErrorLoggingManager.SaveAction(ErrorLoggingManager.GetModel(TypeOperation.Click, "Отмена"));
            _driver.SwitchTo().Alert().Dismiss();
            ErrorLoggingManager.ErrorCheck();
        }
        /// <summary>
        /// Имитация нажатия кнопки Ок, в alert уведомлении
        /// </summary>
        public void AlertToAccept()
        {
            ErrorLoggingManager.SaveAction(ErrorLoggingManager.GetModel(TypeOperation.Click, "Ок"));
            _driver.SwitchTo().Alert().Accept();
            ErrorLoggingManager.ErrorCheck();
        }
    }
}
