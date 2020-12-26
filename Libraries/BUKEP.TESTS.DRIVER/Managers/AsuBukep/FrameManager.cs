using OpenQA.Selenium.Chrome;

namespace BUKEP.TESTS.DRIVER.Managers.AsuBukep
{
    public class FrameManager
    {
        private ChromeDriver _driver;
        private FindElementManager _findElement;
        private JavaScriptManager _jsManager;

        public FrameManager(ChromeDriver chromeDriver, FindElementManager findElement, JavaScriptManager jsManager)
        {
            _driver = chromeDriver;
            _findElement = findElement;
            _jsManager = jsManager;
        }

        /// <summary>
        /// Настройка позиции скрола
        /// </summary>
        /// <param name="formId">Идентификатор формы, в которой присутствует прокрутка</param>
        /// <param name="positionScroll">Позиция вертикального скрола</param>
        public void ScrollPositionSetting(string formId, int positionScroll)
        {
            _driver.ExecuteScript($"document.getElementById('{formId}').scrollTop = {positionScroll}");
        }

        /// <summary>
        /// Переключиться на фрейм
        /// </summary>
        /// <param name="index">Индекс фрейма</param>
        public void SwitchToFrame(int index = 0)
        {
            _driver.SwitchTo().Frame(index);
        }

        /// <summary>
        /// Переключиться на основной контент страницы
        /// </summary>
        public void SwitchToDefaultContent()
        {
            _driver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Закрыть фрейм
        /// </summary>
        public void CloseFrame()
        {
            var webElement = _findElement.GetElementByXPath("//*[@id='aspnetForm']/div[6]/div[2]/div[1]/a[2]");
            ErrorLoggingManager.SaveAction(ErrorLoggingManager.GetModel(webElement, Enums.TypeOperation.Close,
                    _jsManager.GetTitleFrame("ui-win-titlebar-close g-icon-18x18 icon48")));
            webElement.Click();
        }
    }
}
