using System;
using AppServiceHelpers.Models;

namespace FormsSample.Models
{
	public class ToDo : EntityData
    {
		string text;
        public string Text
		{
			get {
				return text;
			}
			set
			{
				SetProperty(ref text, value, nameof(Text));
			}
		}

		bool completed;
        public bool Completed
		{
			get
			{
				return completed;
			}
			set
			{
				SetProperty(ref completed, value, nameof(Completed));
			}
		}
    }
}

