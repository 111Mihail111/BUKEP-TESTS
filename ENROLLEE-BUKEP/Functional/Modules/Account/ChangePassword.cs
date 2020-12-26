using ASU_BUKEP_TESTS.Enums;
using BUKEP.TESTS.DRIVER.Enums;
using BUKEP.TESTS.DRIVER.Infrastructure;
using BUKEP.TESTS.DRIVER.Managers;
using ENROLLEE_BUKEP.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ENROLLEE_BUKEP.Modules.Account
{
    [TestClass]
    public class ChangePassword : Config
    {
        [TestInitialize]
        public void Initialize()
        {
            ConfigInit();
            //TODO: ПОЗЖЕ ВЫВЕСТИ НА ОСНОВНУЮ СРЕДУ
            ErrorLoggingManager.StandType = StandType.EnrolleeBukepTests;

            "http://iis-2:812/Identity/Account/Manage".GoTo(Browser);
            Browser.Manage().Window.Maximize();

            Find.GetElementById("Input_Email").Insert("test@bukep.ru");
            Find.GetElementById("Input_Password").Insert("1234567pP!");
            Find.GetElementById("logIn").ToClick();
        }

        [TestCategory("Аккаунт")]
        [Case("Тест кейс № 1")]
        [Description("Смена пароля от аккаунта")]
        [Priority((int)TestPriority.Average)]
        [TestMethod]
        public void PasswordСhange_ReturnErrors_0()
        {
            try
            {
                Find.GetElementById("change-password").ToClick();

                string newPassword = "2843jbkw*Ks--", oldPassword = "1234567pP!";
                Find.GetElementById("Input_OldPassword").Insert(oldPassword);
                Find.GetElementById("Input_NewPassword").Insert(newPassword);
                Find.GetElementById("Input_ConfirmPassword").Insert(newPassword);
                Find.GetElementById("update").ToClick();

                Find.GetElementById("logOut").ToClick();
                string url = "http://iis-2:812/Identity/Account/Login";
                url.GoTo(Browser);
                Find.GetElementById("Input_Email").Insert("test@bukep.ru");
                Find.GetElementById("Input_Password").Insert(newPassword);
                Find.GetElementById("logIn").ToClick();

                if (url == Browser.Url)
                {
                    Logger.SaveError("Пароль не поменялся.", "Пароль поменяется. Авторизация под ним пройдет успешно.");
                }

                "http://iis-2:812/Identity/Account/Manage/ChangePassword".GoTo(Browser);
                Find.GetElementById("Input_OldPassword").Insert(newPassword);
                Find.GetElementById("Input_NewPassword").Insert(oldPassword);
                Find.GetElementById("Input_ConfirmPassword").Insert(oldPassword);
                Find.GetElementById("update").ToClick();

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
    }
}
