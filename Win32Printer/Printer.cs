using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace Win32Printer
{
    public class Printer
    {
        private Font printFont;
        private StringReader streamToPrint;
        
        public string ImagePath { get; set; }
        public Printer()
        {
            printFont = new Font("Courier New", 8);
           
        }
        public void PrintDocument(string printString)
        {
            streamToPrint = new StringReader(printString);
            
            PrintDocument pd = new PrintDocument();

            pd.PrintPage += new PrintPageEventHandler(PrintPage);
            // Print the document.
            pd.Print();
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = 0; // ev.MarginBounds.Left;
            float topMargin = 0;  //ev.MarginBounds.Top;
            String line = null;

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics);

            // Iterate over the file, printing each line.
            while (count < linesPerPage &&
               ((line = streamToPrint.ReadLine()) != null))
            {

                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }
            streamToPrint.Close();
            streamToPrint.Dispose();
            if (ImagePath != null)
            {
              Bitmap  img = new Bitmap(ImagePath);
                count++;
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString("\r\n", printFont, Brushes.Black,
                                   leftMargin, yPos, new StringFormat());
                count++;
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString("\r\n", printFont, Brushes.Black,
                                   leftMargin, yPos, new StringFormat());
                count++;
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString("Signature:\r\n", printFont, Brushes.Black,
                                   leftMargin, yPos, new StringFormat());
                count++;
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString("\r\n", printFont, Brushes.Black,
                                   leftMargin, yPos, new StringFormat());
                count++;
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawImage(img, new Rectangle(60, (int)yPos, img.Width, img.Height));


            }

            



            // If more lines exist, print another page.
            //if (line != null)
            //{
            //    ev.HasMorePages = true;
            //}
            //else
            //{
            //    ev.HasMorePages = false;

            //    streamToPrint.Close();
            //    streamToPrint.Dispose();
            //}

            
        }
    }
}
