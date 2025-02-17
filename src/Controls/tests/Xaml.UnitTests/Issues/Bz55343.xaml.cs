using System;
using Microsoft.Maui.Controls.Core.UnitTests;
using Microsoft.Maui.Essentials;
using NUnit.Framework;

namespace Microsoft.Maui.Controls.Xaml.UnitTests
{
	public partial class Bz55343 : ContentPage
	{
		public Bz55343()
		{
			InitializeComponent();
		}

		public Bz55343(bool useCompiledXaml)
		{
			//this stub will be replaced at compile time
		}

		[TestFixture]
		class Tests
		{
			MockDeviceInfo mockDeviceInfo;

			[SetUp]
			public void Setup()
			{
				DeviceInfo.SetCurrent(mockDeviceInfo = new MockDeviceInfo());
			}

			[TearDown]
			public void TearDown()
			{
				DeviceInfo.SetCurrent(null);
			}

			[Ignore("[Bug] Types that require conversion don't work in OnPlatform: https://github.com/xamarin/Microsoft.Maui.Controls/issues/13830")]
			[TestCase(true)]
			[TestCase(false)]
			public void OnPlatformFontConversion(bool useCompiledXaml)
			{
				mockDeviceInfo.Platform = DevicePlatform.iOS;
				var layout = new Bz55343(useCompiledXaml);
				Assert.That(layout.label0.FontSize, Is.EqualTo(16d));
				Assert.That(layout.label1.FontSize, Is.EqualTo(64d));
			}
		}
	}
}