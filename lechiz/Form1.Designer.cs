using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Data;
namespace lechiz
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new System.Drawing.Size(1000,1000);
            this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.Text = "Duck Hunter";
            this.Paint += Form1_Paint;
            this.MouseMove += Form1_MouseMove;
            this.Click += Form1_Click;
            //this.BackgroundImage = Image.FromFile("stage.png");
            //this.BackgroundImageLayout = ImageLayout.Stretch;

            this.check.Size = new Size(50, 50);
            this.check.Location = new Point(50, 50);
            this.Controls.Add(check);
            //this.Cursor.Dispose();

            //this.duck_image = new Bitmap("duck.png");
            this.duck_image = Image.FromFile("duck.png");
            //this.duck_image.
            //this.duck_picBox.Image = duck_image;
            //this.ducks.BackColor = Color.Transparent;

            this.start_game_btn.Location = new Point(100, 100);
            this.start_game_btn.Size = new Size(300, 300);
            this.start_game_btn.Text = "Start Game";
            this.start_game_btn.Click += TimerEventDuck;
            this.Controls.Add(start_game_btn);

            this.DoubleBuffered = true;


        }

        private void Form1_Click(object sender, EventArgs e)
        {
            cord_point.Add(new Point(Cursor.Position.X - 10, Cursor.Position.Y - 10));
            
            
        }
        Random rand_duck = new Random();
        Point elipse_location = new Point();

        Rectangle shut_position = new Rectangle();
        //bool isAlive = true;
        Image duck_image;
        static int interval_spawn_duck = 5000;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Flush();
            GC.Collect();
            DrawImage2FloatRectF(e);
            //this.elipse_location.X = 20;
            //this.elipse_location.Y = 20;
            Pen pen_line = new Pen(Color.Red, 5);
            Pen pen_elipse = new Pen(Color.Black, 5);
            //e.Graphics.DrawImage(Image.FromFile("duck.png"), duck_position);

            foreach (Point point in cord_point)
            {
                //this.check.Text = point.X.ToString();
                e.Graphics.FillEllipse(Brushes.Red,
                point.X, point.Y, 20, 20);
                shut_position.X = point.X;
                shut_position.Y = point.Y;
                shut_position.Width = 20;
                shut_position.Height = 20;
                //this.check.Text = cord_point.Count.ToString();
                
            }
            e.Graphics.DrawEllipse(pen_elipse,
                elipse_location.X, elipse_location.Y, 40, 40);
            e.Graphics.DrawLine(pen_line, elipse_location.X + 30, elipse_location.Y + 20, elipse_location.X + 60, elipse_location.Y + 20);
            e.Graphics.DrawLine(pen_line, elipse_location.X + 10, elipse_location.Y + 20, elipse_location.X - 20, elipse_location.Y + 20);
            e.Graphics.DrawLine(pen_line, elipse_location.X + 20, elipse_location.Y + 30, elipse_location.X + 20, elipse_location.Y + 60);
            e.Graphics.DrawLine(pen_line, elipse_location.X + 20, elipse_location.Y + 10, elipse_location.X + 20, elipse_location.Y - 20);


            //e.Graphics.Clear(Color.AliceBlue);
            //e.Graphics.Flush();
            //e.Graphics.DrawLine(pen);



            

        }

        private void TimerEventDuck(Object myObject,EventArgs myEventArgs)
        {
            this.Controls.Remove(start_game_btn);
            timer_duck.Interval = interval_spawn_duck;
            timer_duck.Tick += Timer_duck_Tick;
            timer_duck.Start();
            //timer_duck.Stop();

            //// Displays a message box asking whether to continue running the timer.
            //if (MessageBox.Show("Continue running?", "Count is: " + alarmCounter,
            //   MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    // Restarts the timer and increments the counter.
            //    alarmCounter += 1;
            //    //myTimer.Enabled = true;
            //}
            //else
            //{
            //    // Stops the timer.
            //    exitFlag = true;
            //}
        }

        private void Timer_duck_Tick(object sender, EventArgs e)
        {
            duck_colection.Add(new C_duck(duck_image, 1, rand_duck.Next(50,1800), rand_duck.Next(790, 850)));
            this.Invalidate();

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            elipse_location.X = e.Location.X - 20;
            elipse_location.Y = e.Location.Y - 20;
            //запустить Paint()
            this.check.Text = string.Format("X = {0}\nY = {1}", e.X.ToString(), e.Y.ToString());
            this.Invalidate();
            GC.Collect();
        }
        public void DrawImage2FloatRectF(PaintEventArgs e)
        {

            // Create image.
            //Image newImage = Image.FromFile("duck.png");



            foreach (C_duck duck in duck_colection)
            {
                
                if (check_kill(duck) == true)
                {
                    duck_colection.Remove(duck);
                    break;
                }
                e.Graphics.DrawImage(duck.duck_img,duck.cords);
                //this.check.Text = duck.cords.Location.ToString();
            }
            if (duck_colection.Count >= MAX_DUCK)
            {
                timer_duck.Stop();
                //MessageBox.Show("END GAME");
                end_game();
            }
                

               

            
                //MessageBox.Show("duck!");
                
                //new_duck();
                //duck_colection.Add(newImage);
                //foreach (C_duck duck in duck_colection)
                //{
                //    //this.check.Text =
                //    e.Graphics.DrawImage(duck.duck_img, duck_position.X + 50, duck_position.Y + 20, 50, 50);
                //}
                //this.check.Text = duck_colection.Count.ToString();
            
            GC.Collect();
        }
        private void end_game()
        {
            const string message =
                "Хотите повторить?";
            const string caption = "Form Closing";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

                
            if (result == DialogResult.No)
            {
                MakeScreenshot();
                this.Close();

            }
        }
        public void MakeScreenshot()
        {
            string names = @"screenshot\" + DateTime.Now.ToString().Replace(".","_").Replace(":","_");
            names += ".jpg";
            // получаем размеры окна рабочего стола
            Rectangle bounds = new Rectangle(this.Location.X,this.Location.Y,this.Size.Width,this.Size.Height);
            

            // создаем пустое изображения размером с экран устройства
            using (var bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                // создаем объект на котором можно рисовать
                using (var g = Graphics.FromImage(bitmap))
                {
                    // перерисовываем экран на наш графический объект
                    //g.CopyFromScreen(100,100,300,300, bounds.Size);
                    g.CopyFromScreen(this.Location,Point.Empty,bounds.Size);
                }

                // сохраняем в файл с форматом JPG
                bitmap.Save(names, ImageFormat.Jpeg);
            }
        }


        bool check_kill(C_duck duck)
        {
            
            if (shut_position.IntersectsWith(duck.cords))
            {
                //MessageBox.Show("Kill duck!");
                GC.Collect(GC.GetGeneration(duck));
                return true;
            }
            return false;
        }
        readonly int MAX_DUCK = 10;
        List<Point> cord_point = new List<Point>();
        List<C_duck> duck_colection = new List<C_duck>();
        Label check = new Label();
        System.Windows.Forms.Timer timer_duck = new System.Windows.Forms.Timer();
        Button start_game_btn = new Button();
        PictureBox duck_picBox = new PictureBox();
        #endregion
    }
}

