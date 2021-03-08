using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDragon
{
    public class ChampionList
    {
        [JsonProperty(PropertyName = "data")]
        public Dictionary<string, Champion> champions;
    }

    public class Champion
    {
        public string key;
        public string id;
        public string name;
    }
}

