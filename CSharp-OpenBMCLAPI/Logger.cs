﻿using CSharpOpenBMCLAPI.Modules;

namespace CSharpOpenBMCLAPI
{
    public class Logger
    {
        public Logger()
        {
            
        }

        public void LogDebug(params object[] args)
        {
            lock (this)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\r");
                Console.WriteLine(string.Join(" ", args));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void LogInfo(params object[] args)
        {
            lock (this)
            {
                Console.Write("\r");
                Console.WriteLine(string.Join(" ", args));
            }
        }

        public void LogWarn(params object[] args)
        {
            lock (this)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\r");
                Console.WriteLine(string.Join(" ", args));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void LogError(params object[] args)
        {
            lock (this)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\r");
                Console.WriteLine(string.Join(" ", args));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void LogDebugNoNewLine(params object[] args)
        {
            lock (this)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\r");
                Console.Write(string.Join(" ", args));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void LogInfoNoNewLine(params object[] args)
        {
            lock (this)
            {
                Console.Write("\r");
                Console.Write(string.Join(" ", args));
            }
        }

        public void LogWarnNoNewLine(params object[] args)
        {
            lock (this)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\r");
                Console.Write(string.Join(" ", args));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void LogErrorNoNewLine(params object[] args)
        {
            lock (this)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\r");
                Console.Write(string.Join(" ", args));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
