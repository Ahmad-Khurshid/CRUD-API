using System;
using System.Collections.Generic;

namespace CRUD_APi.Models
{
    public partial class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; } = null!;
        public int EmpSalary { get; set; }
        public int? EmpDepartmentId { get; set; }

        public virtual Department? EmpDepartment { get; set; }
    }
}
