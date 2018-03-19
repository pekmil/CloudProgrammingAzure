using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTodoWebApplication1.Models
{
    public class Todo
    {
        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Todo name is required!")]
        [StringLength(100, ErrorMessage = "Todo name max. length is 100!")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Todo description max. length is 1000!")]
        public string Description { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [StringLength(200, ErrorMessage = "PhotoUrl max. length is 200!")]
        public string PhotoUrl { get; set; }
        [NotMapped]
        public string PhotoLabels { get; set; }
    }
}
