const URL_API = "http://localhost:5117/api/Material";

const formularioMaterial = document.querySelector("#form-material");
const campoTitulo = document.querySelector("#titulo");
const campoAno = document.querySelector("#ano");
const campoAutor = document.querySelector("#autor");
const campoPaginas = document.querySelector("#paginas");
const campoEdicao = document.querySelector("#edicao");
const campoMes = document.querySelector("#mes");
const campoTipo = document.querySelector("#tipo");
const camposLivro = document.querySelector("#campos-livro");
const camposRevista = document.querySelector("#campos-revista");
const tabelaMateriais = document.querySelector("#tabela-materiais");
const mensagem = document.querySelector("#mensagem");





function mostrarMensagem(texto, tipo) {
    mensagem.textContent = texto;
    mensagem.classList.remove("d-none", "alert-success", "alert-danger")
    mensagem.classList.add(`alert-${tipo}`)
}

function carregarMateriais() {
    fetch(URL_API)
        .then(function (resposta) {
            if (!resposta.ok) {
                throw new Error("Não foi possível consultar a API");

            }
            return resposta.json();
        })

        .then(function (materiais) {
            tabelaMateriais.innerHTML = "";

            materiais.forEach(function (material) {
                const linha = document.createElement("tr");

                linha.innerHTML = `
            <td>${material.id}</td>
            <td>${material.tipo}</td>
            <td>${material.descricao}</td>
            <td class="text-end">
            <button class="btn btn-danger btn-sm btn-excluir">Excuir</button>
            </td>
            `;
                linha.querySelector(".btn-excluir").addEventListener("click", function(){
                    excluirMaterial(material.id);
                })
                tabelaMateriais.appendChild(linha);
            })
        })

        .catch(function (erro) {
            tabelaMateriais.innerHTML = `<tr><td colspan="4">Não foi possível carregar.</td></tr>`;
            mostrarMensagem("Erro ao buscar materiais: " + erro.message, "danger")
        })
}

carregarMateriais();


function atualizarCampos() {
    if (campoTipo.value === "livro") {
        camposLivro.classList.remove("d-none");
        camposRevista.classList.add("d-none");
    } else {
        camposLivro.classList.add("d-none");
        camposRevista.classList.remove("d-none");
    }
}


campoTipo.addEventListener("change", atualizarCampos)



function excluirMaterial(id) {
    if (!confirm(`Excluir o material ${id}?`)){
        return;
    }
    fetch(`${URL_API}/${id}`, { method: "DELETE" })
        .then(function(resposta){
            if (!resposta.ok){
                throw new Error("Não foi possível excluir.");
            }
            return resposta.text();
        })
        .then(function(texto){
            mostrarMensagem(texto, "success");
            carregarMateriais();
        })
        .catch(function(erro){
            mostrarMensagem("Erro ao excluir: "+ erro.message, "danger");
        })
}



formularioMaterial.addEventListener("submit", function (evento) {
    evento.preventDefault();

    const tipo = campoTipo.value;
    const titulo = campoTitulo.value.trim();

    let novoMaterial;


    if (tipo === "livro") {
        novoMaterial = {
            titulo: titulo,
            anoPublicacao: Number(campoAno.value),
            autor: campoAutor.value.trim(),
            paginas: Number(campoPaginas.value)
        };
    } else {
        novoMaterial = {
            titulo: titulo,
            anoPublicacao: Number(campoAno.value),
            edicao: Number(campoEdicao.value),
            mes: Number(campoMes.value)
        };
    }


    fetch(`${URL_API}/${tipo}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(novoMaterial)
    })
        .then(function (resposta) {
            if (!resposta.ok) {
                throw new Error("Não foi possível cadastrar.");

            }
            return resposta.json();
        })
        .then(function (criado) {
            mostrarMensagem(`"${criado.titulo}" cadastrado com o id ${criado.id}.`, "success");
            formularioMaterial.reset();
            carregarMateriais();
            atualizarCampos();
        })
        .catch(function (erro) {
            mostrarMensagem("Erro ao cadastrar: " + erro.message, "danger");
        })
})

