using ProjetoApi.Models.Enums;

namespace ProjetoApi.Models
{
    public class ProjetoLei
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TiposProjetosLei TiposProjetosLei { get; set; }
        public string Ementa { get; set; }
        public virtual Politico Autor { get; set; }
        public StatusProjetoLei StatusProjetoLei { get; set; }
        public bool Deletado { get; set; } = false;
    }
}