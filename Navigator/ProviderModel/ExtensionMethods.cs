namespace Navigator.ProviderModel
{
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Text.RegularExpressions;

  public static class ExtensionMethods
  {
    private static readonly Dictionary<string, DirectoryInfo[]> innerDirectories = new Dictionary<string, DirectoryInfo[]>(); 
    private static readonly Dictionary<string, FileInfo[]> innerFiles = new Dictionary<string, FileInfo[]>();

    public static StringBuilder Append(this StringBuilder sb, string text, bool condition)
    {
      if (condition)
        sb.Append(text);
      return sb;
    }

    public static bool HasWildcardCharacter(this string s)
    {
      return s.Any(c => c.IsWildcardCharacter());
    }

    public static bool IsWildcardCharacter(this char c)
    {
      return c == '*' || c == '?';
    }

    public static string WildcardToRegex(string pattern)
    {
      return "^" + Regex.Escape(pattern).
                         Replace(@"\*", ".*").
                         Replace(@"\?", ".") + "$";
    }

    public static bool IsFoundIn(this string @this, string foundInThis)
    {
      if (string.IsNullOrWhiteSpace(@this) ||
          string.IsNullOrWhiteSpace(foundInThis))
        return false;

      // every two characters that are neither * nor ?
      // should have a * between them
      var sb = new StringBuilder();
      if (!@this[0].IsWildcardCharacter())
        sb.Append("*");
      for (int i = 0; i < @this.Length - 1; ++i)
      {
        sb.Append(@this[i]);
        if (!@this[i].IsWildcardCharacter() &&
            !@this[i + 1].IsWildcardCharacter())
        {
          sb.Append('*');
        }
      }
      char last = @this[@this.Length - 1];
      sb.Append(last).Append("*", !last.IsWildcardCharacter());

      // now wildcard it more and check
      var pattern = WildcardToRegex(sb.ToString());
      return Regex.IsMatch(foundInThis, pattern, RegexOptions.IgnoreCase);
    }

    public static bool DriveNameMatchesInput(this string driveName,
      string input)
    {
      return input.IsFoundIn(driveName);
    }

    public static bool DirectoryNameMatchesInput(
      this string dirName, string input)
    {
      // a blank directory returns 'true' because we want
      // a listing
      if (string.IsNullOrWhiteSpace(input))
        return true; // oops

      return input.IsFoundIn(dirName);
    }
    
    public static bool FileNameMatchesInput(
      this string dirName, string input)
    {
      if (string.IsNullOrEmpty(input))
        return false; // otherwise, you get every file under the sun
      // hack
      return dirName.ToUpperInvariant().Contains(input.ToUpperInvariant());
    }

    public static FileInfo[] GetFilesSafe(this DirectoryInfo info)
    {
      if (innerFiles.ContainsKey(info.FullName))
        return innerFiles[info.FullName];

      FileInfo[] files;
      try
      {
        files = info.GetFiles();
        innerFiles.Add(info.FullName, files);
      }
      catch
      {
        files = new FileInfo[]{};
      }
      return files;
    }

    public static DirectoryInfo[] GetDirectoriesSafe(this DirectoryInfo info)
    {
      if (innerDirectories.ContainsKey(info.FullName))
        return innerDirectories[info.FullName];

      DirectoryInfo[] dirs;

      try
      {
        dirs = info.GetDirectories();
        innerDirectories.Add(info.FullName, dirs);
      }
      catch
      {
        dirs = new DirectoryInfo[] { };
      }
      return dirs;
    }
  }
}