using MongoDB.Bson;

namespace CadastroDePessoaMongoDB.Classes
{
    class Pessoa
    {
        public ObjectId _id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

    }
}
