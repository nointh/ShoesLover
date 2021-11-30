using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Tên màu sắc")]
        public string ColorName { get; set; }
        public string ColorImage { get; set; }
        public bool Active { get; set; }
        [NotMapped]
        [DisplayName("Tải ảnh")]
        public IFormFile ImageFile { get; set; }
    }
}
