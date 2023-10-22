using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;

namespace SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
        // GET: SachOnline
        DataClasses1DataContext data = new DataClasses1DataContext("Data Source=FISHDABEZT\\FISHDABEZT;Initial Catalog=SachOnline;Integrated Security=True");
        public ActionResult Index()
        {
            var listSachMoi = LaySachMoi(6);
            return View(listSachMoi);

        }
        /// <summary>
        /// Getchude
        /// </summary>
        /// <returns>ReturnChuDe</returns>
        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd; 
            return PartialView(listChuDe);
        }
        public ActionResult NXBPartial()
        {
            var listNXB = from cd in data.NHAXUATBANs select cd;
            return PartialView(listNXB);
        }
        public ActionResult SachTheoChuDe(int id)
        {
            var sach = from s in data.SACHes where s.MaCD== id select s;
            return View(sach);
        }
        public ActionResult SachTheoNXB(int id)
        {
            var sach = from s in data.SACHes where s.MaNXB == id select s;
            return View(sach);
        }

        public ActionResult ChiTietSach (int id)
        {
            var sach = from s in data.SACHes
                       where s.Masach == id select s;
            return PartialView(sach.Single());
        }


        private List<SACH> LaySachMoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        private List<SACH> ListSachBannhieu(int count)
        {

            return data.SACHes.OrderByDescending(s => s.Soluongban).Take(count).ToList();
        }
      
      
        public ActionResult SachBanNhieuPartial()
        {
            var SachBanNhieu = ListSachBannhieu(6);
            return PartialView(SachBanNhieu);
        }
        
    }

}