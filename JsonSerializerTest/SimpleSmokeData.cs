using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace JsonSerializerTest
{
    [JsonObject(MemberSerialization.Fields)]
    public class SimpleSmokeData
    {
        public List<CustomSelectList> SelectListItems { get; set; }

        public SimpleSmokeData()
        {
            SelectListItems = new List<CustomSelectList>()
            {
                new CustomSelectList() {Text = "Smoke Potential Argos", Value = "0"},
                new CustomSelectList() {Text = "Smoke Potential Fuel", Value = "1"},
                new CustomSelectList() {Text = "Smoke Production", Value = "2"},
                new CustomSelectList() {Text = "Soot yield", Value = "3"}
            };
        }
    }

    public class CustomSelectList
    {
        public string Text { get; set; }

        public string Value { get; set; }
    }
}
