namespace DAL.RequestAttributes
{
    /// <summary>
    /// Атрибуты для получения суммарных заработных плат в разрезе департаментов
    /// </summary>
    public class GetSummarizedSalaryByDepartmentListAttributesDTO
    {
        /// <summary>
        /// Флаг включения/исключения руководителей департаментов в подсчет суммарной зарплаты
        /// </summary>
        public bool IncludeChief { get; set; }
    }
}
