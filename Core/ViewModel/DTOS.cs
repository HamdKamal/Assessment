using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class FieldDto
    {
        public int Ref_ID { get; set; }
        public string? Label { get; set; }
        public string? Unit { get; set; }
        public string? Value { get; set; }
        public string? BaselineValue { get; set; }
        public DateTime? BaselineDate { get; set; }
        public string? TargetValue { get; set; }
        public DateTime? TargetDate { get; set; }
        public string? ChangePercentage { get; set; }
    }

    public class SectionDto
    {
        public int Ref_ID { get; set; }
        public string? Id { get; set; }
        public string? Title { get; set; }
        public bool? IsOpen { get; set; }
        public bool? IsComplete { get; set; }

        public List<FieldDto>? Fields { get; set; }
    }

    public class TBLPrimaryDto
    {
        public int Ref_Id { get; set; }
        public int? OrgenaztionId { get; set; }
        public string? RegionName { get; set; }
        public string? RoleName { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsApproval { get; set; }
        public List<SectionDto>? Sections { get; set; }
    }
}
