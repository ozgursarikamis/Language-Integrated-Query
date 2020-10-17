﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using System.Xml.Linq;

namespace Cars
{
    internal static class Program
    {
        private static void Main()
        {
            Func<int, int> square = x => x * x;
            Expression<Func<int, int, int>> add = (x, y) => x + y;
            Func<int, int, int> addI = add.Compile();
            Console.WriteLine(addI(3, 5));

            var result = add.Compile()(3, 5);
            Console.WriteLine(result);

            Console.ReadLine();
        }

        private static void QueryXml()
        {
            var nameSpace = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";

            var document = XDocument.Load("fuel.xml");
            var query = from element in document.Elements(nameSpace + "Cars").Elements(ex + "Car") ?? Enumerable.Empty<XElement>()
                where element.Attribute("Manufacturer")?.Value == "BMW"
                select element.Attribute("Name")?.Value;

            foreach (var item in query)
            {
                Console.WriteLine($"{item,5}");
            }
        }

        private static void CreateXml()
        {
            var records = ProcessFile("fuel.csv");

            var nameSpace = (XNamespace) "http://pluralsight.com/cars/2016";
            var ex = (XNamespace) "http://pluralsight.com/cars/2016/ex";

            var document = new XDocument();
            var cars = new XElement(nameSpace + "Cars",
                from record in records
                select new XElement(ex + "Car",
                    new XAttribute("Name", record.Name),
                    new XAttribute("Combined", record.Combined),
                    new XAttribute("Manufacturer", record.Manufacturer)
                ));

            cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));

            foreach (var record in records)
            {
                var car = new XElement("Car",
                    new XAttribute("Name", record.Name),
                    new XAttribute("Combined", record.Combined),
                    new XAttribute("Manufacturer", record.Manufacturer)
                );

                cars.Add(car);
            }

            document.Add(cars);
            document.Save("fuel.xml");
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(l => l.Length > 1)
                    .Select(x =>
                    {
                        var columns = x.Split(',');
                        return new Manufacturer
                        {
                            Name = columns[0],
                            Headquarters = columns[1],
                            Year = int.Parse(columns[2])
                        };
                    });

            return query.ToList();
        }

        private static List<Car> ProcessFile(string path)
        {
            //Extension Method Syntax:
            return File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .ToCar()
                .ToList();

            // Linq Query Syntax:
            //var query =
            //    from line in File.ReadAllLines(path).Skip(1)
            //    where line.Length > 1
            //    select Car.ParseFromCsv(line);

            //return query.ToList();
        } 
    }

    public class CarStatistics
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public double Average { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }

        public CarStatistics()
        {
            Max = int.MinValue;
            Min = int.MaxValue;
        }
        public CarStatistics Accumulate(Car car)
        {
            Count += 1;
            Total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);
            return this;
        }

        public CarStatistics Compute()
        {
            Average = Total / Count;
            return this;
        }
    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            { 
                var columns = line.Split(',');
                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}