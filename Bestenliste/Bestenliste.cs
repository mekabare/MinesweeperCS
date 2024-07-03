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

        private Spieler[] playerList;

        private Spieler[] platzhalterList = null;
        #endregion Felder



        #region Konstruktoren

        /// <summary>
        /// Default-Konstruktor
        /// </summary>
        public Bestenliste()   //Hier macht nur ein Default-Konstruktor sinn, weil die Klasse effektiv nur mit Dateien arbeitet
        {
            ReadList();      //Bestückt die Listen.
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
         *   param[0] = Name
         *   param[1] = Time
         *   param[2] = Difficulty.ToString()
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
        public bool ReadList()
        {
            bool ok = false;
            string line = "";
            int number;             //Hilfsvariable für numerische Konvertierungen mit TryParse()
            Spieler spieler = null;
            int entryCount = 0;     //Zählt die Anzahl der eingelesenen Datensätze, erlaubt dynamische Generierung des UIs


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
                                    string[] param = player.Split(';');
                                    if (!string.IsNullOrEmpty(param[0])) //Ist der DS gültig?
                                    {
                                        spieler = new Spieler();

                                        spieler.Name = param[0];                                //Name

                                        ok = int.TryParse(param[1], out number);
                                        if (ok)
                                        {
                                            spieler.Time = number;                              //Time

                                            switch (param[2])
                                            {
                                                case "Easy":
                                                    {
                                                        spieler.Difficulty = new Easy();
                                                        break; //selectedDifficulty
                                                    }

                                                case "Medium":
                                                    {
                                                        spieler.Difficulty = new Medium();      //selectedDifficulty
                                                        break;

                                                    }

                                                case "Hard":
                                                    {
                                                        spieler.Difficulty = new Hard();        //selectedDifficulty
                                                        break;

                                                    }

                                                default:
                                                    {
                                                        ok = false;
                                                        break;
                                                    }
                                            }//switch
                                            Array.Resize<Spieler>(ref playerList, entryCount + 1);
                                            playerList[entryCount] = new Spieler(spieler);
                                            entryCount++;
                                            break;
                                            //Custom werden nicht gespeichert, und deswegen nicht eingelesen


                                        }//if(ok)

                                        //Wenn die Konvertierung eines Objekts scheitert, bricht die Methode ab
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
                    foreach (Spieler player in playerList) {  //easyList speichern
                        using (StreamWriter sw = new StreamWriter(fs)) {
                            sw.Write("{0};{1};{2};{3}#",

                            player.Name,
                            player.Time.ToString(),
                            player.Difficulty.ToString());
                            player.Score.ToString();

                            sw.Flush();
                        }//using
                    }//froeach
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

        //SortList
        public void SortListAscending()
        {
            if (ReadList())

                foreach (Spieler player in playerList)
                {
                    // sort by score

                    Array.Sort(playerList, (x, y) => x.Score.CompareTo(y.Score));

                    // sortingdirection is ascending

                }
        }

        public void SortListDescending
            ()
        {
            if (ReadList())

                foreach (Spieler player in playerList)
                {
                    // sort by score

                    Array.Sort(playerList, (x, y) => y.Score.CompareTo(x.Score));

                    // sortingdirection is descending

                }
        }

        /// <summary>
        /// Vergleicht die Score des Users und ermittelt den Rang in der Bestenliste, erweitert die Liste am Insertion Index
        /// Wird genutzt, um bei Win die richtige Grid.Row zu fuellen, hoechste score ist oben, liste ist descending
        /// </summary>
        /// <param name="player"></param>
        public int GetRankInList(Spieler player)
        {
            SortListDescending();
            int index = 0;
            if (playerList.Length == 0) // Falls die Liste leer ist
            {
                Array.Resize(ref playerList, 1);
                playerList[0] = player;
                return index;
            }
            else // Falls die Liste nicht leer ist
            {
                for (int i = 0; i < playerList.Length; i++)
                {
                    if (playerList[i].Score < player.Score)
                    {
                        index = i;
                        break;
                    }
                    else
                    {
                        index = playerList.Length;
                    }
                }
                Array.Resize(ref playerList, playerList.Length + 1);

                for (int i = playerList.Length - 1; i > index; i--) // Verschiebt die Liste um 1 nach unten, von hinten nach vorne
                {
                    playerList[i] = playerList[i - 1];
                }
              return index+1; // plus 1 weil Grid.Row=0 die listenheader sind
            }

        }//GetRankInList

        #endregion Methoden



        #region Getter und Setter

        public Spieler[] PlayerList
        {
            get => playerList;
            set { playerList = value; }
        }

        public Spieler[] PlatzhalterList
        {
            get => platzhalterList;
            set { platzhalterList = value; }
        }

        #endregion Getter und Setter

    }//Klasse
}//Namespace
