using System;

namespace TestForMindrayWaveDataParse
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] testMessage = { 0x9D, 0xCF, 0xA3, 0xDF, 0xA9, 0xF3, 0xB3, 0xD9 };
            byte[] testMessageTwo= {0x9D, 0xCF, 0xA3, 0xDF, 0xA9, 0xF3, 0xB3, 0xD9, 0x9D, 0xCF, 0xA3, 0xDF, 0xA9, 0xF3, 0xB3, 0xD9};


            WaveDataParser testParser = new WaveDataParser();
            var t1 = testParser.ParseOneDataBlock(testMessage);
            var t2 = testParser.ParseOneMessage(testMessageTwo);

        }
    }
}
