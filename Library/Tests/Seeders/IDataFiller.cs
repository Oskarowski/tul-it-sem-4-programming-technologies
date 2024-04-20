using DataLayer.API;

namespace Tests
{
    public interface IDataFiller
    {
        void Fill(IDataContext context);
    }
}