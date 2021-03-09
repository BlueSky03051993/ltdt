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
    class Bai1
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
        private void XuatDoThi(GRAPH graph)
        {
            for (int i = 0; i < graph.N; i++)
            {
                for (int j = 0; j < graph.N; j++)
                    Console.Write(graph.A[i, j].ToString() + " ");
                Console.WriteLine();
            }
        }
        private bool KiemTraDoThiVoHuong(GRAPH graph)
        {
            // Y tuong: do thi vo huong phai co ma tran ke doi xung, neu ton tai 1 vi tri nao do tren ma tran khong co doi xung thi khong phai la do thi vo huong
            for (int i = 0; i < graph.N; i++)
                for (int j = i + 1; j < graph.N; j++)
                    if (graph.A[i, j] != graph.A[j, i])
                        return false;
            return true;
        }
        private void XulyDothiVohuong(GRAPH graph)
        {
            // b. Do thi vo huong hay co huong => do thi vo huong
            Console.WriteLine("Do thi vo huong");
            // c. So dinh cua do thi vo huong
            Console.WriteLine("So dinh cua do thi: " + graph.N.ToString());

            // Khai bao bien luu tru thong tin bac cua tung dinh
            int[] bac_dinh = new int[graph.N];   // mang chua bac cua dinh
            for (int i = 0; i < graph.N; i++)
                bac_dinh[i] = 0;
            for (int i = 0; i < graph.N; i++)
                for (int j = 0; j < graph.N; j++)
                    if (graph.A[i, j] != 0)
                    {
                        bac_dinh[i] += graph.A[i, j];
                        if (i == j)  // xet canh khuyen
                            bac_dinh[i] += graph.A[i, i];
                    }

            // d. So luong canh cua do thi
            int tong_bac = 0;
            for (int i = 0; i < graph.N; i++)
                tong_bac += bac_dinh[i];
            Console.WriteLine("So canh cua do thi: " + (tong_bac / 2).ToString()); // dinh ly bat tay Handshaking Theorem

            // e. So cap dinh xuat hien canh boi(chua hoan thanh)
            int so_canh_boi = 0;
            int so_cap_dinh_xuat_hien_canh_boi = 0;
            for (int i = 0; i < graph.N; i++)
                for (int j = i + 1; j < graph.N; j++)
                    if (graph.A[i, j] > 1)
                        so_canh_boi += graph.A[i, j];
            Console.WriteLine("So canh boi: " + so_canh_boi.ToString());

            //e2. So canh khuyen
            int so_canh_khuyen = 0;
            for (int i = 0; i < graph.N; i++)
                so_canh_khuyen += graph.A[i, i];
            Console.WriteLine("So canh khuyen: " + so_canh_khuyen.ToString());

            // f1. So dinh treo
            int so_dinh_treo = 0;
            for (int i = 0; i < graph.N; i++)
                if (bac_dinh[i] == 1)
                    so_dinh_treo++;
            Console.WriteLine("So dinh treo: " + so_dinh_treo.ToString());

            // f2. So dinh co lap
            int so_dinh_co_lap = 0;
            for (int i = 0; i < graph.N; i++)
                if (bac_dinh[i] == 0)
                    so_dinh_co_lap++;
            Console.WriteLine("So dinh co lap: " + so_dinh_co_lap.ToString());

            // g. Bac cua tung dinh
            Console.WriteLine("Bac cua tung dinh: ");
            for (int i = 0; i < graph.N; i++)
                Console.Write(i.ToString() + "(" + bac_dinh[i].ToString() + ") ");
            Console.WriteLine();

            // h. Loai do thi
            if (so_canh_khuyen > 0)
                Console.WriteLine("Gia do thi");
            else
            {
                if (so_canh_boi > 0)
                    Console.WriteLine("Da do thi");
                else
                    Console.WriteLine("Don do thi");
            }
        }
        private void XulyDothiCohuong(GRAPH graph)
        {
            // b. Do thi vo huong hay co huong => do thi co huong
            Console.WriteLine("Do thi co huong");
            // c. So dinh
            Console.WriteLine("So dinh cua do thi: " + graph.N.ToString());

            // Khai bao bien luu tru thong tin bac cua tung dinh
            int[] bac_ra = new int[graph.N];               // mang chua bac ra cua dinh
            int[] bac_vao = new int[graph.N];               // mang chua bac vao cua dinh
            for (int i = 0; i < graph.N; i++)
            {
                bac_ra[i] = 0;
                bac_vao[i] = 0;
            }
            for (int i = 0; i < graph.N; i++)
                for (int j = 0; j < graph.N; j++)
                {
                    if (graph.A[i, j] != 0)
                        bac_ra[i] += graph.A[i, j];
                    if (graph.A[j, i] != 0)
                        bac_vao[i] += graph.A[j, i];
                }

            // d. So luong canh cua do thi
            int tong_bac = 0;
            for (int i = 0; i < graph.N; i++)
                tong_bac += bac_ra[i];
            Console.WriteLine("So canh cua do thi: " + tong_bac.ToString()); // dinh ly ve so bac do thi

            // e1. So cap dinh xuat hien canh boi(chua hoan thanh)
            int so_canh_boi = 0;
            for (int i = 0; i < graph.N; i++)
                for (int j = i + 1; j < graph.N; j++)
                    if (graph.A[i, j] > 1)
                        so_canh_boi += graph.A[i, j];
            Console.WriteLine("So canh boi: " + so_canh_boi.ToString());

            // e2. So canh khuyen
            int so_canh_khuyen = 0;
            for (int i = 0; i < graph.N; i++)
                so_canh_khuyen += graph.A[i, i];
            Console.WriteLine("So canh khuyen: " + so_canh_khuyen.ToString());

            // f1. So dinh treo
            int so_dinh_treo = 0;
            for (int i = 0; i < graph.N; i++)
                if ((bac_ra[i] + bac_vao[i]) == 1)
                    so_dinh_treo++;
            Console.WriteLine("So dinh treo: " + so_dinh_treo.ToString());

            // f2. So dinh co lap
            int so_dinh_co_lap = 0;
            for (int i = 0; i < graph.N; i++)
                if ((bac_ra[i] + bac_vao[i]) == 0)
                    so_dinh_co_lap++;
            Console.WriteLine("So dinh co lap: " + so_dinh_co_lap.ToString());

            // g. Bac cua tung dinh
            Console.WriteLine("(Bac vao - Bac ra) cua tung dinh: ");
            for (int i = 0; i < graph.N; i++)
                Console.Write(i.ToString() + "(" + bac_vao[i].ToString() + "-" + bac_ra[i].ToString() + ") ");
            Console.WriteLine();

            // h. Loai do thi
            if (so_canh_boi > 0)
                Console.WriteLine("Da do thi co huong");
            else
                Console.WriteLine("Do thi co huong");
        }
        public void RunModule()
        {
            GRAPH graph = new GRAPH();
            DocDoThi(ref graph, "input.txt");

            // a. In ma tran ke cua do thi
            XuatDoThi(graph);

            // Thuc hien b. den h.
            if (KiemTraDoThiVoHuong(graph) == true)
                XulyDothiVohuong(graph);
            else
                XulyDothiCohuong(graph);
        }
    }
}
