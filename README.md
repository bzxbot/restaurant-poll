Restaurant Poll
-------

Um pequeno aplicativo para um grupo decidir onde ir almoçar.

O aplicativo possui um formulário de login. O formulário de login, conforme instruções dos requisitos
foi feito sem utilizar banco de dados. Os usuários são definidos quando a aplicação é executada na classe User.

Os usuários criados por padrão são os seguintes:

> joao@dbserver.com
 
> maria@dbserver.com

> jose@dbserver.com

A interface do sistema foi desenvolvida utilizando Bootstrap.

A interface e os estilos foram testados no Google Chrome.

Para todos, a senha é "**db**", sem as aspas.

O objetivo do formulário de login é apenas manter um registro dos votos nos restaurantes, e não criar um
formulário de login seguro e robusto. Se esse fosse o caso, os recursos prontos do ASP.NET MVC 5 seria utilizados. Porém,
como a orientação era a não utilização de um banco de dados, foi utilizada uma versão light para autenticação.

Após a tela de login, é exibido uma lista com cinco restaurantes pré-cadastrados para que o usuário possa fazer sua escolha.

A data padrão do sistema é sempre o dia atual. Como os requisitos não são claro, a semana só tem dias uteis, de segunda até sexta-feira.

O sistema permite, para facilitar testes, que a data seja avançada para o próximo dia útil.

Um ponto que não é esclarecido nos requisitos é qual o critério para desempate da votação.

Como não é possível esclarecer os requisitos, o sistema escolhe o primeiro restaurante com mais votos, de acordo com a estrutura interna do sistema.

Os testes unitários cobrem as 3 estórias, e mais alguns métodos importantes do sistema.

Instalação
-------

Obtenha o código fonte e execute através do Visual Studio.

O projeto foi desenvolvido usando Visual Studio 2013 Community Edition.

Autor
-------

Bernardo Botelho
