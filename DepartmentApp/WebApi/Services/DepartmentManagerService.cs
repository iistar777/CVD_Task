using DAL.CommonAttributes;
using DAL.RequestAttributes;
using DAL.ResponseAttributes;
using Microsoft.Data.SqlClient;
using WebApi.Infrastructure;

namespace WebApi.Services
{
    /// <summary>
    /// Сервис для работы с департаментами
    /// </summary>
    public class DepartmentManagerService
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DepartmentManagerService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:testDB");
        }

        /// <summary>
        /// Получить суммарную зарплату в разрезе департаментов (без руководителей и с руководителями)
        /// </summary>
        /// <param name="attributes">Атрибуты для получения данных</param>
        /// <returns></returns>
        public async Task<GetSummarizedSalaryByDepartmentListDTO> GetSummarizedSalaryByDepartmentList(GetSummarizedSalaryByDepartmentListAttributesDTO attributes)
        {
            GetSummarizedSalaryByDepartmentListDTO result = new GetSummarizedSalaryByDepartmentListDTO();

            try
            {
                string sqlExpressionWithoutChiefs = @"select max(dep.name) as DepartmentName, sum(emp.salary) as DepartmentSalary
                                                      from employee emp
                                                      left join (select distinct em.chief_id as id, em.department_id as dep_id
                                                      from employee em
                                                      where em.chief_id is not null) chiefs
                                                      on emp.id = chiefs.id and emp.department_id = chiefs.dep_id
                                                      left join department dep
                                                      on emp.department_id = dep.id
                                                      where chiefs.id is null
                                                      group by emp.department_id
                                                      order by DepartmentName";

                string sqlExpressionWithChiefs = @"select max(dep.name) as DepartmentName, sum(em.salary) as DepartmentSalary
                                                   from (select distinct em.chief_id as id, em.department_id as dep_id
                                                   from employee em
                                                   where em.chief_id is not null
                                                   union select em1.id as id, em1.department_id as dep_id
                                                   from employee em1) emp
                                                   left join employee em
                                                   on emp.id = em.id
                                                   left join department dep
                                                   on emp.dep_id = dep.id
                                                   group by emp.dep_id
                                                   order by DepartmentName";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(attributes.IncludeChief ? sqlExpressionWithChiefs : sqlExpressionWithoutChiefs, connection);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            List<DepartmentSalaryAttributes> salaryList = new List<DepartmentSalaryAttributes>();

                            while (await reader.ReadAsync())
                            {
                                salaryList.Add(new DepartmentSalaryAttributes()
                                {
                                    DepartmentName = reader["DepartmentName"].ToString(),
                                    DepartmentSalary = int.Parse(reader["DepartmentSalary"].ToString())
                                });
                            }

                            result.Salaries = salaryList;
                        }

                        reader.Close();
                    }
                }

                result.RespInfo.AddResponseInformationParameters();
            }
            catch (Exception ex)
            {
                result = new GetSummarizedSalaryByDepartmentListDTO();
                result.RespInfo.AddResponseInfoParametersWithException(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Получить департамент, в котором у сотрудника зарплата максимальна
        /// </summary>
        /// <returns></returns>
        public async Task<GetMaxDepartmentSalaryDTO> GetDepartmentWithMaxSalary()
        {
            GetMaxDepartmentSalaryDTO result = new GetMaxDepartmentSalaryDTO();

            try
            {
                string sqlExpression = @"select top(1) dep.name as DepartmentName, em.salary as DepartmentSalary
                                         from employee em
                                         inner join department dep
                                         on em.department_id = dep.id
                                         where em.salary = (select max(salary) from employee)
                                         order by em.id asc";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            string departmentName = string.Empty;
                            int departmentSalary = 0;

                            while (await reader.ReadAsync())
                            {
                                departmentName = reader["DepartmentName"].ToString();
                                departmentSalary = int.Parse(reader["DepartmentSalary"].ToString());
                            }
                            
                            result.DepartmentSalary = new DepartmentSalaryAttributes()
                            {
                                DepartmentName = departmentName,
                                DepartmentSalary = departmentSalary
                            };
                        }

                        reader.Close();
                    }
                }

                result.RespInfo.AddResponseInformationParameters();
            }
            catch (Exception ex)
            {
                result = new GetMaxDepartmentSalaryDTO();
                result.RespInfo.AddResponseInfoParametersWithException(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Получить зарплаты руководителей департаментов (по убыванию)
        /// </summary>
        /// <returns></returns>
        public async Task<GetChiefsSalariesListDTO> GetChiefsSalariesDescList()
        {
            GetChiefsSalariesListDTO result = new GetChiefsSalariesListDTO();

            try
            {
                string sqlExpression = @"select em.name as ChiefName, dep.name as DepartmentName, em.salary as DepartmentSalary
                                         from employee em
                                         inner join (select distinct chief_id
                                         from employee) as chiefs
                                         on em.id = chiefs.chief_id
                                         left join department dep
                                         on em.department_id = dep.id
                                         order by DepartmentSalary desc";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            List<ChiefDepartmentSalaryAttributes> salaryList = new List<ChiefDepartmentSalaryAttributes>();

                            while (await reader.ReadAsync())
                            {
                                salaryList.Add(new ChiefDepartmentSalaryAttributes()
                                {
                                    ChiefName = reader["ChiefName"].ToString(),
                                    DepartmentName = reader["DepartmentName"].ToString(),
                                    DepartmentSalary = int.Parse(reader["DepartmentSalary"].ToString())
                                });
                            }

                            result.Salaries = salaryList;
                        }

                        reader.Close();
                    }
                }

                result.RespInfo.AddResponseInformationParameters();
            }
            catch (Exception ex)
            {
                result = new GetChiefsSalariesListDTO();
                result.RespInfo.AddResponseInfoParametersWithException(ex.Message);
            }

            return result;
        }
    }
}
