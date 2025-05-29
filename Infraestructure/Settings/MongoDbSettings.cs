namespace FilmesApi.Settings
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string FilmesCollectionName { get; set; }
    }
}
