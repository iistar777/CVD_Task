namespace DAL.CommonAttributes
{
    /// <summary>
    /// Атрибуты для работы с зар. платами руководителей департаментов
    /// </summary>
    public class ChiefDepartmentSalaryAttributes
    {
        /// <summary>
        /// Имя руководителя департамента
        /// </summary>
        public string ChiefName { get; set; }

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
