using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfAPI.DAL.DomainClasses
{
    public class ScoreHole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ScoreId")]
        public Score? Score { get; set; }

        [Required]
        public int ScoreId { get; set; }

        [ForeignKey("HoleId")]
        public Hole? Hole { get; set; }

        [Required]
        public int HoleId { get; set; }

        [Required]
        public int Strokes { get; set; }
    }
}
