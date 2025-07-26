# 多机器人控制

- [多机器人控制](#多机器人控制)
  - [准备工作](#准备工作)
    - [情况一：使用mac或者linux一类的unix系统](#情况一使用mac或者linux一类的unix系统)
    - [情况二：使用中控机器人](#情况二使用中控机器人)
  - [使用方法](#使用方法)
    - [方法一：使用mac或者linux一类的unix系统](#方法一使用mac或者linux一类的unix系统)
    - [方法二：使用中控机器人](#方法二使用中控机器人)
  - [一些重要的信息备份](#一些重要的信息备份)
    - [实验室中已经配置好的中控机器人](#实验室中已经配置好的中控机器人)
    - [路由器相关](#路由器相关)

## 准备工作

因为需要用到expect脚本自动化连接机器人，目前脚本只支持unix类系统，如果要使用Windows，请参考“使用中控机器人”的方法来连接。

当然，欢迎同学开发其他Windows也能使用的方便的工具共享到该仓库。

**首先需要安装expect工具**

### 情况一：使用mac或者linux一类的unix系统

你的机器需要联网，使用系统对应工具安装expect工具

mac系统：

```sh
brew install expect
```

ubuntu：

```sh
apt-get install expect
```

centos:

```sh
yum install expect
```

 ### 情况二：使用中控机器人

【如果中控机器人中已经安装了expect工具，那么下面的1~】

请先准备一个能够联网的WIFI，比如手机热点

1. 连上中控机器人的WIFI

2. 【已经能够连接其他WIFI的机器人可以忽略这步】 通过vnc，先将本项目中assets/MeetingProject放到桌面，运行里面的setFolder.sh

3. 修改机器人里hiwonder-toolbox下的hw_wifi.py文件，翻到最下面，将下面的SSID和PASSWORD改成你可以联网的WIFI名称和密码【注意别改错位置了】

```python
HW_WIFI_STA_SSID = "suferobot"
HW_WIFI_STA_PASSWORD = "N.O.P.E."
```

改为

```python
HW_WIFI_STA_SSID = [你的WIFI名称]
HW_WIFI_STA_PASSWORD = [你的WIFI密码]
```

4. 保存修改后重启机器人

```sh
#在命令行中输入
reboot
```

5. 机器人重启后，应该会正常连接上指定的WIFI，如果机器人背后的LED灯两个都一直常亮，那么直连模式就正常开启了，将计算机链接到和机器人同样的WIFI中，可以通过路由器管理页面查看IP

6. 通过vnc连接机器人，IP输入在路由器中看到的中控机器人IP

7. 通过终端安装expect

```sh
apt-get update
apt-get install expect
```

8. 安装好expect工具后，再通过vnc修改hiwonder-toolbox下的hw_wifi.py文件【同第（3）步】，将wifi设置改回去到时候要用于多台机器人共连的wifi。

**至此expect的准备都弄好了，开始设置服务机和客户机**

1. 服务机

在要用于服务机操作的机器上，拷贝好本项目server-client-scripts中的RobotServer.py文件。

2. 客户机

**如果是有expect工具的mac或者linux系统**，通过scp工具先将meetingProject传到客户机，这里示例中meetingProject被放在了本机的Downloads里面

```sh
scp -r Downloads/meetingProject pi@192.168.149.1:/home/pi/Desktop/
```

然后再在本机终端运行expect脚本（全程不需要用到vnc）

```sh
expect set_client_to_lan.expect
```

**如果没有linux系统的机器，使用windows的话**，那么没有没有很简单的方法，需要一个个通过vnc手动连接，然后上传meetingProject文件夹，在机器人的终端中使用bash运行里面的setFolder.sh脚本

```sh
sudo bash /home/pi/Desktop/assets/meetingProject/setFolder.sh
```

然后再输入reboot命令重启（注意重启的时候路由器需要开着，以便链接）

这样一台机器人就设置好了，用上面的方法将所有机器人都设置好，以连接到路由器。

## 使用方法

### 方法一：使用mac或者linux一类的unix系统

（1）将计算机和客户端机器人都配置好，并连接上中央路由器

通过路由器查看中控机器人的ip，这里示例为`192.168.1.100`

（2）在本机开启server

```sh
# 第一个参数是要将server开在哪个IP
# 这里采用默认端口并开在ip 192.168.1.100，也就是本机的IP
sudo python3 RobotServer.py 192.168.1.100
```

（3）后面的操作与方法二的第（4）步以后的内容相同

### 方法二：使用中控机器人

（1）将中控机器人和客户端机器人都配置好，并连接上中央路由器

通过路由器查看中控机器人的ip，这里示例为`192.168.1.104`

（2）通过vnc连接到已经配置好的中控机器人

（3）把server开启:

```sh
# cd到本仓库中server-client-scripts所在位置，本示例已经将脚本放在了Desktop上
cd Desktop
# 第一个参数是要将server开在哪个IP
# 这里采用默认端口并开在ip 192.168.1.104，也就是中控机的IP
sudo python3 RobotServer.py 192.168.1.104
```

（4）上线所有client机

首先确保所有client机都已经连接到路由器了，通过路由器管理员界面查看ip或者通过ping获取所有client机的ip地址记录下来。

这里示例中举例为其中一台client机的ip地址为`192.168.1.103`

新开一个终端窗口

```sh
# cd到本仓库中bash-scripts所在的位置
cd /home/pi/Desktop/bash-scripts
# 使用expect脚本将所有客户机上线，参数是所有的client机器IP，用逗号隔开
# 如果要同时控制中控机的话，应包括中控机自己的IP
expect client_robot_connect.expect 192.168.1.104  192.168.1.103 
```

（5）查看开启server的那个窗口，如果看到client都上线了，就是正常运行了

（6）最后通过server窗口操作输入指令

指令：

```sh
# [cmd + 具体的shell指令]: 将这个shell指令广播到所有客户机
# 比如下面的命令会再所有上线的客户机执行runDance.py，参数是1，默认是会蹲下
# runDance.py文件可以按需要修改，这个文件的初始版本在本项目assets/MeetingProject中，通过setFolder.sh会自动放在Functions文件夹里面
cmd sudo python3 /home/pi/TonyPi/Functions/runDance.py 1
# [restart]：在可输入状态下输入restart并回车，连接程序会重启，需要重新进行连接
restart
# [stop]: 在可输入状态下输入 stop 并回车，程序会关闭，客户机自动下线
stop
```

## 一些重要的信息备份

### 实验室中已经配置好的中控机器人

中控机编号 76DFDBF8

IP：192.168.1.104

### 路由器相关

路由器已经被重置

> **路由器管理：**
> 访问IP：192.168.1.1
> 密码：suferobot
> 
> **wifi账号密码**
> suferobot
> suferobot@123



