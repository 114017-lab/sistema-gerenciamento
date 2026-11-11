public abstract class MaterialBiblioteca
{
    //Atributos
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public int AnoPublicacao { get; set; }

    //Metodos
    public abstract int CalcularPrazo();

    public virtual string Descricao()
    {
        return $"Titulo: {Titulo}";
    }
}