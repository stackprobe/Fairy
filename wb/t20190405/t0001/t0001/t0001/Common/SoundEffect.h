/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
enum
{
	SE_DUMMY,

	// app > @ SE_

	// < app

	SE_MAX, // num of member
};

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#define SE_HANDLE_MAX 64

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct SEInfo_st
{
	int HandleList[SE_HANDLE_MAX];
	int HandleIndex;
	double Volume; // 0.0 - 1.0, def: 0.5
	int AlterCommand;
}
SEInfo_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int SEEachFrame(void);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SEPlay(int seId);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void SEStop(int seId);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void UpdateSEVolume(void);
