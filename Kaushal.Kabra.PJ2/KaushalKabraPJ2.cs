using System;
using System.Reflection;
using System.IO;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace Kaushal.Kabra.PJ2
{
	public partial class KaushalKabraPJ2 : ContentPage
	{
		int index = 0, i = 0, j = 4;
		string[] images = new string[100];
		string res;
		Label lab;
		Assembly assembly;
		Stream stream;
		StackLayout list;
		Image resource_image, stream_image, file_image, url_image, new_image;
		App app = Application.Current as App;

		public KaushalKabraPJ2()
		{
			//intialising function when application start
			InitializeComponent();
			index = int.Parse(app.redSavedValue);
			i = index;

			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
			{

				if (switchButton.IsToggled == true)
				{
					timer.Text = (int.Parse(timer.Text) - 1).ToString();
					if (int.Parse(timer.Text) == 0)
					{
						//Checking the index of the image
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
							res = images[i];
							image.Source = ImageSource.FromStream(() =>
							{
								assembly = GetType().GetTypeInfo().Assembly;
								stream = assembly.GetManifestResourceStream(res);
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
						timer.Text = entryTime.Text;
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

			resource_image = new Image
			{
				Source = ImageSource.FromResource("Kaushal.Kabra.PJ2.brownie.jpg"),
				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.End,
				HeightRequest = 100,
				WidthRequest = 100
			};

			stream_image = new Image
			{
				Source = ImageSource.FromFile("cake.jpg"),
				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.End,
				HeightRequest = 100,
				WidthRequest = 100
			};

			file_image = new Image
			{
				Source = ImageSource.FromStream(() =>
				{
					assembly = GetType().GetTypeInfo().Assembly;
					stream = assembly.GetManifestResourceStream("Kaushal.Kabra.PJ2.desert.jpg");
					return stream;
				}),
				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.End,
				HeightRequest = 100,
				WidthRequest = 100
			};

			url_image = new Image
			{
				Source = "https://s-media-cache-ak0.pinimg.com/originals/ea/99/d7/ea99d7d60e5322d984a7598a1df54b21.jpg",
				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.End,
				HeightRequest = 100,
				WidthRequest = 100
			};

			// Adding image from Resource
			list1.Children.Add(resource_image);
			images[index] = "Kaushal.Kabra.PJ2.brownie.jpg";
			index = index + 1;

			//Adding image from File
			list2.Children.Add(stream_image);
			images[index] = "cake.jpg";
			index = index + 1;

			//Adding image from stream
			list3.Children.Add(file_image);
			images[index] = "Kaushal.Kabra.PJ2.desert.jpg";
			index = index + 1;

			//adding image from URL
			list4.Children.Add(url_image);
			images[index] = "https://s-media-cache-ak0.pinimg.com/originals/ea/99/d7/ea99d7d60e5322d984a7598a1df54b21.jpg";
			index = index + 1;
		}

		void OnAddButtonClicked(object sender, EventArgs args)
		{
			hide.IsVisible = true;
		}

		void OnAddListButtonClicked(object sender, EventArgs args)
		{
			lab = new Label
			{
				Text = ImageName.Text
			};
			new_image = new Image
			{
				Source = ImageSource.FromUri(new Uri(url.Text)),
				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.End,
				HeightRequest = 100,
				WidthRequest = 100
			};
			list = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Children =
				{
					lab,
					new_image
				}
			};

			j++;
			stacklayout.Children.Add(list);
			images[index] = url.Text;
			index = index + 1;
			url.Text = null;
			ImageName.Text = null;

			hide.IsVisible = false;

		}

		void OnCancelButtonClicked(object sender, EventArgs args)
		{
			hide.IsVisible = false;
		}

		async void OnRemoveButtonClicked(object sender, EventArgs args)
		{
			int count = j;
			if (count == 4)
			{
				await DisplayAlert("Warning!", "Cannot remove default image", "Ok");
			}
			else
			{
				var answer = await DisplayAlert("Confirm", "Do you want to remove \"" + images[index - 1] + "\"from the list?", "Yes", "No");
				if (answer == true)
				{
					list.Children.Clear();
					j--;
				}
			}
		}

		void OnImagePropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "IsLoading")
			{
				activityIndicator.IsRunning = ((Image)sender).IsLoading;
			}
			app.redSavedValue = index.ToString();

		}

		void OnToggled(object sender, ToggledEventArgs args)
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
							res = images[i];
							image.Source = ImageSource.FromStream(() =>
							{
								assembly = GetType().GetTypeInfo().Assembly;
								stream = assembly.GetManifestResourceStream(res);
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
						timer.Text = entryTime.Text;
						if (i == index)
						{
							i = 0;
						}

					}
					//app.I = i.ToString();
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
				DisplayAlert("Alert", "Range of values is between 1 to 60 only", "OK");
				textEntry.Text = stepper.Value.ToString();
			}
		}

		void OnStepperValueChanged(object sender, ValueChangedEventArgs args)
		{
			Stepper step = (Stepper)sender;
			stepper.Value = step.Value;
			entryTime.Text = step.Value.ToString();
			timer.Text = step.Value.ToString();
		}

		void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
		{
			int value = Convert.ToInt32(slider.Value);
			if (value == 0)
			{
				value = value + 1;
				slider.Value = value;
				entryTime.Text = value.ToString();
				timer.Text = value.ToString();
			}
			else
			{
				slider.Value = value;
				entryTime.Text = value.ToString();
				timer.Text = value.ToString();
			}
			//app.TextEntry = entry.Text;
		}

		async void OnImageTapped(object sender, EventArgs args)
		{

			if (sender == list1)
			{
				image.Source = resource_image.Source;
			}
			else if (sender == list2)
			{
				image.Source = stream_image.Source;

			}
			else if (sender == list3)
			{
				image.Source = file_image.Source;
			}
			else if (sender == list4)
			{
				image.Source = url_image.Source;
			}
			else if (sender == stacklayout)
			{
				image.Source = new_image.Source;
			}
			else
			{
				await DisplayAlert("Warning!", "More Tappped", "OK");
			}

		}

	}
}