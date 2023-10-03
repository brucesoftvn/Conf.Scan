using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;


/// <summary>
/// 
/// </summary>
namespace MEDAZ.SCAN
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainPage : ContentPage
    {
        Models.Com_Data Thuoc;
        ObservableCollection<Models.Com_Data> _thuoc;
        List<Models.Con_CheckList> lstKiemke;
        string _makho;
        string _tenkho;
        string _username;
        string _idSelected = "";
        string _nameSelected = "";
        string _diachiSelected = "";
        string _dienthoai = "";
        bool iSua = false;
        DateTime d = DateTime.Now.AddMonths(1);
        /// <summary>
        /// 
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            ButtonScanDefault.Clicked += ButtonScanDefault_Clicked;
            ButtonSave.Clicked += ButtonSave_Cliked;
            ButtonDELETE.Clicked += ButtonDELETE_Clicked;
        }
        /// <summary>
        ///  Xóa dữ liệu kiểm kê
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonDELETE_Clicked(object sender, EventArgs e)
        {
            GetListKiemKe();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="makho"></param>
        public MainPage(string makho)
        {
            InitializeComponent();
            ButtonScanDefault.Clicked += ButtonScanDefault_Clicked;
            ButtonSave.Clicked += ButtonSave_Cliked;
            _makho = makho.Split('-')[0];
            _tenkho = ": " + makho.Split('-')[1];
            //GetListKiemKe(d.Month.ToString(), d.Year.ToString(), _makho);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="makho"></param>
        public MainPage(string makho, string username)
        {
            InitializeComponent();
            ButtonScanDefault.Clicked += ButtonScanDefault_Clicked;
            ButtonSave.Clicked += ButtonSave_Cliked;
            _makho = makho.Split('-')[0];
            _tenkho = ": " + makho.Split('-')[1];
            _username = username;
        }

        /// <summary>
        /// Lưu dữ liệu kiểm kê
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Cliked(object sender, EventArgs e)
        {
            CheckIn();
        }
        /// <summary>
        /// CheckIn
        /// </summary>
        private void CheckIn()
        {
            try
            {
                if (txtBarCode.Text.Trim().Length > 0 && txtTenVT.Text.Trim().Length > 0)
                {
                    Models.ConCheck obj = new Models.ConCheck();
                    obj.MaKH = txtBarCode.Text;
                    obj.MachineCheck = Environment.MachineName;
                    obj.Hoten = txtTenVT.Text.Trim();
                    obj.Dienthoai = txtSoDT.Text;
                    obj.Diachi = txtDiaChi.Text;
                    //obj.Dob = txtNamSinh.Date.ToString("dd/MM/yyyy"); ;
                    obj.Dob = txtNamSinh.Text;
                    SaveProduct(obj);
                    UpdateInfo();
                    GetListKiemKe();
                    //Clear();
                }
                else if (txtBarCode.Text.Trim().Length == 0 && txtSoDT.Text.Trim().Length > 0)
                {
                    Models.ConCheck obj = new Models.ConCheck();
                    obj.MaKH = txtSoDT.Text;
                    obj.MachineCheck = Environment.MachineName;
                    obj.Hoten = txtTenVT.Text.Trim();
                    obj.Dienthoai = txtSoDT.Text;
                    obj.Diachi = txtDiaChi.Text;
                    obj.Dob = txtNamSinh.Text;
                    SaveProduct(obj);
                    UpdateInfo();
                    GetListKiemKe();
                    //Clear();
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Input Error Checkin: " + ex.Message, "OK");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void Clear()
        {
            txtBarCode.Text = "";
            txtTenVT.Text = "";
            txtSoDT.Text = "";
            txtDiaChi.Text = "";
        }
        /// <summary>
        ///  Quét mã vạch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonScanDefault_Clicked(object sender, EventArgs e)
        {
            ZXingScannerPage scanPage;
            scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;
                scanPage.AutoFocus();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync();
                    txtBarCode.Text = result.Text;
                    await GetInfo(txtBarCode.Text);
                });
            };
            await Navigation.PushModalAsync(scanPage);
        }

        /// <summary>
        /// Lấy thông tin mã vạch
        /// </summary>
        /// <param name="mavach"></param>
        private async Task GetInfo(string mavach)
        {
            try
            {
                if (CheckNetwork())
                {
                    using (var client = new HttpClient())
                    {
                        var uri = "http://api.orsimed.vn/api/Data/API_CONF_GETINFO?makh=" + mavach;
                        var result = await client.GetStringAsync(uri);
                        Thuoc = JsonConvert.DeserializeObject<Models.Com_Data>(result);
                        if (Thuoc == null)
                        {
                            ButtonSave.IsEnabled = false;
                        }
                        else
                            ButtonSave.IsEnabled = true;
                    }
                    if (Thuoc != null)
                    {
                        txtTenVT.Text = Thuoc.Hoten;
                        txtBarCode.Text = Thuoc.MaKH;
                        txtSoDT.Text = Thuoc.DienThoai;
                        txtDiaChi.Text = Thuoc.Diachi;
                        txtNamSinh.Text = Thuoc.Dob;
                    }
                    else
                    {
                        txtSoDT.Text = txtBarCode.Text;
                        txtTenVT.Text = "";
                        txtDiaChi.Text = "";
                        
                    }    
                }
                else
                {
                    DisplayAlert("Error", "The network is not stable, check again please!", "OK");
                }
            }
            catch
            {
                txtTenVT.Text = "";
                txtSoDT.Text = "";
                txtDiaChi.Text = "";
                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Clicked(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            if (menuitem != null && txtBarCode.Text.Trim().Length > 0)
            {
                CheckIn();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Clicked(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            if (menuitem != null)
            {
                using (var client = new HttpClient())
                {
                    var uri = "http://api.orsimed.vn/api/data/API_CONF_DELETE?makh=" + txtBarCode.Text;
                    var result = client.GetStringAsync(uri);
                    txtBarCode.Text = "";
                    txtDiaChi.Text = "";
                    txtTenVT.Text = "";
                    txtSoDT.Text = "";
                    GetListKiemKe();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Clicked(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            if (menuitem != null)
            {
                iSua = true;
                if (CheckNetwork())
                {
                    try
                    {
                        UpdateInfo();
                        GetListKiemKe();
                    }
                    catch (Exception ex)
                    {
                        DisplayAlert("Alert", "Input error:" + ex.Message, "OK");
                    }
                }
                else
                {
                    DisplayAlert("Error", "The network is not stable, check again please!!", "OK");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void UpdateInfo()
        {
            Models.Com_Data product = new Models.Com_Data();
            product.MaKH = txtBarCode.Text;
            product.Hoten = txtTenVT.Text;
            product.DienThoai = txtSoDT.Text;
            product.Diachi = txtDiaChi.Text;
            product.Dob = txtNamSinh.Text;

            string responseText = "";
            string json = JsonConvert.SerializeObject(product);
            string webAddr = "http://api.orsimed.vn/api/data/API_CONF_UPDATE";
            var httpWebRequest = WebRequest.CreateHttp(webAddr);

            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                responseText = streamReader.ReadToEnd();
            }
            CrossToastPopUp.Current.ShowToastMessage("Save Successful '" + txtTenVT.Text + "'!");
           
        }
        /// <summary>
        /// Lấy danh sách mã đã nhập kiểm kê
        /// </summary>
        private async void GetListKiemKe()
        {
            try
            {

                using (var client = new HttpClient())
                {
                    var uri = "http://api.orsimed.vn/api/data/API_CONF_LIST?keyword=''";
                    var result = await client.GetStringAsync(uri);
                    lstKiemke = JsonConvert.DeserializeObject<List<Models.Con_CheckList>>(result);
                    ProductsListView.ItemsSource = lstKiemke;
                    CrossToastPopUp.Current.ShowToastMessage("Complete!");
                    ButtonDELETE.Text = "Show All (" + lstKiemke.Count.ToString() + ")";
                }

            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Input error List Product: " + ex.Message, "OK");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public string SaveProduct(Models.ConCheck product)
        {
            if (CheckInput())
            {
                if (CheckNetwork())
                {
                    try
                    {
                        string responseText = "";
                        string json = GetJson(product);
                        string webAddr = "http://api.orsimed.vn/api/data/API_CONF_CHECKIN";
                        var httpWebRequest = WebRequest.CreateHttp(webAddr);

                        httpWebRequest.ContentType = "application/json; charset=utf-8";
                        httpWebRequest.Method = "POST";
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            streamWriter.Write(json);
                            streamWriter.Flush();
                        }
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            responseText = streamReader.ReadToEnd();
                        }
                        CrossToastPopUp.Current.ShowToastMessage("Save Successful '" + txtTenVT.Text + "'!");
                      
                        return responseText;
                    }
                    catch (Exception ex)
                    {
                        DisplayAlert("Alert", "Input error Save Product: " + ex.Message, "OK");
                        return "Error";
                    }
                }
                else
                {
                    DisplayAlert("Error", "The network is not stable, check again please!!", "OK");
                    return "";
                }    
                
            }
            else
            {
                DisplayAlert("Error", "Input erorr: Check Input", "OK");
                return "";
            }
        }
        /// <summary>
        /// 
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
        /// Kiểm tra dữ liệu đầu vào
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            //if (txtBarCode.Text.Trim().Length > 0 && txtTenVT.Text.Trim().Length > 0)
            //{
            //    return true;
            //}
            //else
            //    return false;
            return true;
        }
        /// <summary>
        /// Lấy thông tin chuỗi Json body kiểm kê
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetJson(Models.ConCheck obj)
        {
            return "{" +
            "\"makh\" : \"" + obj.MaKH + "\"," +
            "\"machineCheck\": \"" + obj.MachineCheck + "\"," +
            "\"hoten\" : \"" + obj.Hoten + "\"," +
            "\"dienthoai\": \"" + obj.Dienthoai + "\"," +
            "\"diachi\": \"" + obj.Diachi + "\"," +
            "\"dob\": \"" + obj.Dob + "\"," +
            "}";
        } 

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<Models.Com_Data> Thuocs
        {
            get
            {
                return _thuoc;
            }
            set
            {
                _thuoc = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetInfo(txtBarCode.Text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ProductsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            Models.Con_CheckList obj = (Models.Con_CheckList)e.Item;
            _idSelected = obj.MaKH;
            _nameSelected = obj.Hoten;
            _diachiSelected = obj.Diachi;
            _dienthoai = obj.DienThoai;
            txtBarCode.Text = _idSelected;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Models.Con_CheckList obj = (Models.Con_CheckList)e.SelectedItem;
                _idSelected = obj.MaKH;
                _nameSelected = obj.Hoten;
                _diachiSelected = obj.Diachi;
                _dienthoai = obj.DienThoai;
                
                //txtBarCode.Text = _idSelected;
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert Selected", ex.Message, "OK");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        private async void GetListKiemKe(string keyword)
        {
            try
            {
                ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true, Color = Color.Orange };
                using (var client = new HttpClient())
                {
                    var uri = "http://api.orsimed.vn/api/data/API_CONF_LIST?keyword=" + keyword;
                    var result = await client.GetStringAsync(uri);
                    lstKiemke = JsonConvert.DeserializeObject<List<Models.Con_CheckList>>(result);
                    ProductsListView.ItemsSource = lstKiemke;
                    CrossToastPopUp.Current.ShowToastMessage("Complete!");
                    ButtonDELETE.Text = "Show All (" + lstKiemke.Count.ToString() + ")";
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
        private void btnSearch_Clicked(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length >= 3)
                GetListKiemKe(txtSearch.Text.Trim());
            else
                CrossToastPopUp.Current.ShowToastMessage("Nhập tối thiểu 3 ký tự!");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}