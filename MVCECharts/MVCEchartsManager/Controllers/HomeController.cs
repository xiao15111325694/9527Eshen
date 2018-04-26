using System.IO;
using System.Linq;
using System.Web.Mvc;
using Repository_基础结构层.Models;
using MVCECharts.Models;
using System.Data.Entity;
using System.Linq.Dynamic;
using System.Data;
using System.Web;
using ExcelOperate;
using System;
using MVCEchartsManager.DataServer;
using Repository_基础结构层;
using IOC.Server;

namespace MVCECharts.Controllers
{
    public class HomeController : Controller
    {

        HomeDataServer server = new HomeDataServer();
        private readonly ExportServer _exportServer;
        OpenSPDBContext db = new OpenSPDBContext();

        protected readonly IRepositoryBase<ShopingInfo> _iRepositoryBase;
        protected readonly IShopingServer _iShopingServer;
        public HomeController(  IRepositoryBase<ShopingInfo> iRepositoryBase,
            IShopingServer iShopingServer)
        {
            _iRepositoryBase = iRepositoryBase;
            _iShopingServer = iShopingServer;

        }

        public HomeController()
        {

        }


        // GET: Home
        public ActionResult Index()
        {
            //方案一： 查询导航属性的数据 性能差,速度慢 
            ////var testData2 = db.ShopingInfo.Take(10).ToList();
            //foreach (var item in testData2)
            //{
            //    var a = item.ShopingType.ShopingName;
            //}

            //方案二：查询导航属性的数据, 性能好, 速度快,优先推荐
            //var testData1 = db.ShopingInfo.Take(10).Include(x => x.ShopingType).ToList();
            //foreach (var item in testData1)
            //{
            //    var a = item.ShopingType.ShopingName;
            //}

            //不推荐
            //var shopingName1 = db.ShopingInfo.Where(x => x.ShopingName.Contains("手机")).ToList();
            //推荐 性能好 速度快
            //var shopingName2 = db.ShopingInfo.Where(x => x.ShopingName.Contains("手机")).AsNoTracking().ToList();

            //var shopingName2 = db.ShopingInfo.Where(x => x.ShopingName.Contains("手机")).AsNoTracking()
            //    .OrderBy("ShopingName desc,ID asc").ToList();

            // 不管是直接属性还是导航属性都采用Any
            //var test1 = db.ShopingInfo.Where(x => x.ShopingName == "手机").Count() > 0;
            //var test12 = db.ShopingInfo.Where(x => x.ShopingName == "手机").FirstOrDefault();!= null;
            //var test3 = db.ShopingInfo.Any(x => x.ShopingName == "手机");

            var list = _iShopingServer.GetAll().ToList();
            var ss = _iRepositoryBase.FindAll().ToList();
            ViewBag.Data = server.GetInfoViewModels();
            ViewBag.Type = server.GetAllShopingType();

            return View();
        }

        public ActionResult VueView()
        {
            return View();
        }

        public ActionResult ChartsView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ShopingInfo info)
        {
            var date = server.SelectShopingInfoIsRepetition(info);
            db.ShopingInfo.Add(info);
            db.SaveChanges();
            ViewBag.Type = server.GetAllShopingType();
            ViewBag.Data = server.GetInfoViewModels();
            return View();
        }


        public ActionResult ShowShopingCharts()
        {
            SumShopingSum ss = new SumShopingSum();
            ss.Name = db.ShopingInfo.Select(x => x.ShopingName).ToList();
            ss.Count = db.ShopingInfo.Select(x => x.ShopingCount).ToList();
            ss.ShopingStock = db.ShopingInfo.Select(x => x.Stock).ToList();
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(ss);
            return Json(data);
        }

        public ActionResult SelectShopingInfoByName(string name)
        {
            //var result = db.ShopingInfo.Where(x => x.ShopingName == name).ToList();
            var testResult = db.ShopingInfo.Where(e => e.ShopingName == DbFunctions.AsNonUnicode(name)).ToList();
            return Json(testResult);
        }

        public void DeleteShopingInfo(int id)
        {
            server.DeleteShopingInfo(id);
        }

        public FileResult ExportShopingInfo()
        {
            var reslut = (from sp in server.GetAllShopingInfo()
                          join st in server.GetAllShopingType() on sp.ShopingTypeId equals st.ID into temp
                          from st in temp.DefaultIfEmpty()
                          select new ShopingInfoViewModel
                          {
                              ShopingName = sp.ShopingName,
                              ShopingCount = sp.ShopingCount,
                              ShopingPric = sp.ShopingPric,
                              Stock = sp.Stock,
                              Volumeofvolume = sp.Volumeofvolume,
                              ShopingTypeName = st.ShopingName
                          }).ToList();
            ExportServer exportServer = new ExportServer();
            var result = exportServer.Export("商品信息报表", reslut);

            MemoryStream ms = new MemoryStream();
            result.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "商品信息.xls");

        }

        public FileResult ExportPDF()
        {
            string htmlContext = System.IO.File.ReadAllText(Server.MapPath("~/HTMLTemplate/ShopingBuy.html"));
            var context= _iShopingServer.Placeholderfill(htmlContext);
            PDFHelp pf = new PDFHelp();
            var ms= pf.ConvertHtmlTextToPDF(context);
            return File(ms,"application/pdf", "shoping"+ DateTime.Now+ ".pdf");
        }

        public ActionResult UploadView()
        {
            return View();
        } 

        public string Upload(HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                return "文件为空";
            }
            //try
            //{

                //将硬盘路径转化为服务器路径的文件流
                string fileName = Path.Combine(Request.MapPath("~/SaveFile"), Path.GetFileName(fileUpload.FileName));
                //NPOI得到EXCEL的第一种方法              
                //fileUpload.SaveAs(fileName);
                var fileNames = @"C:\Users\Administrator\Desktop\前端特效文件\商品信息.xls";
                DataTable dtData = ExcelHelper.Import(fileNames);
                var List = ExcelHelper.DataTableToList<ShopingInfoViewModel>(dtData);
                //得到EXCEL的第二种方法(第一个参数是文件流,第二个是excel标签名,第三个是第几行开始读0算第一行)
                DataTable dtData2 = ExcelHelper.RenderDataTableFromExcel(fileNames, "Sheet", 0);
                return "导入成功";
            //}
            //catch
            //{
            //    return "导入失败";
            //}
        }

    }
}