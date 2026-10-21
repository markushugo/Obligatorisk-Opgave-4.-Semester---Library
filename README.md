# Obligatorisk opgave – Secure coding

Webshop i .NET 9 med Razor Pages, Class Library og MSSQL via ADO.NET. Repository indeholder både webprojektet og biblioteket. Ingen ORM anvendes.

## Åbn hele løsningen

Åbn **Obligatorisk Opgave 4. Semester.slnx i repository-roden** i Visual Studio. Den indeholder begge projekter:

```text
Obligatorisk Opgave 4. Semester.slnx
Obligatorisk Opgave Library/
Obligatorisk-Opgave-4.-Semester/
  Obligatorisk Opgave 4. Semester/
    Pages/
    Services/
    Program.cs
docs/
```

Den ældre løsning inde i webmappen indeholder kun webprojektet. Brug rodløsningen til samlet arbejde.

Webprojektet er markeret som standard-startprojekt i rodløsningen med `DefaultStartup="true"`. Hvis Visual Studio stadig husker biblioteket som startprojekt, højreklik på **Obligatorisk Opgave 4. Semester** (webprojektet med Pages-mappen) i Solution Explorer og vælg **Set as Startup Project / Angiv som startprojekt**. Vælg derefter **http**-profilen og tryk F5. Biblioteket kan ikke startes selvstændigt; det bygges automatisk som afhængighed til webprojektet.

Markeringen understøttes af nyere Visual Studio-versioner. En tidligere gemt lokal startindstilling kan kræve det manuelle valg ovenfor. Ændring af startprojekt retter ikke den uafklarede lokale databaseforbindelse.

## Forudsætninger og build

- .NET 9 SDK/runtime og en Visual Studio-version, der understøtter løsningen, eller et kompatibelt nyere SDK.
- Lokal SQL Server med den relevante webshopdatabase.

Fra repository-roden:

```powershell
dotnet restore "Obligatorisk Opgave 4. Semester.slnx"
dotnet build "Obligatorisk Opgave 4. Semester.slnx" -m:1 -nodeReuse:false
```

Baseline-build med de eksisterende restore-data bestod 21. oktober 2026. Den oprindelige TCP-forbindelse fejlede. Der er efterfølgende tilføjet et oprettelsesscript til den lokale SQL Server og en separat database med tre eksempelprodukter. Se [lokal databaseopsætning](docs/lokal-database.md) og den historiske [baseline](docs/baseline.md).

## Lokal databasekonfiguration

Database-login skal ikke gemmes i versionsstyrede appsettings-filer. Webprojektet har et UserSecretsId og indlæser .NET User Secrets i Development gennem ASP.NET Cores standardkonfiguration.

I Visual Studio: højreklik på **webprojektet** → **Manage User Secrets / Administrer brugerhemmeligheder**, og tilføj din egen lokale værdi:

```json
{
  "ConnectionStrings:DefaultConnection": "<din lokale SQL Server-forbindelsesstreng>"
}
```

Pladsholderen er ikke en fungerende forbindelse. Den aktuelle lokale opsætning bruger Windows-login til `SecureCodingWebshopLocal` via `lpc:.`. Se [den konkrete forbindelse og oprettelsesscriptet](docs/lokal-database.md). User Secrets følger ikke med en klon, så andre udviklere skal oprette deres egen lokale konfiguration.

User Secrets er til lokal udvikling og er ikke et krypteret produktionslager. I andre miljøer konfigureres forbindelsen eksempelvis gennem miljøvariablen **ConnectionStrings__DefaultConnection**. Undlad at indsætte credentials i dokumentation, commits eller buildlogs.

Kør lokalt med Development-profilen:

```powershell
dotnet run --project "Obligatorisk-Opgave-4.-Semester/Obligatorisk Opgave 4. Semester/Obligatorisk Opgave 4. Semester.csproj" --launch-profile http
```

HTTP-profilen findes allerede i projektet. HTTPS og øvrig webbeskyttelse er planlagt, men endnu ikke implementeret som del af dokumentationsarbejdet.

## Dokumentation og scope

- [1.1 Kravoversigt for tema 1–5](docs/kravoversigt.md)
- [1.2 Acceptkriterier for 11 user stories](docs/acceptkriterier.md)
- [1.3 Afklaringer og projektbeslutninger](docs/afklaringer.md)
- [1.4 CIA/FIT- og STRIDE-trusselsmodel](docs/trusselsmodel.md)
- [1.5 Build, runtimekontrol og databaseblocker](docs/baseline.md)

Gæster må fylde kurven, men køb kræver login/registrering med bevaret kurv. Lager reserveres ikke i kurven. Checkout skal senere implementeres atomisk med korrekt samtidighedshåndtering. Betaling bliver en gratis lokal simulation uden saldo, tegnebog eller betalingsudbyder. MFA og recovery er udskudt.

Dokumenterede acceptkriterier er ikke implementerede funktioner eller beståede tests. Login, ordreflow og sikkerhedslag færdiggøres i de efterfølgende opgaver. Tema 6/7's særskilte undervisningsøvelser er uden for den aktuelle afgrænsning.

## Repository-samling

Webprojektets aktuelle filer er samlet i det samme repository som biblioteket. Den indlejrede .git-mappe er bevaret som en lokal backup uden for repository; den gamle webhistorik importeres ikke til dette repository. Et almindeligt clone af dette repository skal derfor hente begge projekter uden submodule-init.

Den nuværende delte konfiguration indeholder ikke det lokale database-login. Denne ændring omskriver ikke historikken i et eventuelt tidligere webrepository. Tidligere publicerede credentials skal vurderes og udskiftes særskilt.
