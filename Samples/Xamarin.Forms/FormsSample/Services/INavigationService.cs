using System;
using System.Threading.Tasks;
using AppServiceHelpers.Abstractions;
using AppServiceHelpers.Forms;
using FormsSample.Pages;
using Xamarin.Forms;

namespace FormsSample
{
	public interface INavigationService
	{
		Task NavigateHomeAsync();
		Task NavigateToPageAsync(object viewModel, NavigationStyle mode);
	}
}
