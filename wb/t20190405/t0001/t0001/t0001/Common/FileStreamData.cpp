/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<uchar> *readAllBytes(char *file)
{
	FILE *fp = fileOpen(file, "rb");
	int size;

	{
		__int64 size64 = getFileSizeFPSS(fp);
		errorCase((__int64)IMAX < size64); // ? ëÂÇ´Ç∑Ç¨ÇÈÅB
		size = (int)size64;
	}

	uchar *fileData = (uchar *)memAlloc_NC(size);

	if(size)
		errorCase(fread(fileData, 1, size, fp) != size); // ? read error

	fileClose(fp);
	return new autoList<uchar>(fileData, size);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<uchar> *readAllBytes_x(char *file)
{
	autoList<uchar> *out = readAllBytes(file);
	memFree(file);
	return out;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int readChar(autoList<uchar> *fileData, int &rIndex)
{
	if(rIndex < fileData->GetCount())
	{
		return fileData->GetElement(rIndex++);
	}
	return EOF;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *readLine(autoList<uchar> *fileData, int &rIndex)
{
	autoList<char> *line = new autoList<char>();

	while(rIndex < fileData->GetCount())
	{
		int chr = fileData->GetElement(rIndex++);

		if(chr == '\r')
		{
			// ì«Ç›îÚÇŒÇ∑ÅB
		}
		else if(chr == '\n')
		{
			break;
		}
		else
		{
			line->AddElement(chr);
		}
	}
	if(line->GetCount() == 0 && rIndex == fileData->GetCount()) // ? ì«Ç›çûÇ›èIóπ
	{
		delete line;
		return NULL;
	}
	return unbindBlock2Line(line);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *neReadLine(autoList<uchar> *fileData, int &rIndex)
{
	char *line = readLine(fileData, rIndex);
	errorCase(!line);
	return line;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *nnReadLine(autoList<uchar> *fileData, int &rIndex, char *defaultLine)
{
	char *line = readLine(fileData, rIndex);

	if(!line)
		line = strx(defaultLine);

	return line;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<char *> *readLines(autoList<uchar> *fileData)
{
	autoList<char *> *lines = new autoList<char *>();
	int rIndex = 0;

	for(; ; )
	{
		char *line = readLine(fileData, rIndex);

		if(!line)
			break;

		lines->AddElement(line);
	}
	return lines;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<char *> *readLines_x(autoList<uchar> *fileData)
{
	autoList<char *> *lines = readLines(fileData);
	delete fileData;
	return lines;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeAllBytes(char *file, autoList<uchar> *fileData)
{
	FILE *fp = fileOpen(file, "wb");

	errorCase(fwrite(fileData->ElementAt(0), 1, fileData->GetCount(), fp) != fileData->GetCount()); // ? write error

	fileClose(fp);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeAllBytes_cx(char *file, autoList<uchar> *fileData)
{
	writeAllBytes(file, fileData);
	delete fileData;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeChar(autoList<uchar> *fileData, int chr)
{
	fileData->AddElement(chr);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeToken(autoList<uchar> *fileData, char *token)
{
	for(char *p = token; *p; p++)
	{
		fileData->AddElement(*p);
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeLine(autoList<uchar> *fileData, char *line)
{
	writeToken(fileData, line);
	fileData->AddElement('\n');
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeLine_x(autoList<uchar> *fileData, char *line)
{
	writeLine(fileData, line);
	memFree(line);
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
uint64 readUI64(autoList<uchar> *fileData, int &rIndex, int width)
{
	uint64 value = 0;

	for(int c = 0; c < width; c++)
	{
		value |= (uint64)fileData->GetElement(rIndex++) << c * 8;
	}
	return value;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
uint readUI32(autoList<uchar> *fileData, int &rIndex, int width)
{
	return (uint)readUI64(fileData, rIndex, width);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeUI64(autoList<uchar> *fileData, uint64 value, int width)
{
	for(int c = 0; c < width; c++)
	{
		fileData->AddElement((uchar)(value >> c * 8 & 0xff));
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeUI32(autoList<uchar> *fileData, uint value, int width)
{
	writeUI64(fileData, (uint64)value, width);
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<uchar> *readBlock(FILE *fp, int width)
{
	autoList<uchar> *retBlock = new autoList<uchar>();

	for(int index = 0; index < width; index++)
	{
		retBlock->AddElement((uchar)readChar(fp));
	}
	return retBlock;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<uchar> *readBlock(autoList<uchar> *fileData, int &rIndex, int width)
{
	autoList<uchar> *retBlock = new autoList<uchar>();

	for(int index = 0; index < width; index++)
	{
		retBlock->AddElement(fileData->RefElement(rIndex++, 0x00));
	}
	return retBlock;
}
