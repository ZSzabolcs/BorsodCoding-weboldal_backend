# FOR THE POTATO BACKEND
Hogy egy létező adatabázisból alakítsa ki .NET Web API szerkezetet, modulokat, nekem úgy sikerült, hogy letelepítjük Pomelo.EntityFrameworkCore.MySql-t és megírjuk ezt a parancsot:
`Scaffold-DbContext "server=localhost;database=for_the_potato;user=root;password=;ssl mode=none;" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -f`
VAGY SSL mode=none nélkül:
`Scaffold-DbContext "server=localhost;database=for_the_potato;user=root;password=" Mysql.EntityFrameWorkCore -OutputDir Models -f
`

