using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEDAPI.Models
{
    public class User
    {
        private int id;
        private string username;
        private string password;
        private string manhanvien;
        private string hoten;
        private bool isadmin;
        private int duyetthuoc;
        private int duyetxuat;

        public User(int id, string username, string password, string manhanvien, string hoten, bool isadmin, int duyetthuoc, int duyetxuat)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.manhanvien = manhanvien;
            this.hoten = hoten;
            this.isadmin = isadmin;
            this.duyetthuoc = duyetthuoc;
            this.duyetxuat = duyetxuat;
        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Manhanvien { get => manhanvien; set => manhanvien = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public bool Isadmin { get => isadmin; set => isadmin = value; }
        public int Duyetthuoc { get => duyetthuoc; set => duyetthuoc = value; }
        public int Duyetxuat { get => duyetxuat; set => duyetxuat = value; }
    }
}