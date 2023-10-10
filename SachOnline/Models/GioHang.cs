using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SachOnline.Models
{
    public class GioHang
    {
        DataClasses1DataContext db = new DataClasses1DataContext("Data Source=FISHDABEZT\\FISHDABEZT;Initial Catalog=SachOnline;Integrated Security=True");

        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien { get { return iSoLuong * dDonGia; } }

        //khởi tạo giỏ hàng theo masach duoc truyền vào với soluong mặc định
        public GioHang(int ms)
        {
            iMaSach = ms;
            SACH s = db.SACHes.Single(n => n.Masach == iMaSach);
            sTenSach = s.Tensach;
            sAnhBia = s.Anhbia;
            dDonGia = double.Parse(s.Giaban.ToString());
            iSoLuong = 1;
        }
    }
}