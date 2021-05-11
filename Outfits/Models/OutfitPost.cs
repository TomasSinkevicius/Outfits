using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;


namespace Outfits.Models
{
    public class OutfitPost
    {
        [Key]
        public int Id { get; set; }
        public string ImageName { get; set; }

        public string username { get; set; }
        public string Description { get; set; }
        public string Product1 { get; set; }

        public string Product2 { get; set; }

        public string Product3 { get; set; }


        [NotMapped]
        [DisplayName("Įkelti nuotrauką")]
        public IFormFile ImageFile { get; set; }

        public OutfitPost()
        {
        }
    }
}
