
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace CMS_Core.Common
{
    public class Common
    {
        #region Properties
        [Obsolete]
        private static string ConnPOS = ConfigurationSettings.AppSettings["POS.ConnectionString"];

        private static string Key = "tomec@@)@!";
        /// <summary>
        /// 
        /// </summary>
        public static string GetConnPOS()
        { 
            return ConnPOS;
        }

        #endregion Properties

        #region private
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string getKey()
        {
            return Key;
        }

        // <summary>
        /// format datetime
        /// </summary>
        /// <param name="obj">string datatime</param>
        /// <returns></returns>
        public static string ReFmtTime(object obj)
        {
            try
            {
                string ss = obj.ToString();

                if (DateTime.Parse(ss) < DateTime.Now.AddYears(-100))
                    return "";
                else return DateTime.Parse(ss).ToString("dd/MM/yyyy HH:mm");
            }
            catch
            {
                return "";
            }
        }

        public static string FormatTitleNews(string title, int count)
        {
            string Chuoi = "";
            int icontent = title.Length;
            if (icontent < count || icontent == count)
            {
                Chuoi = title;
            }
            else
            {
                for (int i = 1; i < icontent; i++)
                {
                    string substring = title;
                    substring = substring.Substring(count - i, 1);
                    if (substring.IndexOf(" ") > 0 || substring == "" || substring == " ")
                    {
                        substring = title.Substring(0, count - i);
                        //substring = substring.Substring(0, substring.Length - 1);
                        Chuoi = substring + "...";
                        break;
                    }
                }
            }
            return Chuoi;
        }
        #endregion

        public static string GetURLHome(string aliasCate)
        {
            try
            {

                return aliasCate;

            }
            catch
            {
                return aliasCate;
            }
        }
        public static string GetURLDetailByCate(string aliasCate, string title, string cateid)
        {
            try
            {

                return aliasCate + "/" + getNiceUrl(title) + "-" + cateid;

            }
            catch
            {
                return aliasCate + "/" + getNiceUrl(title) + "-" + cateid;
            }
        }
        public static string GetURLDetailByNews(string aliasCate, string title, string cateid, string newsid)
        {
            try
            {

                return aliasCate + "/" + getNiceUrl(title) + "-" + cateid + "-" + newsid;

            }
            catch
            {
                return aliasCate + "/" + getNiceUrl(title) + "-" + cateid + "-" + newsid;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aliasCate"></param>
        /// <param name="title"></param>
        /// <param name="newsid"></param>
        /// <returns></returns>
        public static string GetURLDetailByDauThau(string aliasCate, string title, string newsid)
        {
            try
            {
                //return aliasCate + "/" + getNiceUrl(title) + "/" + newsid;
                return aliasCate + "/"  + newsid;

            }
            catch
            {
                return aliasCate + "/" + getNiceUrl(title)  + "-" + newsid;
            }
        }

        public static string GetURLDetailByDatLichOnline(string aliasCate, string title, string OrganizeID)
        {
            try
            {
                return aliasCate + "/" + OrganizeID;
            }
            catch
            {
                return aliasCate + "/" + getNiceUrl(title) + "-" + OrganizeID;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objurl"></param>
        /// <returns></returns>
        public static string getNiceUrl(Object objurl)
        {
            try
            {
                String url = objurl.ToString();
                String niceurl = ConvertEN(url);
                niceurl = niceurl.Replace("-", "");
                niceurl = niceurl.Replace(" ", "-");
                niceurl = niceurl.Replace("_", "-");
                niceurl = niceurl.Replace("nbsp;", "-");
                niceurl = niceurl.Replace("'", "");

                niceurl = removeChar(niceurl, new String[] { "/", "m²", ":", ",", "<", ">", "”", "“", ".", "!", "?", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+", "~", "`", "'", "'", "\"" });
                return niceurl;
            }
            catch
            {
                return "";
            }
        }

        public static string getNiceUrl_TV(Object objurl)
        {
            try
            {
                String url = objurl.ToString();
                String niceurl = url;
                niceurl = niceurl.Replace("-", "");
                niceurl = niceurl.Replace(" ", "-");
                niceurl = niceurl.Replace("_", "-");
                niceurl = niceurl.Replace("nbsp;", "-");
                niceurl = niceurl.Replace("'", "");


                niceurl = removeChar(niceurl, new String[] { "/", "m²", ":", ",", "<", ">", "”", "“", ".", "!", "?", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+", "~", "`", "'", "'", "\"" });
                return niceurl;
            }
            catch
            {
                return "";
            }
        }



        public static String removeChar(String niceurl, String[] danhsach)
        {
            foreach (String xoa in danhsach)
            {
                niceurl = niceurl.Replace(xoa, "");
            }
            return niceurl;
        }
        public static string ConvertVietnameseCharacterToEN(string sCharacter)
        {
            string sTemplate = "ĂẮẰẲẴẶăắằẳẵặÂẤẦẨẪẬâấầẩẫậÁÀẢÃẠáàảãạÔỐỒỔỖỘôốồổỗộƠỚỜỞỠỢơớờởỡợÓÒỎÕỌóòỏõọĐđÊẾỀỂỄỆêếềểễệÉÈẺẼẸéèẻẽẹƯỨỪỬỮỰưứừửữựÚÙỦŨỤúùủũụÍÌỈĨỊíìỉĩịÝỲỶỸỴýỳỷỹỵ";
            string sReplate = "AAAAAAaaaaaaAAAAAAaaaaaaAAAAAaaaaaOOOOOOooooooOOOOOOooooooOOOOOoooooDdEEEEEEeeeeeeEEEEEeeeeeUUUUUUuuuuuuUUUUUuuuuuIIIIIiiiiiYYYYYyyyyy";
            char[] arrChar = sTemplate.ToCharArray();
            char[] arrReChar = sReplate.ToCharArray();
            string sResult = "";//sCharacter;
            char digit;

            for (int ch = 0; ch < sCharacter.Length; ch++)
            {
                digit = Convert.ToChar(sCharacter.Substring(ch, 1));//arrChar[ch].ToString();;
                for (int i = 0; i < arrChar.Length; i++)
                    if (digit.Equals(arrChar[i]))
                        digit = arrReChar[i];
                sResult += digit;
            }

            return sResult;
        }
        public static string ConvertEN(string sCharacter)
        {
            string sTemplate = "ĂẮẰẲẴẶăắằẳẵặÂẤẦẨẪẬâấầẩẫậÁÀẢÃẠáàảãạÔỐỒỔỖỘôốồổỗộƠỚỜỞỠỢơớờởỡợÓÒỎÕỌóòỏõọĐđÊẾỀỂỄỆêếềểễệÉÈẺẼẸéèẻẽẹƯỨỪỬỮỰưứừửữựÚÙỦŨỤúùủũụÍÌỈĨỊíìỉĩịÝỲỶỸỴýỳỷỹỵ";
            string sReplate = "AAAAAAaaaaaaAAAAAAaaaaaaAAAAAaaaaaOOOOOOooooooOOOOOOooooooOOOOOoooooDdEEEEEEeeeeeeEEEEEeeeeeUUUUUUuuuuuuUUUUUuuuuuIIIIIiiiiiYYYYYyyyyy";
            string sDau = "[̃́]"; // dấu ngã, sắc, nặng, hỏi, ngã
            char[] arrChar = sTemplate.ToCharArray();
            char[] arrReChar = sReplate.ToCharArray();
            string sResult = "";//sCharacter;
            char digit;
            // modified by datnd - 1/4/2010
            // xoa di tat ca cac dau
            System.Text.RegularExpressions.Regex reg = new Regex(sDau);
            sCharacter = reg.Replace(sCharacter, "");

            try
            {
                sCharacter = Regex.Replace(sCharacter, @"&(\w)(\w){4,5};", "$1");
            }
            catch (ArgumentException ex) { }

            // end modified
            for (int ch = 0; ch < sCharacter.Length; ch++)
            {
                digit = Convert.ToChar(sCharacter.Substring(ch, 1));//arrChar[ch].ToString();;
                for (int i = 0; i < arrChar.Length; i++)
                {
                    if (digit.Equals(arrChar[i]))
                        digit = arrReChar[i];
                }
                sResult += digit;
            }

            return sResult;
        }
        public static string buildUrl(string url, int page, int total, int pageSize)
        {
            string result = string.Empty;
            int countPage = total / pageSize;
            if (total % pageSize != 0)
            {
                countPage = countPage + 1;
            }


            if (total > pageSize)
            {
                result = "<nav aria-label=\"Page navigation example\">";
                result += "<ul class=\"pagination pagination-circle pg-blue justify-content-center\">";
                if (page == 1)
                {
                    result += "<li class=\"page-item disabled\"><a href=\"" + url + "\" class=\"page-link\">Đầu tiên</a></li>";
                }
                else
                {
                    result += "<li class=\"page-item \"><a href=\"" + url + "\" class=\"page-link\">Đầu tiên</a></li>";
                }
                if (page > 1)
                {
                    result += "<li class=\"page-item disabled\"><a href=\"" + url + "/" + (page - 1) + "\" class=\"page-link\" aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span><span class=\"sr-only\">Quay lại</span></a></li>";
                }

                if (page < 4)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        if (i <= countPage)
                        {
                            if (i == page)
                            {
                                result += "<li class=\"page-item active\"><a  href=\"" + url + "/" + i + "\" class=\"page-link\">" + i + "</a></li>";
                            }
                            else
                            {
                                result += "<li class=\"page-item\"><a  href=\"" + url + "/" + i + "\"class=\"page-link\">" + i + "</a></li>";
                            }
                        }
                    }
                }
                else
                {
                    for (int i = page - 2; i <= page + 2; i++)
                    {
                        if (i <= countPage)
                        {
                            if (i == page)
                            {
                                result += "<li class=\"page-item active\"><a  href=\"" + url + "/" + i + "\" class=\"page-link\">" + i + "</a></li>";
                            }
                            else
                            {
                                result += "<li class=\"page-item\"><a  href=\"" + url + "/" + i + "\"class=\"page-link\">" + i + "</a></li>";
                            }
                        }
                    }


                }
                if (page < countPage - 1)
                {
                    result += "<li class=\"page-item\"><a href=\"" + url + "/" + (page + 1) + "\" class=\"page-link\" aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span><span class=\"sr-only\">Tiếp theo</span></a></li>";
                }
                if (page != countPage)
                {
                    result += "<li class=\"page-item\"><a href=\"" + url + "/" + countPage + "\" class=\"page-link\">Cuối cùng</a></li></ul></nav> ";
                }
                else
                {
                    result += "<li class=\"page-item disabled\"><a href=\"" + url + "/" + countPage + "\" class=\"page-link\">Cuối cùng</a></li></ul></nav> ";
                }




            }
            else
            {
                result = string.Empty;
            }





            return result;
        }

        public static string buildUrlbanggia(string url, int page, int total, int pageSize)
        {
            string result = string.Empty;
            int countPage = total / pageSize;
            if (total % pageSize != 0)
            {
                countPage = countPage + 1;
            }


            if (total > pageSize)
            {
                result = "<nav aria-label=\"Page navigation example\">";
                result += "<ul class=\"pagination pagination-circle pg-blue justify-content-center\">";
                if (page == 1)
                {
                    result += "<li class=\"page-item disabled\"><a href=\"" + url + "\" class=\"page-link\">Đầu tiên</a></li>";
                }
                else
                {
                    result += "<li class=\"page-item \"><a href=\"" + url + "\" class=\"page-link\">Đầu tiên</a></li>";
                }
                if (page > 1)
                {
                    result += "<li class=\"page-item disabled\"><a href=\"" + url + "/" + (page - 1) + "\" class=\"page-link\" aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span><span class=\"sr-only\">Quay lại</span></a></li>";
                }

                if (page < 4)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        if (i <= countPage)
                        {
                            if (i == page)
                            {
                                result += "<li class=\"page-item active\"><a  href=\"" + url + "/" + i + "\" class=\"page-link\">" + i + "</a></li>";
                            }
                            else
                            {
                                result += "<li class=\"page-item\"><a  href=\"" + url + "/" + i + "\"class=\"page-link\">" + i + "</a></li>";
                            }
                        }
                    }
                }
                else
                {
                    for (int i = page - 2; i <= page + 2; i++)
                    {
                        if (i <= countPage)
                        {
                            if (i == page)
                            {
                                result += "<li class=\"page-item active\"><a  href=\"" + url + "/" + i + "\" class=\"page-link\">" + i + "</a></li>";
                            }
                            else
                            {
                                result += "<li class=\"page-item\"><a  href=\"" + url + "/" + i + "\"class=\"page-link\">" + i + "</a></li>";
                            }
                        }
                    }


                }
                if (page < countPage - 1)
                {
                    result += "<li class=\"page-item\"><a href=\"" + url + "/" + (page + 1) + "\" class=\"page-link\" aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span><span class=\"sr-only\">Tiếp theo</span></a></li>";
                }
                if (page != countPage)
                {
                    result += "<li class=\"page-item\"><a href=\"" + url + "/" + countPage + "\" class=\"page-link\">Cuối cùng</a></li></ul></nav> ";
                }
                else
                {
                    result += "<li class=\"page-item disabled\"><a href=\"" + url + "/" + countPage + "\" class=\"page-link\">Cuối cùng</a></li></ul></nav> ";
                }




            }
            else
            {
                result = string.Empty;
            }





            return result;
        }
        //public static List<Cms_ImgPMBV> GetImage(int IDCLS, string macp)
        //{
        //    if (IDCLS != 0)
        //    {
        //        SQLServerConnection<Cms_ImgPMBV> sQLServer2 = new SQLServerConnection<Cms_ImgPMBV>();
        //        List<Cms_ImgPMBV> _Ketqua = sQLServer2.SelectQueryCommand("GetAllIMG_IDCLS1", Common.getConnectionStringIMGPMBV(), IDCLS, macp);
        //        return _Ketqua;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}
        public static string GetLinkSId(string sid)
        {
            if(sid != null)
            {
                string tokenkey = "m$dl4tec";
                tokenkey = SaltedHash.GetSHA512(tokenkey);
                sid = AES.EncryptString(sid.Trim(), "").Trim();
                return "/tra-cuu-ket-qua/sid?sid=" + sid + "&place=HN&" + "La=VN&token=" + tokenkey;

            }
            else
            {
                return "";
            }
        }
        public static string GetLinkDoiMKDVGM(string doctorid)
        {
            if (doctorid != null)
            {
                string tokenkey = "m$dl4tec";
                tokenkey = SaltedHash.GetSHA512(tokenkey);
                doctorid = AES.EncryptString(doctorid.Trim(), "").Trim();
                return "/tra-cuu-ket-qua/doi-mat-khau-dvgm?U=" + doctorid + "&token=" + tokenkey;

            }
            else
            {
                return "";
            }
        }
        public static void AddToLogFile(string content)
        {
            string fn = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + ".txt";
            try
            {

                String path = "";

                path = ConfigurationSettings.AppSettings["LOG_PATH"];

                path += "/" + fn;
                if (path != "")
                {
                    System.IO.StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.UTF8);
                    writer.WriteLine(content);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static string ConvertISO8859EntityNameToUnicode(string s)
        {
            string[] src = {"&Agrave;","&Aacute;","&Acirc;","&Atilde;","&Aring;","&Egrave;","&Eacute;","&Ecirc;","&Igrave;","&Iacute;","&ETH;","&Ograve;","&Ocirc;","&Otilde;","&Ugrave;","&Uacute;","&Yacute;","&agrave;","&acirc;","&atilde;","&egrave;","&eacute;","&ecirc;","&igrave;","&iacute;","&ograve;","&oacute;","&ocirc;","&otilde;","&ugrave;","&uacute;","&yacute;","&aacute;", "&nbsp;"
            };

            string[] taget = {"À","Á","Â","Ã","Å ","È","É","Ê","Ì","Í","Ð","Ò","Ô","Õ","Ù","Ú","Ý","à","â","ã","è","é","ê","ì","í","ò","ó","ô","õ","ù","ú","ý","á", " "
            };

            string result = s;
            for (int i = 0; i < src.Length; i++)
            {
                result = result.Replace(src[i], taget[i]);
            }
            return result;
        }

        public static string ConvertHtml(string s)
        {
            //HtmlDocument contenthtml = new HtmlDocument();
            //contenthtml.LoadHtml(s);
            //string content = contenthtml.DocumentNode.InnerText;
            //content = ConvertISO8859EntityNameToUnicode(content);
            //return content;
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="urlapi"></param>
        /// <returns></returns>
        public static object SendPost(object obj, string urlapi)
        {
            object result = string.Empty;
            using (var c = new WebClient())
            {
                c.Headers[HttpRequestHeader.ContentType] = "application/json";
                string serialisedData = JsonConvert.SerializeObject(obj);
                var response = c.UploadString(urlapi, serialisedData);
                result = JsonConvert.DeserializeObject(response);
            }
            return result;
        }
    }

}
