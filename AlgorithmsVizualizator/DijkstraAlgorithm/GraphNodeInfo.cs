using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsVizualizator.Dijkstra_Algorithm
{
    public class NodeInfo
    {
        /// <summary>
        /// Вершина
        /// </summary>
        public GraphNode Node { get; set; }

        /// <summary>
        /// Не посещенная вершина
        /// </summary>
        public bool IsUnvisited { get; set; }

        /// <summary>
        /// Сумма весов ребер
        /// </summary>
        public int EdgesWeightSum { get; set; }

        /// <summary>
        /// Предыдущая вершина
        /// </summary>
        public GraphNode PreviousNode { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="node">Вершина</param>
        public NodeInfo(GraphNode node)
        {
            Node = node;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousNode = null;
        }
    }
}
