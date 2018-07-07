# Cs2Lang

## Alias: Json2Lang

## About

Ŀǰ�汾0.3.6.12

̩������mod�ļ�����ʹ��.lang�ļ����������Ƕ�[tModLocalizer](https://github.com/mistzzt/tmodloader-mod-localizer)�İ�װ��TModLocalizer�ǽ�.tmod���Ϊ.json����������ǽ�������.jsonת��Ϊ.lang��

ʹ���� **Microsoft .Net Framework v4.5.1**����ȷ����װ���������߰汾�� Microsoft .Net Framework���������[��Ӳ����](https://microsoft.com)��

## Download

[���ص�ַ](https://github.com/Shimogawa/Cs2Lang/releases)

## Usage

### Microsoft User

<font size=4>`Cs2Lang mod_file_path mod_namespace [options]`</font>

[options] ���� -l, -h, -v, -nc, -r, -j��������ʹ��`Cs2Lang -h`��

### MacOS User

��ʹ��`brew install mono`��װmono�������֪��ʲô��brew�����[homebrew����](https://brew.sh/)��

`mono ./Cs2Lang.exe mod_file_path mod_namespace [options]`

### All Help

#### 1. �����ĵ�

`Cs2Lang -h`

#### 2. �汾��

`Cs2Lang -v`

#### 3. ��.tmod�ļ�����.lang�ļ�

`Cs2Lang mod_file_path mod_namespace [-nc|-l] [-r replacement_file_path]`

|������|��ѡ��|�÷�|
|-----|:-:|-----|
|`mod_file_path`|��ѡ|mod�ļ�·��|
|`mod_namespace`|��ѡ|mod�����ռ����ƣ��������ƣ������ո񣩡���Сд�����У����ǿ������������ɵ�lang�ļ���|
|`-nc`|��ѡ|�������κ��ļ���Ĭ������������������ļ�����־�ļ���|
|`-l`|��ѡ|��������־�ļ������������ļ��������`-nc`����Դ˲�����|
|`-r rfp`|��ѡ|ʹ����`rfp`Ϊ·�����ļ����ı��ļ������ʽ���еĴʾ��滻.lang�е��ı���������д��Common|

#### 4. ��json�ļ��е���.lang�ļ�

`Cs2Lang json_folder_path [json_folder_name] -j [-nc|-l] [-r replacement_file_path]`

|������|��ѡ��|�÷�|
|-----|:-:|-----|
|`json_folder_path`|��ѡ|json�ļ���·��|
|`json_folder_name`|��ѡ|json�ļ������ƣ���mod�����ռ����ƣ�������ʡ��|
|`-j`|��ѡ|ʹ��json����lang�ļ�|

#### 5. �滻�ı����ļ���ʽ

�������κκ�׺���ļ��������ļ��������ı���ʽ�����Ƕ������ļ�����ʽΪ��

```
// ������б�ܿ�ͷ����Ϊע����
// ������һ����������Ҫ�滻�ĵ��ʻ����
my words
My sentence.

abcdefg
It is good.

// ��֧��//�ڳ���ͷ���κεط���//֮ǰҲ�����пո�
// �Ժ�汾��������֧��
```

ʾ���ı���

```
ItemName.ABC=It is good. That is good.
```

ʾ���滻��

```
Common.Name1=It is good.

ItemName.ABC={$Mods.ThisMod.Common.Name1} That is good.
```



## Version info

### Version 0.3.7

 - �����ƣ���MapObject��������ǰһ��ʹ�� **===UNKNWN===** ��ǣ�ɾ���ڵȺź��UNKNOWN��

### Version 0.3.6

 - �����˴�.json�ļ��л�ȡ.lang�Ĺ���
	- ʹ��`-j`��`--usejson`��
	- ʹ�ô˹��ܿ��Բ��ṩMod Namespace���������ɵ��ļ����ƿ���ΪUnknown��
 - �Ż���ʹ�����飬���ӷ��롣
 - ���������ڲ���ʹ�����������ˣ���ʹ��**UNKNOWN**��

### Version 0.3.5

 - ȥ���˿������滻�������Ĺ���
 - �����ṩ��-r���滻�ı����ܣ���Ҫ����дһ����Ҫ�滻�ĵ��ʻ������б��÷���
	- `Cs2Lang mod_path mod_namespace -r replacement_text_path`
 - �޸����޷�ɾ��ModLocalizer������log�ļ���bug��

### Version 0.3.3

 - �ڽ��ʱ�ṩ������־�Թ��ο�
 - �ṩ����ModLocalizer��.logѡ�
	- **���鿪���˹���**��������־��һ����ϰ�ߡ���־���˾Ͷ���ɾ������
 - ������־ֻ��ɾ����ǰ���в�����һ����־������ɾ��������־�ˡ�

### Version 0.3.2

 - �ṩ�������ļ�ѡ�
 - ���ٲ����Ŀհ���

### Version 0.3.1.7

 - improvements.

### Todo in next versions

 - [x] �����ⲿ�ļ��滻lang�ı���Common
 - [x] �ṩ���Դ�jsonתlang��ѡ��
 - [ ] ����ʶ���ظ�����Common
 - [ ] ʶ���ظ�����

## Credits

��л [@mistzzt(mw)](https://github.com/mistzzt) �� tModLocalizer