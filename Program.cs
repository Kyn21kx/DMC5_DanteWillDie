using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace DanteWillDie {
    class Program {

        #region Variables
        static ProcessMemoryReader memReader;
        const uint MODULE_PTR = 0x07A98340;
        const uint OFFSET_PTR = 10;
        static uint baseAddress;
        static int bytesOut = 0;
        #endregion

        static void Main(string[] args) {
            Initialize();
        }

        private static void Initialize () {
            Process dmc5 = Process.GetProcessesByName("DevilMayCry5").ToList().FirstOrDefault();
            if (dmc5 != null) {
                memReader = new ProcessMemoryReader(dmc5);
                memReader.OpenProcess();
                IntPtr finalModule = (IntPtr)(MODULE_PTR + (ulong)dmc5.MainModule.BaseAddress);
                baseAddress = BitConverter.ToUInt32(memReader.ReadMemory(finalModule, 4, out bytesOut));
            }
            else {
                Exception up = new Exception("The game has not been started or found, please make sure the process is running properly under the name \"DevilMayCry5.exe\"!");
                throw up;
            }
        }
    }
}
