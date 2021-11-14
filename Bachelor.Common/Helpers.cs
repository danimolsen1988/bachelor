using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor.Common
{
    public static class Helpers
    {
        /// <summary>
        /// Adds the EntityPath parameter to an Event Hubs connection string, if it does not exist.
        /// </summary>
        /// <param name="connectionString">The root connection string.</param>
        /// <param name="eventHubName">The name of the event hub, or entity.</param>
        /// <returns></returns>
        public static string CreateEventHubsConnectionString(string connectionString, string eventHubName)
        {
            var eventHubsConnectionString = connectionString;

            if (!connectionString.ToLower().Contains("entitypath="))
            {
                eventHubsConnectionString = $"{connectionString};EntityPath={eventHubName}";
            }

            return eventHubsConnectionString;
        }
    }
}
