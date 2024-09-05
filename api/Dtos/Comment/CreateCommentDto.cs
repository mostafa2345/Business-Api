using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5,ErrorMessage ="Titel must be 5 chars")]
        [MaxLength(280, ErrorMessage = "Titel max is 280 characters")]

        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 chars")]
        [MaxLength(280, ErrorMessage = "Content max is 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}