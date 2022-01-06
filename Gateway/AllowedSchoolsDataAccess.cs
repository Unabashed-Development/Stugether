using Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Gateway
{
    public static class AllowedSchoolsDataAccess
    {
        /// <summary>
        /// Uses the https://github.com/Hipo/university-domains-list API to call all Dutch schools.
        /// </summary>
        /// <returns>IEnumerable list of AllowedSchool classes.</returns>
        public static IEnumerable<AllowedSchool> APICallAllowedSchools()
        {
            const string url = "http://universities.hipolabs.com/search";
            const string urlParameters = "?country=netherlands";

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body
                IEnumerable<AllowedSchool> allowedSchoolList = response.Content.ReadAsAsync<IEnumerable<AllowedSchool>>().Result;
                return allowedSchoolList;
            }
            else
            {
                throw new AccessViolationException($"{nameof(APICallAllowedSchools)} failed");
            }
        }
    }
}
