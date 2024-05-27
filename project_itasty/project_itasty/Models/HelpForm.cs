using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class HelpForm
{
    public int FormId { get; set; }

    public int UserId { get; set; }

    public string QuestionType { get; set; } = null!;

    public string QuestionContent { get; set; } = null!;

    public byte[]? QuestionImage { get; set; }

    public virtual UserInfo User { get; set; } = null!;
}
