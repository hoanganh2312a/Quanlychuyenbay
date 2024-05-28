using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn
{
    internal class MayBay
    {
        private string soHieuMayBay;
        private int soCho;

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

        public int SoCho
        {
            get
            {
                return soCho;
            }

            set
            {
                soCho = value;
            }
        }

        public MayBay()
        {
            this.SoHieuMayBay = "";
            this.SoCho = 0;
        }

        public MayBay(string soHieuMayBay, int soCho)
        {
            this.SoHieuMayBay = soHieuMayBay;
            this.SoCho = soCho;
        }

        //public string SoHieuMayBay { get => soHieuMayBay; set => soHieuMayBay = value; }
        //public int SoCho { get => soCho; set => soCho = value; }

        public string toString()
        {
            return $"\t\t\t\t\t\t{this.SoHieuMayBay,-20}{this.SoCho,-10}";
        }
    }
}
