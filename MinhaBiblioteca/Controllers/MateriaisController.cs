using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]

public class MaterialController : ControllerBase
{
    private IMaterialRepository _repository;

    public MaterialController(IMaterialRepository repository)
    {
        _repository = repository;
    }


    [HttpGet]

    public IActionResult Get()
    {
        var resultado = _repository.ObterTodos().Select(material =>
        {
            return new
            {
                material.Id,
                Tipo = material.GetType().Name,
                Descricao = material.Descricao(),
            };
        });
        return Ok(resultado);
    }

    [HttpPost("livro")]
    public IActionResult PostLivro([FromBody] Livro livro)
    {
        var novoLivro = _repository.Adicionar(livro);
        return Ok(novoLivro);
    }

    [HttpPost("revista")]
    public IActionResult PostRevista([FromBody] Revista revista)
    {
        var novaRevista = _repository.Adicionar(revista);
        return Ok(novaRevista);
    }

    [HttpDelete("{id}")]

    public IActionResult Delete(int id)
    {
        bool removida = _repository.Remover(id);

        if (!removida)
        {
            return NotFound("Material nao encontrado");
        }

        return Ok("Material excluido com sucesso!");
    }

    [HttpPost("{id}/Emprestar")]
    public IActionResult Emprestar(int id, [FromQuery] bool Disponivel)
    {
        var material = _repository.ObterPorId(id);

        if (material == null)
        {
            return NotFound("Material nao encontrado");
        }
        if (material is not IEmprestimo itemRecuperavel)
        {
            return BadRequest($"O material '{material.Titulo}' do tipo '{material.GetType().Name}' nao pode ser emprestado");
        }
        if (!itemRecuperavel.Emprestar(Disponivel))
        {
            return BadRequest($"O livro '{material.Titulo}' nao esta disponivel");
        }

        return Ok(new
        {
            mensagem = "Emprestimo realizado com sucesso",
            titulo = material.Titulo,
        });
    }
}