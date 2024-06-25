using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class ReportTable
{
    public int ReportId { get; set; }

    public int RecipedIdOrCommentId { get; set; }

    public int ReportedUserId { get; set; }

    public int ReportType { get; set; }

    public string? ReportReason { get; set; }

    public bool? ReportStatus { get; set; }

    public int? ReportUserId { get; set; }

    public virtual UserInfo ReportedUser { get; set; } = null!;
}
