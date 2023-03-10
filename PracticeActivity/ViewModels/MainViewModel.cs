using PracticeActivity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PracticeActivity.ViewModels
{
    public class MainViewModel
    {
        public ICommand RegisterGoods => new Command(ShowRegisterGoodsPage);
        public ICommand RegisterServices => new Command(ShowRegisterServicePage);
        public ICommand ShowListGoods => new Command(ShowListGoodsPage);
        public ICommand ShowListService => new Command(ShowListServicePage);

        public async void ShowRegisterGoodsPage()
        {
            //nos envía a la pagina del listado alumnos
            await App.Current.MainPage.Navigation.PushAsync(new RegisterGoodPage());
            //nos elimina la pagina anterior del estack de paginas
            var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() == typeof(RegisterGoodPage))
                    continue;
                App.Current.MainPage.Navigation.RemovePage(page);
            }

        }
        public async void ShowRegisterServicePage()
        {
            //nos envía a la pagina del listado alumnos
            await App.Current.MainPage.Navigation.PushAsync(new RegisterServicePage());
            //nos elimina la pagina anterior del estack de paginas
            var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() == typeof(RegisterServicePage))
                    continue;
                App.Current.MainPage.Navigation.RemovePage(page);
            }

        }
        public async void ShowListGoodsPage()
        {
            //nos envía a la pagina del listado alumnos
            await App.Current.MainPage.Navigation.PushAsync(new GoodList());
            //nos elimina la pagina anterior del estack de paginas
            var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() == typeof(GoodList))
                    continue;
                App.Current.MainPage.Navigation.RemovePage(page);
            }

        }
        public async void ShowListServicePage()
        {
            //nos envía a la pagina del listado alumnos
            await App.Current.MainPage.Navigation.PushAsync(new ServiceListPage());
            //nos elimina la pagina anterior del estack de paginas
            var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() == typeof(ServiceListPage))
                    continue;
                App.Current.MainPage.Navigation.RemovePage(page);
            }

        }
    }
}
