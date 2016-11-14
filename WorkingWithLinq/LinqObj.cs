using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public List<Car> processFile(string path)
        {
            return File.ReadAllLines(path).Skip(1).Where(l => l.Length > 1)
                .Select(Car.parseFromCsv).ToList();
        }


        public void showTop10SortedMostEfficientBmwFrom2016(IEnumerable<Car> cars)
        {
            var query = cars.Where(c => c.Year == 2016 && c.Manufacturer == "BMW")
                .OrderByDescending(c => c.Combined).ThenBy(c => c.Name);
            foreach(Car c in query.Take(10))
            {
                Console.WriteLine(string.Format("Name: {0}, Combined: {1}, Year: {2}", c.Name, c.Combined, c.Year));
            }
            Console.WriteLine("*******");
        }

        public void showAllPassengersFromTop3Cars(IEnumerable<Car> cars)
        {
            var query = cars.OrderBy(c => c.Name).Take(3).SelectMany(c => c.Passengers);
            foreach (var person in query)
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine("*******");
        }

    }
}
