using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttribute : Attribute
    {
        private string name;
        public double version;

        public CustomAttribute(string name, double version = 1.0)
        {
            this.name = name;
            this.version = version;
        }
    }
}
