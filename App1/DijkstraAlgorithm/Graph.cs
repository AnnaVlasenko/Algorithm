using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Dijkstra_Algorithm
{
    public class Graph
    {
        /// <summary>
        /// Список вершин графа
        /// </summary>
        public List<GraphNode> Nodes { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Graph()
        {
            Nodes = new List<GraphNode>();
        }

        /// <summary>
        /// Добавление вершины
        /// </summary>
        /// <param name="vertexName">Имя вершины</param>
        public void AddNode(string vertexName)
        {
            Nodes.Add(new GraphNode(vertexName));
        }

        /// <summary>
        /// Поиск вершины
        /// </summary>
        /// <param name="nodeName">Название вершины</param>
        /// <returns>Найденная вершина</returns>
        public GraphNode FindNode(string nodeName)
        {
            foreach (var v in Nodes)
            {
                if (v.Name.Equals(nodeName))
                {
                    return v;
                }
            }

            return null;
        }

        /// <summary>
        /// Добавление ребра
        /// </summary>
        /// <param name="firstName">Имя первой вершины</param>
        /// <param name="secondName">Имя второй вершины</param>
        /// <param name="weight">Вес ребра соединяющего вершины</param>
        public void AddEdge(string firstName, string secondName, int weight)
        {
            var v1 = FindNode(firstName);
            var v2 = FindNode(secondName);
            if (v2 != null && v1 != null)
            {
                v1.AddEdge(v2, weight);
                v2.AddEdge(v1, weight);
            }
        }
    }
}
