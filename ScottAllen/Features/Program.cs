using System;
using System.Collections.Generic;
using System.Linq;

namespace Features
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    internal class Program
    {
        private static void Main()
        {
            Func<int, int> f1 = Square;
            Func<int, int> f = x => x * x;
            Func<int, int, int> add = (x, y) => x + y;

            Console.WriteLine(f(3));
            Console.WriteLine(add(7, 8));

            Action<int> write = x => Console.WriteLine(x);
            write(8);

            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee{ Id = 1, Name = "Scott" }, 
                new Employee{ Id = 2, Name = "Chris" }
            };

            //Query syntax:
            var dev = from developer in developers
                where developer.Name.StartsWith("S")
                orderby developer.Name
                select developer.Name;

               IEnumerable < Employee > sales = new List<Employee>
               {
                    new Employee {Id = 3, Name = "Alex"}
               };

            foreach (var employee in developers.Where(
                employee => employee.Name.StartsWith("S"))
            )
            {
                Console.WriteLine(employee.Name);
            }

            Console.WriteLine($"Count: {sales.Count()}");
            foreach (var person in developers)
            {
                Console.WriteLine($"{person.Name} - {person.Id}");
            }

            Console.WriteLine("***");

            IEnumerator<Employee> enumerator = developers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine($"{enumerator.Current?.Name} - {enumerator.Current?.Id}");
            }

            Console.ReadLine();
        }

        private static int Square(int arg)
        {
            throw new NotImplementedException();
        }

        private static bool NameStartsWithS(Employee employee)
        {
            return employee.Name.StartsWith("S");
        }
    }
}
