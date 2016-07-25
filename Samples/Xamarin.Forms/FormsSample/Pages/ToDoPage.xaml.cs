using System;
using System.Collections.Generic;
using AppServiceHelpers.Abstractions;
using FormsSample.ViewModels;
using Xamarin.Forms;

namespace FormsSample.Pages
{
    public partial class ToDoPage : ContentPage
    {
		public ToDoPage(IEasyMobileServiceClient client, ToDoViewModel viewModel) : base()
        {
			BindingContext = viewModel;
            InitializeComponent();
        }
    }
}

