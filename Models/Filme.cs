using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Filme
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Titulo { get; set; }
    public string Diretor { get; set; }
    public int Ano { get; set; }
    public string Genero { get; set; }
}
