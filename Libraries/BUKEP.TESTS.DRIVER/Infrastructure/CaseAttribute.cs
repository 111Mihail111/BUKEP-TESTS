using System;

namespace BUKEP.TESTS.DRIVER.Infrastructure
{
    /// <summary>
    /// Наименование кейса
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CaseAttribute : Attribute
    {
        public string Case { get; }

        public CaseAttribute(string name)
        {
            Case = name;
        }
    }
}
