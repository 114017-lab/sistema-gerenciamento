public class Livro : MaterialBiblioteca, IEmprestimo
{
    public string? Autor { get; set; }
    public int Paginas { get; set; }

    public override int CalcularPrazo()
    {
        return 14;
    }

    public override string Descricao()
    {
        return $"Resumo do livro: {Titulo}";
    }

    public bool Emprestar(bool Disponivel)
    {
        return Disponivel;
    }
}