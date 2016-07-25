using System;
using AppServiceHelpers.Abstractions;

namespace FormsSample.ViewModels
{
    public class ToDoViewModel : AppServiceHelpers.Forms.BaseAzureViewModel<Models.ToDo>
    {
		readonly Models.ToDo todo;

		public ToDoViewModel(IEasyMobileServiceClient client, Models.ToDo todo) : base (client)
        {
            this.todo = todo;
        }

		public Models.ToDo Item
		{
			get
			{
				return todo;
			}
		}

		public string Text
        {
            get
            {
                return todo.Text;   
            }
            set
            {
				if (todo.Text != value)
				{
					todo.Text = value;
					OnPropertyChanged(nameof(Text));
				}
            }
        }

		public bool Completed
        {
            get
            {
                return todo.Completed;
            }
            set
            {
				if (todo.Completed != value)
				{
					todo.Completed = value;
					OnPropertyChanged(nameof(Completed));
				}
            }
        }
    }
}

