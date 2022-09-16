namespace DAL.CommonAttributes
{
    /// <summary>
    /// Атрибуты для работы с зар. платами по департаменту
    /// </summary>
    public class DepartmentSalaryAttributes
    {
        /// <summary>
        /// Наименование департамента
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Значение заработной платы
        /// </summary>
        public int DepartmentSalary { get; set; }
    }
}
