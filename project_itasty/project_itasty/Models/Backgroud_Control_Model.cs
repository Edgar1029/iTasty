using System.Collections.Generic;

namespace project_itasty.Models
{
    public class Backgroud_Control_Model
    {
        public Background_Control_ChartData? ChartViewsData { get; set; }
        public Background_Control_ChartData? ChartMembershipData{ get; set; }

        public List<Background_Control_OrderRecipedTable>? OrderRecipedTable { get; set; }

        public List<Background_Control_UserTable>? UserTable { get; set; }

        public List<Background_Control_CommentTable>? CommentTable { get; set; }

        public List<Background_Control_RecipedTable>? RecipedTable { get; set; }

		public List<Background_Control_UserReport>? UserReport { get; set; }

		public List<Background_Control_RecipeReport>? RecipeReport { get; set; }

		public List<Background_Control_IssueReport>? IssueReport { get; set; }

	}

    public class Background_Control_ChartData
    {
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
    }

    public class Background_Control_OrderRecipedTable
	{
        public string? RecipedName { get; set; }
        public string? Author { get; set; }
        public int RecipedView { get; set; }
        public int NumberOfComment { get; set; }
        public string? RecipedStatus { get; set; }
    }

    public class Background_Control_UserTable 
    {
		public int UserId { get; set; }
		public string UserName { get; set; }

		public string Email { get; set; }

		public int UserStatus { get; set; }
	}

	public class Background_Control_CommentTable
	{
		public int ReportId { get; set; }
		public int RecipeId { get; set; }
		public int UserId { get; set; }

		public string? MessageContent { get; set; }

		public string? ReportReason { get; set; }

		public bool? ReportStatus { get; set; }

		public int? ReportUserId { get; set; }
	}

	public class Background_Control_RecipedTable
	{
		public int RecipedId { get; set; }
		public string? RecipedName { get; set; }
		public string? Author { get; set; }
		public int RecipedView { get; set; }
		public int? NumberOfComment { get; set; }

		public string? RecipedStatus { get;set; }
	}

	public class Background_Control_UserReport {
		public int ReportId { get; set; }
		public int? UserId { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? ReportReason { get; set; }
		public bool? ReportStatus { get; set; }

		public string? ReportUser {  get; set; }


	}
	public class Background_Control_RecipeReport
	{
		public int ReportId { get; set; }
		public int RecipeId { get; set; }
		public string? RecipeName { get; set; }
		public string? Author { get; set; }
		public string? ReportReason { get; set; }
		public bool? ReportStatus { get; set; }

		public string? Reporter { get; set; }

	}
	public class Background_Control_IssueReport
	{
		public int FormId { get; set; }
		public int UserId { get; set; }
		public string? QuestionType { get; set; }
		public string? QuestionContent { get; set; }
		public byte[]? QuestionImage { get; set; }

	}



}
