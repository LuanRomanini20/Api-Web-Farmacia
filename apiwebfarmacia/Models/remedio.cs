namespace apiwebfarmacia.Models
{

    //Remédio:
//Id, descrição, valor, datacadastro, data validade.
    public class remedio
    {

        public int id { get; set; }
        public string description { get; set; }
        public int valor { get; set; }
        public string datacadastro { get; set; }
        public string datavalidade { get; set; }

    }
}
