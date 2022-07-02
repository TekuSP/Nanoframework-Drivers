/*
// ----------------------------------------------------------------
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------
// Valon Hoti @ Prishtine // Jul 23 - 2020 
// this class made to work only on debug mode ...
// ----------------------------------------------------------------
// usage :
// ----------------------------------------------------------------

     DebugWritelnToUart debug = new DebugWritelnToUart("COM2", 57600);
                        debug.WriteLine("Hello to uart ! \n"); 


     if you do not want to use more this class

     you can also dispose like 

                       debug.Dispose();

// ----------------------------------------------------------------
// Warning :
// ----------------------------------------------------------------

    you need to choose what to use 

    System.Diagnostics or
    System.Dianogstics.Uart 

    you can not use both, just only ...

 */

using System;

namespace System.Diagnostics.Uart
{
    //-------------------------------------------
    // include references 
    //-------------------------------------------
    //  nanoFramework.Runtime.Events.dll
    //  nanoFramework.System.Text.dll
    //  Windows.Devices.SerialCommunication.dll
    //  Windows.Storage.Stream.dll
    //-------------------------------------------
    using System.Diagnostics;
    using System.IO.Ports;

    using Windows.Storage.Streams;
    public class DebugWritelnToUart : IDisposable
    {
        private SerialPort uart;

        /// <summary>
        ///   Set which COM port you want to use and what baudrate
        /// </summary>
        /// <param name="Comuart"></param>
        /// <param name="BaudRate"></param>
        public DebugWritelnToUart(string Comuart, int BaudRate)
        {
#if DEBUG
            uart = new SerialPort(Comuart);
            uart.WatchChar = '\n';

            uart.BaudRate = BaudRate;
            uart.Parity = Parity.None;
            uart.StopBits = StopBits.One;
            uart.Handshake = Handshake.None;
            uart.DataBits = 8;

            uart.WriteTimeout = 5000;
#endif
        }

        /// <summary>
        ///  use in same way as Debug.Write
        /// </summary>
        /// <param name="message"></param>
        public void Write(string message)
        {
            Debug.Write(message);
#if DEBUG
            uart.WriteLine(message);
#endif
        }

        /// <summary>
        ///  use in same way as Debug.WriteLine
        /// </summary>
        /// <param name="message"></param>
        public void WriteLine(string message)
        {
            Debug.WriteLine(message);
#if DEBUG
            uart.WriteLine(message + "\r\n");
#endif
        }

        /// <summary>
        ///  use in same way as Debug.Assert
        /// </summary>
        /// <param name="condition"></param>
        public void Assert(bool condition)
        {
            Debug.Assert(condition);
#if DEBUG
            uart.WriteLine("Assert[" + condition.ToString() + "]" + "\r\n");
#endif
        }
        /// <summary>
        ///  use in same way as Debug.Assert
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public void Assert(bool condition, string message)
        {
            Debug.Assert(condition, message);
#if DEBUG
            uart.WriteLine("Assert[" + condition.ToString() + "], Message[" + message + "]" + "\r\n");
#endif
        }

        /// <summary>
        ///  use in same way as Debug.Assert
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <param name="detailedMessage"></param>
        public void Assert(bool condition, string message, string detailedMessage)
        {
            Debug.Assert(condition, message, detailedMessage);
#if DEBUG
            uart.WriteLine("Assert[" + condition.ToString() + "], Message[" + message + "], DetailedMessage[" + detailedMessage + "]" + "\r\n");
#endif
        }

        public void Dispose()
        {
            uart.Dispose();
        }
    }
}