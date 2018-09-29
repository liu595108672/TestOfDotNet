using System;
using System.Collections.Generic;
using NLog;
using System.Text;

namespace TestForMindrayWaveDataParse
{
    public class WaveDataParser
    {
        public byte _cursor = 0x01;
        public byte _compare = 0x00;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public byte[] ParseOneMessage(byte[] message)
        {
            List<byte> resultToReturn = new List<byte>();
            List<byte> tmpMessage = new List<byte>(message);
            if (CheckMessageLength(message))
            {
                int i = 0;
                for (; i < message.Length; i+=8)
                {
                    resultToReturn.AddRange(ParseOneDataBlock(tmpMessage.GetRange(i,8).ToArray()));   
                }
                logger.Trace("Parsing one wave message completed. Parsing {0} DataPackages and the actual wave data length is: {1}", i/8, resultToReturn.Count);
            }
            return resultToReturn.ToArray();
        }


        /// <summary>
        /// Parsing one DataBlock  into original byte array.
        /// From byte[8] To byte[7]
        /// </summary>
        /// <param name="dataBlock"></param>
        /// <returns></returns>
        public byte [] ParseOneDataBlock(byte[] dataBlock)
        {
            //Not much log for this function, due to it is frequently used!
            byte[] resultToReturn = new byte[7];
            if (null != dataBlock && dataBlock.Length == 8)
            {
                //cache the 8th byte of dataBlock
                byte tmpByte8 = dataBlock[7];

                for (int i = 0; i < resultToReturn.Length; i++)
                {
                    _compare = (byte)(_cursor & tmpByte8);
                    if ((_compare&_cursor) >0)
                    {
                        resultToReturn[i] = dataBlock[i];
                    }
                    else
                    {
                        resultToReturn[i] = (byte)(dataBlock[i]&0x7F);
                    }
                    _cursor = (byte)(_cursor<<1);//Move cursor to next bit
                }
            }
            else
            {
                if (null == dataBlock)
                {
                    logger.Warn("The DataBlock to be parsed is null! This case is not a normal situation, better to check source code");
                }else if (dataBlock.Length!=8)
                {
                    logger.Warn("The length of DataBlock is not a multiplier of 8! DataBlock length is: {0}", dataBlock.Length);
                }
            }
            _cursor = 0x01;
            return resultToReturn;
        }


        /// <summary>
        /// If Cursor and Compare is the same, That is the special bit7 is setted
        /// </summary>
        /// <returns></returns>
        public bool Bit7Setted()
        {
            return (_cursor & _compare )>0;
        }

        /// <summary>
        /// Check if the message length is a multiplier of 8
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CheckMessageLength(byte[] message)
        {
            bool resultToReturn = false;
            if (null == message || message.Length == 0) 
            {
                //throw new Exception("The message to be parsed is null or has no item!");
                logger.Error("The message to be parsed is null or has no item!");
                resultToReturn = false;
            }
            else if (message.Length % 8 != 0)
            {
                //throw new Exception(string.Format("The length of message is not correct! Message length should be a multiplier of 8, but actual length is: {0}", message.Length));
                logger.Error("The length of message is not correct! Message length should be a multiplier of 8, but actual length is: {0}", message.Length);
                resultToReturn = false;
            }
            else
            {
                logger.Trace("The message length is: {0} , that is there should be {1} DataBlocks.", message.Length, message.Length / 8);
                resultToReturn = true;
            }
            return resultToReturn ;
        }
    }
}
