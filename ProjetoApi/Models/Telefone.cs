using ProjetoApi.Models.Enums;

namespace ProjetoApi.Models
{
    public class Telefone
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public TipoTelefone Tipo { get; set; }
        public bool Deletado { get; set; } = false;
    }
}