using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Библиотека для расчета 
namespace SunRiseSetLibrary
{
    public static class SunRiseSetCalc
    {


        public static DateTime CalcSuntime(double Lat, double Lon, DateTime day, bool isSunrise=true, double zenith = 90)
        /*Lat,lon - in degrees. 
        zenith:                Sun's zenith for sunrise/sunset
            offical      = 90 degrees 50'
            civil        = 96 degrees
            nautical = 102 degrees
            astronomical = 108 degrees
        isSunrise set true for sunrise, set false for sunset*/
        {
            //check input
            double longitude = Lon;
            double rLat = Lat * Math.PI / 180;
           
            //1. first calculate the day of the year
            day = day.ToUniversalTime().Date; //обрезаем время. 
            int N = day.DayOfYear;
          

            //2.convert the longitude to hour value and calculate an approximate time
            double lngHour = longitude / 15;




            double rising_t = N + ((6 - lngHour) / 24);


            double setting_t = N + ((18 - lngHour) / 24);


            // 3.calculate the Sun's mean anomaly


            double M = (0.9856 * rising_t) - 3.289;


            // 4.calculate the Sun's true longitude


            double L = M + (1.916 * Math.Sin(M * Math.PI / 180)) + (0.020 * Math.Sin(2 * M * Math.PI / 180)) + 282.634;

            L = L >= 360 ? L - 360 : L < 0 ? L + 360 : L;
            // NOTE: L potentially needs to be adjusted into the range[0, 360) by adding/ subtracting 360


            //5a.calculate the Sun's right ascension


            double RA = Math.Atan(0.91764 * Math.Tan(L * Math.PI / 180)) * 180 / Math.PI;
            RA = RA >= 360 ? RA - 360 : RA < 0 ? RA + 360 : RA;
            // NOTE: RA potentially needs to be adjusted into the range[0, 360) by adding/ subtracting 360



            Console.WriteLine(" RA " + RA);

            // 5b.right ascension value needs to be in the same quadrant as L


            double Lquadrant = ((int)Math.Floor(L / 90)) * 90;

            double RAquadrant = ((int)Math.Floor(RA / 90)) * 90;

            RA += (Lquadrant - RAquadrant);

            //5c.right ascension value needs to be converted into hours

            RA = RA / 15;


            // 6.calculate the Sun's declination


            double sinDec = 0.39782 * Math.Sin(L * Math.PI / 180);

            double cosDec = Math.Cos(Math.Asin(sinDec));


            //7a.calculate the Sun's local hour angle


            double cosH = (Math.Cos(zenith * Math.PI / 180) - (sinDec * Math.Sin(rLat))) / (cosDec * Math.Cos(rLat));


            if (cosH > 1)
                Console.WriteLine("  the sun never rises on this location(on the specified date)");

            if (cosH < -1)
                Console.WriteLine(" the sun never sets on this location(on the specified date)");

            //7b.finish calculating H and convert into hours


            // if if rising time is desired:
            double H_rise = 360 - Math.Acos(cosH) * 180 / Math.PI;

            //if setting time is desired:
            double H_set = Math.Acos(cosH) * 180 / Math.PI;


            H_rise /= 15;
            H_set /= 15;

            /// 8.calculate local mean time of rising / setting


            double T_rise = H_rise + RA - (0.06571 * rising_t) - 6.622;
            double T_set = H_set + RA - (0.06571 * setting_t) - 6.622;

            T_rise = T_rise >= 24 ? T_rise - 24 : T_rise < 0 ? T_rise + 24 : T_rise;
            T_set = T_set >= 24 ? T_set - 24 : T_set < 0 ? T_set + 24 : T_set;
            // 9.adjust back to UTC


            DateTime UT_rice = day.AddHours(T_rise - lngHour);

            DateTime UT_set = day.AddHours(T_set - lngHour);
            //NOTE: UT potentially needs to be adjusted into the range[0, 24) by adding/ subtracting 24

            //10.convert UT value to local time zone of latitude/ longitude




            if (isSunrise) return UT_rice; else return UT_set;
        }

