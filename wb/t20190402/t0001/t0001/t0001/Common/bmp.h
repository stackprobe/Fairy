/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoTable<uint> *readBmpFile(autoList<uchar> *fileData);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoTable<uint> *readBmpFile_x(autoList<uchar> *fileData);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeBmpFile(autoList<uchar> *fileData, autoTable<uint> *bmp);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeBmpFile_x(autoList<uchar> *fileData, autoTable<uint> *bmp);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeBmpFile(char *file, autoTable<uint> *bmp);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeBmpFile_cx(char *file, autoTable<uint> *bmp);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void writeBmpFile_xx(char *file, autoTable<uint> *bmp);
