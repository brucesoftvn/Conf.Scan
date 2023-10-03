using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
/// <summary>
/// 
/// </summary>
namespace MEDAZ.SCAN
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("User"));
            }
        }
        private string password;
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ICommand SubmitCommand { protected set; get; }
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public void OnSubmit()
        {
            //if (email != "admin" || password != "123")
            //{
            //    DisplayInvalidLoginPrompt();
            //}
        }
    }
}