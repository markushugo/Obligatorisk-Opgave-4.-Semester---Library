# 1.2 – Acceptkriterier for webshoppen

Dato: 21. oktober 2026. Status: krav beskrevet; funktionerne er ikke implementeret eller godkendt ved test i denne opgave.

Grundlag: de 11 brugerhistorier i den udleverede PDF, [kravoversigten](kravoversigt.md), Moodle-tema 1–5 og brugerens præciseringer om gæstekurv, kontokrav, samtidighed og lokal betalingssimulation. Kriterierne nedenfor er vores konkrete operationalisering; de er ikke ordrette krav fra underviseren.

## Omfang og kontrolform

Alle 11 historier beholdes i den funktionelle backlog. Tema 6/7 som selvstændige undervisningsforløb, penetrationstest og ZAP er udskudt. Grundlæggende validering, sikre SQL-parametre og kontrol af autorisation fra tema 1–5 bevares. MFA og password recovery er fravalgt; almindelig passwordændring er med.

Hvert kriterium har et stabilt id. Kontrolscenarierne udføres senere lokalt med testdata. Et beskrevet scenarie er ikke bevis på, at løsningen allerede virker. Tværgående kriterier F01–F12 gælder for alle relevante historier. Konkrete password- og loginbegrænsninger fastlægges i passwordpolitikken; der opfindes ikke tal her.

## US 1 – Oprettelse af bruger

Formål: oprette en kundekonto og beskytte kundens data. Tema 2, 3, 4 og 5. Opgaver 2.3–2.8, 3.1–3.4 og 4.4–4.5.

| ID | Acceptkriterium |
|---|---|
| US01-A1 | En gæst kan registrere sig med de obligatoriske felter, som datamodellen fastlægger. Kun nødvendige persondata indsamles. |
| US01-A2 | Serveren håndhæver den dokumenterede passwordpolitik og de dokumenterede feltgrænser, også når klientvalidering omgås. |
| US01-A3 | Et login-id kan kun tilhøre én konto efter den valgte normalisering. To samtidige registreringer med samme login-id opretter højst én konto. |
| US01-A4 | Databasen gemmer en egnet passwordhash med individuelt salt; passwordet gemmes ikke i klartekst eller logs. |
| US01-A5 | En ny konto tildeles kunderettigheder af serveren. Indsendte felter som Role, IsAdmin eller UserId kan ikke give forhøjede rettigheder. |
| US01-A6 | Ved registrering fra checkout bevares gæstekurvens produkt-id'er og antal. Brugeren kan fortsætte til checkout efter etableret login; automatisk login versus efterfølgende login er et implementeringsvalg. |
| US01-A7 | Afvist registrering opretter ingen delvis konto, tømmer ikke kurven og viser brugbar feedback uden interne fejl. |

Kontrol: registrer en ny kunde; prøv samme login-id igen og samtidigt i to requests; omgå klientvalidering; indsend en administratorrolle; kontroller lagret hash og logs; gennemfør registrering fra en kurv med flere varer og kontroller indholdet bagefter.

## US 2 – Login

Formål: sikker adgang til egen konto og tidligere ordrer, beskyttet mod brute force. Tema 2–5. Opgaver 3.5–3.6 og 4.2–4.6.

| ID | Acceptkriterium |
|---|---|
| US02-A1 | Gyldige legitimationsoplysninger giver en serververificeret loginidentitet. Forkerte oplysninger giver ikke adgang. |
| US02-A2 | Forkert password og ukendt login-id giver samme generiske fejltekst uden direkte oplysning om kontoens eksistens. |
| US02-A3 | Gentagne mislykkede forsøg begrænses efter dokumenterede regler for tælling, tidsvindue, blokering og ophævelse. Parallelle requests kan ikke omgå tællingen. |
| US02-A4 | Kontobeskyttelse gælder også ved skift af IP. En enkelt blokeret konto må ikke automatisk låse alle andre konti på skolens delte IP. Eventuel supplerende IP-begrænsning dokumenteres særskilt. |
| US02-A5 | Ved login udstedes en ny beskyttet autentifikationscookie. Identiteten må ikke kunne etableres ved at ændre et bruger-id eller en kurvesession. |
| US02-A6 | Gæstekurven bevares uden tab eller dublering ved login, og brugeren kan vende tilbage til lokal checkout. En ekstern retur-URL accepteres ikke. |
| US02-A7 | Logout afslutter loginadgangen. En efterfølgende forespørgsel til en privat side kræver login, og kontoskift viser ikke den tidligere brugers private oplysninger. |

