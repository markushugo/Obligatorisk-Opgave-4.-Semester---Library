# Opgave 1.1 – Kravoversigt for tema 1–5

Dato: 21. oktober 2026. Fag: DAT-RO-F-V25V – Sem4-Secure coding.

## Status og afgrænsning

Gennemgangen af de tilgængelige Moodle-sider for tema 1–5 og de syv PDF'er i undervisningsmappen er afsluttet. Dette er en dokumentationsleverance; projektkode, database og Git er ikke ændret. Ingen funktioner er implementeret eller afprøvet som del af denne opgave.

Brugeren har afgrænset det aktuelle arbejde til og med tema 5. Tema 6 (injection), tema 7 (sikkerhedstest), mobiløvelsen og synopsis er derfor ikke aktuelle leverancer. Enkelte senere sider blev læst før denne afgrænsning; de bruges ikke som selvstændigt kravgrundlag her. Sikkerhedsregler, som allerede fremgår af tema 1–5, beholdes, selv om de også behandles senere.

Kategorier:

- **Eksplicit krav:** står i opgavebeskrivelsen eller som konkret implementeringsopgave i undervisningen.
- **Undervisningsprincip:** skal omsættes til et passende forsvar i den valgte arkitektur; er ikke krav om at kopiere alle eksempler.
- **Projektbeslutning:** fastlagt af brugeren i samtalen.
- **Eksempel/tilvalg:** illustration eller mulighed uden et entydigt krav om implementering.
- **Udskudt:** uden for den aktuelle version.

## A. Generelle opgavekrav

