using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace DragAndDrop
{

    [DelimitedRecord(";")]
    class Ctverec
    {
        private int width;
        private int height;
        //private System.Windows.Media.Color color;
        private int x;
        private int y;

        public Ctverec(int width, int height, int x, int y)
        {
            this.width = width;
            this.height = height;
            //this.color = color;
            this.x = x;
            this.y = y;
        }

        public Ctverec()
        {

        }
        
        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }
        /*public System.Windows.Media.Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }*/
        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
    }
}
