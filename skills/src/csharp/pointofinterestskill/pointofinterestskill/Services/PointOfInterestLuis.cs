using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
namespace Luis
{
    public class pointofinterestLuis: IRecognizerConvert
    {
        public string Text;
        public string AlteredText;
        public enum Intent {
            None, 
            NAVIGATION_CANCEL_ROUTE, 
            NAVIGATION_FIND_POINTOFINTEREST, 
            NAVIGATION_ROUTE_FROM_X_TO_Y, 
            NAVIGATION_FIND_PARKING
        };
        public Dictionary<Intent, IntentScore> Intents;

        public class _Entities
        {
            // Simple entities
            public string[] KEYWORD;
            public string[] ADDRESS;

            // Built-in entities
            public double[] number;

            // Lists
            public string[][] ROUTE_TYPE;

            // Instance
            public class _Instance
            {
                public InstanceData[] KEYWORD;
                public InstanceData[] ADDRESS;
                public InstanceData[] number;
                public InstanceData[] ROUTE_TYPE;
            }
            [JsonProperty("$instance")]
            public _Instance _instance;
        }
        public _Entities Entities;

        [JsonExtensionData(ReadData = true, WriteData = true)]
        public IDictionary<string, object> Properties {get; set; }

        public void Convert(dynamic result)
        {
            var app = JsonConvert.DeserializeObject<pointofinterestLuis>(JsonConvert.SerializeObject(result));
            Text = app.Text;
            AlteredText = app.AlteredText;
            Intents = app.Intents;
            Entities = app.Entities;
            Properties = app.Properties;
        }

        public (Intent intent, double score) TopIntent()
        {
            Intent maxIntent = Intent.None;
            var max = 0.0;
            foreach (var entry in Intents)
            {
                if (entry.Value.Score > max)
                {
                    maxIntent = entry.Key;
                    max = entry.Value.Score.Value;
                }
            }
            return (maxIntent, max);
        }
    }
}
