# SunRiseSetLibrary
 
Библиотке для определения времени рассвета и заката. 
Входные параметры 
Lat,lon - Широта и долгота в градусах и долях градуса с плавоющей точкой (без минут и секунд)
DateTime day - дата и время определения рассвета. Указывать локальное время. Определение произведется для дня в нулевом часовом поясе. 
        zenith:   переменная для разного способа определения рассвета. 
            offical      = 90 degrees 50'
            civil        = 96 degrees
            nautical = 102 degrees
            astronomical = 108 degrees
isSunrise  - переключатель функции в режим расчета время рассвета/время заката. 
Пример вызова. 


double Lat = 54;
double Lon = 57;
DateTime day , double zenith = 90, double localOffset=5
DateTime UT_rice = SunRiseSetLibrary.SunRiseSetCalc.CalcSunrise(Lat, Lon, day, zenith);