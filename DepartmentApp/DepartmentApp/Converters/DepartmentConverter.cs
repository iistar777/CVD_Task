﻿using DAL.CommonAttributes;
using DAL.ResponseAttributes;
using System.Linq;
using System.Text;

namespace DepartmentApp.Converters
{
    /// <summary>
    /// Конвертер информации из БД в строковое представление
    /// </summary>
    public static class DepartmentConverter
    {
        /// <summary>
        /// Преобразует суммарные заработные платы в разрезе департаментов в строковое представление
        /// </summary>
        /// <param name="dto">Структура данных, содержащая коллекцию суммарных заработных плат в разрезе департаментов</param>
        /// <returns></returns>
        public static string ConvertSummarizedDepartmentSalariesToString(GetSummarizedSalaryByDepartmentListDTO dto)
        {
            StringBuilder builder = new StringBuilder();

            if (dto.Salaries.Any())
            {
                foreach(DepartmentSalaryAttributes salary in dto.Salaries)
                {
                    builder.Append($"Департамент \"{salary.DepartmentName}\": суммарная з/п составляет {salary.DepartmentSalary} рублей\n");
                }
            }
            else
            {
                builder.Append("Информация не найдена\n");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Преобразует информацию по департаменту с максимальной заработной платой в строковое представление
        /// </summary>
        /// <param name="dto">Структура данных, содержащая информацию по максимальной заработной плате по всем департаментам</param>
        /// <returns></returns>
        public static string ConvertMaxDepartmentSalaryToString(GetMaxDepartmentSalaryDTO dto)
        {
            StringBuilder builder = new StringBuilder();

            if (dto.DepartmentSalary != null)
            {
                builder.Append($"Максимальная з/п среди департаментов составляет {dto.DepartmentSalary.DepartmentSalary} рублей (Департамент \"{dto.DepartmentSalary.DepartmentName}\")\n");
            }
            else
            {
                builder.Append("Информация не найдена\n");
            }

            return builder.ToString();
        }
        /// <summary>
        /// Преобразует информацию по заработным платам руководителей департаментов в строковое представление
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static string ConvertChiefsSalariesToString(GetChiefsSalariesListDTO dto)
        {
            StringBuilder builder = new StringBuilder();

            if (dto.Salaries.Any())
            {
                foreach (ChiefDepartmentSalaryAttributes salary in dto.Salaries)
                {
                    builder.Append($"Руководитель: {salary.ChiefName} - з/п составляет {salary.DepartmentSalary} рублей (Департамент \"{salary.DepartmentName}\")\n");
                }
            }
            else
            {
                builder.Append("Информация не найдена\n");
            }

            return builder.ToString();
        }
    }
}
