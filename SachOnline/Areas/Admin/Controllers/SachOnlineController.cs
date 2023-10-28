using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.ComponentModel.Design;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.Caching;

namespace SachOnline.Areas.Admin.Controllers
{   
    public class SachOnlineController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext("Data Source=FISHDABEZT\\FISHDABEZT;Initial Catalog=SachOnline;Integrated Security=True");
   

        public ActionResult Index(int ? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(data.SACHes.ToList().OrderBy(n =>n.Masach).ToPagedList(iPageNum,iPageSize));
        }
        [HttpGet]
        public ActionResult Create() {
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.Tenchude), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SACH sach, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.Tenchude), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            if ( fFileUpload == null)
            {
                ViewBag.Thongbao = " Hãy chọn ảnh bìa";
                ViewBag.Tensach = f["sTenSach"];
                ViewBag.Mota = f["sMoTa"];
                ViewBag.Soluong = int.Parse(f["iSoLuong"]);
                ViewBag.Giaban = Decimal.Parse(f["mGiaBan"]);
                ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.Tenchude), "MaCD", "TenChuDe", int.Parse(f["MaCD"]));
                ViewBag.MaCD = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaCD", "TenNXB", int.Parse(f["MaNXB"]));
                return View();

            }
            else
            {
                if (ModelState.IsValid)
                {
                    var sFilename = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFilename);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sach.Tensach = f["sTenSach"];
                    sach.Mota = f["sMoTa"];
                    sach.Anhbia = sFilename;
                    sach.Ngaycapnhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                    sach.Soluongban = int.Parse(f["iSoLuong"]);
                    sach.Giaban = Decimal.Parse(f["mGiaBan"]);
                    sach.MaCD = int.Parse(f["MaCD"]);
                    sach.MaNXB = int.Parse(f["MaNXB"]);
                    data.SACHes.InsertOnSubmit(sach);
                    data.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }

        }
        public ActionResult Details(int id)
        {
            var sach =data.SACHes.SingleOrDefault(n=>n.Masach==id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id,FormCollection f)
        {
            var sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ctdh=data.CHITIETDATHANGs.Where(ct=>ct.Masach==id);
            if (ctdh.Count()>0) {
                ViewBag.ThongBao = "Sách này đang có trong bản Chi tiết đặt hàng <br/>" + "Nếu muốn xóa thì phải xóa hết mã sách này trong bảng Chi tiết đặt hàng";
                return View(sach);
            }
            var vietsach =data.VIETSACHes.Where(vs=>vs.Masach==id).ToList();
            if (vietsach != null)
            {
                data.VIETSACHes.DeleteAllOnSubmit(vietsach);
                data.SubmitChanges();   
            }
            data.SACHes.DeleteOnSubmit(sach);
            data.SubmitChanges();
            return RedirectToAction("Index");  
        }
        [HttpGet]
        public ActionResult Edit(int id )
        {
            var sach = data.SACHes.SingleOrDefault(n => n.Masach == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
                ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n=>n.Tenchude),"MaCD","TenChuDe",sach.MaCD);
                ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n=>n.TenNXB),"MaNXB","TenNXB",sach.MaNXB);
            return View(sach);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f,HttpPostedFileBase fFileUpload)
        {
            var sach = data.SACHes.SingleOrDefault(n => n.Masach == int.Parse(f["iMaSach"]));
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.Tenchude), "MaCD", "TenChuDe", sach.MaCD);
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    var sFileName= Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sach.Anhbia = sFileName;

                }
                sach.Tensach = f["sTenSach"];            
                sach.Mota = f["sMoTa"];            
                sach.Ngaycapnhat =Convert.ToDateTime( f["dNgayCapNhat"]);
                sach.Soluongban =int.Parse( f["iSoLuong"]);
                sach.Giaban =Decimal.Parse( f["mGiaBan"]);
                sach.MaCD =int.Parse( f["MaCD"]);
                sach.MaNXB =int.Parse( f["MaNXB"]);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(sach);
        }

    }
}