Kontrol: gyldigt og ugyldigt login; ukendt konto; forsøg lige under/ved/over den senere valgte grænse; parallelle forsøg; udløb af blokering; to konti bag samme IP; login fra kurv; ekstern retur-URL; logout og kontoskift. Beskyttelsen må ikke afhænge af at en knap er skjult.

## US 3 – Gennemse produkter

Formål: vise tilgængelige produkter sikkert. Tema 2 og 4. Opgaver 2.8, 5.1–5.6 og 6.1.

| ID | Acceptkriterium |
|---|---|
| US03-A1 | Gæster og kunder kan se tilgængelige produkter med navn, pris og relevant lagerstatus. |
| US03-A2 | Manglende billede, tom produktliste og ukendt produkt-id håndteres med en brugbar side uden interne fejloplysninger. |
| US03-A3 | Visning ændrer ikke lager, kurv eller ordredata. |
| US03-A4 | Produktnavn og beskrivelse vises som tekst. Billedkilder følger den dokumenterede politik for tilladte URL'er og ressourcer. |
| US03-A5 | Requestparametre valideres på serveren, og databaseopslag bruger SQL-parametre. |

Kontrol: gennemse som gæst; tomt katalog; produkt uden billede; ukendt/ugyldigt id; produkttekst med HTML-specialtegn; sammenlign lager før og efter almindelig visning.

## US 4 – Lægge varer i kurven

Formål: en isoleret gæste-/kundekurv uden manipulation af andre brugeres kurve. Tema 2 og 4. Opgaver 4.4–4.5 og 7.1–7.3.

| ID | Acceptkriterium |
|---|---|
| US04-A1 | En gæst kan lægge et eksisterende, tilgængeligt produkt i kurven uden konto. |
| US04-A2 | Gentagen tilføjelse af samme produkt opdaterer antallet. Antal kan ændres, en vare fjernes, og hele kurven ryddes. |
| US04-A3 | Positive heltal og dokumenterede maksimumgrænser håndhæves på serveren. Ugyldigt input, overløb og ukendte produkter afvises uden delvise ændringer. Nul i antal-opdateringen fjerner varen. |
| US04-A4 | Tilføjelse, ændring, fjernelse, rydning og sessionudløb reserverer eller ændrer aldrig lageret. |
| US04-A5 | To separate browsersessioner har isolerede kurve. Manipulation af indsendte kunde-/kurve-id'er giver ikke adgang til en anden kurv. |
| US04-A6 | Kurvens priser kommer fra serverens produktdata; indsendte pris-/totalfelter er ikke autoritative. Checkout genvaliderer priser og lager. |
| US04-A7 | Kurven bevares gennem login og registrering. Sessionudløb giver en tom/udløbet kurv med forståelig besked og opretter ikke en ordre. |
| US04-A8 | Samtidige ændringer fra samme session håndteres efter en dokumenteret strategi, så mistede opdateringer eller dublering ikke skjules som vellykkede. |

Kontrol: tilføj før login, tilføj igen, opdater, fjern og ryd; indsend negative, brøk-, tekst- og meget store antal; brug to browserprofiler; ændr pris i requesten; lad sessionen udløbe; send parallelle ændringer. Lageret skal være identisk før og efter alle kurveoperationer.

## US 5 – Checkout og lokal købssimulation

Formål: registrere et gennemført køb uden opkrævning. Tema 2, 4 og 5 samt brugerens eksplicitte transaktionskrav. Opgaver 2.9 og 7.3–7.5.

| ID | Acceptkriterium |
|---|---|
| US05-A1 | Checkout kræver en konto og gyldigt login. En gæst tilbydes login eller registrering uden at miste kurven. Et direkte gæste-POST kan ikke gennemføre køb. |
| US05-A2 | En tom kurv kan ikke købes. Serveren kontrollerer produkters gyldighed, antal, aktuelle priser og tilstrækkeligt lager ved gennemførsel. |
| US05-A3 | En lokal mock/service godkender automatisk betalingsdelen. Der kræves ingen kortdata, betalingsudbyder, saldo, tegnebog eller dækning. Intet beløb opkræves. |
| US05-A4 | Ordren gemmer produkt-id, historisk navn/enhedspris, antal, total, kunde-id, tidspunkt og status gennemført. Ordretotalen er varernes viste købsværdi, ikke et faktisk opkrævet beløb. |
| US05-A5 | Lagerkontrol, lagertræk, ordre og ordrelinjer gennemføres atomisk i databasen. Mangler én vare, rulles hele købet tilbage. Lager kan aldrig blive negativt. |
| US05-A6 | Når to kunder samtidigt køber den sidste enhed, oprettes præcis én gennemført ordre for enheden. Den anden får lagerafvisning uden lagertræk eller delvis ordre. |
| US05-A7 | Gentagne indsendelser af samme checkout-forsøg skaber højst én ordre og ét lagertræk, også ved parallelle requests eller genforsøg efter tabt svar. En serverkontrolleret identifikator knyttes til kunden og købsforsøget. |
| US05-A8 | Kurven ryddes først efter bekræftet databasecommit. Ved afvist køb bevares den til rettelse. Efter genforsøg for et allerede gennemført køb vises samme ordre uden at købe igen. |
| US05-A9 | Browseren kan ikke vælge ordre-ejer, ændre autoritativ pris eller sætte gennemført-status ved indsendte felter alene. |

