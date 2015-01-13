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
             
        List<PointPairList> pointList = new List<PointPairList>();
        List<Stock> stocksList = new List<Stock>();

        
        double x = 0;
        int count = 0;
        double highestStock = 0;
        User user = new User("Lord Wimbley", 50000.00);



        public Form1()
        {
            InitializeComponent();

            CreateStock();

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 800;
            t.Tick += new EventHandler(timer_Tick);
            t.Start();


            zedGraph.GraphPane.YAxis.Scale.Max = 5;
            zedGraph.GraphPane.YAxis.Scale.Min = -1;
            
            foreach (Stock stock in stocksList)
            {
                stocksComboBx.Items.Add(stock.StockName);

            }
           
        }




        void timer_Tick(object sender, EventArgs e)
        {
            int stockNo;
            double rand;
            double totalValue = 0;
            double stockValue = 0;


            x += 0.2;

            zedGraph.GraphPane.XAxis.Scale.Max = x + 2;
            if (x > 10)
            {
                zedGraph.GraphPane.XAxis.Scale.Min = x - 7;
            }

            int i = 0;

            //Convert.ToInt16(stocksList.GetEnumerator().Current)

           foreach (Stock stock in stocksList)
            {
                Random rnd = new Random();
                rand = rnd.Next(-5,10);
                              
                pointList.ElementAt(i).Add(x, stock.StockValue + rand/10);
                stock.StockValue += rand/10;
                
                i++;

                if (stock.StockValue + rand / 10 > highestStock)
               {
                   highestStock = stock.StockValue + rand / 10;
                   zedGraph.GraphPane.YAxis.Scale.Max = highestStock+2;
               }
   
            }
             zedGraph.Invalidate();
             userNameBx.Text = user.UserName;
             cashTxt.Text = user.Cash.ToString();


             foreach (KeyValuePair<string, double> userStock in user.portfolio)
             {

                stockValue = stocksList.Find(name => name.StockName.Equals(userStock.Key)).StockValue;
                totalValue += userStock.Value * stockValue;

             }
             stkValueTxt.Text = totalValue.ToString();


             if (stocksComboBx.SelectedItem != null)
             {
                 stockValue = stocksList.Find(name => name.StockName.Equals(stocksComboBx.SelectedItem.ToString())).StockValue;
                 stockNo = stocksList.Find(name => name.StockName.Equals(stocksComboBx.SelectedItem.ToString())).availableStocks;

                 totalStkValueTxt.Text = (stockValue * stockNo).ToString();

                 availSharesTxt.Text = stockNo.ToString();
             }

             

        }


        private void findStock(string stock)
        {

        }
       
        private void createBtn_Click_1(object sender, EventArgs e)
        {
            int num;
   
            if (stkNameTxt.Text != "" && int.TryParse(StkVauleTxt.Text, out num))
            {
                Stock stock = new Stock();

                stock.StockName = stkNameTxt.Text;

                stock.StockValue = num;

                stocksList.Add(stock);

                PointPairList pointPair = new PointPairList();
                pointList.Add(pointPair);

                zedGraph.GraphPane.AddCurve(stock.StockName,
                pointList.ElementAt(count),
                Color.FromName(colurComboBx.SelectedItem.ToString()), SymbolType.Diamond);
                count++;

                stocksComboBx.Items.Add(stock.StockName);
            }
            
        }







        private void CreateStock()
        {
            Stock stock = new Stock();

            stock.StockName = "Monkey Paws";

            stock.StockValue = 2;

            stock.availableStocks = 100;

            stocksList.Add(stock);

            PointPairList pointPair = new PointPairList();
            pointList.Add(pointPair);

            LineItem myCurve = zedGraph.GraphPane.AddCurve(stock.StockName,
            pointList.ElementAt(0),
            Color.Blue, SymbolType.Diamond);
        }


        void createGraph()
        {
            ZedGraph.ZedGraphControl zeddo = new ZedGraph.ZedGraphControl();
            GraphPane myPane = new GraphPane();

            myPane = zeddo.GraphPane;

            myPane.Title.Text = "This is an example!";



            zeddo.AxisChange();
            zeddo.Location = new Point(500, 0);
            Controls.Add(zeddo);
        }

        private void buyBtn_Click(object sender, EventArgs e)
        {
            int num;
            double stockValue;
            int stockNo;
            stockValue = stocksList.Find(name => name.StockName.Equals(stocksComboBx.SelectedItem.ToString())).StockValue;
            stockNo = stocksList.Find(name => name.StockName.Equals(stocksComboBx.SelectedItem.ToString())).availableStocks;


            if (int.TryParse(buyTxtBx.Text, out num) && user.Cash >= num * stockValue && num <= stockNo)
            {
                if (!user.portfolio.ContainsKey(stocksComboBx.SelectedItem.ToString()))
                {
                    user.portfolio.Add(stocksComboBx.SelectedItem.ToString(), num);
                }
                else
                {
                    user.portfolio[stocksComboBx.SelectedItem.ToString()] += num;
                }

                user.Cash -= num * stockValue;
                stocksList.Find(name => name.StockName.Equals(stocksComboBx.SelectedItem.ToString())).availableStocks = stockNo - num;
            }
        }

        private void sellBtn_Click(object sender, EventArgs e)
        {
            int num;
            double stockValue;

            if (int.TryParse(sellTxtBx.Text, out num) && user.portfolio[stocksComboBx.SelectedItem.ToString()] >= num)
            {
                stockValue = stocksList.Find(name => name.StockName.Equals(stocksComboBx.SelectedItem.ToString())).StockValue;

                user.portfolio[stocksComboBx.SelectedItem.ToString()] -= num;

                user.Cash += num * stockValue;

                stocksList.Find(name => name.StockName.Equals(stocksComboBx.SelectedItem.ToString())).availableStocks += num;
            }
        }



    

    }
}
