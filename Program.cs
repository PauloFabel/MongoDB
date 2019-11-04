using CadastroDePessoaMongoDB.Classes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CadastroDePessoaMongoDB
{
    class Program
    {
        static MongoClient client = new MongoClient(
                ConfigurationManager.AppSettings["ConexaoPessoa"]);

        static IMongoDatabase db = client.GetDatabase("DBPessoa");

        static IMongoCollection<Pessoa> cadastroPessoa = db.GetCollection<Pessoa>("Cadastros");

        static void Main(string[] args)
        {
            int resp = 0;


            Console.WriteLine("Incluindo Cadastros no Mongo DB");
            do
            {
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("--Digite a opcao desejada:------------------");
                Console.WriteLine("--Para inserir um registro digite 1:--------");
                Console.WriteLine("--Para atualizar um registro digite 2:------");
                Console.WriteLine("--Para deletar um registro digite 3:--------");
                Console.WriteLine("--Para ver todos registros digite 4:--------");
                Console.WriteLine("--Para sair digite 0:-----------------------");
                Console.WriteLine("--------------------------------------------");
                int op = Int32.Parse(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        Console.WriteLine("--------------------------------------------\n");
                        Pessoa pessoa = new Pessoa();
                        Console.WriteLine("Digite o nome que deseja inserir : ");
                        pessoa.Nome = Console.ReadLine();
                        Console.WriteLine("Digite a cidade que deseja inserir : ");
                        pessoa.Cidade = Console.ReadLine();
                        Console.WriteLine("Digite o Estado que deseja inserir : ");
                        pessoa.Estado = Console.ReadLine();
                        CreateOne(pessoa);
                        break;
                    case 2:
                        Console.WriteLine("Digite o nome que voce deseja alterar: ");
                        string name1 = Console.ReadLine();
                        Console.WriteLine("Digite o novo nome: ");
                        string name2 = Console.ReadLine();
                        UpdateOne(name1, name2);
                        break;
                    case 3:
                        Console.WriteLine("Digite o nome que voce deseja deletar: ");
                        string name3 = Console.ReadLine();
                        DeleteOne(name3);
                        break;
                    case 4:
                        Console.WriteLine("Buscando todos registros : ");
                        Console.WriteLine("--------------------------------------------------------------");
                        ReadAll();
                        break;
                    default:
                        Console.WriteLine("Terminando o projeto");
                        resp = 1;
                        break;

                }
            } while (resp == 0);

            Console.WriteLine("Finalizando o processo:");

        }

        public static void ReadAll()
        {
            List<Pessoa> list = cadastroPessoa.AsQueryable().ToList<Pessoa>();
            var pessoas = from s in list select s;
            Console.WriteLine("Todos os cadastros deste documento são:\n");
            foreach (Pessoa p in pessoas)
            {

                Console.WriteLine(" \tNome: " + p.Nome + " | Cidade: " + p.Cidade + " | Estado: " + p.Estado);
            }
        }

        public static void UpdateOne(string name1, string name2)
        {
            var updateDef = Builders<Pessoa>.Update.Set(p => p.Nome, name2);
            cadastroPessoa.UpdateOne(p => p.Nome == name1, updateDef);
        }

        public static void DeleteOne(string name3)
        {
            cadastroPessoa.DeleteOne(s => s.Nome == name3);
            Console.WriteLine("Cadastro Removido : " + name3);

        }

        public static void CreateOne(Pessoa pessoa)
        {
            cadastroPessoa.InsertOne(pessoa);
        }
    }
}
