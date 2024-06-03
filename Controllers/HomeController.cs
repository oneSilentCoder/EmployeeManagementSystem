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
        SqlConnection Connection;
        SqlDataAdapter SqlDataAdapter;
        SqlCommand Command;
        string ConncetionString;

        public HomeController(IConfiguration configuration)
        {
            sqlManager = new SQLManager(configuration);
            ConncetionString = configuration["ConnectionStrings:MSSQLConnectionString"]; 
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
        public IActionResult Index(EmployeeModel ObjEmployeeModel, string submit)
        {
            if (submit != null)
            {
                if (submit == "save")
                {
                    var parameters = new DynamicParameters(
                                new
                                {
                                    EventID = 1,
                                    chvEmployeeName = ObjEmployeeModel.EmployeeName,
                                    chvEmployeeMobNo = ObjEmployeeModel.MobileNum,
                                    chvEmployeeEmail = ObjEmployeeModel.EmailID,
                                    tnyEmployeeGender = ObjEmployeeModel.Gender,
                                    chvEmployeeDepartment = ObjEmployeeModel.Department,
                                    chvEmployeePlace = ObjEmployeeModel.Place,
                                    chvEmployeeDOB = ObjEmployeeModel.DOB,
                                });
                    var obj = sqlManager.Execute("[dbo].[spEmployeeMasterTransactions]", parameters);
                    ObjEmployeeModel.SuccessRate = 1;
                    ModelState.Clear();
                    ObjEmployeeModel = FetchEmployeeData();
                    FetchDepartmentList(ObjEmployeeModel); 
                }
                else if(submit == "update")
                {
                    var parameters = new DynamicParameters(
                        new
                        {
                            EventId = 2,
                            chvEmployeeName = ObjEmployeeModel.EmployeeName,
                            chvEmployeeMobNo = ObjEmployeeModel.MobileNum,
                            chvEmployeeEmail = ObjEmployeeModel.EmailID,
                            tnyEmployeeGender = ObjEmployeeModel.Gender,
                            chvEmployeeDepartment = ObjEmployeeModel.Department,
                            chvEmployeePlace = ObjEmployeeModel.Place,
                            chvEmployeeDOB = ObjEmployeeModel.DOB,
                        });
                    using(Connection = new SqlConnection(ConncetionString))
                    {
                        try
                        {
                            Connection.Open();
                            var Obj = Connection.Execute("[dbo].[spEmployeeMasterTransactions]", parameters, commandType: CommandType.StoredProcedure);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            Connection.Close();
                        }
                    }
                }
            }
            return View(ObjEmployeeModel);
        }

        public JsonResult Update(EmployeeModel ObjEmployeeModel)
        {
            var parameters = new DynamicParameters(
               new
               {
                   EventID = 2,
                   EmployeeID = ObjEmployeeModel.EmployeeID
               });

            var param = new List<SqlParameter>
            {
                new SqlParameter
                {
                    ParameterName= "@EventID",
                    Value   = "2",
                    SqlDbType= SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName= "@EmployeeID",
                    Value   = ObjEmployeeModel.EmployeeID,
                    SqlDbType= SqlDbType.Int,
                }
            };
            var dt = sqlManager.GetTable("[dbo].[spFetchEmployee]", param);
            //ObjEmployeeModel = (EmployeeModel)sqlManager.Execute("[dbo].[spFetchEmployee]", parameters);
            foreach(DataRow Dtr in dt.Rows)
            {
                ObjEmployeeModel.EmployeeID = Convert.ToInt32(Dtr["EmpID"]);
                ObjEmployeeModel.EmployeeName = Convert.ToString(Dtr["EmpName"]);
                ObjEmployeeModel.MobileNum = Convert.ToInt32(Dtr["EmpMobNo"]);
                ObjEmployeeModel.EmailID = Dtr["EmpEmail"].ToString();
                ObjEmployeeModel.Gender = Dtr["EmpGender"].ToString();
                ObjEmployeeModel.Department = Dtr["EmpDepartment"].ToString();
                ObjEmployeeModel.Place = Dtr["EmpPlace"].ToString();
                ObjEmployeeModel.DOB = Dtr["EmpDOB"].ToString();
            }
            return Json(ObjEmployeeModel);
        }


        //public IActionResult Update(EmployeeModel ObjEmployeeModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var parameters = new DynamicParameters(
        //            new
        //            {
        //                EventID = 2,
        //                chvEmployeeName = ObjEmployeeModel.EmployeeName,
        //                chvEmployeeMobNo = ObjEmployeeModel.MobileNum,
        //                chvEmployeeEmail = ObjEmployeeModel.EmailID,
        //                tnyEmployeeGender = "1",
        //                chvEmployeeDepartment = "1",
        //                chvEmployeePlace = ObjEmployeeModel.Place,
        //                chvEmployeeDOB = ObjEmployeeModel.DOB,
        //            });
        //        sqlManager.Execute("[dbo].[spEmployeeMasterTransactions]", parameters);
        //        ObjEmployeeModel.SuccessRate = 1;
        //        ModelState.Clear();
        //        ObjEmployeeModel = FetchEmployeeData();
        //    }
        //    return View(ObjEmployeeModel);
        //}

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

        //public EmployeeModel FetchEmployeeDataEdit(int EmployeeID)
        //{
        //    EmployeeModel ObjEmployeeModel = new EmployeeModel();
        //    //var param = new List<SqlParameter>
        //    //{
        //    //    new SqlParameter
        //    //    {
        //    //        ParameterName = "@EventID",
        //    //        Value = "2",
        //    //        SqlDbType= SqlDbType.Int,
        //    //    },
        //    //     new SqlParameter
        //    //     {
        //    //         ParameterName = "@EmployeeID",
        //    //         Value = EmployeeID,
        //    //         SqlDbType = SqlDbType.Int,
        //    //     }
        //    //};

           
        //    return FetchEmployeeData(ObjEmployeeModel);
        //}

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