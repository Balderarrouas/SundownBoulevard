using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SundownBoulevard.Models
{
    public class Amount
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class BoilVolume
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class Fermentation
    {
        [JsonPropertyName("temp")]
        public Temp Temp { get; set; }
    }

    public class Hop
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("amount")]
        public Amount Amount { get; set; }

        [JsonPropertyName("add")]
        public string Add { get; set; }

        [JsonPropertyName("attribute")]
        public string Attribute { get; set; }
    }

    public class Ingredients
    {
        [JsonPropertyName("malt")]
        public List<Malt> Malt { get; set; }

        [JsonPropertyName("hops")]
        public List<Hop> Hops { get; set; }

        [JsonPropertyName("yeast")]
        public string Yeast { get; set; }
    }

    public class Malt
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("amount")]
        public Amount Amount { get; set; }
    }

    public class MashTemp
    {
        [JsonPropertyName("temp")]
        public Temp Temp { get; set; }

        [JsonPropertyName("duration")]
        public int? Duration { get; set; }
    }

    public class Method
    {
        [JsonPropertyName("mash_temp")]
        public List<MashTemp> MashTemp { get; set; }

        [JsonPropertyName("fermentation")]
        public Fermentation Fermentation { get; set; }

        [JsonPropertyName("twist")]
        public object Twist { get; set; }
    }

    // public class DrinkList
    // {
    //     public List<Drink> Drinks { get; set; }
    // }
    public class Drink
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("tagline")]
        public string Tagline { get; set; }

        [JsonPropertyName("first_brewed")]
        public string FirstBrewed { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("abv")]
        public double? Abv { get; set; }

        [JsonPropertyName("ibu")]
        public double? Ibu { get; set; }

        [JsonPropertyName("target_fg")]
        public double? TargetFg { get; set; }

        [JsonPropertyName("target_og")]
        public double? TargetOg { get; set; }

        [JsonPropertyName("ebc")]
        public double? Ebc { get; set; }

        [JsonPropertyName("srm")]
        public double? Srm { get; set; }

        [JsonPropertyName("ph")]
        public double? Ph { get; set; }

        [JsonPropertyName("attenuation_level")]
        public double? AttenuationLevel { get; set; }

        [JsonPropertyName("volume")]
        public Volume Volume { get; set; }

        [JsonPropertyName("boil_volume")]
        public BoilVolume BoilVolume { get; set; }

        [JsonPropertyName("method")]
        public Method Method { get; set; }

        [JsonPropertyName("ingredients")]
        public Ingredients Ingredients { get; set; }

        [JsonPropertyName("food_pairing")]
        public List<string> FoodPairing { get; set; }

        [JsonPropertyName("brewers_tips")]
        public string BrewersTips { get; set; }

        [JsonPropertyName("contributed_by")]
        public string ContributedBy { get; set; }
    }

    public class Temp
    {
        [JsonPropertyName("value")]
        public int? Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class Volume
    {
        [JsonPropertyName("value")]
        public double? Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }



}