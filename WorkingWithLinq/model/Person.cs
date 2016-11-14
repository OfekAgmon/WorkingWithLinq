using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithLinq
{
    public class Person
    {
        public int Id { set; get; }
        public String Name { set; get; }
        public int Height { set; get; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}, Height: {2}", Id, Name, Height);
        }
    }
}
