// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function ContarCaracteres(palabra)
{
    let result = [];
    let palabraList = [...palabra];

    let palabraSet = new Set(palabraList);

    for (let letraSet of palabraSet) {
        let count = palabraList.filter(letra => letra == letraSet).length;
        result.push({ letra: letraSet,  veces: count});
    }
    
    return result;
}


function RenderizarListaCaracteres(tbodyId, palabra)
{
    let listaElemento = document.querySelector(`#${tbodyId}`);

    listaElemento.innerHTML = '';

    let contaCaracteresList = ContarCaracteres(palabra);

    for (let elem of contaCaracteresList) {
        let fila = document.createElement("tr");

        let celdaLetra = document.createElement("td");
        let celdaVeces = document.createElement("td");

        celdaLetra.innerText = elem.letra;
        celdaVeces.innerText = elem.veces;

        fila.appendChild(celdaLetra);
        fila.appendChild(celdaVeces);

        listaElemento.appendChild(fila);
    }

}