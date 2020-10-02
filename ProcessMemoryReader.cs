using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DanteWillDie {
    public class ProcessMemoryReader {
        
        #region Variables
        public Process ReadProcess { get; private set; }
        private IntPtr handle;
        #endregion

        public ProcessMemoryReader (Process readProcess) {
            ReadProcess = readProcess;
        }

        public void OpenProcess () {
            uint access = (uint) GetAccesType(); 
            handle = ReaderApi.OpenProcess(access, 1, (uint)ReadProcess.Id);
        }

        public void CloseHandle () {
            int returnValue = ReaderApi.CloseHandle(handle);
            if (returnValue != 0)
                throw new Exception("Closing handle failed!");
        }

        private ReaderApi.ProcessAccessType GetAccesType () {
            return ReaderApi.ProcessAccessType.PROCESS_QUERY_INFORMATION |
                ReaderApi.ProcessAccessType.PROCESS_VM_READ | ReaderApi.ProcessAccessType.PROCESS_VM_WRITE |
                ReaderApi.ProcessAccessType.PROCESS_VM_OPERATION;
        }

        public byte[] ReadMemory(IntPtr memoryAddress, uint bytesToRead, out int bytesRead) {
            byte[] buffer = new byte[bytesToRead];
            IntPtr pBytesRead = IntPtr.Zero;
            ReaderApi.ReadProcessMemory(handle, memoryAddress, buffer, bytesToRead, out pBytesRead);
            bytesRead = pBytesRead.ToInt32(); //This might be unnecessary
            return buffer;
        }

        public void WriteMemory(IntPtr memoryAddress, byte[] buffer, out int bytesWritten) {
            //Add error handling
            IntPtr pBytesWritten = IntPtr.Zero;
            ReaderApi.WriteProcessMemory(handle, memoryAddress, buffer, (uint)buffer.Length, out pBytesWritten);
            bytesWritten = pBytesWritten.ToInt32();
        }

    }
}
