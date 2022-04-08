using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEntities
{
    public class ProjectMember
    {
        [Key]
        public long MemberId { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime JoinedOn { get; set; }
        public bool IsActive { get; set; }

    }
}
