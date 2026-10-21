# 1.5 – Teknisk udgangspunkt og baseline

Dato: 21. oktober 2026. Omfang: Git-struktur, build og læsende lokal kørsels-/databasekontrol. Ingen funktioner eller databaseskemaer er implementeret som del af baseline.

## Resultat

| Kontrol | Resultat | Betydning |
|---|---|---|
| Løsning og projektreferencer | Bestået | Rodens .slnx refererer både net9.0-webprojektet og net9.0-biblioteket. |
| Build | Bestået, 0 advarsler / 0 fejl | Begge projekter blev bygget med eksisterende restore-data. |
| GET /Cart med tom session | HTTP 200 | Siden viser tom kurv uden databaseopslag. |
| GET / med den konfigurerede databaseadresse | HTTP 500 med generisk fejlside i Production | Produktvisning kunne ikke verificeres; databaseforbindelsen fejler. Ingen SQL-detaljer set i det målte fejlsvar. |
| GET /Privacy | HTTP 404 | Layoutets Privacy-link peger på en side, som ikke findes i den gennemgåede kildekode. |
| CSP og nosniff på målte sider | Ikke til stede | Kendt arbejde til webbeskyttelse i del 5. |
| SQL Server-tjeneste | MSSQLSERVER kører | Det beviser ikke, at det konfigurerede TCP-endpoint eller login virker. |
| SQL-forbindelse på konfigureret endpoint | Fejlet med timeout | Skema, tabeller, lagerdata, transportstatus, DB-rettigheder og kryptering i hvile er ikke verificeret. |

## Buildmetode og begrænsning

Installerede SDK'er: 9.0.308 og 10.0.402. Projekternes TargetFramework er net9.0. Uden global.json er SDK-valget ikke låst til SDK 9; build blev udført med maskinens SDK-valg, mens output retter sig mod .NET 9.

Den beståede kommando fra løsningens rod var:

```powershell
dotnet build "Obligatorisk Opgave 4. Semester.slnx" --no-restore --nologo -v minimal -m:1 -nodeReuse:false
```

Et første forsøg med parallel standardkonfiguration afsluttede med fejl uden en brugbar compilerdiagnose. Den serielle variant ovenfor lykkedes. Der er ikke foretaget en ren klon/restore, Release-build eller fuld afhængighedsaudit i denne kontrol.

Build skriver genererede bin/obj-filer. Ingen ændring af funktionel kode blev foretaget for at få build til at lykkes.

## Lokal kørselskontrol

Den byggede applikation blev startet midlertidigt på http://127.0.0.1:5189 i Production. Den eksisterende connection string blev kun i processens miljø suppleret med tre sekunders forbindelsestimeout; projektfilerne blev ikke ændret af denne kontrol. Der blev kun sendt GET-requests til /Cart, / og /Privacy. Ingen tilføjelse/rydning af kurv, køb eller databaseændring blev forsøgt.

Den midlertidige server blev stoppet efter kontrollen. Visual Studios egne processer blev ikke stoppet.

Kontrollen dokumenterer de tre målte svar. Den beviser ikke funktionalitet for registrering, login, søgning, checkout, administration eller historik, som endnu ikke er implementeret. Antiforgery og HTTPS/cookieadfærd er ikke verificeret ved denne HTTP-only kontrol.

## Databasekontrol

- Konfigurationen pegede ved baseline på localhost,1433 og indeholdt SQL-loginoplysninger samt TrustServerCertificate=True.
- Der var ikke en eksplicit Encrypt-indstilling i den læste connection string. Den faktiske Microsoft.Data.SqlClient-forbindelses transport kunne ikke måles, fordi forbindelsen fejlede; der drages ikke en konklusion ud fra en anden drivers standardindstillinger.
- Læsende sqlcmd-kontrol blev forsøgt med krypteret transport og den eksisterende tillidsindstilling. Password blev overført til underprocessen via et midlertidigt miljøfelt og blev ikke udskrevet eller gemt i rapporten.
- Første forsøg ramte miljøets socketbegrænsning. Efter udvidet adgang gav forbindelsen stadig timeout.
- Den observerede sqlservr-proces lyttede på loopback-port 1434. Dette kan være en dedikeret administrationsport; den er ikke antaget som det korrekte almindelige applikationsendpoint. Et særskilt læsende diagnostikforsøg på denne port lykkedes heller ikke.
- SQLBrowser var stoppet. Det er ikke i sig selv en forklaring på fejl mod en eksplicit TCP-port, så tjenesten blev ikke startet.
- Der blev ikke ændret port, protokol, tjenestestatus, database-login, lager eller skema.

