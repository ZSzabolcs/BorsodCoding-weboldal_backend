# FOR THE POTATOE BACKEND
Hogy egy létezõ adatabázisból alakítsa ki .NET Web API szerkezetet, modulokat, nekem úgy sikerült, hogy letelepítjük Pomelo.EntityFrameworkCore.MySql-t és megírjuk ezt a parancsot:
Scaffold-DbContext "server=localhost;database=for_the_potatoe;user=root;password=;ssl mode=none;" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -f