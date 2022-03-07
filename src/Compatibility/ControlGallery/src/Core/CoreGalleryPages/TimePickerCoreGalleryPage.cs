using System;
using Microsoft.Maui.Controls.CustomAttributes;
using Microsoft.Maui.Essentials;
using Microsoft.Maui.Graphics;

namespace Microsoft.Maui.Controls.Compatibility.ControlGallery
{
	internal class TimePickerCoreGalleryPage : CoreGalleryPage<TimePicker>
	{
		protected override bool SupportsTapGestureRecognizer => false;

		protected override void Build(StackLayout stackLayout)
		{
			base.Build(stackLayout);
			var formatContainer = new ViewContainer<TimePicker>(Test.TimePicker.Format, new TimePicker { Format = "HH-mm-ss" });
			var timeContainer = new ViewContainer<TimePicker>(Test.TimePicker.Time,
				new TimePicker { Time = new TimeSpan(14, 45, 50) });
			var textColorContainer = new ViewContainer<TimePicker>(Test.TimePicker.TextColor,
				new TimePicker { Time = new TimeSpan(14, 45, 50), TextColor = Colors.Lime });
			var fontAttributesContainer = new ViewContainer<TimePicker>(Test.TimePicker.FontAttributes,
				new TimePicker { FontAttributes = FontAttributes.Bold });

			var fontFamilyContainer = new ViewContainer<TimePicker>(Test.TimePicker.FontFamily,
				new TimePicker());
			// Set font family based on available fonts per platform
			if (DeviceInfo.Platform == DevicePlatform.Android)
				fontFamilyContainer.View.FontFamily = "sans-serif-thin";
			else if (DeviceInfo.Platform == DevicePlatform.iOS)
				fontFamilyContainer.View.FontFamily = "Courier";
			else if (DeviceInfo.Platform == DevicePlatform.WinUI)
				fontFamilyContainer.View.FontFamily = "Comic Sans MS";
			else
				fontFamilyContainer.View.FontFamily = "Garamond";

			var fontSizeContainer = new ViewContainer<TimePicker>(Test.TimePicker.FontSize,
				new TimePicker { FontSize = 24 });

			Add(formatContainer);
			Add(timeContainer);
			Add(textColorContainer);
			Add(fontAttributesContainer);
			Add(fontFamilyContainer);
			Add(fontSizeContainer);
		}
	}
}