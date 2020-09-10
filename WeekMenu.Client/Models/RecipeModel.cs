using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

using System.Threading.Tasks;

namespace WeekMenu.Client.Models
{
    public class RecipeModel
    {
        public int RecipeModelID { get; set; }

        [Required]
        [MaxLength(200)]
        public string RecipeName { get; set; }
        public string How { get; set; }
        public string Ingredients { get; set; }
        public bool IsVegan { get; set; }
        public bool IsBreakfast { get; set; }
        public bool IsSecondBreakfast { get; set; }
        public bool IsLunch { get; set; }
        public bool IsAfternoonTea { get; set; }
        public bool IsDinner { get; set; }
        public string RecipeImageName { get; set; }
        public byte[] RecipeImageFile { get; set; }


    }
}
