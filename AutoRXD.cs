using System;

public class AutoRxd
{
	public AutoRxd()
	{

        private System.IO.Ports.SerialPort serialPort1 = new SerialPort();
    public delegate byte[] getDate(byte[] bts);
    private getDate mygetDate;
    private string com;

    public AutoRxd(string com)
    {
        this.com = com;
        serialPort1.PortName = com;  //端口号
        serialPort1.BaudRate = 9600;   //比特率
        serialPort1.Parity = Parity.None;//奇偶校验
        serialPort1.StopBits = StopBits.One;//停止位
        serialPort1.ReadTimeout = 1000; //读超时，即在1000内未读到数据就引起超时异常
    }
    #region 发送接收数据
    public void SendMsg(string senStr)
    {

        serialPort1.Open();

        byte[] myByte = StringToByte_2(senStr);

        serialPort1.Write(myByte, 0, myByte.Length);
        serialPort1.Close();

    }
    public string ReadMsg()
    {
        serialPort1.Open();
        string rd = "null ";
        #region 读数据老方法
        ////------------mothed1----
        //int i = serialPort1.ReadBufferSize;
        // byte[] data = Convert.FromBase64String(serialPort1.ReadLine());


        // rd = Encoding.Unicode.GetString(data);
        // //  mothed2
        //int DataLength = serialPort1.BytesToRead;

        //int i = 0;

        //StringBuilder sb = new StringBuilder();

        //while (i  < DataLength)
        //{

        //    byte[] ds = new byte[1024];

        //    int len = serialPort1.Read(ds, 0, 1024);

        // //   sb.Append(Encoding.ASCII.GetString(ds, 0, len));
        //    sb.Append(ByteToString(ds));

        //    i += len;

        //}
        // //  mothed2
        //byte[] data= new byte[serialPort1.BytesToRead];
        //int i = serialPort1.Read(data, 0, serialPort1.BytesToRead);

        //rd = ByteToString(data);

        #endregion

        //  mothed3
        byte[] data = new byte[serialPort1.BytesToRead];
        int i = serialPort1.Read(data, 0, serialPort1.BytesToRead);

        rd = ByteToString(data);
        return rd;

    }
    // string getdate(byte[])

    public void Sp_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
    {
        byte[] readBuffer = new byte[serialPort1.ReadBufferSize];
        serialPort1.Read(readBuffer, 0, readBuffer.Length);
        //this.Invoke(interfaceUpdataHandle, new string[] { Encoding.UTF8.GetString(readBuffer) });

        // this.Invoke(interfaceUpdataHandle, new string[] { ToCom.ByteToString(readBuffer) });
        string s = ToCom.ByteToString(readBuffer);

    }
    #endregion

    ///  <summary>
    /// 串口读(非阻塞方式读串口，直到串口缓冲区中没有数据
    ///  </summary>
    ///  <param name="readBuf">串口数据缓冲 </param>
    ///  <param name="bufRoom">串口数据缓冲空间大小 </param>
    ///  <param name="HowTime">设置串口读放弃时间 </param>
    ///  <param name="ByteTime">字节间隔最大时间 </param>
    ///  <returns>串口实际读入数据个数 </returns>
    public int ReadComm(out Byte[] readBuf, int bufRoom, int HowTime, int ByteTime)
    {
        //throw new System.NotImplementedException();
        readBuf = new Byte[64];
        Array.Clear(readBuf, 0, readBuf.Length);

        int nReadLen, nBytelen;
        if (serialPort1.IsOpen == false)
            return -1;
        nBytelen = 0;
        serialPort1.ReadTimeout = HowTime;


        try
        {
            readBuf[nBytelen] = (byte)serialPort1.ReadByte();
            byte[] bTmp = new byte[1023];
            Array.Clear(bTmp, 0, bTmp.Length);

            nReadLen = ReadBlock(out bTmp, bufRoom - 1, ByteTime);

            if (nReadLen > 0)
            {
                Array.Copy(bTmp, 0, readBuf, 1, nReadLen);
                nBytelen = 1 + nReadLen;

            }
            else if (nReadLen == 0)
                nBytelen = 1;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);

        }

        return nBytelen;
    }

