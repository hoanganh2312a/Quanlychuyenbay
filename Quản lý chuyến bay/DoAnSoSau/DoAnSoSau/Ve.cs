using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace DoAn
{
    internal class Ve
    {
        private string maVe,maChuyenBay;
        private KhachHang khachHang;
        private int soThuTuCuaGhe;

        public string MaVe
        {
            get
            {
                return maVe;
            }

            set
            {
                maVe = value;
            }
        }

        public string MaChuyenBay
        {
            get
            {
                return maChuyenBay;
            }

            set
            {
                maChuyenBay = value;
            }
        }

        internal KhachHang KhachHang
        {
            get
            {
                return khachHang;
            }

            set
            {
                khachHang = value;
            }
        }

        public int SoThuTuCuaGhe
        {
            get
            {
                return soThuTuCuaGhe;
            }

            set
            {
                soThuTuCuaGhe = value;
            }
        }



        //public string MaVe { get => maChuyenBay + "-" + soThuTuCuaGhe; set => maVe = value; }
        //public string MaChuyenBay { get => maChuyenBay; set => maChuyenBay = value; }
        //public int SoThuTuCuaGhe { get => soThuTuCuaGhe; set => soThuTuCuaGhe = value; }
        //internal KhachHang KhachHang { get => khachHang; set => khachHang = value; }



        public Ve()
        {
            this.MaVe = null;
            this.MaChuyenBay = null;
            this.KhachHang = new KhachHang();
            this.SoThuTuCuaGhe = 0;
        }
        public Ve(string maChuyenBay, KhachHang khachHang, int soThuTuCuaGhe)
        {
            this.MaVe = maChuyenBay+"-"+soThuTuCuaGhe;
            this.MaChuyenBay = maChuyenBay;
            this.KhachHang = khachHang;
            this.SoThuTuCuaGhe = soThuTuCuaGhe;
        }
        public void GhiFile()
        {
            try
            {
                using (StreamWriter SW = new StreamWriter(@"" +this.MaVe + ".txt", true))
                {                    
                            SW.WriteLine(update());
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Ghi file that bai!");
            }
        }
        public string update()
        {
            return MaVe + "," + MaChuyenBay + "," + KhachHang.CMND+","+ KhachHang.HoTen+","+ SoThuTuCuaGhe;
        }
    }
}
