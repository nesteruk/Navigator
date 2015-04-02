using Navigator.ProviderModel;
using NUnit.Framework;

namespace Navigator.Tests
{
  [TestFixture]
  public class StringTests
  {
    [Test]
    [TestCase("a", "a")]
    [TestCase("a", "abc")]
    [TestCase("fb", "FooBar")]
    [TestCase("*.dll", "Fun.dll")]
    [TestCase("d", "ada")]
    public void StringsFoundInTests(string @this, string foundInThis)
    {
      Assert.That(@this.IsFoundIn(foundInThis));
    }

    [Test]
    [TestCase("p*", "abc")]
    public void StringNotFoundInTest(string @this, string notFoundInThis)
    {
      Assert.That(@this.IsFoundIn(notFoundInThis), Is.Not.True);
    }
  }
}