    ///  <summary>
    /// 串口同步读(阻塞方式读串口，直到串口缓冲区中没有数据,靠字符间间隔超时确定没有数据)
    ///  </summary>
    ///  <param name="ReadBuf">串口数据缓冲 </param>
    ///  <param name="ReadRoom">串口数据缓冲空间大小 </param>
    ///  <param name="ByteTime">字节间隔最大时间 </param>
    ///  <returns>从串口实际读入的字节个数 </returns>
    public int ReadBlock(out byte[] ReadBuf, int ReadRoom, int ByteTime)
    {
        //throw new System.NotImplementedException();
        ReadBuf = new byte[1024];
        Array.Clear(ReadBuf, 0, ReadBuf.Length);

        sbyte nBytelen;
        //long nByteRead;

        if (serialPort1.IsOpen == false)
            return 0;
        nBytelen = 0;
        serialPort1.ReadTimeout = ByteTime;

        while (nBytelen < (ReadRoom - 1))
        {
            try
            {
                ReadBuf[nBytelen] = (byte)serialPort1.ReadByte();
                nBytelen++; // add one
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                break;
            }
        }
        ReadBuf[nBytelen] = 0x00;
        return nBytelen;

    }

    #region 格式转换

    ///  <summary>
    /// 字符数组转字符串16进制
    ///  </summary>
    ///  <param name="InBytes"> 二进制字节 </param>
    ///  <returns>类似"01 02 0F" </returns>
    public static string ByteToString(byte[] InBytes)
    {
        string StringOut = "";
        foreach (byte InByte in InBytes)
        {
            StringOut = StringOut + String.Format("{0:X2}", InByte) + " ";
        }

        return StringOut.Trim();
    }
    ///  <summary>
    /// strhex 转字节数组
    ///  </summary>
    ///  <param name="InString">类似"01 02 0F" 用空格分开的  </param>
    ///  <returns> </returns>
    public static byte[] StringToByte(string InString)
    {
        string[] ByteStrings;
        ByteStrings = InString.Split(" ".ToCharArray());
        byte[] ByteOut;
        ByteOut = new byte[ByteStrings.Length];
        for (int i = 0; i <= ByteStrings.Length - 1; i++)
        {
            ByteOut[i] = byte.Parse(ByteStrings[i], System.Globalization.NumberStyles.HexNumber);
        }
        return ByteOut;
    }

    ///  <summary>
    /// strhex 转字节数组
    ///  </summary>
    ///  <param name="InString">类似"01 02 0F" 中间无空格 </param>
    ///  <returns> </returns>
    public static byte[] StringToByte_2(string InString)
    {
        byte[] ByteOut;
        InString = InString.Replace(" ", "");
        try
        {
            string[] ByteStrings = new string[InString.Length / 2];
            int j = 0;
            for (int i = 0; i < ByteStrings.Length; i++)
            {

                ByteStrings[i] = InString.Substring(j, 2);
                j += 2;
            }

            ByteOut = new byte[ByteStrings.Length];
            for (int i = 0; i <= ByteStrings.Length - 1; i++)
            {
                ByteOut[i] = byte.Parse(ByteStrings[i], System.Globalization.NumberStyles.HexNumber);
            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

        return ByteOut;
    }
    ///  <summary>
    /// 字符串 转16进制字符串
    ///  </summary>
    ///  <param name="InString">unico </param>
    ///  <returns>类似“01 0f” </returns>
    public static string Str_To_0X(string InString)
    {
        return ByteToString(UnicodeEncoding.Default.GetBytes(InString));
    }

    #endregion
}
}
