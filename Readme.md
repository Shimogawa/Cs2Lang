# Cs2Lang

## Alias: Json2Lang

## About

泰拉瑞亚mod文件汉化使用.lang文件，这个软件是对(tModLocalizer)[https://github.com/mistzzt/tmodloader-mod-localizer]的包装。TModLocalizer是将.tmod拆包为.json，而此软件是将产生的.json转换为.lang。

## Usage

### Microsoft User

<font size=4>`.\Cs2Lang.exe mod_file_path mod_namespace [options]`</font>

[options] 包括 -l, -h, -v, -nc。详情请使用`.\Cs2Lang.exe -h`。


## Version info

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

 - 提供可以从json转lang的选项
 - 获取文件中共有名称与描述，将其放入Common



## Credits

感谢@mistzzt (mw)的tModLocalizer