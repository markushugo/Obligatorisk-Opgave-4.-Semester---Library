# Lokal SQL Server til webshoppen

Database: **SecureCodingWebshopLocal** på maskinens eksisterende lokale standardinstans. Der bruges MSSQL med ADO.NET og ingen ORM.

## Opret via SQL Server Management Studio

1. Åbn SSMS og opret forbindelse til server **lpc:.** med **Windows Authentication**. Det vælger den lokale SQL Server via shared memory; du behøver ikke TCP-port 1433.
2. Ved den aktuelle lokale udviklingsinstans anvendes kryptering med tillid til servercertifikatet. Hvis SSMS kræver det for denne forbindelse, vælg **Trust server certificate** under forbindelsesindstillinger. Det er en lokal udviklingsindstilling, ikke en produktionsanbefaling.
3. Åbn `database/001-create-local-database.sql` fra projektet, og vælg **Execute / F5**.
4. Opdater **Databases** i Object Explorer. Åbn **SecureCodingWebshopLocal → Tables → dbo.Product**.

Scriptet opretter den navngivne database og Product-tabellen, hvis de mangler. Tre eksempelprodukter tilføjes kun, hvis tabellen er tom. Genkørsel sletter ikke data eller nulstiller lager. Scriptet ændrer ikke andre databaser og opretter ingen serverlogins.

Databasen ligger i SQL Server, ikke inde i SSMS eller Visual Studio. Begge programmer kan forbinde til samme database. Databasescriptet vises som en løsningsfil i Visual Studios Database-mappe.

## Forbind webshoppen

Webprojektets lokale User Secrets indeholder:

```json
{
  "ConnectionStrings:DefaultConnection": "Server=lpc:.;Database=SecureCodingWebshopLocal;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Connect Timeout=5;Application Name=SecureCodingWebshop"
}
```

Strengen bruger den aktuelle Windows-identitet og indeholder intet password. Åbn User Secrets ved at højreklikke på webprojektet i Visual Studio og vælge **Manage User Secrets**. ASP.NET Core indlæser dem automatisk i Development.

Vælg webprojektet som startprojekt, vælg **http**-profilen og tryk F5. Produktoversigten skal hente de tre produkter fra databasen.

I Visual Studios SQL Server Object Explorer kan du tilføje en SQL Server-forbindelse med servernavnet **lpc:.**, Windows Authentication og de samme lokale certifikatindstillinger. Vælg databasen **SecureCodingWebshopLocal**. En separat SQL Database Project-installation er ikke nødvendig.

## Afgrænsning

- Dette er et minimalt databaseskema til det eksisterende produktrepository. Brugere, ordrer og de efterfølgende sikkerhedsopgaver er endnu ikke implementeret.
- Price lagres som decimal(18,2), men C#-modellerne bruger fortsat double indtil den planlagte ændring i opgave 2.5.
- Stock-constraint forhindrer lagring af negative tal. Den erstatter ikke den planlagte atomiske lager-/ordretransaktion ved samtidige køb.
- Den eksisterende kurvelogik reducerer stadig lager ved tilføjelse til kurv. Det er en kendt afvigelse fra det aftalte slutdesign og ændres i den planlagte kurveopgave.
- Windows-kontoen havde administratorrettigheder på denne lokale instans ved kontrollen. En separat applikationsidentitet med begrænsede rettigheder skal vurderes i opgave 2.7. Webappen opretter ikke selv databaser ved opstart.
- Portopsætning, SQL-tjenester og eksisterende databaser er ikke ændret.

Kilde til protokolformatet: [Microsoft: lokale SQL Server-forbindelser](https://learn.microsoft.com/en-us/sql/sql-server/connect-to-database-engine).

## Verificeret 21. oktober 2026

- Begge projekter bygger med 0 fejl og 0 advarsler.
- Den faktiske Razor Pages-applikation blev startet i Development med User Secrets, uden en midlertidig connection-string-override.
- GET / gav HTTP 200 og viste Tastatur, Mus og Skaerm fra den nye database.
- GET /Cart gav HTTP 200 og viste en tom kurv.
- Oprettelsesscriptet blev genkørt; der er stadig tre produkter med uændrede priser og lagerantal.
- Ingen kurve-POST eller køb blev udført. Den midlertidige testserver blev stoppet bagefter.

Commit-forslag: `fix(database): provision local SQL Server database for product pages`
