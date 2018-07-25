# Redis安装

## Windows下
1. Github[下载](https://github.com/MicrosoftArchive/redis/releases)
   > 本文当前版本为3.2.100， 此发行版本基于antirez/redis/3.2.1 附加一些Windows特别的修复, 通过了所有的标准测试但是尚未在生产环境中测试。
2. 安装.msi版本， 选择添加至系统变量， 安装后Redis将作为一项Windows服务运行。默认端口号为6379

## Linux（发行版：Manjaro）下
1. 安装
    1. `sudo pacman -S redis`
    2. `sudo pacman -S redis-desktop-manager`
2. 