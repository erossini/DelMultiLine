using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelMultiLine {
   class Program {
      static void Main(string[] args) {
         if (args.Count() == 0 || args.Count() > 2) {
            Console.WriteLine("\nDelMultiLine (by Enrico Rossini - puresourcecode.com)");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Remove duplicate line in a file\n");
            Console.WriteLine("Usage:");
            Console.WriteLine("   delmultiline <Filename> <resultFilename>\n");
            Console.WriteLine("filename:       define a full path for the file you want to elaborate");
            Console.WriteLine("resultFilename: define the full path for the original file for a backup");
            Environment.Exit(0);
         }

         string file1 = args[0];
         string file2 = "";

         if (args.Count() == 1) {
            if (string.IsNullOrEmpty(file2)) {
               file2 = file1 + ".old";
            }
            else {
               file2 = args[1];
            }
         }

         Console.WriteLine(string.Format("Reading {0} in progress...", args[0]));
         string[] lines = File.ReadAllLines(file1);
         List<string> newline = new List<string>();

         for (int i = 0; i < lines.Length; i++) {
            newline.Add(lines[i]);
         }

         Console.WriteLine("Deleting multiple line in progress...");
         for (int i = 0; i < lines.Length; i++) {
            List<string> temp = new List<string>();
            int duplicate_count = 0;

            for (int j = newline.Count - 1; j >= 0; j--) {
               //checking for duplicate records
               if (lines[i] != newline[j])
                  temp.Add(newline[j]);
               else {
                  duplicate_count++;
                  if (duplicate_count == 1)
                     temp.Add(lines[i]);
               }
            }
            newline = temp;
         }

         // reverse the array
         newline.Reverse();

         //assigning into a string array
         string[] newFile = newline.ToArray();
         newline.Sort();

         // move the original file in a new location
         Console.WriteLine(string.Format("Copying original file in {0}", args[0]));
         File.Move(file1, file2);

         //now writing the data to a text file
         Console.WriteLine(string.Format("Write new file {0}", args[0]));
         File.WriteAllLines(file1, newFile);

         Console.WriteLine("Convertion is finished.");
         Console.WriteLine("\nPress any key to continue...");
         Console.ReadLine();
      }
   }
}