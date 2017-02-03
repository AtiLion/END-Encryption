using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace END
{
    public class Cryption
    {
        private List<EncPt> shifts = new List<EncPt>();
        private Random r = new Random();

        private int ascii_start = 33;
        private int ascii_end = 126;
        private int ascii_max = 0;

        public Cryption()
        {
            for (int i = ascii_start; i <= ascii_end; i++)
            {
                shifts.Add(new EncPt(ascii_max + 1, (char)i));
                ascii_max++;
            }
        }

        private char getChr(int pos)
        {
            foreach (EncPt ep in shifts)
            {
                if (ep.getID() == pos)
                {
                    return ep.getKey();
                }
            }
            return 'a';
        }

        private int getPos(char ch)
        {
            foreach (EncPt ep in shifts)
            {
                if (ep.getKey() == ch)
                {
                    return ep.getID();
                }
            }
            return 1;
        }

        private string mod_bytes(string text)
        {
            byte[] by_text = Encoding.Default.GetBytes(text);
            byte[] by_final = new byte[by_text.Length * 2];
            int rnd_shift = r.Next(1, ascii_max);
            int a = 0;
            string final = getChr(rnd_shift).ToString();

            for (int i = 0; i < by_text.Length; i++)
            {
                int rnd_add = r.Next(1, ascii_max);
                by_final[a] = (byte)(by_text[i] + getChr(rnd_add) + getChr(rnd_shift));
                by_final[a + 1] = (byte)getChr(rnd_add);
                a = a + 2;
            }

            final = final + Encoding.Default.GetString(by_final);

            return final;
        }

        private string unmod_bytes(string text)
        {
            byte[] by_text = Encoding.Default.GetBytes(text.Remove(0, 1));
            byte[] by_final = new byte[by_text.Length / 2];
            int rnd_shift = text[0];
            int a = 0;
            string final = "";

            for (int i = 0; i < by_text.Length; i = i + 2)
            {
                int rnd_add = Convert.ToChar(by_text[i + 1]);
                by_final[a] = (byte)(by_text[i] - rnd_add - rnd_shift);
                a++;
            }

            final = Encoding.Default.GetString(by_final);

            return final;
        }

        private string addJunk(string text)
        {
            int rnd = r.Next(1, ascii_max);
            string final = "";

            for (int i = 0; i < rnd; i++)
            {
                char rnd_char = getChr(r.Next(1, ascii_max));
                final = final + rnd_char;
            }

            final = final + text + getChr(rnd).ToString();

            return final;
        }

        private string removeJunk(string text)
        {
            int rnd = getPos(text[text.Length - 1]);
            string final = text.Remove(text.Length - 1, 1);

            final = final.Remove(0, rnd);

            return final;
        }

        private string reverseString(string text)
        {
            char[] final = text.ToCharArray();
            Array.Reverse(final);
            return new string(final);
        }

        public string encrypt(string text)
        {
            string final = text;

            final = addJunk(final);
            final = reverseString(final);
            final = mod_bytes(final);

            return final;
        }

        public string decrypt(string text)
        {
            string final = text;

            final = unmod_bytes(final);
            final = reverseString(final);
            final = removeJunk(final);

            return final;
        }
    }
}
