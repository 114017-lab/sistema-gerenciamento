public class Revista : MaterialBiblioteca
{
    public int Edicao { get; set; }
    public int Mes { get; set; }

   public override int CalcularPrazo()
    {
        return 7;
    }

    public override string Descricao()
    {
        return $"Titulo: {Titulo}";
    }
}