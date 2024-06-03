using System.Diagnostics.Contracts;

namespace EmployeeAPP.Models
{
    public class EmployeeModel
    {
        public int? EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public int MobileNum { get; set; }
        public string? EmailID { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Place { get; set; }
        public string? Department { get; set; }
        public int? SuccessRate { get; set; }
        public List<EmployeeList>? EmployeeListView { get; set; }
        public List<DepartmentList>? DepartmentListView { get; set; }
    }

    public class EmployeeList
    {
        public int? EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public int MobileNum { get; set; }
        public string? EmailID { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Place { get; set; }
        public string? Department { get; set; }
    }

    public class DepartmentList
    {
        public int? DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
    }
}
