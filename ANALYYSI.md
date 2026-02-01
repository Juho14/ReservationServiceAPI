Analyysi

1. Mitä tekoäly teki hyvin?

- Tekoäly tuotti syntaksin puolesta toimivaa koodia.
- Yleiset käytännnöt ovat sille pääosin tuttuja.
- Se pystyi tuottamaan koodia nopeasti, mikä nopeutti kehitysprosessia.
- Tekoäly osasi hyödyntää viestiketjun kontekstia, minkä kautta
se pystyi tuottamaan koodia, joka liittyi aiemmin keskusteltuihin aiheisiin.

2. Mitä tekoäly teki huonosti?

- Koska minulla oli selkeä rakenne jo mielessä projektia tehdessä,
tuotti tekoäly usein koodia, joka ei sopinut haluamaani arkkitehtuuriin.
- Tekoälyllä oli vaiheittain vaikeuksia ymmärtää projektin laajempaa kontekstia.
- Tekoäly ei aina noudattanut haluttuja käytäntöjä, minkä seurauksena koodissa
oli toistuvia rakenteita. Muun muassa datan muotoilussa se suosi in-line ratkaisuja
geneerisempien ratkaisujen sijaan.
- Tietokantakyselyt olivat myös toistuvia yhtenäisen logiikan sijaan.
- Käytin tässä ChatGPT:tä, joka päätti muokata aikaisempia vastauksia jatkuvasti, mikä
vaikeutti historian seurantaa.

3. Mitkä olivat tärkeimmät parannukset, jotka teit tekoälyn tuottamaan koodiin ja miksi?

- Keskitin kyselyt, data-muunnokset ja muut toistuvat rakenteet. Koodin luettavuus ja kehitettävyys
on paljon parempi, kun logiikka on keskitetty yhteen paikkaan. Jos mitään pitää muuttaa, tarvitsee
koskea vain yhteen paikkaan. Navigaatio-kyselyt pidin vielä funktiotasolla, jotta niitä voi muuttaa
helposti funktiokohtaisesti.
- Tyypityksen vahvistaminen. Muutin statuksen vahvaksi Enumiksi ja vavhistin validoinnin palautuksen.
Tämä poistaa mm. string-vertailut ja tekee koodista selkeämpää.
- Controllerien siistiminen. Yksi geneerinen funktio palauttaa vastauksen kaikille endpointeille.
Koska käytin jo valmiiksi geneeristä Result<T>-rakennetta, pystyin hyödyntämään sitä controllereissa.
Tämä tekee logiikasta todella yksinkertaista, ja helpottaa uusien endpointien lisäämistä.

4. Omat huomiot tekoälyn käytöstä ohjelmistokehityksessä.

- Minulla on tapana miettiä rakenne jo aika pitkälle ennen koodaamisen aloittamista.
Tässä tehtävässä siitä oli haittaa, koska tehtävänantona oli nimenomaan tekoälyn koodin käyttö. Jos olisi
heti alusta asti voinut käyttää tekoälyä vain apurina, olisi prosessi ollut luonnollisempaa.
- Tekoäly oli kuitenkin hyödyllinen kaveri, kunhan sain selitettyä koodin rakenteen tarkkaan mietityillä
prompteilla.