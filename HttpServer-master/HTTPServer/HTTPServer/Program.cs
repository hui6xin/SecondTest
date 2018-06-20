using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTPServerLib;
using HttpServer;

namespace HTTPServerLib
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleServer server = new ExampleServer("127.0.0.1", 4050);
            server.SetRoot(@"D:\SVN\凤凰电子书\02开发库\05版本管理\01临时版本\源码\fheb\Debug\Books\102\102\");
            server.Logger = new ConsoleLogger();
            server.Start();
        }
    }
}
