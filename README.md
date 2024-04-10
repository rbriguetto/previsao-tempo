## Escopo

Desenvolva uma aplicação com o propósito de disponibilizar previsões meteorológicas diárias para diversas cidades. A aplicação deve aderir aos seguintes padrões:

**Tecnologia:** ASP .NET Core Web API com .NET 8.0, C#, banco de dados Microsoft SQL Server.

Para obter informações de previsão do tempo, é necessário consumir a API `https://api.openweathermap.org/data/3.0/onecall?lat=33.44&lon=-94.04&appid=[{API key}](https://home.openweathermap.org/api_keys)` conforme as instruções fornecidas para a criação da chave [aqui](https://home.openweathermap.org/api_keys).

Exemplo da Chave da API: `a691f923da4fb6dd8a642821b4ff2bd1`

## Funcionalidades

1. Rota para retornar todas as cidades cadastradas no banco de dados.
2. Rota para obter os detalhes de uma cidade cadastrada no banco de dados e com as informações do OpenWeather.
    1. As informações a serem obtidas incluem temperatura (main.temp), sensação térmica (main.feelks_like), valores mínimos/máximos (main.temp_min, main.temp_max), umidade (main.humidity), horário do nascer do sol (sys.sunrise) e pôr do sol (sys.sunset).
3. Rota para adicionar uma cidade.
4. Rota para editar uma cidade.
5. Rota para excluir uma cidade.

## Diferenciais extras

- Utilizar o CQRS como padrão organizacional com MediatR.


## Como rodar

- Utilize `docker-compose up` para rodar o sistema com todas as peças de infraestrutura.
- Acesse o frontend pela porta `http://localhost:8080`.
