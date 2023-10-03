using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MEDAZ.SCAN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private List<Models.Khole> lstKhole;
        /// <summary>
        /// 
        /// </summary>
        public LoginPage()
        {
            var vm = new LoginViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            InitializeComponent();
            Username.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };
            Password.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
            
        }
        /// <summary>
        /// Kiểm tra tình trạng mạng
        /// </summary>
        /// <returns></returns>
        private bool CheckNetwork()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if(pickedKho.Items.Count>0)
            {

            }   
            else
            {
                GetKho();
            }    
            //var picker = (Picker)sender;
            //int selectedIndex = pickedKho.SelectedIndex;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnNavigateButtonClicked(object sender, EventArgs e)
        {
            if (CheckNetwork())
            {
                if (CheckLoginAsync(Username.Text, Password.Text) == true && pickedKho.SelectedItem.ToString().Length > 0)
                {
                    await Navigation.PushAsync(new MainPage(pickedKho.SelectedItem.ToString(), Username.Text));
                }
                else
                {
                    DisplayAlert("Lỗi", "Sai user, mật khẩu hoặc chưa chọn kho, vui lòng đăng nhập lại", "OK");
                }
            }
            else
            {
                DisplayAlert("Lỗi", " Kết nối mạng không ổn định, vui lòng kiểm tra lại !", "OK");
            }    
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool CheckLoginAsync(string username, string password)
        {
            try
            {
                var webRequest = WebRequest.Create("http://online.orsimed.vn/api/data/API_Login?username=" + username + "&password=" + password) as HttpWebRequest;
                if (webRequest == null)
                {
                    return false;
                }           
                webRequest.ContentType = "application/json;charset=utf-8";
                using (var s = webRequest.GetResponse().GetResponseStream())
                {
                    using (var sr = new StreamReader(s))
                    {
                        var contributorsAsJson = sr.ReadToEnd();
                        Models.User item = JsonConvert.DeserializeObject<Models.User>(contributorsAsJson);
                        if (item != null)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Lấy danh sách kho
        /// </summary>
        private async void GetKho()
        {
            try
            {
                if (CheckNetwork())
                {
                    pickedKho.Items.Clear();
                    var webRequest = WebRequest.Create("http://online.orsimed.vn/api/data/API_GETLIST_KHO?makho=" + Username.Text) as HttpWebRequest;
                    webRequest.ContentType = "application/json;charset=utf-8";
                    using (var s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (var sr = new StreamReader(s))
                        {
                            var contributorsAsJson = sr.ReadToEnd();
                            lstKhole = JsonConvert.DeserializeObject<List<Models.Khole>>(contributorsAsJson);
                            for (int i = 0; i < lstKhole.Count; i++)
                            {
                                pickedKho.Items.Add(lstKhole[i].Makho + "-" + lstKhole[i].Tenkho);
                            }
                        }
                    }
                }
                else
                {
                    DisplayAlert("Lỗi", " Kết nối mạng không ổn định, vui lòng kiểm tra lại !", "OK");
                }    
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pickedKho_Focused(object sender, FocusEventArgs e)
        {
            GetKho();
        }
    }
}