        //расчет времени рассвета
        public static DateTime CalcSunrise(double Lat, double Lon, DateTime day, double zenith = 90)
        /*Lat,lon - in degrees. 
        zenith:                Sun's zenith for sunrise/sunset
            offical      = 90 degrees 50'
            civil        = 96 degrees
            nautical = 102 degrees
            astronomical = 108 degrees
        */
        {
            //check input
            double longitude = Lon;
            double rLat = Lat * Math.PI / 180;

            //1. first calculate the day of the year
            day = day.ToUniversalTime().Date; //обрезаем время. 
            int N = day.DayOfYear;


            //2.convert the longitude to hour value and calculate an approximate time
            double lngHour = longitude / 15;




            double rising_t = N + ((6 - lngHour) / 24);


            

            // 3.calculate the Sun's mean anomaly


            double M = (0.9856 * rising_t) - 3.289;


            // 4.calculate the Sun's true longitude


            double L = M + (1.916 * Math.Sin(M * Math.PI / 180)) + (0.020 * Math.Sin(2 * M * Math.PI / 180)) + 282.634;

            L = L >= 360 ? L - 360 : L < 0 ? L + 360 : L;
            // NOTE: L potentially needs to be adjusted into the range[0, 360) by adding/ subtracting 360


            //5a.calculate the Sun's right ascension


            double RA = Math.Atan(0.91764 * Math.Tan(L * Math.PI / 180)) * 180 / Math.PI;
            RA = RA >= 360 ? RA - 360 : RA < 0 ? RA + 360 : RA;
            // NOTE: RA potentially needs to be adjusted into the range[0, 360) by adding/ subtracting 360



            Console.WriteLine(" RA " + RA);

            // 5b.right ascension value needs to be in the same quadrant as L


            double Lquadrant = ((int)Math.Floor(L / 90)) * 90;

            double RAquadrant = ((int)Math.Floor(RA / 90)) * 90;

            RA += (Lquadrant - RAquadrant);

            //5c.right ascension value needs to be converted into hours

            RA = RA / 15;


            // 6.calculate the Sun's declination


            double sinDec = 0.39782 * Math.Sin(L * Math.PI / 180);

            double cosDec = Math.Cos(Math.Asin(sinDec));


            //7a.calculate the Sun's local hour angle


            double cosH = (Math.Cos(zenith * Math.PI / 180) - (sinDec * Math.Sin(rLat))) / (cosDec * Math.Cos(rLat));


            if (cosH > 1)
                Console.WriteLine("  the sun never rises on this location(on the specified date)");

            if (cosH < -1)
                Console.WriteLine(" the sun never sets on this location(on the specified date)");

            //7b.finish calculating H and convert into hours


            // if if rising time is desired:
            double H_rise = 360 - Math.Acos(cosH) * 180 / Math.PI;

            //if setting time is desired:
            double H_set = Math.Acos(cosH) * 180 / Math.PI;


            H_rise /= 15;
            H_set /= 15;

            /// 8.calculate local mean time of rising / setting


            double T_rise = H_rise + RA - (0.06571 * rising_t) - 6.622;
           
            T_rise = T_rise >= 24 ? T_rise - 24 : T_rise < 0 ? T_rise + 24 : T_rise;
            
            // 9.adjust back to UTC


            DateTime UT_rice = day.AddHours(T_rise - lngHour);

           
            //NOTE: UT potentially needs to be adjusted into the range[0, 24) by adding/ subtracting 24

            //10.convert UT value to local time zone of latitude/ longitude




             return UT_rice; 
        }

        public static DateTime CalcSunset(double Lat, double Lon, DateTime day, double zenith = 90)
        /*Lat,lon - in degrees. 
        zenith:                Sun's zenith for sunrise/sunset
            offical      = 90 degrees 50'
            civil        = 96 degrees
            nautical = 102 degrees
            astronomical = 108 degrees
       */
        {
            //check input
            double longitude = Lon;
            double rLat = Lat * Math.PI / 180;

            //1. first calculate the day of the year
            day = day.ToUniversalTime().Date; //обрезаем время. 
            int N = day.DayOfYear;


            //2.convert the longitude to hour value and calculate an approximate time
            double lngHour = longitude / 15;




          

            double setting_t = N + ((18 - lngHour) / 24);


            // 3.calculate the Sun's mean anomaly


            double M = (0.9856 * setting_t) - 3.289;


            // 4.calculate the Sun's true longitude


            double L = M + (1.916 * Math.Sin(M * Math.PI / 180)) + (0.020 * Math.Sin(2 * M * Math.PI / 180)) + 282.634;

            L = L >= 360 ? L - 360 : L < 0 ? L + 360 : L;
            // NOTE: L potentially needs to be adjusted into the range[0, 360) by adding/ subtracting 360


            //5a.calculate the Sun's right ascension


            double RA = Math.Atan(0.91764 * Math.Tan(L * Math.PI / 180)) * 180 / Math.PI;
            RA = RA >= 360 ? RA - 360 : RA < 0 ? RA + 360 : RA;
            // NOTE: RA potentially needs to be adjusted into the range[0, 360) by adding/ subtracting 360



            Console.WriteLine(" RA " + RA);

            // 5b.right ascension value needs to be in the same quadrant as L


            double Lquadrant = ((int)Math.Floor(L / 90)) * 90;

            double RAquadrant = ((int)Math.Floor(RA / 90)) * 90;

            RA += (Lquadrant - RAquadrant);

            //5c.right ascension value needs to be converted into hours

            RA = RA / 15;


            // 6.calculate the Sun's declination


            double sinDec = 0.39782 * Math.Sin(L * Math.PI / 180);

            double cosDec = Math.Cos(Math.Asin(sinDec));


            //7a.calculate the Sun's local hour angle


            double cosH = (Math.Cos(zenith * Math.PI / 180) - (sinDec * Math.Sin(rLat))) / (cosDec * Math.Cos(rLat));


            if (cosH > 1)
                Console.WriteLine("  the sun never rises on this location(on the specified date)");

            if (cosH < -1)
                Console.WriteLine(" the sun never sets on this location(on the specified date)");

            //7b.finish calculating H and convert into hours


            // if if rising time is desired:
            double H_rise = 360 - Math.Acos(cosH) * 180 / Math.PI;

            //if setting time is desired:
            double H_set = Math.Acos(cosH) * 180 / Math.PI;


            H_rise /= 15;
            H_set /= 15;

            /// 8.calculate local mean time of rising / setting


            
            double T_set = H_set + RA - (0.06571 * setting_t) - 6.622;

            
            T_set = T_set >= 24 ? T_set - 24 : T_set < 0 ? T_set + 24 : T_set;
            // 9.adjust back to UTC


           

            DateTime UT_set = day.AddHours(T_set - lngHour);
            //NOTE: UT potentially needs to be adjusted into the range[0, 24) by adding/ subtracting 24

            //10.convert UT value to local time zone of latitude/ longitude



             return UT_set;
        }
    }
}
