using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class MessageTable
{
    public int MessageId { get; set; }

    public int RecipeId { get; set; }

    public int UserId { get; set; }

    public string MessageContent { get; set; } = null!;

    public int? TopMessageid { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ChangeTime { get; set; }

    public string? ViolationStatus { get; set; }

    public string? ExistDelete { get; set; }

    public virtual RecipeTable Recipe { get; set; } = null!;

    public virtual UserInfo User { get; set; } = null!;
}