Kontrol: køb med login; gæste-POST; tom kurv; manipuleret pris/kunde-id; utilstrækkeligt lager for én af flere varer; to samtidige kunder mod lager 1; dobbeltsubmit; kontrolleret databasefejl før commit; genforsøg efter gennemført køb. Kontroller både ordretabeller, lager og kurv.

## US 6 – Ændring af adgangskode

Formål: en bruger kan ændre eget password ved mistanke om kompromittering. Tema 3–5. Opgave 3.7.

| ID | Acceptkriterium |
|---|---|
| US06-A1 | Kun en logget ind bruger kan ændre eget password. Kontoen identificeres fra den serververificerede identitet. |
| US06-A2 | Nuværende password skal verificeres. Nyt password og bekræftelse skal matche og opfylde samme politik som registrering. |
| US06-A3 | Et afvist forsøg ændrer ingen legitimationsoplysninger. Gentagne forsøg med forkert nuværende password begrænses efter den dokumenterede politik. |
| US06-A4 | Et godkendt nyt password gemmes som en ny hash. Det gamle password virker ikke ved efterfølgende login. |
| US06-A5 | Andre eksisterende login-sessioner ugyldiggøres efter den valgte sessionvalideringsmekanisme. Den aktuelle session fornyes eller kræver nyt login; adfærden dokumenteres. |
| US06-A6 | Hændelsen auditeres uden password, hash eller sessiontoken. Der tilføjes ingen recovery- eller MFA-funktion. |

Kontrol: gyldig ændring; forkert nuværende password; forskellig bekræftelse; svagt password; andet indsendt bruger-id; forsøg fra gæst; to aktive browserlogins, hvor det ene ændrer password. Kontroller gammelt/nyt login og adgang fra den anden session.

## US 7 – Søgning

Formål: finde produkter sikkert. Tema 2 og 4; tema 6's selvstændige undervisningsøvelser er udskudt. Opgave 6.2.

| ID | Acceptkriterium |
|---|---|
| US07-A1 | Gæster og kunder kan søge blandt tilgængelige produkter og se relevante resultater. Søgefelter og tom søgnings adfærd dokumenteres. |
| US07-A2 | Søgetekst har en dokumenteret længdegrænse og begrænset resultatmængde. Serveren afviser input over grænsen. |
| US07-A3 | Søgeværdier sendes som SQL-parametre, og søgeresultater samt gengivet søgetekst outputencodes. |
| US07-A4 | Ingen resultater vises forståeligt. Fejl afslører ikke SQL, credentials eller databaseopbygning. |
| US07-A5 | Søgning ændrer ikke data og returnerer ikke bruger-, log- eller ordredata. |

Kontrol: eksisterende og ikke-eksisterende produkt; tom søgning; tekst med apostrof og HTML-specialtegn; input ved og over grænsen; gentagne opslag. Særskilte injection-/penetrationstestforløb kræves ikke i denne del.

## US 8 – Ordrehistorik

Formål: adgang til egne historiske køb. Tema 2, 4 og 5. Opgaver 4.3, 7.6 og 8.2.

