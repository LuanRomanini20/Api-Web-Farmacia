using apiwebfarmacia.Data;
using apiwebfarmacia.Models;
using apiwebfarmacia.Controllers;
using Microsoft.EntityFrameworkCore;


namespace testapiwebfarmacia
{
    public class UnitTest1
    {

        private DbContextOptions<apiwebfarmaciaContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            //Precisa instalar a dependencia pelo nuget: Microsoft.EntityFrameworkCore.InMemory
            options = new DbContextOptionsBuilder<apiwebfarmaciaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new apiwebfarmaciaContext(options))
            {
                context.remedio.Add(new remedio { id = 1, description = "Puran T4", valor = 40, datacadastro = "23-10-2023", datavalidade = "23-10-2025" });
                context.remedio.Add(new remedio { id = 2, description = "Puran T5", valor = 45, datacadastro = "23-10-2024", datavalidade = "23-10-2025" });
                context.remedio.Add(new remedio { id = 3, description = "Puran T6", valor = 50, datacadastro = "23-10-2025", datavalidade = "23-10-2027" });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new apiwebfarmaciaContext(options))
            {
                remediosController remediosController = new remediosController(context);
                IEnumerable<remedio> clients = remediosController.Getremedio().Result.Value;
                Assert.Equal(3, clients.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new apiwebfarmaciaContext(options))
            {
                int clientId = 2;
                remediosController remedioController = new remediosController(context);
                remedio remedio = remedioController.Getremedio(clientId).Result.Value;
                Assert.Equal(2, remedio.id);
            }
        }

        [Fact]
        public async void Create()
        {
            InitializeDataBase();

            remedio remedio = new remedio()
            {
                id = 4,
                description = "Puran T8",
                valor = 66,
                datacadastro = "20-03-2023",
                datavalidade = "20-03-2030"
            };

            // Use a clean instance of the context to run the test
            using (
                var context = new apiwebfarmaciaContext(options))
            {
                remediosController remedioController = new remediosController(context);
                await remedioController.Postremedio(remedio);
                remedio remedioReturn = remedioController.Getremedio(4).Result.Value;
                Assert.Equal("Puran T8", remedioReturn.description);
            }
        }

        [Fact]
        public async void Update()
        {
            InitializeDataBase();

            remedio remedio = new remedio()
            {
                id = 2,
                description = "Puran T18",
                valor = 78,
                datacadastro = "20-03-2020",
                datavalidade = "20-03-2025"
            };

            // Use a clean instance of the context to run the test
            using (var context = new apiwebfarmaciaContext(options))
            {
                remediosController remedioController = new remediosController(context);
                await remedioController.Putremedio(2, remedio);
                remedio remedioReturn = remedioController.Getremedio(2).Result.Value;
                Assert.Equal("Puran T18", remedioReturn.description);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new apiwebfarmaciaContext(options))
            {
                remediosController remediosController = new remediosController(context);
                remedio remedio = remediosController.Deleteremedio(2).Result.Value;
                Assert.Null(remedio);
            }
        }

    }
}