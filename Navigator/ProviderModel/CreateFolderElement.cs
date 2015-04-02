using System.Windows.Media;

namespace Navigator.ProviderModel
{
  class CreateFolderElement : NavigationElement
  {
    private string path;

    public CreateFolderElement(string path)
    {
      this.path = path;
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
      get { return "Create " + path; }
    }
  }
}