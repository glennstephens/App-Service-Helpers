using System;
using System.Threading.Tasks;
using AppServiceHelpers.Abstractions;
using AppServiceHelpers.Forms;
using FormsSample.Models;
using Xamarin.Forms;

namespace FormsSample.ViewModels
{
	public class ToDosViewModel : BaseAzureViewModel<ToDo>
    {
        public ToDosViewModel(IEasyMobileServiceClient client) : base (client)
        {
			Title = "To do List";
        }

		Models.ToDo selectedItem;
		public Models.ToDo SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
				OnPropertyChanged(nameof(SelectedItem));

                if (selectedItem != null)
                {
					Device.BeginInvokeOnMainThread(() =>
					{
						AppNavigation.Instance.NavigateToPageAsync(new ToDoViewModel(client, selectedItem), NavigationStyle.ToDoEdit).IgnoreResult();
						SelectedItem = null;
					});
                }
            }
        }

		// Overrides to hook into some custom inbuilt navigation. 

		public override async Task<ToDo> AddNewItemAsync()
		{
			var item = await base.AddNewItemAsync();
			await AppNavigation.Instance.NavigateToPageAsync(new ToDoViewModel(client, item), NavigationStyle.ToDoEdit);

			return item;
		}

		public override async Task UpdateItemAsync(ToDo item)
		{
			await base.UpdateItemAsync(item);

			await AppNavigation.Instance.NavigateHomeAsync();

			SelectedItem = null;
		}

		public async override Task DeleteItemAsync(ToDo item)
		{
			await base.DeleteItemAsync(item);

			await AppNavigation.Instance.NavigateHomeAsync();

			SelectedItem = null;
		}
    }
}

