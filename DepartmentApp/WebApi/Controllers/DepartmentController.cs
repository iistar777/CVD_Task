using DAL.CommonAttributes;
using DAL.RequestAttributes;
using DAL.ResponseAttributes;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер по работе с лицевыми счетами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DepartmentController()
        {
        }

        /// <summary>
        /// Получить суммарную зарплату в разрезе департаментов (без руководителей и с руководителями)
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getsummarizedsalarybydepartmentlist")]
        public ActionResult GetSummarizedSalaryByDepartmentList(GetSummarizedSalaryByDepartmentListAttributesDTO attributes)
        {
            if (attributes == null)
            {
                return new BadRequestResult();
            }

            return new JsonResult(new GetSummarizedSalaryByDepartmentListDTO()
            {
                Salaries = new List<DepartmentSalaryAttributes>()
                {
                    new DepartmentSalaryAttributes() { DepartmentName = "A", DepartmentSalary = 100},
                    new DepartmentSalaryAttributes() { DepartmentName = "B", DepartmentSalary = 200 }
                }
            });
        }

        /// <summary>
        /// Получить департамент, в котором у сотрудника зарплата максимальна
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getdepartmentwithmaxsalary")]
        public ActionResult GetDepartmentWithMaxSalary()
        {
            return new JsonResult(new GetMaxDepartmentSalaryDTO()
            {
                DepartmentSalary = new DepartmentSalaryAttributes()
                {
                    DepartmentName = "A",
                    DepartmentSalary = 500
                }
            });
        }

        /// <summary>
        /// Получить зарплаты руководителей департаментов (по убыванию)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getchiefssalariesdesclist")]
        public ActionResult GetChiefsSalariesDescList()
        {
            return new JsonResult(new GetChiefsSalariesListDTO()
            {
                Salaries = new List<ChiefDepartmentSalaryAttributes>()
                {
                    new ChiefDepartmentSalaryAttributes() { ChiefName = "Ivan", DepartmentName = "A", DepartmentSalary = 300 },
                    new ChiefDepartmentSalaryAttributes() { ChiefName = "Petr", DepartmentName = "B", DepartmentSalary = 400 }
                }
            });
        }
    }
}
