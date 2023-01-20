# Opis rzeczywistego problemu: </br>
Projekt dotyczący rozpoznawania twarzy w czasie rzeczywistym polega na stworzeniu systemu, który jest w stanie rozpoznać twarz danej osoby za pomocą kamery lub innego urządzenia wykonującego zdjęcia. System ten może być wykorzystywany w różnych celach, takich jak dostęp do pomieszczeń zabezpieczonych, identyfikacja osób na lotniskach, identyfikacji osoby podczas odblokowywania telefonu lub na przykład w systemach monitorujących ruch na ulicach. Projekt może wykorzystywać różne algorytmy uczenia maszynowego, takie jak konwolucyjne sieci neuronowe (CNN), aby rozpoznać twarze.
Projekt dotyczący rozpoznawania twarzy w czasie rzeczywistym składa się z kilku głównych komponentów:
* Kamera lub inne urządzenie wykonujące zdjęcia - to urządzenie jest odpowiedzialne za przechwytywanie obrazów twarzy osób, które chcemy rozpoznać.
* Algorytm rozpoznawania twarzy - to algorytm jest odpowiedzialny za rozpoznawanie twarzy na podstawie przechwyconych obrazów. Może to być algorytm oparty na konwolucyjnych sieciach neuronowych (CNN), które są bardzo skuteczne w rozpoznawaniu obrazów.
* Baza danych twarzy - system potrzebuje bazy danych twarzy, zawierającej zdjęcia twarzy osób, które chcemy rozpoznać. Baza danych ta jest używana do porównywania przechwyconych obrazów z zapisanymi w niej twarzami i wykrycia, czy dana twarz jest znana czy nieznana.
* Interfejs użytkownika - system musi mieć prosty interfejs użytkownika, który pozwala na dodawanie i usuwanie twarzy z bazy danych oraz wyświetlanie informacji o rozpoznanej twarzy.

Projekt ten może być wykorzystywany w różnych scenariuszach, takich jak:
* Dostęp do budynku lub pomieszczeń zabezpieczonych - system może być używany do kontrolowania dostępu do pomieszczeń zabezpieczonych, wymagając od osób przechodzących przez drzwi, aby pokazały swoją twarz przed kamerą.
* Identyfikacja osób na lotniskach - system może być używany do identyfikacji osób na lotniskach, aby zwiększyć bezpieczeństwo i zapobiegać podróżom fałszywymi tożsamościami.
* Monitoring ruchu na ulicach - system może być używany do monitorowania ruchu na ulicach, aby pomóc w identyfikacji osób, które poruszają się po mieście.
* Systemy bezpieczeństwa - system może być wykorzystywany w systemach bezpieczeństwa, takich jak monitoring sklepów czy budynków publicznych, aby pomóc w identyfikacji osób, które mogą stanowić zagrożenie.
* Aplikacje mobilne - system może być również wykorzystywany w aplikacjach mobilnych, takich jak aplikacje do rozpoznawania twarzy, które pozwalają użytkownikom na rozpoznawanie twarzy znajomych za pomocą ich smartfonów.

# Znane koncepcje rozwiązujące ten problem:
* Rozpoznawanie twarzy oparte na algorytmie klasyfikacji - jest to jedna z najprostszych metod rozpoznawania twarzy, polegająca na przypisaniu każdej twarzy do jednej z zaprogramowanych klas. Mocną stroną tego rozwiązania jest prostota i szybkość działania, jednak jego słabą stroną jest niska skuteczność w przypadku twarzy nietypowych lub nieznanych.

* Rozpoznawanie twarzy oparte na sieciach neuronowych - jest to bardziej zaawansowana metoda rozpoznawania twarzy, polegająca na wykorzystaniu sieci neuronowej do uczenia się reprezentacji twarzy. Mocną stroną tego rozwiązania jest wysoka skuteczność i zdolność do rozpoznawania twarzy nietypowych, jednak jego słabą stroną jest złożoność i zapotrzebowanie na dużo danych treningowych.

* Rozpoznawanie twarzy oparte na reprezentacji generatywnej - jest to najnowocześniejsza metoda rozpoznawania twarzy, polegająca na generowaniu reprezentacji twarzy za pomocą generatywnych sieci neuronowych. Mocną stroną tego rozwiązania jest zdolność do rozpoznawania twarzy nietypowych i braku potrzeby dużej ilości danych treningowych, jednak jego słabą stroną jest złożoność i potrzeba dużej mocy obliczeniowej.

# Opis wybranej koncepcji </br>
Naszymi podstawowymi rzeczami do opracowania bylo: 

* wytrenowanie modelu do klasyfikacji twarzy
* przystosowanie modelu do pracy z kamera </br>

Jedną z popularnych koncepcji rozwiązania problemu rozpoznawania twarzy z zastosowaniem konwolucyjnych sieci neuronowych jest użycie tzw. sieci konwolucyjnej (CNN). 
W CNN naurony sa ułożone w postaci macierzy(nazywanej tensorem) oraz są połączone z niewielką liczbą neuronów warstwy poprzedniej, a więc warstwy nie są w pełni połączone. Najważniejszym pojęciem w tym zadaniu jest Transfer Learning. Jest to proces wykorzystania wstepnie wytrenowanego modelu do wlasnego zadania, poniewaz zbudowanie klasyfikatora twarzy od podstaw jest bardzo trudne (my w tym celu wykorzystalismy TensorFlow). Nasz model sklada się z kilku warst:
* Warstw konwolucyjnych, które służą do wykrywania cech twarzy na obrazie.</br>
* Warstwy max pooling, która służy do zmniejszenia rozmiaru obrazu.
* Warstw fully connected, które służą do klasyfikacji obrazu jako twarz lub brak twarzy.</br>

W celu przetwarzania obrazów skorzystaliśmy z biblioteki OpenCV. Dane potrzebne do tego rozwiązania to zbiór zdjęć twarzy osoby, którą chcemy rozpoznać oraz zbiór zdjęć twarzy osób, które będą stanowić "przeciwników" dla modelu. W naszym przypadku są to samodzielnie robione zdjęcia 5 osób. Aby zwiekszyc skutecznosc modelu skorzystalismy z argumentacji danych, czyli procesu sztucznego zwiększania rozmiaru zbioru danych poprzez zastosowanie pewnych przekształceń do oryginalnych obrazów. Na przykład, możemy użyć poziomego i pionowego odbicia, niewielkiego obrotu lub powiększenia, inwersji kolorów itp.
Dzięki tym wszystkim zabiegom możemy wykrywać i analizować cechy twarzy, a następnie porównywać ich z cechami twarzy znajdującymi się w bazie danych.