| ID | Krav | Kategori | Kilde / konsekvens |
|---|---|---|---|
| G1 | En fungerende webshop med kundeoprettelse, produktvisning og køb. | Eksplicit krav | [Obligatorisk opgave](https://moodle.zealand.dk/course/view.php?id=19546&section=34). |
| G2 | En database skal anvendes. ORM'er, herunder Entity Framework og Hibernate, er ikke tilladt. | Eksplicit krav | Samme kilde. Eksisterende MSSQL/ADO.NET kan beholdes. |
| G3 | Løsningen skal køre lokalt med adgang til webserverens konfiguration. | Eksplicit krav | Samme kilde. |
| G4 | Monolitisk arkitektur anbefales kraftigt, eksempelvis Razor Pages eller MVC. | Anbefaling | Samme kilde. Der kræves ikke yderligere Class Libraries. |
| G5 | Kildekode i Git-repository og fungerende live demo med forklaring af sikkerhedsvalg. | Eksplicit krav | Samme kilde. Der foretages ingen upload i opgave 1.1. |
| G6 | Implementer relevante forsvarsmekanismer fra fagets temaer; anvend user stories som funktionelt kravgrundlag. | Eksplicit krav | [Introduktion](https://moodle.zealand.dk/course/view.php?id=19546&section=1). Aktuelt afgrænset til tema 1–5 af brugeren. |

## B. Tema 1–2: sikkerhedsgrundlag og trusselsmodellering

| ID | Krav / emne | Kategori | Placering i den tidligere opgaveliste |
|---|---|---|---|
| T1 | Beskriv fortrolighed, integritet og tilgængelighed for webshoppen. | Undervisningsprincip | 1.4 |
| T2 | Kortlæg angrebsoverfladen og anvend STRIDE på relevante user stories. | Konkret undervisningsopgave | 1.4 og senere acceptkriterier i 1.2 |
| T3 | Sikkerhed må ikke alene bero på netværk/infrastruktur. Anvend mindst mulige rettigheder. | Undervisningsprincip | 2.7, 4.1–4.3 og serviceregler |
| T4 | Kundens egne ordredata skal kræve autentifikation og autorisation. | Acceptkriterium i STRIDE-eksempel | 4.3 og 7.6 |
| T5 | Ordredata skal beskyttes i transit og i hvile. | Acceptkriterium i STRIDE-eksempel | 2.7 og 5.5; krypteringsteknik fastlægges i databaseopgaven. |
| T6 | Adgang til ordredata skal logges til revisionssporing. | Acceptkriterium i STRIDE-eksempel | Præcisering til 8.2: også læsning af ordredata, ikke kun ændringer. |
| T7 | Valider input til ordreopslag, og anvend sikre databaseforespørgsler. | Acceptkriterium i STRIDE-eksempel | 2.8, 4.3 og 5.2. Udskydelse af tema 6 ophæver ikke dette. |
| T8 | Sessionidentifikatorer skal være tilfældige, unikke og tidsbegrænsede ved inaktivitet. | Acceptkriterium i STRIDE-eksempel | 4.4–4.5 |

Kilder: [CIA/FIT](https://moodle.zealand.dk/course/view.php?id=19546&section=3), [infrastruktur](https://moodle.zealand.dk/course/view.php?id=19546&section=8), [angrebsoverflade](https://moodle.zealand.dk/course/view.php?id=19546&section=9), [STRIDE](https://moodle.zealand.dk/course/view.php?id=19546&section=10). STRIDE-slides side 31–39 kobler modellen til user stories og opstiller sikkerhedsacceptkriterier. Det er en gennemarbejdet model for ordrehistorik, ikke et bevis på at alle nævnte teknikker kræves i samtlige funktioner.

## C. Tema 3: passwords og login

| ID | Krav / emne | Kategori | Konsekvens |
|---|---|---|---|
| P1 | Udarbejd en konkret passwordpolitik med begrundelser fra OWASP, NIST og Center for Cybersikkerhed. | Eksplicit undervisningsopgave | Gør dette til en særskilt dokumentationsdel før implementering af politikken. Ingen minimumslængde fastlægges i opgave 1.1. |
| P2 | Implementer registrering og passwordhjælp med et Pass’O meter. | Eksplicit undervisningsopgave | US 1 og 11; 3.3–3.4. |
| P3 | Pass’O meter skal give en score fra 0–100 og forståelig feedback. | Konkret angivelse i Moodle | Præcisering til 3.4. Scoringsregler begrundes i passwordpolitikken. |
| P4 | Gem aldrig det oprindelige password i databasen; brug egnet passwordhashing og individuelt salt. | Eksplicit sikkerhedsprincip | 3.1–3.2. BCrypt, Argon2 og PBKDF2 nævnes som muligheder. Det konkrete bibliotek vælges senere. |
| P5 | Beskyt login mod brute force og credential stuffing; opdater passwordpolitikken med beskyttelsen. | Eksplicit undervisningsopgave / US 2 | 3.6 samt dokumentationen af passwordpolitikken. |
| P6 | Implementer passwordændring. | US 6 og slidesenes konkrete opgave | 3.7. Skal fortsat indgå, selv om recovery er fravalgt. |
| P7 | Præcis tre forsøg og ti minutters spærring. | Eksempel | Slides kalder det et forslag til flow. Værdierne er ikke et ubetinget krav. |
| P8 | CAPTCHA, fingerprinting, Redis, eksternt lækagetjek og pepper. | Eksempler/tilvalg | Indføres ikke automatisk; kræver et konkret behov og et begrundet valg. |
| P9 | Password recovery. | Udskudt efter brugerbeslutning | Indgår i undervisningen. Det er en afgrænsning af vores version, ikke en påstand om at emnet mangler i kurset. |
| P10 | MFA. | Udskudt efter brugerbeslutning; teoretisk i slides | Password del 2, side 37, siger direkte at faget ikke bruger tid på implementering af en MFA-løsning. |

Kilder: [passwordtema](https://moodle.zealand.dk/course/view.php?id=19546&section=11), [passwordpolitik](https://moodle.zealand.dk/course/view.php?id=19546&section=12), [Pass’O meter](https://moodle.zealand.dk/course/view.php?id=19546&section=13), [lagring](https://moodle.zealand.dk/course/view.php?id=19546&section=14), [rate limiting](https://moodle.zealand.dk/course/view.php?id=19546&section=15), [recovery](https://moodle.zealand.dk/course/view.php?id=19546&section=16), [MFA](https://moodle.zealand.dk/course/view.php?id=19546&section=17).

## D. Tema 4: HTTP, cookies og sessioner

| ID | Krav / emne | Kategori | Konsekvens |
|---|---|---|---|
| W1 | HTTPS og HSTS-konfiguration. | Konkret implementeringsopgave | 5.5. Skeln mellem konfiguration og hvad der kan demonstreres på det lokale værtsnavn. |
| W2 | Accepter kun relevante HTTP-metoder; håndter ugyldige metoder og relevante hændelser. | ASVS-princip i slides | Tilføjes eksplicit til 5.3/5.6. Metodelisten tilpasses Razor Pages og statiske ressourcer; kopier ikke eksempelmidware blindt. |
| W3 | Implementer Content-Type-validering efter et bevidst valg af tilladte formater. | Konkret implementeringsopgave | Tilføjes eksplicit til 5.1/5.2. Almindelige formularer skal fortsat virke. |
| W4 | Korrekt Content-Type og tegnsæt i svar samt nosniff. | Konkret undervisningsopgave/princip | Tilføjes eksplicit til 5.5. Svarformater og statiske ressourcer skal kontrolleres. |
| W5 | Implementer CSP og beskyttelse mod indlejring/clickjacking. | Konkret implementeringsopgave | 5.5, tilpasset eksisterende layout og scripts. |
| W6 | Implementer/konfigurer en restriktiv CORS-tilgang. | Konkret implementeringsopgave | Ny tydelig del under 5.5. For monolitten dokumenteres, om cross-origin adgang overhovedet behøves; ingen unødvendig åbning. |
| W7 | Sikre cookies med Secure, HttpOnly, SameSite og passende afgrænsning/udløb. __Host- indgår i slidesenes kontrolpunkter. | Konkret undervisningsmål / ASVS-princip | 4.4. Kravene skal tilpasses hver cookie og verificeres. |
| W8 | Beskyt tilstandsændrende cookieautentificerede requests mod CSRF. | ASVS-princip | 5.3. Verificer frameworkets eksisterende antiforgery-beskyttelse. |
| W9 | Nye autentifikationstokens ved login og ingen sessiontokens i URL'er. | ASVS-princip i slides | 4.5. Skeln mellem autentifikationscookie og anonym kurvesession; kurven bevares uden at videreføre usikker identitet. |
| W10 | Brug frameworkets sikre generering af identifikatorer og passende inaktivitetsudløb. | ASVS-princip | 4.4–4.5. Ingen egen sessiontoken-generator. |
| W11 | Implementer sessionbaseret indkøbskurv med tilføjelse, antal, fjernelse, total og rydning efter gennemført køb. | Konkret undervisningsopgave | 7.1–7.6; sessionadapter i webprojektet og relevante regler i biblioteket. |
| W12 | Forstå cookie-/storage-risici og dokumenter anvendte cookies. | Undervisningsmål | Kun nødvendige funktioner planlægges; ingen analytics eller tracking tilføjes. Juridisk vurdering ligger ikke i denne kravgennemgang. |

Kilder: [HTTP/HTTPS](https://moodle.zealand.dk/course/view.php?id=19546&section=19), [headers](https://moodle.zealand.dk/course/view.php?id=19546&section=20), [cookies](https://moodle.zealand.dk/course/view.php?id=19546&section=21), [sessioner og kurv](https://moodle.zealand.dk/course/view.php?id=19546&section=22). Milk and cookies del 1, side 13, 15, 18–31, indeholder de konkrete HTTP-opgaver. Del 2, side 15 og 23, indeholder cookie- og sessionskontroller.

## E. Tema 5: autentifikation og autorisation

| ID | Krav / emne | Kategori | Konsekvens |
|---|---|---|---|
| A1 | Adskil kontrol af identitet fra kontrol af rettigheder. | Undervisningsprincip | 3.5 og 4.1–4.3. |
| A2 | Anvend least privilege og relevante roller til kunde-/administratorfunktioner. | Undervisningsprincip / US 9 | 4.1–4.3 og 6.3–6.4. Serveren skal håndhæve rettigheder. |
| A3 | Lav en adgangsmatrix og kontrol af ejerskab til personlige ressourcer. | Model i undervisningen / US 8 | 4.1 og 4.3; rolle alene giver ikke adgang til alle kunders ordrer. |
| A4 | DAC, MAC og ABAC. | Alternative modeller / teori | Skal kunne forklares, men alle modeller skal ikke implementeres samtidigt. |
| A5 | JWT og REST API/Vue-eksempel. | Undervisningseksempel | Ikke et entydigt krav om at ændre Razor Pages til et API. Cookieautentificering beholdes som planlagt. |

Kilder: [adgangskontrol](https://moodle.zealand.dk/course/view.php?id=19546&section=23), [modeller og adgangsmatrix](https://moodle.zealand.dk/course/view.php?id=19546&section=24), [JWT](https://moodle.zealand.dk/course/view.php?id=19546&section=25).

## F. Brugerens fastlagte projektbeslutninger

1. Gæster må se produkter og lægge varer i kurven.
2. Checkout kræver login eller ny konto. Kurven følger automatisk med gennem begge forløb.
3. Kunder må kun se egne tidligere ordrer. Login beskyttes mod brute force.
4. Ingen lagerreservation i kurven. Lagertræk sker først ved gennemført køb.
5. Lagerkontrol, lagertræk og ordreoprettelse skal være konsistente under samtidige køb. Ved utilstrækkeligt lager afvises hele transaktionen; lageret må aldrig blive negativt.
6. Betaling er en lokal simulation med automatisk godkendelse, uden betalingsudbyder, saldo eller tegnebog. Priser vises og gemmes som købsværdier; intet opkræves.
7. Automatisk betalingsgodkendelse ophæver ikke validering, lagerkontrol eller fejlhåndtering.
8. MFA og password recovery implementeres ikke i den aktuelle version.
9. Kun tema 1–5 er aktuelt. Senere temaers særskilte opgaver udskydes.
10. En delopgave ad gangen. Efter hver afslutning leveres status og en commit-besked; ingen automatisk Git-commit.

## G. Ændringer til den tidligere opgaveliste

- 1.1 er afsluttet med denne kravoversigt. 1.2 er ikke påbegyndt.
- Dokumenteret passwordpolitik med OWASP/NIST/CFCS-grundlag gøres eksplicit som en forudsætning for 3.3–3.7.
- 3.4 præciseres til en 0–100-score og forståelig vejledning.
- 3.8 udgår af den aktuelle version (recovery/MFA).
- 4.4–4.5 udvides med __Host--vurdering, adskillelse af kurvesession og loginidentitet samt ny autentifikationstoken ved login.
- 5.1–5.6 præciseres med Content-Type, HTTP-metoder, nosniff, CORS og clickjacking-beskyttelse.
- 8.2 omfatter audit af læsning af ordredata, ud over administratorændringer og køb.
- 7.1 skal bruge lagertræk ved køb, ikke en reservationsmodel.
- 7.5 er en lokal automatisk betalingssimulation.
- Tema 6-specifikke injectionøvelser, tema 7-specifik penetrationstest/ZAP og generel udvidelse til senere temaer udskydes. Relevante funktions- og sikkerhedskontroller gennemføres stadig ved de opgaver, vi implementerer.
- De 11 user stories bevares som samlet backlog. At en story senere understøttes af tema 6 eller 7 gør ikke automatisk storyens funktionalitet irrelevant; den detaljerede prioritering udarbejdes først i 1.2.

## H. Åbne punkter og kildebegrænsninger

- STRIDE-eksemplet kræver beskyttelse af ordredata i hvile, men materialet specificerer ikke felt-, database- eller diskkryptering. Det noteres til 2.7; der vælges ingen teknik i 1.1.
- Passwordpolitikkens tal og algoritmeparametre skal vælges og begrundes ved den relevante opgave efter kontrol af de officielle kilder. Eksempler i slides er ikke i sig selv en fast politik.
- Materialet blander ASVS-henvisninger fra forskellige versioner. Versionsnummer og kontrol-id skal verificeres før en senere compliance-mapping; der hævdes ingen fuld ASVS-overensstemmelse.
- JWT-eksemplet bruger localStorage, mens cookie-materialet advarer mod følsomme data dér. Eksemplet kopieres derfor ikke som vores sikkerhedsarkitektur.
- Undervisningsplan og slides har forskellige datoangivelser for enkelte temaer. Introen nævner 15.–16. november uden tydelig årstalsangivelse i datoen. En aktuel eksamens-/fremlæggelsesdato er ikke verificeret her.
- DOCX-skabeloner og eksterne videoer er registreret som hjælperessourcer, men indholdet er ikke gennemgået. Skabelonerne fremgår som støtte til passwordpolitik og acceptkriterier, ikke som yderligere selvstændige krav i det læste materiale.
- Dette er en afgrænsning af den aktuelle version; ikke en garanti for at fravalg opfylder samtlige fremtidige fagkrav.

## I. Gennemgåede filer og kilder

[Undervisningsmappen på Moodle](https://moodle.zealand.dk/mod/folder/view.php?id=501213) indeholdt ved gennemgangen:

| Fil | Sider | Anvendelse |
|---|---|---|
| 26-08-2026 Tema 1 - Introduktion til softwaresikkkerhed.pdf | 59 | Baggrund om sikkerhed, CIA/FIT og sikkerhed gennem udviklingen. |
| 02-09-26 Tema 2 - Trusler.pdf | 40 | Angrebsoverflade, STRIDE og eksempel på sikkerhedsacceptkriterier. |
| 08-09-2026 Tema 3 - Password - del 1.pdf | 39 | Passwordpolitik, registrering, Pass’O meter og hashing. |
| 16-09-2026 Tema 3 - Password del 2.pdf | 39 | Rate limiting, policyopdatering, recovery og MFA-afgrænsning. |
| 02-10-2026 Tema 4 - Milk and cookies - del 1.pdf | 32 | HTTPS/HSTS, HTTP-metoder, Content-Type, CSP og CORS. |
| 09-10-2026 Tema 4 - Milk and cookies - del 2.pdf | 25 | Cookies, CSRF, sessioner og kurv. |
| Undervisningsplan 1.02.pdf | 2 | Forløbsoversigt; ikke autoritativ bekræftelse af en aktuel afleveringsdato. |

Tema 5 blev gennemgået på Moodle; der lå ikke særskilte tema 5-slides i den viste undervisningsmappe. PDF-tekst er udtrukket til analyse, og centrale sider med STRIDE-acceptkriterier, Content-Type/CORS-opgaver og MFA-afgrænsning er også kontrolleret visuelt.

## J. Commit-besked

Denne dokumentation er gemt i projektets docs-mappe. Følgende commit-besked kan bruges:

`docs(requirements): document theme 1-5 requirements and agreed scope`

Filen docs/kravoversigt.md er tilføjet i projektets mappe. Der er ikke lavet nogen Git-commit. Projektkode og database er uændret.


## Opdatering efter del 1

21. oktober 2026: 1.2–1.4 er dokumenteret i acceptkriterier.md, afklaringer.md og trusselsmodel.md. Baseline i baseline.md viser et bestået build og en fortsat blokeret databaseforbindelse. Ingen webshopfunktioner er implementeret i del 1. På brugerens efterfølgende anmodning samles webprojekt og bibliotek i samme repository, og database-login flyttes til lokale User Secrets før publicering. Denne afgrænsede klargøring ændrer ikke de planlagte webshopflows. De tidligere statusafsnit ovenfor beskriver tilstanden ved levering af 1.1.
