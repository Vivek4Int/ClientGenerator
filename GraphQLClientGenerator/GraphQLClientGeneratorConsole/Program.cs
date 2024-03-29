﻿using GraphQlClientGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;

namespace GraphQLClientGeneratorConsole;
public class Program
{
    /// <summary>
    /// Console application to generate 
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        //Endpoint of GraphQLServer
        var  graphQLEndpoint = "endpoint";
        
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, graphQLEndpoint);
        
        //The constant QueryToFetchGraphQLSchema has the Introspection query
        var content = new StringContent(Constants.IntrospectionQuery,null, "application/json");
        request.Content = content;

        //GraphQL endpoint hosted at APIM, does not accept content-type as application/json with charset as UTF-8.
        //Hence explicitly assigning the ContentType header, otherwise it will throw exception stating bad request.
        request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        //Fetch schema string from GraphQL endpoint
        var response = client.SendAsync(request).Result;
        
        //Checking status code to be 200 before proceeding
        response.EnsureSuccessStatusCode();

        //Reading string content
        var schemaString = response.Content.ReadAsStringAsync().Result;

        //Generating schma type from fetched schema
        var schema = GraphQlGenerator.DeserializeGraphQlSchema(schemaString);

        //Generating C# client from schema type
        var csharpCode = new GraphQlGenerator().GenerateFullClientCSharpFile(schema, "APIMGraphQLClient");


        //Write the generated client class to file named GraphQLClient.cs
        var filename = "GraphQLClient.cs";
        if(File.Exists(filename))
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Deleting old file.");
            Console.ResetColor();
            File.Delete(filename);
        }
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Creating new client file.");
        File.WriteAllText(filename,csharpCode);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Creating new client file: {0}.",filename);
        Console.WriteLine("FullPath: {0}",new FileInfo(filename).FullName);
    }

    
}  
