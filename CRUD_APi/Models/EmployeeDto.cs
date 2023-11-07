namespace CRUD_APi.Models
{
    public class EmployeeDto
    {
       // public int EmpId { get; set; }
        public string EmpName { get; set; } = null!;
        public int EmpSalary { get; set; }
        public int? EmpDepartmentId { get; set; }
    }
}
