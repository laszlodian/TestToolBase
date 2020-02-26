using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows;
using System.Runtime;
using System.Reflection;
using System.Management;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Configuration;
using TestToolsBase;
using System.Windows.Forms;
using TestToolsBase.Properties;

namespace TestToolsBase.Console
{
    /// <summary>
    /// Generall exit codes for Console-based measures
    /// </summary>
    public enum ConsoleExitCode
    {
        /// <summary>
        /// result has been stored into SQL, and is is OK
        /// </summary>
        MeasureOK = 1,

        /// <summary>
        /// result has been stored into SQL, and is is Failed
        /// </summary>
        MeasureFailed = 2,

        /// <summary>
        /// There is no any SQL
        /// </summary>
        Cancelled = 0x100,

        /// <summary>
        /// Internall measure SW Error
        /// </summary>
        InternalError = 0x101
    }

    public static class EnumExtensions
    {
        public static int ToInt(this Enum enumValue)
        {
            return (int)((object)enumValue);
        }
    }

    static public class ConsoleHelper
    {
        public static string PRESS_ANY_KEY = Resources.CONSOLEHELPER_PRESSANYKEY;

        static public void ClearInputBuffer()
        {
            while (System.Console.KeyAvailable)//empties the buffer
                System.Console.ReadKey();
        }

        public static void InverseColors()
        {
            ConsoleColor c = System.Console.ForegroundColor;
            System.Console.ForegroundColor = System.Console.BackgroundColor;
            System.Console.BackgroundColor = c;
        }

        /// <summary>
        /// Write line to the console at position width colors
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="s"></param>
        /// <param name="ForegroundColor"></param>
        /// <param name="BackgroundColor"></param>
        public static void WriteLineTo(int x, int y, string s, ConsoleColor ForegroundColor, ConsoleColor BackgroundColor)
        {
            System.Console.BackgroundColor = BackgroundColor;
            WriteLineTo(x, y, string.Format(s), ForegroundColor);
        }

        public static void WriteLineTo(int x, int y, string s, ConsoleColor ForegroundColor)
        {
            System.Console.ForegroundColor = ForegroundColor;
            WriteLineTo(x, y, string.Format(s));
        }

        public static void WriteLineTo(int x, int y, string s)
        {
            int width = x + s.Length +1;
            int height = y + 1;
            if (System.Console.BufferWidth < width) System.Console.BufferWidth = width;
            if (System.Console.BufferHeight < height) System.Console.BufferHeight = height;
            if (width > System.Console.LargestWindowWidth) width = System.Console.LargestWindowWidth;
            if (height > System.Console.LargestWindowHeight) height = System.Console.LargestWindowHeight;
            if (System.Console.WindowWidth < width) System.Console.WindowWidth = width;
            if (System.Console.WindowHeight < height) System.Console.WindowHeight = height;
            System.Console.CursorTop = y;
            System.Console.CursorLeft = x;
            System.Console.Write(string.Format(s));
        }

