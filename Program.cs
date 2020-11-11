using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace newflasher {
    public class Program {
        public static void Main(string[] args) {
            if (File.Exists("fwinfo.xml")) {
                XElement xml = XElement.Load("fwinfo.xml");

                String model = xml.Element("model").Value;
                String product = xml.Element("product").Value;

                Console.WriteLine("---------------------------------");
                Console.WriteLine("Model: " + model + ", " + product);
                Console.WriteLine("Operator: " + xml.Element("operator").Value + ", " + xml.Element("market").Value);
                Console.WriteLine("Build number: " + xml.Element("swVer").Value);
                Console.WriteLine("---------------------------------\n");
            }

            Console.WriteLine("Removing *.ta and userdata*.sin\n");
            foreach (String name in Directory.GetFiles(".", "*")) {
                String file = name.ToLower();
                if (file.StartsWith(".\\") || file.StartsWith("./")) {
                    file = file.Substring(2);
                }
                if (file.StartsWith("userdata_") || file.EndsWith(".ta")) {
                    Console.WriteLine("Deleting " + file + ".");
                    File.Delete(file);
                }
            }

            Console.WriteLine("\nPress Enter to run newflasher...");
            Console.ReadLine();

            Process.Start("cmd.exe", "/C newflasher_run");
        }
    }
}
