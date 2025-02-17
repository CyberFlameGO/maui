using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Maui.Controls.Compatibility.Internals;
using Microsoft.Maui.Controls.Compatibility.Platform.Tizen.Native;
using Microsoft.Maui.Controls.Compatibility.PlatformConfiguration.TizenSpecific;
using Microsoft.Maui.Controls.Compatibility.Shapes;
using Microsoft.Maui.Controls.Compatibility.Xaml.Internals;

namespace Microsoft.Maui.Controls.Compatibility.Platform.Tizen
{
	public class StaticRegistrar<TRegistrable> where TRegistrable : class
	{
		readonly Dictionary<Type, Func<TRegistrable>> _handlers = new Dictionary<Type, Func<TRegistrable>>();

		public void Register(Type tview, Func<TRegistrable> renderer)
		{
			if (renderer == null)
				return;

			_handlers[tview] = renderer;
		}

		public TOut GetHandler<TOut>(Type type, params object[] args) where TOut : class, TRegistrable
		{
			if (LookupHandler(type, out Func<TRegistrable> renderer))
			{
				return (TRegistrable)renderer() as TOut;
			}
			Log.Error("No handler could be found for that type :" + type);
			return null;

		}

		public bool LookupHandler(Type viewType, out Func<TRegistrable> handler)
		{
			while (viewType != null && viewType != typeof(Element))
			{
				if (_handlers.TryGetValue(viewType, out handler))
					return true;

				viewType = viewType.GetTypeInfo().BaseType;
			}
			handler = null;
			return false;
		}

		public TOut GetHandlerForObject<TOut>(object obj) where TOut : class, TRegistrable
		{
			return GetHandlerForObject<TOut>(obj, null);
		}

		public TOut GetHandlerForObject<TOut>(object obj, params object[] args) where TOut : class, TRegistrable
		{
			var reflectableType = obj as IReflectableType;
			var type = reflectableType != null ? reflectableType.GetTypeInfo().AsType() : obj.GetType();
			return GetHandler<TOut>(type, args);
		}
	}

	public static class StaticRegistrar
	{
		public static StaticRegistrar<IRegisterable> Registered { get; internal set; }

		static StaticRegistrar()
		{
			Registered = new StaticRegistrar<IRegisterable>();
		}

