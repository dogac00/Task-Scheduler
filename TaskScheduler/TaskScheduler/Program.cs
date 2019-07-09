using SimpleInjector;
using System;
using System.Windows.Forms;

namespace TaskScheduler
{
    static class Program
    {
        private static Container container;

        private static void Bootstrap()
        {
            // Create the container as usual.
            container = new Container();

            // Register your types, for instance:
            container.Register<IRepository, JsonRepository>(Lifestyle.Singleton);
            container.Register<Form1>(Lifestyle.Singleton);

            // Optionally verify the container.
            container.Verify();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Bootstrap();
            Application.Run(container.GetInstance<Form1>());
        }
    }
}
