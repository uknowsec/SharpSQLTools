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


### References

https://github.com/blackarrowsec/mssqlproxy
