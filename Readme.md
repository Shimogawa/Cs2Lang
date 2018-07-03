# Cs2Lang

## Alias: Json2Lang

## About

泰拉瑞亚mod文件汉化使用.lang文件，这个软件是对[tModLocalizer](https://github.com/mistzzt/tmodloader-mod-localizer)的包装。TModLocalizer是将.tmod拆包为.json，而此软件是将产生的.json转换为.lang。

使用了 **Microsoft .Net Framework v4.5.1**。请确保安装了这个或更高版本的 Microsoft .Net Framework。详情请见[巨硬官网](https://microsoft.com)。

## Download

[下载地址](https://github.com/Shimogawa/Cs2Lang/releases)

## Usage

### Microsoft User

<font size=4>`.\Cs2Lang.exe mod_file_path mod_namespace [options]`</font>

[options] 包括 -l, -h, -v, -nc, -r。详情请使用`.\Cs2Lang.exe -h`。

### MacOS User

请使用`brew install mono`安装mono。如果不知道什么是brew，详见[homebrew官网](https://brew.sh/)。

`mono .\Cs2Lang.exe mod_file_path mod_namespace [options]`


## Version info

### Version 0.3.5

 - 去除了空内容替换成类名的功能
 - 现在提供了-r的替换文本功能，需要自行写一个需要替换的单词或词组的列表。用法：
	- `Cs2Lang mod_path mod_namespace -r replacement_text_path`
 - 修复了无法删除ModLocalizer产生的log文件的bug。

### Version 0.3.3

 - 在解包时提供更多日志以供参考
 - 提供保留ModLocalizer的.log选项。
	- **建议开启此功能**：保留日志是一个好习惯。日志多了就定期删除它。
 - 现在日志只会删除当前运行产生的一份日志，不会删除所有日志了。

### Version 0.3.2

 - 提供不清理文件选项。
 - 减少产生的空白行

### Version 0.3.1.7

 - improvements.

### Todo in next versions

 [x] 根据外部文件替换lang文本至Common
 [ ] 提供可以从json转lang的选项
 [ ] 整段识别重复加入Common
 [ ] 识别重复次数

## Credits

感谢 [@mistzzt(mw)](https://github.com/mistzzt) 的 tModLocalizer