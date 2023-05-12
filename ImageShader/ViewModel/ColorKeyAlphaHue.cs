using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ImageShader.ViewModel
{
	public class ColorKeyAlphaHue : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(ColorKeyAlphaHue), 0);
        public static readonly DependencyProperty Input2Property = ShaderEffect.RegisterPixelShaderSamplerProperty("Input2", typeof(ColorKeyAlphaHue), 1);
        public static readonly DependencyProperty ColorKeyProperty = DependencyProperty.Register("ColorKey", typeof(Color), typeof(ColorKeyAlphaHue), new UIPropertyMetadata(Color.FromArgb(255, 0, 0, 0), PixelShaderConstantCallback(0)));
        public static readonly DependencyProperty ToleranceBackProperty = DependencyProperty.Register("ToleranceBack", typeof(double), typeof(ColorKeyAlphaHue), new UIPropertyMetadata(((double)(0.1D)), PixelShaderConstantCallback(1)));
        public static readonly DependencyProperty ToleranceEdge1Property = DependencyProperty.Register("ToleranceEdge1", typeof(double), typeof(ColorKeyAlphaHue), new UIPropertyMetadata(((double)(0.2D)), PixelShaderConstantCallback(2)));
        public static readonly DependencyProperty ToleranceEdge2Property = DependencyProperty.Register("ToleranceEdge2", typeof(double), typeof(ColorKeyAlphaHue), new UIPropertyMetadata(((double)(0.3D)), PixelShaderConstantCallback(3)));
        public static readonly DependencyProperty MaxColorProperty = DependencyProperty.Register("MaxColor", typeof(double), typeof(ColorKeyAlphaHue), new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(4)));
        public ColorKeyAlphaHue()
        {
            Init();
        }

        public void Init()
        {
            PixelShader pixelShader = new PixelShader();
            pixelShader.UriSource = new Uri(@"/ImageShader;Component/Shader/ColorKeyAlphaHue.ps", UriKind.Relative);
            this.PixelShader = pixelShader;

            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(Input2Property);
            this.UpdateShaderValue(ColorKeyProperty);
            this.UpdateShaderValue(ToleranceBackProperty);
            this.UpdateShaderValue(ToleranceEdge1Property);
            this.UpdateShaderValue(ToleranceEdge2Property);
            this.UpdateShaderValue(MaxColorProperty);
        }

		public Brush Input
        {
            get => ((Brush)(this.GetValue(InputProperty)));
            set => this.SetValue(InputProperty, value);
        }

        public Brush Input2
        {
            get => ((Brush)(this.GetValue(Input2Property)));
            set => this.SetValue(Input2Property, value);
        }

        public Color ColorKey
        {
            get => ((Color)(this.GetValue(ColorKeyProperty)));
            set => this.SetValue(ColorKeyProperty, value);
        }

        public double ToleranceBack
        {
            get => ((double)(this.GetValue(ToleranceBackProperty)));
            set => this.SetValue(ToleranceBackProperty, value);
        }

        public double ToleranceEdge1
        {
            get => ((double)(this.GetValue(ToleranceEdge1Property)));
            set => this.SetValue(ToleranceEdge1Property, value);
        }

        public double ToleranceEdge2
        {
            get => ((double)(this.GetValue(ToleranceEdge2Property)));
            set => this.SetValue(ToleranceEdge2Property, value);
        }

        public double MaxColor
        {
            get => ((double)(this.GetValue(MaxColorProperty)));
            set => this.SetValue(MaxColorProperty, value);
        }

	}
}
