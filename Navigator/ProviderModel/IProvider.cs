using System.IO;

namespace Navigator.ProviderModel
{
  using System.Collections.Generic;

  public interface IProvider
  {
    IEnumerable<NavigationElement> GetElements(string query);
  }
}