		public static void RegisterHandlers(Dictionary<Type, Func<IRegisterable>> customHandlers)
		{
			//Renderers
			if (Forms.UseFastLayout)
			{
				Registered.Register(typeof(Layout), () => new FastLayoutRenderer());
			}
			else
			{
				Registered.Register(typeof(Layout), () => new LayoutRenderer());
			}
			Registered.Register(typeof(ScrollView), () => new ScrollViewRenderer());
			Registered.Register(typeof(CarouselPage), () => new CarouselPageRenderer());
			Registered.Register(typeof(Page), () => new PageRenderer());
			Registered.Register(typeof(NavigationPage), () => new NavigationPageRenderer());
#pragma warning disable CS0618 // Type or member is obsolete
			Registered.Register(typeof(MasterDetailPage), () => new MasterDetailPageRenderer());
#pragma warning restore CS0618 // Type or member is obsolete
			Registered.Register(typeof(FlyoutPage), () => new FlyoutPageRenderer());
			Registered.Register(typeof(TabbedPage), () => new TabbedPageRenderer());
			Registered.Register(typeof(Label), () => new LabelRenderer());
			Registered.Register(typeof(Button), () => new ButtonRenderer());
			Registered.Register(typeof(Image), () => new ImageRenderer());
			Registered.Register(typeof(Slider), () => new SliderRenderer());
			Registered.Register(typeof(Picker), () => new PickerRenderer());
			Registered.Register(typeof(Frame), () => new FrameRenderer());
			Registered.Register(typeof(Stepper), () => new StepperRenderer());
			Registered.Register(typeof(DatePicker), () => new DatePickerRenderer());
			Registered.Register(typeof(TimePicker), () => new TimePickerRenderer());
			Registered.Register(typeof(ProgressBar), () => new ProgressBarRenderer());
			Registered.Register(typeof(Switch), () => new SwitchRenderer());
			Registered.Register(typeof(CheckBox), () => new CheckBoxRenderer());
			Registered.Register(typeof(ListView), () => new ListViewRenderer());
			Registered.Register(typeof(BoxView), () => new BoxViewRenderer());
			Registered.Register(typeof(ActivityIndicator), () => new ActivityIndicatorRenderer());
			Registered.Register(typeof(SearchBar), () => new SearchBarRenderer());
			Registered.Register(typeof(Entry), () => new EntryRenderer());
			Registered.Register(typeof(Editor), () => new EditorRenderer());
			Registered.Register(typeof(TableView), () => new TableViewRenderer());
			Registered.Register(typeof(NativeViewWrapper), () => new NativeViewWrapperRenderer());
			Registered.Register(typeof(WebView), () => new WebViewRenderer());
			Registered.Register(typeof(ImageButton), () => new ImageButtonRenderer());
			Registered.Register(typeof(StructuredItemsView), () => new StructuredItemsViewRenderer());
			Registered.Register(typeof(CarouselView), () => new CarouselViewRenderer());
			Registered.Register(typeof(SwipeView), () => new SwipeViewRenderer());
			Registered.Register(typeof(RefreshView), () => new RefreshViewRenderer());
			Registered.Register(typeof(IndicatorView), () => new IndicatorViewRenderer());
			Registered.Register(typeof(RadioButton), () => new RadioButtonRenderer());

			if (DeviceInfo.Idiom == DeviceIdiom.Watch)
			{
				Registered.Register(typeof(Shell), () => new Watch.ShellRenderer());
			}
			else if (DeviceInfo.Idiom == DeviceIdiom.TV)
			{
				Registered.Register(typeof(Shell), () => new TV.TVShellRenderer());
			}
			else
			{
				Registered.Register(typeof(Shell), () => new ShellRenderer());
			}

			//ImageSourceHandlers
			Registered.Register(typeof(FileImageSource), () => new FileImageSourceHandler());
			Registered.Register(typeof(StreamImageSource), () => new StreamImageSourceHandler());
			Registered.Register(typeof(UriImageSource), () => new UriImageSourceHandler());

			//Cell Renderers
			Registered.Register(typeof(TextCell), () => new TextCellRenderer());
			Registered.Register(typeof(ImageCell), () => new ImageCellRenderer());
			Registered.Register(typeof(SwitchCell), () => new SwitchCellRenderer());
			Registered.Register(typeof(EntryCell), () => new EntryCellRenderer());
			Registered.Register(typeof(ViewCell), () => new ViewCellRenderer());

			//Font Loaders
			Registered.Register(typeof(EmbeddedFont), () => new EmbeddedFontLoader());

			//Dependencies
			DependencyService.Register<ISystemResourcesProvider, ResourcesProvider>();
			DependencyService.Register<IDeserializer, Deserializer>();
			DependencyService.Register<INativeBindingService, NativeBindingService>();
			DependencyService.Register<INativeValueConverterService, NativeValueConverterService>();

			//SkiaSharp Renderers
			if (Forms.UseSkiaSharp)
			{
				// Register all skiasharp-based rednerers here for StaticRegistrar
				Registered.Register(typeof(Frame), () => new SkiaSharp.FrameRenderer());
				Registered.Register(typeof(BoxView), () => new SkiaSharp.BoxViewRenderer());
				Registered.Register(typeof(Image), () => new SkiaSharp.ImageRenderer());

				Registered.Register(typeof(Ellipse), () => new SkiaSharp.EllipseRenderer());
				Registered.Register(typeof(Line), () => new SkiaSharp.LineRenderer());
				Registered.Register(typeof(Path), () => new SkiaSharp.PathRenderer());
				Registered.Register(typeof(Polygon), () => new SkiaSharp.PolygonRenderer());
				Registered.Register(typeof(Polyline), () => new SkiaSharp.PolylineRenderer());
				Registered.Register(typeof(Shapes.Rectangle), () => new SkiaSharp.RectangleRenderer());
			}

			//Custom Handlers
			if (customHandlers != null)
			{
				foreach (KeyValuePair<Type, Func<IRegisterable>> handler in customHandlers)
				{
					Registered.Register(handler.Key, handler.Value);
				}
			}
		}
	}
}
