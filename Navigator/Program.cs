using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Navigator.Annotations;
using WinJump;

namespace Navigator
{
  public class Person : IEquatable<Person>, INotifyPropertyChanged
  {
    public bool Equals(Person other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return id == other.id && string.Equals(Name, other.Name) && Age == other.Age;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Person) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        int hashCode = id;
        hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
        hashCode = (hashCode*397) ^ Age;
        return hashCode;
      }
    }

    public static bool operator ==(Person left, Person right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Person left, Person right)
    {
      return !Equals(left, right);
    }

    private int id;
    private string name;

    public string Name
    {
      get { return name; }
      set
      {
        if (value == name) return;
        name = value;
        OnPropertyChanged();
      }
    }

    public int Age { get; set; }

    public Person(string name, int age)
    {
      Name = name;
      Age = age;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }

  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      var app = new App();
      var jumper = Jumper.Instance;
      Application.Run();
      jumper = null;
    }
  }
}