| ID | Acceptkriterium |
|---|---|
| US08-A1 | Kun loggede ind kunder har adgang til deres ordrehistorik og ordredetaljer. |
| US08-A2 | Ordre-ejerskab kontrolleres på serveren for både oversigt og enkeltordre. Et andet ordre-/kunde-id giver ikke adgang eller afslører ordredetaljer. |
| US08-A3 | Historikken viser gennemførte køb med oprindelige antal, priser, total, tidspunkt og ordreidentifikation. Senere produktændringer omskriver ikke tidligere køb. |
| US08-A4 | En kunde uden ordrer får en tom oversigt, og ukendte/ugyldige id'er giver kontrolleret feedback. |
| US08-A5 | Adgang til ordredata og afviste adgangsforsøg auditeres uden at kopiere unødvendige person-/ordredata til logs. |
| US08-A6 | Private svar caches ikke på en måde, der viser en tidligere kundes ordrehistorik efter logout eller kontoskift. |

Kontrol: opret køb for kunde A og B; vis egen historik; lad A forespørge på B's ordre-id og kunde-id; prøv som gæst; ændr efterfølgende produktpris og kontroller historikken; prøv efter logout og kontoskift.

## US 9 – Administration af produkter

Formål: administrator kan vedligeholde kataloget. Tema 2, 4 og 5. Opgaver 4.1–4.2 og 6.3–6.4.

| ID | Acceptkriterium |
|---|---|
| US09-A1 | Kun administratorer kan åbne administrationssider og udføre produktændringer. Kontrol gælder både GET og alle POST-handlers. |
| US09-A2 | Administrator kan oprette, redigere og slette/deaktivere produkter med dokumenterede krav til navn, beskrivelse, pris, lager og billedkilde. |
| US09-A3 | Negative priser og lagerantal samt ugyldige id'er afvises på serveren uden delvise ændringer. |
| US09-A4 | Ændringer i et administrationsskærmbillede må ikke overskrive lagertræk fra samtidige køb med en gammel lagerkopi. Konflikter håndteres eller ændringen udføres atomisk. |
| US09-A5 | Sletning af et produkt må ikke fjerne historiske ordrelinjer. Er fysisk sletning uforenelig med relationer, bruges deaktivering med tydelig feedback. |
| US09-A6 | Oprettelse, ændring og sletning/deaktivering auditeres med aktør, handling, produkt-id, tidspunkt og resultat. |
| US09-A7 | En kundes registrering, profilinput eller manipuleret request kan ikke tildele administratorrettigheder. |

Kontrol: alle handlinger som administrator, kunde og gæst; direkte POST uden menuadgang; negative værdier; redigering parallelt med køb; deaktiver et tidligere købt produkt og kontroller historikken.

## US 10 – Logning og overvågning

Formål: systemadministrator kan identificere mistænkelig aktivitet, mens logdata beskyttes. Tema 2 og 5. Opgaver 8.1–8.4.

| ID | Acceptkriterium |
|---|---|
| US10-A1 | Relevante loginforsøg, blokeringer, passwordændringer, adgangsafvisninger, ordreopslag, produktændringer og købsresultater registreres. |
| US10-A2 | En hændelse har tidspunkt, hændelsestype, resultat, relevant intern aktør-/ressourceidentifikation og korrelations-id, hvor relevant. Uautentificerede hændelser påstår ikke en verificeret identitet. |
| US10-A3 | Logs indeholder ikke passwords, passwordhashes, databasecredentials, cookies, tokens eller komplette requestbodies med følsomme oplysninger. |
| US10-A4 | Kun relevante administratorer/driftskonti kan læse logs. Logs kan ikke hentes som statiske webfiler af kunder eller gæster. En webbaseret logportal er ikke et krav. |
| US10-A5 | Brugerinput i logs kan ikke fremstille falske ekstra logposter via linjeskift; struktureret logning anvendes. |
| US10-A6 | Logplacering, adgang, opbevaring/rotation og procedure for gennemgang er dokumenteret. Gentagne loginfejl og adgangsafvisninger kan findes ved gennemgangen. |
| US10-A7 | Fejl i audit-/loglagring håndteres efter en dokumenteret strategi uden at lække data eller omgå købs-/adgangskontrol. |

Kontrol: fremkald de aftalte hændelser; find dem via procedure; prøv logadgang som kunde/gæst; søg efter kendte testhemmeligheder; kontroller linjeskift som input og en kontrolleret fejl i logdestinationen. Ingen ekstern overvågningstjeneste kræves.

## US 11 – Hjælp til password

Formål: hjælpe brugeren med at vælge et stærkere password. Tema 3. Opgave 3.4.

