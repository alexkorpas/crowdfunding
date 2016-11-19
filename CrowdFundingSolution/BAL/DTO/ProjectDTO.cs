using System;


namespace BAL
{
    public class ProjectDTO
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public string UserFK { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public decimal Goal { get; set; }
        public decimal? GoalMin { get; set; }
        public int? MainPhotoFK { get; set; }
        public string Video { get; set; }
        public int? CategoryFK { get; set; }
        public string CategoryDesc { get; set; }
        public string CategoryName { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? BlockedDate { get; set; }
        public int? StateFK { get; set; }
        public string Website { get; set; }

        public virtual decimal Progress { get; set; }
        public virtual int Backers { get; set; }
        public virtual decimal AmountGathered { get; set; }
        public virtual string Left { get; set; }

    } // End class
} // End namespace
