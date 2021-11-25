using App1.AStarAlgorithm;
using App1.Dijkstra_Algorithm;
using App1.WaveAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestDijksrtaAlgorithm();
			Console.WriteLine();
            TestAStarAlgorithm();
			Console.WriteLine();
			Console.WriteLine();
			TestWaveAlgorithm();
        }

        private static void TestDijksrtaAlgorithm()
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
            g.AddEdge("A", "C", 33);
            g.AddEdge("A", "D", 61);
            g.AddEdge("B", "C", 47);
            g.AddEdge("B", "E", 93);
            g.AddEdge("C", "D", 11);
            g.AddEdge("C", "E", 79);
            g.AddEdge("C", "F", 63);
            g.AddEdge("D", "F", 41);
            g.AddEdge("E", "F", 17);
            g.AddEdge("E", "G", 58);
            g.AddEdge("F", "G", 84);

            var dijkstra = new DijkstraAlgorithmClass(g);
            var path = dijkstra.FindShortestPath("A", "G");
            Console.WriteLine(path);
            Console.ReadLine();
        }

		private static void TestAStarAlgorithm()
		{
			int [,] map = new int[,]{
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
				{1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}};

			AStarAlgorithmClass aStarAlgorithmClass = new AStarAlgorithmClass();

			List<System.Drawing.Point> result = aStarAlgorithmClass.FindPath(map, new System.Drawing.Point(1, 1), new System.Drawing.Point(4, 3));

			int mapWidht = map.GetLength(1);
			int mapHeight = map.GetLength(0);

			for (int y = 0; y < mapHeight; y++)
			{
				Console.WriteLine();
				for (int x = 0; x < mapWidht; x++)
					if (map[y, x] == 1)
						Console.Write("+");
					else
						Console.Write(" ");
			}
			Console.WriteLine();

			foreach(var r in result)
			{
				map[r.X, r.Y] = 2;
			}

			for (int y = 0; y < mapHeight; y++)
			{
				Console.WriteLine();
				for (int x = 0; x < mapWidht; x++)
				{
					if (map[y, x] == 1)
					{
						Console.Write("+");
					}
					else if (map[y, x] == 2)
					{
						Console.Write("P");
					}
					else
					{
						Console.Write(" ");
					}
				}
			}
			Console.ReadKey();
		}

		private static void TestWaveAlgorithm()
		{
			int[,] map = new int[,]{
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
				{1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}};

			WaveAlgorithmClass waveAlgorithmClass = new WaveAlgorithmClass(map);

			waveAlgorithmClass.FindWave(1, 1, 3, 4);
		}
	}
}
