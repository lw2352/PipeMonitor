using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using MyClassLibrary;

namespace pipemonitor
{
    public class Net_Analyze
    {
        private static double[] g_DataA = new double[300000];//归一化后的A
        private static double[] g_DataB = new double[300000];
        private static double[] g_DataC = new double[300000];
        private static int g_DataLength = 300000;
        private static int g_OffSet = 0;

        public double[] getDataA(string FileName)
        {
            int datalength = 0;
            double dataA_avg = 0;
            double[] dataA;

            FileStream fs = new FileStream(FileName, FileMode.Open);
            datalength = (int)(fs.Length - 2) / 2;
            dataA = new double[datalength];
            byte[] data = new byte[datalength * 2];

            fs.Seek(1, SeekOrigin.Begin);//跳过第一个字符
            fs.Read(data, 0, datalength * 2);
            fs.Dispose();

            for (int i = 0; i < datalength; i++)
                dataA[i] = (double)(data[2 * i] * 0x100 + data[2 * i + 1]);
            dataA_avg = 0;
            for (int i = 0; i < datalength; i++)
                dataA_avg += dataA[i] / datalength;

            for (int i = 0; i < datalength; i++)//归一处理
                dataA[i] = (dataA[i] - dataA_avg) / dataA_avg;

            g_DataA = dataA;
            return dataA;
        }

        public double[] getDataB(string FileName)
        {
            int datalength = 0;
            double dataB_avg = 0;
            double[] dataB;

            FileStream fs = new FileStream(FileName, FileMode.Open);
            datalength = (int)(fs.Length - 2) / 2;
            dataB = new double[datalength];
            byte[] data = new byte[datalength * 2];

            fs.Seek(1, SeekOrigin.Begin);//跳过第一个字符
            fs.Read(data, 0, datalength * 2);
            fs.Dispose();

            for (int i = 0; i < datalength; i++)
                dataB[i] = (double)(data[2 * i] * 0x100 + data[2 * i + 1]);
            dataB_avg = 0;
            for (int i = 0; i < datalength; i++)
                dataB_avg += dataB[i] / datalength;

            for (int i = 0; i < datalength; i++)//归一处理
                dataB[i] = (dataB[i] - dataB_avg) / dataB_avg;

            g_DataB = dataB;
            return dataB;
        }

        public double[] getAnalyzeDataC()
        {
            g_OffSet = 0;//初始化
            //使用互相关算法分析
            int len = 262144;
            double[] A = new double[len];
            double[] B = new double[len];

            for (int i = 0; i < len; i++)
            {
                A[i] = g_DataA[i];
                B[i] = g_DataB[i];
            }

            CrossCorrelation ccorr = new CrossCorrelation();
            double[] R = ccorr.Rxy(A, B);
            int R_length = R.Length;

            Position p = new Position();
            int location = p.max(R);
            int d = location - R_length / 2;

            g_OffSet = d;//偏移值

            return R;//经过互相关计算后的数组
        }

        public int GetOffSet()
        {//返回偏移值
            return g_OffSet;
        }

        public string AutoAnalyze(int idA, int idB)
        {
            string dateA = Net_Analyze_DB.readDataDate(idA);
            string dateB = Net_Analyze_DB.readDataDate(idB);
            DateTime dt1 = DateTime.Parse(dateA);
            DateTime dt2 = DateTime.Parse(dateB);
            TimeSpan ts = dt1.Subtract(dt2);

            double t = ts.TotalHours;

            if (-6 < t && t < 6)
            {
                string pathA = Net_Analyze_DB.readDataPath(4);
                pathA = pathA.Replace("\\\\", "\\");

                string pathB = Net_Analyze_DB.readDataPath(5);
                pathB = pathB.Replace("\\\\", "\\");

                getDataA(pathA);
                getDataA(pathB);
                getAnalyzeDataC();

                return g_OffSet.ToString();
                //再把偏移值写入数据库中

            }
            return null;
        }

    }//end of public class Net_Analyze
}//end of namespace pipemonitor