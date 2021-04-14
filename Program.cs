using System;
using System.Text;

namespace PayrollProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Paycheck entry = new Paycheck();
            LoadInputs(entry);
            PrintPayCheck(entry);

        }

        static void LoadInputs(Paycheck entry)
        {
            Console.Write("Enter Name: ");
            entry.SetName(Console.ReadLine());

            Console.Write("Enter ID#: ");
            entry.SetID(Console.ReadLine());

            Console.Write("Enter Title: ");
            entry.SetTitle(Console.ReadLine());

            Console.Write("Enter Hours: ");
            entry.SetHours(Convert.ToInt32(Console.ReadLine()));

            Console.Write("Enter Rate: ");
            entry.SetRate(Convert.ToDouble(Console.ReadLine()));
        }

        static void PrintPayCheck(Paycheck entry)
        {
            Console.WriteLine("\nInk Incorporated\n--------------------");
            var f = new StringBuilder();
            f.Append(String.Format("{0, -10} {1, -18} {2, -5} {3, 8} {4, 3} {5, -6} {6, -12} {4, 3} {7, -8} {8:d}\n",
                                    "Employee:", entry.GetName(), "ID #:", entry.GetID(), "", "Title:", entry.GetTitle(), "Week Of:", DateTime.Today));
            f.Append(String.Format("{0, -8} {1, 5} {2, 14} {3, 10} {4, 2:C2} {2, 8} {5, -7} {6, 3} {2, 3} {7, -10} {8, 2:C2}\n",
                                    "Reg Hrs:", entry.GetHours(), "", "Reg Rate: ", entry.GetRate(), "OT Hrs:", entry.CalculateOTHours(), "OT Rate: ", entry.CalculateOTRate()));
            f.Append(String.Format("{0, 9} {1, 9:C2} {2, 9} {3, 8} {4, 3:C2} {2, 7} {5, -12} {6, 1} {2, 3} {7, -14} {8, 2:C0}\n",
                                    "Reg Pay: ", entry.CalculateRegPay(), "", "OT Pay: ", entry.CalculateOTPay(), "Tax Status:", entry.CalculateTaxStatus(), "Union Dues: ", entry.CalculateUnionDues()));
            f.Append(String.Format("{0, 11} {1, 4:C2} {2, 51} {3, -10} {4, 3:C2}\n==========================================================================================\n",
                                    "Gross Pay: ", entry.CalculateGrossPay(), "", "Taxes: ", entry.CalculateTaxDeduction()));
            f.Append(String.Format("{0, 60} {1, 21} {2, 4:C2}", "", "Net Pay: ", entry.CalculateNetPay()));

            Console.WriteLine(f);
        }
    }
}
