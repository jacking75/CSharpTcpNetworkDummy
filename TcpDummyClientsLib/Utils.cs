﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpDummyClientsLib
{
    public class Utils
    {
        static public Tuple<int,int> MinMaxThreadCount()
        {
            int minWorkThreads, maxWorkThreads = 0;
            int iocpThreads = 0;

            System.Threading.ThreadPool.GetMaxThreads(out minWorkThreads, out iocpThreads);
            System.Threading.ThreadPool.GetMinThreads(out maxWorkThreads, out iocpThreads);

            return Tuple.Create(minWorkThreads, maxWorkThreads);
        }

        static public Tuple<int, int> 나누기_몫과나머지(int 제수, int 피제수)
        {
            int 몫 = (int)(피제수 / 제수);
            int 나머지 = (int)(피제수 % 제수);
            return Tuple.Create(몫, 나머지);
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public enum Status
        {
            STOP = 0,
            PAUSE = 1,
            RUN = 2,
        }

        public struct PacketData
        {
            public Int16 PacketID;
            public Int16 BodySize;
            public byte[] BodyData;
        }

        public static byte[] MakeRandomStringPacket(int minSize=32, int maxSize=512)
        {
            var length = random.Next(minSize, maxSize);
            var text = Utils.RandomString(length);


            Int16 packetId = 241;
            var textLen = (Int16)Encoding.Unicode.GetBytes(text).Length;
            var bodyLen = (Int16)(textLen + 2);

            var sendData = new byte[4 + 2 + textLen];
            Buffer.BlockCopy(BitConverter.GetBytes(packetId), 0, sendData, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(bodyLen), 0, sendData, 2, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(textLen), 0, sendData, 4, 2);
            Buffer.BlockCopy(Encoding.Unicode.GetBytes(text), 0, sendData, 6, textLen);

            return sendData;
        }
    }
}
