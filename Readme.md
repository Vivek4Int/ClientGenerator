# GraphQL client class auto generation

Small application to generate GraphQL client C# code. It leverages GraphQLClientGenerator libraray to achive this. Following are the process to generating GraphQL client,

1. Fetch the backend schema,
2. Deserilize the schema to GraphQLClientGenerator's schema format and,
3. Generate client out of the schema object.

## Fetching GraphQL Schema

Even though GraphQLClientGenerator library can fetch and serialize the schema in one go, it did not work with the APIM GraphQL endpoint, as it by default added charset as UTF-8, which resulted APIM endpoint prompting bad request error. The way around it was to use http client to directly fetch the schema leveraging Introspection. This way we make sure that the content header is set to "application/json" alone, with no specification of charset to use.

For more info on Introspection follow these links,

1. [Performing GraphQL interospection](https://blog.yeswehack.com/yeswerhackers/how-exploit-graphql-endpoint-bug-bounty/)
2. [Introspection](https://graphql.org/learn/introspection/)

## Deserializing

By deserialization, we are just converting the json schema to GraphQLClientGenerator's Schema object, which can be used to generator the client class.

## Client class generation

The deserilized schema is plugged into GraphQLClientGenerator's helper method to generate the client class.

## Usage

todo: add usage
