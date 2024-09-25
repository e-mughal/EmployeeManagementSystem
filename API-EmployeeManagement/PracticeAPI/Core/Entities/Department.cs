using System.ComponentModel.DataAnnotations;

namespace PracticeAPI.Core.Entities
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public string Employee { get; set; }
    }
}
