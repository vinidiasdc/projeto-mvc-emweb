// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.querySelectorAll('th').forEach(function (el) {
    el.classList.add('align-middle');
});

document.querySelectorAll('td').forEach(function (el) {
    el.classList.add('align-middle');
});


const exampleModal = document.getElementById('modal-excluir')
exampleModal.addEventListener('show.bs.modal', event => {

    const button = event.relatedTarget

    const recipient = button.getAttribute('data-bs-whatever')

    let arrayAluno = recipient.split(',')

    let matricula = arrayAluno[0]
    let nome = arrayAluno[1]
    let sexo = arrayAluno[2]
    let nascimento = arrayAluno[3]
    let cpf = arrayAluno[4]

    let spanMAT = document.getElementById("matricula")
    let spanNOME = document.getElementById("nome")
    let spanSEXO = document.getElementById("sexo")
    let spanNASC = document.getElementById("nascimento")
    let spanCPF = document.getElementById("cpf")

    spanMAT.textContent = matricula
    spanNOME.textContent = nome
    spanSEXO.textContent = sexo
    spanNASC.textContent = nascimento
    spanCPF.textContent = cpf

    const modalTitle = exampleModal.querySelector('.modal-title')
    const modalBodyInput = exampleModal.querySelector('.modal-body input')
    const btn_deletar = document.querySelector("a#btn-excluir-permanente")
    btn_deletar.setAttribute('href', 'Alunos/Delete/' + matricula)

    modalBodyInput.value = recipient
})


function mascara(i) {

    var v = i.value;

    if (isNaN(v[v.length - 1])) { // impede entrar outro caractere que não seja número
        i.value = v.substring(0, v.length - 1);
        return;
    }

    i.setAttribute("maxlength", "14");
    if (v.length == 3 || v.length == 7) i.value += ".";
    if (v.length == 11) i.value += "-";

}

