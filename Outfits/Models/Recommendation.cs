using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Outfits.Models
{
    public class Recommendation
    {
        [Key]
        public int Id { get; set; }
        public int OutfitPostId { get; set; }
        public string Recommendation1 { get; set; }
        public string Recommendation2 { get; set; }
        public string Recommendation3 { get; set; }

        public Recommendation()
        {
            Recommendation1 = "";
            Recommendation2 = "";
            Recommendation3 = "";
        }
    }
}
