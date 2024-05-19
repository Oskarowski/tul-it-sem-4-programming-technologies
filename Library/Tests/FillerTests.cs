using DataLayer.API;
using DataLayer.Implementations;
using Tests.Seeders;

namespace Tests;

[TestClass]
public class FillerTests
{
    [TestMethod]
    public void PredefinedFillerTests()
    {
        IDataRepository repository = DataRepository.NewInstance(DataContext.NewInstance());

        repository.Seed(new PresetFiller());

        Assert.AreEqual(5, repository.GetAllUsers().Count);
        Assert.AreEqual(5, repository.GetAllProducts().Count);
        Assert.AreEqual(5, repository.GetAllStates().Count);
        Assert.AreEqual(11, repository.GetAllEvents().Count);
    }

    [TestMethod]
    public void RandomFillerTests()
    {
        IDataRepository repository = DataRepository.NewInstance(DataContext.NewInstance());
        repository.Seed(new RandomFiller());

        Assert.ThrowsException<ArgumentException>(() => {RandomFiller.GetRandomNumber<int>(0);});
        Assert.AreEqual(15, RandomFiller.GetRandomString(15).Length);
    }
}
