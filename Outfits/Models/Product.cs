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
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Likes { get; set; }
        public int IsInCart { get; set; }
        public int IsInWishList { get; set; }

        public string HasInCart { get; set; }
        public string HasInWishList { get; set; }

        public string WhoHasLiked { get; set; }

        public string Priority { get; set; }
        public int PriorityLevel { get; set; }
        public Shop Shop { get; set; }

        [NotMapped]
        [DisplayName("Įkelti nuotrauką")]
        public IFormFile ImageFile { get; set; }

        public Product()
        {
            IsInCart = 0;
            IsInWishList = 0;
            WhoHasLiked = "a";
            HasInCart = "a";
            HasInWishList = "a";
            Priority = "";
            PriorityLevel = 0;

        }
    }
}
