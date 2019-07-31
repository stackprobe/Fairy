/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static autoList<uchar> *LoadFileData(autoList<uchar> *fileData)
{
	return fileData->Eject();
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void UnloadFileData(autoList<uchar> *fileData)
{
	delete fileData;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static oneObject(
	resCluster<autoList<uchar> *>,
	new resCluster<autoList<uchar> *>("Etcetera.dat", "..\\..\\Etcetera.txt", ETC_MAX, 140000000, LoadFileData, UnloadFileData),
	GetEtcRes
	);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<uchar> *GetEtcFileData(int etcId) // ret: åƒÇ—èoÇµë§Ç≈äJï˙ÇµÇ»ÇØÇÍÇŒÇ»ÇÁÇ»Ç¢ÅB
{
	autoList<uchar> *fileData = GetEtcRes()->GetHandle(etcId)->Eject();
	GetEtcRes()->UnloadAllHandle();
	return fileData;
}
