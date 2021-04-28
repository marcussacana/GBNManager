using System;
using System.Text;

namespace GBNManager
{
    public class GBN
    {
        IGBN Manager;

        public GBN(byte[] Script) : this(Script, Encoding.UTF8) { }
        public GBN(byte[] Script, Encoding Encoding) {
            if (Script.Length < 3)
                throw new Exception("Invalid GBN Script");

            bool HeaderInBegin = Script[0] == 0x47 && Script[1] == 0x53 && Script[2] == 0x54;

            Manager = HeaderInBegin ? (IGBN)new GSTR(Script, Encoding) : new GBIN(Script, Encoding);
        }

        public string[] Import() => Manager.Import();
        public byte[] Export(string[] Content) => Manager.Export(Content);

    }

    interface IGBN {
        string[] Import();
        byte[] Export(string[] Content);
    }
}
