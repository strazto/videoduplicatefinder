using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace VideoDuplicateFinder.gui
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
			base.Initialize();
		}


		public override void OnFrameworkInitializationCompleted() {
			if (!(ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)) {
				throw new PlatformNotSupportedException();
			}

			desktop.MainWindow = new MainWindow();

			base.OnFrameworkInitializationCompleted();
		}

		public static void AttachDevTools(Window window) {
#if DEBUG
			DevToolsExtensions.AttachDevTools(window);
#endif
		}
	}
}
