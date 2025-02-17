﻿using System;
using System.Diagnostics;
using Microsoft.Maui.Controls.CustomAttributes;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Essentials;
using Microsoft.Maui.Graphics;
using AbsoluteLayoutFlags = Microsoft.Maui.Layouts.AbsoluteLayoutFlags;

namespace Microsoft.Maui.Controls.Compatibility.ControlGallery.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 2987, "When setting the minimum and maximum date for a date picker, only allow valid dates to be seen/selected from the DatePicker dialog", PlatformAffected.Android)]
	public class Issue2987 : TestContentPage
	{
		public AbsoluteLayout layout;

		protected override void Init()
		{
			var datePicker = new DatePicker { AutomationId = "datePicker" };
			datePicker.MinimumDate = new DateTime(2015, 1, 1);
			datePicker.MaximumDate = new DateTime(2015, 6, 1);
			datePicker.Date = DateTime.Now;
			datePicker.Format = "MMM dd, yyyy";
			datePicker.DateSelected += (object sender, DateChangedEventArgs e) =>
			{
				Debug.WriteLine("Date changed");
			};

			Padding = DeviceInfo.Platform == DevicePlatform.iOS ? new Thickness(10, 20, 10, 5) : new Thickness(10, 0, 10, 5);

			layout = new AbsoluteLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

			AbsoluteLayout.SetLayoutFlags(datePicker, AbsoluteLayoutFlags.None);
			AbsoluteLayout.SetLayoutBounds(datePicker, new Rect(0f, 0f, 300f, 50f));

			layout.Children.Add(datePicker);

			Content = layout;
		}
	}
}
