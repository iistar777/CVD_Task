using DAL.CommonAttributes;

namespace DAL.ResponseAttributes
{
    /// <summary>
    /// Ответ на вопрос о загрузке максимальной заработной платы по всем департаментам
    /// </summary>
    public class GetMaxDepartmentSalaryDTO : BaseResponseDTO
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public GetMaxDepartmentSalaryDTO() : base()
        {
            DepartmentSalary = new DepartmentSalaryAttributes();
        }

        /// <summary>
        /// Структура с информацией по заработной плате в департаменте
        /// </summary>
        public DepartmentSalaryAttributes DepartmentSalary { get; set; }
    }
}
