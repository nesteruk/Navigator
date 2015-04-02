using System.Windows.Media;

namespace Navigator.ProviderModel
{
  public abstract class NavigationElement
  {
    public virtual int Priority { get { return 100; } }
    public abstract Color TextColor { get; }
    public abstract DrawingImage Icon { get; }
    public abstract string Text { get; }
  }
}