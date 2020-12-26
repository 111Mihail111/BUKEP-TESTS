using BUKEP.TESTS.DRIVER.Enums;
using BUKEP.TESTS.DRIVER.Extensions;
using BUKEP.TESTS.DRIVER.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace BUKEP.TESTS.DRIVER.Managers
{
    public class ErrorLoggingManager
    {
        private static ChromeDriver _driver;
        private static List<Operation> _stepLog;
        private static StreamWriter _errorFile;
        private static FindElementManager _find;
        private static IEnumerable<LogEntry> _logEntry;
        public static int CountError;
        public static StandType StandType;

        public ErrorLoggingManager(ChromeDriver chromeDriver, StreamWriter errorFile, FindElementManager findElementManager)
        {
            _driver = chromeDriver;
            _stepLog = new List<Operation>();
            _errorFile = errorFile;
            _find = findElementManager;
            CountError = 0;
        }


        public static void ErrorCheck()
        {
            _logEntry = GetErrorConsole();

            if (_logEntry.Any())
            {
                SaveErrorsConsole();
            }

            if (IsAlertExists())
            {
                return;
            }

            if (GetErrorPage(StandType).Any())
            {
                SaveErrorsPage();
            }
        }
        public static void ErrorCheck(Operation operation)
        {
            SaveAction(operation);

            if (IsAlertExists())
            {
                if (GetErrorConsole().Any())
                {
                    SaveErrorsConsole();
                }

                return;
            }

            if (GetErrorConsole().Any() || GetErrorPage(StandType).Any())
            {
                SaveErrorsConsole();
                SaveErrorsPage();
                return;
            }

            RemoveAction(operation);
        }



        /// <summary>
        /// Сохранить ошибку
        /// </summary>
        /// <param name="result">Результат</param>
        /// <param name="expectation">Ожидание</param>
        public void SaveError(string result, string expectation)
        {
            _errorFile.WriteLine("");
            _errorFile.WriteLine("Шаги:");

            SaveStepsInFile();

            _errorFile.WriteLine("");
            _errorFile.WriteLine("Результат:");
            _errorFile.WriteLine(result);

            _errorFile.WriteLine("");
            _errorFile.WriteLine("Ожидание:");
            _errorFile.WriteLine(expectation);
        }
        /// <summary>
        /// Сохранить шаги в файл
        /// </summary>
        protected static void SaveStepsInFile()
        {
            foreach (var item in _stepLog)
            {
                switch (item.TypeOperation)
                {
                    case TypeOperation.Click:
                        _errorFile.WriteLine(item.NumberAction + ". Нажать на: " + item.ElementName);
                        break;
                    case TypeOperation.GoTo:
                        _errorFile.WriteLine(item.NumberAction + ". Перейти по адресу: " + item.ElementName);
                        break;
                    case TypeOperation.Enter:
                        _errorFile.WriteLine(item.NumberAction + ". В элемент: " + item.ElementName + " ввести: " + item.Text);
                        break;
                    case TypeOperation.ToUncover:
                        _errorFile.WriteLine(item.NumberAction + ". Раскрыть: " + item.ElementName);
                        break;
                    case TypeOperation.Select:
                        _errorFile.WriteLine(item.NumberAction + ". Выбрать: " + item.ElementName);
                        break;
                    case TypeOperation.Close:
                        _errorFile.WriteLine(item.NumberAction + ". Закрыть: " + item.ElementName);
                        break;
                    case TypeOperation.Refresh:
                        _errorFile.WriteLine(item.NumberAction + ". Обновить страницу.");
                        break;
                    case TypeOperation.DoubleClick:
                        _errorFile.WriteLine(item.NumberAction + ". Дважды нажать на:" + item.ElementName);
                        break;
                    default:
                        _errorFile.WriteLine(item.NumberAction + ". Непредвиденная операция!");
                        break;
                }
            }
        }



        /// <summary>
        /// Открыто ли alert-уведомление в текущий момент
        /// </summary>
        /// <returns>True - если уведомление открыто, иначе - false</returns>
        protected static bool IsAlertExists()
        {
            try
            {
                _driver.SwitchTo().Alert();
                return true;
            }
            catch
            {
                return false;
            }
        }



        /// <summary>
        /// Получить ошибку на странице
        /// </summary>
        /// <returns></returns>
        protected static IEnumerable<IWebElement> GetErrorPage(StandType standType)
        {
            switch (standType)
            {
                case StandType.AsuBukep:
                    return _find.GetElementsById("fErrors").ToList();
                case StandType.AsuBukepTests:
                    return _find.GetElementsByXPath("/html/body/span/h2/i").ToList();
                case StandType.EnrolleeBukepTests:
                    return _find.GetElementsByClass("titleerror").ToList();
                default:
                    return null;
            }
        }
        /// <summary>
        /// Сохранить ошибку страницы
        /// </summary>
        /// <param name="webElements">Коллекция ошибок страницы</param>
        protected static void SaveErrorsPage()
        {
            var pageError = GetErrorPage(StandType);
            if (!pageError.Any())
            {
                return;
            }

            CountError++;

            _errorFile.WriteLine("");
            _errorFile.WriteLine("Шаги:");

            SaveStepsInFile();

            _errorFile.WriteLine("");
            _errorFile.WriteLine("Результат:");

            foreach (var item in pageError)
            {
                _errorFile.WriteLine(item.Text);
            }

            _errorFile.WriteLine("");
            _errorFile.WriteLine("Ожидание:");
        }


        /// <summary>
        /// Получить ошибки в веб-консоли.
        /// </summary>
        /// <returns>Лист моделей лог-ошибок</returns>
        protected static IEnumerable<LogEntry> GetErrorConsole()
        {
            //TODO: Это костыль. Нажатие на элемент обрабатывается медленне, чем код ниже
            Thread.Sleep(100);

            return _driver.Manage().Logs.GetLog("browser").ToList();
        }
        /// <summary>
        /// Сохранить ошибки консоли
        /// </summary>
        /// <param name="consoleLog">Коллекция ошибок консоли</param>
        protected static void SaveErrorsConsole()
        {
            if (!_logEntry.Any())
            {
                return;
            }

            CountError += _logEntry.Count();

            _errorFile.WriteLine("");
            _errorFile.WriteLine("Шаги:");

            SaveStepsInFile();

            _errorFile.WriteLine("");
            _errorFile.WriteLine("Результат:");

            foreach (var item in _logEntry)
            {
                _errorFile.WriteLine(item.Message);
            }

            _errorFile.WriteLine("");
            _errorFile.WriteLine("Ожидание:");
            _errorFile.WriteLine("Исправить ошибку/ошибки в консоли");
        }



        /// <summary>
        /// Сохранить действие
        /// </summary>
        /// <param name="operation">Операция в браузере</param>
        public static void SaveAction(Operation operation)
        {
            _stepLog.Add(operation);
        }
        /// <summary>
        /// Удалить действие
        /// </summary>
        /// <param name="operation">Операция в браузере</param>
        protected static void RemoveAction(Operation operation)
        {
            _stepLog.Remove(operation);
        }



        public static Operation GetModel(TypeOperation typeOperation, string text)
        {
            var operation = new Operation
            {
                TypeOperation = typeOperation,
                ElementName = text,
                NumberAction = _stepLog.Count
            };

            return operation;
        }
        public static Operation GetModel(IWebElement webElement, TypeOperation typeOperation)
        {
            var operation = new Operation
            {
                Tag = webElement.TagName,
                TypeOperation = typeOperation,
                ElementName = webElement.Text,
                NumberAction = _stepLog.Count,
                Text = webElement.GetValue(),
            };

            return operation;
        }
        public static Operation GetModel(IWebElement webElement, TypeOperation typeOperation, string elementName)
        {
            var operation = new Operation
            {
                NumberAction = _stepLog.Count,
                Tag = webElement.TagName,
                TypeOperation = typeOperation,
                ElementName = elementName,
                Text = webElement.GetValue(),
            };

            return operation;
        }
    }
}
