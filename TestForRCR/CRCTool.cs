using System;
using System.Collections.Generic;
using System.Text;

namespace TestForRCR
{
    public class CRCTool
    {
        public string Cmd;
        public byte[] GetCRC(string cmdWithoutCRC)
        {
            try
            {
                var bCmd = Encoding.Default.GetBytes(cmdWithoutCRC);
                return GetCRC(bCmd);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public byte[] GetCRC(List <byte> cmdWithoutCRC)
        {
            try
            {
                return GetCRC(cmdWithoutCRC.ToArray());
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public byte[] GetCRC(byte[] cmdWithoutCRC)
        {
            ushort CRCRegister = 0xFFFF;
            var bTmp = new byte[2];
            foreach (var item in cmdWithoutCRC)
            {
                CRCRegister = (ushort)(CRCRegister ^ item);
                for (int i = 0; i < 8; i++)
                {
                    var tmpFlag = CRCRegister & 1;
                    CRCRegister = (ushort)(CRCRegister >> 1);

                    if (tmpFlag == 1)
                    {
                        CRCRegister = (ushort)(CRCRegister ^ (ushort)0xA001);
                    }
                }
            }

            var result = new byte[] { (byte)(CRCRegister & 0xFF), (byte)((CRCRegister >> 8) & 0xFF) };
            return result;
        }
    }
}
