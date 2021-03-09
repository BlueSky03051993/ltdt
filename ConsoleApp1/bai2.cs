using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class GRAPH
    {
        int n;
        int[,] a;

        public int N
        {
            get { return n; }
            set { n = value; }
        }

        public int[,] A
        {
            get { return a; }
            set { a = value; }
        }
    }
class Bai2
    {
        private void DocDoThi(ref GRAPH graph, string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Tap tin khong ton tai");
                graph = null;
                return;
            }
            string[] lines = File.ReadAllLines(filename);
            int n = Int32.Parse(lines[0]);
            int[,] a = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                string[] tokens = lines[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < n; j++)
                    a[i, j] = Int32.Parse(tokens[j]);
            }
            graph.N = n;
            graph.A = a;
        }
        private int KiemTraDoThiDayDu(GRAPH graph)
        {
            for (int i = 0; i < graph.N; i++)
                for (int j = i + 1; j < graph.N; j++)
                    if (graph.A[i, j] == 0)
                        return -1;
            return graph.N;
        }
        private int KiemTraDoThiChinhQuy(GRAPH graph)
        {
            if (graph.N < 2)
                return graph.N;

            int[] degree = new int[graph.N];
            for (int i = 0; i < graph.N; i++)
            {
                int count = 0;
                for (int j = 0; j < graph.N; j++)
                    if (graph.A[i, j] != 0)
                        count++;
                degree[i] = count;
            }
            for (int i = 1; i < graph.N; i++)
                if (degree[i] != degree[0])
                    return -1;
            return degree[0];
        }
        private int KiemTraDoThiVong(GRAPH graph)
        {
            if (KiemTraDoThiChinhQuy(graph) != 2)  // do thi vong luon luon la do thi 2-chinh quy
                return -1;
            int v = 0;              // bat dau xet tu dinh 0
            int[] label = new int[graph.N];         // danh dau cac dinh da duyet qua
            int nLabeled = 0;
            for (int i = 0; i < graph.N; i++)
                label[i] = 0;

            bool bIsCycle = true;
            while (nLabeled < graph.N)
            {
                int next = -1;
                for (int j = 0; j < graph.N; j++)
                    if (graph.A[v, j] != 0 && label[j] == 0)  // tim dinh ke voi v ma chua duyet qua
                    {
                        next = j;
                        break;
                    }
                if (next == -1)    // khong tim thay dinh ke thich hop
                {
                    bIsCycle = false;
                    break;
                }
                label[next] = 1;
                v = next;
                nLabeled++;
            }
            if (bIsCycle)
                return graph.N;
            return -1;
        }
        public void RunModule()
        {
            GRAPH graph = new GRAPH();
            DocDoThi(ref graph, "input.txt");

            int n1 = KiemTraDoThiDayDu(graph);
            if (n1 != -1)
                Console.WriteLine("Day la do thi day du K" + n1.ToString());
            else
                Console.WriteLine("Day khong phai la do thi day du");

            int n2 = KiemTraDoThiChinhQuy(graph);
            if (n2 != -1)
                Console.WriteLine("Day la do thi " + n2.ToString() + "-chinh quy");
            else
                Console.WriteLine("Day khong phai la do thi chinh quy");

            int n3 = KiemTraDoThiVong(graph);
            if (n3 != -1)
                Console.WriteLine("Day la do thi vong C" + n3.ToString());
            else
                Console.WriteLine("Day khong phai la do thi vong");
        }
    }
}