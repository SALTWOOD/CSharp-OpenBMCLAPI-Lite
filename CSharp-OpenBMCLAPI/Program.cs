﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CSharpOpenBMCLAPI.Modules;
using Newtonsoft.Json;
using TeraIO.Runnable;

namespace CSharpOpenBMCLAPI
{
    internal class Program : RunnableBase
    {
        public static void PrintTypeInfo<T>(T instance)
        {
            Type type = typeof(T);
            Console.WriteLine($"Type: {type.Name}");

            // Print fields
            Console.WriteLine("Fields:");
            foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                Console.WriteLine($"|---{field.Name}: {field.FieldType.Name} = {field.GetValue(instance)}");
            }

            // Print private fields
            Console.WriteLine("NonPublic fields:");
            foreach (FieldInfo field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Console.WriteLine($"|---{field.Name}: {field.FieldType.Name} = {field.GetValue(instance)}");
            }

            // Print properties
            Console.WriteLine("Properties:");
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Console.WriteLine($"|---{property.Name}: {property.PropertyType.Name} = {property.GetValue(instance)}");
            }

            // Print methods
            Console.WriteLine("Methods:");
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                Console.WriteLine($"|---{method.Name}({string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))})");
                if (method.ReturnType != typeof(void))
                {
                    Console.WriteLine($"|---returns: {method.ReturnType.Name}");
                }
            }
        }

        public Program() : base()
        {

        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
            program.WaitForStop();
        }

        protected override int Run(string[] args)
        {
            Task<int> task = AsyncRun();
            task.Wait();
            return task.Result;
        }

        protected async Task<int> AsyncRun()
        {
            int returns = 0;

            // TODO

            return returns;
        }
    }
}