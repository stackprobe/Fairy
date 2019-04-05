/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<uchar> *readAllBytes(char *file);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<uchar> *readAllBytes_x(char *file);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int readChar(autoList<uchar> *fileData, int &rIndex);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *readLine(autoList<uchar> *fileData, int &rIndex);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *neReadLine(autoList<uchar> *fileData, int &rIndex);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *nnReadLine(autoList<uchar> *fileData, int &rIndex, char *defaultLine = "");
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<char *> *readLines(autoList<uchar> *fileData);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<char *> *readLines_x(autoList<uchar> *fileData);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeAllBytes(char *file, autoList<uchar> *fileData);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeAllBytes_cx(char *file, autoList<uchar> *fileData);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeChar(autoList<uchar> *fileData, int chr);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeToken(autoList<uchar> *fileData, char *token);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeLine(autoList<uchar> *fileData, char *line);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeLine_x(autoList<uchar> *fileData, char *line);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
uint64 readUI64(autoList<uchar> *fileData, int &rIndex, int width = 8);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
uint readUI32(autoList<uchar> *fileData, int &rIndex, int width = 4);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeUI64(autoList<uchar> *fileData, uint64 value, int width = 8);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeUI32(autoList<uchar> *fileData, uint value, int width = 4);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<uchar> *readBlock(FILE *fp, int width);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<uchar> *readBlock(autoList<uchar> *fileData, int &rIndex, int width);
