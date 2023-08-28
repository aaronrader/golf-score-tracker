using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfAPI.DAL.DomainClasses
{
    public class Hole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int Par { get; set; }

        [Required]
        public int Distance { get; set; }
    }
}
