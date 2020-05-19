using System;
using System.Globalization;
using TollFeeCalculator;

public static class TollCalculator
{
    //changed to static cuz there is nothing to 'instansera'
    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public static int GetTotalTollFee(Vehicle vehicle, DateTime[] dates)
    {
        //Added this line here so that we don't have to take vehicle into "GetTollFee" as well
        if (IsTollFreeVehicle(vehicle)) return 0;

        DateTime intervalStart = dates[0];
        int tempFee = 0;
        int totalFee = 0;

        for (int i = 0; i < dates.Length; i++)
        {
            var minutes = 0;

            //En bil som passerar flera betalstationer inom 60 minuter bara beskattas en gång.
            //Det belopp som då ska betalas är det högsta beloppet av de passagerna.
            //Måste räkna med att man räknar om

            //första -> åk rätt in
            if (i == 0)
            {
                totalFee += GetTollFee(dates[i]);
            }
            else
            {
                int fee = GetTollFee(dates[i]);
                minutes = (int)Math.Round((dates[i] - dates[i - 1]).TotalMinutes);

                if (minutes <= 60)
                {
                    if (fee > tempFee)
                    {
                        tempFee = fee;
                    }
                }
                else
                {
                    totalFee += fee;
                    minutes = 0;
                    tempFee = 0;
                }
            }
        }

        return totalFee > 60 ? 60 : totalFee;

        //foreach (DateTime date in dates)
        //{
        //    int nextFee = GetTollFee(date);

        //    var minutes = Math.Round((date - intervalStart).TotalMinutes);

        //    if (minutes <= 60)
        //    {
        //        if (totalFee > 0) totalFee -= tempFee;
        //        if (nextFee >= tempFee) tempFee = nextFee;

        //        totalFee += tempFee;
        //    }
        //    else
        //    {

        //        totalFee += nextFee;
        //    }
        //}
    }

    private static bool IsTollFreeVehicle(Vehicle vehicle)
    {
        //I don't like to check strings when it can be a type
        if (vehicle == null) return false;
        VehicleType vehicleType = vehicle.GetVehicleType();

        return vehicleType.Equals(VehicleType.Motorbike) ||
               vehicleType.Equals(VehicleType.Tractor) ||
               vehicleType.Equals(VehicleType.Emergency) ||
               vehicleType.Equals(VehicleType.Diplomat) ||
               vehicleType.Equals(VehicleType.Foreign) ||
               vehicleType.Equals(VehicleType.Military);
    }

    public static int GetTollFee(DateTime date)
    {
        if (IsTollFreeDate(date)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        //Cleaned it up

        if (hour == 6 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30) return 13;
        else if (hour == 7) return 18;
        else if (hour == 8 && minute <= 29) return 13;
        else if (hour <= 14 && minute <= 59) return 8;
        else if (hour == 15 && minute <= 29) return 13;
        else if (hour <= 16 && minute <= 59) return 18;
        else if (hour == 17 && minute <= 59) return 13;
        else if (hour == 18 && minute <= 29) return 8;
        else return 0;
    }


    //Maybe break out this, might be good to know the red days for other parts of the system
    private static Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        //Skatt tas inte ut lördagar, helgdagar, dagar före helgdag eller under juli månad. 

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }

}