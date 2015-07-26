using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeldaStyle.Map {
    struct IDHolder {
        public int X;
        public int Y;
        public int ID;
        public IDHolder(int X, int Y, int ID) {
            this.X = X;
            this.Y = Y;
            this.ID = ID;
        }        
    }
}
