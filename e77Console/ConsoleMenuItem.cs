﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e77.MeasureBase.e77Console
{
    public class ConsoleMenuItem
    {
        /// <summary>
        /// Only Fix string
        /// </summary>
        /// <param name="text_in"></param>
        /// <param name="key_in"></param>
        public ConsoleMenuItem(string text_in) 
            : this(text_in, null, null, ConsoleColor.Gray)
        { ;}

        public ConsoleMenuItem(string text_in, string key_in)
            : this(text_in, key_in, null, ConsoleColor.Gray) { ;}

        public ConsoleMenuItem(string text_in, string key_in, ConsoleColor fixColor_in)
            : this(text_in, key_in, null, fixColor_in) { ;}

        public ConsoleMenuItem(string text_in, string key_in, DoWorkDelegate doWork_in)
            : this(text_in, key_in, doWork_in, ConsoleColor.Gray) { ;}

        public ConsoleMenuItem(string text_in, string key_in, DoWorkDelegate doWork_in, ConsoleColor fixColor_in)
        {
            _fixText = text_in;
            if (key_in != null)
                Key = key_in.ToString();
            else
                Key = string.Empty;

            GetColorCallback = null;
            DoWorkCallback = doWork_in;
            _fixColor = fixColor_in;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text_in"></param>
        /// <param name="key_in">optional, if null: 1. Auto Key Generation: if menuStartInt_in has been set at constuctor of ConsoleMenu. 2. no key at all: user can select with arrow buttons.</param>
        /// <param name="doWork_in">optional, if null: does nothing by default. Retrun value of ConsoleMenu.DoMenu() can be used.</param>
        /// <param name="getColor_in">optional, if null: default color is used.</param>
        public ConsoleMenuItem(GetTextDelegate text_in, string key_in, DoWorkDelegate doWork_in, GetColorDelegate getColor_in)
        {
            if (key_in != null)
                Key = key_in.ToString();
            else
                Key = string.Empty;

            GetTextCallback = text_in;
            GetColorCallback = getColor_in;
            DoWorkCallback = doWork_in;
        }

        public delegate string GetTextDelegate();
        public delegate void DoWorkDelegate();
        public delegate ConsoleColor GetColorDelegate();

        internal void Show(bool selected_in, int textStartPos)
        {
            ConsoleColor orig = Console.ForegroundColor;
            Console.ForegroundColor = Color;

            if (selected_in) //inverse
                ConsoleHelper.InverseColors();

            if (Console.ForegroundColor == Console.BackgroundColor)
                Console.ForegroundColor = (ConsoleColor)(((int)Console.ForegroundColor + 7) & 0xf);

            Console.Write(Key);
            Console.CursorLeft = textStartPos;
            Console.Write( Text);

            if (selected_in) //inverse
                ConsoleHelper.InverseColors();

            Console.ForegroundColor = orig;
            Console.Write("\n");

        }

        /// <summary>
        /// "a" for 'A' or 'a', "12" for '1' and '2'
        /// </summary>
        public string Key { get; set; }
        public GetTextDelegate GetTextCallback;
        public GetColorDelegate GetColorCallback;
        public DoWorkDelegate DoWorkCallback;
        public int Index { get; internal set; } //initialized by ConsoleMenu

        /// <summary>
        /// Custom data
        /// </summary>
        public object Tag { get; set; }

        public string Text
        {
            get
            {
                if (GetTextCallback != null)
                    return GetTextCallback();
                return _fixText;
            }
        }

        public ConsoleColor Color
        {
            get
            {
                if (GetColorCallback != null)
                    return GetColorCallback();
                return _fixColor;
            }
        }

        public override string ToString()
        {
            return Text;
        }

        internal string _fixText;
        internal ConsoleColor _fixColor;
        internal int? AutoGeneratedKey { get; set; } //initialized by ConsoleMenu
        
    }
}
