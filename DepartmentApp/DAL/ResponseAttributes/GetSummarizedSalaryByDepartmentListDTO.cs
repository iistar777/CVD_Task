using DAL.CommonAttributes;
using System.Collections.Generic;
using System.Linq;

namespace DAL.ResponseAttributes
{
    /// <summary>
    /// Структура данных для коллекции суммарных заработных плат в разрезе департаментов
    /// </summary>
    public class GetSummarizedSalaryByDepartmentListDTO : BaseResponseDTO
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public GetSummarizedSalaryByDepartmentListDTO() : base()
        {
            Salaries = Enumerable.Empty<DepartmentSalaryAttributes>();
        }

        /// <summary>
        /// Коллекция суммарных заработных плат в разрезе департаментов
        /// </summary>
        public IEnumerable<DepartmentSalaryAttributes> Salaries { get; set; }
    }
}
