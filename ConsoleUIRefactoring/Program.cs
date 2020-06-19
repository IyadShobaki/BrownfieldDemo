using BrownfieldLibrary;
using BrownfieldLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUIRefactoring
{
    class Program
    {
        static void Main(string[] args)
        {

            List<TimeSheetEntryModel> timeSheets = LoadTimeSheets();
            List<CustomerModel> customers = DataAccess.GetCustomers();
            EmployeeModel currentEmployee = DataAccess.GetCurrentEmployee();

            //Linq expression
            customers.ForEach(x => BillCustomer(timeSheets, x));

            PayEmployee(timeSheets, currentEmployee);

            Console.WriteLine();
            Console.WriteLine("Press any key to exit application...");
            Console.ReadKey();
        }
        private static void PayEmployee(List<TimeSheetEntryModel> timeSheets, EmployeeModel employee)
        {
            decimal totalPay = TimeSheetProcessor.CalculateEmployeePay(timeSheets, employee);
            Console.WriteLine($"You will get paid ${ totalPay } for your time.");
            Console.WriteLine();
     
        }
        private static void BillCustomer(List<TimeSheetEntryModel> timeSheets,CustomerModel customer)
        {

            double totalHours = TimeSheetProcessor.GetHoursWorksForCompany(timeSheets, customer.CustomerName);

            Console.WriteLine($"Simulating Sending email to { customer.CustomerName }");
            Console.WriteLine("Your bill is $" + (decimal) totalHours * customer.HourlyRateToBill + " for the hours worked.");
            Console.WriteLine();
        
       }

        private static List<TimeSheetEntryModel> LoadTimeSheets()
        {
            List<TimeSheetEntryModel> output = new List<TimeSheetEntryModel>();
            string enterMoreTimeSheet = "";

            do
            {
                Console.WriteLine("Enter what you did: ");
                string workDone = Console.ReadLine();
                Console.WriteLine("How long did you do it for: ");
                string rawTimeWorked = Console.ReadLine();
                double hoursWorked;

                while (double.TryParse(rawTimeWorked, out hoursWorked) == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid number given");
                    Console.WriteLine("How long did you do it for: ");
                    rawTimeWorked = Console.ReadLine();
                }

                TimeSheetEntryModel timesheet = new TimeSheetEntryModel();
                timesheet.HoursWorked = hoursWorked;
                timesheet.WorkDone = workDone;
                output.Add(timesheet);

                Console.WriteLine("Do you want to enter more time (yes/no): ");
                enterMoreTimeSheet = Console.ReadLine();


            } while (enterMoreTimeSheet.ToLower() == "yes");

            Console.WriteLine();

            return output;
        }
     
    }
}


//Old code without refactoring 

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleUIRefactoring
//{
//    internal class Program
//    {
//        private static void Main(string[] args)
//        {
//            string w, rawTimeWorked;
//            int i;
//            double ttl, t;
//            List<TimeSheetEntry> ents = new List<TimeSheetEntry>();
//            Console.WriteLine("Enter what you did: ");
//            w = Console.ReadLine();
//            Console.WriteLine("How long did you do it for: ");
//            rawTimeWorked = Console.ReadLine();

//            while (double.TryParse(rawTimeWorked, out t) == false)
//            {
//                Console.WriteLine();
//                Console.WriteLine("Invalid number given");
//                Console.WriteLine("How long did you do it for: ");
//                rawTimeWorked = Console.ReadLine();
//            }

//            TimeSheetEntry ent = new TimeSheetEntry();
//            ent.HoursWorked = t;
//            ent.WorkDone = w;
//            ents.Add(ent);
//            Console.WriteLine("Do you want to enter more time (yes/no): ");

//            string answer = Console.ReadLine();
//            bool cont = false;

//            if (answer.ToLower() == "yes")
//            {
//                cont = true;
//            }

//            while (cont == true)
//            {
//                Console.WriteLine("Enter what you did: ");
//                w = Console.ReadLine();
//                Console.WriteLine("How long did you do it for: ");
//                rawTimeWorked = Console.ReadLine();

//                while (double.TryParse(rawTimeWorked, out t) == false)
//                {
//                    Console.WriteLine();
//                    Console.WriteLine("Invalid number given");
//                    Console.WriteLine("How long did you do it for: ");
//                    rawTimeWorked = Console.ReadLine();
//                }

//                ent = new TimeSheetEntry();
//                ent.HoursWorked = t;
//                ent.WorkDone = w;
//                ents.Add(ent);

//                Console.WriteLine("Do you want to enter more time (yes/no): ");

//                answer = Console.ReadLine();
//                cont = false;

//                if (answer.ToLower() == "yes")
//                {
//                    cont = true;
//                }
//            }
//            ttl = 0;
//            for (i = 0; i < ents.Count; i++)
//            {
//                if (ents[i].WorkDone.ToLower().Contains("acme"))
//                {
//                    ttl += ents[i].HoursWorked;
//                }
//            }
//            Console.WriteLine("Simulating Sending email to Acme");
//            Console.WriteLine("Your bill is $" + ttl * 150 + " for the hours worked.");

//            ttl = 0;
//            for (i = 0; i < ents.Count; i++)
//            {
//                if (ents[i].WorkDone.ToLower().Contains("abc"))
//                {
//                    ttl += ents[i].HoursWorked;
//                }
//            }
//            Console.WriteLine("Simulating Sending email to ABC");
//            Console.WriteLine("Your bill is $" + ttl * 125 + " for the hours worked.");

//            ttl = 0;
//            for (i = 0; i < ents.Count; i++)
//            {

//                ttl += ents[i].HoursWorked;

//            }
//            if (ttl > 40)
//            {
//                Console.WriteLine("You will get paid $" + (((ttl - 40) * 15) + (40 * 10)) + " for your work.");
//            }
//            else
//            {
//                Console.WriteLine("You will get paid $" + ttl * 10 + " for your time.");
//            }
//            Console.WriteLine();
//            Console.WriteLine("Press any key to exit application...");
//            Console.ReadKey();
//        }
//    }
//    public class TimeSheetEntry
//    {
//        public string WorkDone;
//        public double HoursWorked;
//    }
//}