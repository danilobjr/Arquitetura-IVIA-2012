/// <reference path="../Main/Namespace.js" />

ArquiteturaDotNet2012.Componente.Tabela = function (idTabela) {

    /// <summary>
    /// Renderiza o plugin DataTables numa tabela HTML.
    /// </summary>
    /// <param name="idTabela" type="string">ID da tabela a ser renderizada.</param>


    // Propriedades 

    this.tabela = null;
    var _idTabela = '#' + idTabela;


    // Construtor

    this.tabela = $(_idTabela).dataTable({
        // Estilo

        "bJQueryUI": true,
        "sPaginationType": "full_numbers",        

        // Internacionalização        

        "oLanguage": {
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "Nada encontrado",
            "sEmptyTable": "Não há registros",
            "sInfo": "Registro de _START_ a _END_. Total: _TOTAL_",
            "sInfoEmpty": "Nenhum registro",
            "sInfoFiltered": "(_MAX_ registros filtrados)",
            "sSearch": "Busca",

            "oPaginate": {
                "sFirst": "Primeira",
                "sLast": "Última",
                "sNext": "Próxima",
                "sPrevious": "Anterior"
            }
        }
    });
};

ArquiteturaDotNet2012.Componente.Tabela.prototype = {

    constructor: ArquiteturaDotNet2012.Componente.Tabela,

    adicionarLinha: function (linha) {

        /// <summary>
        /// Adiciona uma linha à tabela. 
        /// &#10;Exceções: TypeError, RangeError.
        /// </summary>
        /// <param name="linha" type="Array">
        /// Um array onde cada item representa uma coluna na tabela.
        /// </param>
        /// <returns type="Array" />

        if (!(linha instanceof Array)) {
            throw new TypeError("DataTables.adicionarLinha(): O parâmetro 'linha' não é do tipo Array");
        }

        if (linha.length) {
            throw new RangeError("DataTables.adicionarLinha(): O parâmetro 'linha' não pode ser um Array vazio");
        }

        try {
            linha[0][0];
        } catch (error) {
            if (error instanceof TypeError)
                error.message = "DataTables.adicionarLinha(): O parâmetro 'linha' deve ser um Array de apenas 1 dimensão";
        }

        return this.tabela.fnAddData(linha);
    },

    adicionarLinhas: function (linhas) {

        /// <summary>
        /// Adiciona várias linhas à tabela.
        /// &#10;Exceções: TypeError, RangeError.
        /// </summary>
        /// <param name="linhas" type="Array">
        /// Um array de 2 dimensões onde cada item representa uma linha na tabela 
        /// &#10;e cada linha é um array que representa cada coluna da tabela.
        /// </param>
        /// <returns type="Array" />

        if (!(linhas instanceof Array)) {
            throw new TypeError("DataTables.adicionarLinhas(): O parâmetro 'linhas' não é do tipo Array");
        }

        if (!linhas.length) {
            throw new RangeError("DataTables.adicionarLinhas(): O parâmetro 'linhas' não pode ser um Array vazio");
        }

        for (var cont = 0; cont < linhas.length; cont++) {
            var temSubitemVazio = false;
            if (!linhas[cont].length && !temSubitemVazio) {
                temSubitemVazio = true;
            }
        }

        if (temSubitemVazio) {
            throw new RangeError("DataTables.adicionarLinhas(): Os subitens do array do parâmetro 'linhas' não devem ser vazios");
        }

        try {
            linhas[0][0];
        } catch (error) {
            if (error instanceof TypeError) {
                throw new RangeError("DataTables.adicionarLinhas(): O parâmetro 'linhas' deve ser um Array de duas dimensões");
            }
        }

        return this.tabela.fnAddData(linhas);
    },

    removerLinha: function (linha, callback) {

        /// <summary>
        /// Remove uma linha da tabela.
        /// &#10;Exceções: TypeError.
        /// </summary>
        /// <param name="linha" type="HTMLTableRowElement">
        /// Um nó TR da tabela em questão, que será removido.
        /// </param>
        /// <param name="callback" type="Function">
        /// Uma função que será executada após a remoção.
        /// </param>
        /// <returns type="void" />

        if (linha instanceof jQuery)
            linha = linha[0];

        if (!(linha instanceof HTMLTableRowElement) || !(!isNaN(parseFloat(linha)) && isFinite(linha))) {
            throw new TypeError("DataTables.removerLinha(): O parâmetro 'linha' deve ser um número ou um objeto do tipo HTMLTableRowElement");
        }

        if (callback && typeof (callback) !== "function") {
            throw new TypeError("DataTables.removerLinha(): O parâmetro 'callback' não é uma função");
        }

        this.tabela.fnDeleteRow(linha, callback);
    },

    obterIndiceDaLinha: function (linha) {

        /// <summary>
        /// Obtém índice de determinada linha.
        /// &#10;Exceções: TypeError.
        /// </summary>
        /// <param name="linha" type="object">
        /// Um nó TR da tabela em questão.
        /// </param>
        /// <returns type="int" />

        if (linha instanceof jQuery)
            linha = linha[0];

        if (!(linha instanceof HTMLTableRowElement)) {
            throw new TypeError("DataTables.obterIndiceDaLinha(): O parâmetro 'linha' não é um elemento do tipo TR");
        }

        return this.tabela.fnGetPosition(linha);
    },

    obterLinhaPorIndice: function (indice) {

        /// <summary>
        /// Obtém uma linha por determinado índice.
        /// &#10;Exceções: TypeError.
        /// </summary>
        /// <param name="indice" type="int">
        /// Índice da linha a ser obtida.
        /// </param>
        /// <returns type="HTMLTableRowElement" />

        if (typeof (indice) !== "number") {
            throw new TypeError("DataTables.obterLinhaPorIndice(): O parâmetro 'indice' deve ser do tipo Number ou do tipo primitivo int");
        }

        return this.tabela.fnGetNodes(indice);
    },

    obterTodasAsLinhas: function () {

        /// <summary>
        /// Obtém todas as linhas da tabela.
        /// </summary>
        /// <returns type="Array" />

        return this.tabela.fnGetNodes();
    },

    limparTabela: function () {

        /// <summary>
        /// Exclui todas as linhas da tabela.
        /// </summary>
        /// <returns type="void" />

        this.tabela.fnClearTable();
    }
}

// USAGE =================================================================================//

//var tabela = new ModeloIVIA.Componente.Tabela("tabelaTal");
//tabela.adicionarLinha(['Dado da Coluna 1', 'Dado da Coluna 2', 'Dado da Coluna 3']);
//tabela.adicionarLinhas([['', ''], ['', '']]);
//tabela.limparTabela();
//tabela.obterIndiceDaLinha(linhaTal);
//tabela.obterLinhaPorIndice(0);
//tabela.obterTodasAsLinhas();