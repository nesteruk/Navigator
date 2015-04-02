using System.Windows.Media;

namespace Navigator.ProviderModel
{
  /// <summary>
  /// 
  /// </summary>
  public class FolderNavigationElement : NavigationElement
  {
    public string Path { get; set; }

    public FolderNavigationElement(string path)
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