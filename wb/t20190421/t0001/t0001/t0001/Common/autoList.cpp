/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *unbindBlock2Line(autoList<char> *list)
{
	list->AddElement('\0');
	return unbindBlock(list);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *unbindBlock2Line_NR(autoList<char> *list)
{
	list->AddElement('\0');
	return list->UnbindBuffer();
}
