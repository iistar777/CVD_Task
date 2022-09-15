namespace DepartmentApp.Services
{
    /// <summary>
    /// Сервис для работы с департаментами
    /// </summary>
    public class DepartmentService
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DepartmentService()
        {

        }

        /// <summary>
        /// Получить суммарную зарплату в разрезе департаментов (без руководителей и с руководителями)
        /// </summary>
        /// <param name="includeChief">Флаг включения/исключения руководителей департаментов в подсчет суммарной зарплаты</param>
        /// <returns></returns>
        public string GetSummarizedSalaryByDepartmentList(bool includeChief = false)
        {
            return null;//List<DepartmentSalaryAttributes>
        }

        /// <summary>
        /// Получить департамент, в котором у сотрудника зарплата максимальна
        /// </summary>
        /// <returns></returns>
        public string GetDepartmentWithMaxSalary()
        {
            return null;//DepartmentSalaryAttributes
        }

        /// <summary>
        /// Получить зарплаты руководителей департаментов (по убыванию)
        /// </summary>
        /// <returns></returns>
        public string GetChiefsSalariesDescList()
        {
            return null;//List<ChiefDepartmentSalaryAttributes>
        }
    }
}
