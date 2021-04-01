using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace OOP_cv8
{
    public class TemperatureArchive
    {
        private SortedDictionary<int, AnnualTemperature> _archive = new SortedDictionary<int, AnnualTemperature> { };
        public void Save(string filePath)
        {
            StreamWriter outputFile = File.CreateText(filePath);
            foreach (AnnualTemperature yearEntry in _archive.Values)
            {
                outputFile.Write(yearEntry.Year);
                outputFile.Write(":");
                outputFile.WriteLine(String.Join(";", yearEntry.MonthlyTemperatures.Select(s => s.ToString(CultureInfo.InvariantCulture))));
            }
            outputFile.Close();
        }
        public void Load(string filePath)
        {
            StreamReader inputFile = File.OpenText(filePath);
            string line = null;
            string[] inputData;
            int year;
            while ((line = inputFile.ReadLine()) != null)
            {
                inputData = line.Split(':');   //[0] - year, [1] - monthly temperatures
                if (inputData.Length != 2) throw new Exception("Wrong data in file");
                year = Int16.Parse(inputData[0]);
                if (_archive.ContainsKey(year)) throw new Exception("Year contained multiple times in file");
                _archive.Add(year, new AnnualTemperature(year, inputData[1].Split(';').Select(s => Double.Parse(s, CultureInfo.InvariantCulture)).ToList()));
            }
            inputFile.Close();
        }
        public void Calibration(double calConstant)
        {
            foreach (AnnualTemperature yearEntry in _archive.Values)
            {
                yearEntry.MonthlyTemperatures = yearEntry.MonthlyTemperatures.Select(s => s + calConstant).ToList();
            }
        }
        public AnnualTemperature Find(int year)
        {
            if (!_archive.ContainsKey(year)) throw new Exception("This entry does not exist");
            return _archive[year];
        }
        public void PrintTemperatures()
        {
            foreach (AnnualTemperature yearEntry in _archive.Values)
            {
                Console.WriteLine(yearEntry.Year + ":" + String.Join(" ", yearEntry.MonthlyTemperatures.Select(s => String.Format("{0,6:F1}", s))));
            }
        }
        public void PrintAverageAnnualTemperatures()
        {
            foreach (AnnualTemperature yearEntry in _archive.Values)
            {
                Console.WriteLine("{0}: {1,6:F1}", yearEntry.Year, yearEntry.AverageAnnualTemperature);
            }
        }
        public void PrintAverageMonthlyTemperatures()
        {
            for (int month = 1; month <= 12; month++)
            {
                double sum = 0;
                foreach (AnnualTemperature yearEntry in _archive.Values)
                {
                    sum += yearEntry.MonthlyTemperatures[month - 1];
                }
                Console.Write(" {0,6:F1}", sum / _archive.Count());
            }
            Console.WriteLine();
        }
    }
}
