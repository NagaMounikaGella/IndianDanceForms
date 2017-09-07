//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IndianDanceForms.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DanceFormsDetail
    {
        public int Id { get; set; }

        [StringLength(25, MinimumLength = 2)]
        [Required(ErrorMessage = "Error: Please Enter Name" )]
        public string Name { get; set; }

        
        [Required(ErrorMessage = "Error: Please Select Classical or Folk")]
        public string DanceFormType { get; set; }

        [StringLength(50, MinimumLength = 0)]
        public string Origin { get; set; }

        [StringLength(1500, MinimumLength = 0)]
        public string Description { get; set; }

        public Nullable<int> LikesNo { get; set; }
        public Nullable<int> DisLikesNo { get; set; }
        public string PictureUrl { get; set; }
        public string VideoUrl { get; set; }
        public string WikiUrl { get; set; }
    }
}
