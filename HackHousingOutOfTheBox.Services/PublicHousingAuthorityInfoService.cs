using Esri.ArcGISRuntime.Tasks.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHousingOutOfTheBox.Services
{
    public class PublicHousingAuthorityInfoService
    {
        public PublicHousingAuthorityInfo GetPublicHousingAuthorityInfo(string city, string state)
        {
            return GetPublicHousingAuthorityInfoAsync(city, state).Result;
        }

        private async Task<PublicHousingAuthorityInfo> GetPublicHousingAuthorityInfoAsync(string city, string state)
        {
            ArcGisBaseUri uri = new ArcGisBaseUri("VTyQ9soqVukalItT", "PublicHousingAuthorities");
            var queryTask = new QueryTask(new Uri(uri.ToString()));

            var timeWindow = new Esri.ArcGISRuntime.Data.TimeExtent(DateTime.Now.Subtract(new TimeSpan(0, 5, 0, 0)), DateTime.Now);

            var queryParams = new Esri.ArcGISRuntime.Tasks.Query.Query(timeWindow);
            queryParams.Where = String.Format("(STD_CITY = '{0}' AND STD_ST = '{1}')", city, state);
            queryParams.OutFields = OutFields.All;

            QueryResult queryResult = await queryTask.ExecuteAsync(queryParams);

            var attributes = queryResult.FeatureSet.Features[0].Attributes;

            return new PublicHousingAuthorityInfo
            {
                Name = (string) attributes["FORMAL_PARTICIPANT_NAME"],
                Address = (string)attributes["STD_ADDR"],
                Phone = (string)attributes["EXEC_DIR_PHONE"],
                City = (string)attributes["STD_CITY"],
                State = (string)attributes["STD_ST"],
                PostalCode = (string)attributes["STD_ZIP5"],
                Email = (string)attributes["EXEC_DIR_EMAIL"]
            };
        }
    }
}
