# FOR THE POTATO BACKEND

## Technológia
Visual Studio 2022 ASP .NET Core Web API

Hogy egy létező adatabázisból alakítsa ki .NET Web API szerkezetté  hogy elérhető legyen. Előszőr letelepítjük Mysql.EntityFrameWorkCore-t és beírjuk ezt a parancsot:

`Scaffold-DbContext "server=localhost;database=for_the_potato;user=root;password=" Mysql.EntityFrameWorkCore -OutputDir Models -f`

## Adatbázis
Az adatbázis létrehozásához és adatok feltöltésével egy Migration fájlt használunk.
Visual Studio 2022 Package Manager Consolbe kell írni a parancsot a végrahajtásához:

`update-database`

