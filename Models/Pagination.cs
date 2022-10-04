using System.Text.Json.Serialization;

namespace SundownBoulevard.Models
{
    public class Pagination
    {
        public int Page { get; set; }
        
        
        public int PerPage { get; set; }
    }
}