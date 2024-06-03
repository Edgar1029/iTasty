using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class UserFollower
{
    public int UserId { get; set; }

    public int FollowerId { get; set; }

    public DateOnly FollowDate { get; set; }

    public DateOnly? UnfollowDate { get; set; }

    public virtual UserInfo Follower { get; set; } = null!;

    public virtual UserInfo User { get; set; } = null!;
}
