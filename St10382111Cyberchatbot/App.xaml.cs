using System.Windows;

namespace St10382111Cyberchatbot
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new ChatWindow().Show();
        }
    }
}