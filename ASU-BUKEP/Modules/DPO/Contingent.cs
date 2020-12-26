using ASU_BUKEP.Extensions;
using ASU_BUKEP_TESTS.Enums;
using BUKEP.TESTS.DRIVER.Enums;
using BUKEP.TESTS.DRIVER.Infrastructure;
using BUKEP.TESTS.DRIVER.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Linq;
using System.Threading;

namespace ASU_BUKEP.Modules.DPO
{
    [TestClass]
    public class Contingent : Config
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Init();
            ErrorLoggingManager.StandType = StandType.AsuBukepTests;

            Browser.Manage().Window.Maximize();
            //"http://iis-2:803/DPO/DpoKont.aspx".GoTo(Browser);
            chromeDriver.Navigate().GoToUrl("http://iis-2:803/Dec/DecStudEdit.aspx?a=0&t=dec&k=54187");
            
            //Find.GetElementById("tbUserName").Insert("PolovinkinMV", false);
            //Find.GetElementById("tbPassword").Insert("123qwerzxcv", false);
            //Find.GetElementById("lbLogin").ToClick(false);
        }

        /// <summary>
        /// Выбрать подразделение ДПО в структурном дереве
        /// </summary>
        protected void SelectPodrWithStructTree()
        {
            Find.GetElementById("ctl00_ctl00_ParentContent_structFiltern1").ExpandStructTreeNode();
            Find.GetElementById("ctl00_ctl00_ParentContent_structFiltern11").ExpandStructTreeNode();
            Find.GetElementById("ctl00_ctl00_ParentContent_structFiltert25").Select();
        }

        protected void CheckingFiltering(int count)
        {
            for (int i = 2; i <= count; i++)
            {
                string studentName =
                    Find.GetElementByXPath($"//*[@id='ctl00_ctl00_ParentContent_Content_gvDec']/tbody/tr[{i}]/td[3]").Text;
                if (studentName != "Нешвеев Виталий Владимирович")
                {
                    ErrorLoggingManager.CountError++;
                    Logger.SaveError($"Неверная фильтрация. Вышел некорректный слушатель: {studentName}",
                        "Корректная фильтрация слушателей по ФИО");
                }
            }
        }

        [TestCategory("ДПО")]
        [Case("Тест кейс № 1")]
        [Description("Добавление слушателя на факультет ДПО")]
        [Priority((int)TestPriority.Average)]
        [TestMethod]
        public void Iis2_AddStudentOnFacultyDPO_ReturnErrors_0()
        {
            try
            {
                SelectPodrWithStructTree();

                Find.GetElementById("ctl00_ctl00_ParentContent_Content_TextBoxFio").Insert("Нешвеев Виталий Владимирович");
                Find.GetElementById("ctl00_ctl00_ParentContent_Content_ButtonSearchStud").ToClick();

                string xPath = "//*[@id='ctl00_ctl00_ParentContent_Content_gvDec']/tbody/tr[*]";
                int count = Find.GetCountElementsByXPath(xPath);
                CheckingFiltering(count);

                Find.GetElementByXPath("//*[@id='ctl00_ctl00_ParentContent_Content_gvDec']/tbody/tr[2]").Select();
                Find.GetElementById("ctl00_ctl00_ParentContent_treeLabel").ToClick(false);

                Thread.Sleep(300);
                Find.GetElementById("ctl00_ctl00_ParentContent_menuLabel").ToUncover();

                Find.GetElementById("ctl00_ctl00_ParentContent_ContentLeftMenu_lbNewStud").ToClick();
                Frame.ScrollPositionSetting("mSpec", 100);

                string labelModalWindow = Find.GetElementById("ctl00_ctl00_ParentContent_Content_lAddStudByGroup").Text;
                if (labelModalWindow != "Выбор специальности")
                {
                    ErrorLoggingManager.CountError++;
                    Logger.SaveError($"Открылось неверное модал. окно: {labelModalWindow}",
                        "Откроется модал. окно: Выбор специальности");
                }

                Thread.Sleep(200);
                Find.GetElementById("ctl00_ctl00_ParentContent_Content_lbNext").ToClick();
                Thread.Sleep(550);

                string labelFrame = Find.GetElementByXPath("//*[@id='aspnetForm']/div[6]/div[2]/div[1]/span").Text;
                if (labelFrame != "Личное дело :")
                {
                    ErrorLoggingManager.CountError++;
                    Logger.SaveError($"Открылся неверный фрейм: {labelModalWindow}",
                        "Откроется фрейм: Личное дело :");
                }

                Frame.SwitchToFrame();
                Find.GetElementById("PersonView_tbSearchFio").Insert("Нешвеев Виталий Владимирович");
                Find.GetElementById("PersonView_lbFind").ToClick();

                Find.GetElementByXPath("//*[@id='PersonView_gvPerson']/tbody/tr[2]/td[1]/a").ToClick();
                Frame.ScrollPositionSetting("ctl01", 300);
                Find.GetElementById("PersonView_btnSave").ToClick();

                Thread.Sleep(1000);
                Frame.SwitchToDefaultContent();
                Frame.CloseFrame();
                Find.GetElementByXPath(
                    "//*[@id='ctl00_ctl00_ParentContent_Content_UpdatePanel4']//button[@class='btn btn-default btn-sm btn-block']").ToClick();
                Thread.Sleep(1000);

                Find.GetElementById("ctl00_ctl00_ParentContent_Content_ButtonSearchStud").ToClick();
                if (!(Find.GetCountElementsByXPath(xPath) > count))
                {
                    ErrorLoggingManager.CountError++;
                    Logger.SaveError("Студент не создался", "Студент должен создаться");
                }

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

        [TestCategory("ДПО")]
        [Case("Тест кейс № 2")]
        [Description("Добавление слушателя на головной ВУЗ")]
        [Priority((int)TestPriority.Average)]
        [TestMethod]
        public void Iis2_AddStudentOnHeadUniversity_ReturnErrors_0()
        {
            try
            {
                Find.GetElementById("ctl00_ctl00_ParentContent_menuLabel").ToUncover();
                Thread.Sleep(300);
                Find.GetElementById("ctl00_ctl00_ParentContent_ContentLeftMenu_lbNewStud").ToClick();
                Thread.Sleep(200);
                Find.GetElementById("ctl00_ctl00_ParentContent_Content_lbNext").ToClick();

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

        [TestCategory("ДПО")]
        [Case("Тест кейс № 3")]
        [Description("Удаление студента в группе")]
        [Priority((int)TestPriority.Average)]
        [TestMethod]
        public void Iis2_DeleteStudentInGroup_ReturnErrors_0()
        {
            try
            {
                SelectPodrWithStructTree();

                Find.GetElementById("ctl00_ctl00_ParentContent_Content_TextBoxFio").Insert("Нешвеев Виталий Владимирович");
                Find.GetElementById("ctl00_ctl00_ParentContent_Content_ButtonSearchStud").ToClick();

                string xPath = "//*[@id='ctl00_ctl00_ParentContent_Content_gvDec']/tbody/tr[*]";
                int count = Find.GetCountElementsByXPath(xPath);
                CheckingFiltering(count);

                Find.GetElementByXPath($"//*[@id='ctl00_ctl00_ParentContent_Content_gvDec']/tbody/tr[{count}]").Select();
                Find.GetElementById("ctl00_ctl00_ParentContent_treeLabel").ToClick(false);
                Thread.Sleep(300);
                Find.GetElementById("ctl00_ctl00_ParentContent_menuLabel").ToUncover();
                Thread.Sleep(300);
                Find.GetElementById("ctl00_ctl00_ParentContent_ContentLeftMenu_lbDeleteStud").ToClick();

                JSManager.AlertToDismiss();
                Thread.Sleep(500);
                Find.GetElementById("ctl00_ctl00_ParentContent_Content_ButtonSearchStud").ToClick();
                if (Find.GetCountElementsByXPath(xPath) < count)
                {
                    ErrorLoggingManager.CountError++;
                    Logger.SaveError("Запись удалилась", "Запись не удалится при нажатии на кнопку 'Отмена'");
                }

                Find.GetElementByXPath($"//*[@id='ctl00_ctl00_ParentContent_Content_gvDec']/tbody/tr[{count}]").Select();
                Thread.Sleep(300);
                Find.GetElementById("ctl00_ctl00_ParentContent_ContentLeftMenu_lbDeleteStud").ToClick();
                JSManager.AlertToAccept();
                Find.GetElementById("ctl00_ctl00_ParentContent_Content_ButtonSearchStud").ToClick();

                if (Find.GetCountElementsByXPath(xPath) == count)
                {
                    ErrorLoggingManager.CountError++;
                    Logger.SaveError("Запись не удалилась", "Запись удалится при нажатии на кнопку 'Ок'");
                }

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


        [TestMethod]
        public void tests()
        {
            //Frame.SwitchToFrame();
            //Find.GetElementById("btnSave").ToDoubleClick2(Browser);
            chromeDriver.SwitchTo().Frame(0);

            string jsCode = "var evObj = new MouseEvent('dblclick', {bubbles: true, cancelable: true, view: window});";
            jsCode += " arguments[0].dispatchEvent(evObj);";
            chromeDriver.ExecuteScript(jsCode, chromeDriver.FindElement(By.Id("btnSave")));
        }
    }
}
