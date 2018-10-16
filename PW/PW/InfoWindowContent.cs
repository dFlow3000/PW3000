using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Preiswattera_3000
{
    class InfoWindowContent
    {
        public struct Header
        {
            public string HeaderContent;
        }

        public struct Main
        {
            public const string Para1_Header = "Aktuelle Informationen";
            public const string Para1_Content1 = "Aktueller Durchgang: " +
                "Zeigt die Nummer des aktuell gespielten Durchgangs.";
            public const string Para1_Content2 = "Abschgeschlossene Spiele: " +
                "Zeigt wie viele Spiele des aktuellen Durchgangs bereits erfasst wurden.";
            public const string Para1_Content3 = "Oberer Fortschrittsbalken:" +
                "Zeigt Fortschritt des aktuell gespielten Durchgangs.";
            public const string Para1_Content4 = "Unterer Fortschrittsbalken:" +
                "Zeigt Fortschritt des gesamten Turniers.";
            public const string Para2_Header = "Allgemeine Informationen";
            public const string Para2_Content1 = "Anzahl Durchgänge:" +
                "Zeigt die für das Turnier festgelegte Anzahl an Durchgängen.";
            public const string Para2_Content2 = "Anzahl Spiele pro Durchgang:" +
                "Zeigt die für das Turnier festgelegte Anzahl an Spielen pro Durchgang.";
            public const string Para2_Content3 = "Anzahl Teams:" +
                "Zeigt die aktuelle Anzahl angemeldeter Teams.";
            public const string Para2_Content4 = "Anzahl Tische:" +
                "Zeigt die benötigte Anzahl Tische um mit aktueller Anzahl angemeldeter Teams Tunier umzusetzen.";
            public const string Para3_Header = "Turnier beenden";
            public const string Para3_Content = "Beendet das Turnier und speichert aktuellen Turnierstand in einem Ordner entsprechend des Turniernamens";
        }
    }
}
