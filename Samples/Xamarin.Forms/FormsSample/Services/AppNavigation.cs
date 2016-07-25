using System;
using System.Threading.Tasks;
using AppServiceHelpers.Abstractions;
using AppServiceHelpers.Forms;
using FormsSample.Pages;
using Xamarin.Forms;

namespace FormsSample
{
	public class AppNavigation 
	{
		public static INavigationService Instance { get; set; }  
	}
}
