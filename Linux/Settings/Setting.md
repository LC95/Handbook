# 设置

## 配置国内源

1. 设置官方镜像源
```bash
sudo pacman-mirrors -i -c China -m rank //更新镜像排名
sudo pacman -Syy //更新数据源
pacman -S archlinux-keyring 
```
2. 设置archlinuxcn
```bash
sudo pacman-mirrors -i -c China -m rank //更新镜像排名
sudo pacman -Syy //更新数据源
pacman -S archlinux-keyring 
```

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