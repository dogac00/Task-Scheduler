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
            container = new Container();

            container.Register<IRepository, JsonRepository>(Lifestyle.Singleton);
            container.Register<MainForm>(Lifestyle.Singleton);

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
            Application.Run(container.GetInstance<MainForm>());
        }
    }
}
