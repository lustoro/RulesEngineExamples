using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEnginePerformanceExample
{
    public class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public float Salary { get; set; }

        public string ToString()
        {
            return $"{{Name: \"{Name}\", Age:{Age}, Salary:{Salary}}}";
        }
    }
}
