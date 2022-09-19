using DAL.RequestAttributes;
using DAL.ResponseAttributes;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер по работе с департаментами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с департаментами
        /// </summary>
        private readonly DepartmentManagerService _departmentManagerService;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DepartmentController(IConfiguration configuration)
        {
            _departmentManagerService = new DepartmentManagerService(configuration);
        }

        /// <summary>
        /// Получить суммарную зарплату в разрезе департаментов (без руководителей и с руководителями)
        /// </summary>
        /// <param name="attributes">Атрибуты для получения данных</param>
        /// <returns></returns>
        [HttpPost]
        [Route("getsummarizedsalarybydepartmentlist")]
        public ActionResult GetSummarizedSalaryByDepartmentList(GetSummarizedSalaryByDepartmentListAttributesDTO attributes)
        {
            if (attributes == null)
            {
                return new BadRequestResult();
            }

            GetSummarizedSalaryByDepartmentListDTO dto = _departmentManagerService.GetSummarizedSalaryByDepartmentList(attributes).Result;

            return new JsonResult(dto);
        }

        /// <summary>
        /// Получить департамент, в котором у сотрудника зарплата максимальна
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getdepartmentwithmaxsalary")]
        public ActionResult GetDepartmentWithMaxSalary()
        {
            GetMaxDepartmentSalaryDTO dto = _departmentManagerService.GetDepartmentWithMaxSalary().Result;

            return new JsonResult(dto);
        }

        /// <summary>
        /// Получить зарплаты руководителей департаментов (по убыванию)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getchiefssalariesdesclist")]
        public ActionResult GetChiefsSalariesDescList()
        {
            GetChiefsSalariesListDTO dto = _departmentManagerService.GetChiefsSalariesDescList().Result;

            return new JsonResult(dto);
        }
    }
}
