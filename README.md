# Analise de Sentimento em Comentário - Machine Learning .NET

Aplicativo que fornece Endpoint(JSON) para verificar o sentimento de um texto, sendo possível treinar o modelo para o contexto da aplicação que irá integrar, para isso basta informar dados qualificados para o DataSet e treinar o modelo novamente, tudo isso via API, também foi pensado na performance da analise do texto fazendo cache em memória do **PrectionEngine** durante 60 segundos.

![GitHub](https://img.shields.io/github/license/JdouglasMendes/machine-learning-analise-sentimento)
![GitHub issues](https://img.shields.io/github/issues/JdouglasMendes/machine-learning-analise-sentimento)
![GitHub top language](https://img.shields.io/github/languages/top/JdouglasMendes/machine-learning-analise-sentimento)

## Proposta de valor para o aplicativo

Negócio    | Cenário
-----------|-----------------------------------------------------------------------------------
Ecommerce  | Retirar produtos com baixa reputação.
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
