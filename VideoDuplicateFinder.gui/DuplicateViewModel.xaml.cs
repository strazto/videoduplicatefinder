using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace VideoDuplicateFinder.gui
{
    public class DuplicateViewModel : UserControl
    {
        public DuplicateViewModel()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
