using System;
using System.Collections.Generic;

namespace Features
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    internal class Program
    {
        private static void Main(string[] args)
        {
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee{ Id = 1, Name = "Scott" }, 
                new Employee{ Id = 2, Name = "Chris" }
            };

            IEnumerable<Employee> sales = new List<Employee>
            {
                new Employee {Id = 3, Name = "Alex"}
            };

            foreach (var person in developers)
            {
                Console.WriteLine($"{person.Name} - {person.Id}");
            }

            Console.WriteLine("***");

            IEnumerator<Employee> enumerator = developers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine($"{enumerator.Current.Name} - {enumerator.Current.Id}");
            }
        }
    }
}
