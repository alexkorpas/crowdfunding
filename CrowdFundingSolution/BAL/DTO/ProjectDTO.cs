using System;


namespace BAL
{
    public class ProjectDTO
    {
        public int Project_Id { get; set; }
        public string Description { get; set; }
        public int User_Id { get; set; }
        public string Title { get; set; }
        public string Short_Description { get; set; }
        public decimal Goal { get; set; }
        public decimal? Goal_Min { get; set; }
        public int? Photo_Id_Main { get; set; }
        public string Video { get; set; }
        public int? Category_Id { get; set; }
        public DateTime? Due_Date { get; set; }
        public bool Is_Active { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Updated_Date { get; set; }
        public DateTime? Deleted_Date { get; set; }
        public DateTime? Blocked_Date { get; set; }
        public int? State_Id { get; set; }
        public string Website { get; set; }
    } // End class
} // End namespace
