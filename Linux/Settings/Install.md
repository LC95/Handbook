# 下载和安装

## 下载
`Manjaro`[下载](https://mirrors.tuna.tsinghua.edu.cn/manjaro-cd/)

## windows配置
1. 压缩卷， 为系统提供一片未分配的空间
1. 安装`Rufus`[下载](https://rufus.akeo.ie/)
2. `Rufus`选择DD格式创建USB启动盘
3. 关闭Windows快速启动和BIOS安全启动

## 进入系统
1. 注意选择中国地区
2. 随意安装


```Shell
# 移除旧的keys
sudo rm -rf /etc/pacman.d/gnupg
# 初始化pacman的keys
sudo pacman-key --init
# 加载签名的keys
sudo pacman-key --populate archlinux manjaro
# 刷新升级已经签名的keys
sudo pacman-key --refresh-keys
# 清空并且下载新数据
sudo pacman -Sc
# 更新
sudo pacman -Syu

```