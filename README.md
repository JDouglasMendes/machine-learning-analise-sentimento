# Analise de Sentimento em Comentário - Machime Learning .NET

Aplicativo que fornece Endpoint(JSON) para verificar o sentimento de um texto, sendo possível treinar o modelo para o contexto de uso, para isso basta informar dados qualificados para o DataSet e treinamento o modelo novamente, também foi pensado na permance da analise doo texto fazendo cache em memória do **PrectionEngine** durante 60 segundos.

## Proposta de valor para o aplicativo

Negócio    | Cenário
-----------|-----------------------------------------------------------------------------------
Ecommerce  | Ranquear os produtos por comentários.
Feedback   | Filtrar os comentários que devem ser respondido de forma personalizada.
Bot(s)     | Criar respostas automaticas para comentários.

### Techs

* .Net Core WEB API 3.1
* Entity Framework
* ML.Net
* Cache Em Memória.

### Features

Endpoint            | Método| Recurso
--------------------|-------|-------------------------------------------------------------------
*/TreinarModelo*    | Post  | Treina o modelo com os dados da tabela __DataSet__.
*/TreinarModelo*    | Get   | Obtém informações dos resultados do treinamento.    
*/DataSet*          | Post  | Adiciona texto para compor a base de treinamento do modelo.
*/DataSet*          | Get   | Obtém TODOS os textos utilizados para treinar o modelo. 
*/AnaliseComentario*| Post  | Recebe um comentário e retorna se é depreciativo ou não.

### Executando o projeto.

* Configurar a **ConectionString** no arquivo **appsettings.json**. Deve ser uma base SQL Server!;
* Configurar a **PathAnalise:Path** no arquivo **appsettings.json** para salvar o modelo.
* A aplicação deve ter permissão para *leitura* e *escrita* no diretório configurado;
