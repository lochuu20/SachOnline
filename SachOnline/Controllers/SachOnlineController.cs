using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;
using PagedList;
using PagedList.Mvc;
namespace SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
        // GET: SachOnline
        DataClasses1DataContext data = new DataClasses1DataContext("Data Source=FISHDABEZT\\FISHDABEZT;Initial Catalog=SachOnline;Integrated Security=True");
        public ActionResult Index(int? page)
        {
            int iSize = 6;
            int iPageNum = (page ?? 1);
            var listSachMoi = LaySachMoi(20);
            return View(listSachMoi.ToPagedList(iPageNum, iSize));

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
        //public ActionResult SachTheoChuDe(int id)
        //{
        //    var sach = from s in data.SACHes where s.MaCD == id select s;
        //    return View(sach);
        //}
        public ActionResult SachTheoChuDe(int iMaCD, int? page)
        {
            ViewBag.MaCD = iMaCD;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            //int iPageNum = 1;
            var sach = from s in data.SACHes where s.MaCD == iMaCD select s;
            return View(sach.ToPagedList(iPageNum, iSize));
        } public ActionResult SachTheoNXB(int iMaNXB, int? page)
        {
            ViewBag.MaNXB = iMaNXB;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            //int iPageNum = 1;
            var sach = from s in data.SACHes where s.MaCD == iMaNXB select s;
            return View(sach.ToPagedList(iPageNum, iSize));
        }

        //public ActionResult SachTheoNXB(int id)
        //{
        //    var sach = from s in data.SACHes where s.MaNXB == id select s;
        //    return View(sach);
        // }

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
        public ActionResult NavPartial()
        {
            
            return PartialView();
        }
        public ActionResult LoginLogout()
        {
            return PartialView();
        }
    }

}