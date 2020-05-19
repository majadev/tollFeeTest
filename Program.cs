using System;
using TollFeeCalculator;

namespace tollFeeCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            var car = new Car();
            var dates = new DateTime[]
            {
                DateTime.Parse("2020-05-12 6:00"),
                DateTime.Parse("2020-05-12 6:30"),
                DateTime.Parse("2020-05-12 14:00"),
                DateTime.Parse("2020-05-12 18:00")
            };

            var fee = TollCalculator.GetTotalTollFee(car, dates);

            Console.WriteLine(fee.ToString());
        }
    }
}
