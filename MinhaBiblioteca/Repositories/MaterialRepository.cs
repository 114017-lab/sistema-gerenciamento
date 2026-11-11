using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
public class MaterialRepository : IMaterialRepository
{
    private Dictionary<int, MaterialBiblioteca> _materialbiblioteca = new Dictionary<int, MaterialBiblioteca>()
    {
        {1, new Livro { Id = 1, Autor = "Lebron James",  Paginas = 5}},
        {2, new Revista { Id = 2, Edicao = 1 , Mes = 6 }},
    };

    public IEnumerable<MaterialBiblioteca> ObterTodos()
    {
        return _materialbiblioteca.Values;
    }

    public MaterialBiblioteca? ObterPorId(int id)
    {
        if (!_materialbiblioteca.ContainsKey(id))
        {
            return null;
        }
        return _materialbiblioteca[id];
    }

    public MaterialBiblioteca? Adicionar(MaterialBiblioteca material)
    {
        
    }


    public bool Remover(int id)
    {
        if (!_materialbiblioteca.ContainsKey(id))
        {
            return false;
        }
        _materialbiblioteca.Remove(id);
        return true;
    }
}
