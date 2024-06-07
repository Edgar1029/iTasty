using System.Collections.Generic;

namespace project_itasty.Models
{
    public class Backgroud_Control_Model
    {
        public Background_Control_ChartData? ChartViewsData { get; set; }
        public Background_Control_ChartData? ChartMembershipData{ get; set; }

        public List<Background_Control_RecipedTable>? RecipedTable { get; set; }

    }

    public class Background_Control_ChartData
    {
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
    }

    public class Background_Control_RecipedTable 
    {
        public string RecipedName { get; set; }
        public int Author { get; set; }
        public int RecipedView { get; set; }
        public int NumberOfComment { get; set; }
        public string RecipedStatus { get; set; }
    }
}
