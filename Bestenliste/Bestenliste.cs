using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Bestenliste
{
    /// <summary>
    /// Bestenliste enthält Spieler als Member-Objekte, um daraus eine Bestenliste zu Formen
    /// Die 10 besten Spieler für die jeweiligen Schwierigkeitsgrade
    /// </summary>
    public class Bestenliste
    {
        #region Felder
        private const string path = @"Bestenlisten.txt";

        private Spieler[] easyList = new Spieler[10];
        private Spieler[] mediumList = new Spieler[10];
        private Spieler[] hardList = new Spieler[10];

        #endregion Felder



        #region Konstruktoren

        #endregion Konstruktoren



        #region Methoden
        /*
         * public static bool SaveBest() //Speichert die Bestenlisten in die Bestenlisten.txt
         * 
         * public static bool CheckBest(Spieler player, out int platz) //Checkt, ob der überreichte player es in die Bestenliste geschafft hat.
         * 
         * public static bool InsertSorted(Spieler player) //fügt ein Spieler-Objekt in die entsprechende Liste ein.
         */
        #endregion Methoden



        #region Getter und Setter

        #endregion Getter und Setter
    }//Klasse
}//Namespace
