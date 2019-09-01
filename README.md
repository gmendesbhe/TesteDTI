# Teste DTI

Programa escrito em dotnet core versao 2.1.503 com linguagem C#

## Executando o programa

Clone o repositorio `git clone https://github.com/gmendesbhe/TesteDTI.git` ou faça o download do mesmo como `.zip`

Abra um console na pasta src do projeto e execute `dotnet build` para compilar o código fonte.

Execute `dotnet test` para executar os testes.

Execute `dotnet run --project .\DTI\DTI.csproj` para iniciar o sistema ou execute `dotnet publish -c Release -r win-x64 --self-contained false` para gerar o executavel na pasta `<pasta-do-projeto>\src\DTI\bin\Release\netcoreapp2.1\win-x64\publish`.

A qualquer momento durante a execução, pressionar `ctrl+c` irá parar a execução do programa.

## Observações gerais

O programa utilizará o locale para **pt-br**, ou seja, datas terão o formato *"dd/MM/yyyy"* e separador decimal será o caracter *" , "*.

O programa também considerará que "hora" esteja no formato 24h.

O programa conta com alguns alimentos já cadastrados, separados em 3 grupos, com seus respectivos valors calóricos.

O programa conta com 4 operações básica, cadastro e lista de pacientes, e cadastro e lista de consultas.

Para o cadastro de pacientes todos os dados são obrigatórios.

Para o cadastro de consultas, "Sensação física" e "Dieta" são os únicos dados **não** obrigatórios.

Selecionar um item nas listas de paciente e consulta permite a alteração do mesmo.

O código foi dividido em camadas de Apresentação (classes para exibição na tela), Modelo (classes de modelo de persistência), Dados (classes de acesso aos dados persistidos), Menu (classes de "tela"), Interfaces e Utilitários.


