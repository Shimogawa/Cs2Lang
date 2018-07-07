# Cs2Lang

## Alias: Json2Lang

## About

目前版本0.3.6.12

泰拉瑞亚mod文件汉化使用.lang文件，这个软件是对[tModLocalizer](https://github.com/mistzzt/tmodloader-mod-localizer)的包装。TModLocalizer是将.tmod拆包为.json，而此软件是将产生的.json转换为.lang。

使用了 **Microsoft .Net Framework v4.5.1**。请确保安装了这个或更高版本的 Microsoft .Net Framework。详情请见[巨硬官网](https://microsoft.com)。

## Download

[下载地址](https://github.com/Shimogawa/Cs2Lang/releases)

## Usage

### Microsoft User

<font size=4>`Cs2Lang mod_file_path mod_namespace [options]`</font>

[options] 包括 -l, -h, -v, -nc, -r, -j。详情请使用`Cs2Lang -h`。

### MacOS User

请使用`brew install mono`安装mono。如果不知道什么是brew，详见[homebrew官网](https://brew.sh/)。

`mono ./Cs2Lang.exe mod_file_path mod_namespace [options]`

### All Help

#### 1. 帮助文档

`Cs2Lang -h`

#### 2. 版本号

`Cs2Lang -v`

#### 3. 从.tmod文件导出.lang文件

`Cs2Lang mod_file_path mod_namespace [-nc|-l] [-r replacement_file_path]`

|参数名|可选性|用法|
|-----|:-:|-----|
|`mod_file_path`|必选|mod文件路径|
|`mod_namespace`|必选|mod命名空间名称，或其名称（不带空格）。大小写不敏感，但是可能体现在生成的lang文件上|
|`-nc`|可选|不清理任何文件（默认是清理产生的垃圾文件与日志文件）|
|`-l`|可选|不清理日志文件，清理其余文件（如果有`-nc`则忽略此参数）|
|`-r rfp`|可选|使用以`rfp`为路径的文件（文本文件任意格式）中的词句替换.lang中的文本，并将其写入Common|

#### 4. 从json文件夹导出.lang文件

`Cs2Lang json_folder_path [json_folder_name] -j [-nc|-l] [-r replacement_file_path]`

|参数名|可选性|用法|
|-----|:-:|-----|
|`json_folder_path`|必选|json文件夹路径|
|`json_folder_name`|可选|json文件夹名称（或mod命名空间名称），可以省略|
|`-j`|必选|使用json导出lang文件|

#### 5. 替换文本的文件格式

可以是任何后缀的文件名，但文件必须是文本格式，而非二进制文件。格式为：

```
// 以两个斜杠开头的行为注释行
// 下面这一行认作是需要替换的单词或句子
my words
My sentence.

abcdefg
It is good.

// 不支持//在除开头的任何地方，//之前也不能有空格。
// 以后版本可能增加支持
```

示例文本：

```
ItemName.ABC=It is good. That is good.
```

示例替换：

```
Common.Name1=It is good.

ItemName.ABC={$Mods.ThisMod.Common.Name1} That is good.
```



## Version info

### Version 0.3.7

 - 空名称（除MapObject）现在在前一行使用 **===UNKNWN===** 标记，删除在等号后的UNKNOWN。

### Version 0.3.6

 - 增加了从.json文件夹获取.lang的功能
	- 使用`-j`或`--usejson`。
	- 使用此功能可以不提供Mod Namespace，但是生成的文件名称可能为Unknown。
 - 优化了使用体验，增加翻译。
 - 空名称现在不会使用类名代替了，而使用**UNKNOWN**。

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

 - [x] 根据外部文件替换lang文本至Common
 - [x] 提供可以从json转lang的选项
 - [ ] 整段识别重复加入Common
 - [ ] 识别重复次数

## Credits

感谢 [@mistzzt(mw)](https://github.com/mistzzt) 的 tModLocalizer