/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *xcout(char *format, ...)
{
	char *buffer;
	va_list marker;

	va_start(marker, format);

	for(int size = strlen(format) + 128; ; size *= 2)
	{
		errorCase(IMAX < size);

		buffer = (char *)memAlloc(size + 20);
		int retval = _vsnprintf(buffer, size + 10, format, marker);
		buffer[size + 10] = '\0';

		if(0 <= retval && retval <= size)
			break;

		memFree(buffer);
	}
	va_end(marker);

	return buffer;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *strrm(char *line, size_t extend)
{
	return (char *)memRealloc(line, strlen(line) + 1 + extend);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *strr(char *line)
{
	return strrm(line, 0);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *strx(char *line)
{
	return (char *)memClone(line, strlen(line) + 1);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void strz(char *&buffer, char *line)
{
	memFree(buffer);
	buffer = strx(line);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void strz_x(char *&buffer, char *line)
{
	memFree(buffer);
	buffer = line;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int atoi_x(char *line)
{
	int value = atoi(line);
	memFree(line);
	return value;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
__int64 atoi64_x(char *line)
{
	__int64 value = _atoi64(line);
	memFree(line);
	return value;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *mbs_strrchr(char *str, int chr)
{
	char *ret = NULL;

	for(char *p = str; *p; p = mbsNext(p))
		if(*p == chr)
			ret = p;

	return ret;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int ReplacedFlag;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void replaceChar(char *str, int srcChr, int destChr) // mbs_
{
	for(char *p = str; *p; p = mbsNext(p))
		if(*p == srcChr)
			*p = destChr;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *replacePtn(char *str, char *srcPtn, char *destPtn, int ignoreCase) // ret: strr()
{
	autoList<char> *buff = new autoList<char>();
	int srcPtnLen = strlen(srcPtn);
	int destPtnLen = strlen(destPtn);

	errorCase(srcPtnLen < 1);
	ReplacedFlag = 0;

	for(char *p = str; *p; )
	{
		if((ignoreCase ? _strnicmp : strncmp)(p, srcPtn, srcPtnLen) == 0)
		{
			buff->AddElements(destPtn, destPtnLen);
			ReplacedFlag = 1;
			p += srcPtnLen;
		}
		else
		{
			buff->AddElement(*p);
			p++;
		}
	}
	memFree(str);
	return unbindBlock2Line(buff);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *replacePtnLoop(char *str, char *srcPtn, char *destPtn, int ignoreCase, int loopMax) // ret: strr()
{
	for(int c = 0; c < loopMax; c++)
	{
		str = replacePtn(str, srcPtn, destPtn, ignoreCase);

		if(!ReplacedFlag)
			break;
	}
	return str;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *addLine(char *line, char *addPtn)
{
	line = strrm(line, strlen(addPtn));
	strcat(line, addPtn);
	return line;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *addChar(char *line, int chr)
{
	char chrLine[2];

	chrLine[0] = chr;
	chrLine[1] = '\0';

	return addLine(line, chrLine);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *insertLine(char *line, int index, char *insPtn)
{
	char *trailer;

	errorCase(index < 0 || (int)strlen(line) < index);

	trailer = strx(line + index);

	line = strrm(line, strlen(insPtn));
	strcpy(line + index, insPtn);
	strcat(line, trailer);

	memFree(trailer);
	return line;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *insertChar(char *line, int index, int chr)
{
	char insPtn[2];

	insPtn[0] = chr;
	insPtn[1] = '\0';

	return insertLine(line, index, insPtn);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void reverseLine(char *line)
{
	char *l = line;
	char *r = strchr(line, '\0');

	if(l < r)
	{
		r--;

		while(l < r)
		{
			int tmp = *l;

			*l = *r;
			*r = tmp;

			l++;
			r--;
		}
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *thousandComma(char *line) // ret: strr(line)
{
	uint index;

	reverseLine(line);

	for(index = 3; index < strlen(line); index += 4)
	{
		line = insertChar(line, index, ',');
	}
	reverseLine(line); // •œŒ³
	return line;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void trimLead(char *line, int delimChr)
{
	if(delimChr == '\0') return; // Factory‚Å‚ÍƒGƒ‰[‚É‚µ‚Ä‚¢‚È‚¢B

	if(*line == delimChr)
	{
		char *p;

		for(p = line + 1; *p == delimChr; p++);

		for(char *w = line; *w = *p; w++, p++);
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void trimTrail(char *line, int delimChr)
{
	char *p;

	for(p = strchr(line, '\0'); line < p; p--)
		if(p[-1] != delimChr)
			break;

	*p = '\0';
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void trimSequ(char *line, int delimChr)
{
	char *n = line;

	errorCase(delimChr == '\0');

	while(n = strchr(n, delimChr))
	{
		n++;

		if(*n == delimChr)
		{
			char *f = n;

			do
			{
				f++;
			}
			while(*f == delimChr);

			while(*f)
			{
				if(*f == delimChr)
					while(f[1] == delimChr)
						f++;

				*n++ = *f++;
			}
			*n = '\0';
			break;
		}
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void trim(char *line, int delimChr)
{
	trimTrail(line, delimChr);
	trimLead(line, delimChr);
	trimSequ(line, delimChr);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void trimEdge(char *line, int delimChr)
{
	trimTrail(line, delimChr);
	trimLead(line, delimChr);
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static char *TokPtr;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static uchar *TokDelims;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void tokinit(char *str, char *delims)
{
	if(str)
	{
		TokPtr = str;
		memFree(TokDelims);
		TokDelims = NULL;
	}
	if(delims)
	{
		char *p;

		if(*delims)
		{
			if(!TokDelims)
				TokDelims = (uchar *)memAlloc(32);

			memset(TokDelims, 0x00, 32);

			for(p = delims; ; p++)
			{
				uint delim = (uint)*p;

				TokDelims[delim / 8] |= (1 << (delim % 8));

				if(!delim)
					break;
			}
		}
		else
		{
			memFree(TokDelims);
			TokDelims = NULL;
		}
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *toknext(char *str, char *delims)
{
	char *ret;

	tokinit(str, delims);

	if(!TokPtr)
		return NULL;

	ret = TokPtr;

	if(TokDelims)
	{
		char *p;
		uint upchr;

		for(p = TokPtr; ; p++)
		{
			upchr = (uint)*p;

			if(TokDelims[upchr / 8] & (1u << (upchr % 8)))
				break;
		}
		if(upchr)
		{
			*p = '\0';
			p++;
		}
		else
			p = NULL;

		TokPtr = p;
	}
	else
		TokPtr = NULL;

	return ret;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *ne_toknext(char *str, char *delims)
{
	char *ret = toknext(str, delims);

	errorCase(!ret);
	return ret;
}