        /// <summary>
        /// Note: modifies cursor position to first char of the specified Top line!
        /// </summary>
        /// <param name="?">top_in</param>
        /// <param name="?">button_in</param>
        public static void ClearArea(int top_in, int button_in)
        {
            StringBuilder emptyLine = new StringBuilder(System.Console.WindowWidth);
            for (int i = 0; i < System.Console.WindowWidth; i++)
                emptyLine.Append(' ');

            for (int i = button_in; i >= top_in; i--)
            {
                System.Console.CursorTop = i;
                System.Console.CursorLeft = 0;
                System.Console.Write(emptyLine.ToString());
            }

            System.Console.CursorLeft = 0;
            System.Console.CursorTop = top_in;
        }

                
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result_out"></param>
        /// <param name="?"></param>
        /// <returns>true = OK</returns>
        static public bool GetUserInputInt(string message, ref int result_out,
            out bool noAnswareFlag_out, out bool badAnswareFlag_out)
        {
            noAnswareFlag_out = false;
            badAnswareFlag_out = false;
            ConsoleHelper.ClearInputBuffer();

            if (message != null || message != string.Empty)
                ConsoleHelper.InfoMessage(message);

            System.Console.Write(">");
            string strRes = System.Console.ReadLine();
            System.Console.WriteLine();

            if (strRes == null)
                strRes = string.Empty;
            else
                strRes = strRes.Trim();

            if(strRes == string.Empty)
            {
                noAnswareFlag_out = true;
                return false;
            }

            if (int.TryParse(strRes.Trim(), out result_out))
            {
                return true;
            }

            badAnswareFlag_out = true;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="minLength">0-no minimal lenght</param>
        /// <returns></returns>
        static public string GetUserInputString(string message_in, int minLength_in)
        {
            ConsoleHelper.ClearInputBuffer();
            Trace.TraceInformation("Console Info Out: '{0}'", message_in);
            System.Console.Write(message_in);

            string res;
            do
            {
                System.Console.Write(">");
                res = System.Console.ReadLine();
                System.Console.WriteLine();

                Trace.TraceInformation("Console User answer: '{0}'", res != null ? res : "<null>");

                if (res == null || res.Length < minLength_in)
                    ConsoleHelper.WarningMessage(Resources.ERROR_MIN_LENGHT, minLength_in);
            } while (res == null || res.Length < minLength_in);
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="timeout_in">[ms], null for infinite</param>
        /// <returns>null in case of timeout</returns>
        static public ConsoleKeyInfo? GetUserAnswerFor(string message, int? timeout_in)
        {
            ClearInputBuffer();            

            if(message != null && message != string.Empty)
                InfoMessage(message);

            while (!System.Console.KeyAvailable && ( timeout_in.HasValue == false || timeout_in > 0) )
            {
                if(timeout_in.HasValue)
                    timeout_in -= 250;

                Thread.Sleep(250);
            }

            if (System.Console.KeyAvailable)
            {
                ConsoleKeyInfo res = System.Console.ReadKey();
                System.Console.WriteLine();
                return res;
            }
            else
                return null;//timeout
        }

        static public ConsoleKeyInfo GetUserAnswerFor(string message)
        {
            return GetUserAnswerFor(message, int.MaxValue).Value;
        }

        /// <summary>
        /// Provides similar functionality as Forms.MessageBox() 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="buttnos_in">AbortRetryIgnore and Ok is not supported</param>
        /// <param name="default_in">default item</param>
        /// <param name="enterRequired_in"></param>
        /// <param name="positionY_in">optional</param>
        /// <returns></returns>
        static public DialogResult ConsoleMessage(string message_in, MessageBoxButtons buttnos_in, DialogResult? default_in, bool enterRequired_in, int? positionY_in)
        {
            bool requiredOk = false, requiredYes = false, requiredNo = false;
            bool requiredCancel = false, requiredRetry = false;
            switch (buttnos_in)
            {
                case MessageBoxButtons.OKCancel:
                    requiredOk = requiredCancel = true;
                    break;
                case MessageBoxButtons.RetryCancel:
                    requiredRetry = requiredCancel = true;
                    break;
                case MessageBoxButtons.YesNo:
                    requiredYes = requiredNo = true;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    requiredCancel = requiredYes = requiredNo = true;
                    break;
                default:
                    throw new ArgumentException(string.Format("buttnos_in: '{0}' is not supported", buttnos_in));
            }

            ConsoleMenuItem mi;
            List<ConsoleMenuItem> items = new List<ConsoleMenuItem>();

            if (requiredOk)
            {
                mi = new ConsoleMenuItem(Resources.DIALOG_RESULT_OK);
                mi.Tag = DialogResult.OK;
                items.Add(mi);
            }
            if (requiredYes)
            {
                mi = new ConsoleMenuItem(Resources.DIALOG_RESULT_YES);
                mi.Tag = DialogResult.Yes;
                items.Add(mi);
            }
            if (requiredNo)
            {
                mi = new ConsoleMenuItem(Resources.DIALOG_RESULT_NO);
                mi.Tag = DialogResult.No;
                items.Add(mi);
            }
            if (requiredRetry)
            {
                mi = new ConsoleMenuItem(Resources.DIALOG_RESULT_RETRY);
                mi.Tag = DialogResult.Retry;
                items.Add(mi);
            }
            if (requiredCancel)
            {
                mi = new ConsoleMenuItem(Resources.DIALOG_RESULT_CANCEL);
                mi.Tag = DialogResult.Cancel;
                items.Add(mi);
            }

            ConsoleMenu cm = new ConsoleMenu(message_in, string.Empty, requiredCancel, enterRequired_in, 0, items);
            
            int? defaultIndex = null;
            if (default_in.HasValue)
            {
                defaultIndex = items.First(item => (DialogResult)item.Tag == default_in.Value).Index;
            }            
            ConsoleMenuItem cmi = cm.DoMenu(defaultIndex, positionY_in);

            return cmi == null ? DialogResult.Cancel : (DialogResult)cmi.Tag;
        }//ConsoleMessage()

        public static void ErrorMessage(string format, params object[] args)
        {
            ErrorMessage(string.Format(format, args));
        }

        /// <summary>
        /// writes error message to console and warning to trace file
        /// </summary>
        /// <param name="msg"></param>
        public static void ErrorMessage(string msg)
        {
            Trace.TraceWarning("Console Error Out: '{0}'", msg.Trim());
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(string.Format(Resources.ERROR_HAPPENED_PARAM, msg));
            System.Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void ResultMessage(string msg, bool Ok)
        {
            Trace.TraceInformation("Console Result Out: '{0}' {1}", msg.Trim(), Ok.ToString());
            if (Ok)
                System.Console.ForegroundColor = ConsoleColor.Green;
            else
                System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(string.Format(msg));
            System.Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void ResultMessageTo(int x, int y, string msg, bool Ok)
        {
            Trace.TraceInformation("Console Result Out: '{0}' {1}", msg.Trim(), Ok.ToString());
            if (Ok)
                System.Console.ForegroundColor = ConsoleColor.Green;
            else
                System.Console.ForegroundColor = ConsoleColor.Red;
            ConsoleHelper.WriteLineTo(x, y, string.Format(msg));
            System.Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WarningMessage(string format, params object[] args)
        {
            WarningMessage(string.Format(format, args));
        }
        /// <summary>
        /// writes error message to console and information to trace file
        /// </summary>
        /// <param name="msg"></param>
        public static void WarningMessage(string msg)
        {
            Trace.TraceWarning("Console Warning Out: '{0}'", msg.Trim());

            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine(msg);
            System.Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void InfoMessage(string format, params object[] args)
        {
            InfoMessage(string.Format(format, args));
        }

        /// <summary>
        /// writes error message to console and information to trace file
        /// </summary>
        /// <param name="msg"></param>
        public static void InfoMessage(string msg)
        {
            Trace.TraceInformation("Console Info Out: '{0}'", msg.Trim());
            System.Console.Write(msg);
            System.Console.BackgroundColor = ConsoleColor.Black; System.Console.WriteLine();
        }
    }
}
