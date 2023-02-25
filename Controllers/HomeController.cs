using Dapper;
using EmployeeAPP.Helper;
using EmployeeAPP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace EmployeeAPP.Controllers
{
    public class HomeController : Controller
    {
        private SQLManager sqlManager;

        public HomeController(IConfiguration configuration)
        {
            sqlManager = new SQLManager(configuration);
        }

        public IActionResult Index()
        {
            EmployeeModel ObjEmployeeModel = new EmployeeModel();
            var param = new List<SqlParameter>
            {
                new SqlParameter
                {
                    ParameterName= "@EventID",
                    Value   = "1",
                    SqlDbType= SqlDbType.Int,
                }
            };
            var dt = sqlManager.GetTable("[dbo].[spFetchEmployee]", param);
            ObjEmployeeModel.EmployeeListView = new List<EmployeeList>();
            foreach (DataRow dataRow in dt.Rows)
            {
                ObjEmployeeModel.EmployeeListView.Add(new EmployeeList
                {
                    EmployeeID = Convert.ToInt32(dataRow["EmpID"]),
                    MobileNum = Convert.ToInt32(dataRow["EmpMobNo"]),
                    EmployeeName = dataRow["EmpName"].ToString(),
                    EmailID = dataRow["EmpEmail"].ToString(),
                    Gender = dataRow["EmpGender"].ToString(),
                    Department = dataRow["EmpDepartment"].ToString(),
                    Place = dataRow["EmpPlace"].ToString(),
                    DOB = dataRow["EmpDOB"].ToString()
                });
            }
            ObjEmployeeModel = FetchDepartmentList(ObjEmployeeModel);
            ObjEmployeeModel.SuccessRate = 0;
            return View(ObjEmployeeModel);
        }

        [HttpPost]
        public IActionResult Index(EmployeeModel ObjEmployeeModel)
        {
            var parameters = new DynamicParameters(
                new
                {
                    EventID = 1,
                    chvEmployeeName = ObjEmployeeModel.EmployeeName,
                    chvEmployeeMobNo = ObjEmployeeModel.MobileNum,
                    chvEmployeeEmail = ObjEmployeeModel.EmailID,
                    tnyEmployeeGender = ObjEmployeeModel.Gender,
                    chvEmployeeDepartment = "1",
                    chvEmployeePlace = ObjEmployeeModel.Place,
                    chvEmployeeDOB = ObjEmployeeModel.DOB,
                });
            sqlManager.Execute("[dbo].[spEmployeeMasterTransactions]", parameters);
            ObjEmployeeModel.SuccessRate = 1;
            ModelState.Clear();
            ObjEmployeeModel = FetchEmployeeData();
            return View(ObjEmployeeModel);
        }

        public IActionResult Update(int EmployeeID)
        {
            EmployeeModel ObjEmployeeModel = new EmployeeModel();
            ObjEmployeeModel = FetchEmployeeDataEdit();
            return View(ObjEmployeeModel);
        }

        [HttpPost]
        public IActionResult Update(EmployeeModel ObjEmployeeModel)
        {
            if (ModelState.IsValid)
            {
                var parameters = new DynamicParameters(
                    new
                    {
                        EventID = 2,
                        chvEmployeeName = ObjEmployeeModel.EmployeeName,
                        chvEmployeeMobNo = ObjEmployeeModel.MobileNum,
                        chvEmployeeEmail = ObjEmployeeModel.EmailID,
                        tnyEmployeeGender = "1",
                        chvEmployeeDepartment = "1",
                        chvEmployeePlace = ObjEmployeeModel.Place,
                        chvEmployeeDOB = ObjEmployeeModel.DOB,
                    });
                sqlManager.Execute("[dbo].[spEmployeeMasterTransactions]", parameters);
                ObjEmployeeModel.SuccessRate = 1;
                ModelState.Clear();
                ObjEmployeeModel = FetchEmployeeData();
            }
            return View(ObjEmployeeModel);
        }

        public EmployeeModel FetchEmployeeData()
        {
            EmployeeModel ObjEmployeeModel = new EmployeeModel();
            var param = new List<SqlParameter>
            {
                new SqlParameter
                {
                    ParameterName= "@EventID",
                    Value   = "1",
                    SqlDbType= SqlDbType.Int,
                }
            };
            var dt = sqlManager.GetTable("[dbo].[spFetchEmployee]", param);
            ObjEmployeeModel.EmployeeListView = new List<EmployeeList>();
            foreach (DataRow dataRow in dt.Rows)
            {
                ObjEmployeeModel.EmployeeListView.Add(new EmployeeList
                {
                    EmployeeID = Convert.ToInt32(dataRow["EmpID"]),
                    MobileNum = Convert.ToInt32(dataRow["EmpMobNo"]),
                    EmployeeName = dataRow["EmpName"].ToString(),
                    EmailID = dataRow["EmpEmail"].ToString(),
                    Gender = dataRow["EmpGender"].ToString(),
                    Department = dataRow["EmpDepartment"].ToString(),
                    Place = dataRow["EmpPlace"].ToString(),
                    DOB = dataRow["EmpDOB"].ToString()
                });
            }
            return ObjEmployeeModel;
        }

        public EmployeeModel FetchDepartmentList(EmployeeModel ObjEmployeeModel)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter
                {
                    ParameterName = "@EventID",
                    Value = "1",
                    SqlDbType=SqlDbType.Int,
                }
            };
            var dt = sqlManager.GetTable("[dbo].[spFetchDepartment]",param);
            ObjEmployeeModel.DepartmentListView = new List<DepartmentList>();
            foreach(DataRow dataRow in dt.Rows)
            {
                ObjEmployeeModel.DepartmentListView.Add(new DepartmentList
                {
                    DepartmentID = Convert.ToInt32(dataRow["DepartmentID"]),
                    DepartmentName = dataRow["DepartmentName"].ToString(),
                });
            }
            return ObjEmployeeModel;
        }

        public EmployeeModel FetchEmployeeDataEdit()
        {
            EmployeeModel ObjEmployeeModel = new EmployeeModel();
            var param = new List<SqlParameter>
            {
                new SqlParameter
                {
                    ParameterName = "@EventID",
                    Value = "2",
                    SqlDbType= SqlDbType.Int,
                }
            };
            var dt = sqlManager.GetTable("[dbo].[spFetchEmployee]", param);

            return FetchEmployeeData();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}