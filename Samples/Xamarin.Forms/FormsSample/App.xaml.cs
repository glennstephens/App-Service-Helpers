using AppServiceHelpers;
using AppServiceHelpers.Abstractions;
using FormsSample.Models;
using Xamarin.Forms;

namespace FormsSample
{
	public partial class App : Application
    {
		public static ViewModels.ToDosViewModel AppViewModel;

        IEasyMobileServiceClient client;
     
		public App()
        {
            InitializeComponent();

            client = new EasyMobileServiceClient();
            client.Initialize("http://xamarin-todo-sample.azurewebsites.net");
            client.RegisterTable<ToDo>();
            client.FinalizeSchema();

			AppViewModel = new ViewModels.ToDosViewModel(client);

			var navPage = new NavigationPage(new Pages.ToDoListPage());
			MainPage = navPage;

			AppNavigation.Instance = new NavigationService(client, navPage);
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps

        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

