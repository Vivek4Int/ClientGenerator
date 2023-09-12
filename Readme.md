# GraphQL client class auto generation

This is a small application to generate GraphQL client C# code. It leverages GraphQLClientGenerator libraray to achive this. Following are the process associated with generating GraphQL client,

1. Fetch the backend schema,
2. Deserilize the schema to GraphQLClientGenerator's schema format and,
3. Generate client out of the schema object.

## Fetch GraphQL Schema

Even though GraphQLClientGenerator library can fetch and serialize the schema in one go, it did not work with the APIM GraphQL endpoint, as it by default added charset as UTF-8, which resulted APIM endpoint prompting bad request error. The way around it was to use http client to directly fetch the schema leveraging Introspection. This way we make sure that the content header is set to "application/json" alone, with no specification of charset to use.

For more info on Introspection follow these links,

1. [Performing GraphQL interospection](https://blog.yeswehack.com/yeswerhackers/how-exploit-graphql-endpoint-bug-bounty/)
2. [Introspection](https://graphql.org/learn/introspection/)

## Deserialize

By deserialization, we are just converting the json schema to GraphQLClientGenerator's Schema object, which can be used to generator the client class.

## Client Class Generation

The deserilized schema is plugged into GraphQLClientGenerator's helper method to generate the client class.

## Usage

### Replace the placeholder

Replace the endpoint placeholder in program.cs with your actual endpoint.

### Run the program

Go to folder ClientGenerator\GraphQLClientGenerator\GraphQLClientGeneratorConsole and run command,

``` powershell
dotnet run
```

This will result in a generation of file named GraphQLClient.cs, which is the GraphQL client for your endpoint. This will help you in property construction your GraphQL query and mutation, rather than hardcoding them in query or mutation string.

## Usecase for client

Refer to the [GraphQLClientGenerator GitHub page.](https://github.com/Husqvik/GraphQlClientGenerator)

Internal reference title **GraphQL Client Class Generator**.
