using System;
using System.Collections.Generic;

namespace Final_project_test.Models;

public partial class UserFollower
{
    public int UserId { get; set; }

    public int FollowerId { get; set; }

    public DateTime FollowDate { get; set; }

    public DateTime? UnfollowDate { get; set; }

    public virtual UserInfo Follower { get; set; } = null!;

    public virtual UserInfo User { get; set; } = null!;
}