| ID | Acceptkriterium |
|---|---|
| US11-A1 | Registreringssiden og siden til passwordændring viser forståelige passwordregler og styrkevejledning. |
| US11-A2 | Pass’O meter giver en score fra 0–100 med en tekstlig vurdering og konkrete forbedringsforslag efter den dokumenterede algoritme. |
| US11-A3 | Scoregrænser og testeksempler dokumenteres. Tomt, almindeligt/forudsigeligt og længere varieret input vurderes konsistent efter reglerne. |
| US11-A4 | Styrkeindikatoren garanterer ikke sikkerhed og erstatter ikke serverens passwordpolitik. Omgået JavaScript kan ikke få et ugyldigt password accepteret. |
| US11-A5 | Passwordet sendes ikke til tredjepartstjenester og gemmes ikke af måleren i browserstorage, analytics eller logs. |
| US11-A6 | Feedback kan forstås uden farver alene og er knyttet til det relevante inputfelt. |

Kontrol: tomt og forudsigeligt input samt dokumenterede grænseeksempler; sammenlign score med algoritmen; slå JavaScript fra og indsend ugyldigt password; kontroller netværkskald og storage; kontroller feedback med tastatur og tekst.

## Tværgående kriterier fra tema 1–5

| ID | Kriterium | Kravoversigt |
|---|---|---|
| F01 | Løsningen bruger .NET/Razor Pages, Class Library og MSSQL via ADO.NET uden ORM og kan køres lokalt. | G1–G4 |
| F02 | Autorisation og ejerskab håndhæves på serveren med mindst mulige rettigheder. SQL-kontoen har dokumenterede begrænsninger. | T3–T4, A1–A3 |
| F03 | Inputgrænser håndhæves i det betroede servicelag og suppleres af relevante databaseconstraints. Databaseværdier bruger SQL-parametre. | T7 |
| F04 | HTTPS beskytter login og private flows. HSTS er konfigureret for relevant miljø med dokumenteret lokal testbegrænsning. | W1 |
| F05 | Login-/sessionscookies har dokumenterede sikkerhedsflag og udløb. Ingen tokens i URL'er; __Host--muligheden vurderes. | T8, W7, W9–W10 |
| F06 | Tilstandsændrende requests har valideret antiforgery-beskyttelse. Ugyldigt/manglende token giver ingen ændring; SameSite alene er ikke acceptbevis. | W8 |
| F07 | HTTP-metoder og bodyformater er begrænset efter endpointets behov. Uunderstøttede metoder/formater afvises passende; almindelige formularer og statiske ressourcer virker. | W2–W3 |
| F08 | Svar har passende Content-Type/tegnsæt, nosniff og CSP tilpasset appen, herunder kontrol af indlejring. | W4–W5 |
| F09 | CORS åbner ikke unødvendigt for andre origins. Eventuelle tilladte origins er eksplicit dokumenteret. | W6 |
| F10 | Fejlsvar og logs afslører ikke hemmeligheder, SQL eller interne stacktraces til brugeren. | T3, T7 |
| F11 | Ordredata beskyttes i hvile efter en dokumenteret løsning. Teknik og konkret kontrol afventer databaseopgave 2.7; kriteriet er ikke afkrydset. | T5 |
| F12 | Priser/totaler bruger decimalpræcision; køb og lager beskytter integritet under samtidighed og fejl. | T1 og brugerens projektbeslutninger |

## Kontroldata og dokumentation af resultat

Brug separate testkonti for kunde A, kunde B og administrator samt separate browsersessioner. Til samtidighedskontrol anvendes et testprodukt med lager 1 og en ordre med flere produkter, hvor ét mangler. Testadgangskoder og databasehemmeligheder medtages ikke i rapporten.

Ved implementering registreres for hvert relevant id: dato, miljø/commit, forudsætninger, handling, forventet/faktisk resultat og evidens uden hemmeligheder. Status er enten ikke udført, bestået, fejlet eller blokeret med begrundelse. Alle id'er i dette dokument står ved levering som **ikke udført**.

## Afklaringer før implementering

- Passwordpolitikkens konkrete minimum/maksimum, scorealgoritme og parametre samt loginbegrænsningernes tærskler fastlægges i den relevante opgave.
- Felter og entydighed for login-id/e-mail fastlægges ved brugermodellen. Ingen ny e-mailtjeneste eller mailverifikation er antaget her.
- Kryptering i hvile fastlægges i 2.7. Beskyttelse af filer og kryptering af konkrete felter har forskellige egenskaber.
- Nøjagtige kurvegrænser, cookielevetider og konfliktstrategier dokumenteres ved implementeringen.
- Historiske køb er ikke reelle betalinger. Visning må ikke påstå, at penge er hævet.

Commit-forslag: `docs(requirements): define acceptance criteria for all webshop user stories`
