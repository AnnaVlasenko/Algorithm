using AlgorithmsVizualizator.AStarAlgorithm;
using AlgorithmsVizualizator.Dijkstra_Algorithm;
using AlgorithmsVizualizator.WaveAlgorithm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmsVizualizator
{
    public partial class Form1 : Form
    {
        List<NodesCoordinates> nodesCoordinates = new List<NodesCoordinates>();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load Dijkstra test data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            var g = new Graph();

            //добавление вершин
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddNode("E");
            g.AddNode("F");
            g.AddNode("G");

            //добавление ребер
            g.AddEdge("A", "B", 22);
            this.dataGridView1.Rows.Add("A", "B", 22);
            g.AddEdge("A", "C", 33);
            this.dataGridView1.Rows.Add("A", "C", 33);
            g.AddEdge("A", "D", 61);
            this.dataGridView1.Rows.Add("A", "D", 61);
            g.AddEdge("B", "C", 47);
            this.dataGridView1.Rows.Add("B", "C", 47);
            g.AddEdge("B", "E", 93);
            this.dataGridView1.Rows.Add("B", "E", 93);
            g.AddEdge("C", "D", 11);
            this.dataGridView1.Rows.Add("C", "D", 11);
            g.AddEdge("C", "E", 79);
            this.dataGridView1.Rows.Add("C", "E", 79);
            g.AddEdge("C", "F", 63);
            this.dataGridView1.Rows.Add("C", "F", 63);
            g.AddEdge("D", "F", 41);
            this.dataGridView1.Rows.Add("D", "F", 41);
            g.AddEdge("E", "F", 17);
            this.dataGridView1.Rows.Add("E", "F", 17);
            g.AddEdge("E", "G", 58);
            this.dataGridView1.Rows.Add("E", "G", 58);
            g.AddEdge("F", "G", 84);
            this.dataGridView1.Rows.Add("F", "G", 84);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<DrawEdge> edges = new List<DrawEdge>();

            for (int rows = 0; rows < dataGridView1.Rows.Count - 1; rows++)
            {
                string from = dataGridView1.Rows[rows].Cells[0].Value.ToString();
                string to = dataGridView1.Rows[rows].Cells[1].Value.ToString();
                int weight = int.Parse(dataGridView1.Rows[rows].Cells[2].Value.ToString());

                edges.Add(
                    new DrawEdge() 
                    { 
                        From = from, 
                        To = to, 
                        Weight = weight 
                    });
            }

            if (edges.Count > 0)
            {
                List<string> nodes = edges.Select(s => s.From).ToList();
                nodes.AddRange(edges.Select(s => s.To).ToList());
                nodes = nodes.Select(n => n).Distinct().ToList();

                Bitmap DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
                pictureBox1.Image = DrawArea;

                Graphics g;
                g = Graphics.FromImage(DrawArea);

                Pen mypen = new Pen(Color.Black);

                int radius = 10;

                int degree = 360 / nodes.Count;

                int centerX = pictureBox1.Size.Width / 2;
                int centerY = pictureBox1.Size.Height / 2;
                int bigRadius = centerX > centerY ? centerY - 2 * radius : centerX - 2 * radius;

                this.nodesCoordinates = new List<NodesCoordinates>();

                for (int i = 0; i < nodes.Count; i++)
                {
                    int x = Convert.ToInt32(bigRadius * Math.Cos(i * degree)) + centerX;
                    int y = Convert.ToInt32(bigRadius * Math.Sin(i * degree)) + centerY;

                    nodesCoordinates.Add(
                        new NodesCoordinates()
                        {
                            NodeName = nodes[i],
                            X = x,
                            Y = y
                        });

                    g.DrawEllipse(mypen, x, y, radius * 2, radius * 2);
                    g.DrawString(nodes[i], new Font("Arial", 14), Brushes.Black, x, y);
                }

                foreach (var edge in edges)
                {
                    int xStart = nodesCoordinates.Single(n => n.NodeName == edge.From).X;
                    int yStart = nodesCoordinates.Single(n => n.NodeName == edge.From).Y;
                    int xEnd = nodesCoordinates.Single(n => n.NodeName == edge.To).X;
                    int yEnd = nodesCoordinates.Single(n => n.NodeName == edge.To).Y;

                    g.DrawLine(mypen, xStart, yStart, xEnd, yEnd);
                }

                pictureBox1.Image = DrawArea;

                g.Dispose();
            }
        }

        class DrawEdge
        {
            public string From { get; set; }
            public string To { get; set; }
            public int Weight { get; set; }
        }

        class NodesCoordinates
        {
            public string NodeName { get; set; }
            public int X { get; set; }
            public int Y{ get; set; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var g = new Graph();

            foreach(var node in nodesCoordinates)
            {
                g.AddNode(node.NodeName);
            }

            for (int rows = 0; rows < dataGridView1.Rows.Count - 1; rows++)
            {
                string from = dataGridView1.Rows[rows].Cells[0].Value.ToString();
                string to = dataGridView1.Rows[rows].Cells[1].Value.ToString();
                int weight = int.Parse(dataGridView1.Rows[rows].Cells[2].Value.ToString());

                g.AddEdge(from, to, weight);
            }

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) &&
                nodesCoordinates.Select(nc => nc.NodeName).Contains(textBox1.Text) && nodesCoordinates.Select(nc => nc.NodeName).Contains(textBox2.Text))
            {
                var dijkstra = new DijkstraAlgorithmClass(g);
                var path = dijkstra.FindShortestPath(textBox1.Text, textBox2.Text);

                textBox13.Text = path;

                if (!string.IsNullOrEmpty(path))
                {
                    Bitmap DrawArea = (Bitmap)pictureBox1.Image;
                    pictureBox1.Image = DrawArea;
                    Graphics gr = Graphics.FromImage(pictureBox1.Image);
                    Pen mypen = new Pen(Color.Red, 5);

                    for (int i = 0; i < path.Length - 1; i++)
                    {
                        int xStart = nodesCoordinates.Single(n => n.NodeName == path[i].ToString()).X;
                        int yStart = nodesCoordinates.Single(n => n.NodeName == path[i].ToString()).Y;
                        int xEnd = nodesCoordinates.Single(n => n.NodeName == path[i + 1].ToString()).X;
                        int yEnd = nodesCoordinates.Single(n => n.NodeName == path[i + 1].ToString()).Y;

                        gr.DrawLine(mypen, xStart, yStart, xEnd, yEnd);
                    }

                    pictureBox1.Image = DrawArea;

                    gr.Dispose();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string map = 
                "1111111111111111" + 
                "\r\n1000000000000001" +
                "\r\n1010111111111111" +
                "\r\n1010111111111111" +
                "\r\n1010111111111111" +
                "\r\n1001111111111111" +
                "\r\n1001111111111111" +
                "\r\n1001111111111111" +
                "\r\n1111111111111111";

            textBox3.Text = map;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DrawArea(textBox3.Text, pictureBox2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox5.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrEmpty(textBox7.Text))
            {
                string map = textBox3.Text;

                if (!string.IsNullOrEmpty(map))
                {
                    string[] lst = map.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                    int height = lst.Length;
                    int width = lst[0].Length;
                    int dx = pictureBox2.Width / width;
                    int dy = pictureBox2.Height / height;
                    int x = 0;
                    int y = 0;

                    int[,] intMap = new int[width, height];
                    for (int j = 0; j < height; j++)
                    {
                        string line = lst[j];
                        for (int i = 0; i < width; i++)
                        {
                            intMap[i, j] = int.Parse(line[i].ToString());
                        }
                    }

                    AStarAlgorithmClass aStarAlgorithmClass = new AStarAlgorithmClass();

                    List<System.Drawing.Point> result = aStarAlgorithmClass.FindPath(intMap, new System.Drawing.Point(int.Parse(textBox4.Text), int.Parse(textBox5.Text)), new System.Drawing.Point(int.Parse(textBox6.Text), int.Parse(textBox7.Text)));

                    if (result != null)
                    {
                        foreach (var r in result)
                        {
                            intMap[r.X, r.Y] = 2;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Path not found");
                    }

                    Bitmap DrawArea = new Bitmap(pictureBox2.Size.Width, pictureBox2.Size.Height);
                    pictureBox2.Image = DrawArea;

                    Graphics g;
                    g = Graphics.FromImage(DrawArea);

                    for (int j = 0; j < height; j++)
                    {
                        y = j * dy;

                        for (int i = 0; i < width; i++)
                        {
                            x = i * dx;

                            if (intMap[i, j] == 1)
                            {
                                g.FillRectangle(Brushes.Gray, x, y, x + dx, y + dy);
                            }
                            if (intMap[i, j] == 0)
                            {
                                g.FillRectangle(Brushes.LightBlue, x, y, x + dx, y + dy);
                            }
                            if (intMap[i, j] == 2)
                            {
                                g.FillRectangle(Brushes.Green, x, y, x + dx, y + dy);
                            }
                        }
                    }

                    pictureBox2.Image = DrawArea;

                    g.Dispose();                    
                }                
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string map =
                "1111111111111111" +
                "\r\n1000000000000001" +
                "\r\n1010111111111111" +
                "\r\n1010111111111111" +
                "\r\n1010111111111111" +
                "\r\n1001111111111111" +
                "\r\n1001111111111111" +
                "\r\n1001111111111111" +
                "\r\n1111111111111111";

            textBox12.Text = map;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DrawArea(textBox12.Text, pictureBox3);
        }

        private void DrawArea(string map, PictureBox pictureBox)
        {
            if (!string.IsNullOrEmpty(map))
            {
                string[] lst = map.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                int height = lst.Length;
                int width = lst[0].Length;
                int dx = pictureBox.Width / width;
                int dy = pictureBox.Height / height;
                int x = 0;
                int y = 0;

                Bitmap DrawArea = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
                pictureBox.Image = DrawArea;

                Graphics g;
                g = Graphics.FromImage(DrawArea);

                for (int j = 0; j < lst.Length; j++)
                {
                    string line = lst[j];

                    y = j * dy;

                    for (int i = 0; i < line.Length; i++)
                    {
                        x = i * dx;

                        if (line[i] == '1')
                        {
                            g.FillRectangle(Brushes.Gray, x, y, x + dx, y + dy);
                        }
                        if (line[i] == '0')
                        {
                            g.FillRectangle(Brushes.LightBlue, x, y, x + dx, y + dy);
                        }
                    }
                }

                pictureBox.Image = DrawArea;

                g.Dispose();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrEmpty(textBox9.Text) &&
                !string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrEmpty(textBox11.Text))
            {
                string map = textBox12.Text;

                if (!string.IsNullOrEmpty(map))
                {
                    string[] lst = map.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                    int height = lst.Length;
                    int width = lst[0].Length;

                    int[,] intMap = new int[width, height];
                    for (int j = 0; j < height; j++)
                    {
                        string line = lst[j];
                        for (int i = 0; i < width; i++)
                        {
                            intMap[i, j] = int.Parse(line[i].ToString());
                        }
                    }

                    WaveAlgorithmClass waveAlgorithmClass = new WaveAlgorithmClass(intMap);

                    int startX = int.Parse(textBox8.Text);
                    int startY = int.Parse(textBox9.Text);
                    int targetX = int.Parse(textBox10.Text);
                    int targetY = int.Parse(textBox11.Text);

                    int [,] cMap = waveAlgorithmClass.FindWave(startX, startY, targetX, targetY);

                    DrawResultPathWave(cMap, pictureBox3);
                }
            }
        }

        private void DrawResultPathWave(int[,] cMap, PictureBox pictureBox)
        {
            int height = cMap.GetLength(1);
            int width = cMap.GetLength(0);
            int dx = pictureBox.Width / width;
            int dy = pictureBox.Height / height;
            int x = 0;
            int y = 0;

            Bitmap DrawArea = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            pictureBox.Image = DrawArea;

            Graphics g;
            g = Graphics.FromImage(DrawArea);

            for (int j = 0; j < height; j++)
            {
                y = j * dy;

                for (int i = 0; i < width; i++)
                {
                    x = i * dx;

                    if (cMap[i, j] == -2)
                    {
                        g.FillRectangle(Brushes.Gray, x, y, x + dx, y + dy);
                    }
                    else if (cMap[i, j] == -1)
                    {
                        g.FillRectangle(Brushes.LightBlue, x, y, x + dx, y + dy);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.Green, x, y, x + dx, y + dy);
                        g.DrawString(cMap[i, j].ToString(), new Font("Arial", 14), Brushes.Black, x, y);
                    }
                }
            }

            pictureBox.Image = DrawArea;

            g.Dispose();
        }
    }
}
