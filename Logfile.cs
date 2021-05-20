using System;
using System.IO;

namespace oop3 {
    static class Logfile {

        private static string Now;


        private static string GetTimeStamp() {
            Now = DateTime.Now.ToString();
            string s = "";
            int[] _indexesForDateTime = { 8, 9, 3, 4, 0, 1 };
            foreach (int a in _indexesForDateTime) {
                s += Now.Substring(a, 1);
            }
            return s;
        }

        public static string GetErrorLogfileName() {
            string s = GetTimeStamp();
            s += "_errorlog.txt";
            return s;
        }

        public static string GetLogfileName() {
            string s = GetTimeStamp();
            s += "_lincolnlog.txt";
            return s;
        }


        public static void Add(string s) {
            Add(GetLogfileName(), s);
        }

        public static void AddError(string s) {
            Add(GetErrorLogfileName(), s);
        }


        private static void Add(string filepath, string s) {
            try {
                using (StreamWriter sw = new StreamWriter(filepath, true)) {
                    if (s != null || s != "\n") {
                        sw.WriteLine(Now + "    " + s);
                    }
                }
            } catch (System.IO.IOException) {
                Console.WriteLine("Cannot write to file.");
            }
        }

    }
}
