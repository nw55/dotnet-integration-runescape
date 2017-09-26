using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace NW55.Integration.RuneScape.Api
{
    public class PlayerDetailsApi : RuneScapeApi<IList<string>, IList<PlayerDetails>>
    {
        static string ExtractJsonFromJsonpCallback(string jsonp)
        {
            // jQuery000000000000000_0000000000(...json...);
            int argStart = jsonp.IndexOf('(') + 1;
            int argEnd = jsonp.LastIndexOf(')');
            return jsonp.Substring(argStart, argEnd - argStart);
        }

        public override Encoding OverrideResponseEncoding => IsoEncoding;

        public override string GetUri(IList<string> playerNames)
        {
            StringWriter jsonWriter = new StringWriter();
            JsonSerializer.CreateDefault().Serialize(jsonWriter, playerNames);
            string playerNamesString = Uri.EscapeDataString(jsonWriter.ToString());
            return $"{ServicesBaseUri}/m=website-data/playerDetails.ws?names={playerNamesString}&callback=jQuery000000000000000_0000000000";
        }

        public override IList<PlayerDetails> ParseResult(IList<string> playerNames, string responseText)
        {
            if (responseText == null)
                return null;

            string json = ExtractJsonFromJsonpCallback(responseText);
            ApiJsonEntry[] apiObjects = JsonConvert.DeserializeObject<ApiJsonEntry[]>(json);

            PlayerDetails[] result = new PlayerDetails[playerNames.Count];
            Dictionary<string, int> nameToIndex = playerNames.Select((name, index) => (name, index)).ToDictionary(pair => pair.name, pair => pair.index);

            foreach (ApiJsonEntry apiObject in apiObjects)
            {
                PlayerDetails player = new PlayerDetails(apiObject.name, apiObject.clan, apiObject.recruiting ?? false, apiObject.title, apiObject.isSuffix);
                int index = nameToIndex[apiObject.name];
                result[index] = player;
            }

            return result;
        }

#pragma warning disable CS0649 // no value assigned to field
        // fields are assigned by JsonConvert.DeserializeObject
        class ApiJsonEntry
        {
            public bool isSuffix;
            public bool? recruiting;
            public string name;
            public string clan;
            public string title;
        }
#pragma warning restore CS0649
    }
}
