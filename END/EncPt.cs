using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace END
{
    public class EncPt
    {
        private int id;
        private char key;

        public EncPt(int id, char key)
        {
            this.id = id;
            this.key = key;
        }

        public int getID()
        {
            return id;
        }

        public char getKey()
        {
            return key;
        }
    }
}
