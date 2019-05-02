using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace safe_web_app.Models
{
    public class RequestViewModel
    {
        [Required]
        [Display(Name = "Title")]
        [MaxLength(50)]
        public string Title { get; set; }
        
        [Display(Name = "Price")]
        [MaxLength(50)]
        public string Price { get; set; }
        
        [Display(Name = "Description")]        
        public string Description { get; set; }

        [Display(Name = "Genre")]
        [MaxLength(50)]
        public string Genre { get; set; }

        [Display(Name = "Url")]
        [MaxLength(200)]
        public string Url { get; set; }        
    }
    
    public class CommentViewModel
    {
        public Application application { get; set; }
        public List<Comment> comments { get; set; }
    }

    public class CatalogueViewModel
    {
        public List<Application> applications { get; set; }
        public List<string> genres { get; set; }
        public List<string> developers { get; set; }
    }
}