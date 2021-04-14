using System;
using System.Text;

namespace PayrollProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = new string[6];
            LoadInputs(inputs);
            int othours = CalculateOTHours(Convert.ToInt32(inputs[4]));
            double otrate = CalculateOTRate(Convert.ToDouble(inputs[5]));
            double otpay = CalculateOTPay(otrate, othours);
            int taxstatus = CalculateTaxStatus(Convert.ToDouble(inputs[5]));
            double regpay = CalculateRegPay(Convert.ToDouble(inputs[5]), Convert.ToDouble(inputs[4]));
            double grosspay = CalculateGrossPay(regpay, otpay);
            double taxes = CalculateTaxDeduction(taxstatus, grosspay);
            double dues = CalculateUnionDues(grosspay);
            PrintPayCheck(inputs, othours, otrate, otpay, taxstatus, regpay, grosspay, taxes, dues);

        }

        static void LoadInputs(string[] inputs)
        {
            Console.Write("Enter Name: ");
            inputs[0] = Console.ReadLine();
            Console.Write("Enter ID#: ");
            inputs[1] = Console.ReadLine();
            Console.Write("Enter Title: ");
            inputs[2] = Console.ReadLine();
            Console.Write("Enter Date: ");
            inputs[3] = Console.ReadLine();
            Console.Write("Enter Hours: ");
            inputs[4] = Console.ReadLine();
            Console.Write("Enter Rate: ");
            inputs[5] = Console.ReadLine();
        }

        static int CalculateOTHours(int hours)
        {
            return hours <= 40 ? 0 : hours - 40;
        }

        static double CalculateOTRate(double rate)
        {
            return Math.Round(rate * 1.5, 2, MidpointRounding.AwayFromZero);
        }

        static double CalculateOTPay(double otrate, double othours)
        {
            return othours == 0 ? 0 : otrate / othours;
        }

        static int CalculateTaxStatus(double rate)
        {
            if (rate > 16)
                return 5;
            else if (rate > 12)
                return 4;
            else if (rate > 8)
                return 3;
            else if (rate > 6)
                return 2;
            else
                return 1;
        }

        static double CalculateRegPay(double rate, double hours)
        {
            //Console.WriteLine(rate * hours);
            return Math.Round(rate * hours, 2, MidpointRounding.AwayFromZero);
        }

        static double CalculateGrossPay(double regpay, double otpay)
        {
            //Console.WriteLine(regpay + otpay);
            return regpay + otpay;
        }

        static double CalculateTaxDeduction(int taxstatus, double grosspay)
        {
            switch (taxstatus)
            {
                case 5:
                    {
                        //Console.WriteLine(Math.Round(grosspay * 0.30, 2, MidpointRounding.AwayFromZero));
                        return Math.Round(grosspay * 0.30, 2, MidpointRounding.AwayFromZero);
                    }
                case 4: return Math.Round(grosspay * 0.24, 2, MidpointRounding.AwayFromZero);
                case 3: return Math.Round(grosspay * 0.20, 2, MidpointRounding.AwayFromZero);
                case 2: return Math.Round(grosspay * 0.18, 2, MidpointRounding.AwayFromZero);
                default: return Math.Round(grosspay * 0.15, 2, MidpointRounding.AwayFromZero);
            }
        }

        static double CalculateUnionDues(double grosspay)
        {
            return grosspay > 100 ? 15.0 : 0.0;
        }

        static double CalculateNetPay(double grosspay, double taxes, double dues)
        {
            return Math.Round(grosspay - taxes - dues, 2, MidpointRounding.AwayFromZero);
        }

        static void PrintPayCheck(string[] inputs, int othours, double otrate, double otpay, int taxstatus, double regpay, double grosspay, double taxes, double dues)
        {
            Console.WriteLine("\nInk Incorporated\n--------------------");
            var f = new StringBuilder();
            f.Append(String.Format("{0, -10} {1, -18} {2, -5} {3, 8} {4, 3} {5, -6} {6, -12} {4, 3} {7, -8} {8, 8}\n",
                                    "Employee:", inputs[0], "ID #:", inputs[1], "", "Title:", inputs[2], "Week Of:", inputs[3]));
            f.Append(String.Format("{0, -8} {1, 5} {2, 14} {3, 10} {4, 2:C2} {2, 8} {5, -7} {6, 3} {2, 3} {7, -10} {8, 2:C2}\n",
                                    "Reg Hrs:", inputs[4], "", "Reg Rate: ", Convert.ToDouble(inputs[5]), "OT Hrs:", othours, "OT Rate: ", otrate));
            f.Append(String.Format("{0, 9} {1, 9:C2} {2, 9} {3, 8} {4, 3:C2} {2, 7} {5, -12} {6, 1} {2, 3} {7, -14} {8, 2:C0}\n",
                                    "Reg Pay: ", regpay, "", "OT Pay: ", otpay, "Tax Status:", taxstatus, "Union Dues: ", dues));
            f.Append(String.Format("{0, 11} {1, 4:C2} {2, 51} {3, -10} {4, 3:C2}\n==========================================================================================\n",
                                    "Gross Pay: ", grosspay, "", "Taxes: ", taxes));
            f.Append(String.Format("{0, 60} {1, 21} {2, 4:C2}", "", "Net Pay: ", CalculateNetPay(grosspay, taxes, dues)));

            Console.WriteLine(f);
        }
    }
}
