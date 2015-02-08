using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHousingOutOfTheBox.Services
{
    public class ArcGisBaseUri
    {
        private string _url;

        public ArcGisBaseUri(string apiId, string apiName)
        {
            _url = String.Format("http://services.arcgis.com/{0}/arcgis/rest/services/{1}/FeatureServer/0/", apiId, apiName);
        }

        public override string ToString()
        {
            return _url.ToString();
        }
    }
}
