using System;
using MeteoProject;
using System.Collections.Generic;
namespace MeteoProject
{
    public static class Search
    {
        public static List<string> Suggestions;
        public static List<Prevision> Previsions;
        public static string CityName;
        public static string Latitude;
        public static string Longitude;
        public static string Celsius;
        public static string Fahrenheit;
        public static string Time_Description;
        public static string Humidity;
        public static string MeteoPicture;

       
    }

    public class Search2
    {

        public string Celsius;
        public string Fahrenheit;

        public string Time_Description;
        public string Humidity;
        public string MeteoPicture;
    }

    public class Prevision {
        public Search2 Search = new Search2();
        public string Date;
        public string Hour;



    }

    public static class Parametres {
        public static List<string> Languages;
        public static string Langue;
        public static string DefaultCity;
        public static string TempUnit;
        public static string APIKey; 
    }

     public  class Parameters {
        public  string Langue;
        public  string DefaultCity;
        public  string TempUnit;
        public string APIKey; 

    }

   

}


// -------Search-----

// CityName
// Latitude
// Longitude
// Celsius
// Time_Description
// Humidity
// MeteoPicture


    // Prevision s'affiche seulement une fois la recherche faite
    // [5 elements Prevision]

// -------Prevision-----

// Search
// Date
// Hour


//  ------Parametres-----

// Langue
// Default


// => creation d'un fichier options.json en ligne de code
// => gestion des erreurs internet & recherche inconnue


