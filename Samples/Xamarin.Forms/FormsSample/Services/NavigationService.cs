using System;
using System.Threading.Tasks;
using AppServiceHelpers.Abstractions;
using AppServiceHelpers.Forms;
using FormsSample.Pages;
using Xamarin.Forms;

namespace FormsSample
{
	public class NavigationService : INavigationService
	{
		NavigationPage NavPage { get; set; }
		IEasyMobileServiceClient client { get; set; }

		public NavigationService(IEasyMobileServiceClient client, NavigationPage navPage)
		{
			this.client = client;
			this.NavPage = navPage;
		}

		public async Task NavigateHomeAsync()
		{
			await NavPage.PopToRootAsync(true);
		}

		public async Task NavigateToPageAsync(object viewModel, NavigationStyle mode)
		{
			switch (mode)
			{
				case NavigationStyle.ToDoList:
					await NavPage.PopToRootAsync(true);
					break;
				case NavigationStyle.ToDoEdit:
					var todoPage = new ToDoPage(client, viewModel as ViewModels.ToDoViewModel);
					await NavPage.PushAsync(todoPage, true);
					break;
			}
		}
	}

}

