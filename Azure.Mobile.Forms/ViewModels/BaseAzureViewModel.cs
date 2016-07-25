using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

using AppServiceHelpers.Abstractions;
using AppServiceHelpers.Models;

using Xamarin.Forms;

namespace AppServiceHelpers.Forms
{
	public class BaseAzureViewModel<T> : INotifyPropertyChanged where T : EntityDataAlwaysLatest
    {
        protected IEasyMobileServiceClient client;

        ITableDataStore<T> table;	

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Azure.Mobile.Forms.BaseAzureViewModel`1"/> class.
		/// </summary>
		/// <param name="client">EasyMobileServiceClient for performing table operations.</param>
        public BaseAzureViewModel(IEasyMobileServiceClient client)
        {
            this.client = client;
            table = client.Table<T>();
        }

        /// <summary>
        /// Returns an ObservableCollection of all the items in the table.
        /// </summary>
        ObservableCollection<T> items = new ObservableCollection<T>();
        public ObservableCollection<T> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("items");
            }
        }

		/// <summary>
		/// Adds an item to the table.
		/// </summary>
		/// <param name="item">The item to add to the table.</param>
		public virtual async Task AddItemAsync(T item) 
        {
			await table.AddAsync(item);

			Device.BeginInvokeOnMainThread(() =>
			{
				items.Add(item);
			});
        }

		/// <summary>
		/// Creates and adds a new item to the table
		/// </summary>
		/// <returns>The new item async.</returns>
		public virtual async Task<T> AddNewItemAsync()
		{
			var item = (T)Activator.CreateInstance(typeof(T));

			await table.AddAsync(item);

			Device.BeginInvokeOnMainThread(() =>
			{
				items.Add(item);
			});

			return item;
		}

		/// <summary>
		/// Deletes an item from the table.
		/// </summary>
		/// <param name="item">The item to delete from the table.</param>
		public virtual async Task DeleteItemAsync(T item)
        {
            await table.DeleteAsync(item);

			Device.BeginInvokeOnMainThread(() =>
			{
				items.Remove(item);
			});
        }

		/// <summary>
		/// Updates an item in the table.
		/// </summary>
		/// <param name="item">The item to update in the table.</param>
		public virtual async Task UpdateItemAsync(T item)
        {
            await table.UpdateAsync(item);
        }

        /// <summary>
        /// Refresh the table, and sychronize data with Azure.
        /// </summary>
        Command refreshCommand;
        public Command RefreshCommand
        {
            get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
        }

        async Task ExecuteRefreshCommand()
        {
            IsBusy = true;

			try
			{
				var _items = await table.GetItemsAsync();

				Device.BeginInvokeOnMainThread(() =>
				{
					Items.Clear();
					foreach (var item in _items)
						Items.Add(item);
				});
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
			finally
			{
				IsBusy = false;
			}
        }

		Command<T> saveItemCommand;
		public Command<T> SaveItemCommand
		{
			get { return saveItemCommand ?? (saveItemCommand = new Command<T>(async (item) => await UpdateItemAsync(item))); }
		}

		Command<T> deleteItemCommand;
		public Command<T> DeleteItemCommand
		{
			get { return deleteItemCommand ?? (deleteItemCommand = new Command<T>(async (item) => await DeleteItemAsync(item))); }
		}

		Command addNewItemCommand;
		public Command AddNewItemCommand
		{
			get { return addNewItemCommand ?? (addNewItemCommand = new Command(async () => await AddNewItemAsync())); }
		}

        string title = string.Empty;
        public const string TitlePropertyName = "Title";

        /// <summary>
        /// The title of the page.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value, TitlePropertyName); }
        }

        string subTitle = string.Empty;
        /// <summary>
        /// The subtitle of the page.
        /// </summary>
        public const string SubtitlePropertyName = "Subtitle";
        public string Subtitle
        {
            get { return subTitle; }
            set { SetProperty(ref subTitle, value, SubtitlePropertyName); }
        }

        string icon = null;
        /// <summary>
        /// The icon of the page.
        /// </summary>
        public const string IconPropertyName = "Icon";
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value, IconPropertyName); }
        }

		bool isBusy = false;
        /// <summary>
        /// The current state of the view.
        /// </summary>
        public const string IsBusyPropertyName = "IsBusy";
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value, IsBusyPropertyName); }
        }

        bool canLoadMore = true;
        /// <summary>
        /// Can we load more items?
        /// </summary>
        public const string CanLoadMorePropertyName = "CanLoadMore";
        public bool CanLoadMore
        {
            get { return canLoadMore; }
            set { SetProperty(ref canLoadMore, value, CanLoadMorePropertyName); }
        }

        protected void SetProperty<T>(
            ref T backingStore, T value,
            string propertyName,
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

		/// <summary>
		/// An implementation of INotifyPropertyChanged.
		/// </summary>
		/// <param name="propertyName">The property name to fire the PropertyChanged event on.</param>
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}