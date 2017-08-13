using System;

namespace Platform.Support.ConsoleEx
{
    /// <summary>
    /// Extracted from http://www.c-sharpcorner.com/uploadfile/b942f9/creating-a-console-listbox-in-C-Sharp/
    /// </summary>
    public static class ListBox
    {
        public static int ChooseListBoxItem(string[] items, int ucol, int urow, ConsoleColor back, ConsoleColor fore)
        {
            Console.TreatControlCAsInput = false;
            Console.CancelKeyPress += new ConsoleCancelEventHandler(BreakHandler);
            //Console.Clear();
            Console.CursorVisible = false;

            int numItems = items.Length;
            int maxLength = items[0].Length;
            for (int i = 1; i < numItems; i++)
            {
                if (items[i].Length > maxLength)
                {
                    maxLength = items[i].Length;
                }
            }
            int[] rightSpaces = new int[numItems];
            for (int i = 0; i < numItems; i++)
            {
                rightSpaces[i] = maxLength - items[i].Length + 1;
            }
            int lcol = ucol + maxLength + 3;
            int lrow = urow + numItems + 1;
            DrawBox(ucol, urow, lcol, lrow, back, fore, true);
            WriteColorString(" " + items[0] + new string(' ', rightSpaces[0]), ucol + 1, urow + 1, fore, back);
            for (int i = 2; i <= numItems; i++)
            {
                WriteColorString(items[i - 1], ucol + 2, urow + i, back, fore);
            }
            ConsoleKeyInfo cki;
            char key;
            int choice = 1;

            while (true)
            {
                cki = Console.ReadKey(true);
                key = cki.KeyChar;
                if (key == '\r') // enter
                {
                    return choice;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    WriteColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), ucol + 1, urow + choice, back, fore);
                    if (choice < numItems)
                    {
                        choice++;
                    }
                    else
                    {
                        choice = 1;
                    }
                    WriteColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), ucol + 1, urow + choice, fore, back);
                }
                else if (cki.Key == ConsoleKey.UpArrow)
                {
                    WriteColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), ucol + 1, urow + choice, back, fore);
                    if (choice > 1)
                    {
                        choice--;
                    }
                    else
                    {
                        choice = numItems;
                    }
                    WriteColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), ucol + 1, urow + choice, fore, back);
                }
            }
        }

        public static void DrawBox(int ucol, int urow, int lcol, int lrow, ConsoleColor back, ConsoleColor fore, bool fill)
        {
            const char Horizontal = '\u2500';
            const char Vertical = '\u2502';
            const char UpperLeftCorner = '\u250c';
            const char UpperRightCorner = '\u2510';
            const char LowerLeftCorner = '\u2514';
            const char LowerRightCorner = '\u2518';
            string fillLine = fill ? new string(' ', lcol - ucol - 1) : "";
            SetColors(back, fore);
            // draw top edge
            Console.SetCursorPosition(ucol, urow);
            Console.Write(UpperLeftCorner);
            for (int i = ucol + 1; i < lcol; i++)
            {
                Console.Write(Horizontal);
            }
            Console.Write(UpperRightCorner);
            // draw sides
            for (int i = urow + 1; i < lrow; i++)
            {
                Console.SetCursorPosition(ucol, i);
                Console.Write(Vertical);
                if (fill) Console.Write(fillLine);
                Console.SetCursorPosition(lcol, i);
                Console.Write(Vertical);
            }
            // draw bottom edge
            Console.SetCursorPosition(ucol, lrow);
            Console.Write(LowerLeftCorner);
            for (int i = ucol + 1; i < lcol; i++)
            {
                Console.Write(Horizontal);
            }
            Console.Write(LowerRightCorner);
        }

        public static void WriteColorString(string s, int col, int row, ConsoleColor back, ConsoleColor fore)
        {
            SetColors(back, fore);
            // write string
            Console.SetCursorPosition(col, row);
            Console.Write(s);
        }

        public static void SetColors(ConsoleColor back, ConsoleColor fore)
        {
            Console.BackgroundColor = back;
            Console.ForegroundColor = fore;
        }

        public static void CleanUp()
        {
            Console.ResetColor();
            Console.CursorVisible = true;
            Console.Clear();
        }

        public static void BreakHandler(object sender, ConsoleCancelEventArgs args)
        {
            // exit gracefully if Control-C or Control-Break pressed
            CleanUp();
        }
    }
}