using System;
using System.Collections.Generic;
using AppServiceHelpers;
using AppServiceHelpers.Abstractions;
using Xamarin.Forms;

namespace FormsSample.Pages
{
    public partial class ToDoListPage : ContentPage
    {
        public ToDoListPage()
        {
			BindingContext = App.AppViewModel;
            InitializeComponent();
        }
    }
}

