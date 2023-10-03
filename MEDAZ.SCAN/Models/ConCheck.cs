using System;
using System.Collections.Generic;
using System.Text;

namespace MEDAZ.SCAN.Models
{
    public class ConCheck
    {
        private int iD;
        private string maKH;
        private DateTime timeCheck;
        private string machineCheck;
        private string hoten;
        private string dienthoai;
        private string diachi;
        private string dob;
        public int ID { get => iD; set => iD = value; }
        public string MaKH { get => maKH; set => maKH = value; }
        public DateTime TimeCheck { get => timeCheck; set => timeCheck = value; }
        public string MachineCheck { get => machineCheck; set => machineCheck = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public string Dienthoai { get => dienthoai; set => dienthoai = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string Dob { get => dob; set => dob = value; }
    }
}
