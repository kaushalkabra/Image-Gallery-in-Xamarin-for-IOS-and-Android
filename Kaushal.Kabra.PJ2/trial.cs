using System;
using System.Reflection;
using System.IO;
using Xamarin.Forms;
using System.ComponentModel;

namespace Kaushal.Kabra.PJ2
{
	public partial class MainPage : ContentPage
	{
		int index = 0, i = 1;
		string[] images = new string[100];
		string resourseID;
		Assembly assembly;
		Stream stream;

		public MainPage()
		{
			//InitializeComponent();

			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
			{

				if (switchButton.IsToggled == true)
				{
					timer.Text = (int.Parse(timer.Text) - 1).ToString();
					if (int.Parse(timer.Text) == 0)
					{
						if (i == 0)
						{
							activityIndicator.IsRunning = true;
							image.Source = ImageSource.FromResource(images[i]);
							i = i + 1;
							activityIndicator.IsRunning = false;
						}
						else if (i == 1)
						{
							activityIndicator.IsRunning = true;
							image.Source = ImageSource.FromFile(images[i]);
							i = i + 1;
							activityIndicator.IsRunning = false;
						}
						else if (i == 2)
						{
							activityIndicator.IsRunning = true;
							resourseID = images[i];
							image.Source = ImageSource.FromStream(() =>
							{
								Assembly assembly = GetType().GetTypeInfo().Assembly;
								Stream stream = assembly.GetManifestResourceStream(resourseID);
								return stream;
							});
							i = i + 1;
							activityIndicator.IsRunning = false;
						}
						else
						{
							activityIndicator.IsRunning = true;
							image.Source = ImageSource.FromUri(new Uri(images[i]));
							i = i + 1;
							activityIndicator.IsRunning = false;
						}
						timer.Text = entry.Text;
						if (i == index)
						{
							i = 0;
						}

					}
					return true;
				}
				else
				{
					return false;
				}

			});

			tableSection.Add(new ImageCell
			{
				ImageSource = ImageSource.FromResource("frame.image1.jpg"),
				Text = "LAMBORGHINI AVENTADOR SUPERVELOCE",
				Detail = "Static Image loaded from Resource"
			});
			images[index] = "frame.image1.jpg";
			index = index + 1;

			tableSection.Add(new ImageCell
			{
				ImageSource = ImageSource.FromFile("image2.jpg"),
				Text = "Lamborghini Avantador Roadster",
				Detail = "Static Image loaded from File"
			});
			images[index] = "image2.jpg";
			index = index + 1;

			tableSection.Add(new ImageCell
			{

				ImageSource = ImageSource.FromStream(() =>
				{
					assembly = GetType().GetTypeInfo().Assembly;
					stream = assembly.GetManifestResourceStream("frame.image3.jpg");
					return stream;
				}),
				Text = "Vrsteiner Zaragoza",
				Detail = "Static Image loaded from Stream"
			});
			images[index] = "frame.image3.jpg";
			index = index + 1;

			tableSection.Add(new ImageCell
			{
				ImageSource = ImageSource.FromUri(new Uri("http://www.hdcarwallpapers.com/walls/2016_vorsteiner_lamborghini_aventador_zaragoza_edizione_2-HD.jpg")),
				Text = "Lamborghini-Aventador",
				Detail = "Static Image loaded from URL"
			});
			images[index] = "http://www.hdcarwallpapers.com/walls/2016_vorsteiner_lamborghini_aventador_zaragoza_edizione_2-HD.jpg";
			index = index + 1;

		}

		async void OnRemoveButtonClicked(object sender, EventArgs args)
		{
			int count = tableSection.Count;
			if (count == 4)
			{
				await DisplayAlert("Warning!", "Cannot remove anymore items from the list", "OK");
			}
			else
			{
				var answer = await DisplayAlert("Confirm", "Do you want to remove \"" + images[index - 1] + "\"from the list?", "Yes", "No");
				if (answer == true)
				{
					tableSection.RemoveAt(count - 1);
					index = index - 1;
				}
				//   app.Index = index.ToString();
			}
		}

		void OnAddButtonClicked(object sender, EventArgs args)
		{
			hide.IsVisible = true;
		}

		void OnSwitchToggled(object sender, ToggledEventArgs args)
		{
			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
			{
				if (switchButton.IsToggled == true)
				{
					timer.Text = (int.Parse(timer.Text) - 1).ToString();
					if (int.Parse(timer.Text) == 0)
					{
						if (i == 0)
						{
							activityIndicator.IsRunning = true;
							image.Source = ImageSource.FromResource(images[i]);
							i = i + 1;
							activityIndicator.IsRunning = false;
						}
						else if (i == 1)
						{
							activityIndicator.IsRunning = true;
							image.Source = ImageSource.FromFile(images[i]);
							i = i + 1;
							activityIndicator.IsRunning = false;
						}
						else if (i == 2)
						{
							activityIndicator.IsRunning = true;
							resourseID = images[i];
							image.Source = ImageSource.FromStream(() =>
							{
								Assembly assembly = GetType().GetTypeInfo().Assembly;
								Stream stream = assembly.GetManifestResourceStream(resourseID);
								return stream;
							});
							i = i + 1;
							activityIndicator.IsRunning = false;
						}
						else
						{
							activityIndicator.IsRunning = true;
							image.Source = ImageSource.FromUri(new Uri(images[i]));
							i = i + 1;
							activityIndicator.IsRunning = false;
						}
						timer.Text = entry.Text;
						if (i == index)
						{
							i = 0;
						}

					}
					// app.I = i.ToString();
					return true;
				}
				else
				{
					return false;
				}
			});
		}

		void OnTextChanged(object sender, EventArgs args)
		{
			Entry textEntry = (Entry)sender;
			int seconds = int.Parse(textEntry.Text);
			if (seconds > 0 && seconds <= 60)
			{
				stepper.Value = seconds;
				slider.Value = seconds;
				timer.Text = textEntry.Text;

			}
			else
			{
				DisplayAlert("Alert", "Enter values between 1 to 60 inclusive", "OK");
				textEntry.Text = stepper.Value.ToString();
			}
			/* app.TextEntry = textEntry.Text;
			 DisplayAlert("", app.TextEntry, "OK");*/
		}

		void OnStepperValueChanged(object sender, ValueChangedEventArgs args)
		{
			Stepper step = (Stepper)sender;
			stepper.Value = step.Value;
			entry.Text = step.Value.ToString();
			timer.Text = step.Value.ToString();
			// app.TextEntry = timeEntry.Text;
		}

		void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
		{
			Slider slid = (Slider)sender;
			int value = Convert.ToInt32(slider.Value);
			if (value == 0)
			{
				value = value + 1;
				slider.Value = value;
				entry.Text = value.ToString();
				timer.Text = value.ToString();
			}
			else
			{
				slider.Value = value;
				entry.Text = value.ToString();
				timer.Text = value.ToString();
			}
			//app.TextEntry = entry.Text;

		}

		void OnAddListButtonClicked(object sender, EventArgs args)
		{
			tableSection.Add(new ImageCell
			{
				ImageSource = ImageSource.FromUri(new Uri(url.Text)),
				Text = ImageName.Text
			});
			images[index] = url.Text;
			index = index + 1;
			url.Text = null;
			ImageName.Text = null;

			hide.IsVisible = false;
			// app.Index = index.ToString();
		}

		void OnCancelButtonClicked(object sender, EventArgs args)
		{
			hide.IsVisible = false;
		}

		void OnImagePropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "IsLoading")
			{
				activityIndicator.IsRunning = ((Image)sender).IsLoading;
			}
		}

	}
}