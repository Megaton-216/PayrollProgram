using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollProgram
{
    class Paycheck
    {
        string name;
        string id;
        string title;
        int hours;
        double rate;

        public Paycheck() {}

        public void SetName(string nameIn)
        {
            name = nameIn;
        }

        public string GetName()
        {
            return name;
        }

        public void SetID(string idIn)
        {
            id = idIn;
        }

        public string GetID()
        {
            return id;
        }

        public void SetTitle(string titleIn)
        {
            title = titleIn;
        }

        public string GetTitle()
        {
            return title;
        }

        public void SetHours(int hoursIn)
        {
            hours = hoursIn;
        }

        public int GetHours()
        {
            return hours;
        }

        public void SetRate(double rateIn)
        {
            rate = rateIn;
        }

        public double GetRate()
        {
            return rate;
        }

        public int CalculateOTHours()
        {
            return hours <= 40 ? 0 : hours - 40;
        }

        public double CalculateOTRate()
        {
            return Math.Round(rate * 1.5, 2, MidpointRounding.AwayFromZero);
        }

        public double CalculateOTPay()
        {
            return CalculateOTHours() == 0 ? 0 : CalculateOTRate() / CalculateOTHours();
        }

        public int CalculateTaxStatus()
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

        public double CalculateRegPay()
        {
            return Math.Round(rate * hours, 2, MidpointRounding.AwayFromZero);
        }

        public double CalculateGrossPay()
        {
            return CalculateRegPay() + CalculateOTPay();
        }

        public double CalculateTaxDeduction()
        {
            double grosspay = CalculateGrossPay();

            switch (CalculateTaxStatus())
            {
                case 5: return Math.Round(grosspay * 0.30, 2, MidpointRounding.AwayFromZero);
                case 4: return Math.Round(grosspay * 0.24, 2, MidpointRounding.AwayFromZero);
                case 3: return Math.Round(grosspay * 0.20, 2, MidpointRounding.AwayFromZero);
                case 2: return Math.Round(grosspay * 0.18, 2, MidpointRounding.AwayFromZero);
                default: return Math.Round(grosspay * 0.15, 2, MidpointRounding.AwayFromZero);
            }
        }

        public double CalculateUnionDues()
        {
            return CalculateGrossPay() > 100 ? 15.0 : 0.0;
        }

        public double CalculateNetPay()
        {
            return Math.Round(CalculateGrossPay() - CalculateTaxDeduction() - CalculateUnionDues(), 2, MidpointRounding.AwayFromZero);
        }
    }
}
