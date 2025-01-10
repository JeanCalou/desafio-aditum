API
Este projeto utiliza uma estrutura API utilizando .net core 8.0

Tecnologias utilizadas

.NET Core 8.0
MongoDb
Docker
Xunit
Moq
CSVHelper
MongoDBDriver

Instalação
Requisitos:

Docker

Execução
Na pasta .\desafio-aditum\src\desafio.aditum executar o comando
docker-compose up --build -d

EndPoints:
GET: localhost:8080/restaurant/getRestaurantByHour => Postman: necessario input no formato HH:MM exemplo: "09:00"

Esse metodo é oque foi pedido no desafio, "receba uma hora do dia como input no formato HH:MM, 
sendo HH entre 1 e 24 e MM entre 00 e 60, e retorne uma lista com os nomes dos restaurantes que estão abertos nessa hora".

Vale ressaltar que nao existe hora 24 e nem 60 minutos entao o correto seria entre 1 e 23 e entre 00 e 59 e o resto retornar erro que é oq acontece na solução.

POST: localhost:8080/restaurant/processCSV => Postman: necessario Key "file" e value arquivo.csv

esse metodo processa o arquivo csv enviado para o desafio e guarda as informações no mongoDB para poder acessar no endpoint getRestaurantByHour.

POST: localhost:8080/csv/readCSV => Postman: necessario Key "file" e value arquivo.csv

esse endpoint é um extra, ele processa qualquer arquivo csv pois utiliza objetos dinamicos, vale ressaltar que o header do csv é
obrigatorio.

Sobre o Desafio:

O Projeto foi feito utilizando uma API que é o tipo que estou mais acostumado a utilizar.
Utilizei o MongoDb pois comentaram sobre ele e eu queria começar a aprender a utiliza-lo ja que nunca havia trabalho com ele.
Utilizei a biblioteca CSVHelper para fazer a leitura dos arquivos csv, é uma biblioteca bem simples, rapida e flexivel de ser utilizada, por isso a escolha.

Desafios:

Nunca havia utilizado MongoDb então passei muitas horas estudando sobre ele, e para estudar mais a fundo,
criei uma api de crud de restaurants que acabou virando o proprio desafio no final após a refatoração do codigo.