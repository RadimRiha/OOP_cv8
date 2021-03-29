using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_cv8
{
    class Program
    {
        static void Main(string[] args)
        {
            TemperatureArchive archive = new TemperatureArchive();
            Console.WriteLine("Loading file...");
            Console.WriteLine();
            try
            {
                archive.Load(@"archiveInput.txt");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return;
            }
            Console.WriteLine("Loaded file:");
            archive.PrintTemperatures();
            Console.WriteLine();
            Console.WriteLine("Average temperatures for all years:");
            archive.PrintAverageAnnualTemperatures();
            Console.WriteLine();
            Console.WriteLine("Average temperatures in January:");
            archive.PrintAverageMonthlyTemperatures(1);
            archive.Calibration(-0.1);
            Console.WriteLine();
            Console.WriteLine("Temperatures after calibration of -0.1:");
            archive.PrintTemperatures();
            Console.WriteLine();
            Console.WriteLine("Saving file...");
            Console.WriteLine();
            try
            {
                archive.Save(@"D:\archiveOutput.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
        }
    }
}
