using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace AppServiceHelpers.Models
{
    public class EntityDataAlwaysLatest : INotifyPropertyChanged
    {  
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "createdAt")]
		public DateTimeOffset CreatedAt { get; set; }

		[UpdatedAt]
		public DateTimeOffset UpdatedAt { get; set; }

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
    }
}

