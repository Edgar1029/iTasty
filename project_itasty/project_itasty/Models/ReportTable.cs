using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_itasty.Models
{
    public partial class ReportTable
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        public int RecipedIdOrCommentId { get; set; }

        [Required]
        public int ReportedUserId { get; set; }

        [Required]
        public int ReportType { get; set; }

        public string? ReportReason { get; set; }

        public bool? ReportStatus { get; set; }

        public int? ReportUserId { get; set; }

        [ForeignKey("ReportUserId")]
        public virtual UserInfo? Report { get; set; }

        [ForeignKey("ReportedUserId")]
        public virtual UserInfo? ReportedUser { get; set; }
    }
}
