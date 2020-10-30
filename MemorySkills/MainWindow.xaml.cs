using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MemorySkills
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int NR_OF_COLS = 4;
        private const int NR_OF_ROWS = 4;

        Ellipse SelectPosition;

        int scoreplayer1 = 0;
        int scoreplayer2 = 0;




        List<int> nummerskaartjes = new List<int>();

        ImageSource imageSourceEersteKaart;


        int currentrow = 0;
        int currentcol = 0;



        Image[,] kaartjes = new Image[NR_OF_COLS, NR_OF_ROWS];

        ImageSource[,] Voorkantkaartjes = new ImageSource[NR_OF_COLS, NR_OF_ROWS];

        int rowEersteKaartje;
        int colEersteKaartje;

        int Spacepressed = 0;
        bool Turnofplayer1 = true;


        public MainWindow()
        {
            InitializeComponent();



            InitializeGameGrid(NR_OF_ROWS, NR_OF_COLS);

            bgimage(NR_OF_ROWS, NR_OF_COLS);

            this.KeyDown += MainWindow_KeyDown;

            SelectPosition = new Ellipse();
            SelectPosition.Fill = Brushes.Red;
            SelectPosition.Height = 10;
            SelectPosition.Width = 10;
            GameGrid.Children.Add(SelectPosition);


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dtTicker;
            dt.Start();

        }

        private void dtTicker(object sender, EventArgs e)
        {

            increment++;
            TimerLabel.Content = Convert.ToString(increment);
        }
        private int increment = 0;
        










        private void OpenWindow(object sender, RoutedEventArgs e)
        {
            MainWindow objMainwindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainwindow.Show();


        }
       
            

        private void GameMovement(object sender, EventArgs e)
        {

            Grid.SetColumn(GameGrid, 0);
            Grid.SetRow(GameGrid, 0);

        }


        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            //textBox1.Text = scoreplayer1.ToString();

            if (e.Key == Key.A)
            {
                //goLeft = true;
                if (currentcol > 0)
                {
                    currentcol--;
                }
               
            }
            if (e.Key == Key.D)
            {
                //goRight = true;
                if (currentcol < 3)
                {
                    currentcol++;

                }
            }
            if (e.Key == Key.W)
            {
                //goUp = true;
                if (currentrow > 0)
                {
                    currentrow--;
                }
            }
            if (e.Key == Key.S)
            {
                //goDown = true;
                if (currentrow < 3)
                {
                    currentrow++;
                }


            }
            if (e.Key == Key.Space)
            {
                if (Spacepressed < 2)
                {


                    Image card = kaartjes[currentcol, currentrow];
                    Image card1 = kaartjes[colEersteKaartje, rowEersteKaartje]; ;
                    int row = ((int[])card.Tag)[1];
                    int col = ((int[])card.Tag)[0];
                    //MessageBox.Show("row: " + row + ", col: " + col);
                    ImageSource front = Voorkantkaartjes[col, row];
                    card.Source = front;
                    Spacepressed++;
                    






                    if (Spacepressed == 1)
                    {
                        rowEersteKaartje = row;
                        colEersteKaartje = col;
                        imageSourceEersteKaart =front;



                    }
                    if (Spacepressed == 2)
                    {
                        if (front.ToString() == imageSourceEersteKaart.ToString())
                        {
                            Console.WriteLine("Gelijke kaartjes");
                            //TODO: Administratie van score bijwerken
                            Spacepressed = 0;
                            if (Turnofplayer1 == true)
                            {
                                
                                scoreplayer1++;
                                label1.Content = Convert.ToString(scoreplayer1);
                            }
                            else
                            {

                                scoreplayer2++;
                                label2.Content = Convert.ToString(scoreplayer2);
                            }
                        }

                        else
                        {
                            //TODO: Timer Kaartjes nog 5seconden laten zien
                            DispatcherTimer timer = new DispatcherTimer();
                            timer.Interval = TimeSpan.FromSeconds(2);
                            timer.Tick += (s, ev) =>
                            {

                                card.Source = new BitmapImage(new Uri("Images/FrozenSpel/FrozenAchterkant.png", UriKind.Relative));
                                card1.Source = new BitmapImage(new Uri("Images/FrozenSpel/FrozenAchterkant.png", UriKind.Relative));
                                timer.Stop();
                                Spacepressed = 0;
                                Turnofplayer1 = !Turnofplayer1;


                            };
                            timer.Start();

                            Console.WriteLine("niet gelijke kaartjes");
                            // Console.WriteLine(Turnofplayer1);
                            // Console.WriteLine(Turnofplayer2);

                        }


                        // Console.WriteLine(Turnofplayer1);
                        // Console.WriteLine(Turnofplayer2);
                    }


                }
               


                
            }
            ShowPos();
        }

        private void ShowPos ()
        {
           

            Grid.SetColumn(SelectPosition, currentcol);
            Grid.SetRow(SelectPosition, currentrow);


        }
        
        

        private void bgimage(int rows, int cols)        
        {
            List<ImageSource> images = GetImageList();
            for (int r= 0; r< rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image backgroundImage = new Image();
                    backgroundImage.Source = new BitmapImage(new Uri("Images/FrozenSpel/FrozenAchterkant.png", UriKind.Relative));
                    backgroundImage.Tag = new int[] { c, r };
                   
                   // backgroundImage.MouseDown += new MouseButtonEventHandler(CardClick);
                    Grid.SetColumn(backgroundImage,c);
                    Grid.SetRow(backgroundImage,r);
                    GameGrid.Children.Add(backgroundImage);
                    kaartjes[c, r] = backgroundImage;
                    Voorkantkaartjes[c, r] = images.First();
                    images.RemoveAt(0);
                }
            }
           
        }

        //public bool checkcards()
        //{
        //    //using ()
        //    {
        //        if (this.TurnCount[0].Tag != this.TurnCount[1].Tag)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //}

        //private void CardClick(object sender, MouseButtonEventArgs e)
        //{

        //    Image card = (Image)sender;
        //    ImageSource front = (ImageSource)card.Tag;
        //    card.Source = front;
        //    TurnCount.Add(card);
        //}

        //     if (this.TurnCount.Count == 2)
        //    {
        //        Task.Delay(200).ContinueWith(x =>
        //        {
        //            if (!this.checkcards())
        //            {
        //                Image reverseStateNewCard = (Image)sender;
        //                Image reverseStateOldCard = (Image)sender;
        //                ImageSource frontOfReversedState = new BitmapImage(new Uri("Images/FrozenSpel/frozen1.png", UriKind.Relative));
        //                this.TurnCount[0].Source = frontOfReversedState;
        //                this.TurnCount[1].Source = frontOfReversedState;
        //            }
        //        });  
        //    }

        //}

        private void Spacebar(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Image card = (Image)sender;
                ImageSource front = (ImageSource)card.Tag;
                card.Source = front;
            }
           
        }

        private List<ImageSource> GetImageList()
        {
            List<ImageSource> images = new List<ImageSource>();
            
            for (int i = 0; i < 16; i++)
            {
                int imageNr = i % 8 + 1;
                ImageSource source = new BitmapImage(new Uri("Images/FrozenSpel/" + "frozen" + imageNr + ".png", UriKind.Relative));
                images.Add(source);
                nummerskaartjes.Add(imageNr);
               
            }

            Random rnd = new Random();
            for (int i = 0; i < images.Count; i++)
            {
                int randomIndex = rnd.Next(images.Count);
                ImageSource temp = images[i]; //0
                images[i] = images[randomIndex]; // 0 word random tussen 0-15 voorbeeld 5
                images[randomIndex] = temp;//random getal tussen 0-15 voorveeld 5 = temp
                int tempnummer = nummerskaartjes[i]; //0
                nummerskaartjes[i] = nummerskaartjes[randomIndex]; // 0 word random tussen 0-15 voorbeeld 5
                nummerskaartjes[randomIndex] = tempnummer;//random getal tussen 0-15 voorveeld 5 = temp
            }
            
            return images;
        }

    private void InitializeGameGrid(int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < cols; i++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

    
        }
     
    }
}
