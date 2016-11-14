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
            IEnumerable<Car> cars = linq.processFile("fuel.csv");
            linq.showTop10SortedMostEfficientBmwFrom2016(cars);
            linq.showAllPassengersFromTop3Cars(cars);
        }
    }
}
