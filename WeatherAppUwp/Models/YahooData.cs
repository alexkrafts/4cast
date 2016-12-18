using System;

namespace WeatherAppUwp.Models
{
    public class Rootobject
    {
        public Query query { get; set; }
    }

    public class Query
    {
        public int count { get; set; }
        public DateTime created { get; set; }
        public string lang { get; set; }
        public Results results { get; set; }
    }

    public class Results
    {
        public Channel channel { get; set; }
    }

    public class Channel
    {
        public Location location { get; set; }
        public Item item { get; set; }
    }

    public class Location
    {
        public string city { get; set; }
        public string country { get; set; }
        public string region { get; set; }
    }

    public class Item
    {
        public Condition condition { get; set; }
    }

    public class Condition
    {
        public string code { get; set; }
        public string date { get; set; }
        public string temp { get; set; }
        public string text { get; set; }
    }

}
