# SunRiseSetLibrary
 
��������� ��� ����������� ������� �������� � ������. 
������� ��������� 
Lat,lon - ������ � ������� � �������� � ����� ������� � ��������� ������ (��� ����� � ������)
DateTime day - ���� � ����� ����������� ��������. ��������� ��������� �����. ����������� ������������ ��� ��� � ������� ������� �����. 
        zenith:   ���������� ��� ������� ������� ����������� ��������. 
            offical      = 90 degrees 50'
            civil        = 96 degrees
            nautical = 102 degrees
            astronomical = 108 degrees
isSunrise  - ������������� ������� � ����� ������� ����� ��������/����� ������. 
������ ������. 


double Lat = 54;
double Lon = 57;
DateTime day , double zenith = 90, double localOffset=5
DateTime UT_rice = SunRiseSetLibrary.SunRiseSetCalc.CalcSunrise(Lat, Lon, day, zenith);