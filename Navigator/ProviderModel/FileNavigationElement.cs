using System.Windows.Media;

namespace Navigator.ProviderModel
{
  /// <summary>
  /// Represents a file. Default action opens said file, submenu lets you pick additional
  /// options.
  /// </summary>
  class FileNavigationElement : NavigationElement
  {
    public string Path { get; set; }

    public FileNavigationElement(string path)
    {
      Path = path;
    }

    public override Color TextColor
    {
      get { throw new System.NotImplementedException(); }
    }

    public override DrawingImage Icon
    {
      get { throw new System.NotImplementedException(); }
    }

    public override string Text
    {
      get { return Path; }
    }
  }
}