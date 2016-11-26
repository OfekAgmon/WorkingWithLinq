using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorkingWithLinq.model;

namespace WorkingWithLinq
{

    public class LinqObj
    {
        private IEnumerable<Person> persons;

        public LinqObj()
        {
            persons = new Person[]
            {
                new Person {Id = 1, Name= "ofek", Height=180 },
                new Person {Id = 2, Name= "nadav", Height=170 },
                new Person {Id = 3, Name= "aviv", Height=160 },
                new Person {Id = 4, Name= "imri", Height=190 }
            };
        }

        public void printByHeight()
        {
            Console.WriteLine("printByHeight");
            foreach (var person in persons.OrderBy(p => p.Height))
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine("*******");
        }

        public void printOnlyTopThreeAlpha()
        {
            Console.WriteLine("printOnlyTopThreeAlpha");
            IEnumerable<Person> query = persons.OrderBy(p => p.Name).Take(3);
            foreach (var person in query)
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine("*******");
        }

        public void printAbove175()
        {
            Console.WriteLine("printAbove175");
            IEnumerable<Person> query = persons.ofekBetterFilter(p => p.Height > 175);
            foreach (var person in query)
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine("*******");
        }

        public void showExtenstionsMethods()
        {
            Console.WriteLine("showExtenstionsMethods");
            string temp = "ofek";
            Console.WriteLine(temp.addA().addB().addC());
            Console.WriteLine(temp);
            Console.WriteLine("*******");
        }


        public List<Car> perocessCars(string path)
        {
            // Skip first line and take all lines that length in bigger than 1
            // Select and convert each line to object using Car.parseFromCsv
            return File.ReadAllLines(path).Skip(1).Where(l => l.Length > 1)
                .Select(Car.parseFromCsv).ToList();
        }

        public List<Manufacturer> processManufacturers(string path)
        {
            var qurey = File.ReadAllLines(path).Where(l => l.Length > 1)
                .Select(l =>
                {
                    var columns = l.Split(',');
                    return new Manufacturer
                    {
                        Name = columns[0],
                        Headquarters = columns[1],
                        Year = int.Parse(columns[2
                        ])
                    };
                });
            return qurey.ToList();
        }


        public void showTop10SortedMostEfficientBmwFrom2016(IEnumerable<Car> cars)
        {
            // get cars that have year 2016 and manufacturer BMW
            // order by the combined and then (secondary sort) by name 
            // (will sort group of cars with same 'combined'. take 10
            var query = cars.Where(c => c.Year == 2016 && c.Manufacturer == "BMW")
                .OrderByDescending(c => c.Combined).ThenBy(c => c.Name);

            // can also do .First(). will return the first Car in qurey.
            // this will return the first car of 2016 - First(c => c.Year == 2016)

            foreach (Car c in query.Take(10))
            {
                Console.WriteLine(string.Format("Name: {0}, Combined: {1}, Year: {2}", c.Name, c.Combined, c.Year));
            }
            Console.WriteLine("*******");
        }

        public void showAllPassengersFromTop3Cars(IEnumerable<Car> cars)
        {
            // select all the passengers in the top 3 cars after sort by name
            var query = cars.OrderBy(c => c.Name).Take(3).SelectMany(c => c.Passengers);
            foreach (var person in query)
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine("*******");
        }

        public void showMostEfficientManufacturersCounty(IEnumerable<Car> cars, IEnumerable<Manufacturer> manufacturers)
        {
            // join both cars and manufacturers 
            // where car's Manufacturer equal to manufacturer's Name
            // select new anonimus obj that's holding 
            //            manufacturer.Headquarters,
            //            car.Name,
            //            car.Combined
            var query = from car in cars
                        join manufacturer in manufacturers
                        on car.Manufacturer equals manufacturer.Name
                        orderby car.Combined descending
                        select new
                        {
                            manufacturer.Headquarters,
                            car.Name,
                            car.Combined
                        };

            foreach (var carSummary in query)
            {
                Console.WriteLine(carSummary.Headquarters + " " + carSummary.Name + " " + carSummary.Combined);
            }
            Console.WriteLine(query.ToList().Count);
            Console.WriteLine("*******");
        }

        public void groupByCarManufacturer(IEnumerable<Car> cars)
        {
            // group by manufacturer key
            // print for each groupBy sequence how many items it has
            var qurey = cars.GroupBy(c => c.Manufacturer);
            foreach (var result in qurey.Take(30))
            {
                Console.WriteLine($"{result.Key} has {result.Count()} cars");
            }
            Console.WriteLine("*******");
        }

        public void groupJoinCarsBynManufacturerCountry(IEnumerable<Car> cars, IEnumerable<Manufacturer> manufacturers)
        {
            // create grouping of cars by their matching manufacturer's country
            // so group by manufacturer.Name equals car.Manufacturer
            // take those car-groups and put into carGroup
            // project (select) into new object containing the Manufacturer object 
            // and its matching Cars group

            // print for each manufaturer its name and country
            // and its top 2 cars in its group
            var query = from manufacturer in manufacturers
                        join car in cars
                        on manufacturer.Name equals car.Manufacturer
                        into carGroup
                        select new
                        {
                            Manufacturer = manufacturer,
                            Cars = carGroup,

                        };

            foreach (var carsGroup in query)
            {
                Console.WriteLine($"{carsGroup.Manufacturer.Name}:{carsGroup.Manufacturer.Headquarters}");
                foreach (var car in carsGroup.Cars.Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
                Console.WriteLine("*******");
            }
        }

        public void showMinMaxAverageCarsPerManufacturer(IEnumerable<Car> cars)
        {
            var query = from car in cars
                        group car by car.Manufacturer
                into carGroup
                        select new
                        {
                            Name = carGroup.Key,
                            Max = carGroup.Max(c => c.Combined),
                            Min = carGroup.Min(c => c.Combined),
                            Avg = carGroup.Average(c => c.Combined)
                        };

            foreach (var carStatistics in query)
            {
                Console.WriteLine($"{carStatistics.Name}");
                Console.WriteLine($"\tmin: {carStatistics.Min}");
                Console.WriteLine($"\tmax: {carStatistics.Max}");
                Console.WriteLine($"\tavg: {carStatistics.Avg}");
            }
            Console.WriteLine("*******");
        }

        public void createCarsXml(IEnumerable<Car> cars)
        {
            var document = new XDocument();
            var carsElemt = new XElement("Cars",
                from car in cars
                select new XElement("Car",
                new XAttribute("Name", car.Name),
                new XAttribute("Combined", car.Combined),
                new XAttribute("Manufacturer", car.Manufacturer))
                );

            document.Add(carsElemt);
            document.Save("fuel.xml");

            //creates 

            //            <? xml version = "1.0" encoding = "utf-8" ?>
            //            < Cars >
            //                < Car Name = "4C" Combined = "28" Manufacturer = "ALFA ROMEO" />
            //                < Car Name = "V12 Vantage S" Combined = "14" Manufacturer = "Aston Martin Lagonda Ltd" />
            //                ...
            //            </ Cars >

        }

        public void qureyXmlFromFile()
        {
            var document = XDocument.Load("fuel.xml");
            var query =
                from elemnt in document.Element("Cars").Elements("Car")
                where elemnt.Attribute("Manufacturer").Value == "BMW"
                select elemnt.Attribute("Name").Value;

            foreach (var name in query)
            {
                Console.WriteLine("name: " + name);
            }
            Console.WriteLine("*******");
        }

    }
}
