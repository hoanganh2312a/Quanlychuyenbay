using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Xml.Linq;

namespace DoAn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int width = 200; // Chiều rộng mới của cửa sổ console
            //int height = 50; // Chiều cao mới của cửa sổ console

            //Console.SetWindowSize(width, height); // Thiết lập kích thước cửa sổ console
            bool isLoggedIn = Login(); // Hàm đăng nhập

            if (isLoggedIn)
            {
                ShowMainMenu(); // Hiển thị menu chính
            }
            else
            {
                Console.WriteLine("\t\t\t\t\t\tBan da nhap sai tai khoan hoac mat khau qua so lan quy dinh. Tam biet!");
            }
            
        }
        static bool Login()
        {
            int loginAttempts = 0;
            const int maxLoginAttempts = 3;

            while (loginAttempts < maxLoginAttempts)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t\t\t\t\t\t*********************************");
                Console.WriteLine("\t\t\t\t\t\t*      DANG NHAP HE THONG       *");
                Console.WriteLine("\t\t\t\t\t\t*********************************");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("\t\t\t\t\t\tNhap tai khoan: ");
                string username = Console.ReadLine();

                Console.Write("\t\t\t\t\t\tNhap mat khau: ");
                string password = GetHiddenPassword();

                // Kiểm tra thông tin đăng nhập trong file Admin.txt
                if (CheckCredentials(username, password))
                {
                    //Console.WriteLine("\t\t\t\t\tDang nhap thanh cong!");
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t\t\t\tTai khoan hoac mat khau khong chinh xac. Vui long thu lai.");
                    loginAttempts++;
                }
            }

            return false;
        }
        static bool CheckCredentials(string username, string password)
        {
            string[] credentials = File.ReadAllLines("Admin.txt");

            foreach (string credential in credentials)
            {
                string[] parts = credential.Split(',');
                if (parts[0] == username && parts[1] == password)
                {
                    return true;
                }
            }

            return false;
        }
        static string GetHiddenPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Remove(password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    
        static void ShowMainMenu()
        {
            LinkedList<ChuyenBay> chuyenBays = docChuyenBay("ChuyenBay.txt");
            xuatChuyenBay(chuyenBays);
            Queue<KhachHang> khachHangs = new Queue<KhachHang>();
            khachHangs = DocFileKhachHang("KhachHang.txt");
            XuatKhachHang(khachHangs);
            LinkedList<Ve> C = new LinkedList<Ve>();
            C = DocFileVeAll();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t\t*********************************");
                Console.WriteLine("\t\t\t\t\t\t*             MENU              *");
                Console.WriteLine("\t\t\t\t\t\t*********************************");
                Console.WriteLine("\t\t\t\t\t\t1. Hien thi danh sach chuyen bay");
                Console.WriteLine("\t\t\t\t\t\t2. Dat ve");
                Console.WriteLine("\t\t\t\t\t\t3. Thong ke");
                Console.WriteLine("\t\t\t\t\t\t4. Thoat");
                Console.WriteLine("\t\t\t\t\t\t--------------");
                Console.Write("\t\t\t\t\t\tVui long chon: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        xuatChuyenBay(chuyenBays);
                        break;
                    case "2":
                        QuaTrinhDatVe(chuyenBays, khachHangs, C);
                        break;
                    case "3":
                        ThongKe(chuyenBays);
                        break;
                    case "4":
                        Console.WriteLine("\t\t\t\t\t\tTam biet!");
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t\t\t\t\t\tLua chon khong hop le. Vui long nhap lai.");
                        break;
                }
                Console.ReadKey();
            }
        }



        static void ThongKe(LinkedList<ChuyenBay> chuyenBays)
        {

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t\t\t\t\t\t*********************************");
                Console.WriteLine("\t\t\t\t\t\t*        MENU THÔNGE KÊ         *");
                Console.WriteLine("\t\t\t\t\t\t*********************************");
                Console.WriteLine("\t\t\t\t\t\t1. Hien thi danh sach khach cua mot chuyen bay");
                Console.WriteLine("\t\t\t\t\t\t2. Hien thi danh sach ghe con trong cua 1 chuyen bay");
                Console.WriteLine("\t\t\t\t\t\t3. Thong ke so luong thuc hien chuyen bay cua mot may bay");
                Console.WriteLine("\t\t\t\t\t\t4. Quay lại");
                
                
                
                Console.Write("\t\t\t\t\t\tVui long chon: ");
                string maChuyenBay;
                LinkedListNode<ChuyenBay> CB;
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        xuatChuyenBay(chuyenBays);
                        Console.Write("\t\t\t\t\t\tNhap ma chuyen bay ban muon xem thong tin: ");
                        maChuyenBay = Console.ReadLine();
                        CB = TimChuyenBay(chuyenBays, maChuyenBay);
                        XuatDanhSachKhachHang(CB.Value.DanhSachVe);
                        break;
                    case "2":
                        xuatChuyenBay(chuyenBays);
                        Console.Write("\t\t\t\t\t\tNhap ma chuyen bay ban muon xem danh sach ghe trong: ");
                        maChuyenBay = Console.ReadLine();
                        CB = TimChuyenBay(chuyenBays, maChuyenBay);
                        XuatA(CB.Value.DanhSachGheTrong);
                        break;
                    case "3":
                        xuatChuyenBay(chuyenBays);
                        ThongKeSoLuongChuyenBay(chuyenBays);
                        break;
                    case "4":
                        Console.WriteLine("\t\t\t\t\t\tQuay lai menu quan ly.");
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t\t\t\t\t\tLua chon khong hop le. Vui long nhap lai.");
                        break;
                }
                Console.ReadKey();
            }
        }


        /// <summary>
        /// Ham xuat danh sach khach hang da ve cua 1 chuyen bay
        /// </summary>
        /// <param name="ves"></param>
        static void XuatDanhSachKhachHang(LinkedList<Ve> ves)
        {
            if (ves.Count == 0)
            {
                Console.WriteLine($"\t\t\t\t\t\tHien dang khong co khach hang da ve bay!");
            }
            else
            {
                Console.WriteLine("\t\t\t\t\t\tDanh sach khach hang");
                Console.WriteLine($"\t\t\t\t\t\t{"CMND",-15}{"HoTen",-15}");
                for (LinkedListNode<Ve> p = ves.First; p != null; p = p.Next)
                {
                    Console.WriteLine(p.Value.KhachHang.toString());
                }
            }
            
        }

        /// <summary>
        /// Ham thong ke so lan thuc hien cua mot may bay
        /// </summary>
        /// <param name="chuyenBays"></param>
        static void ThongKeSoLuongChuyenBay(LinkedList<ChuyenBay> chuyenBays)
        {
            Console.Write("\t\t\t\t\t\tNhap so hieu may bay: ");
            string soHieuMayBay = Console.ReadLine();

            int soLuongChuyenBay = 0;

            for (LinkedListNode<ChuyenBay> p = chuyenBays.First; p != null; p=p.Next)
            {
                if (p.Value.SoHieuMayBay == soHieuMayBay)
                {
                    soLuongChuyenBay++;
                }
            }
            Console.WriteLine($"\t\t\t\t\t\tSo luong chuyen bay cua may bay {soHieuMayBay}: {soLuongChuyenBay}");
        }

        /// <summary>
        /// Ham quan ly qua trinh dat ve
        /// </summary>
        /// <param name="chuyenBays"></param>
        /// <param name="khachHangs"></param>
        /// <param name="C"></param>
        static void QuaTrinhDatVe(LinkedList<ChuyenBay> chuyenBays, Queue<KhachHang> khachHangs, LinkedList<Ve> C)
        {
            int chonCN = 0;
            do
            {
                Console.Clear();
                Menu();
                Console.Write("\t\t\t\t\t\tBan chon chuc nang so: ");
                int.TryParse(Console.ReadLine(), out chonCN);
                switch (chonCN)
                {
                    case 1:
                        xuatChuyenBay(chuyenBays);
                        break;
                    case 2:
                        XuatKhachHang(khachHangs);
                        break;
                    case 3:
                        XuatVe(C);
                        break;
                    case 4:
                        LaySo(khachHangs);
                        break;
                    case 5:
                        xuatChuyenBay(chuyenBays);
                        
                        MuaVe(chuyenBays, khachHangs, C);
                        break;
                    case 6:
                        xuatChuyenBay(chuyenBays);
                        HuyVe(chuyenBays, C);
                        break;
                    case 7:
                        Console.WriteLine("\t\t\t\t\t\tQuay lai menu quan ly.");
                        return;
                    default:
                        Console.WriteLine("\t\t\t\t\t\tBan chon THOAT!!!");
                        break;
                }
                Console.ReadKey();
            } while (chonCN >= 1 && chonCN <= 6);
        }
        /// <summary>
        /// ham menu
        /// </summary>
        static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t\t\t*********************************");
            Console.WriteLine("\t\t\t\t\t\t*             MENU              *");
            Console.WriteLine("\t\t\t\t\t\t*********************************");
            Console.WriteLine("\t\t\t\t\t\t1. Hien thi danh sach chuyen bay");
            Console.WriteLine("\t\t\t\t\t\t2. Hien thi danh sach khach hang cho");
            Console.WriteLine("\t\t\t\t\t\t3. Hien thi danh sach ve");
            Console.WriteLine("\t\t\t\t\t\t4. Lay so.");
            Console.WriteLine("\t\t\t\t\t\t5. Mua ve.");
            Console.WriteLine("\t\t\t\t\t\t6. huy ve.");
            Console.WriteLine("\t\t\t\t\t\t7. Quay lai menu quan ly.");
            
        }

        /// <summary>
        /// Huy ve
        /// </summary>
        /// <param name="A"></param>
        /// <param name="C"></param>
        static void HuyVe(LinkedList<ChuyenBay> chuyenBays, LinkedList<Ve> C)
        {
            Console.WriteLine("\t\t\t\t\t\tNhap thong tin ve can huy: ");
            Console.Write("\t\t\t\t\t\tNhap ma chuyen bay: ");
            string maChuyenBay = Console.ReadLine();
            Console.Write("\t\t\t\t\t\tNhap ma ve: ");
            string maVe = Console.ReadLine();
            LinkedListNode<ChuyenBay> CB = TimChuyenBay(chuyenBays, maChuyenBay);

            LinkedListNode<Ve> p = TimVeTrongDanhSach(maVe, C);
            if (p == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t\t\t\t\t\tHuy ve that bai!");
            }
            else
            {
                C.Remove(p);
                File.Delete(@"" + p.Value.MaVe + ".txt");
                chuyenBays.Find(CB.Value).Value.DanhSachGheTrong.AddLast(p.Value.SoThuTuCuaGhe);
                chuyenBays.Find(CB.Value).Value.DanhSachVe.Remove(p.Value);
                File.WriteAllText("ChuyenBay.txt", string.Empty);
                ghiDanhSachChuyenBay(chuyenBays, "ChuyenBay.txt");
                Console.WriteLine("\t\t\t\t\t\tHuy ve thanh cong");
            }
        }
        /// <summary>
        /// Ham tim ve trong danh sach ve
        /// </summary>
        /// <param name="s"></param>
        /// <param name="L"></param>
        /// <returns></returns>
        static LinkedListNode<Ve> TimVeTrongDanhSach(string s, LinkedList<Ve> L)
        {
            for (LinkedListNode<Ve> i = L.First; i != null; i = i.Next)
            {
                if (i.Value.MaVe == s)
                {
                    return i;
                }
            }
            return null;
        }


        /// <summary>
        /// Tim ve trong danh sach lien ket ve
        /// </summary>
        /// <param name="C"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        static LinkedListNode<Ve> Find(LinkedList<Ve> C, Ve v)
        {
            for (LinkedListNode<Ve> p = C.First; p != null; p = p.Next)
            {
                if (p.Value.MaVe == v.MaVe && p.Value.SoThuTuCuaGhe == v.SoThuTuCuaGhe)
                {
                    return p;
                }
            }
            return null;
        }
        /// <summary>
        /// Chuc nang mua ve
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        static void MuaVe(LinkedList<ChuyenBay> chuyenBays, Queue<KhachHang> B, LinkedList<Ve> C)
        {
            Console.Write("\t\t\t\t\t\tNhap ma chuyen bay ban muon di: ");
            string maChuyenBay = Console.ReadLine();
            LinkedListNode<ChuyenBay> CB = TimChuyenBay(chuyenBays, maChuyenBay);
            XuatA(CB.Value.DanhSachGheTrong);
            if (CB.Value.TrangThai == 0)
            {
                Console.WriteLine("\t\t\t\t\t\tChuyen bay da bi huy!");
            }
            else if(CB.Value.TrangThai == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t\t\t\t\t\tChuyen bay da het cho trong!");
            }
            else if(CB.Value.TrangThai == 1)
            {
                if (chuyenBays.Find(CB.Value).Value.DanhSachGheTrong.Count > 0 && B.Count > 0)
                {
                    Console.WriteLine("\t\t\t\t\t\tNhap thong tin mua ve: ");

                    int soGhe = 0;
                    do
                    {
                        Console.Write("\t\t\t\t\t\tNhap so ghe: ");
                        int.TryParse(Console.ReadLine(), out soGhe);
                    } while (chuyenBays.Find(CB.Value).Value.DanhSachGheTrong.Find(soGhe) == null);
                    Ve v = new Ve(maChuyenBay, B.Last(), soGhe);
                    C.AddLast(v);
                    v.GhiFile();
                    chuyenBays.Find(CB.Value).Value.DanhSachVe.AddLast(v);
                    chuyenBays.Find(CB.Value).Value.DanhSachGheTrong.Remove(soGhe);
                    File.WriteAllText("ChuyenBay.txt", string.Empty);
                    ghiDanhSachChuyenBay(chuyenBays, "ChuyenBay.txt");
                    B.Dequeue();
                    File.WriteAllText("KhachHang.txt", string.Empty);
                    GhiFileKhachHang(B, "KhachHang.txt");
                    Console.WriteLine("\t\t\t\t\t\tMua ve thanh cong!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t\t\t\tMua ve that bai!");
                }
            }
        }


        /// <summary>
        /// Ham Tim chuyen bay
        /// </summary>
        /// <param name="chuyenBays"></param>
        /// <param name="maChuyenBay"></param>
        /// <returns></returns>
        static LinkedListNode<ChuyenBay> TimChuyenBay(LinkedList<ChuyenBay> chuyenBays, string maChuyenBay)
        {
            for (LinkedListNode<ChuyenBay> p = chuyenBays.First; p != null; p = p.Next)
            {
                if (p.Value.MaChuyenBay == maChuyenBay)
                {
                    return p;
                }
            }
            return null;
        }
        /// <summary>
        /// Xuat hang doi B
        /// </summary>
        /// <param name="B"></param>
        static void XuatB(Queue<KhachHang> B)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if (B.Count == 0)
            {
                Console.WriteLine("\t\t\t\t\t\t\t\t\t\tKhong co nguoi cho mua ve!");
                return;
            }
            else
            {
                Console.WriteLine("Danh sach so dang cho mua ve: ");
                foreach (var item in B)
                {
                    Console.Write(item);
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Nhap hang doi B
        /// </summary>
        /// <param name="B"></param>
        static void LaySo(Queue<KhachHang> B)
        {
            KhachHang khachHang;
            Console.WriteLine("\t\t\t\t\t\tNhap thong tin khach hang:");
            if (B.Count == 0)
            {
                Console.Write("\t\t\t\t\t\tNhap chung minh nhan dan: ");
                string cMND = Console.ReadLine();
                Console.Write("\t\t\t\t\t\tNhap ho ten: ");
                string hoTen = Console.ReadLine();
                Console.WriteLine();
                khachHang = new KhachHang(1, cMND, hoTen);
                B.Enqueue(khachHang);
            }
            else
            {
                Console.Write("\t\t\t\t\t\tNhap chung minh nhan dan: ");
                string cMND = Console.ReadLine();
                Console.Write("\t\t\t\t\t\tNhap ho ten: ");
                string hoTen = Console.ReadLine();
                Console.WriteLine();
                khachHang = new KhachHang(B.Last().SoThuTu + 1, cMND, hoTen);
                B.Enqueue(khachHang);
            }
            khachHang.GhiFile("KhachHang.txt");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t\t\t\t\t\tLay so thanh cong!");
        }

        static void XuatA(LinkedList<int> A)
        {
            if (A.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t\t\t\t\t\t\t\t\t\tMay bay het ghe!");
                return;
            }
            Console.WriteLine("\t\t\t\t\t\tDanh sach ghe trong co trong May bay: ");
            Console.Write("\t\t\t\t\t\t");
            for (LinkedListNode<int> p = A.First; p != null; p = p.Next)
            {
                Console.Write(p.Value + " ");
            }
            Console.WriteLine();
        }



        static void xuatMayBay(LinkedList<MayBay> L)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\t\t\t\t\t\t{"SoHieuMayBay",-20}{"SoCho",-10}");
            for (LinkedListNode<MayBay> p = L.First; p != null; p = p.Next)
            {
                Console.WriteLine(p.Value.toString());
            }
        }
        static LinkedList<MayBay> docMayBay(string path)
        {
            LinkedList<MayBay> L = new LinkedList<MayBay>();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] t = sr.ReadLine().Split(',');
                        MayBay vd = new MayBay(t[0], int.Parse(t[1]));
                        L.AddLast(vd);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return L;
        }



        static void ghiDanhSachChuyenBay(LinkedList<ChuyenBay> L, string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    for (LinkedListNode<ChuyenBay> p = L.First; p != null; p = p.Next)
                    {
                        sw.WriteLine(p.Value.update());
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Loi ghi danh sach chuyen bay!");
            }
        }

        static LinkedList<ChuyenBay> docChuyenBay(string path)
        {
            LinkedList<ChuyenBay> L = new LinkedList<ChuyenBay>();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] t = sr.ReadLine().Split(',');
                        string[] s = t[5].Split('#');
                        LinkedList<Ve> ves = new LinkedList<Ve>();
                        ves = DocFileVe(s);
                        string[] n = t[6].Split('#');
                        LinkedList<int> N = new LinkedList<int>();
                        for (int i = 0; i < n.Length; i++)
                        {
                            N.AddLast(int.Parse(n[i]));
                        }
                        ChuyenBay vd = new ChuyenBay(t[0], t[1], t[2], DateTime.Parse(t[3]), int.Parse(t[4]), ves, N);
                        L.AddLast(vd);
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Khong doc duoc danh sach chuyen bay!");
            }
            return L;
        }

        static void xuatChuyenBay(LinkedList<ChuyenBay> L)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t\t\t\t\t\tDanh sach chuyen bay: ");
            Console.WriteLine($"{"maChuyenBay",-15}{"sanBayDen",-15}{"soHieuMayBay",-15}{"ngayKhoiHanh",-15}{"Trang thai",-15}{"danhSachVe",-30}");
            for (LinkedListNode<ChuyenBay> p = L.First; p != null; p = p.Next)
            {
                Console.WriteLine(p.Value.toString());
            }
        }



        /// <summary>
        /// Ham ghi file ve
        /// </summary>
        /// <param name="ves"></param>
        static void XuatVe(LinkedList<Ve> ves)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t\t\t\t\t\tDanh sach ve bay");
            Console.WriteLine($"\t\t\t{"MaVe",-15}{"MaChuyenBay",-15}{"CMND",-15}{"HoTen",-25}{"SoGheNgoi",-15}");
            for (LinkedListNode<Ve> i = ves.First; i != null; i = i.Next)
            {
                Console.WriteLine($"\t\t\t{i.Value.MaVe,-15}{i.Value.MaChuyenBay,-15}{i.Value.KhachHang.CMND,-15}{i.Value.KhachHang.HoTen,-25}{i.Value.SoThuTuCuaGhe,-15}");
            }
        }
        /// <summary>
        /// Ham doc file ve cua 1 chuyen bay
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static LinkedList<Ve> DocFileVe(string[] path)
        {
            LinkedList<Ve> ves = new LinkedList<Ve>();
            for (int i = 0; i < path.Length; i++)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(@"" + path[i] + ".txt", true))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] t = sr.ReadLine().Split(',');
                            Ve v = new Ve(t[1], new KhachHang(t[2], t[3]), int.Parse(t[4]));
                            ves.AddLast(v);
                        }
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("Loi ghi file ve!");
                }
            }
            return ves;
        }
        static Ve DocFileVe2(string path)
        {
            Ve v = new Ve();

                try
                {
                    using (StreamReader sr = new StreamReader(@"" + path + ".txt", true))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] t = sr.ReadLine().Split(',');
                            v = new Ve(t[1], new KhachHang(t[2], t[3]), int.Parse(t[4]));
                        }
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("Loi ghi file ve!");
                }
            return v;
        }
        /// <summary>
        /// Ham doc file tat ca cac ve bay
        /// </summary>
        /// <param name="ves"></param>
        /// <returns></returns>
        static LinkedList<Ve> DocFileVeAll()
        {          
            LinkedList<Ve> ves = new LinkedList<Ve>();
            try
            {
                using (StreamReader sr = new StreamReader("ChuyenBay.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] t = sr.ReadLine().Split(',');
                        string[] s = t[5].Split('#');
                        for (int i = 0; i < s.Length; i++)
                        {
                            ves.AddLast(DocFileVe2(s[i]));
                        }
                        
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Loi ghi file ve!");
            }
            return ves;
        }



        /// <summary>
        /// Ham xuat danh sach khach hang cho
        /// </summary>
        /// <param name="khachHangs"></param>
        static void XuatKhachHang(Queue<KhachHang> khachHangs)
        {
            if (khachHangs.Count == 0)
            {
                Console.WriteLine("Hien dang khong co khach hang cho!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\t\t\t\t\t\tDanh sach khach hang");
                Console.WriteLine($"\t\t\t\t\t\t{"SoThuTu",-10}{"CMND",-15}{"HoTen",-15}");
                foreach (var item in khachHangs)
                {
                    Console.WriteLine($"\t\t\t\t\t\t{item.SoThuTu,-10}{item.CMND,-15}{item.HoTen,-15}");
                }
            }

        }
        /// <summary>
        /// Ham doc file khach hang
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static Queue<KhachHang> DocFileKhachHang(string path)
        {
            Queue<KhachHang> khachHangs = new Queue<KhachHang>();
            try
            {
                using (StreamReader SR = new StreamReader(path))
                {
                    while (!SR.EndOfStream)
                    {
                        string[] t = SR.ReadLine().Split(',');
                        KhachHang kh = new KhachHang(int.Parse(t[0]), t[1], t[2]);
                        khachHangs.Enqueue(kh);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Danh sach khach hang cho dang trong!");
            }
            return khachHangs;
        }
        /// <summary>
        /// Ham ghi file khach hang
        /// </summary>
        /// <param name="khachHangs"></param>
        /// <param name="path"></param>
        static void GhiFileKhachHang(Queue<KhachHang> khachHangs, string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (var item in khachHangs)
                    {
                        sw.WriteLine(item.update());
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Ghi danh sach khach hang!");
            }
        }
    }
}
