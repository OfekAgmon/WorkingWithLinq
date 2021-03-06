﻿using System;
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
            linq.createCarsXmlFile(cars);
            linq.qureyXml();



            // newest change on master!

            // new chnage on master!

            // newest chnage on branch!

        }
    }
}
