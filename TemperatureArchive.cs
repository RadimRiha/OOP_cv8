using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
                outputFile.WriteLine(String.Join(";", yearEntry.MonthlyTemperatures));
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
                _archive.Add(year, new AnnualTemperature(year, inputData[1].Split(';').Select(double.Parse).ToList()));
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
                Console.WriteLine(yearEntry.Year + ":\t" + string.Join("\t", yearEntry.MonthlyTemperatures.Select(s => string.Format("{0,5:F1}", s))));
            }
        }
        public void PrintAverageAnnualTemperatures()
        {
            foreach (AnnualTemperature yearEntry in _archive.Values)
            {
                Console.WriteLine("{0}:\t{1:F1}", yearEntry.Year, yearEntry.AverageAnnualTemperature);
            }
        }
        public void PrintAverageMonthlyTemperatures(int month)
        {
            if (month < 1 || month > 12) throw new Exception("Month outside of range <1:12>");
            foreach (AnnualTemperature yearEntry in _archive.Values)
            {
                Console.WriteLine("{0}:\t{1:F1}", yearEntry.Year, yearEntry.MonthlyTemperatures[month - 1]);
            }
        }
    }
}
