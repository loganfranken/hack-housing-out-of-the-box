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
            // Geocode an address provided by the user
            GeocodingService geoCodingService = new GeocodingService();
            LocationInfo locationInfo = geoCodingService.Geocode("150 GRANDVIEW WAY MISSOULA MT");

            // Use the information from the geocoded address to get Fair Market Rent
            FairMarketRentService fmrService = new FairMarketRentService();
            FairMarketRentInfo fmrInfo = fmrService.GetFairMarketRentInfo(locationInfo.Latitude, locationInfo.Longitude);
            
            // Use the information from the geocoded address to get Public Housing Authority Conact Information
            PublicHousingAuthorityInfoService phaiService = new PublicHousingAuthorityInfoService();
            PublicHousingAuthorityInfo info = phaiService.GetPublicHousingAuthorityInfo(locationInfo.City, locationInfo.State);
        }
    }
}
