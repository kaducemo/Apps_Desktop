using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimPgmDataprom
{
    public enum ME
    {
        None = 0,
        Desconectado,
        Conectado,
        Aguardando,
    };
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        

        public static CancellationTokenSource cts = new CancellationTokenSource();

        [STAThread]
        static void Main()
        {
            //Thread thread = new Thread(() => MainLoop(cts.Token));
            //thread.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //thread.Join(); // Aguarda a thread terminar

        }

        static void MainLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Console.WriteLine("Executando loop...");
                Thread.Sleep(1000);
            }

            Console.WriteLine("Loop encerrado.");
        }

    }
}
