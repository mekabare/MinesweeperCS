using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Minesweeper
{
    /// <summary>
    /// Bestenliste enthält Spieler als Member-Objekte, um daraus eine Bestenliste zu Formen
    /// Die 10 besten Spieler für die jeweiligen Schwierigkeitsgrade
    /// </summary>
    public class Bestenliste
    {
        #region Felder
        private const string path = @"Bestenlisten.txt";

        private Spieler[] easyList = null;
        private Spieler[] mediumList = null;
        private Spieler[] hardList = null;

        private Spieler[] platzhalterList = null;
        #endregion Felder



        #region Konstruktoren

        /// <summary>
        /// Default-Konstruktor
        /// </summary>
        public Bestenliste()   //Hier macht nur ein Default-Konstruktor sinn, weil die Klasse effektiv nur mit Dateien arbeitet
        {
            ReadLists();        //Bestückt die Listen.
        }//default

        #endregion Konstruktoren



        #region Methoden

        /*
         * Info zu Datei-oop.:
         * - Es gibt eine Datei mit allen Datensätzen!
         * - Datensätze werden mit '#' auseinandergehalten.
         * - Da jeder Datensatz aus einem Spieler-Objekt besteht, sind die einzelnen Datensätze in sich
         *   mit ';' getrennt.
         * - Die Reihenfolge der Datenelemente eines Datensatzes ist:
         *   items[0] = Name
         *   items[1] = Time
         *   items[2] = Difficulty.ToString()
         * - Insgesamt sollte es 30 Datensätze geben (3 Bestenliste á 10 Spielern)
         */

        /// <summary>
        /// Liest die Datei "Bestenlisten.txt" auf Spieler Datensätze ein,
        /// die dann in eines der entsprechenden Bestenlistenarrays gespeichert werden.
        /// </summary>
        /// <returns>boolsches True, wenn das einlesen KOMPLETT geklappt hat.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        public bool ReadLists()
        {
            bool ok = false;
            string line = "";
            int number;             //Hilfsvariable für numerische Konvertierungen mit TryParse()
            Spieler spieler = null;
            int easyCount = 0;
            int mediumCount = 0;
            int hardCount = 0;

            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    if (fs.CanRead)
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            line = sr.ReadLine(); //Einlesen aller Datensätze als ein String

                            //Trennen der Datensätze
                            if (line != null && line.Length > 0) //Exitieren Daten?
                            {
                                string[] list = line.Split('#'); //Es sollten 30 Datensätze dabei entstehen.

                                foreach (string player in list)
                                {

                                    //Trennen der Datensatz-Items
                                    string[] items = player.Split(';');
                                    if (!string.IsNullOrEmpty(items[0])) //Ist der DS gültig?
                                    {
                                        spieler = new Spieler();

                                        spieler.Name = items[0];                                //Name

                                        ok = int.TryParse(items[1], out number);
                                        if (ok)
                                        {
                                            spieler.Time = number;                              //Time

                                            switch (items[2])
                                            {
                                                case "Easy":
                                                    {
                                                        spieler.Difficulty = new Easy();        //selectedDifficulty

                                                        //in die Liste
                                                        Array.Resize<Spieler>(ref easyList, easyCount + 1);
                                                        easyList[easyCount] = new Spieler(spieler);
                                                        easyCount++;
                                                        break;
                                                    }

                                                case "Medium":
                                                    {
                                                        spieler.Difficulty = new Medium();      //selectedDifficulty

                                                        //in die Liste
                                                        Array.Resize<Spieler>(ref mediumList, mediumCount + 1);
                                                        mediumList[mediumCount] = new Spieler(spieler);
                                                        mediumCount++;
                                                        break;
                                                    }

                                                case "Hard":
                                                    {
                                                        spieler.Difficulty = new Hard();        //selectedDifficulty

                                                        //in die Liste
                                                        Array.Resize<Spieler>(ref hardList, hardCount + 1);
                                                        hardList[hardCount] = new Spieler(spieler);
                                                        hardCount++;
                                                        break;
                                                    }

                                                //Custom werden nicht gespeichert, und deswegen nicht eingelesen

                                                default:
                                                    {
                                                        ok = false;
                                                        break;
                                                    }
                                            }//switch
                                        }//if(ok)

                                        else
                                            break;  //Wenn die Konvertierung eines Objekts scheitert, bricht die Methode ab
                                    }//if(string != null)
                                }//foreach
                            }//if
                        }//using
                    }//if
                }//using
            }//if
            return ok;
        }//ReadLists

        /// <summary>
        /// Speichert die Bestenliste in die Bestenliste.txt (überschreibt alte)
        /// </summary>
        /// <returns>boolsches True, wenn alles funktioniert hat</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        public bool SaveBest()
        {
            bool ok = false;

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)) {
                if (fs.CanWrite)    //Kann die Datei zum Schreiben geöffnet werden? 
                {
                    foreach (Spieler player in easyList) {  //easyList speichern
                        using (StreamWriter sw = new StreamWriter(fs)) {
                            sw.Write("{0};{1};{2}#",
                                    player.Name,
                                    player.Time.ToString(),
                                    player.Difficulty.ToString());
                            sw.Flush();
                        }//using
                    }//froeach easyList
                    foreach (Spieler player in mediumList) {    //mediumList speichern
                        using (StreamWriter sw = new StreamWriter(fs)) {
                            sw.Write("{0};{1};{2}#",
                                    player.Name,
                                    player.Time.ToString(),
                                    player.Difficulty.ToString());
                            sw.Flush();
                        }//using
                    }//froeach mediumList
                    foreach (Spieler player in hardList) {  //hardList speichern
                        using (StreamWriter sw = new StreamWriter(fs)) {
                            sw.Write("{0};{1};{2}#",
                                    player.Name,
                                    player.Time.ToString(),
                                    player.Difficulty.ToString());
                            sw.Flush();
                        }//using
                    }//froeach hardList
                    ok = true;  //alles hat geklappt
                }//if
            }//using
            return ok;
        }//SaveBest


        /// <summary>
        /// Checkt, ob der überreichte player es in die Bestenliste geschafft hat.
        /// </summary>
        /// <param name="player">Spieler, der die Liste contestet</param>
        /// <param name="platz">Platzierung des Spielers, wenn return == true.
        /// In Array-Format! Heißt:
        /// 1.Platz -> Platzierung = 0
        /// 2.Platz -> Platzierung = 1 usw.</param>
        /// <returns>boolsches True, wenn der player in seiner Liste einen PLatz kriegt</returns>
        public bool CheckBest(Spieler player, out int platz)
        {
            bool ok = false;
            platz = 0;
            int arrLength;

            switch (player.Difficulty.ToString())
            {
                /*
                 * Ablauf eines case:
                 * - bestimmt die groesse des Arrays.
                 * - for durchläuft das Array von vorne nach hinten:
                 *   - Wenn eine Stelle die gleiche, oder eine schlechtere Zeit hat, wie der Contester,
                 *     wird der Platz der Stelle in int platz eingetragen und ok = true gesetzt.
                 * - Wenn keine Stelle past, bleibt ok = false.
                 */
                case "Easy":
                    {
                        arrLength = easyList.Length;
                        for (int i = 0; i < arrLength; i++) {
                            if (easyList[i].Time <= player.Time) {
                                platz = i;
                                ok = true;
                                break;
                            }
                        }
                        break;
                    }//GameInstance
                case "Medium": {
                        arrLength = mediumList.Length;
                        for (int i = 0; i < arrLength; i++) {
                            if (mediumList[i].Time <= player.Time) {
                                platz = i;
                                ok = true;
                                break;
                            }
                        }
                        break;
                    }//Medium
                case "Hard": {
                        arrLength = hardList.Length;
                        for (int i = 0; i < arrLength; i++) {
                            if (hardList[i].Time <= player.Time) {
                                platz = i;
                                ok = true;
                                break;
                            }
                        }
                        break;
                    }//Hard
                default: { ok = false; break; } //Wenn weder GameInstance, Medium oder Hard.
            }//switch

            return ok;
        }//CeckBest


        /// <summary>
        /// Fügt ein Spieler-Objekt in eine Bestenliste ein, sofern es nach der Funktion CheckBest(...) in die
        /// entsprechende Bestenliste gehört.
        /// </summary>
        /// <param name="player">Spieler-Objekt, der in die Bestenliste eingefügt werden soll.</param>
        /// <returns>boolsches True, Wenn das einfügen Funktioniert hat.</returns>
        public bool InsertSorted(Spieler player)
        {
            bool ok = true;
            int platzierung;
            int arrLength;

            //ruft selber CheckBest auf und setzt dann an der Stelle Platz ein.
            if (CheckBest(player, out platzierung))
            {
                switch (player.Difficulty.ToString())
                {
                    /*
                     * Ablauf der Cases:
                     * - Wenn das Array == null ist, wird nichts gemacht.
                     * - Groesse des Arrays wird bestimmt
                     * - Wenn das Array nur einen Eintrag hat, wird dieser überschrieben und die
                     *   for-Schleife übersprungen.
                     * - for-Schleife
                     *   - int i wird hinten angesetzt.
                     *   - durchläuft das Array bis einschließlich int platzierung
                     *   - verlegt dabei alle Elemente, einschließlich Array[platzierung] um eins
                     *     nach hinten (8->9, 7->8, usw.). Dabei geht das letzte Element der Liste
                     *     geziehlt verloren.
                     * - Am Ende wird das neue Element an der Stelle Array[platzierung] eingesetzt
                     */
                    case "Easy": {
                            if(easyList == null) { ok = false; break; }
                            arrLength = easyList.Length;
                            if (arrLength == 1) { easyList[platzierung] = new Spieler(player); break; }
                            for (int i = arrLength - 2; i >= platzierung; i--) {
                                easyList[i] = easyList[i + 1];
                            }
                            easyList[platzierung] = new Spieler(player);
                            break;
                        }//GameInstance
                    case "Medium": {
                            if (mediumList == null) { ok = false; break; }
                            arrLength = mediumList.Length;
                            if (arrLength == 1) { mediumList[platzierung] = new Spieler(player); break; }
                            for (int i = arrLength - 2; i >= platzierung; i--) {
                                mediumList[i] = mediumList[i + 1];
                            }
                            mediumList[platzierung] = new Spieler(player);
                            break;
                        }//Medium
                    case "Hard": {
                            if (hardList == null) { ok = false; break; }
                            arrLength = hardList.Length;
                            if (arrLength == 1) { hardList[platzierung] = new Spieler(player); break; }
                            for (int i = arrLength - 2; i >= platzierung; i--) {
                                hardList[i] = hardList[i + 1];
                            }
                            hardList[platzierung] = new Spieler(player);
                            break;
                        }//Hard
                    default: { ok = false; break; } //Wenn das Spieler-Objekt weder GameInstance, Medium oder Hard ist.
                }//switch(selectedDifficulty)
            }//if(CheckBest)
            else { ok = false; }    //Wenn CheckBest sagt, Nein.
            return ok;
        }//InsertSorted

        #endregion Methoden



        #region Getter und Setter

        public Spieler[] EasyList
        {
            get => easyList;
            set { easyList = value; }
        }

        public Spieler[] MediumList
        {
            get => mediumList;
            set { mediumList = value; }
        }

        public Spieler[] HardList
        {
            get => hardList;
            set { hardList = value; }
        }

        public Spieler[] PlatzhalterList
        {
            get => platzhalterList;
            set { platzhalterList = value; }
        }

        #endregion Getter und Setter

    }//Klasse
}//Namespace
