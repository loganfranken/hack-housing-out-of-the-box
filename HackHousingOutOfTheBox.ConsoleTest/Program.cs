using SODA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Esri.ArcGISRuntime.Tasks.Geocoding;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Tasks.Query;
using HackHousingOutOfTheBox.Services;
using Geocoding.Google;
using Geocoding;
using Newtonsoft.Json;

namespace HackHousingOutOfTheBox.ConsoleTest
{
    class Program
    {
        public static void Main(string[] args)
        {
            /*
            while(true)
            {
                Test();
            }
            */
            FairMarketRentService service = new FairMarketRentService();
            FairMarketRentInfo info = service.GetFairMarketRent("-122.3359059", "47.614848");

            //FairMarketRentService service = new FairMarketRentService();
            //service.GetFairMarketRent("King County", "WA", 3);

            // Geocode an address
            // ...

            // Use this to get fair market value
            // ...

            //GeocodeGoogleAddress("150 GRANDVIEW WAY MISSOULA MT");
              
            //Esri.ArcGISRuntime.Layers.

            /*
            while(true)
            {
                HudServiceExample();
            }
             * */

            //http://services.arcgis.com/VTyQ9soqVukalItT/ArcGIS/rest/services/MultiFamilyProperties/FeatureServer/0/query?where=LAT=46.815616+AND+LON=-114.02801&outFields=*1
        }

        /*
        public static void GetFmrViaWebRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://services.arcgis.com/VTyQ9soqVukalItT/arcgis/rest/services/FairMarketRents/FeatureServer/0/query?where=1%3D1&objectIds=&time=&geometry=%7B%22x%22%3A+-122.3359059%2C+%22y%22%3A+47.614848%2C+%22spatialReference%22%3A+%7B%22wkid%22%3A+4326%7D%7D&geometryType=esriGeometryPoint&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Meter&outFields=*&returnGeometry=false&maxAllowableOffset=&geometryPrecision=&outSR=&returnIdsOnly=false&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&resultOffset=&resultRecordCount=&returnZ=false&returnM=false&quantizationParameters=&f=pjson&token=");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader loResponseStream = new StreamReader(response.GetResponseStream());
            string content = loResponseStream.ReadToEnd();

            var result = JsonConvert.DeserializeObject<dynamic>(content);
            result["features"][0]["attributes"]["FMR_0BDR"]
        }

         * /




        public async static void Test()
        {
            var queryTask = new QueryTask(new Uri("http://services.arcgis.com/VTyQ9soqVukalItT/arcgis/rest/services/FairMarketRents/FeatureServer/0"));
            var timeWindow = new Esri.ArcGISRuntime.Data.TimeExtent(DateTime.Now.Subtract(new TimeSpan(0, 5, 0, 0)), DateTime.Now);

            var queryParams = new Query(timeWindow);
            //queryParams.Where = String.Format("(STD_ADDR = '{0}' AND STD_CITY = '{1}' AND STD_ST = '{2}' AND STD_ZIP5 = '{3}')", address, city, state, zipCode);
            queryParams.OutFields = OutFields.All;

            QueryResult queryResult = await queryTask.ExecuteAsync(queryParams);
            //return "";
        }

        public async static void HudServiceExample()
        {
            var uri = "http://services.arcgis.com/VTyQ9soqVukalItT/ArcGIS/rest/services/MultiFamilyProperties/FeatureServer/0/";
            var queryTask = new QueryTask(new Uri(uri));

            var timeWindow = new Esri.ArcGISRuntime.Data.TimeExtent
        (DateTime.Now.Subtract(new TimeSpan(0, 5, 0, 0)), DateTime.Now); // 5 minutes ago to present

var queryParams = new Esri.ArcGISRuntime.Tasks.Query.Query(timeWindow);
queryParams.Where = "(STD_ADDR = '150 Grandview Way' AND STD_CITY = 'Missoula' AND STD_ST = 'MT' AND STD_ZIP5 = '59803')";
queryParams.OutFields = OutFields.All;

            QueryResult queryResult = await queryTask.ExecuteAsync(queryParams);
        }

        public async static void GeocodeAddress(string text)
        {
            var uri = new Uri("http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer");
            var token = String.Empty;
            var locator = new OnlineLocatorTask(uri, token);

            var findParams = new OnlineLocatorFindParameters(text);
            findParams.SourceCountry = "US";
            findParams.OutFields = OutFields.All;

            var results = await locator.FindAsync(findParams, new System.Threading.CancellationToken());

            string address = results[0].Feature.Attributes["StAddr"].ToString();
            string city = results[0].Feature.Attributes["City"].ToString();
            string state = results[0].Feature.Attributes["Region"].ToString();
            string postalCode = results[0].Feature.Attributes["Postal"].ToString();
        }

        public static void GeocodeGoogleAddress(string text)
        {
            IGeocoder geocoder = new GoogleGeocoder();
            IEnumerable<Address> addresses = geocoder.Geocode(text);
            Console.WriteLine("Formatted: " + addresses.First().FormattedAddress); //Formatted: 1600 Pennslyvania Avenue Northwest, Presiden'ts Park, Washington, DC 20500, USA
            Console.WriteLine("Coordinates: " + addresses.First().Coordinates.Latitude + ", " + addresses.First().Coordinates.Longitude); //Coordinates: 38.8978378, -77.0365123
        }

        public static void ZillowHudTest()
        {

        }

        public class VacantUnit
        {
            public string Property { get; set; }
            public string Unit_Name { get; set; }
            public string Unit_Size { get; set; }
            public DateTime Last_Occupied_Date { get; set; }
        }

        public const string VacantUnitKey = "bdcf-2t4x";

        public static void SocrataTest()
        {
            var client = new SodaClient("communities.socrata.com", "PyK6afb9LJDUZLGnmLPMLREvZ");

            //read metadata of a dataset using the resource identifier (Socrata 4x4)
            var metadata = client.GetMetadata(VacantUnitKey);
            Console.WriteLine("{0} has {1} views.", metadata.Name, metadata.ViewsCount);

            //get a reference to the resource itselfs
            //the result (a Resouce object) is a generic type
            //the type parameter represents the underlying rows of the resource
            //var dataset = client.GetResource<Dictionary<string, object>>(VacantUnitKey);

            //of course, a custom type can be used as long as it is JSON serializable
            var dataset = client.GetResource<VacantUnit>(VacantUnitKey);

            //Resource objects read their own data
            var allRows = dataset.GetRows();
            var first10Rows = dataset.GetRows(10);

            /*
            //collections of an arbitrary type can be returned
            //using SoQL and a fluent query building syntax
            var soql = new SoqlQuery().Select("column1", "column2")
                                      .Where("something > nothing")
                                      .Group("column3");

            var results = dataset.Query<MyOtherClass>(soql);
            */
        /*}*/
    }
}
