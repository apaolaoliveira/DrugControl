
using DrugControl.MedicineModule;
using System.Drawing;
using System.Text;

namespace DrugControl.Shared
{
    public class InterfaceBase
    {
        public static void ColorfulMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        public static int SetMenu(string title, string fieldOne, string fieldTwo, string fieldThree, string fieldFour,
                                  string fieldFive = null, string fieldSix = null, string fieldSeven = null, string fieldEight = null)
        {
            Console.Clear();

            ColorfulMessage(
              $"\n{title.ToUpper()}"
            + $"\n-------------------"
            + $"\n[1] {fieldOne}."
            + $"\n[2] {fieldTwo}."
            + $"\n[3] {fieldThree}."
            + $"\n[4] {fieldFour}."
            , ConsoleColor.DarkYellow);

            if (fieldFive != null)
                ColorfulMessage($"\n[5] {fieldFive}.", ConsoleColor.DarkYellow);

            if (fieldSix != null)
                ColorfulMessage($"\n[6] {fieldSix}.", ConsoleColor.DarkYellow);

            if (fieldSeven != null)
                ColorfulMessage($"\n[7] {fieldSeven}.", ConsoleColor.DarkYellow);

            if (fieldEight != null)
                ColorfulMessage($"\n[8] {fieldEight}.", ConsoleColor.DarkYellow);

            ColorfulMessage("\n\n→ ", ConsoleColor.DarkYellow);

            int selectedOption = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            return selectedOption;
        }

        public static void SetTable(string[] columnNames, int[] columnWidths, List<object> data)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            // Print - separator
            const int pipeCount = 2;
            const char SeparatorChar = '-';
            int totalWidth = columnWidths.Sum() + columnNames.Length * pipeCount - 1;
            string separator = new string(SeparatorChar, totalWidth);

            Console.WriteLine($"\n\n {separator}");

            // Print header
            string header = "";
            for (int i = 0; i < columnNames.Length; i++)
            {
                header += String.Format("| {0,-" + columnWidths[i] + "}", columnNames[i].ToUpper());
            }
            Console.WriteLine($"{header}|\n {separator}");

            Console.ResetColor();

            // Print the each row from 'data'
            foreach (object[] row in data)
            {
                string rowString = "";
                for (int i = 0; i < row.Length; i++)
                {
                    rowString += String.Format("| {0,-" + columnWidths[i] + "}", row[i]);
                }
                Console.WriteLine(rowString + "|");
            } 
            Console.WriteLine($" {separator}");            
        }

        public static void SetHeader(string header) 
        {
            Console.Clear();

            ColorfulMessage(
              $"\n\n{header.ToUpper()}"
            + "\n------------------------------\n"
            , ConsoleColor.Cyan);
        }

        public static void SetFooter()
        {
            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadLine();
        }

        public static string SetStringField(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(
                $"\n{message}"
                + "\n→ ");
            Console.ResetColor();

            string reply = Console.ReadLine();

            return reply;
        }

        public static int SetIntField(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(
                $"\n{message}"
                + "\n→ ");
            Console.ResetColor();

            int reply = Convert.ToInt32(Console.ReadLine());

            return reply;
        }

        public static long SetLongField(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(
                $"\n{message}"
                + "\n→ ");
            Console.ResetColor();

            long reply = Convert.ToInt64(Console.ReadLine());

            return reply;
        }
    }
}
