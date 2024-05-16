using System.ComponentModel.DataAnnotations;

namespace BlogsApi.Models
{
    public class Blogs
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        public string BlogTitle { get; set; } = string.Empty;

        [Required]
        public string BlogDescription { get; set; } = string.Empty;

        [Required]
        public string BlogAuthor { get; set; } = string.Empty;
    }
}
