using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ZedGraph;

namespace Stocks_
{
    public partial class Form1 : Form
    {
       
        Random rnd = new Random();
        PointPairList list1 = new PointPairList();
        PointPairList list2 = new PointPairList();
        PointPairList list3 = new PointPairList();
        PointPairList list4 = new PointPairList();
        PointPairList list5 = new PointPairList();
        PointPairList list6 = new PointPairList();
        PointPairList list7 = new PointPairList();


        double x = 0;

        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 500;
            t.Tick += new EventHandler(timer_Tick);
            t.Start();

            CreateGraph(zedGraph);
            CreateGraph(zedGraphControl1);
            CreateGraph(zedGraphControl2);
            CreateGraph(zedGraphControl3);
            CreateGraph(zedGraphControl4);
            CreateGraph(zedGraphControl5);
           
            zedGraph.GraphPane.YAxis.Scale.Max = 5;
            zedGraphControl1.GraphPane.YAxis.Scale.Max = 5;
            zedGraphControl2.GraphPane.YAxis.Scale.Max = 5;
            zedGraphControl3.GraphPane.YAxis.Scale.Max = 5;
            zedGraphControl4.GraphPane.YAxis.Scale.Max = 5;
            zedGraphControl5.GraphPane.YAxis.Scale.Max = 5;
        }



        void timer_Tick(object sender, EventArgs e)
        {
           
            double rand = rnd.Next(3);
            double rand1 = rnd.Next(4);
            double rand2 = rnd.Next(2);
            double rand3 = rnd.Next(2);
            double rand4 = rnd.Next(5);
            double rand5 = rnd.Next(3);

            x = x + 0.2;
            list1.Add(x, rand);
            list3.Add(x, rand1);
            list4.Add(x, rand2);
            list5.Add(x, rand3);
            list6.Add(x, rand4);
            list7.Add(x, rand5);
          

            zedGraph.GraphPane.XAxis.Scale.Max = x + 2;
            zedGraphControl1.GraphPane.XAxis.Scale.Max = x + 2;
            zedGraphControl2.GraphPane.XAxis.Scale.Max = x + 2;
            zedGraphControl3.GraphPane.XAxis.Scale.Max = x + 2;
            zedGraphControl4.GraphPane.XAxis.Scale.Max = x + 2;
            zedGraphControl5.GraphPane.XAxis.Scale.Max = x + 2;

            if (x > 10) 
            {
                zedGraph.GraphPane.XAxis.Scale.Min = x - 10;
                zedGraphControl1.GraphPane.XAxis.Scale.Min = x - 10;
                zedGraphControl2.GraphPane.XAxis.Scale.Min = x - 10;
                zedGraphControl3.GraphPane.XAxis.Scale.Min = x - 10;
                zedGraphControl4.GraphPane.XAxis.Scale.Min = x - 10;
                zedGraphControl5.GraphPane.XAxis.Scale.Min = x - 10;
            }
            

            zedGraph.Invalidate();
            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
            zedGraphControl3.Invalidate();
            zedGraphControl4.Invalidate();
            zedGraphControl5.Invalidate();
 
         
        }

       
        private void zedGraph_Load(object sender, EventArgs e)
        {
            
        }
        private void CreateGraph(ZedGraphControl zgc)
        {

            
            LineItem myCurve = zedGraph.GraphPane.AddCurve("Google",
            list1, Color.Red, SymbolType.Diamond);

            LineItem myCurve1 = zedGraphControl1.GraphPane.AddCurve("Facebook",
            list3, Color.Red, SymbolType.Diamond);

            LineItem myCurve2 = zedGraphControl2.GraphPane.AddCurve("Intel",
            list4, Color.Red, SymbolType.Diamond);

            LineItem myCurve3 = zedGraphControl3.GraphPane.AddCurve("SAP",
            list5, Color.Red, SymbolType.Diamond);

            LineItem myCurve4 = zedGraphControl4.GraphPane.AddCurve("HP",
            list6, Color.Red, SymbolType.Diamond);

            LineItem myCurve5 = zedGraphControl5.GraphPane.AddCurve("Yahoo",
            list7, Color.Red, SymbolType.Diamond);

            
        }

        

        private void zedGraph_Load_1(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


 
      
      
    }
}

