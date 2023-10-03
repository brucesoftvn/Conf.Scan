using System;
using System.Collections.Generic;
using System.Text;

namespace MEDAZ.SCAN.Models
{
    public class ItemSelected
    {
        string maKH;
        string hoten;
        string diachi;
        string dienThoai;
        string timeCheck;
        string machineCheck;
        string iD;
        string chucVu;
        string donVi;
        string email;
        public string MaKH { get => maKH; set => maKH = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string DienThoai { get => dienThoai; set => dienThoai = value; }
        public string TimeCheck { get => timeCheck; set => timeCheck = value; }
        public string MachineCheck { get => machineCheck; set => machineCheck = value; }
        public string ID { get => iD; set => iD = value; }
        public string ChucVu { get => chucVu; set => chucVu = value; }
        public string DonVi { get => donVi; set => donVi = value; }
        public string Email { get => email; set => email = value; }
    }
}
