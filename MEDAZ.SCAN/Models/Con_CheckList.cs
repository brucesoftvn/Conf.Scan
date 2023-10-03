using System;
using System.Collections.Generic;
using System.Text;

namespace MEDAZ.SCAN.Models
{
    public class Con_CheckList
    {
        private int iD;
        private string maKH;
        private string hoten;
        private string diachi;
        private string dienThoai;
        private string email;
        private string donVi;
        private string chucVu;
        private DateTime timeCheck;
        private string machineCheck;
        private string dob;

        public int ID { get => iD; set => iD = value; }
        public string MaKH { get => maKH; set => maKH = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string DienThoai { get => dienThoai; set => dienThoai = value; }
        public string Email { get => email; set => email = value; }
        public string DonVi { get => donVi; set => donVi = value; }
        public string ChucVu { get => chucVu; set => chucVu = value; }
        public DateTime TimeCheck { get => timeCheck; set => timeCheck = value; }
        public string MachineCheck { get => machineCheck; set => machineCheck = value; }
        public string Dob { get => dob; set => dob = value; }
    }
}
