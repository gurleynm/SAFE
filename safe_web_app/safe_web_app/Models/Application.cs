//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace safe_web_app.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Application
    {
        public int id { get; set; }
        public string title { get; set; }
        public string price { get; set; }
        public string app_desc { get; set; }
        public string genre { get; set; }
        public Nullable<double> rating { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
        public bool approved { get; set; }
        public string developer { get; set; }
        public Nullable<double> sumOfRates { get; set; }
        public Nullable<int> numOfPeopleWhoRated { get; set; }
    }
}
