using System;
using Gtk;
using MeteoProject;
using UI = Gtk.Builder.ObjectAttribute;
using xml = System.Xml;
using xml2 = System.Xml.Serialization;
using System.IO;
using MeteoProject;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeteoProject
{

    class Glade_meteo : Window
    {
        
        // Onglet Weather
        [UI] private Label latitude_longitude; 
        [UI] private Button temp;
        [UI] private Label description;
        [UI] private Label humidity;
        [UI] private Image img;
        [UI] private Label city;
        [UI] private Label city1;
        [UI] private Button button;
        [UI] private SearchEntry searchBar_weather;
        [UI] private Label error; 


        // Onglet Previsions
        [UI] private Label temp1;
        [UI] private Label humidity1;
        [UI] private Label desc1;
        [UI] private Label date1;
        [UI] private Image img1;
        [UI] private Label temp2;
        [UI] private Label humidity2;
        [UI] private Label desc2;
        [UI] private Label date2;
        [UI] private Image img2;
        [UI] private Label temp3;
        [UI] private Label humidity3;
        [UI] private Label desc3;
        [UI] private Label date3;
        [UI] private Image img3;
        [UI] private Label temp4;
        [UI] private Label humidity4;
        [UI] private Label desc4;
        [UI] private Label date4;
        [UI] private Image img4;
        [UI] private Label temp5;
        [UI] private Label humidity5;   
        [UI] private Label desc5;
        [UI] private Label date5;
        [UI] private Image img5;


        // Onglet Settings
        [UI] private SearchEntry searchBar_settings;
        [UI] private Label default_city; 
        [UI] private Label error_settings; 
        [UI] private Button button_settings; 
        [UI] private Button button_reset_default_city; 


        public Glade_meteo() : this(new Builder("glade_Meteo.glade")) { }

        private Glade_meteo(Builder builder) : base(builder.GetRawOwnedObject("window"))
        {
            builder.Autoconnect(this);
            temp.Clicked += CelciusToFahrenheit;
            button.Clicked += Button_Clicked;
            DeleteEvent += Window_DeleteEvent;
            button_settings.Clicked += Button_settings_Clicked;
            button_reset_default_city.Clicked += Button_reset_default_city_Clicked;

            if (!File.Exists("options.json")) {
                Console.WriteLine("Bienvenue sur MeteoApp!!! :)");
                Console.WriteLine("Si vous n'avez pas de clé API, rendez vous sur ce site https://openweathermap.org/");
                Console.WriteLine("Votre clé API : ");

                string apiKey = Console.ReadLine().Split(" ")[0];
                Parameters param = new Parameters();
                param.APIKey = apiKey;
                param.DefaultCity = "Bordeaux";
                param.Langue = "fr";
                param.TempUnit = "C";
                string json = JsonConvert.SerializeObject(param);
                File.WriteAllText("options.json", json);
            }
            string json2 = File.ReadAllText("options.json");
            Parameters param2 = JsonConvert.DeserializeObject<Parameters>(json2);
            Parametres.APIKey = param2.APIKey;
            Parametres.DefaultCity = param2.DefaultCity;
            Parametres.Langue = param2.Langue;
            Parametres.TempUnit = param2.TempUnit;
            


            if (Parametres.DefaultCity != null){
                bool goodKey = false;
                while (goodKey == false){
                    try {
                        API APIRequest = new API();
                        APIRequest.RecupAPI(Parametres.DefaultCity);
                        goodKey = true;
                    } catch {
                        Console.WriteLine("Erreur Clé API!!");
                        Console.WriteLine("Si vous n'avez pas de clé API, rendez vous sur ce site https://openweathermap.org/");
                        Console.WriteLine("Votre clé API : ");
                        string apiKey = Console.ReadLine().Split(" ")[0];
                        Parameters param = new Parameters();
                        param.APIKey = apiKey;
                        param.DefaultCity = "Bordeaux";
                        param.Langue = "fr";
                        param.TempUnit = "C";
                        string json = JsonConvert.SerializeObject(param);
                        File.WriteAllText("options.json", json);
                        Parametres.APIKey = apiKey;
                    }
                }
                
                Update();

            }
        }


        private void CelciusToFahrenheit(object sender, EventArgs e)
        {

            if (Parametres.TempUnit == "F") {
                temp.Label = Search.Celsius;
                Parametres.TempUnit = "C";

            } else {
                temp.Label = Search.Fahrenheit;
                Parametres.TempUnit = "F";
            }
            Updatetemp();

        }

        private void Button_reset_default_city_Clicked(object sender, EventArgs e)
        {
            Parametres.DefaultCity = null;
            default_city.Text = "Default City : None";
            SaveJsonParameters();
        }

        private void Button_settings_Clicked(object sender, EventArgs e)
        {
            try {
                API APIRequest = new API();
                APIRequest.RecupAPI(searchBar_settings.Text);
                error_settings.Text = "";
                Parametres.DefaultCity = Search.CityName;
            } catch {
                Console.WriteLine("Error : The search " + searchBar_settings.Text + " could not be completed.");
                error_settings.Text = "Error : The search " + searchBar_settings.Text + " could not be completed.";
            }
            searchBar_settings.Text = "";

            default_city.Text = Parametres.DefaultCity;
            
            string json2 = File.ReadAllText("options.json");
            Parameters param2 = JsonConvert.DeserializeObject<Parameters>(json2);
            param2.DefaultCity = Parametres.DefaultCity;
            string json = JsonConvert.SerializeObject(param2);
            File.WriteAllText("options.json", json);
            SaveJsonParameters();

        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit(); 
        }

        private void Button_Clicked(object sender, EventArgs a)
        {
            try {
                API APIRequest = new API();
                APIRequest.RecupAPI(searchBar_weather.Text);
                error.Text = "";
                Update();
            } catch {
                Console.WriteLine("Error : The search " + searchBar_weather.Text + " could not be completed.");
                error.Text = "Error : The search " + searchBar_weather.Text + " could not be completed.";
            }
            searchBar_weather.Text = "";

            

        }

        public void Updatetemp(){
            if (Parametres.TempUnit == "C"){
                temp.Label = Search.Celsius;
                temp1.Text = Search.Previsions[0].Search.Celsius;
                temp2.Text = Search.Previsions[1].Search.Celsius;
                temp3.Text = Search.Previsions[2].Search.Celsius;
                temp4.Text = Search.Previsions[3].Search.Celsius;
                temp5.Text = Search.Previsions[4].Search.Celsius;
            } else{
                temp.Label = Search.Fahrenheit;
                temp1.Text = Search.Previsions[0].Search.Fahrenheit;
                temp2.Text = Search.Previsions[1].Search.Fahrenheit;
                temp3.Text = Search.Previsions[2].Search.Fahrenheit;
                temp4.Text = Search.Previsions[3].Search.Fahrenheit;
                temp5.Text = Search.Previsions[4].Search.Fahrenheit;
            }
        }

        public void Update(){

            // Onglet Weather
            Updatetemp();
            latitude_longitude.Text = "Latitude : " + Search.Latitude + "    Longitude : " + Search.Longitude;
            city.Text = Search.CityName;
            temp.Label = Search.Celsius;
            description.Text = Search.Time_Description;
            humidity.Text = Search.Humidity + "%"; 
            img.Pixbuf = new Gdk.Pixbuf("images/image.png");
            city1.Text = Search.CityName;


            // Onglet Prevision

            for (int i= 0; i<5; i++){
                API.ImageDecompression(Search.Previsions[i].Search.MeteoPicture, @"images/image" + (i+1).ToString() + ".png");
            }
            humidity1.Text = Search.Previsions[0].Search.Humidity + "%";
            desc1.Text = Search.Previsions[0].Search.Time_Description;
            date1.Text = Search.Previsions[0].Date + "\n" + Search.Previsions[0].Hour;
            img1.Pixbuf = new Gdk.Pixbuf("images/image1.png");
        

            humidity2.Text = Search.Previsions[1].Search.Humidity + "%";
            desc2.Text = Search.Previsions[1].Search.Time_Description;
            date2.Text = Search.Previsions[1].Date + "\n" + Search.Previsions[1].Hour;
            img2.Pixbuf = new Gdk.Pixbuf("images/image2.png");

            humidity3.Text = Search.Previsions[2].Search.Humidity + "%";
            desc3.Text = Search.Previsions[2].Search.Time_Description;
            date3.Text = Search.Previsions[2].Date + "\n" + Search.Previsions[2].Hour;
            img3.Pixbuf = new Gdk.Pixbuf("images/image3.png");

            humidity4.Text = Search.Previsions[3].Search.Humidity + "%";
            desc4.Text = Search.Previsions[3].Search.Time_Description;
            date4.Text = Search.Previsions[3].Date + "\n" + Search.Previsions[3].Hour;
            img4.Pixbuf = new Gdk.Pixbuf("images/image4.png");

            humidity5.Text = Search.Previsions[4].Search.Humidity + "%";
            desc5.Text = Search.Previsions[4].Search.Time_Description;
            date5.Text = Search.Previsions[4].Date + "\n" + Search.Previsions[4].Hour;
            img5.Pixbuf = new Gdk.Pixbuf("images/image5.png");


            // Onglet Settings

            default_city.Text = Parametres.DefaultCity;

        }
        
        public void SaveJsonParameters()
        {
            string json2 = File.ReadAllText("options.json");
            Parameters param2 = JsonConvert.DeserializeObject<Parameters>(json2);
            param2.DefaultCity = Parametres.DefaultCity;
            param2.Langue = Parametres.Langue;
            string json = JsonConvert.SerializeObject(param2);
            File.WriteAllText("options.json", json);
        }
    }
}

// API KEY : f8087ef34dd769eaa9a4004d1665d226