using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn
{
    internal class ChuyenBay
    {
        private string maChuyenBay, soHieuMayBay, sanBayDen;
        private DateTime ngayKhoiHanh;
        private int trangThai;
        private LinkedList<Ve> danhSachVe;
        private LinkedList<int> danhSachGheTrong;

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

        public string SoHieuMayBay
        {
            get
            {
                return soHieuMayBay;
            }

            set
            {
                soHieuMayBay = value;
            }
        }

        public string SanBayDen
        {
            get
            {
                return sanBayDen;
            }

            set
            {
                sanBayDen = value;
            }
        }

        public DateTime NgayKhoiHanh
        {
            get
            {
                return ngayKhoiHanh;
            }

            set
            {
                ngayKhoiHanh = value;
            }
        }

        public int TrangThai
        {
            get
            {
                return trangThai;
            }

            set
            {
                trangThai = value;
            }
        }

        internal LinkedList<Ve> DanhSachVe
        {
            get
            {
                return danhSachVe;
            }

            set
            {
                danhSachVe = value;
            }
        }

        public LinkedList<int> DanhSachGheTrong
        {
            get
            {
                return danhSachGheTrong;
            }

            set
            {
                danhSachGheTrong = value;
            }
        }

        public ChuyenBay()
        {
            this.MaChuyenBay = "";
            this.SoHieuMayBay = "";
            this.SanBayDen = "";
            this.DanhSachVe = new LinkedList<Ve>();
            this.TrangThai = 1;
            this.DanhSachGheTrong = new LinkedList<int>();
            this.NgayKhoiHanh = DateTime.MinValue;
        }

        public ChuyenBay(string maChuyenBay, string sanBayDen, string soHieuMayBay, DateTime ngayKhoiHanh, int trangThai, LinkedList<Ve> danhSachVe, LinkedList<int> danhSachGheTrong)
        {
            this.MaChuyenBay = maChuyenBay;
            this.SoHieuMayBay = soHieuMayBay;
            this.SanBayDen = sanBayDen;
            this.NgayKhoiHanh = ngayKhoiHanh;
            if (danhSachGheTrong.First==null)
            {
                this.TrangThai = 2;
            }
            else
            {
                this.TrangThai = trangThai;
            }
            this.DanhSachVe = danhSachVe;
            this.DanhSachGheTrong = danhSachGheTrong;
        }

        //public string MaChuyenBay { get => maChuyenBay; set => maChuyenBay = value; }
        //public string SoHieuMayBay { get => soHieuMayBay; set => soHieuMayBay = value; }
        //public string SanBayDen { get => sanBayDen; set => sanBayDen = value; }
        //public DateTime NgayKhoiHanh { get => ngayKhoiHanh; set => ngayKhoiHanh = value; }
        //public int TrangThai { get => trangThai; set => trangThai = value; }
        //public LinkedList<int> DanhSachGheTrong { get => danhSachGheTrong; set => danhSachGheTrong = value; }
        //internal LinkedList<Ve> DanhSachVe { get => danhSachVe; set => danhSachVe = value; }
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
            string dsv = "[";
            for (LinkedListNode<Ve> i = this.DanhSachVe.First; i != null; i = i.Next)
            {
                if (i.Next != null)
                {
                    dsv += i.Value.MaVe + ",";
                }
                else
                {
                    dsv += i.Value.MaVe+ "]";
                }
            }
            //string dsgt = "[";
            //for (LinkedListNode<int> i = this.danhSachGheTrong.First; i != null; i = i.Next)
            //{
            //    if (i.Next!=null)
            //    {
            //        dsgt += i.Value + "-";
            //    }
            //    else
            //    {
            //        dsgt += i.Value+"]";
            //    }
            //}
            return $"{this.MaChuyenBay,-15}{this.SanBayDen,-15}{this.SoHieuMayBay,-15}{this.NgayKhoiHanh.ToShortDateString(), -15}{toStringTrangThai(),-15}{dsv,-30}";
        }
        public string toStringTrangThai()
        {
            if (this.TrangThai == 0) return "Huy chuyen";
            else if (this.TrangThai == 1) return "Con ve";
            else if (this.TrangThai == 2) return "Het ve";
            return null;
        }
        public string update()
        {
            string dsv = "";
            for (LinkedListNode<Ve> i = this.DanhSachVe.First; i != null; i = i.Next)
            {
                if (i.Next != null)
                {
                    dsv += i.Value.MaVe + "#";
                }
                else
                {
                    dsv += i.Value.MaVe;
                }
            }
            string dsgt = "";
            for (LinkedListNode<int> i = this.DanhSachGheTrong.First; i !=null; i=i.Next)
            {
                if (i.Next!=null)
                {
                    dsgt += $"{i.Value}" + "#";
                }
                else
                {
                    dsgt += $"{i.Value}";
                }
            }
            return $"{this.MaChuyenBay},{this.SanBayDen},{this.SoHieuMayBay},{this.NgayKhoiHanh.ToShortDateString()},{this.TrangThai},{dsv},{dsgt}";
        }
    }
}
