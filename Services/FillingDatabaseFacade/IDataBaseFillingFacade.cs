namespace StockLens.Services.FileReaderFacade
{
    public interface IDataBaseFillingFacade
    {
        public Task ReadJsonFile(IFormFile jsonFile);
    }
}
