using System;
using System.Collections.Generic;
using System.Text;

namespace MEDAZ.SCAN.Models
{
    public class User
    {
        private string username;
        private string password;
        private string manhanvien;
        private string hoten;
        private string makho;
        public User(string username, string password, string manhanvien, string hoten, string makho)
        {
            this.username = username;
            this.password = password;
            this.manhanvien = manhanvien;
            this.hoten = hoten;
            this.makho = makho;
        }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Manhanvien { get => manhanvien; set => manhanvien = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public string Makho { get => makho; set => makho = value; }
    }
}
