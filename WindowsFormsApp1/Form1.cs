using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.FormGestionInventario;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Image CloseImage;
        Point _imageLocation = new Point(20, 4);
        Point imageHitArea = new Point(20, 4);

        private void Form1_Load(object sender, EventArgs e)
        {
            CloseImage = Properties.Resources.Close;
            tabControl1.Padding = new Point(20, 4);
        }

        public Form1()
        {
            InitializeComponent();
            this.Icon = Icon.FromHandle(new Bitmap(Properties.Resources.Stonks, 32,32).GetHicon());

            panelGestionInventario.Height= 0;
            panel2.Height = 0;
            panel3.Height = 0;

            tabPageHome.MouseEnter += (s, e) => tabPageHome.Focus();

        }

        Panel panel;
        internal enum Panel {panelGestionInventario,panel2,panel3 }

        #region Botones
        private void btnGestionInventario_Click(object sender, EventArgs e)
        {
            if ((panelGestionInventario.Height == 0 || panelGestionInventario.Height == 90)
            && (panel2.Height == 0 || panel2.Height == 90)
            && (panel3.Height == 0 || panel3.Height == 90)
            )
            {
                panel = Panel.panelGestionInventario;
                if (panelGestionInventario.Height == 0)
                {
                    timer1.Start();
                }
                else if (panelGestionInventario.Height == 90)
                {
                    timer2.Start();
                }
            }
        }
        #region Group Gestion Inventario
        private void btn1_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormDemandaIndependiente(), "D. Independiente");
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormDemandaDependiente(), "MRP");
        }
        #endregion
        private void btnGestionRecursosHumanos_Click(object sender, EventArgs e)
        {
            if ((panelGestionInventario.Height == 0 || panelGestionInventario.Height == 90)
            && (panel2.Height == 0 || panel2.Height == 90)
            && (panel3.Height == 0 || panel3.Height == 90)
                )
            {
                panel = Panel.panel2;
                if (panel2.Height == 0)
                {
                    timer1.Start();
                }
                else if (panel2.Height == 90)
                {
                    timer2.Start();
                }
            }
        }

        private void btnGestionProcesos_Click(object sender, EventArgs e)
        {
            if ((panelGestionInventario.Height == 0 || panelGestionInventario.Height == 90)
            && (panel2.Height == 0 || panel2.Height == 90)
            && (panel3.Height == 0 || panel3.Height == 90)
            )
            {
                panel = Panel.panel3;
                if (panel3.Height == 0)
                {
                    timer1.Start();
                }
                else if (panel3.Height == 90)
                {
                    timer2.Start();
                }
            }
        }
        #endregion

        #region Timers
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (panel)
            {
                case Panel.panelGestionInventario:
                    if (panelGestionInventario.Height < 90)
                    {
                        panelGestionInventario.Height=panelGestionInventario.Height+5;
                    }
                    else
                    {
                        btn1.TabStop = true;
                        btn2.TabStop = true;
                        btn3.TabStop = true;
                        timer1.Stop();
                    }
                    break;
                case Panel.panel2:
                    if (panel2.Height < 90)
                    {
                        panel2.Height++;
                    }
                    else
                    {
                        btn4.TabStop = true;
                        btn5.TabStop = true;
                        btn6.TabStop = true;
                        timer1.Stop();
                    }
                    break;

                case Panel.panel3:
                    if (panel3.Height < 90)
                    {
                        panel3.Height++;
                    }
                    else
                    {
                        btn7.TabStop = true;
                        btn8.TabStop = true;
                        btn9.TabStop = true;
                        timer1.Stop();
                    }
                    break;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            switch (panel)
            {
                case Panel.panelGestionInventario:
                    if (panelGestionInventario.Height > 0)
                    {
                        panelGestionInventario.Height=panelGestionInventario.Height-5;
                    }
                    else
                    {
                        btn1.TabStop = false;
                        btn2.TabStop = false;
                        btn3.TabStop = false;
                        timer2.Stop();
                    }
                    break;
                case Panel.panel2:
                    if (panel2.Height > 0)
                    {
                        panel2.Height--;
                    }
                    else
                    {
                        btn4.TabStop = false;
                        btn5.TabStop = false;
                        btn6.TabStop = false;
                        timer2.Stop();
                    }
                    break;

                case Panel.panel3:
                    if (panel3.Height > 0)
                    {
                        panel3.Height--;
                    }
                    else
                    {
                        btn7.TabStop = false;
                        btn8.TabStop = false;
                        btn9.TabStop = false;
                        timer2.Stop();
                    }
                    break;
            }
        }

        #endregion

        #region TabControl
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Image img;

            img = new Bitmap(CloseImage);

            Rectangle r = e.Bounds;
            r = this.tabControl1.GetTabRect(e.Index);
            r.Offset(2, 2);
            Brush TitleBrush = new SolidBrush(Color.Black);
            Font f = this.Font;
            string title = this.tabControl1.TabPages[e.Index].Text;
            e.Graphics.DrawString(title, f, TitleBrush, new PointF(r.X, r.Y));
            e.Graphics.DrawImage(img, new Point(r.X + (
                this.tabControl1.GetTabRect(e.Index).Width - _imageLocation.X),
                _imageLocation.Y));
        }
        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            Point p = e.Location;
            int _tabWidth = 0;
            _tabWidth = this.tabControl1.GetTabRect(tabControl.SelectedIndex).Width - (imageHitArea.X);
            Rectangle r = this.tabControl1.GetTabRect(tabControl.SelectedIndex);
            r.Offset(_tabWidth, imageHitArea.Y);
            r.Width = 16;
            r.Height = 16;

            if (r.Contains(p))
            {
                TabPage tabPage = (TabPage)tabControl.TabPages[tabControl.SelectedIndex];
                if (tabPage.Name != "tabPageHome")
                {
                    tabControl.TabPages.Remove(tabPage);
                }
            }
            else
            {
                //((TabPage)tabControl.TabPages[tabControl.SelectedIndex]).Focus();

                //tabControl1.SelectedTab = (TabPage)tabControl.TabPages[tabControl.SelectedIndex];
            }
        }
        #endregion

        private void AbrirFormularioHijo(Form formulario, string titulo)
        {
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            
            TabPage tab = new TabPage();
            tab.Text = titulo;

            tab.Controls.Add(formulario); 
            tab.Tag = formulario;

            tabControl1.Controls.Add(tab);
        
            formulario.BringToFront();
            formulario.Show();

            tabControl1.SelectedTab = tab;

            //tab.Click += (s,e) => tab.Focus();
            //tab.MouseClick += (s, e) => tab.Focus();
            //tab.MouseEnter += (s, e) => tab.Focus();
        }

    }
}
