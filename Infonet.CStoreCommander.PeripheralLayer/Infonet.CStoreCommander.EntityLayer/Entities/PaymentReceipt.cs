using System;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Model
{

    public enum TextWeight : int
    {
        Normal = 0,
        Bold = 1
    }

    public enum TextAlignment : int
    {
        Left = 0,
        Center = 1,
        Right = 2
    }

    public enum TextSize : int
    {
        Normal = 0,
        Large = 1
    }


    public struct ReceiptLine
    {
        public TextSize Size;
        public TextAlignment Alignment;
        public TextWeight Weight;
        public String Text;
        public String TextRight;
    }


    public class PaymentReceipt
    {
        private List<ReceiptLine> _lines;
        private int _paperCutPercentage;
        private string _signatureUrl;

        public PaymentReceipt()
        {
            _lines = new List<ReceiptLine>();
            _paperCutPercentage = 0;
        }

        public string SignatureUrl
        {
            get { return _signatureUrl; }
            set { _signatureUrl = value; }
        }

        public int CutPercentage
        {
            get { return _paperCutPercentage; }
            set { _paperCutPercentage = value; }
        }

        public IList<ReceiptLine> Lines
        {
            get { return _lines; }
        }

        public void AddLineValuePair(string name, string value, TextSize size = TextSize.Large, TextWeight weight = TextWeight.Normal)
        {
            ReceiptLine l = new ReceiptLine();
            l.Text = (name ?? string.Empty);
            l.TextRight = (value ?? string.Empty);
            l.Size = size;
            l.Weight = weight;
            l.Alignment = TextAlignment.Center;

            _lines.Add(l);
        }

        public void AddLine()
        {
            AddLine(string.Empty);
        }

        public void AddLine(string line, TextSize size = TextSize.Normal, TextAlignment align = TextAlignment.Left, TextWeight weight = TextWeight.Normal)
        {
            ReceiptLine l = new ReceiptLine();
            l.Text = (line ?? string.Empty);
            l.Size = size;
            l.Weight = weight;
            l.Alignment = align;
            l.TextRight = null;

            _lines.Add(l);
        }

        public override string ToString()
        {
            string str = "";
            foreach (var l in _lines)
            {
                if (string.IsNullOrEmpty(l.TextRight))
                {
                    str += (l.Text ?? string.Empty) + Environment.NewLine;
                }
                else
                {
                    string text = (l.Text ?? string.Empty);
                    string textRight = (l.TextRight ?? string.Empty);
                    string spaces = "";
                    while (text.Length + textRight.Length + spaces.Length < 40)
                        spaces += " ";

                    str += string.Format("{0}{1}{2}{3}", text, spaces, textRight, Environment.NewLine);
                }
            }

            return str;
        }
    }
}
