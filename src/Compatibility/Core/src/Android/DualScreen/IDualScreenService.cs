using System;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;

namespace Microsoft.Maui.Controls.DualScreen.Android
{
	internal interface IDualScreenService
	{
		event EventHandler OnScreenChanged;
		bool IsSpanned { get; }
		bool IsLandscape { get; }
		Rect GetHinge();
		Size ScaledScreenSize { get; }
		Point? GetLocationOnScreen(VisualElement visualElement);
		object WatchForChangesOnLayout(VisualElement visualElement, Action action);
		void StopWatchingForChangesOnLayout(VisualElement visualElement, object handle);
		Task<int> GetHingeAngleAsync();
		bool IsDualScreenDevice { get; }
	}
}
