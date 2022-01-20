using ProjetoApi.Models.Enums;

namespace ProjetoApi.Models
{
    public class Processo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual Politico Politico { get; set; }
        public string Descricao { get; set; }
        public StatusProcesso Status { get; set; }
        public bool Deletado { get; set; } = false;
    }
}