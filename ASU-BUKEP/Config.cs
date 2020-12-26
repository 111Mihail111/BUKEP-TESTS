using BUKEP.TESTS.DRIVER.Managers;
using BUKEP.TESTS.DRIVER.Managers.AsuBukep;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace ASU_BUKEP
{
    public class Config
    {
        public ChromeDriver chromeDriver = DriverInitialization();
        public static ChromeDriver Browser;
        public ErrorLoggingManager Logger;
        public FrameManager Frame;
        public static JavaScriptManager JSManager;
        public static FindElementManager Find;
        public static StreamWriter LogError;
        public static StreamWriter LogExeption;

        public void Init()
        {
            LogError = StreamWriterLogErrorInitialization();
            LogExeption = StreamWriterLogExeptionInitialization();

            Browser = DriverInitialization();
            Find = WebElementInitialization();
            Logger = LoggerInitialization();
            JSManager = JavaScriptInitialization();
            Frame = FrameInitialization();
        }

        /// <summary>
        /// Инициализация драйвера
        /// </summary>
        private static ChromeDriver DriverInitialization()
        {
            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            return new ChromeDriver("/Project/BUKEP-TESTS/Libraries/BUKEP.TESTS.DRIVER/bin/Debug/netcoreapp2.0", options, TimeSpan.FromSeconds(200));
        }
        /// <summary>
        /// Инициализация логера
        /// </summary>
        private static ErrorLoggingManager LoggerInitialization()
        {
            return new ErrorLoggingManager(Browser, LogError, Find);
        }
        /// <summary>
        /// Инициализация фрейм менеджера
        /// </summary>
        private static FrameManager FrameInitialization()
        {
            return new FrameManager(Browser, Find, JSManager);
        }
        /// <summary>
        /// Инициализация js менеджера
        /// </summary>
        private static JavaScriptManager JavaScriptInitialization()
        {
            return new JavaScriptManager(Browser);
        }
        /// <summary>
        /// Инициализация менеджера поиска элементов
        /// </summary>
        /// <returns></returns>
        private static FindElementManager WebElementInitialization()
        {
            return new FindElementManager(Browser);
        }
        /// <summary>
        /// Инициализация журнала ошибок
        /// </summary>
        /// <returns></returns>
        private static StreamWriter StreamWriterLogErrorInitialization()
        {
            return new StreamWriter(@"C:\Project\BUKEP-TESTS\_Logs\_LogError.txt", true);
        }
        /// <summary>
        /// Инициализация журнала исключений
        /// </summary>
        /// <returns></returns>
        private static StreamWriter StreamWriterLogExeptionInitialization()
        {
            return new StreamWriter(@"C:\Project\BUKEP-TESTS\_Logs\_LogExeption.txt", true);
        }
    }
}
