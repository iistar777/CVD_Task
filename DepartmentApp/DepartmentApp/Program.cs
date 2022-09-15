using DepartmentApp.Services;
using System;

namespace DepartmentApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DepartmentService departmentService = new DepartmentService();

            string action = string.Empty;

            while (action != "exit")
            {
                Console.WriteLine("Введите номер действия, которое хотите выполнить:");
                Console.WriteLine("1. Получить суммарную зарплату в разрезе департаментов (без руководителей и с руководителями).");
                Console.WriteLine("2. Получить департамент, в котором у сотрудника зарплата максимальна.");
                Console.WriteLine("3. Получить зарплаты руководителей департаментов (по убыванию).");
                Console.WriteLine("Для выхода из приложения введите Exit.");

                action = Console.ReadLine().ToLower();

                switch(action)
                {
                    case "1":
                        Console.WriteLine("Включать зарплаты руководителей в расчет? (y/n)");
                        string answer = Console.ReadLine().ToLower();

                        while (answer != "y" && answer != "n")
                        {
                            Console.WriteLine("Введено некорректное значение. Введите Y или N.");
                            answer = Console.ReadLine().ToLower();
                        }

                        Console.WriteLine(departmentService.GetSummarizedSalaryByDepartmentList(answer == "y"));
                        break;
                    case "2":
                        Console.WriteLine(departmentService.GetDepartmentWithMaxSalary());
                        break;
                    case "3":
                        Console.WriteLine(departmentService.GetChiefsSalariesDescList());
                        break;
                    case "exit":
                        break;
                    default:
                        Console.WriteLine("Введено некорректное значение!");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
