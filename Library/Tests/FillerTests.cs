using DataLayer.API;
using DataLayer.Implementations;

namespace Tests;

[TestClass]
public class FillerTests
{
    [TestMethod]
    public void PredefinedFillerTests()
    {
        IDataContext context = new DataContext();
        IDataFiller filler = new PresetFiller();

        filler.Fill(context);

        Assert.AreEqual(5, context.Users.Count);
        Assert.AreEqual(5, context.Products.Count);
        Assert.AreEqual(5, context.States.Count);
        Assert.AreEqual(11, context.Events.Count);
    }

    [TestMethod]
    public void RandomFillerTests(){
        IDataContext context = new DataContext();
        IDataFiller filler = new RandomFiller();

        filler.Fill(context);

        Assert.ThrowsException<ArgumentException>(() => {RandomFiller.GetRandomNumber<int>(0);});
        Assert.AreEqual(15, RandomFiller.GetRandomString(15).Length);
    }
}