**Blokeret del:** databaseafhængige funktioner kan ikke godkendes endnu. Ved databaseopgaven skal det korrekte lokale SQL-endpoint, TCP-konfigurationen og loginadgangen afklares. Derpå gentages forbindelse og SELECT-kontrol af Product-skemaet, relevante constraints, rettigheder og kryptering. De planlagte SELECT-forespørgsler er ikke bevis på at resultaterne findes.

## Git-udgangspunkt

Ved starten af 1.5:

- Ydre repository var på master med seneste commit 7d1cb37 (kravoversigten).
- Class Library og løsning var registreret i det ydre repository.
- Mappen Obligatorisk-Opgave-4.-Semester var uregistreret i det ydre repository, fordi den rummede et særskilt indlejret repository.
- Det indlejrede repository havde en ren working tree og seneste commit 991013e samt egen GitHub-remote.
- De nye dokumenter til 1.2–1.4 var uregistrerede. Der blev ikke staged eller committed noget under baselinekontrollen.

Brugeren bad derefter eksplicit om at gøre både webprojekt og bibliotek tilgængelige fra det fælles GitHub-repository. Denne repository-samling håndteres som en særskilt ændring efter baseline med lokal backup af den indlejrede Git-metadata og kontrol af hemmeligheder. Den fletter ikke automatisk historikken fra det gamle webrepository ind i det ydre repository.

## Kendte fund til den videre opgaveliste

| Fund | Kilde i projektet | Næste relevante opgave |
|---|---|---|
| Lager reduceres ved tilføjelse til kurv; sessionudløb frigiver ikke reservationen. | Services/CartService.cs i webprojektet | 7.1 |
| Lager læses og gemmes uden atomisk betinget opdatering. | Services/ProductService.cs i biblioteket | 2.9 |
| Hele produktet, inklusive Stock, overskrives ved produktopdatering. | Repos/ProductRepo.cs | 2.8–2.9 og 6.3 |
| Pengeværdier bruger double. | Product, Cart og CartItem | 2.5 |
| Brugerrepository-implementering og login mangler. | IUserRepo, UserService og Program.cs | 3.1–3.6 |
| Passwordfelt sendes videre uden hashing i den viste service; faktisk lagring findes ikke endnu. | UserService.CreateUser | 3.2 |
| HTTPS-redirect, HSTS og CSP er ikke konfigureret i opstarten. | Program.cs | 5.5 |
| DB-credentials lå i appsettings.json ved baseline. | Webprojektets konfiguration | Håndteres før publicering; fuld DB-sikkerhed i 2.6–2.7 |
| Privat-/adminfunktioner, køb, ordrehistorik og audit er ikke implementeret. | Eksisterende projektstruktur | Del 3–8 |

## Status for del 1

1.1: kravgrundlag gennemgået, tema 1–5.

1.2: acceptkriterier dokumenteret; ingen af de nye acceptkontroller er afkrydset som bestået.

1.3: overordnede projektvalg dokumenteret; tekniske parametre har placerede opfølgningsopgaver.

1.4: trusselsmodel med aktiver, tillidsgrænser og 17 STRIDE-scenarier dokumenteret.

1.5: baselineundersøgelse gennemført. Build og tre HTTP-svar er verificeret; databaseforbindelse er forsøgt men fortsat blokeret. Opgaven dokumenterer dermed udgangspunkt og blocker, ikke et fuldt fungerende databaseflow.

Commit-forslag: `docs(baseline): record build runtime checks and database blocker`

## Opdatering: lokal databaseforbindelse rettet

Efter baseline bad brugeren om at oprette en ny lokal database. SecureCodingWebshopLocal er nu oprettet på den eksisterende standardinstans via shared memory (lpc:.) med Windows Authentication. Scriptet database/001-create-local-database.sql opretter Product og tre eksempelprodukter og kan genkøres uden dublering eller lagerreset. Projektets User Secrets er opdateret til denne lokale forbindelse uden password. Ingen eksisterende databaser eller SQL Server-netværksindstillinger er ændret.

Begge projekter bygger fortsat med 0 fejl/0 advarsler. Den faktiske webapp blev kørt i Development uden forbindelse-override: GET / returnerede HTTP 200 med alle tre produkter, og GET /Cart returnerede HTTP 200 med tom kurv. Den oprindelige forbindelsesblocker er dermed løst for denne lokale opsætning. Øvrige funktions-/sikkerhedsfund i baseline er fortsat åbne. Se lokal-database.md.
