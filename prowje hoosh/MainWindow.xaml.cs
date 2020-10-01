using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;


namespace prowje_hoosh
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        
        List<Line> ll = new List<Line>();
        List<Line> lines = new List<Line>();
        List<Point> lp = new List<Point>();
        Point newposition = new Point();
        Point currentPoint = new Point();
        int PointCount;
        bool isClicked = false;
        public Window1()
        {

            InitializeComponent();
            for (int i = 0; i < 1000; i++)
            {
                lbox_Copy.Items.Add(i.ToString());
                lbox.Items.Add(i.ToString());
            }
            lbox.SelectedIndex = 0;
            lbox_Copy.SelectedIndex = 0;
        }
        int[,] array;

        private int pov;

        private void Canvas_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (isCircle)
            {
                if (PointCount >= lbox.SelectedIndex)
                {
                    TB.Content = "game started draw your line";
                    array = new int[PointCount, PointCount];
                    pov = PointCount - 1;
                    isCircle = false;
                    if (StarTurn == "computer")
                    {
                        Random ranndom = new Random();
                        var randm = (ranndom.Next(0, lp.Count));
                        var p1 = lp[randm];
                        var p2 = lp[ranndom.Next(0, lp.Count)];
                        if (p1.Equals(p2))
                            Canvas_MouseDown_1(sender, e);
                        Line line = new Line()
                        {
                            X1 = p1.X,
                            X2 = p2.X,
                            Y1 = p1.Y,
                            Y2 = p2.Y,
                            Stroke = Brushes.Red
                        };
                        TURN = "player";
                        lines.Add(line);
                        paintSurface.Children.Add(line);
                        funcArray(p1, p2, true);
                    }
                }
                else
                {

                    PointCount++;

                    Ellipse circle = new Ellipse();
                    circle.Margin = new Thickness(e.GetPosition(paintSurface).X, e.GetPosition(paintSurface).Y, 0, 0);
                    circle.Width = 7;
                    circle.Height = 7;
                    circle.Stroke = Brushes.Black;
                    circle.StrokeThickness = 2;
                    circle.Fill = Brushes.GreenYellow;
                    lp.Add(new Point(e.GetPosition(paintSurface).X + circle.Width / 2, e.GetPosition(paintSurface).Y + circle.Height / 2));
                    paintSurface.Children.Add(circle);
                    if (PointCount >= lbox.SelectedIndex)
                        Canvas_MouseDown_1(sender, e);

                }

            }
            else if (e.ButtonState == MouseButtonState.Pressed)
            {
                currentPoint.getPoint(e.GetPosition(paintSurface));
                isClicked = true;
                newposition.getPoint(e.GetPosition(paintSurface));

            }
        }
        private void Canvas_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isCircle)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Line line = new Line();

                    if (TURN == "computer")
                        line.Stroke = Brushes.OrangeRed;
                    else
                        line.Stroke = Brushes.BlueViolet;
                    line.X1 = newposition.X;
                    line.Y1 = newposition.Y;
                    line.X2 = e.GetPosition(paintSurface).X;
                    line.Y2 = e.GetPosition(paintSurface).Y;
                    newposition.getPoint(e.GetPosition(paintSurface));
                    ll.Add(line);
                    paintSurface.Children.Add(line);
                }
            }
        }

        string StarTurn;
        private void lbox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
           
            if (e.VerticalChange > 0)
            {
                int a = (int)e.VerticalChange;
                lbox.SelectedItem = lbox.Items[lbox.SelectedIndex + a];
            }
            else if (e.VerticalChange < 0)
            {

                int a = (int)e.VerticalChange;
                lbox.SelectedItem = lbox.Items[lbox.SelectedIndex + a];
            }
           
        }
        bool isCircle = false;
        string TURN;
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TB.Content = "please draw points";
            isCircle = true;
            paintSurface.Children.Clear();
            lp.Clear();
            lines.Clear();
            TURN = StarTurn;
            PointCount = 0;
            array = new int[PointCount, PointCount];
            finish = false;
        }
        private int[,] isFinal(int[,] Array ,bool maximizing)
        {
            int count = 0;
                    int[] iEs=new int[pov+1];
                    int[] jEs= new int[pov + 1];
                    int[] iEsMIN = new int[pov + 1];
                    int[] jEsMIN = new int[pov + 1];
            for (int i = 0; i < pov + 1; i++)
            {
                for (int j = i + 1; j < pov + 1; j++)
                {
                    if (Array[i, j] != 0)
                    {
                        count++;
                        if (Array[i, j] ==1)
                        {
                            iEs[i]++;
                            jEs[j]++;
                        }
                       else if (Array[i, j] == -1)
                        {
                            iEsMIN[i]++;
                            jEsMIN[j]++;
                        }
                        if (count >= lbox_Copy.SelectedIndex)
                        {
                            for (int ee = 1; ee < iEs.Length; ee++)
                            {if(iEs[ee]>1)
                                iEs[0] += iEs[ee];
                            }
                            for (int ww = 1; ww < iEs.Length; ww++)
                            {
                                if (jEs[ww] > 1)
                                    jEs[0] += jEs[ww];
                            }
                            for (int ee = 1; ee < iEs.Length; ee++)
                            {
                                if (iEsMIN[ee] > 1)
                                    iEsMIN[0] += iEsMIN[ee];
                            }
                            for (int ww = 1; ww < iEs.Length; ww++)
                            {
                                if (jEsMIN[ww] > 1)
                                    jEsMIN[0] += jEsMIN[ww];
                            }
                            if (maximizing)
                                if (iEs[0]+jEs[0]>=iEsMIN[0]+jEsMIN[0])
                                    Array[pov, 0] = 1;
                                else
                                    Array[pov, 0] = -1;
                            else if (!maximizing)
                                if (iEs[0] + jEs[0] <= iEsMIN[0] + jEsMIN[0])
                                    Array[pov, 0] = 1;
                                else
                                    Array[pov, 0] = -1;
                            i = pov + 1;
                            break;
                        }
                    }
                }
            }
                return Array;
        }
        //private bool linesAccident(int[,] Array)
        //{
        //    for (int i = 0; i < lp.Count; i++)
        //    {
        //        for (int j = 0; j < lp.Count; j++)
        //        {
        //            for (int k = 0; k < lp.Count; k++)
        //            {
        //                for (int l = 0; l < lp.Count; l++)
        //                {
        //                    if ((i != j && k != i && i != l && k != j && j != l && l != k))
        //                        if(lp[i].conLines.Any(x => lp[j].conLines.Contains(x)) && lp[j].conLines.Any(u => lp[k].conLines.Contains(u)) && lp[k].conLines.Any(g => lp[l].conLines.Contains(g)) &&
        //                       ( lines.Exists(z => z.X1 == lp[i].X && z.Y1 == lp[i].Y && z.X2 == lp[j].X && z.Y2 == lp[j].Y) &&
        //                        lines.Exists(z => z.X1 == lp[j].X && z.Y1 == lp[j].Y && z.X2 == lp[k].X && z.Y2 == lp[k].Y) 
        //                        && lines.Exists(z => z.X1 == lp[k].X && z.Y1 == lp[k].Y && z.X2 == lp[l].X && z.Y2 == lp[l].Y)  ||
        //                        lines.Exists(z => z.X1 == lp[i].X && z.Y1 == lp[i].Y && z.X2 == lp[j].X && z.Y2 == lp[j].Y) &&
        //                        lines.Exists(z => z.X1 == lp[j].X && z.Y1 == lp[j].Y && z.X2 == lp[k].X && z.Y2 == lp[k].Y)
        //                        && lines.Exists(z => z.X1 == lp[k].X && z.Y1 == lp[k].Y && z.X2 == lp[l].X && z.Y2 == lp[l].Y)))
        //                        {
        //                            if (lines.Where(z => z.X1 == lp[i].X && z.Y1 == lp[i].Y && z.X2 == lp[j].X && z.Y2 == lp[j].Y).Single().Stroke == Brushes.Blue &&
        //                        lines.Where(z => z.X1 == lp[j].X && z.Y1 == lp[j].Y && z.X2 == lp[k].X && z.Y2 == lp[k].Y).Single().Stroke == Brushes.Blue
        //                        && lines.Where(z => z.X1 == lp[k].X && z.Y1 == lp[k].Y && z.X2 == lp[l].X && z.Y2 == lp[l].Y).Single().Stroke == Brushes.Blue ||
        //                        lines.Where(z => z.X1 == lp[i].X && z.Y1 == lp[i].Y && z.X2 == lp[j].X && z.Y2 == lp[j].Y).Single().Stroke == Brushes.Red &&
        //                        lines.Where(z => z.X1 == lp[j].X && z.Y1 == lp[j].Y && z.X2 == lp[k].X && z.Y2 == lp[k].Y).Single().Stroke == Brushes.Red
        //                        && lines.Where(z => z.X1 == lp[k].X && z.Y1 == lp[k].Y && z.X2 == lp[l].X && z.Y2 == lp[l].Y).Single().Stroke == Brushes.Red)
        //                            {

        //                            if (Math.Abs((i - j) + (l - k)) > lp.Count)
        //                                return true;
        //                            }
        //                        }
        //                }
        //            }
        //        }
        //    }

        //    return false;
        //}

        public int[,] ABpruning(int depth, int nodeIndex, bool maximizingPlayer, int[,] Array, int[,] alpha, int[,] beta)
        {
            //if (linesAccident(Array))
            //{
            //            Array[pov, 0] = 1;
            //    return Array;
            //}


            if (isLoose(Array) != 0)
            {
                if (maximizingPlayer)
                {
                    if (isLoose(Array) == 1)
                        Array[pov, 0] = -1;
                    else if (isLoose(Array) == -1)
                        Array[pov, 0] = 1;
                }
                else if (!maximizingPlayer)
                {
                    if (isLoose(Array) == -1)
                        Array[pov, 0] = 1;
                    else if (isLoose(Array) == 1)
                        Array[pov, 0] = -1;
                }
                return Array;

            }
            else if(depth>lbox_Copy.SelectedIndex)
             return isFinal(Array, maximizingPlayer);
           
            if (maximizingPlayer)
            {
                int[,] best = new int[pov + 1, pov + 1];
                best[pov, 0] = -1;
                for (int i = 0; i < pov + 1; i++)
                {
                    for (int j = i + 1; j < pov + 1; j++)
                    {
                        if (Array[i, j] == 0)
                        {

                            int[,] val = ABpruning(depth + 1, nodeIndex * 2 + i, false, addChildren(i, j, (int[,])Array.Clone(), false), alpha, beta);
                            if (best[pov, 0] <= val[pov, 0])
                                best = val;

                            if (best[pov, 0] > alpha[pov, 0])
                                alpha = best;

                            if (beta[pov, 0] <= alpha[pov, 0])
                                break;
                        }
                    }
                }
                return best;
            }
            else
            {
                int[,] best = new int[pov + 1, pov + 1];
                best[pov, 0] = 1;
                for (int i = 0; i < pov + 1; i++)
                {
                    for (int j = i + 1; j < pov + 1; j++)
                    {
                        if (Array[i, j] == 0)
                        {
                            int[,] val = ABpruning(depth + 1, nodeIndex * 2 + i, true, addChildren(i, j, (int[,])Array.Clone(), true), alpha, beta);
                            if (best[pov, 0] >= val[pov, 0])
                                best = val;
                            if (beta[pov, 0] > best[pov, 0])
                                beta = best;

                            if (beta[pov, 0] <= alpha[pov, 0])
                                break;
                        }
                    }
                }
                return best;
            }
        }

        private int[,] addChildren(int i, int j, int[,] Array, bool isMax)
        {


            if (Array[i, j] == 0)
            {
                if (isMax)
                    Array[i, j] = -1;
                else
                    Array[i, j] = 1;

                if (Array[pov, 1] == 0 && Array[pov, 2] == 0)
                {

                    Array[pov, 1] = i;
                    Array[pov, 2] = j;

                }
            }
            return Array;
        }
        
        private int isLoose(int[,] array)
        {
            for (int i = 0; i < lp.Count; i++)
            {
                for (int j = 0; j < lp.Count; j++)
                {
                    for (int k = 0; k < lp.Count; k++)
                    {

                        if (array[i, j] == 1 && array[j, k] == 1 && array[i, k] == 1)
                        {
                            if ((i != j && k != i && j != k))
                                return 1;
                        }
                        else if (array[i, j] == -1 && array[j, k] == -1 && array[i, k] == -1)
                        {
                            if ((i != j && k != i && j != k))
                                return -1;
                        }
                    }

                }

            }
            return 0;
        }
        private void funcArray(Point point, Point point2, bool isMax)
        {
            if (isMax)
                array[Math.Min(lp.IndexOf(point), lp.IndexOf(point2)), Math.Max(lp.IndexOf(point), lp.IndexOf(point2))] = 1;
            else
                array[Math.Min(lp.IndexOf(point), lp.IndexOf(point2)), Math.Max(lp.IndexOf(point), lp.IndexOf(point2))] = -1;
        }


        private void paintSurface_MouseUp(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < ll.Count; i++)
                paintSurface.Children.Remove(ll[i]);
            if (e.LeftButton == MouseButtonState.Released && isClicked == true)
            {


                for (int i = 0; i < lp.Count; i++)
                {
                    for (int j = 0; j < lp.Count; j++)
                    {

                        if (Math.Sqrt((e.GetPosition(paintSurface).X - lp[i].X) * (e.GetPosition(paintSurface).X - lp[i].X)
                            + (e.GetPosition(paintSurface).Y - lp[i].Y) * (e.GetPosition(paintSurface).Y - lp[i].Y)) < 12
                            && Math.Sqrt((currentPoint.X - lp[j].X) * (currentPoint.X - lp[j].X)
                            + (currentPoint.Y - lp[j].Y) * (currentPoint.Y - lp[j].Y)) < 12)
                        {
                            TB.Content = "";
                            Line line = new Line();

                            line.X1 = lp[i].X;
                            line.Y1 = lp[i].Y;
                            lp[i].conLines.Add(line);
                            line.X2 = lp[j].X;
                            line.Y2 = lp[j].Y;
                            lp[j].conLines.Add(line);
                            line.StrokeThickness = 1.5;
                            currentPoint.getPoint(e.GetPosition(paintSurface));

                            if ((line.X1 != line.X2 && line.Y1 != line.Y2) &&
                                !(lines.Any(x => (x.X1 == line.X1 && x.Y1 == line.Y1 && x.Y2 == line.Y2 && line.X2 == x.X2) ||
                                                  x.X1 == line.X2 && x.Y1 == line.Y2 && x.Y2 == line.Y1 && line.X1 == x.X2)))
                            {
                                line.Stroke = Brushes.Blue;
                                TURN = "computer";
                                lines.Add(line);
                                paintSurface.Children.Add(line);


                                var x = lp.Where(t => t.X == line.X1 && t.Y == line.Y1).Single();
                                var iks = lp.Where(t => t.X == line.X2 && t.Y == line.Y2).Single();

                                array[Math.Min(lp.IndexOf(x), lp.IndexOf(iks)), Math.Max(lp.IndexOf(x), lp.IndexOf(iks))] = -1;


                                if (isLoose(array) == -1)
                                {
                                    MessageBox.Show("computer wins");
                                    finish = true;
                                }
                                else if (isLoose(array) == 1)// || linesAccident(array))
                                {
                                    MessageBox.Show("you win");
                                    finish = true;
                                }
                                var a = new int[pov + 1, pov + 1];
                                a[pov, 0] = -1;
                                var b = new int[pov + 1, pov + 1];
                                b[pov, 0] = 1;
                               // aera.Clear();

                                var zz = ABpruning(1, 1, true, array, a, b);
                                int[,] aaa = new int[pov + 1, pov + 1];
                               // aera.Clear();
                                zz=(addChildren(Math.Min(zz[pov, 2], zz[pov, 1]), Math.Max(zz[pov, 2], zz[pov, 1]), aaa, true));
                                Point pooint = new Point();
                                Point pooint2 = new Point();


                                pooint = lp[Math.Min(zz[pov, 2], zz[pov, 1])];
                                pooint2 = lp[Math.Max(zz[pov, 2], zz[pov, 1])];
                                if (!(lines.Any(dd =>
                               (dd.X1 == pooint.X && dd.Y1 == pooint.Y && dd.Y2 == pooint2.Y && pooint2.X == dd.X2) ||
                               dd.X1 == pooint2.X && dd.Y1 == pooint2.Y && dd.Y2 == pooint.Y && pooint.X == dd.X2)))
                                {
                                    i = zz[pov, 1];
                                    j = zz[pov, 2];
                                }


                                Line lain = new Line()
                                {
                                    Stroke = Brushes.Red,
                                    X1 = pooint.X,
                                    Y1 = pooint.Y,
                                    X2 = pooint2.X,
                                    Y2 = pooint2.Y
                                };
                                lp[i].conLines.Add(lain);
                                lp[j].conLines.Add(lain);
                                lain.StrokeThickness = 1.5;
                                currentPoint.getPoint(e.GetPosition(paintSurface));
                                array[i, j] = 1;
                                lines.Add(lain);
                                paintSurface.Children.Add(lain);

                                if (!finish)
                                {

                                if (isLoose(array) == 1)// || linesAccident(array))
                                    MessageBox.Show("you win");
                                
                                else if (lines.Count == (lp.Count * (lp.Count - 1))/2)
                                    MessageBox.Show("equal");
                                }
                                TURN = "player";

                            }
                            isClicked = false;
                            ll.Clear();
                            i = lp.Count;
                            break;
                        }
                    }
                }
            }
        }
        bool finish=false;
        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            StarTurn = "computer";
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            StarTurn = "player";
        }

        private void lbox_Copy_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange > 0)
            {
                int a = (int)e.VerticalChange;
                lbox_Copy.SelectedItem = lbox_Copy.Items[lbox_Copy.SelectedIndex + a];
            }
            else if (e.VerticalChange < 0)
            {

                int a = (int)e.VerticalChange;
                lbox_Copy.SelectedItem = lbox_Copy.Items[lbox_Copy.SelectedIndex + a];
            }
        }

        private void paintSurface_MouseUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void paintSurface_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void paintSurface_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
