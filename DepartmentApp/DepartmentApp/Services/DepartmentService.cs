using DAL.RequestAttributes;
using DAL.ResponseAttributes;
using DepartmentApp.Converters;
using DepartmentApp.Infrastructure;
using System.Threading.Tasks;

namespace DepartmentApp.Services
{
    /// <summary>
    /// Сервис для работы с департаментами
    /// </summary>
    public class DepartmentService
    {
        /// <summary>
		/// HTTP-клиент для отправки запросов на web API
		/// </summary>
		protected readonly NetClient _netClient;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="baseUrlAddress">Базовый адрес web API</param>
        public DepartmentService(string baseUrlAddress)
        {
            _netClient = new NetClient(baseUrlAddress);
        }

        /// <summary>
        /// Получить суммарную зарплату в разрезе департаментов (без руководителей и с руководителями)
        /// </summary>
        /// <param name="includeChief">Флаг включения/исключения руководителей департаментов в подсчет суммарной зарплаты</param>
        /// <returns></returns>
        public async Task<string> GetSummarizedSalaryByDepartmentList(bool includeChief = false)
        {
            GetSummarizedSalaryByDepartmentListAttributesDTO getListAttributes = new GetSummarizedSalaryByDepartmentListAttributesDTO()
            {
                IncludeChief = includeChief
            };

            GetSummarizedSalaryByDepartmentListDTO getListDTO = await _netClient.SendRequest<GetSummarizedSalaryByDepartmentListDTO>(getListAttributes, @"api/department/getsummarizedsalarybydepartmentlist");

            string result = DepartmentConverter.ConvertSummarizedDepartmentSalariesToString(getListDTO);

            return result;
        }

        /// <summary>
        /// Получить департамент, в котором у сотрудника зарплата максимальна
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetDepartmentWithMaxSalary()
        {
            GetMaxDepartmentSalaryDTO getDTO = await _netClient.SendRequest<GetMaxDepartmentSalaryDTO>(null, @"api/department/getdepartmentwithmaxsalary");

            string result = DepartmentConverter.ConvertMaxDepartmentSalaryToString(getDTO);

            return result;
        }

        /// <summary>
        /// Получить зарплаты руководителей департаментов (по убыванию)
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetChiefsSalariesDescList()
        {
            GetChiefsSalariesListDTO getListDTO = await _netClient.SendRequest<GetChiefsSalariesListDTO>(null, @"api/department/getchiefssalariesdesclist");

            string result = DepartmentConverter.ConvertChiefsSalariesToString(getListDTO);

            return result;
        }
    }
}
