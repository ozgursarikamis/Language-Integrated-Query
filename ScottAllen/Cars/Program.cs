﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    internal static class Program
    {
        private static void Main()
        {
            PetOwner[] owners = new[]
            {
                new PetOwner { Name = "Higa", Pets = new List<string>{ "Scruffy", "Sam" }},
                new PetOwner { Name="Ashkenazi",
                Pets = new List<string>{ "Walker", "Sugar" } },
                new PetOwner { Name="Price", Pets = new List<string>{ "Scratches", "Diesel" } },
                new PetOwner { Name="Hines", Pets = new List<string>{ "Dusty" } }
            };

            var petQuery = owners
                .SelectMany(
                    petOwner => petOwner.Pets, 
                    (petOwner, petName) => new {petOwner, petName})
                .Where(ownerAndPet => ownerAndPet.petName.StartsWith("S"))
                .Select(ownerAndPet => new
                {
                    Owner = ownerAndPet.petOwner.Name,
                    Pet = ownerAndPet.petName
                });

            foreach (var pet in petQuery)
            {
                Console.WriteLine($"Owner: {pet.Owner, 10} : Pet: {pet.Pet, 20}");
            }


            var cars = ProcessFile("fuel.csv");

            var query = from car in cars
                orderby car.Combined ascending, car.Name ascending
                select new
                {
                    Manifacturer = car.Manufacturer,
                    car.Name, car.Combined 
                };

            var result = query.SelectMany(x => x.Name);
            //foreach (var character in result)
            //{
            //    Console.WriteLine(character);
            //}

            //foreach (var car in query.Take(5))
            //{
            //    Console.WriteLine($"{car.Manifacturer} : {car.Combined}");
            //}

            Console.ReadLine();
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