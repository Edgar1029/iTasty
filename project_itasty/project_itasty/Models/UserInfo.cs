using System;
using System.Collections.Generic;

namespace project_itasty.Models;

public partial class UserInfo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public byte[]? Photo { get; set; }

    public virtual ICollection<UserFollower> UserFollowerFollowers { get; set; } = new List<UserFollower>();

    public virtual ICollection<UserFollower> UserFollowerUsers { get; set; } = new List<UserFollower>();
}
