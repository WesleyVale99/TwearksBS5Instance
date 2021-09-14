using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwearksBS5Instance.Classes
{
    public class ImputMapper
    {
        [JsonProperty("ControlSchemes")]
        public List<Hud> ControlSchemes { get; set; }
       
        public class Hud
        {
            [JsonProperty("BuiltIn")]
            public bool BuiltIn { get; set; }

            [JsonProperty("GameControls")]
            public List<Mapper> GameControls { get; set; }

        }
        public class Mapper
        {
            [JsonProperty("Tweaks")]
            public int Tweaks { get; set; }

            [JsonProperty("ExclusiveDelay")]
            public int ExclusiveDelay { get; set; }
        }
    }
}
