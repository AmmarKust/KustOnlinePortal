using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Application.Models;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;



namespace Application.Controllers
{

   

    [Authorize]
   
    

    public class AdminController : Controller
    {


        SqlConnection con = new SqlConnection("Data Source=DESKTOP-2PLGR6E;Initial Catalog=KustOnlinePortal;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
        OleDbConnection Econ;

        // GET: Admin
        public ActionResult Dashboard()
        {
            var AdminData = this.Session["UserData"];
            return View();
        }
        
        [HttpGet]
        public ActionResult AddUsers()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUsers(Authentication user)
        {

           using (var context = new KustOnlinePortalEntities())
            {
                context.Authentications.Add(user);
               context.SaveChanges();
            }

            return RedirectToAction("ViewUsers");
        }

        private KustOnlinePortalEntities db = new KustOnlinePortalEntities();

        public ActionResult ViewUsers()
        {
            List<Authentication> users = db.Authentications.ToList<Authentication>();
            return View(users);
        }
        
        [HttpGet]
        public ActionResult EditUser(int id)
        {

            using(KustOnlinePortalEntities db=new KustOnlinePortalEntities())
            {
                return View(db.Authentications.Where(x=>x.ID==id).FirstOrDefault());
            }
            
        }
        [HttpPost]
        public ActionResult EditUser(int id, Authentication auth)
        {
            try
            {
                using(KustOnlinePortalEntities db=new KustOnlinePortalEntities())
                {
                    db.Entry(auth).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("ViewUsers");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteUser(int id)
        {
            using (KustOnlinePortalEntities db=new KustOnlinePortalEntities())
            {
                return View(db.Authentications.Where(x => x.ID == id).FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(int id,Authentication auth)
        {
            try
            {
                    using(KustOnlinePortalEntities db=new KustOnlinePortalEntities())
                {
                    Authentication authentication = db.Authentications.Where(x => x.ID == id).FirstOrDefault();
                    db.Authentications.Remove(authentication);
                    db.SaveChanges();
                }
                return RedirectToAction("ViewUsers");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult UserFromFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserFromFile(HttpPostedFileBase file)
        {

            /*var usersList = new List<Authentication>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator <= noOfRow-1; rowIterator++)
                        {
                            var user = new Authentication();
                            user.ID = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            user.RegistrationNo = workSheet.Cells[rowIterator, 2].Value.ToString();
                            user.Password = workSheet.Cells[rowIterator, 3].Value.ToString();
                            user.level = workSheet.Cells[rowIterator, 4].Value.ToString();
                            usersList.Add(user);
                        }
                    }
                }
            }
            using (KustOnlinePortalEntities excelImportDBEntities = new KustOnlinePortalEntities())
            {
                foreach (var item in usersList)
                {
                    excelImportDBEntities.Authentications.Add(item);
                }
                excelImportDBEntities.SaveChanges();
            }
            return RedirectToAction("ViewUsers");*/





             string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
             string filepath = "/excelfolder/" + filename;
             file.SaveAs(Path.Combine(Server.MapPath("/excelfolder"), filename));
             InsertExceldata(filepath, filename);

             return View("ViewUsers");

        }
     

        private void ExcelConnection(string filepath)
        {
            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", filepath);
            Econ = new OleDbConnection(constr);
        }
        private void InsertExceldata(string filepath,string filename)
        {
            string fullpath = Server.MapPath("/excelfolder/") + filename;
            ExcelConnection(fullpath);
            string query = string.Format("Select * from [{0}]", "Sheet1$");
            OleDbCommand Ecom = new OleDbCommand(query, Econ);
            Econ.Open();

            DataSet ds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);
            Econ.Close();
            oda.Fill(ds);

            DataTable dt = ds.Tables[0];

            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            objbulk.DestinationTableName = "Authentication";
            objbulk.ColumnMappings.Add("ID", "ID");
            objbulk.ColumnMappings.Add("RegistrationNo", "RegistrationNo");
            objbulk.ColumnMappings.Add("Password", "Password");
            objbulk.ColumnMappings.Add("level", "level");
            con.Open();
            objbulk.WriteToServer(dt);
            con.Close();
        }


       
    }
}