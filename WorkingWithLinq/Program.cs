using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWithLinq.model;

namespace WorkingWithLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqObj linq = new LinqObj();
            linq.printByHeight();
            linq.printOnlyTopThreeAlpha();
            linq.printAbove175();
            linq.showExtenstionsMethods();
            IEnumerable<Car> cars = linq.perocessCars("fuel.csv");
            IEnumerable<Manufacturer> manufacturers = linq.processManufacturers("manufacturers.csv");
            linq.showTop10SortedMostEfficientBmwFrom2016(cars);
            linq.showAllPassengersFromTop3Cars(cars);
            linq.groupByCarManufacturer(cars);
            linq.groupJoinCarsBynManufacturerCountry(cars, manufacturers);
            linq.showMinMaxAverageCarsPerManufacturer(cars);
//            linq.showMostEfficientManufacturersCounty(cars, manufacturers);
<<<<<<< HEAD
            linq.createCarsXml(cars);
            linq.qureyXmlFromFile();
=======
            linq.createCarsXmlFile(cars);
            linq.qureyXml();
>>>>>>> 37fd9a3163045aac6bd563605f5ae3a940bc0db8
        }
    }
}
