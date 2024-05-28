using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn
{
    internal class KhachHang
    {
        private int soThuTu;
        private string cMND, hoTen;

        public int SoThuTu
        {
            get
            {
                return soThuTu;
            }

            set
            {
                soThuTu = value;
            }
        }

        public string CMND
        {
            get
            {
                return cMND;
            }

            set
            {
                cMND = value;
            }
        }

        public string HoTen
        {
            get
            {
                return hoTen;
            }

            set
            {
                hoTen = value;
            }
        }

        //public int SoThuTu { get => soThuTu; set => soThuTu = value; }
        //public string CMND { get => cMND; set => cMND = value; }
        //public string HoTen { get => hoTen; set => hoTen = value; }

        public KhachHang()
        {
            this.SoThuTu = 0;
            this.CMND = null;
            this.HoTen = null;
        }

        public KhachHang(string cMND, string hoTen)
        {
            this.CMND = cMND;
            this.HoTen = hoTen;
        }
        public KhachHang(int soThuTu, string cMND, string hoTen)
        {
            this.SoThuTu = soThuTu;
            this.CMND = cMND;
            this.HoTen = hoTen;
        }
        public void GhiFile(string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(update());
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        public string toString()
        {
            return $"\t\t\t\t\t\t{CMND,-15}{HoTen,-15}";
        }
        public string update()
        {
            return SoThuTu + "," + CMND + "," + HoTen;
        }
    }
}
