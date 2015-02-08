using Geocoding;
using Geocoding.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHousingOutOfTheBox.Services
{
    public class GeocodingService
    {
        public LocationInfo Geocode(string input)
        {
            IGeocoder geocoder = new GoogleGeocoder();
            IEnumerable<Address> addresses = geocoder.Geocode(input);

            GoogleAddress address = (GoogleAddress) addresses.First();

            return new LocationInfo
            {
                Latitude = address.Coordinates.Latitude.ToString(),
                Longitude = address.Coordinates.Longitude.ToString(),
                City = address[GoogleAddressType.Locality].LongName,
                State = address[GoogleAddressType.AdministrativeAreaLevel1].ShortName
           };
        }
    }
}
