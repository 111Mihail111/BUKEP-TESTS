using ASU_BUKEP_TESTS.Enums;
using BUKEP.TESTS.DRIVER.Enums;
using BUKEP.TESTS.DRIVER.Extensions;
using BUKEP.TESTS.DRIVER.Infrastructure;
using BUKEP.TESTS.DRIVER.Managers;
using ENROLLEE_BUKEP.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ENROLLEE_BUKEP.Modules.Account
{
    [TestClass]
    public class Manage : Config
    {
        [TestInitialize]
        public void Initialize()
        {
            ConfigInit();
            ErrorLoggingManager.StandType = StandType.EnrolleeBukepTests;

            Browser.Manage().Window.Maximize();
            "http://iis-2:812/Identity/Account/Manage".GoTo(Browser);

            Find.GetElementById("Input_Email").Insert("polowinckin.mixail@yandex.ru");
            Find.GetElementById("Input_Password").Insert("!123QWERZXCV!");
            Find.GetElementById("logIn").ToClick(false);
        }

        [TestCategory("Аккаунт")]
        [Case("Тест кейс № 1")]
        [Description("Ввод максимальной длины и всяческих символов в поля")]
        [Priority((int)TestPriority.Average)]
        [TestMethod]
        public void Iis2_InputSymbolsInFieldsFullNameAndPhone_ReturnErrors_0()
        {
            try
            {
                string itemText;

                #region Block_1
                Find.GetElementById("Input_LastName").Insert(string.Empty);
                Find.GetElementById("update-profile-button").ToDoubleCLick();
                itemText = Find.GetElementById("Input_LastName-error").GetInnerText();
                if (itemText != "Поле фамилия не может быть пустым")
                {
                    Logger.SaveError(itemText, "Выйдет сообщение: Поле фамилия не может быть пустым");
                    ErrorLoggingManager.CountError++;
                }

                Browser.Refresh();
                Find.GetElementById("Input_FirstName").Insert(string.Empty);
                Find.GetElementById("update-profile-button").ToDoubleCLick();
                itemText = Find.GetElementById("Input_FirstName-error").GetInnerText();
                if (itemText != "Поле имя не может быть пустым")
                {
                    Logger.SaveError(itemText, "Выйдет сообщение: Поле имя не может быть пустым");
                    ErrorLoggingManager.CountError++;
                }

                Browser.Refresh();
                Find.GetElementById("Input_PhoneNumber").Insert(string.Empty);
                Find.GetElementById("update-profile-button").ToDoubleCLick();
                itemText = Find.GetElementById("Input_PhoneNumber-error").GetInnerText();
                if (itemText != "Поле телефон не может быть пустым")
                {
                    Logger.SaveError(itemText, "Выйдет сообщение: Поле телефон не может быть пустым");
                    ErrorLoggingManager.CountError++;
                }
                #endregion Block_1

                #region Block_2
                Browser.Refresh();

                Find.GetElementById("Input_LastName").Insert("   ");
                Find.GetElementById("update-profile-button").ToDoubleCLick();
                itemText = Find.GetElementByXPath("//*[@id='profile-form']/div[1]/div[1]/div/span").GetInnerText();
                if (itemText != "Поле фамилия не может быть пустым")
                {
                    Logger.SaveError(itemText, "Выйдет сообщение: Поле фамилия не может быть пустым");
                    ErrorLoggingManager.CountError++;
                }

                Browser.Url.GoTo(Browser, false);
                Find.GetElementById("Input_FirstName").Insert("   ");
                Find.GetElementById("update-profile-button").ToDoubleCLick();
                itemText = Find.GetElementByXPath("//*[@id='profile-form']/div[1]/div[2]/div/span").GetInnerText();
                if (itemText != "Поле имя не может быть пустым")
                {
                    Logger.SaveError(itemText, "Выйдет сообщение: Поле имя не может быть пустым");
                    ErrorLoggingManager.CountError++;
                }

                Browser.Url.GoTo(Browser, false);
                Find.GetElementById("Input_PhoneNumber").Insert("   ");
                Find.GetElementById("update-profile-button").ToDoubleCLick();
                itemText = Find.GetElementByXPath("//*[@id='profile-form']/div[2]/div[4]/div/span").GetInnerText();
                if (itemText != "Поле телефон не может быть пустым")
                {
                    Logger.SaveError(itemText, "Выйдет сообщение: Поле телефон не может быть пустым");
                    ErrorLoggingManager.CountError++;
                }
                #endregion Block_2

                #region Block_3
                Browser.Url.GoTo(Browser, false);

                string dataTestOne = "Wqwq ejewkjkweewqkjqq qwewqee wewqewewwqe wqeqewqewewqqewwewq ewqqewqewew qqweqewe ewqewqq " +
                        "ewqweqewqewe eqwqewqewqew32432f2 @!@#$%^&*()-+=<>//--";

                Find.GetElementById("Input_LastName").Insert(dataTestOne);
                Find.GetElementById("update-profile-button").ToDoubleCLick();

                Find.GetElementById("Input_FirstName").Insert(dataTestOne);
                Find.GetElementById("update-profile-button").ToDoubleCLick();

                Find.GetElementById("Input_Patronymic").Insert(dataTestOne);
                Find.GetElementById("update-profile-button").ToDoubleCLick();

                string dataTestTwo = "23423 22222  22 22 2 22 2344 4 4";

                Find.GetElementById("Input_PhoneNumber").Insert(dataTestTwo);
                Find.GetElementById("update-profile-button").ToDoubleCLick();

                //TODO: Заменить на ID: Проверка появления сообщения об успешном сохранении, согласно документации
                #endregion Block_3

                #region Block_4
                Browser.Refresh();

                if (Find.GetElementById("Input_LastName").GetValue() != dataTestOne)
                {
                    Logger.SaveError($"Введеные данные не сохранились: {dataTestOne}", "Данные сохранятся");
                    ErrorLoggingManager.CountError++;
                }
                else if (Find.GetElementById("Input_FirstName").GetValue() != dataTestOne)
                {
                    Logger.SaveError($"Введеные данные не сохранились: {dataTestOne}", "Данные сохранятся");
                    ErrorLoggingManager.CountError++;
                }
                else if (Find.GetElementById("Input_Patronymic").GetValue() != dataTestOne)
                {
                    Logger.SaveError($"Введеные данные не сохранились: {dataTestOne}", "Данные сохранятся");
                    ErrorLoggingManager.CountError++;
                }
                else if (Find.GetElementById("Input_PhoneNumber").GetValue() != dataTestTwo)
                {
                    Logger.SaveError($"Введеные данные не сохранились: {dataTestTwo}", "Данные сохранятся");
                    ErrorLoggingManager.CountError++;
                }
                #endregion Block_4

                #region Insert_Default_Value
                Find.GetElementById("Input_LastName").Insert("Половинкин");
                Find.GetElementById("Input_FirstName").Insert("Михаил");
                Find.GetElementById("Input_Patronymic").Insert("Валерьевич");
                Find.GetElementById("Input_PhoneNumber").Insert("+79066050955");
                Find.GetElementById("update-profile-button").ToClick();
                #endregion Insert_Default_Value

                Browser.Quit();
                LogError.Close();
                Assert.AreEqual(0, ErrorLoggingManager.CountError);
            }
            catch (Exception exeption)
            {
                Browser.Quit();
                LogError.Close();

                LogExeption.WriteLine("");
                LogExeption.WriteLine(exeption);
                LogExeption.Close();

                Assert.AreEqual(0, ErrorLoggingManager.CountError);
            }
        }

        [TestCategory("Аккаунт")]
        [Case("Тест кейс № 2")]
        [Description("Проверка клиентской и серверной валидации, путем ввода тестовых данных в числовые поля.")]
        [Priority((int)TestPriority.Average)]
        [TestMethod]
        public void Iis2_InputNumbersIntoFieldsDayMonthYear_ReturnErrors_0()
        {
            Browser.ToDoubleClick2();
            //try
            //{
            string itemText;

            #region Block_1
            Find.GetElementById("Input_Day").Insert(string.Empty);
            Find.GetElementById("update-profile-button").ToDoubleCLick();
            itemText = Find.GetElementById("Input_Day-error").GetInnerText();
            if (itemText != "Поле день не может быть пустым")
            {
                Logger.SaveError(itemText, "Выйдет сообщение: Поле день не может быть пустым");
                ErrorLoggingManager.CountError++;
            }

            Browser.Refresh();
            Find.GetElementById("Input_Month").Insert(string.Empty);
            Find.GetElementById("update-profile-button").ToDoubleCLick();
            itemText = Find.GetElementById("Input_Month-error").GetInnerText();
            if (itemText != "Поле месяц не может быть пустым")
            {
                Logger.SaveError(itemText, "Выйдет сообщение: Поле месяц не может быть пустым");
                ErrorLoggingManager.CountError++;
            }

            Browser.Refresh();
            Find.GetElementById("Input_Year").Insert(string.Empty);
            Find.GetElementById("update-profile-button").ToDoubleCLick();
            itemText = Find.GetElementById("Input_Year-error").GetInnerText();
            if (itemText != "Поле год не может быть пустым")
            {
                Logger.SaveError(itemText, "Выйдет сообщение: Поле год не может быть пустым");
                ErrorLoggingManager.CountError++;
            }
            #endregion Block_2

            #region Block_2
            Browser.Refresh();
            Find.GetElementById("Input_Day").Insert("   ");
            Find.GetElementById("update-profile-button").ToDoubleCLick();
            itemText = Find.GetElementById("Input_Day-error").GetInnerText();
            if (itemText != "Поле день не может быть пустым")
            {
                Logger.SaveError(itemText, "Выйдет сообщение: Поле день не может быть пустым");
                ErrorLoggingManager.CountError++;
            }

            Browser.Url.GoTo(Browser, false);
            Find.GetElementById("Input_Month").Insert("   ");
            Find.GetElementById("update-profile-button").ToDoubleCLick();
            itemText = Find.GetElementById("Input_Month-error").GetInnerText();
            if (itemText != "Поле месяц не может быть пустым")
            {
                Logger.SaveError(itemText, "Выйдет сообщение: Поле месяц не может быть пустым");
                ErrorLoggingManager.CountError++;
            }

            Browser.Url.GoTo(Browser, false);
            Find.GetElementById("Input_Year").Insert("   ");
            Find.GetElementById("update-profile-button").ToDoubleCLick();
            itemText = Find.GetElementById("Input_Year-error").GetInnerText();
            if (itemText != "Поле год не может быть пустым")
            {
                Logger.SaveError(itemText, "Выйдет сообщение: Поле год не может быть пустым");
                ErrorLoggingManager.CountError++;
            }
            #endregion Block_2

            #region Block_3
            Browser.Url.GoTo(Browser, false);
            Find.GetElementById("Input_Day").Insert("выалд");
            Find.GetElementById("update-profile-button").ToDoubleCLick();

            if (Find.GetElementById("Input_Day").GetValue() != string.Empty)
            {
                Logger.SaveError("Ввелись буквы", "Буквы не введутся");
                ErrorLoggingManager.CountError++;
            }

            Browser.Refresh();
            Find.GetElementById("Input_Month").Insert("выалд");
            Find.GetElementById("update-profile-button").ToDoubleCLick();

            if (Find.GetElementById("Input_Month").GetValue() != string.Empty)
            {
                Logger.SaveError("Ввелись буквы", "Буквы не введутся");
                ErrorLoggingManager.CountError++;
            }

            Browser.Refresh();
            Find.GetElementById("Input_Year").Insert("выалд");
            Find.GetElementById("update-profile-button").ToDoubleCLick();

            if (Find.GetElementById("Input_Year").GetValue() != string.Empty)
            {
                Logger.SaveError("Ввелись буквы", "Буквы не введутся");
                ErrorLoggingManager.CountError++;
            }

            Browser.Refresh();
            #endregion Block_3

            //TODO: Заменить на ID: Заменить текст сообщения
            #region Block_4
            Find.GetElementById("Input_Day").Insert("eee");
            Find.GetElementById("update-profile-button").ToDoubleCLick();
            itemText = Find.GetElementById("Input_Day-error").GetInnerText();
            if (itemText != "Please enter a valid number.")
            {
                Logger.SaveError(itemText, "Выйдет сообщение: Please enter a valid number.");
                ErrorLoggingManager.CountError++;
            }

            Browser.Refresh();
            Find.GetElementById("Input_Month").Insert("eee");
            Find.GetElementById("update-profile-button").ToDoubleCLick();
            itemText = Find.GetElementById("Input_Month-error").GetInnerText();
            if (itemText != "Please enter a valid number.")
            {
                Logger.SaveError(itemText, "Выйдет сообщение: Please enter a valid number.");
                ErrorLoggingManager.CountError++;
            }

            Browser.Refresh();
            Find.GetElementById("Input_Year").Insert("eee");
            Find.GetElementById("update-profile-button").ToDoubleCLick();
            itemText = Find.GetElementById("Input_Year-error").GetInnerText();
            if (itemText != "Please enter a valid number.")
            {
                Logger.SaveError(itemText, "Выйдет сообщение: Please enter a valid number.");
                ErrorLoggingManager.CountError++;
            }
            #endregion Block_4

            #region Block_5

            string day = "29", month = "2", year = "1996";
            Find.GetElementById("Input_Day").Insert(day);
            Find.GetElementById("Input_Month").Insert(month);
            Find.GetElementById("Input_Year").Insert(year);
            //Find.GetElementById("update-profile-button").ToDoubleClick2();
            
            Browser.Refresh();

            if (day != Find.GetElementById("Input_Day").GetValue())
            {
                Logger.SaveError($"Не сохранился день: {day}", "День сохранится");
            }
            if (month != Find.GetElementById("Input_Month").GetValue())
            {
                Logger.SaveError($"Не сохранился месяц: {month}", "Месяц сохранится");
            }
            if (year != Find.GetElementById("Input_Year").GetValue())
            {
                Logger.SaveError($"Не сохранился год: {year}", "Год сохранится");
            }

            Find.GetElementById("Input_Year").Insert("1997");
            Find.GetElementById("update-profile-button").ToDoubleCLick();

            string messageInvalid = Find.GetElementByXPath("//*[@id='profile-form']/div[1]/ul/li").GetValue(); //TODO: Заменить на ID
            if (messageInvalid == string.Empty)
            {
                Logger.SaveError($"Не вышло уведомление: {year}", "Уведомление выйдет");
            }

            Browser.Url.GoTo(Browser, false);

            if (Convert.ToInt32(Find.GetElementById("Input_Year").GetValue()) == 1997)
            {
                Logger.SaveError("Год сохранился. Он не является високосным, но в сохраненной дате 29 дней", "Год не сохранится");
            }

            #endregion Block_5

            #region Block_6
            string caseOne = "-11";
                Find.GetElementById("Input_Day").Insert(caseOne);
                Find.GetElementById("update-profile-button").ToDoubleCLick();
                itemText = Find.GetElementById("Input_Day-error").GetInnerText();
                if (itemText != "Не может быть больше 31 и меньше 1")
                {
                    Logger.SaveError(
                        $"Не вышло сообщение о валидации при вводе: {caseOne}", "Выйдет сообщение: Не может быть больше 31 и меньше 1");
                    ErrorLoggingManager.CountError++;
                }

            Browser.Refresh();
                Find.GetElementById("Input_Month").Insert(caseOne);
                Find.GetElementById("update-profile-button").ToDoubleCLick();
                itemText = Find.GetElementById("Input_Month-error").GetInnerText();
                if (itemText != "Не может быть больше 12 и меньше 1")
                {
                    Logger.SaveError(
                        $"Не вышло сообщение о валидации при вводе: {caseOne}", "Выйдет сообщение: Не может быть больше 12 и меньше 1");
                    ErrorLoggingManager.CountError++;
                }

            Browser.Refresh();
            Find.GetElementById("Input_Year").Insert(caseOne);
                Find.GetElementById("update-profile-button").ToDoubleCLick();
                itemText = Find.GetElementById("Input_Year-error").GetInnerText();
                if (itemText != "Не может быть больше 2100 и меньше 1900")
                {
                    Logger.SaveError(
                        $"Не вышло сообщение о валидации при вводе: {caseOne}", "Выйдет сообщение: Не может быть больше 2100 и меньше 1900");
                    ErrorLoggingManager.CountError++;
                }

            string caseTwo = "11111";
            Find.GetElementById("Input_Day").Insert(caseTwo);
            Find.GetElementById("update-profile-button").ToDoubleCLick();
            itemText = Find.GetElementById("Input_Day-error").GetInnerText();
            if (itemText != "Не может быть больше 31 и меньше 1")
            {
                Logger.SaveError(
                    $"Не вышло сообщение о валидации при вводе: {caseTwo}", "Выйдет сообщение: Не может быть больше 31 и меньше 1");
                ErrorLoggingManager.CountError++;
            }

            Browser.Refresh();
            Find.GetElementById("Input_Month").Insert(caseTwo);
            Find.GetElementById("update-profile-button").ToDoubleCLick();
            itemText = Find.GetElementById("Input_Month-error").GetInnerText();
            if (itemText != "Не может быть больше 12 и меньше 1")
            {
                Logger.SaveError(
                    $"Не вышло сообщение о валидации при вводе: {caseTwo}", "Выйдет сообщение: Не может быть больше 12 и меньше 1");
                ErrorLoggingManager.CountError++;
            }

            Browser.Refresh();
            Find.GetElementById("Input_Year").Insert(caseTwo);
            Find.GetElementById("update-profile-button").ToDoubleCLick();
            itemText = Find.GetElementById("Input_Year-error").GetInnerText();
            if (itemText != "Не может быть больше 2100 и меньше 1900")
            {
                Logger.SaveError(
                    $"Не вышло сообщение о валидации при вводе: {caseTwo}", "Выйдет сообщение: Не может быть больше 2100 и меньше 1900");
                ErrorLoggingManager.CountError++;
            }

            #endregion Block_6

                Find.GetElementById("Input_Day").Insert("4");
                Find.GetElementById("Input_Month").Insert("9");
                Find.GetElementById("Input_Year").Insert("1996");
                Find.GetElementById("update-profile-button").ToDoubleCLick();
                Browser.Url.GoTo(Browser, false);

            if (Convert.ToInt32(Find.GetElementById("Input_Day").GetValue()) != 4)
            {
                Logger.SaveError("День '4' не сохранился", "День сохранится");
                ErrorLoggingManager.CountError++;
            }
            if (Convert.ToInt32(Find.GetElementById("Input_Month").GetValue()) != 9)
            {
                Logger.SaveError("Месяц '4' не сохранился", "Месяц сохранится");
                ErrorLoggingManager.CountError++;
            }
            if (Convert.ToInt32(Find.GetElementById("Input_Day").GetValue()) != 1996)
            {
                Logger.SaveError("Год '4' не сохранился", "Год сохранится");
                ErrorLoggingManager.CountError++;
            }

            Browser.Quit();
                LogError.Close();
                Assert.AreEqual(0, ErrorLoggingManager.CountError);
            //}
            //catch (Exception exeption)
            //{
            //    Browser.Quit();
            //    LogError.Close();

            //    LogExeption.WriteLine("");
            //    LogExeption.WriteLine(exeption);
            //    LogExeption.Close();

            //    Assert.AreEqual(0, ErrorLoggingManager.CountError);
            //}
        }

    }
}
