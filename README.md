# StageStrike
A custom Cobalt Strike stager written in C

There's a high possiblity that I haven't looked hard enough, but I have not found an open source stager that works with CS.
So my goal is to make a stager that is simple and that can load into memory stages hosted by CS.
This project was inspired by Mudges own Metasploit payload stager here: https://github.com/rsmudge/metasploit-loader
And explained here https://blog.cobaltstrike.com/2013/06/28/staged-payloads-what-pen-testers-should-know/


I'm guessing most mature teams have their own stager whipped up already, but for ones looking for inspiration, here is one 
implementation using WindowsApi.

I would like to in the future use other methods of executing the stage in memory as outlined by this great blog post:
https://www.contextis.com/en/blog/a-beginners-guide-to-windows-shellcode-execution-techniques

    -Dynamic Allocation of Memory
    -Function Pointer Execution
    -.TEXT-Segment Execution
    -RWX-Hunter Execution
    
But for now it'll just be Dynamic Allocation of memory using Win32.
