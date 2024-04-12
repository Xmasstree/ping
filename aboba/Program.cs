using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace aboba
{
    class Program
    {
        static void Main(string[] args)
        {

            Process myProcess = new Process();

            try
            {
                myProcess.StartInfo.UseShellExecute = false;
                
                myProcess.StartInfo.FileName = args[0];

                myProcess.StartInfo.RedirectStandardInput = true;
                myProcess.StartInfo.RedirectStandardOutput = true;


                myProcess.Start();
                Console.WriteLine("Дочерний процесс запущен. Ждём его окончания...");
                // Ждём окончания дочернего процесса.
                Console.Write("Введите что пинговать: ");
                string ping = Console.ReadLine();
                Console.Write("Введите количество пакетов: ");
                string pac = Console.ReadLine();
                myProcess.StandardInput.WriteLine("ping -n " + pac + " " + ping);
                myProcess.StandardInput.WriteLine("exit");
                StreamReader sr = myProcess.StandardOutput;
                string line = sr.ReadToEnd();
                line = line.Replace("Пакетов", "`");
                line = line.Replace("Приблизительное", "`");
                string[] str = line.Split('`');
                Console.WriteLine("Пакетов" + str[1]);


                
                myProcess.WaitForExit();
                Console.WriteLine("Дочерний процесс закончен.");

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
