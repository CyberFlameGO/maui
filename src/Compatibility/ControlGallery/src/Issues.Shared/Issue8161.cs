﻿
using Microsoft.Maui.Controls.CustomAttributes;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Essentials;

namespace Microsoft.Maui.Controls.Compatibility.ControlGallery.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 8161, "On WPF ToolbarItem.IsEnabled = false does nothing", PlatformAffected.WPF, navigationBehavior: NavigationBehavior.SetApplicationRoot)]
	public class Issue8161 : TestFlyoutPage
	{
		protected override void Init()
		{
			Flyout = new ContentPage
			{
				Title = "8161"
			};
			var page = new ContentPage();
			page.ToolbarItems.Add(new ToolbarItem() { Text = "enabled 1", IsEnabled = true, Order = ToolbarItemOrder.Primary, IconImageSource = "bank.png" });
			page.ToolbarItems.Add(new ToolbarItem() { Text = "disabled 1", IsEnabled = false, Order = ToolbarItemOrder.Primary, IconImageSource = "bank.png" });
			page.ToolbarItems.Add(new ToolbarItem() { Text = "enabled 2", IsEnabled = true, Order = ToolbarItemOrder.Secondary, IconImageSource = "coffee.png" });
			page.ToolbarItems.Add(new ToolbarItem() { Text = "disabled 2", IsEnabled = false, Order = ToolbarItemOrder.Secondary, IconImageSource = "coffee.png" });
			Detail = new NavigationPage(page);
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			// WPF doesn't show ToolbarItems for pages in modal stack 
			// so we use NavigationBehavior.SetApplicationRoot and pop TestCases page from modal stack to make this test case visible with toolbar items
			if (DeviceInfo.Platform == DevicePlatform.Create("WPF"))
				Navigation.PopModalAsync();
		}
	}
}
