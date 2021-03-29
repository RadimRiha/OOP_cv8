using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_cv8
{
    public class AnnualTemperature
    {
        public int Year;
        public List<double> MonthlyTemperatures = new List<double> { };

        public AnnualTemperature(int year, List<double> monthlyTemperatures)
        {
            if (year < 0) throw new Exception("You are not a time traveller");
            if (monthlyTemperatures.Count != 12) throw new Exception("There must be 12 temperature entries in a year");
            Year = year;
            MonthlyTemperatures = monthlyTemperatures;
        }
        public double MaxTemperature
        {
            get
            {
                return MonthlyTemperatures.Max();
            }
        }
        public double MinTemperature
        {
            get
            {
                return MonthlyTemperatures.Min();
            }
        }
        public double AverageAnnualTemperature
        {
            get
            {
                return MonthlyTemperatures.Average();
            }
        }
    }
}
