using System;
using Gtk;
using Newtonsoft.Json.Linq; 
using Newtonsoft.Json; 
using System.Net.Http; 
using System.Threading.Tasks; 
using System.Linq; 
using System.Collections.Generic;
using System.Xml;
using MeteoProject;
using System.Net;

namespace MeteoProject{

    public class API {

        public const string geoCoding = "http://api.openweathermap.org/geo/1.0/direct?q={0},&limit={1}&appid={2}";
        public const string weather_url = "https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}";
        public const string previsions_url = "http://api.openweathermap.org/data/2.5/forecast?id={0}&appid={1}";
        public const string icons_url = "http://openweathermap.org/img/wn/{0}@2x.png";
        private static HttpClient client; 



        //  GEOCODING       http://api.openweathermap.org/geo/1.0/direct?q=aa,&limit=1&appid=f8087ef34dd769eaa9a4004d1665d226
        //  WEATHER         http://api.openweathermap.org/data/2.5/weather?lat=10.99&lon=44.34&appid=f8087ef34dd769eaa9a4004d1665d226
        //  PREVISIONS      http://api.openweathermap.org/data/2.5/forecast?id=524901&appid=f8087ef34dd769eaa9a4004d1665d226
        //  ICONS           https://openweathermap.org/weather-conditions
        //  ICON            http://openweathermap.org/img/wn/03d@2x.png      
    


    


        public static Task<string> GetGlobalDataAsync(string url)
        {
            try{
                HttpClient client = new HttpClient();
                var response = client.GetAsync(url).Result;
                return response.Content.ReadAsStringAsync();
            }catch{
                return null;
            }
        }



        public T Deserialise<T>(T type, string url){
            if (typeof(T) == typeof(RootP)){
                string geoJson = GetGlobalDataAsync(url).GetAwaiter().GetResult(); 
                var geoData = JObject.Parse(geoJson).ToObject<T>();
                return geoData;
            } else {
                string geoJson = GetGlobalDataAsync(url).GetAwaiter().GetResult(); 
                var geoData = JsonConvert.DeserializeObject<T>(geoJson);
                return geoData;
            }
        }




        public void RecupAPI(string city)
        {

            client = new HttpClient(); 
            
            // Coordinate with the city
            string geoURL = string.Format(geoCoding, city, "5", Parametres.APIKey);
            var geoData = Deserialise<List<RootG>>(new List<RootG>(), geoURL);

            Search.Suggestions = geoData.ConvertAll(obj => obj.name);
            Search.Latitude = geoData[0].lat.ToString();
            Search.Longitude = geoData[0].lon.ToString();
            Search.CityName = GGTrad(geoData);


            // Meteo with Coordinate
            string meteoURL = string.Format(weather_url, Search.Latitude, Search.Longitude, Parametres.APIKey);
            var meteoData = Deserialise<RootM>(new RootM(), meteoURL);
            double Kelvin = meteoData.main.temp;
            Search.Celsius = Math.Round(Kelvin - 273.15).ToString() + "°C";
            Search.Fahrenheit = Math.Round((Kelvin - 273.15) * 9/5 + 32).ToString() + "°F";
            Search.Time_Description = NiceArray(meteoData.weather[0].description);
            Search.Humidity = meteoData.main.humidity.ToString();

            
            // Prevision with Id
            string City_Id = meteoData.id.ToString();
            string previsionURL = string.Format(previsions_url, City_Id, Parametres.APIKey);
            var previsionData = Deserialise<RootP>(new RootP(), previsionURL);
            Search.Previsions = PrevisionMaker(previsionData);


            // DEBUGG
            Console.Write(Search.CityName + " : ");
            Console.WriteLine(Search.Time_Description); 


        
            // Icon Link with Meteo
            Search.MeteoPicture = string.Format(icons_url, meteoData.weather[0].icon);
            ImageDecompression(Search.MeteoPicture, @"images/image.png");

        }






        public static void ImageDecompression(string url, string path){
            using (WebClient c = new WebClient()) {
                c.DownloadFile(new Uri(url), path);
            }
        }
        
        public string NiceArray(string text){
            string[] list = text.Split(" ");
            for (int i = 0; i < list.Count(); i++)
            {
                string str = list[i];
                list[i] = char.ToUpper(str[0]) + str.Substring(1);
            }
            string joined = string.Join<string>(" ", list);

            return joined;
        }






        public List<Prevision> PrevisionMaker(RootP Data){

            List<Prevision> Previsions = new List<Prevision>();
            List<List2> days = new List<List2>();
            days = Data.list.FindAll(e => (DateTime.ParseExact(e.dt_txt, "yyyy-MM-dd HH:mm:ss",null).Hour == 12));

            foreach (List2 day in days) {

                Prevision dailyForecast = new Prevision();
                DateTime dd = DateTime.ParseExact(day.dt_txt, "yyyy-MM-dd HH:mm:ss",null);
                double Kelvin = day.main.temp;
                dailyForecast.Search.Celsius = Math.Round(Kelvin - 273.15).ToString() + "°C";
                dailyForecast.Search.Fahrenheit = Math.Round((Kelvin - 273.15) * 9/5 + 32).ToString() + "°F";
                dailyForecast.Search.Time_Description = NiceArray(day.weather[0].description);
                dailyForecast.Search.Humidity = day.main.humidity.ToString();
                dailyForecast.Search.MeteoPicture = string.Format(icons_url, day.weather[0].icon);
                dailyForecast.Date = NiceArray(dd.ToString("ddd dd MMM yyyy", new System.Globalization.CultureInfo(Parametres.Langue)));
                dailyForecast.Hour = dd.ToString("HH:mm" , new System.Globalization.CultureInfo(Parametres.Langue));

                Previsions.Add(dailyForecast);
            }
            return Previsions;
        }





        public string GGTrad(List<RootG> geoData){
            if (geoData[0].local_names == null){
                Console.WriteLine("Traduction Impossible ;(");
                return geoData[0].name;
            }
            string langue = Parametres.Langue.Substring(0,2);
            var l = geoData[0].local_names;
            Dictionary<string, string> dict = geoData[0].local_names.GetType().GetProperties().ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(geoData[0].local_names, null));
            Parametres.Languages = new List<string>(dict.Keys);
            string name = dict[langue];
            if (name == null){
                return geoData[0].name;
            }
            return name;
        }
    }
}