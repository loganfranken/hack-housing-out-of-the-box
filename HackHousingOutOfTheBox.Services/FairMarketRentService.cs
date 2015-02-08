using Esri.ArcGISRuntime.Tasks.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HackHousingOutOfTheBox.Services
{
    public class FairMarketRentService
    {
        public FairMarketRentInfo GetFairMarketRent(string latitude, string longitude)
        {
            string url = String.Format("http://services.arcgis.com/VTyQ9soqVukalItT/arcgis/rest/services/FairMarketRents/FeatureServer/0/query?where=1%3D1&objectIds=&time=&geometry=%7B%22x%22%3A+{0}%2C+%22y%22%3A+{1}%2C+%22spatialReference%22%3A+%7B%22wkid%22%3A+4326%7D%7D&geometryType=esriGeometryPoint&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Meter&outFields=*&returnGeometry=false&maxAllowableOffset=&geometryPrecision=&outSR=&returnIdsOnly=false&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&resultOffset=&resultRecordCount=&returnZ=false&returnM=false&quantizationParameters=&f=pjson&token=", longitude, latitude);    
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);

            using(HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            using(StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string jsonResponse = stream.ReadToEnd();
                var result = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                var attributes = result["features"][0]["attributes"];

                FairMarketRentInfo info = new FairMarketRentInfo();
                info.Efficiency = (int) attributes["FMR_0BDR"].Value;
                info.OneBedroom = (int) attributes["FMR_1BDR"].Value;
                info.TwoBedrooms = (int) attributes["FMR_2BDR"].Value;
                info.ThreeBedrooms = (int) attributes["FMR_3BDR"].Value;
                info.FourBedrooms = (int) attributes["FMR_4BDR"].Value;

                return info;
            }
        }
    }
}
