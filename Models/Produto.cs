namespace MeuSiteMVC.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public List<Cores> Cores { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

    }

    public class Carrinho
    {
        public int Id { get; set; }
        public List<Produto> Produtos { get; set; }
    }

    public class Cores
    {
        public string Azul { get; set; }
        public string Preto { get; set; }
        public string Laranja { get; set; }
    }
}
