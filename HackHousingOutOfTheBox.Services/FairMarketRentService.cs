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
        public FairMarketRentInfo GetFairMarketRentInfo(string latitude, string longitude)
        {
            var response = RequestByCoordinate("VTyQ9soqVukalItT", "FairMarketRents", latitude, longitude);

            return new FairMarketRentInfo()
            {
                Efficiency = (int)response["FMR_0BDR"].Value,
                OneBedroom = (int)response["FMR_1BDR"].Value,
                TwoBedrooms = (int)response["FMR_2BDR"].Value,
                ThreeBedrooms = (int)response["FMR_3BDR"].Value,
                FourBedrooms = (int)response["FMR_4BDR"].Value
            };
        }

        public static dynamic RequestByCoordinate(string apiId, string apiName, string latitude, string longitude)
        {
            string url = String.Format("http://services.arcgis.com/{0}/arcgis/rest/services/{1}/FeatureServer/0/query?where=1%3D1&objectIds=&time=&geometry=%7B%22x%22%3A+{2}%2C+%22y%22%3A+{3}%2C+%22spatialReference%22%3A+%7B%22wkid%22%3A+4326%7D%7D&geometryType=esriGeometryPoint&inSR=&spatialRel=esriSpatialRelIntersects&distance=&units=esriSRUnit_Meter&outFields=*&returnGeometry=false&maxAllowableOffset=&geometryPrecision=&outSR=&returnIdsOnly=false&returnCountOnly=false&returnExtentOnly=false&orderByFields=&groupByFieldsForStatistics=&outStatistics=&resultOffset=&resultRecordCount=&returnZ=false&returnM=false&quantizationParameters=&f=pjson&token=", apiId, apiName, longitude, latitude);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string jsonResponse = stream.ReadToEnd();
                var result = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                return result["features"][0]["attributes"];
            }
        }
    }
}
