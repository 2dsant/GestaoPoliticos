![Logo](https://git.gft.com/ddds/projetoapi/-/raw/master/imgs/Logo_GFT_506x160px.png)


## 🚀 Projeto Api - Gestão de Políticos
Api desenvolvida durante o treinamento starter para o desafio de conteúdo de api rest. Se trata de
duas apis, onde uma possui acesso admin(ProjetoApi) e é possivel realizar o crud completo dentro da aplicação
com o devido login. A segunda api(ConsumoProjetoApi) é para consumir os dados provenientes da primeira api.
Realizando o tratamento dos dados e retornando apenas os dados não sensiveis dos políticos.

## 🔐Login

A aplicação utiliza o Jwt Token para validar o login. Para se utilizar a primeira Api é preciso estar logado como o admin, porém é possivel 
cadastrar um novo usuário, mas esse será cadastrado como um usuário comum e não estará autorizado para
acessar as funcionalidades do sistema. A segunda api é livre e não precisa de login, porém como
ela consome a primeira api é necessário que ela tenha um token de autorização, sendo necessário
que se realize o login a partir da segunda api através do link:

    https://localhost:5006/v1/Autenticacao/Login

Caso opte por utilizar diretamente a primeira api, o link para login é através do link:

    https://localhost:5001/v1/AuthManagement/Login

O sistema já cria automaticamente dois logins para testes, um com usuário admin e outro como
um usuário comum.
| Usuário | Senha |
|---------|------|
| admin@gft.com | Gft2021 
| usuario@gft.com | Gft2021 

## 🌍 Conexão
As duas aplicações são distintas, então é necessário que para que funcione corretamente é necessário
que se de o *dotnet watch run* nas dentro das duas pastas da aplicação.

Cada aplicação irá rodar em uma porta diferente, porém em *localhost:*

	 - API Admin 
	https://localhost:5001/swagger/index.html
	- API Consumo:
	https://localhost:5006/swagger/index.html


## 📃UML
![Logo](https://github.com/2dsant/GestaoPoliticos/blob/main/imgs/Politicos.png)

## 📰 Documentação
A documentação da aplicação foi desenvolvida através do Swagger, dessa forma assim que se iniciar a aplicação
irá ser redirecionado para uma página contendo todos os endpoints, o formato de json para envio, e as entidades do sistema. 
Sendo possível também realizar alguns testes, porém o postman continua sendo o mais recomendado para que se realize os testes.

## 🔊 Importante
- Ao realizar o cadastro de um novo político é necessário que informe uma foto convertida para base64, então recomendo o site https://codebeautify.org/image-to-base64-converter para realizar essa conversão.
- O sistema conta com um validador de CPF. então é necessário que seja informado um CPF válido. Recomendo que se utilize o site https://www.4devs.com.br/gerador_de_cpf para gerar o CPF.

