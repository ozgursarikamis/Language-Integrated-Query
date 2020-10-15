using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    internal static class Program
    {
        private static void Main()
        {
            var cars = ProcessFile("fuel.csv");

            var query = from car in cars
                orderby car.Combined ascending, car.Name ascending
                select car;

            var query2 = cars
                .OrderByDescending(x => x.Combined)
                .ThenBy(x => x.Name)
                .Select(x => x)
                .FirstOrDefault(z => z.Manufacturer == "B,MW" && z.Year == 2016);

            var top = cars.Any(c => c.Manufacturer == "Ford");
            Console.WriteLine(top);
            Console.WriteLine();

            Console.WriteLine(query2?.Name);
            Console.WriteLine();

            //foreach (var car in query)
            //{
            //    Console.WriteLine($"{car.Name} : {car.Combined}");
            //}

            Console.ReadLine();
        }

        private static List<Car> ProcessFile(string path)
        {
            // Extension Method Syntax:
            //return File.ReadAllLines(path)
            //    .Skip(1)
            //    .Where(line => line.Length > 1)
            //    .Select(Car.ParseFromCsv)
            //    .ToList();

            // Linq Query Syntax:
            var query =
                from line in File.ReadAllLines(path).Skip(1)
                where line.Length > 1
                select Car.ParseFromCsv(line);

            return query.ToList();
        } 
    }
}