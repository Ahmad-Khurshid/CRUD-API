using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUD_APi.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly EmpDatabaseContext emp;
        public EmployeeApiController(EmpDatabaseContext emp)
        {
            this.emp = emp;
        }
        [HttpGet("GetAllData")]
        public async Task<ActionResult<List<string>>> GetAllData()
        {
            var result = await emp.Employees.ToListAsync();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                List<string> list = new List<string>()
                {
                    "No Record Found"
                };
                return list;
            }
        }
        [HttpGet("GetEmpNameById/{id}")]
        public async Task<ActionResult<string>> GetEmpNameById(int id)
        {
            var result = await emp.Employees.Where(x => x.EmpId == id).FirstOrDefaultAsync();

            if (result != null)
            {
                return Ok("Employee name is :" + result.EmpName + "\nEmployee Salary is " + result.EmpSalary);
            }
            else
            {
                return $"No Record Found for Id ={id}"; // Return a 404 Not Found response if no employee is found with the specified id.
            }
        }

        [HttpGet("GetEmpNameByD_Name")]
        public async Task<ActionResult<List<string>>> GetEmpNameByDepartmentName(string DepartmentName)
        {
            var result = await (from employees in emp.Employees
                                join dep in emp.Departments
                                on employees.EmpDepartmentId equals dep.DepartmentId
                                where dep.DepartmentName == DepartmentName
                                select new
                                {
                                    employee = employees.EmpName
                                    // ,Department = dep.DepartmentName
                                }).ToListAsync();
            var result2 = await emp.Employees.Join(emp.Departments,
                                             e => e.EmpDepartmentId,
                                             o => o.DepartmentId,
                                             (e, o) => new
                                             {
                                                 EmpName = e.EmpName
                                             }).ToListAsync();
            return Ok(result);
        }
        [HttpPost("Create-New-Record")]
        public async Task<ActionResult> CreateEmp(EmployeeDto Emp)
        {
            var employee = new Employee()
            {
                EmpName = Emp.EmpName,
                EmpSalary = Emp.EmpSalary,
                EmpDepartmentId = Emp.EmpDepartmentId
            };
            var chk1 = await emp.Departments.AnyAsync(x => x.DepartmentId == employee.EmpDepartmentId);
            if (chk1 == true)
            {
                await emp.Employees.AddAsync(employee);
                await emp.SaveChangesAsync();
                return Ok(Emp);
            }
            else
            {
                return BadRequest("Foreign Key Constraint Violation");
            }
            //await emp.Employees.AddAsync(Emp);
            //await emp.SaveChangesAsync();
            //return Ok(Emp);
        }

        //public ActionResult GetEmpNameById(int id)
        //{
        //    var result = emp.Employees.ToList().ElementAt(id);

        //    return Ok(result);
        //}
        [HttpPut("UpdateEmpById/{id}")]
        public async Task<ActionResult<string>> UpdateEmpById(int id, EmployeeDto emp)
        {

            var ExistId = this.emp.Employees.Any(x => x.EmpId == id);

            if (ExistId != true)
            {
                return BadRequest("No id for updated record Exists!!");
            }
            var ExistDepId = this.emp.Employees.Any(x => x.EmpDepartmentId == emp.EmpDepartmentId);
            if (ExistDepId == false)
            {
                return BadRequest("Violation of Foreign Key Constraint");
            }
            var employee = this.emp.Employees.Find(id);
            employee.EmpName = emp.EmpName;
            employee.EmpSalary = emp.EmpSalary;
            employee.EmpDepartmentId = emp.EmpDepartmentId;
            await this.emp.SaveChangesAsync();
            //this.emp.Entry(emp).State = EntityState.Modified;
            //await this.emp.SaveChangesAsync();
            return Ok(emp);

        }
        [HttpDelete("DeleteEmployee")]
        public async Task<ActionResult<string>> DelEmpById(int id)
        {
            var DelEmp = await emp.Employees.FindAsync(id);
            if (DelEmp != null)
            {
                emp.Employees.Remove(DelEmp);
                await emp.SaveChangesAsync();
                return Ok(DelEmp);
            }
            return NotFound();
        }

    }
}
