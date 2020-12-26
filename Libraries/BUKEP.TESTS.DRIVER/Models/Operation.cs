using BUKEP.TESTS.DRIVER.Enums;

namespace BUKEP.TESTS.DRIVER.Models
{
    /// <summary>
    /// Действие в браузере
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Номер действия
        /// </summary>
        public int NumberAction { get; set; }

        /// <summary>
        /// Тэг элемента
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Название элемента
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        /// Текст вводимый в элемент
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Тип операции
        /// </summary>
        public TypeOperation TypeOperation { get; set; }

    }
}
