using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsVizualizator.Dijkstra_Algorithm
{
    public class GraphNode
    {
        /// <summary>
        /// Название вершины
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Список ребер
        /// </summary>
        public List<GraphEdge> Edges { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Название вершины</param>
        public GraphNode(string name)
        {
            Name = name;
            Edges = new List<GraphEdge>();
        }

        /// <summary>
        /// Добавить ребро
        /// </summary>
        /// <param name="newEdge">Ребро</param>
        public void AddEdge(GraphEdge newEdge)
        {
            Edges.Add(newEdge);
        }

        /// <summary>
        /// Добавить ребро
        /// </summary>
        /// <param name="node">Вершина</param>
        /// <param name="edgeWeight">Вес</param>
        public void AddEdge(GraphNode node, int edgeWeight)
        {
            AddEdge(new GraphEdge(node, edgeWeight));
        }

        /// <summary>
        /// Преобразование в строку
        /// </summary>
        /// <returns>Имя вершины</returns>
        public override string ToString() => Name;
    }
}
