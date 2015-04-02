namespace Navigator.ProviderModel
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;

  public class FileAndFolderProvider : IProvider
  {
    private readonly char[] querySeparators = {' ', '\\'};
    private List<DirectoryInfo> directoriesInContext = new List<DirectoryInfo>();

    public IEnumerable<NavigationElement> GetElements(string query)
    {
      directoriesInContext.Clear();
      var parts = query.Split(querySeparators, StringSplitOptions.None);

      for (int i = 0; i < parts.Length; i++)
      {
        var currentContext = new List<DirectoryInfo>();
        var part = parts[i];
        bool last = i + 1 == parts.Length;

        // is there anything in context
        if (directoriesInContext.Any())
        {
          var innerDirs = directoriesInContext.SelectMany(d => d.GetDirectoriesSafe());
          bool hasExactMatch = false;
          foreach (var innerDir in innerDirs)
          {
            if (innerDir.Name.DirectoryNameMatchesInput(part))
            {
              if (innerDir.Name.Equals("part"))
                hasExactMatch = true;
              if (last)
                yield return new FolderNavigationElement(innerDir.FullName);
              else
              {
                currentContext.Add(innerDir);
              }
            }
          }

          // if no exact match, offer to create directory as well
          if (!hasExactMatch && !string.IsNullOrEmpty(part) && last
            && !part.HasWildcardCharacter())
          {
            foreach (var dir in directoriesInContext)
            {
              string path = Path.Combine(dir.FullName, part);
              yield return new CreateFolderElement(path);
            }
          }
        }
        else
        {
          // we're at the root level, so go looking for folders
          var drives = Environment.GetLogicalDrives();
          foreach (var drive in drives)
          {
            if (drive.DriveNameMatchesInput(part))
            {
              // if last, yield a folder navigator
              if (last)
                yield return new FolderNavigationElement(drive);
              else
              {
                // just add it to the context
                currentContext.Add(new DirectoryInfo(drive));
              }
            }
          }
        }

        // maybe a file in this context
        foreach (var file in directoriesInContext.SelectMany(d => d.GetFilesSafe()))
        {
          if (file.Name.FileNameMatchesInput(part))
          {
            yield return new FileNavigationElement(file.FullName);
          }
        }

        directoriesInContext = currentContext;
      }

      
    }
  }
}