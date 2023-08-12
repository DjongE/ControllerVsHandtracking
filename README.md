# Controller Vs. Handtracking

## Welche Unity Version wird benötigt?
Es wird die Unity Version 2022.2.1f1 benötigt.
Zudem muss bei der Installation folgende Platforms ausgewählt werden:
- Android Build Support
- OpenJDK
- Android SDK & NDK Tools
- Linux Build Support (IL2CPP)
- Windows Build Support (IL2CPP)

## How to install?
1. Das Projekt klonen oder von dem USB-Stick auf den eigenen Computer kopieren
2. In Unity Hub das Projek über den Button "Open" auswählen (Das Projekt sollte nun in der Liste Projects sichtbar sein)
3. In Unity Hub das Projekt "ControllerVsHandtracking" starten

## Auswahl zwischen Controller und Handtracking und die Interaktions Reihenfolge
Auf dem Bild [![name]([link to image on GH](https://github.com/DjongE/ControllerVsHandtracking/blob/3cc77bc7520320515b48483e6dacedb07a2d2b3d/InteractionHandler.PNG))] sind die Einstellungungen zu sehen, mit welchen die Reihenfolge der Interaktionen ausgewählt und zwischen Controller und Handtracking gewechselt werden kann.

Auswahl der Interaktionsform:
- Wenn der Haken bei der Option "Controller" gesetzt wird, ist der Controller-Modi aktiv.</br>
- Wenn der Haken bei der Option "Controller" nicht gesetzt wird, ist der Handtracking-Modi aktiv.</br>

Auswahl der Interaktionsreihenfolge:
- Wenn der Haken bei der Option "Reverse" gesetzt wird, ist die Reihenfolge wie folgt:

| Index | Interaktion            |
|-------|------------------------|
| 1     | Objekte werfen         |
| 2     | Hanoi (Turm stapeln)   |
| 2     | Lichtschalter          |
| 3     | Greifen und Platzieren |
| 4     | Würfeln                |
| 5     | Virtual Buttons        |

- Wenn der Haken bei der Option "Reverse" nicht gesetzt wird, ist die Reihenfolge wie folgt:

| Index | Interaktion            |
|-------|------------------------|
| 1     | Virtual Buttons        |
| 2     | Würfeln                |
| 2     | Greifen und Platzieren |
| 3     | Lichtschalter          |
| 4     | Hanoi (Turm stapeln)   |
| 5     | Objekte werfen         |

![InteractionHandler\label{InteractionHandler}](https://github.com/DjongE/ControllerVsHandtracking/blob/3cc77bc7520320515b48483e6dacedb07a2d2b3d/InteractionHandler.PNG "InteractionHandler")
