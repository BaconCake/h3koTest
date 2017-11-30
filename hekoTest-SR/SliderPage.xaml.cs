using System;
using Xamarin.Forms;

namespace hekoTestSR
{
    public partial class SliderPage : ContentPage
    {

        private int sliderValue = 0;
        private int sliderTicks = 11;
        private int minSliderValue = 1;

        public SliderPage()
        {
            InitializeComponent();
            UpdateSliderLabels();
        }

        void OnSliderChanged(object sender, EventArgs e)
        {
            UpdateSliderLabels();
        }

        void UpdateSliderLabels()
        {
            sliderValue = Convert.ToInt32(SliderView.Value * sliderTicks) + minSliderValue;
            sliderValueText.Text = "Value: " + sliderValue.ToString();

            //Special Case: Slider Value of 12 brings up a Fibunacci Value of 100
            if(sliderValue.Equals(12))
            {
                sliderFibText.Text = "Fibunacci Value: 100";
            } else 
            {
                sliderFibText.Text = "Fibunacci Value: " + GetFibonacci(sliderValue).ToString();
            }

        }

        private int GetFibonacci(int n)
        {
            int a = 0;
            int b = 1;
            // In N steps compute Fibonacci sequence iteratively.
            for (int i = 0; i < n; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
            }
            return a;
        }
    }
}
