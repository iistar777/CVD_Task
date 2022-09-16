using DAL.CommonAttributes;
using System.Collections.Generic;
using System.Linq;

namespace DAL.ResponseAttributes
{
    /// <summary>
    /// Структура данных для коллекции заработных плат руководителей департаментов
    /// </summary>
    public class GetChiefsSalariesListDTO : BaseResponseDTO
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public GetChiefsSalariesListDTO() : base()
        {
            Salaries = Enumerable.Empty<ChiefDepartmentSalaryAttributes>();
        }

        /// <summary>
        /// Коллекция заработных плат руководителей департаментов
        /// </summary>
        public IEnumerable<ChiefDepartmentSalaryAttributes> Salaries { get; set; }
    }
}
