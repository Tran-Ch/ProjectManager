using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class TakePart
    {
        public Guid ProjectId { get; private set; }
        public Guid EmployeeId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public TakePart(Guid projectId, Guid employeeId, DateTime startDate, DateTime? endDate)
        {
            ProjectId = projectId;
            EmployeeId = employeeId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool IsParticipatingAt(DateTime date)
        {
            return date >= StartDate && (EndDate == null || date <= EndDate);
        }
    }
}
