using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace AppServiceHelpers.Models
{
	public class EntityData : EntityDataAlwaysLatest
    {
        [Version]
        public string AzureVersion { get; set; }
	}
}

