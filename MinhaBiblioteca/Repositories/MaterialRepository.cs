public class MaterialRepository : IMaterialRepository
{
    private Dictionary<int, MaterialBiblioteca> _materialbiblioteca = new Dictionary<int, MaterialBiblioteca>()
    {
        {1, new Livro { Id = 1, Titulo = "Minha Vida no Basquete", Autor = "Lebron James",  Paginas = 5}},
        {2, new Revista { Id = 2, Titulo = "Revista Esporte", Edicao = 1 , Mes = 6 }},
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

    public MaterialBiblioteca Adicionar(MaterialBiblioteca material)
    {
        int novoId = _materialbiblioteca.Count == 0 ? 1 : _materialbiblioteca.Keys.Max() + 1;
        material.Id = novoId;
        _materialbiblioteca.Add(novoId, material);
        return material;
    }

    public bool Atualizar(int id, MaterialBiblioteca material)
    {
        if (!_materialbiblioteca.ContainsKey(id))
        {
            return false;
        }
        material.Id = id;
        _materialbiblioteca[id] = material;
        return true;
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
