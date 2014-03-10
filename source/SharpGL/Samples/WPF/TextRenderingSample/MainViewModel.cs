using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apex.MVVM;
using System.Windows.Media;

namespace TextRenderingSample
{
    public class MainViewModel : ViewModel
    {
        
        private NotifyingProperty XProperty =
          new NotifyingProperty("X", typeof(int), 20);

        public int X
        {
            get { return (int)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        
        private NotifyingProperty YProperty =
          new NotifyingProperty("Y", typeof(int), 20);

        public int Y
        {
            get { return (int)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        
        private NotifyingProperty RProperty =
          new NotifyingProperty("R", typeof(float), 1.0f);

        public float R
        {
            get { return (float)GetValue(RProperty); }
            set { SetValue(RProperty, value); }
        }

        
        private NotifyingProperty GProperty =
          new NotifyingProperty("G", typeof(float), 1.0f);

        public float G
        {
            get { return (float)GetValue(GProperty); }
            set { SetValue(GProperty, value); }
        }


        
        private NotifyingProperty BProperty =
          new NotifyingProperty("B", typeof(float), 1.0f);

        public float B
        {
            get { return (float)GetValue(BProperty); }
            set { SetValue(BProperty, value); }
        }
                
                
        private NotifyingProperty FontSizeProperty =
          new NotifyingProperty("FontSize", typeof(float), 12.0f);

        public float FontSize
        {
            get { return (float)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        
        private NotifyingProperty FaceNameProperty =
          new NotifyingProperty("FaceName", typeof(string), "Courier New");

        public string FaceName
        {
            get { return (string)GetValue(FaceNameProperty); }
            set { SetValue(FaceNameProperty, value); }
        }
                

        
        private NotifyingProperty TextProperty =
          new NotifyingProperty("Text", typeof(string), "You can render text in SharpGL with the 'OpenGL.DrawText' function.");

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private NotifyingProperty FontSize3DProperty =
          new NotifyingProperty("FontSize3D", typeof(float), 12.0f);

        public float FontSize3D
        {
            get { return (float)GetValue(FontSize3DProperty); }
            set { SetValue(FontSize3DProperty, value); }
        }


        private NotifyingProperty FaceName3DProperty =
          new NotifyingProperty("FaceName3D", typeof(string), "Times New Roman");

        public string FaceName3D
        {
            get { return (string)GetValue(FaceName3DProperty); }
            set { SetValue(FaceName3DProperty, value); }
        }



        private NotifyingProperty Text3DProperty =
          new NotifyingProperty("Text3D", typeof(string), "3D Text!");

        public string Text3D
        {
            get { return (string)GetValue(Text3DProperty); }
            set { SetValue(Text3DProperty, value); }
        }

        
        private NotifyingProperty Extrusion3DProperty =
          new NotifyingProperty("Extrusion3D", typeof(float), .2f);

        public float Extrusion3D
        {
            get { return (float)GetValue(Extrusion3DProperty); }
            set { SetValue(Extrusion3DProperty, value); }
        }

        
        private NotifyingProperty Deviation3DProperty =
          new NotifyingProperty("Deviation3D", typeof(float), 0f);

        public float Deviation3D
        {
            get { return (float)GetValue(Deviation3DProperty); }
            set { SetValue(Deviation3DProperty, value); }
        }
                
    }
}
