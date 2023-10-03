using System;
using System.Collections.Generic;
using System.Text;

namespace MEDAZ.SCAN.Models
{
    public class Com_Data
    {
        int iD;
        string maKH;
        string hoten;
        string diachi;
        string dienThoai;
        string email;
        string donVi;
        string chucVu;
        string dob;

        public int ID { get => iD; set => iD = value; }
        public string MaKH { get => maKH; set => maKH = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string DienThoai { get => dienThoai; set => dienThoai = value; }
        public string Email { get => email; set => email = value; }
        public string DonVi { get => donVi; set => donVi = value; }
        public string ChucVu { get => chucVu; set => chucVu = value; }
        public string Dob { get => dob; set => dob = value; }
    }
}
