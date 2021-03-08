using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Collections.Generic;

namespace Utils.CodeGen
{
    public static class CustomDataGenerator
    {
        public static string Generate(string className) {
            var syntaxFactory = SyntaxFactory.CompilationUnit();
            syntaxFactory = syntaxFactory.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("UnityEngine")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("UnityEditor")));

            //var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("Events.CustomData"));

            ClassDeclarationSyntax dataClass = SyntaxFactory.ClassDeclaration(className);
            dataClass = dataClass.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            dataClass = dataClass.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("BaseCustomData")));

            var attributes = new SeparatedSyntaxList<AttributeSyntax>();
            attributes = attributes.Add(SyntaxFactory.Attribute(SyntaxFactory.ParseName("Serializable")));
            
            dataClass = dataClass.AddAttributeLists(SyntaxFactory.AttributeList(attributes));

            var variableDeclaration = SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName("Vector3"))
                .AddVariables(SyntaxFactory.VariableDeclarator("testVector"));

            var fieldDeclaration = SyntaxFactory.FieldDeclaration(variableDeclaration)
               .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

            var constructorDeclaration = SyntaxFactory.ConstructorDeclaration(className)
                .WithBody(SyntaxFactory.Block());

            dataClass = dataClass.AddMembers(fieldDeclaration, constructorDeclaration);

            //@namespace = @namespace.AddMembers(dataClass);

            syntaxFactory = syntaxFactory.AddMembers(dataClass);

            var code = syntaxFactory
                .NormalizeWhitespace()
                .ToFullString();

            return code;
        }
    }
}