using Xamarin.Forms;

namespace Kaushal.Kabra.PJ2
{
	public partial class App : Application
	{
		const int index = 0;

		public App()
		{
			if (Properties.ContainsKey(index.ToString()))
			{
				redSavedValue = (string)Properties[index.ToString()];
			}

			MainPage = new KaushalKabraPJ2();
		}

		public string redSavedValue { set; get; }

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			Properties[index.ToString()] = redSavedValue;
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
