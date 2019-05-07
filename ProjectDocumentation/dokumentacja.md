# Inteligentny tramwaj
## Spis treści
1. [Opis projektu](#1)
2. [Skład zespołu](#2)
3. [Baza danych i serwer aplikacyjny](#3)
4. [Aplikacja mobilna](#4)
5. [Sieć neuronowa](#5)
6. [Technologia beacon](#6)
## 1. Opis projektu<a name="1"></a>
Celem projektu ***Inteligentny tramwaj*** jest stworzenie systemu informatycznego, który umożliwi naliczanie opłat za korzystanie z usług transportowych w sposób jak najbardziej intuicyjny i korzystny dla użytkowników komunikacji miejskiej.<br/>
System będzie składał się z urządzeń beacon znajdującyh się w pojazdach komunikacji miejskiej, aplikacji moblinej dla użytkowników końcowych oraz serwera aplikacyjnego. Ogólny diagram działania systemu znajduje się poniżej. Poszczególne komponenty zostały opisane w stosownych rozdziałach.
<br/>

|<img src="graf.jpg" height="500"></img>|
|:--:| 
| *Diagram przedstawiający działanie systemu* |
```diff
+ Dodać diagram sekwencji (lub diagramy dla poszczegónych elementów systemu??)
```
#### Funkcjonalności: ####
- Nadawanie danych dotyczących pojazdu i jego stanu przez urządzenie beacon
- Odbiór danych przez aplikację mobilną w telefonie
- Pozyskiwanie dodatkowych danych telemetrycznych z sensorów w telefonie
- Komunikacja aplikacji mobilnej z serwerem
- Określanie stanu użytkownika przez sieć neuronową, na podstawie dostarczonych danych i naliczanie opłat
- Obsługa kont użytkoników (rejestracja, autoryzacja, wybór sposobu płatności)
- Weryfikacja uprawnień przejazdowych przez kontrolera biletów
- Wysyłanie powiadomień wypychanych o rozpoczęciu i zakończeniu podróży (w zależności od preferencji użytkownika)
- Naliczanie opłat w przypadku utraty połączenia z telefonem
## 2. Skład zespołu<a name="2"></a>
### Prowadzący: *mgr inż. Bartosz Wieczorek* 
```diff
- Zweryfikować podział zadań
```
### Baza danych i serwer aplikacyjny:
- Yaroslav Goretskyi
- Krzysztof Wierzbicki
- Przemysław Fortuna
- Jakub Przybylski
- Paweł Młynarczyk
- Dariusz Syncerek
- Konrad Kowalczyk
### Aplikacja mobilna:
- Paweł Ciupka
- Adam Lindner
- Michał Szwarocki
### Sieć neuronowa:
- Krzysztof Pilcicki
### Technologia beacon:
- Przemysław Brzoska
- Dorian Grabarczyk
### Dokumentacja:
- Jan Kisielewicz
- Mateusz Wadlewski<br/><br/><br/>
## 3. Baza danych i serwer aplikacyjny<a name="3"></a>
Aplikacja korzysta z bazy danych *Microsoft SQL* znajdującej się w chmurze *Microsoft Azure*. Strukturę bazy prezentuje poniższy diagram.
```diff
- W związku ze zmianą bazy zmienić opis i diagram encji
```
<br/>

|![](db.jpg)|
|:--:| 
| *Diagram ERD przedstawiający strukturę bazy danych* |

Link do repozytorium: [https://github.com/PrzemekFortuna/iTram](https://github.com/PrzemekFortuna/iTram)<br/>
Do komunikacji z aplikacją mobilną i przetwarzania pozyskanych danych utworzono serwer aplikacyjny w technologii *ASP.NET Core*.
API serwera znajduje się pod adresem: [http://itram.azurewebsites.net](http://itram.azurewebsites.net)<br/>
``` diff
+ Dodać diagram klas UML
```

#### Dostępne endpointy:
Endpointy opisane są na stronie: [http://itram.azurewebsites.net](http://itram.azurewebsites.net)
``` diff
+ Opisać brakujące endpointy w swaggerze
```
## 4. Aplikacja mobilna<a name="4"></a>
Link do repozytorium:<br/>
*Będzie.*
``` diff
+ Dodać link do repozytorium
+ Dodać krótki opis
+ Dodać diagram klas UML
+ Dodać instrukcję korzystania z aplikacji i informację o wykorzystywanych danych telemetrycznych
```
## 5. Sieć neuronowa<a name="5"></a>
Link do repozytorium: <a href="https://github.com/kpilcicki/problem-workshop-net-poc">https://github.com/kpilcicki/problem-workshop-net-poc</a> <br/>
Sieć neuronowa ma na celu, na podstawie otrzymanych od aplikacji mobilnej danych, określić czy dana osoba znjaduje się wewnątrz pojazdu komunikacji miejskiej. Docelowo aplikacja zostanie zintegrowana z serwerem.<br/>
Sieć została przygotowana w języku programowania *Python* z wykorzystaniem frameworku *TensorFlow*.
``` diff
+ Dodać diagram klas UML po przeniesieniu na docelowy serwer
+ Dodać wyniki testów (w przyszłości jak już będą)
```

## 6. Technologia beacon<a name="6"></a>
W obecnej wersji systemu w zastępstwie beacona wykorzystujemy *Rasberry Pi*, które pobiera informacje nt. lokalizacji za pomocą modułu *GPS*. Następnie nadaje, korzystając z *Bluetooth*, sygnał składający się z *id* urządzenia oraz współrzędnych zapisanych w postaci `xxx.xxxxxx`, gdzie `x ∈ {0...9}`. Liczba dopełniana jest "`0`" z prawej strony oraz "`3`" z lewej strony.

``` diff
+ Dodać diagram prezentujący informacje o zabezpieczeniach
+ Dodać informację o wykorzystanym modelu RasberryPi i sensorach, dodać informację o oprogramowaniu.
```
## Miejsce na uwagi:
