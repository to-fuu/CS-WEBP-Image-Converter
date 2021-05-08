﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


public enum ProgressBarDisplayText
{
    Percentage,
    CustomText
}

//code from https://stackoverflow.com/a/3529945/14190733
public class CustomProgressBar : ProgressBar
{
    //Property to set to decide whether to print a % or Text
    public ProgressBarDisplayText DisplayStyle { get; set; }

    //Property to hold the custom text
    public String CustomText { get; set; }

    public CustomProgressBar()
    {
        // Modify the ControlStyles flags
        //http://msdn.microsoft.com/en-us/library/system.windows.forms.controlstyles.aspx
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Rectangle rect = ClientRectangle;
        Graphics g = e.Graphics;

        ProgressBarRenderer.DrawHorizontalBar(g, rect);
        rect.Inflate(-3, -3);
        if (Value > 0)
        {
            // As we doing this ourselves we need to draw the chunks on the progress bar
            Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
            ProgressBarRenderer.DrawHorizontalChunks(g, clip);
        }

        // Set the Display text (Either a % amount or our custom text
        string text = DisplayStyle == ProgressBarDisplayText.Percentage ? Value.ToString() + '%' : CustomText;


        using (Font f = new Font(FontFamily.GenericSansSerif, 8))
        {

            SizeF len = g.MeasureString(text, f);
            // Calculate the location of the text (the middle of progress bar)
            // Point location = new Point(Convert.ToInt32((rect.Width / 2) - (len.Width / 2)), Convert.ToInt32((rect.Height / 2) - (len.Height / 2)));
            Point location = new Point(Convert.ToInt32((Width / 2) - len.Width / 2), Convert.ToInt32((Height / 2) - len.Height / 2));
            // The commented-out code will centre the text into the highlighted area only. This will centre the text regardless of the highlighted area.
            // Draw the custom text
            g.DrawString(text, f, Brushes.Black, location);
        }
    }
}

