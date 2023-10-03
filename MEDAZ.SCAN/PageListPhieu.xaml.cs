using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MEDAZ.SCAN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListPhieu : ContentPage
    {
        public PageListPhieu()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           
        }
    }
}