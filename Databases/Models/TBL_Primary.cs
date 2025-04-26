using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Databases.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace Databases.Models
    {
        public class TBL_Primary
        {
            [Key]
            public int Ref_Id { get; set; } = new int();
            public int? OrgenaztionId { get; set; }
            public string? RegionName { get; set; }
            public string? RoleName { get; set; }
            public string? CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public bool? IsApproval { get; set; }
            public ICollection<Section>? Sections { get; set; }
        }

        public class Section
        {
            [Key]
            public int Ref_ID { get; set; } = new int();

            public string? Id { get; set; }
            public string? Title { get; set; }
            public bool? IsOpen { get; set; }
            public bool? IsComplete { get; set; }

            public int? TBL_PrimaryId { get; set; }
            [ForeignKey("TBL_PrimaryId")]
            public TBL_Primary? TBL_Primary { get; set; }

            public ICollection<Fields>? Fields { get; set; }
        }

        public class Fields
        {
            [Key]
            public int Ref_ID { get; set; } = new int();

            public string? Id { get; set; }
            public string? Label { get; set; }
            public string? Unit { get; set; }
            public string? Value { get; set; }
            public string? BaselineValue { get; set; }
            public DateTime? BaselineDate { get; set; }
            public string? TargetValue { get; set; }
            public DateTime? TargetDate { get; set; }
            public string? ChangePercentage { get; set; }

            public int? SectionRef_ID { get; set; }
            [ForeignKey("SectionRef_ID")]
            public Section? Section { get; set; }
        }
    }

}
