using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace PiggyGame.Objects
{
    public static class DiceGenerator
    {
        //generaza nr random
        private static Random rnd = new Random();
        //generaza un nr random intre 1 si 6
        private static int generateNum()
        {
            return rnd.Next(1, 7);
        }

        //marimea relativa unui punct de pe zar
        private static double DotSize = 6;
        //marimea absoluta a unui punct de pe zar
        private static double GetSize(Canvas cvs)
        {
            return ((cvs.Width / DotSize) + 0);
        }
        //distanta intre puncte si marginile zarului
        private static double GetOffset(double size)
        {
            return size / 2;
        }

        //grosimea bordurii punctului
        private static double DotStroke = 2;

        //culorile pentru punctul de pe zar si bordura lui
        private static SolidColorBrush Stroke = new SolidColorBrush(Colors.Black);
        private static SolidColorBrush Fill = new SolidColorBrush(Colors.Gray);

        //genereaza o elipsa(cerc)(punct de pe zar) in functie de marimea canvasului
        private static Ellipse GetEllipse(Canvas cvs)
        {
            //calculeaza marimea punctului fara grosimea bordurii
            double size = GetSize(cvs) - DotStroke;

            //creeaza elipsa
            Ellipse el = new Ellipse
            {
                Width = size,
                Height = size,

                StrokeThickness = DotStroke,

                Stroke = Stroke,
                Fill = Fill
            };

            return el;
        }

        //punctul din stanga sus
        private static void LeftTop(Ellipse el, Canvas cvs)
        {
            Canvas.SetLeft(el, GetOffset(GetSize(cvs)) * 2);
            Canvas.SetTop(el, GetOffset(GetSize(cvs)) * 2);
            cvs.Children.Add(el);
        }

        //punctul din stanga mijloc
        private static void LeftMid(Ellipse el, Canvas cvs)
        {
            Canvas.SetLeft(el, GetOffset(GetSize(cvs) * 2));
            Canvas.SetTop(el, GetOffset(GetSize(cvs)) * 5);
            cvs.Children.Add(el);
        }

        //punctul din stanga jos
        private static void LeftBottom(Ellipse el, Canvas cvs)
        {
            Canvas.SetLeft(el, GetOffset(GetSize(cvs)) * 2);
            Canvas.SetBottom(el, GetOffset(GetSize(cvs)) * 2);
            cvs.Children.Add(el);
        }

        //punctul din mijloc
        private static void Mid(Ellipse el, Canvas cvs)
        {
            Canvas.SetLeft(el, GetOffset(GetSize(cvs)) * 5);
            Canvas.SetTop(el, GetOffset(GetSize(cvs)) * 5);
            cvs.Children.Add(el);
        }

        //punctul din dreapta sus
        private static void RightTop(Ellipse el, Canvas cvs)
        {
            Canvas.SetRight(el, GetOffset(GetSize(cvs)) * 2);
            Canvas.SetTop(el, GetOffset(GetSize(cvs)) * 2);
            cvs.Children.Add(el);
        }
        //punctul din dreapta mijloc
        private static void RightMid(Ellipse el, Canvas cvs)
        {
            Canvas.SetRight(el, GetOffset(GetSize(cvs)) * 2);
            Canvas.SetTop(el, GetOffset(GetSize(cvs)) * 5);
            cvs.Children.Add(el);
        }
        //punctul din dreapta jos
        private static void RightBottom(Ellipse el, Canvas cvs)
        {
            Canvas.SetRight(el, GetOffset(GetSize(cvs)) * 2);
            Canvas.SetBottom(el, GetOffset(GetSize(cvs)) * 2);
            cvs.Children.Add(el);
        }


        //zarul cu fata 1
        private static void generateOne(Canvas cvs)
        {
            Mid(GetEllipse(cvs), cvs);
        }
        //zarul cu fata 2
        private static void generateTwo(Canvas cvs)
        {
            LeftTop(GetEllipse(cvs), cvs);
            RightBottom(GetEllipse(cvs), cvs);
        }
        //zarul cu fata 3
        private static void generateThree(Canvas cvs)
        {
            LeftTop(GetEllipse(cvs), cvs);
            Mid(GetEllipse(cvs), cvs);
            RightBottom(GetEllipse(cvs), cvs);
        }
        //zarul cu fata 4
        private static void generateFour(Canvas cvs)
        {
            LeftTop(GetEllipse(cvs), cvs);
            LeftBottom(GetEllipse(cvs), cvs);
            RightTop(GetEllipse(cvs), cvs);
            RightBottom(GetEllipse(cvs), cvs);
        }
        //zarul cu fata 5
        private static void generateFive(Canvas cvs)
        {
            LeftTop(GetEllipse(cvs), cvs);
            LeftBottom(GetEllipse(cvs), cvs);
            Mid(GetEllipse(cvs), cvs);
            RightTop(GetEllipse(cvs), cvs);
            RightBottom(GetEllipse(cvs), cvs);
        }
        //zarul cu fata 6
        private static void generateSix(Canvas cvs)
        {
            LeftTop(GetEllipse(cvs), cvs);
            LeftMid(GetEllipse(cvs), cvs);
            LeftBottom(GetEllipse(cvs), cvs);
            RightTop(GetEllipse(cvs), cvs);
            RightMid(GetEllipse(cvs), cvs);
            RightBottom(GetEllipse(cvs), cvs);
        }
        //generaza fata zarului si returneaza valoarea
        public static int Roll(Canvas cvs)
        {
            int nr = generateNum();

            switch (nr)
            {
                case 1:
                    generateOne(cvs);
                    return nr;
                case 2:
                    generateTwo(cvs);
                    return nr;
                case 3:
                    generateThree(cvs);
                    return nr;
                case 4:
                    generateFour(cvs);
                    return nr;
                case 5:
                    generateFive(cvs);
                    return nr;
                case 6:
                    generateSix(cvs);
                    return nr;
                default:
                    return -1;

            }
        }
    }
}
