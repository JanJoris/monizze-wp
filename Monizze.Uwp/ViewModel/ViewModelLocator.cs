/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Monizze"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Monizze.Api.Client;
using Monizze.Common.Implementations;
using Monizze.Common.Interfaces;
using Monizze.Common.Model;
using Monizze.Interfaces;
using Monizze.LiveTile;
using Monizze.Model;

namespace Monizze.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<ICredentialManager, CredentialManager>();
            SimpleIoc.Default.Register<INotificationManager, NotificationManager>();
            SimpleIoc.Default.Register<ILogger, Logger>();
            SimpleIoc.Default.Register<IDeviceInfo, DeviceInfo>();
            SimpleIoc.Default.Register<ITileUpdater, TileUpdater>();
#if MOCK
            SimpleIoc.Default.Register<IMonizzeClient, MockClient>();
#else
            SimpleIoc.Default.Register<IMonizzeClient, MonizzeClient>();
#endif
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<AccountViewModel>();
            
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public LoginViewModel LoginViewModel => ServiceLocator.Current.GetInstance<LoginViewModel>();


        public AccountViewModel AccountViewModel => ServiceLocator.Current.GetInstance<AccountViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}