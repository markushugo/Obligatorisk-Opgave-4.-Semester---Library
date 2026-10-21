# 1.3 – Afklaringer og projektbeslutninger

Dato: 21. oktober 2026. Status: de overordnede valg er afklaret. Tekniske parametre er bevidst placeret hos de relevante implementeringsopgaver.

## Aftalte beslutninger

| ID | Beslutning | Konsekvens / kontrol |
|---|---|---|
| B01 | Aktuelt omfang er tema 1–5. | Tema 6/7's særskilte øvelser, ZAP, mobilopgave og synopsis udskydes. Grundlæggende sikkerhed fra tema 1–5 bevares. |
| B02 | Alle 11 user stories beholdes som funktionel backlog. | [Acceptkriterierne](acceptkriterier.md) afgrænser den nuværende løsning. Senere temaers særskilte undervisningskrav tilføjes ikke nu. |
| B03 | .NET 9, Razor Pages, Class Library og MSSQL beholdes. | Databaseadgang med ADO.NET/SqlClient. Ingen EF Core eller anden ORM, jf. Moodle. |
| B04 | Gæster kan se produkter og fylde kurven. | Registrering kræves ikke for at oprette en kurv. |
| B05 | Checkout kræver en konto. | Gæsten tilbydes login eller registrering, og kurven bevares automatisk gennem begge flows. |
| B06 | Kontoen giver adgang til egne tidligere ordrer. | Ejerskab skal kontrolleres på serveren, også ved direkte forespørgsler. |
| B07 | Login skal beskyttes mod brute force. | Begrænsninger, fejlsvar og audit beskrives i passwordpolitikken og implementeres senere. |
| B08 | Varer reserveres ikke i kurven. | Kurvehandlinger, timeout og logout ændrer ikke lager. |
| B09 | Lager ændres ved køb, med databasebeskyttelse mod samtidighed. | Én samlet transaktion for lagertræk og ordre. Utilstrækkeligt lager medfører rollback; lager aldrig under nul. |
| B10 | Betalingsdelen er en lokal mock med automatisk godkendelse. | Ingen betalingsudbyder, saldo, fiktiv pengebeholdning eller kortoplysninger. Ingen opkrævning. |
| B11 | Produkter har priser, og ordren gemmer købsværdier. | Viste historiske beløb er ikke en påstand om gennemført opkrævning. |
| B12 | Betalingsgodkendelse omgår ikke validering. | Ugyldig/tom kurv, lagerfejl og databasefejl kan stadig afvise købet. |
| B13 | MFA og password recovery er ude af den aktuelle version. | Almindelig passwordændring i US 6 er fortsat med. |
| B14 | Der arbejdes én delopgave ad gangen med status og commit-forslag. | Brugeren har særskilt autoriseret gennemførelse af hele 1.2–1.5. Der implementeres ikke funktioner fra del 2 og frem i denne omgang. |
| B15 | Commit og push kræver en konkret anmodning. | Tidligere push vedrørte kravoversigten i 1.1. Nye dokumenter committes/pushes ikke automatisk. |

## Ikke fastlagt endnu – hører til senere tekniske opgaver

Disse punkter blokerer ikke dokumentation eller baselinekontrol i del 1. Ved den relevante opgave skal valget gøres konkret og dokumenteres. Hvis materialet og projektet ikke giver tilstrækkelig kontekst, spørges brugeren før implementering.

| Punkt | Ansvarlig opgave | Det, der skal fastlægges |
|---|---|---|
| Passwordpolitik og Pass’O meter | 3.2–3.4 | Minimum/maksimum, accepterede tegn, afviste passwords, scorealgoritme, hasher og parametre. Officielle kilder kontrolleres da. |
| Loginbegrænsning | 3.6 | Tærskler, tidsvinduer, ophævelse, samtidighed og konto-/IP-afgrænsning. Tre forsøg/ti minutter er et undervisningseksempel. |
| Brugerfelter | 2.3 og 3.1 | Brugernavn versus e-mail som login-id, normalisering, unikhed og nødvendige personoplysninger. Ingen mailtjeneste antaget. |
| Ny konto ved checkout | 3.3 og 3.5 | Om en registrering automatisk etablerer login eller leder videre til login. Kurven skal bevares i begge tilfælde. |
| Beskyttelse i hvile | 2.7 | Hvordan relevante ordredata krypteres og nøgler beskyttes. Materialet specificerer ikke felt-, database- eller diskkryptering. |
| Samtidige køb | 2.9 | Konkrete SQL-operationer, transaktionsgrænser, deadlockhåndtering og idempotens. Ingen tabt opdatering eller negativt lager accepteres. |
| Samtidige kurveændringer | 7.2 | Konfliktstrategi for samme browsersession. En ASP.NET-session er ikke i sig selv en transaktion. |
| Cookie-/loginlevetid | 4.4–4.5 | Udløb, kontoskift, tokenfornyelse og ugyldiggørelse ved passwordændring. |
| Administratoroprettelse | 4.1 | En kontrolleret lokal bootstrap-procedure uden offentlig rolleeskalering eller hardcoded adgangskode. |
| Logs og drift | 8.3–8.4 | Lager, adgang, opbevaring, fejlstrategi og manuel overvågningsprocedure. Ingen ekstern tjeneste kræves. |

## Kilder og prioritet

1. Brugerens aftaler i samtalen bestemmer vores aktuelle omfang og betalings-/lagerflow.
2. [Kravoversigten](kravoversigt.md) indeholder Moodle-kilder og skelner mellem opgavekrav og eksempler.
3. [Acceptkriterierne](acceptkriterier.md) omsætter beslutningerne til verificerbar adfærd. Kriterierne er endnu ikke testresultater.

Et fravalg i vores version er ikke dokumentation for, at underviseren har fritaget projektet fra alle fremtidige krav. Vi udvider ikke omfanget uden at afklare det med brugeren.

Commit-forslag: `docs(scope): record checkout inventory and authentication decisions`
