using System;
using System.Collections.Generic;
using System.Text;

namespace MEDAZ.SCAN.Models
{
    public class Khole
    {
        private string makho;
        private string tenkho;

        public Khole(string makho, string tenkho)
        {
            this.makho = makho;
            this.tenkho = tenkho;
        }

        public string Makho { get => makho; set => makho = value; }
        public string Tenkho { get => tenkho; set => tenkho = value; }
    }
}
