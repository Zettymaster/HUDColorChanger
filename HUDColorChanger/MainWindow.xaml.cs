using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace HUDColorChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly string XML_PATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
            @"\AppData\Local\Frontier Developments\Elite Dangerous\Options\Graphics\GraphicsConfigurationOverride.xml";

        public MainWindow()
        {
            InitializeComponent();

            InitializeXML();
            ParseXML();

        }
        #region dependency properties

        public float RR
        {
            get { return (float)GetValue(RRProperty); }
            set { SetValue(RRProperty, value); }
        }
        public static readonly DependencyProperty RRProperty =
            DependencyProperty.Register("RR", typeof(float), typeof(MainWindow), new PropertyMetadata(0f, OnPropertyChanged));
        
        public float RG
        {
            get { return (float)GetValue(RGProperty); }
            set { SetValue(RGProperty, value); }
        }
        public static readonly DependencyProperty RGProperty =
            DependencyProperty.Register("RG", typeof(float), typeof(MainWindow), new PropertyMetadata(0f, OnPropertyChanged));
        
        public float RB
        {
            get { return (float)GetValue(RBProperty); }
            set { SetValue(RBProperty, value); }
        }
        public static readonly DependencyProperty RBProperty =
            DependencyProperty.Register("RB", typeof(float), typeof(MainWindow), new PropertyMetadata(0f, OnPropertyChanged));
        
        public float GR
        {
            get { return (float)GetValue(GRProperty); }
            set { SetValue(GRProperty, value); }
        }
        public static readonly DependencyProperty GRProperty =
            DependencyProperty.Register("GR", typeof(float), typeof(MainWindow), new PropertyMetadata(0f, OnPropertyChanged));
        
        public float GG
        {
            get { return (float)GetValue(GGProperty); }
            set { SetValue(GGProperty, value); }
        }
        public static readonly DependencyProperty GGProperty =
            DependencyProperty.Register("GG", typeof(float), typeof(MainWindow), new PropertyMetadata(0f, OnPropertyChanged));
        
        public float GB
        {
            get { return (float)GetValue(GBProperty); }
            set { SetValue(GBProperty, value); }
        }
        public static readonly DependencyProperty GBProperty =
            DependencyProperty.Register("GB", typeof(float), typeof(MainWindow), new PropertyMetadata(0f, OnPropertyChanged));
        
        public float BR
        {
            get { return (float)GetValue(BRProperty); }
            set { SetValue(BRProperty, value); }
        }
        public static readonly DependencyProperty BRProperty =
            DependencyProperty.Register("BR", typeof(float), typeof(MainWindow), new PropertyMetadata(0f, OnPropertyChanged));
        
        public float BG
        {
            get { return (float)GetValue(BGProperty); }
            set { SetValue(BGProperty, value); }
        }
        public static readonly DependencyProperty BGProperty =
            DependencyProperty.Register("BG", typeof(float), typeof(MainWindow), new PropertyMetadata(0f, OnPropertyChanged));
        
        public float BB
        {
            get { return (float)GetValue(BBProperty); }
            set { SetValue(BBProperty, value); }
        }
        public static readonly DependencyProperty BBProperty =
            DependencyProperty.Register("BB", typeof(float), typeof(MainWindow), new PropertyMetadata(0f, OnPropertyChanged));



        public Color FinalHUDColor
        {
            get { return (Color)GetValue(FinalHUDColorProperty); }
            set { SetValue(FinalHUDColorProperty, value); }
        }
        public static readonly DependencyProperty FinalHUDColorProperty =
            DependencyProperty.Register("FinalHUDColor", typeof(Color), typeof(MainWindow), new PropertyMetadata(Color.FromRgb(0xFF, 0x71, 0x00)));
        
        public Color FinalAccentColor
        {
            get { return (Color)GetValue(FinalAccentColorProperty); }
            set { SetValue(FinalAccentColorProperty, value); }
        }
        public static readonly DependencyProperty FinalAccentColorProperty =
            DependencyProperty.Register("FinalAccentColor", typeof(Color), typeof(MainWindow), new PropertyMetadata(Color.FromRgb(0x0A, 0x8B, 0xD6)));
        
        public Color FinalFriendlyColor
        {
            get { return (Color)GetValue(FinalFriendlyColorProperty); }
            set { SetValue(FinalFriendlyColorProperty, value); }
        }
        public static readonly DependencyProperty FinalFriendlyColorProperty =
            DependencyProperty.Register("FinalFriendlyColor", typeof(Color), typeof(MainWindow), new PropertyMetadata(Color.FromRgb(0x00, 0xFF, 0x00)));
        
        public Color FinalEnemyColor
        {
            get { return (Color)GetValue(FinalEnemyColorProperty); }
            set { SetValue(FinalEnemyColorProperty, value); }
        }
        public static readonly DependencyProperty FinalEnemyColorProperty =
            DependencyProperty.Register("FinalEnemyColor", typeof(Color), typeof(MainWindow), new PropertyMetadata(Color.FromRgb(0xFF, 0x00, 0x00)));
        
        #endregion

        //initializes the GraphicsConfigurationOverride.xml and prepares the needed elements
        private void InitializeXML()
        {

            if (!File.Exists(XML_PATH))
            {
                
                XDocument doc = XDocument.Parse(Properties.Resources.DefaultXML);
                doc.Save(XML_PATH);

            }
            else
            {

                XDocument doc = XDocument.Load(XML_PATH);

                if(doc.Element("GraphicsConfig") == null)
                {
                    doc.Add(new XElement("GraphicsConfig",
                                    new XElement("GUIColour", 
                                        new XElement("Default",
                                            new XElement("LocalisationName", "Standard"),
                                            new XElement("MatrixRed", " 1, 0, 0 "),
                                            new XElement("MatrixGreen", " 0, 1, 0 "),
                                            new XElement("MatrixBlue", " 0, 0, 1 ")
                                        )
                                    )
                        ));
                }
                //enshure the right elements are present
                else
                {

                    if(doc.Element("GraphicsConfig").Element("GUIColour") == null)
                    {

                        doc.Element("GraphicsConfig").Add(new XElement("GUIColour",
                                        new XElement("Default",
                                            new XElement("LocalisationName", "Standard"),
                                            new XElement("MatrixRed", " 1, 0, 0 "),
                                            new XElement("MatrixGreen", " 0, 1, 0 "),
                                            new XElement("MatrixBlue", " 0, 0, 1 ")
                                        )
                                    ));

                    }
                    else
                    {

                        if(doc.Element("GraphicsConfig").Element("GUIColour").Element("Default") == null)
                        {
                            doc.Element("GraphicsConfig").Element("GUIColour").Add(new XElement("Default",
                                                                                        new XElement("LocalisationName", "Standard"),
                                                                                        new XElement("MatrixRed", " 1, 0, 0 "),
                                                                                        new XElement("MatrixGreen", " 0, 1, 0 "),
                                                                                        new XElement("MatrixBlue", " 0, 0, 1 ")
                                                                                    )
                                                                                );
                        }
                        else
                        {
                            if (doc.Element("GraphicsConfig").Element("GUIColour").Element("Default").Element("LocalisationName") == null)
                            {
                                doc.Element("GraphicsConfig").Element("GUIColour").Element("Default").Add(new XElement("LocalisationName", "Standard"));
                            }
                            if (doc.Element("GraphicsConfig").Element("GUIColour").Element("Default").Element("MatrixRed") == null)
                            {
                                doc.Element("GraphicsConfig").Element("GUIColour").Element("Default").Add(new XElement("MatrixRed", " 1, 0, 0 "));
                            }
                            if (doc.Element("GraphicsConfig").Element("GUIColour").Element("Default").Element("MatrixGreen") == null)
                            {
                                doc.Element("GraphicsConfig").Element("GUIColour").Element("Default").Add(new XElement("MatrixGreen", " 0, 1, 0 "));
                            }
                            if (doc.Element("GraphicsConfig").Element("GUIColour").Element("Default").Element("MatrixBlue") == null)
                            {
                                doc.Element("GraphicsConfig").Element("GUIColour").Element("Default").Add(new XElement("MatrixBlue", " 0, 0, 1 "));
                            }
                        }

                    }


                }

                doc.Save(XML_PATH);

            }

        }

        private void ParseXML()
        {

            XDocument doc = XDocument.Load(XML_PATH);
            var elem = doc.Element("GraphicsConfig").Element("GUIColour").Element("Default");

            string[] redLine = elem.Element("MatrixRed").Value.Split(',').Select(s => s.Trim()).ToArray();
            string[] greenLine = elem.Element("MatrixGreen").Value.Split(',').Select(s => s.Trim()).ToArray();
            string[] blueLine = elem.Element("MatrixBlue").Value.Split(',').Select(s => s.Trim()).ToArray();

            try
            {
                RR = float.Parse(redLine[0]);
                RG = float.Parse(redLine[1]);
                RB = float.Parse(redLine[2]);

                GR = float.Parse(greenLine[0]);
                GG = float.Parse(greenLine[1]);
                GB = float.Parse(greenLine[2]);

                BR = float.Parse(blueLine[0]);
                BG = float.Parse(blueLine[1]);
                BB = float.Parse(blueLine[2]);

            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Malformed XML! reinitializing...");
                InitializeXML();
                ParseXML();
            }

        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MainWindow)?.MultiplierChanged();
        }

        private void MultiplierChanged()
        {

            float r, g, b;

            r = RR * ((SolidColorBrush)RectInHUD.Fill).Color.ScR + 
                GR * ((SolidColorBrush)RectInHUD.Fill).Color.ScG +
                BR * ((SolidColorBrush)RectInHUD.Fill).Color.ScB;

            g = RG * ((SolidColorBrush)RectInHUD.Fill).Color.ScR +
                GG * ((SolidColorBrush)RectInHUD.Fill).Color.ScG +
                BG * ((SolidColorBrush)RectInHUD.Fill).Color.ScB;

            b = RB * ((SolidColorBrush)RectInHUD.Fill).Color.ScR +
                GB * ((SolidColorBrush)RectInHUD.Fill).Color.ScG +
                BB * ((SolidColorBrush)RectInHUD.Fill).Color.ScB;

            FinalHUDColor = Color.FromScRgb(1, r, g, b);


            r = RR * ((SolidColorBrush)RectInAccent.Fill).Color.ScR +
                GR * ((SolidColorBrush)RectInAccent.Fill).Color.ScG +
                BR * ((SolidColorBrush)RectInAccent.Fill).Color.ScB;

            g = RG * ((SolidColorBrush)RectInAccent.Fill).Color.ScR +
                GG * ((SolidColorBrush)RectInAccent.Fill).Color.ScG +
                BG * ((SolidColorBrush)RectInAccent.Fill).Color.ScB;

            b = RB * ((SolidColorBrush)RectInAccent.Fill).Color.ScR +
                GB * ((SolidColorBrush)RectInAccent.Fill).Color.ScG +
                BB * ((SolidColorBrush)RectInAccent.Fill).Color.ScB;

            FinalAccentColor = Color.FromScRgb(1, r, g, b);


            r = RR * ((SolidColorBrush)RectInFriendly.Fill).Color.ScR +
                GR * ((SolidColorBrush)RectInFriendly.Fill).Color.ScG +
                BR * ((SolidColorBrush)RectInFriendly.Fill).Color.ScB;

            g = RG * ((SolidColorBrush)RectInFriendly.Fill).Color.ScR +
                GG * ((SolidColorBrush)RectInFriendly.Fill).Color.ScG +
                BG * ((SolidColorBrush)RectInFriendly.Fill).Color.ScB;

            b = RB * ((SolidColorBrush)RectInFriendly.Fill).Color.ScR +
                GB * ((SolidColorBrush)RectInFriendly.Fill).Color.ScG +
                BB * ((SolidColorBrush)RectInFriendly.Fill).Color.ScB;

            FinalFriendlyColor = Color.FromScRgb(1, r, g, b);

            r = RR * ((SolidColorBrush)RectInEnemy.Fill).Color.ScR +
                GR * ((SolidColorBrush)RectInEnemy.Fill).Color.ScG +
                BR * ((SolidColorBrush)RectInEnemy.Fill).Color.ScB;

            g = RG * ((SolidColorBrush)RectInEnemy.Fill).Color.ScR +
                GG * ((SolidColorBrush)RectInEnemy.Fill).Color.ScG +
                BG * ((SolidColorBrush)RectInEnemy.Fill).Color.ScB;

            b = RB * ((SolidColorBrush)RectInEnemy.Fill).Color.ScR +
                GB * ((SolidColorBrush)RectInEnemy.Fill).Color.ScG +
                BB * ((SolidColorBrush)RectInEnemy.Fill).Color.ScB;

            FinalEnemyColor = Color.FromScRgb(1, r, g, b);

        }

        private void Save(object sender, RoutedEventArgs e)
        {

            XDocument doc = XDocument.Load(XML_PATH);
            var elem = doc.Element("GraphicsConfig").Element("GUIColour").Element("Default");

            elem.Element("MatrixRed").Value = $" {RR}, {RG}, {RB} ";
            elem.Element("MatrixGreen").Value = $" {GR}, {GG}, {GB} ";
            elem.Element("MatrixBlue").Value = $" {BR}, {BG}, {BB} ";

            doc.Save(XML_PATH);

        }

        private void Reset(object sender, RoutedEventArgs e)
        {

            RR = 1f;
            RG = 0f;
            RB = 0f;

            GR = 0f;
            GG = 1f;
            GB = 0f;


            BR = 0f;
            BG = 0f;
            BB = 1f;

        }

    }
}
