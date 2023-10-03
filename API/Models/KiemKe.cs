using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class KiemKe
    {
        private Int64 mavach;
        private string mavt;
        private float soluong;
        private int thang;
        private int nam;
        private string ghichu;
        private string khole;
        private string ngaytao;
        private string nguoitao;

        public KiemKe(long mavach, string mavt, float soluong, int thang, int nam, string ghichu, string khole, string ngaytao, string nguoitao)
        {
            this.mavach = mavach;
            this.mavt = mavt;
            this.soluong = soluong;
            this.thang = thang;
            this.nam = nam;
            this.ghichu = ghichu;
            this.khole = khole;
            this.ngaytao = ngaytao;
            this.nguoitao = nguoitao;
        }

        public long Mavach { get => mavach; set => mavach = value; }
        public string Mavt { get => mavt; set => mavt = value; }
        public float Soluong { get => soluong; set => soluong = value; }
        public int Thang { get => thang; set => thang = value; }
        public int Nam { get => nam; set => nam = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
        public string Khole { get => khole; set => khole = value; }
        public string Ngaytao { get => ngaytao; set => ngaytao = value; }
        public string Nguoitao { get => nguoitao; set => nguoitao = value; }
    }
}