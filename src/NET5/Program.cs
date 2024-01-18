using NET5.Models;
using System;

namespace NET5
{
    public class Program
    {
        public static void Main()
        {
            ExperimentWithInitOnlyProperties();
            ExperimentWithRecords();
        }

        /// <summary>
        /// #1 Init-Only Properties
        /// </summary>
        private static void ExperimentWithInitOnlyProperties()
        {
            var myInitCar = new InitCar
            {
                Color = "green",
                CountDoors = 4
            };

            // CS8852: Init-only property or indexer can only be assigned in an object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor.
            //myInitCar.CountDoors = 5;

            Console.WriteLine(myInitCar.ToString());
        }

        /// <summary>
        /// #2 Records
        /// </summary>
        private static void ExperimentWithRecords()
        {
            var myRecordPerson = new RecordPerson("Test", "Testov");

            // CS0200: Property or indexer cannot be assigned to -- it is read only.
            //myRec.FirstName = "No"; 

            Console.WriteLine(myRecordPerson.FirstName);
        }
    }
}

// #3 Top-level statements
// Program.cs could look like this:

// using System;
// 
// string message = "Top-level statements remove unnecessary ceremony from many applications.\n" +
//     "There’s only one line of code that does anything. With top-level statements, \n" +
//     "you can replace all that boilerplate with the using statement and the single line that does the work.";
// 
// Console.WriteLine(message);