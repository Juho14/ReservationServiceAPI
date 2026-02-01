Palvelun tulee tarjota käyttäjille seuraavat toiminnot:
● Varauksen luonti: Varaa huone tietylle aikavälille.
● Varauksen peruutus: Poista varaus.
● Varausten katselu: Listaa kaikki tietyn huoneen varaukset.
Toimintalogiikka (business rules):
● Varaukset eivät saa mennä päällekkäin (kaksi henkilöä ei voi varata samaa huonetta
samaan aikaan).
● Varaukset eivät voi sijoittua menneisyyteen.
● Aloitusajan täytyy olla ennen lopetusaikaa.

Omat oletukset:

Varausta ei voi muokata varauksen ajankohdan jälkeen.
Admin käyttäjää tai muita rooleja ei ole määritelty tässä palvelussa.

Poistamislogiikalle ei ole asetettu rajoituksia. Poisto on sallittus milloin tahansa.

Versiot:

.NET 10, Entity Framework Core 10