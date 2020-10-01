using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DanteWillDie {
    public class ProcessMemoryReader {
        
        #region Variables
        public Process ReadProcess { get; set; }
        private IntPtr handle;
        #endregion

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

    }
}
