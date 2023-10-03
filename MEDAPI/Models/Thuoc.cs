using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEDAPI.Models
{
    public class Thuoc
    {
        private string mavt;
        private long mavach;
        private string tenvt;
        private string dvt;
        private string solo;

        public Thuoc(string mavt, long mavach, string tenvt, string dvt, string solo)
        {
            this.mavt = mavt;
            this.mavach = mavach;
            this.tenvt = tenvt;
            this.dvt = dvt;
            this.solo = solo;
        }

        public string Mavt { get => mavt; set => mavt = value; }
        public long Mavach { get => mavach; set => mavach = value; }
        public string Tenvt { get => tenvt; set => tenvt = value; }
        public string Dvt { get => dvt; set => dvt = value; }
        public string Solo { get => solo; set => solo = value; }
    }
}